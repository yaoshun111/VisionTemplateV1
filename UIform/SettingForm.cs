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

        public string ConfigPath = Application.StartupPath + "\\Config";
        public bool m_bCamOpenOk { get; set; }


        public static SettingForm SetFormSingle = null;

        private SettingForm()
        {
            InitializeComponent();

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
            string PathLiaoHao = Application.StartupPath + "\\Config\\LiaoHao";
            if (!Directory.Exists(PathLiaoHao))
            {
                Directory.CreateDirectory(PathLiaoHao);
            }

            comBox_TypeNow.Items.Clear();
            string[] str = null;
            using (FileStream fsRead = new FileStream(ConfigPath + "\\LiaoHao\\LiaoHao.ini", FileMode.Open))
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
            S1 = IniAPI.INIGetStringValue(Application.StartupPath + "\\Config\\System.ini", "SYSTEM", "常用料号", "");
            if (S1 != "")
            {
                comBox_TypeNow.Text = S1;
            }

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

            string pth = Application.StartupPath + "\\Config\\LiaoHao\\" + typeNow;
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

            using (FileStream fsWrite = new FileStream(ConfigPath + "\\LiaoHao\\LiaoHao.ini", FileMode.Create))
            {
                fsWrite.Write(System.Text.Encoding.UTF8.GetBytes(""), 0, 0);
            };

            for (int i = 0; i < comBox_TypeNow.Items.Count; i++)
            {
                comBox_TypeNow.SelectedIndex = i;
                string w = comBox_TypeNow.SelectedItem.ToString();
                string msg = comBox_TypeNow.SelectedItem.ToString() + ",";
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg);

                using (FileStream fsWrite = new FileStream(ConfigPath + "\\LiaoHao\\LiaoHao.ini", FileMode.Append))
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
                IniAPI.INIWriteValue(Application.StartupPath + "\\Config\\System.ini", "SYSTEM", "常用料号", comBox_TypeNow.Text);
            }
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {
            if (tb_NewType.Text != string.Empty)
            {
                string st = tb_NewType.Text.ToUpper();

                #region 527修改  判断当前料号是否存在
                string stPath = Directory.GetCurrentDirectory() + "\\Config\\LiaoHao" + "\\" + st;
                string[] filePath = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\Config\\LiaoHao");
                for (int i = 0; i < filePath.Length; i++)
                {
                    if (filePath[i] == stPath)
                    {
                        MessageBox.Show("料号" + "【" + st + "】" + "已经存在", "操作提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion

                comBox_TypeNow.Items.Add(st);
                string pth = Application.StartupPath + "\\Config\\LiaoHao\\" + st;
                if (!Directory.Exists(pth))
                {
                    Directory.CreateDirectory(pth);
                }

                using (FileStream fsWrite = new FileStream(ConfigPath + "\\LiaoHao\\LiaoHao.ini", FileMode.Create))
                {
                    fsWrite.Write(System.Text.Encoding.UTF8.GetBytes(""), 0, 0);
                };

                for (int i = 0; i < comBox_TypeNow.Items.Count; i++)
                {
                    comBox_TypeNow.SelectedIndex = i;

                    string w = comBox_TypeNow.SelectedItem.ToString();
                    string msg = comBox_TypeNow.SelectedItem.ToString() + ",";
                    byte[] myByte = System.Text.Encoding.UTF8.GetBytes(msg);

                    using (FileStream fsWrite = new FileStream(ConfigPath + "\\LiaoHao\\LiaoHao.ini", FileMode.Append))
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
                // m_isMayZoom = true;
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
                catch(Exception df)
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
    }
}
