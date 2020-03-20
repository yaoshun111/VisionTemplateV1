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
using INIAPI;


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


        /// <summary>
        /// 配置文件中读取的常用型号
        /// </summary>
        public string m_sLiaoHao = string.Empty;


        public HTuple hv_Exposure;

        public HTuple hv_Gain;

        public int m_TotalCount = 0;
        public int m_OKCount = 0;
        public int m_NGCount = 0;


        StartControl.Welcom wel = new StartControl.Welcom();


        Timer OpacyTimer = new Timer();

        public FormMain()
        {
            wel.Start();
            Global g = new Global();
            this.TopMost = true;
          
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

            #region 加载型号，得到当前常型号，然后读取对应的曝光和增益，读取当前型号的产量信息。

            m_sLiaoHao = IniAPI.INIGetStringValue(Global.m_sConfigPath + "\\System.ini", "SYSTEM", "常用料号", "");
            int a1 = 0;
            int a2 = 0;

            if (m_sLiaoHao != string.Empty)
            {
                label3.Text = m_sLiaoHao;
                CommonClass.m_sLiaohao = m_sLiaoHao;

                Fun_ReadIni(Global.m_sLiaoHaoPath + "\\" + m_sLiaoHao, "Exposure", out a1);
                hv_Exposure = a1;

                Fun_ReadIni(Global.m_sLiaoHaoPath + "\\" + m_sLiaoHao, "Gain", out a2);
                hv_Gain = a2;

                Fun_ReadIni(Global.m_sLiaoHaoPath + "\\" + m_sLiaoHao, "TotalCount", out CommonClass.m_TotalCount);
                tb_TotalCount.Text = CommonClass.m_TotalCount.ToString();
                Fun_ReadIni(Global.m_sLiaoHaoPath + "\\" + m_sLiaoHao, "OKCount", out CommonClass.m_OKCount);
                tb_OKCount.Text = CommonClass.m_OKCount.ToString();
                Fun_ReadIni(Global.m_sLiaoHaoPath + "\\" + m_sLiaoHao, "NGCount", out CommonClass.m_NGCount);
                tb_NGCount.Text = CommonClass.m_NGCount.ToString();
            }
            else
            {
                CommonClass.m_sLiaohao = string.Empty;
            }
            #endregion

            #region  打开相机，得到hv_AcqHandle。
            Thread.Sleep(500);
            // ConnectDivice();
            if (m_bCamOpenOK == true)
            {
                CommonClass.hv_AcqHandle = hv_AcqHandle;

                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", hv_Exposure);
                HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "Gain", hv_Gain);
            }
            #endregion

        }


        private void ConnectDivice()
        {
            try
            {
                //打开相机及配置
                HOperatorSet.CloseAllFramegrabbers();
                HOperatorSet.OpenFramegrabber("GigEVision", 1, 1, 0, 0, 0, 0, "default", -1,
                 "default", -1, "false", "default", "Cam1", 0, -1, out hv_AcqHandle);

                //  panel3.BackColor = Color.Green;
                m_bCamOpenOK = true;
            }
            catch (Exception eCam)
            {
                MessageBox.Show("未能成功打开相机，可能导致问题的原因是：相机被占用，相机网线未插上或者相机未通电！");
                m_bCamOpenOK = false;
            }

        }


        public void Fun_ReadIni(string liaohaopath, string keyname, out int hv_ParamsOut)
        {
            hv_ParamsOut = 0;
            bool b1 = false;
            try
            {
                string pathtemp = liaohaopath + "\\System.ini";

                switch (keyname)
                {
                    case "Exposure":
                        b1 = int.TryParse(IniAPI.INIGetStringValue(pathtemp, "SYSTEM", keyname, "5000"), out hv_ParamsOut);
                        break;

                    case "Gain":
                        b1 = int.TryParse(IniAPI.INIGetStringValue(pathtemp, "SYSTEM", keyname, "10"), out hv_ParamsOut);
                        break;

                    case "TotalCount":
                        b1 = int.TryParse(IniAPI.INIGetStringValue(pathtemp, "SYSTEM", keyname, "0"), out hv_ParamsOut);
                        break;

                    case "OKCount":
                        b1 = int.TryParse(IniAPI.INIGetStringValue(pathtemp, "SYSTEM", keyname, "0"), out hv_ParamsOut);
                        break;

                    case "NGCount":
                        b1 = int.TryParse(IniAPI.INIGetStringValue(pathtemp, "SYSTEM", keyname, "0"), out hv_ParamsOut);
                        break;
                }

                if (!b1)
                {
                    switch (keyname)
                    {
                        case "Exposure":
                            hv_ParamsOut = 5000;
                            break;

                        case "Gain":
                            hv_ParamsOut = 10;
                            break;

                        case "TotalCount":
                            hv_ParamsOut = 0;
                            break;

                        case "OKCount":
                            hv_ParamsOut = 0;
                            break;

                        case "NGCount":
                            hv_ParamsOut = 0;
                            break;
                    }
                }

            }
            catch (Exception df)
            {
                if (keyname == "Exposure")
                {
                    hv_ParamsOut = 5000;
                }
                else if (keyname == "Gain")
                {
                    hv_ParamsOut = 10;
                }
                else if (keyname == "TotalCount")
                {
                    hv_ParamsOut = 0;
                }
                else if (keyname == "OKCount")
                {
                    hv_ParamsOut = 0;
                }
                else
                {
                    hv_ParamsOut = 0;
                }
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
            this.TopMost = false;
        }

        public void SendMsg(HTuple hv_Msg)
        {
            this.Invoke(new EventHandler(delegate
            {
                Btn_ResultColor.Text = hv_Msg.TupleSelect(0).S.ToString();
                Btn_ResultColor.BackColor = (hv_Msg.TupleSelect(0).S == "OK" ? Color.Green : Color.Red);

                tb_TotalCount.Text = hv_Msg.TupleSelect(1).I.ToString();
                tb_OKCount.Text = hv_Msg.TupleSelect(2).I.ToString();
                tb_NGCount.Text = hv_Msg.TupleSelect(3).I.ToString();

                tb_PassRate.Text = hv_Msg.TupleSelect(4).D.ToString("f2") + "%";
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
