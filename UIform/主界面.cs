
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
using INIAPI;

namespace UIform
{
    public partial class 主界面 : Form
    {
        #region Halcon 引擎
        HDevEngine m_HDevEngine = new HDevEngine();
        HDevProcedure m_CamProcedure = new HDevProcedure();
        HDevProcedureCall m_CamProcedureCall;

        string m_sHDevEnginePath = string.Empty;

        #endregion

        /// <summary>
        /// 图像处理的最终结果
        /// </summary>
        public bool m_bInspectResult = false;

      
        private int m_iOriFormWidth = 0, m_iOriFormHeight = 0;

        HTuple hv_hWindowHandle;

        HObject ho_Image;
        HObject ho_ObjectOut;

        Thread m_AutoRunThread;

        public SendMsgDelegate MySendMsgDelegate;

        public static 主界面 Form1Single = null;

        private 主界面()
        {
            InitializeComponent();

            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ObjectOut);

            m_sHDevEnginePath = Global.di + "\\HE";

            #region 标记窗体原有尺寸

            hv_hWindowHandle = hWindowControl1.HalconWindow;

            HOperatorSet.SetColor(hv_hWindowHandle, "red");

            ListControl(this);
            m_iOriFormWidth = this.Width;
            m_iOriFormHeight = this.Height;

            #endregion

        }

        public static 主界面 GetSingle()
        {
            if (Form1Single == null || Form1Single.IsDisposed == true)
            {
                Form1Single = new 主界面();
            }
            return Form1Single;
        }


        private void 主界面_Load(object sender, EventArgs e)
        {
            #region 加载hdvp文件
            try
            {
                m_HDevEngine.SetProcedurePath(m_sHDevEnginePath);
                m_CamProcedure = new HDevProcedure("_829test");
                m_CamProcedureCall = m_CamProcedure.CreateCall();
            }
            catch (Exception df)
            {
                string s = "a";
                // Fun_Add_lb_Info("文件读取失败!" + df.Message);
            }
            #endregion

            #region 开启作业线程
            m_AutoRunThread = new Thread(Fun_AutoRun);
            m_AutoRunThread.IsBackground = true;
            m_AutoRunThread.Start();
            Console.WriteLine("自动作业线程已开启！" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            #endregion
        }

        HTuple result = new HTuple();
        public void Fun_AutoRun()
        {
            while (true)
            {
                if (CommonClass.m_bStartAutoRun == true)
                {
                    #region 图像处理
                    //测试显示图片

                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, Global.di + "\\1.bmp");

                    HTuple h, w;
                    HOperatorSet.GetImageSize(ho_Image, out w, out h);
                    HOperatorSet.SetPart(hv_hWindowHandle, 0, 0, h, w);
                    HOperatorSet.DispObj(ho_Image, hv_hWindowHandle);

                    ho_ObjectOut.Dispose();
                    CommonClass.Cam1Procedure(m_CamProcedureCall, hv_hWindowHandle, ho_Image, out ho_ObjectOut);
                    HOperatorSet.DispObj(ho_ObjectOut, hv_hWindowHandle);

                    #endregion

                    #region 图像保存
                    Thread.Sleep(50);

                    #endregion

                    #region 向UI界面发送处理结果

                    result = new HTuple();
                    CommonClass.m_TotalCount++;

                    result[1] = CommonClass.m_TotalCount;
                    result[2] = CommonClass.m_OKCount;
                    result[3] = CommonClass.m_NGCount;
                    if (result[1] % 2 == 0)
                    {
                        result[0] = "OK";
                        CommonClass.m_OKCount++;
                    }
                    else
                    {
                        result[0] = "NG";
                        CommonClass.m_NGCount++;
                    }

                    //将当前产量保存到本地
                    Fun_WriteIni(CommonClass.m_sLiaohao, "TotalCount", CommonClass.m_TotalCount.ToString());
                    Fun_WriteIni(CommonClass.m_sLiaohao, "OKCount", CommonClass.m_OKCount.ToString());
                    Fun_WriteIni(CommonClass.m_sLiaohao, "NGCount", CommonClass.m_NGCount.ToString());

                    //将合格率的计算也放到该界面中，将计算结果输出到UI界面
                    result[4] = 100 * (double)CommonClass.m_OKCount / CommonClass.m_TotalCount;

                    if (MySendMsgDelegate != null)
                    {
                        MySendMsgDelegate(result);
                    }

                    #endregion
                }
                Thread.Sleep(5);
            }
        }

        public void Fun_WriteIni(string liaohaoPath, string keyName, string value)
        {
            if (liaohaoPath != string.Empty)
            {
                IniAPI.INIWriteValue(Global.m_sLiaoHaoPath + "\\" + liaohaoPath + "\\System.ini", "SYSTEM", keyName, value);
            }
        }

        private void 清除所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void 加载最后一次数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 主界面.dt.ReadXml("F:\\表格临时数据\\dt.xml");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 主界面_Shown(object sender, EventArgs e)
        {
            // wel.Start();
            //测试显示图片
            //ho_Image.Dispose();
            //HOperatorSet.ReadImage(out ho_Image, Application.StartupPath + "\\1.bmp");
            //HTuple h, w;
            //HOperatorSet.GetImageSize(ho_Image, out w, out h);
            //HOperatorSet.SetPart(hv_hWindowHandle, 0, 0, h, w);
            //HOperatorSet.DispObj(ho_Image, hv_hWindowHandle);

            //    autoResize();
            //this.dataGridView1.EnableHeadersVisualStyles = false;
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            //dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewCellStyle1.BackColor = System.Drawing.Color.Pink;
            //this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            //foreach (DataGridViewColumn dc in dataGridView1.Columns)
            //{
            //    dc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //    dc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //}

            //Thread.Sleep(200);
            //wel.Stop();
        }

        private void 主界面_SizeChanged(object sender, EventArgs e)
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
                            // control.Controls[i].Font = new System.Drawing.Font("宋体", (float)(fSize * (WRatio + HRatio) / 2));
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
                    case 2:

                        #region 显示相机2原图
                        /*
                        if (m_ho_RotateImage2.IsInitialized() == true)
                        {
                            if (m_ho_RotateImage2.CountObj() != 0)
                            {
                                HOperatorSet.ClearWindow(hWindowControlID);
                                HOperatorSet.DispObj(m_ho_RotateImage2, hWindowControlID);
                            }
                        }
                        */
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

    }
}
