using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
using System.IO;
using Timer = System.Windows.Forms.Timer;

namespace UIform
{
    public delegate void SendMsgDelegate(HTuple hv_msg);

    public partial class FormMain : Form

    {
        
        /// <summary>
        /// 正在自动运行标志位
        /// </summary>
        public bool m_bIsAutoRun = false;

        /// <summary>
        /// 相机采集句柄。由UI界面打开相机后，传递到主窗体和SetForm窗体
        /// </summary>
        HTuple hv_AcqHandle;

        /// <summary>
        /// 相机打开正常
        /// </summary>
        public bool m_bCamOpenOK = false;

        StartControl.Welcom wel = new StartControl.Welcom();


        Timer OpacyTimer = new Timer();

        public FormMain()
        {
            wel.Start();
            Global g = new Global();
            wel.TopMost = true;
            OpacyTimer.Tick += new EventHandler(OpacyTimer_Tick);
            OpacyTimer.Interval = 20;
          
            Opacity = 0;
            InitializeComponent();
            OpacyTimer.Start();

            Global.mainform.MySendMsgDelegate += this.SendMsg;
        }
        private void OpacyTimer_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {
                OpacyTimer.Stop();
                OpacyTimer.Tick -= new EventHandler(OpacyTimer_Tick);
            }
            else
            {
                Opacity += 0.2;
            }
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Add(Global.loghelper);//添加日志窗口到窗体
            newPanel1.Show(Global.mainform);//显示主界面



            Thread.Sleep(1900);
            wel.Stop();

            #region 加载料号，得到当前常用料号。

            #endregion

            #region  打开相机，得到hv_AcqHandle。
            Thread.Sleep(500);
            // ConnectDivice();
            if (m_bCamOpenOK == true)
            {
                CommonClass.hv_AcqHandle = hv_AcqHandle;
            }
            #endregion

        }
        private void ConnectDivice()
        {
            try
            {
                //  string CameraDevice = IniAPI.INIGetStringValue(filepath, "CAMERA", "C_DEVICE", "Cam1");
                //打开相机及配置
                HOperatorSet.CloseAllFramegrabbers();
                HOperatorSet.OpenFramegrabber("GigEVision", 1, 1, 0, 0, 0, 0, "default", -1,
                 "default", -1, "false", "default", "Cam1",
                  0, -1, out hv_AcqHandle);

                //  panel3.BackColor = Color.Green;
                m_bCamOpenOK = true;
            }
            catch (Exception eCam)
            {
                MessageBox.Show("未能成功打开相机，可能导致问题的原因是：相机被占用，相机网线未插上或者相机未通电！");
                m_bCamOpenOK = false;
            }
            try
            {
                if (m_bCamOpenOK == true)
                {
                    //ho_Image.Dispose();
                    //HOperatorSet.GrabImage(out ho_Image, hv_AcqHandle);
                    //GOLABLE.m_AcqHandle1 = hv_AcqHandle;
                    //try
                    //{
                    //    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Off");
                    //    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "GainAuto", "Off");
                    //}
                    //catch (Exception) { }
                    //hv_ExposureTime = Convert.ToInt32(INIAPI.IniAPI.INIGetStringValue(filepath, "SYSTEM", "exposure", null));
                    //hv_Gain = Convert.ToInt32(INIAPI.IniAPI.INIGetStringValue(filepath, "SYSTEM", "gain", null));
                    //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", hv_ExposureTime);
                    //HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "Gain", hv_Gain);

                    //panel3.BackColor = Color.Green;
                }
            }
            catch (Exception eaa)
            {
                MessageBox.Show("在初始化相机参数时出错，请检查！");
                // btn_OnlineDevice.Enabled = true;
            }
        }


        private void Btn_Set_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.setting);

            if (m_bCamOpenOK == true)
            {
                Global.setting.m_bCamOpenOk = true;
            }
            else
            {
                Global.setting.m_bCamOpenOk = false;
            }

        }

        private void Btn_Home_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.mainform);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.dataForm);
        }

        private void Btn_Admin_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.login);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.product);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        public void SendMsg(HTuple hv_Msg)
        {
            this.Invoke((EventHandler)(delegate
            {
                button1.Text = hv_Msg.TupleSelect(0).S.ToString();
                button1.BackColor = (hv_Msg.TupleSelect(0).S == "OK" ? Color.Green : Color.Red);

                textBox1.Text = hv_Msg.TupleSelect(1).I.ToString();
            }));
        }



        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (m_bIsAutoRun == false)
            {
                m_bIsAutoRun = true;
                CommonClass.m_bStartAutoRun = true;
                label29.BackColor = Color.Green;
                label33.BackColor = Color.Red;
            }
            else
            {
                MessageBox.Show("程序已经在自动运行中，禁止重复启动！", "操作异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            if (m_bIsAutoRun == true)
            {
                m_bIsAutoRun = false;
                CommonClass.m_bStartAutoRun = false;
                label29.BackColor = Color.Red;
                label33.BackColor = Color.Green;
            }
        }
    }
}
