using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INIAPI;
using System.IO;
using HalconDotNet;



namespace UIform
{
    public partial class SettingForm : Form
    {
        HObject ho_Image;
        HTuple hv_hWindowHandle;

        HTuple hv_Gain;

        HTuple hv_Exposure;
        private int m_iOriFormWidth = 0, m_iOriFormHeight = 0;

        public string ConfigPath = Application.StartupPath + "\\Config";
        public bool m_bCamOpenOk { get; set; }


        public static SettingForm SetFormSingle = null;

        private SettingForm()
        {
            InitializeComponent();


            #region 标记窗体原有尺寸

            hv_hWindowHandle = hWindowControl1.HalconWindow;

            HOperatorSet.SetColor(hv_hWindowHandle, "red");

            ListControl(this);
            m_iOriFormWidth = this.Width;
            m_iOriFormHeight = this.Height;

            #endregion

            HOperatorSet.GenEmptyObj(out ho_Image);
            hv_hWindowHandle = hWindowControl1.HalconWindow;
        }

        public static SettingForm GetSingle()
        {
            if (SetFormSingle == null || SetFormSingle.IsDisposed == true)
            {
                SetFormSingle = new SettingForm();
            }

            return SetFormSingle;

        }

        private void SetForm_Load(object sender, EventArgs e)
        {
            #region 初始化ComboBox控件
            string PathLiaoHao = Global.m_sLiaoHaoPath;
            if (!Directory.Exists(PathLiaoHao))
            {
                Directory.CreateDirectory(PathLiaoHao);
            }

            comBox_TypeNow.Items.Clear();
            string[] str = null;
            using (FileStream fsRead = new FileStream(Global.m_sLiaoHaoPath + "\\LiaoHao.ini", FileMode.Open))
            {
                int fsLen = (int)fsRead.Length;
                byte[] heByte = new byte[fsLen];
                int r = fsRead.Read(heByte, 0, heByte.Length);
                string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                str = myStr.Split(',');
            }
            for (int i = 0; i < str.Length - 1; i++)
            {
                comBox_TypeNow.Items.Add(str[i]);
            }

            string S1 = "";
            S1 = IniAPI.INIGetStringValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "常用料号", "");
            if (S1 != "")
            {
                comBox_TypeNow.Text = S1;
            }

            #endregion

            #region 读取曝光和增益的最大值

            int IntexposureMax = 0;
            int IntgainMax = 0;

            string StrexposureMax = string.Empty;
            string StrgainMax = string.Empty;

