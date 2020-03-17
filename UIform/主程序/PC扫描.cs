using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myEvent;
using OmroPlcVar;
namespace VisionFram3
{
    public partial class PC扫描 : Form
    {
        public PC扫描()
        {
            InitializeComponent();
            //串口.onExceptionV += new OnExceptionVEventHandler(串口_onExceptionV);
            串口.onLogEvent += new OnLogEventHandler(串口_onLogEvent);
            //PLC扫描.onExceptionV += new OnExceptionVEventHandler(PLC扫描_onExceptionV);
            PLC扫描.onLogEvent += new OnLogEventHandler(PLC扫描_onLogEvent);
            //电子秤扫描.onExceptionV += new OnExceptionVEventHandler(电子秤扫描_onExceptionV);
            电子秤扫描.onLogEvent += new OnLogEventHandler(电子秤扫描_onLogEvent);
            指令执行.onLogEvent += new OnLogEventHandler(指令执行_onLogEvent);
            状态控制.onLogEvent += new OnLogEventHandler(状态控制_OnLogEvent);
            FormMain.onLogEvent += new OnLogEventHandler(FormMain_OnLogEvent);
            OmroPLCvar.onLogEvent += new OnLogEventHandler(OmroPLCvar_onLogEvent);
            ATL扫描.onLogEvent += new OnLogEventHandler(ATL扫描_onLogEvent);
            登录.onLogEvent += new OnLogEventHandler(登录_onLogEvent);
            开机验证.onLogEvent += new OnLogEventHandler(开机验证_onLogEvent);
            点检.onLogEvent += new OnLogEventHandler(点检_onLogEvent);
            主界面.onLogEvent += new OnLogEventHandler(主界面_onLogEvent);
        }


        private void PcScan_log_Action(OnLogEventArgs e)
        {

            Program.FormSaveTODatabase.TaskSaveInfoToAccess(e.LogStr);
            if (e.LogStr.Contains("ERR") && (Program.FormExcuteCmd.vrStatemode == StateMode.AutoRun||Program.FormExcuteCmd.vrStatemode == StateMode.Init))
            {
                Program.FormExcuteCmd.vrStatemode = StateMode.ErrPC;
            }
            if (e.LogStr.Contains("ERR_B"))
            {
                MessageBox.Show(e.LogStr, "系统内部错误");
            }
        }

        private void 主界面_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void PLC扫描_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 串口_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 电子秤扫描_onExceptionV(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 电子秤扫描_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 指令执行_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);

        }

        private void 状态控制_OnLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);

        }
        private void FormMain_OnLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void OmroPLCvar_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);

        }

        private void ATL扫描_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 登录_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }
         private void 开机验证_onLogEvent(OnLogEventArgs e)
        {
            PcScan_log_Action(e);
        }

        private void 点检_onLogEvent(OnLogEventArgs e)
         {
             PcScan_log_Action(e);
         }
    }
}