            StrexposureMax = IniAPI.INIGetStringValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "ExposureMax", "10000");
            StrgainMax = IniAPI.INIGetStringValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "GainMax", "15");

            bool b1 = int.TryParse(StrexposureMax, out IntexposureMax);
            if (b1 != true)
            {
                IntexposureMax = 10000;
            }
            numUD_Exposure.Maximum = IntexposureMax;
            bool b2 = int.TryParse(StrgainMax, out IntgainMax);
            if (b2 != true)
            {
                IntgainMax = 15;
            }
            numUD_Gain.Maximum = IntgainMax;

            IniAPI.INIWriteValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "ExposureMax", StrexposureMax);
            IniAPI.INIWriteValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "GainMax", StrgainMax);

            #endregion

        }

        private void btn_DeleteOld_Click(object sender, EventArgs e)
        {
            string typeNow = "";
            typeNow = comBox_TypeNow.Text;

            if (comBox_TypeNow.SelectedIndex > -1 && comBox_TypeNow.SelectedIndex < comBox_TypeNow.Items.Count)
            {
                comBox_TypeNow.Items.RemoveAt(comBox_TypeNow.SelectedIndex);
            }

            string pth = Application.StartupPath + "\\" + typeNow;
            if (Directory.Exists(pth))
            {
                string[] fileNumber = Directory.GetFiles(pth);
                for (int i = 0; i < fileNumber.Length; i++)
                {
                    File.Delete(fileNumber[i]);
                }
                //目录不是空的会报错
                Directory.Delete(pth);
            }

            using (FileStream fsWrite = new FileStream(Global.m_sLiaoHaoPath + "\\LiaoHao.ini", FileMode.Create))
            {
                fsWrite.Write(System.Text.Encoding.UTF8.GetBytes(""), 0, 0);
            };

            for (int i = 0; i < comBox_TypeNow.Items.Count; i++)
            {
                comBox_TypeNow.SelectedIndex = i;
                string w = comBox_TypeNow.SelectedItem.ToString();
                string msg = comBox_TypeNow.SelectedItem.ToString() + ",";
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg);

                using (FileStream fsWrite = new FileStream(Global.m_sLiaoHaoPath + "\\LiaoHao.ini", FileMode.Append))
                {
                    fsWrite.Write(myByte, 0, myByte.Length);
                };
            }
            MessageBox.Show("删除成功!");
        }

        private void Btn_SetUsual_Click(object sender, EventArgs e)
        {
            DialogResult Dr1 = MessageBox.Show("确定要将当前料号设为常用料号吗?", "操作提示", MessageBoxButtons.OKCancel);
            if (Dr1 == DialogResult.OK && comBox_TypeNow.Text != string.Empty)
            {
                IniAPI.INIWriteValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "常用料号", comBox_TypeNow.Text);
            }
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {
            if (tb_NewType.Text != string.Empty)
            {
                string st = tb_NewType.Text.ToUpper();

                #region 527修改  判断当前料号是否存在
                string stPath = Global.m_sLiaoHaoPath + "\\" + st;
                string[] filePath = Directory.GetDirectories(Global.m_sLiaoHaoPath);
                for (int i = 0; i < filePath.Length; i++)
                {
                    if (filePath[i] == stPath)
                    {
                        MessageBox.Show("料号" + "【" + st + "】" + "已经存在，请重新命名。", "操作提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion

                comBox_TypeNow.Items.Add(st);
                string pth = Global.m_sLiaoHaoPath + "\\" + st;
                if (!Directory.Exists(pth))
                {
                    Directory.CreateDirectory(pth);
                }

                using (FileStream fsWrite = new FileStream(Global.m_sLiaoHaoPath + "\\LiaoHao.ini", FileMode.Create))
                {
                    fsWrite.Write(System.Text.Encoding.UTF8.GetBytes(""), 0, 0);
                };

                for (int i = 0; i < comBox_TypeNow.Items.Count; i++)
                {
                    comBox_TypeNow.SelectedIndex = i;

                    string w = comBox_TypeNow.SelectedItem.ToString();
                    string msg = comBox_TypeNow.SelectedItem.ToString() + ",";
                    byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg);

                    using (FileStream fsWrite = new FileStream(Global.m_sLiaoHaoPath + "\\LiaoHao.ini", FileMode.Append))
                    {
                        fsWrite.Write(myByte, 0, myByte.Length);
                    };
                }
            }
        }

        private void Btn_ReadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog flg = new OpenFileDialog();
            flg.Filter = "(*jpeg;*bmp;*jpg;*tif)|*jpeg;*bmp;*jpg;*tif";
            if (flg.ShowDialog() == DialogResult.OK)
            {
                string filepath = flg.FileName;
                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, filepath);
                CommonClass.Set_Disp_Obj(hv_hWindowHandle, ho_Image);
            }
        }

        private void Btn_SaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog Sfd = new SaveFileDialog();
            Sfd.Filter = "(*.bmp;*.jpg;*.jpeg;*.tif;*.png)|*.bmp;*.jpg;*.jpeg;*.tif;*.png";
            if (Sfd.ShowDialog() == DialogResult.OK)
            {
                CommonClass.Write_image(ref ho_Image, Sfd.FileName);
            }
            Sfd.Dispose();
        }

        private void Btn_GrabSingle_Click(object sender, EventArgs e)
        {
            if (m_bCamOpenOk == true)
            {
                try
                {
                    ho_Image.Dispose();
                    HOperatorSet.GrabImage(out ho_Image, CommonClass.hv_AcqHandle);
                    CommonClass.Set_Disp_Obj(hv_hWindowHandle, ho_Image);
                }
                catch (Exception df)
                {
                    MessageBox.Show("没有联机，操作无效！");
                }
            }
        }

        private void Btn_GrabContinue_Click(object sender, EventArgs e)
        {
            if (m_bCamOpenOk == true)
            {

            }
        }

        private void Btn_SaveExGa_Click(object sender, EventArgs e)
        {
            int exp = Convert.ToInt32(numUD_Exposure.Value.ToString()); ;
            int gain = Convert.ToInt32(numUD_Gain.Value.ToString());

            if (comBox_TypeNow.Text != string.Empty)
            {
                string liaohaotemp = Global.m_sLiaoHaoPath + "\\" + comBox_TypeNow.Text + "\\System.ini";
                IniAPI.INIWriteValue(liaohaotemp, "SYSTEM", "Exposure", exp.ToString());
                IniAPI.INIWriteValue(liaohaotemp, "SYSTEM", "Gain", gain.ToString());
            }
            else
            {
                MessageBox.Show("请先加载料号，然后重新保存", "料号不能为空", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            hv_Exposure = Convert.ToInt32(numUD_Exposure.Value.ToString());
            hv_Gain = Convert.ToInt32(numUD_Gain.Value.ToString());

            if (m_bCamOpenOk == true)
            {
                try
                {
                    HOperatorSet.SetFramegrabberParam(CommonClass.hv_AcqHandle, "ExposureTime", hv_Exposure);
                    HOperatorSet.SetFramegrabberParam(CommonClass.hv_AcqHandle, "Gain", hv_Gain);
                }
                catch (Exception df)
                {

                }
            }
        }

        private void Btn_ReadExGa_Click(object sender, EventArgs e)
        {
            if (comBox_TypeNow.Text == string.Empty)
            {
                MessageBox.Show("请先加载料号，然后重新读取", "料号不能为空", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string liaohaotemp = Global.m_sLiaoHaoPath + "\\" + comBox_TypeNow.Text + "\\System.ini";
                string StrExposureNow = string.Empty;
                string StrGainNow = string.Empty;
                int IntEposureNow = 0;
                int IntGainNow = 0;
                StrExposureNow = IniAPI.INIGetStringValue(liaohaotemp, "SYSTEM", "Exposure", "5000");
                StrGainNow = IniAPI.INIGetStringValue(liaohaotemp, "SYSTEM", "Gain", "5");

                bool b1 = int.TryParse(StrExposureNow, out IntEposureNow);
                if (!b1)
                {
                    IntEposureNow = (int)numUD_Exposure.Maximum;
                }
                bool b2 = int.TryParse(StrGainNow, out IntGainNow);
                if (!b2)
                {
                    IntGainNow = (int)numUD_Gain.Maximum;
                }
                numUD_Exposure.Value = IntEposureNow;
                numUD_Gain.Value = IntGainNow;
            }
        }

        private void ListControl(Control control, int Index = 0, double WRatio = 1.0, double HRatio = 1.0)
        {
            try
            {
                int Count = control.Controls.Count;

                if (Count < 0)
                {
                    return;
                }

                for (int i = 0; i < Count; i++)
                {
                    switch (Index)
                    {
                        case 0:

                            int width = control.Controls[i].Width;
                            int height = control.Controls[i].Height;
                            int left = control.Controls[i].Left;
                            int top = control.Controls[i].Top;
                            float size = control.Controls[i].Font.Size;
                            control.Controls[i].Tag = width.ToString() + "," + height.ToString() + "," +
                                                      left.ToString() + "," + top.ToString() + "," + size.ToString();
                            ListControl(control.Controls[i], Index);

                            break;
                        case 1:

                            MessageBox.Show(control.Controls[i].ToString() + ":" + control.Controls[i].Tag.ToString());
                            ListControl(control.Controls[i], Index);

                            break;

                        case 2:
                            //提取控件原始 width height
                            string tag = control.Controls[i].Tag.ToString();
                            string[] strs = tag.Split(',');
                            int iWidth = int.Parse(strs[0]);
                            int iHeight = int.Parse(strs[1]);
                            int iLeft = int.Parse(strs[2]);
                            int iTop = int.Parse(strs[3]);
                            float fSize = float.Parse(strs[4]);
                            control.Controls[i].Width = (int)(iWidth * WRatio);
                            control.Controls[i].Height = (int)(iHeight * HRatio);
                            control.Controls[i].Left = (int)(iLeft * WRatio);
                            control.Controls[i].Top = (int)(iTop * HRatio);
                            control.Controls[i].Font = new System.Drawing.Font("宋体", (float)(fSize * (WRatio + HRatio) / 2));
                            ListControl(control.Controls[i], Index, WRatio, HRatio);

                            break;

                        default:
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ListControl" + ex.Message);
            }
        }

        private void FlushWindow(HTuple hWindowControlID, int Index = 0)
        {
            try
            {
                switch (Index)
                {
                    case 1:

                        #region 显示相机1原图
                        if (ho_Image.IsInitialized() == true)
                        {
                            if (ho_Image.CountObj() != 0)
                            {
                                HOperatorSet.ClearWindow(hWindowControlID);
                                HOperatorSet.DispObj(ho_Image, hWindowControlID);
                            }
                        }
                        #endregion

                        break;

                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("FlushWindow:" + ex.Message);
            }

        }

        private void SettingForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                #region 根据窗体原有尺寸进行对应比例缩放

                int iFormWidth = this.Width;
                int iFormHeight = this.Height;
                double WRatio = 1.0 * iFormWidth / m_iOriFormWidth;
                double HRatio = 1.0 * iFormHeight / m_iOriFormHeight;

                ListControl(this, 2, WRatio, HRatio);

                FlushWindow(hv_hWindowHandle, 1);

                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("CHalconTemplate_SizeChanged:" + ex.Message);
            }
        }
    }
}
