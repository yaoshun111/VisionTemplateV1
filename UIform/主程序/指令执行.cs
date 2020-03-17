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
using System.Threading;
using OmroPlcVar;
using System.IO;
using DataAction;
namespace VisionFram3
{
    public partial class 指令执行 : Form
    {
        public static Task TaskAutoRun;
        public static event OnLogEventHandler onLogEvent;
        public static event EventHandler StateModeChanged;
        private StateMode _vrStatemode = StateMode.Stop;
        public static float Q_weight1 = 0;
        public static float Q_weight2 = 0;
        public static float H_weight1 = 0;
        public static float H_weight2 = 0;

        public static string Cell_ID1 = "0000";
        public static string Cell_ID2 = "0000";
        public static string Cell_ID3 = "0000";
        public static string Cell_ID4 = "0000";

        public static int Needle1_A = 0;
        public static int Needle2_A = 0;
        public static float Press_A = 0f;
        public static float Temp_A = 0f;
        public static float Inject_A = 0f;

        public static int Needle1_B = 0;
        public static int Needle2_B = 0;
        public static float Press_B = 0f;
        public static float Temp_B = 0f;
        public static float Inject_B = 0f;

        public static string 弹夹条码 = "0000000";
        public static string I_Cell_ID = "0000000";
        public static string I_Box_ID = "0000000";

        List<object[]> bihuanlist0 = new List<object[]>();
        List<object[]> bihuanlist1 = new List<object[]>();
        List<object[]> bihuanlist2 = new List<object[]>();
        List<object[]> bihuanlist3 = new List<object[]>();


      public static  List<object[]> chartlist0 = new List<object[]>();
      public static  List<object[]> chartlist1 = new List<object[]>();
       public static List<object[]> chartlist2 = new List<object[]>();
      public static  List<object[]> chartlist3 = new List<object[]>();

        public static List<float> cpklist1 = new List<float>();
        public static List<float> cpklist2 = new List<float>();
        public static List<float> cpklist3 = new List<float>();
        public static List<float> cpklist4 = new List<float>();

        int k = 0;

        public static string path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_0.csv";
        Task task1, task2, task3, task4, task5, task6, task7, task8, task9, task10, task11, task12, task13, task14;


        public static bool autoSelect = false;
        public static bool autoCreate = false;

        //int x = 1;
        int x1 = 0, x2 = 0, x3 = 0, x4 = 0;

        public static int txtcount = 500;

        public static bool autorun_first = true;

        public static int atemp = 0;

        public StateMode vrStatemode
        {
            get { return _vrStatemode; }
            set
            {
                if (_vrStatemode == value)
                    return;
                _vrStatemode = value;
                onLogEvent(new OnLogEventArgs("LOG:状态切换到" + Enum.GetName(typeof(StateMode), _vrStatemode)));
                if (StateModeChanged != null)
                {
                    StateModeChanged(null, EventArgs.Empty);
                }
            }
        }
        public 指令执行()
        {
            InitializeComponent();
            Init();


            int m = 0;
            while (File.Exists(path))
            {
                m++;
                path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
            }
        }

        public void Init()
        {

        }

        public void TASKRun()
        {
            TaskAutoRun = new Task(new Action(Autorun));
            TaskAutoRun.Start();
            
        }

        public void Autorun()
        {
            Thread.CurrentThread.Name = "Autorun";
            int initorder = 0;
            int Q_1 = 0, Q_2 = 0, H_1 = 0, H_2 = 0;
            int try_q1 = 0, try_q2 = 0, try_h1 = 0, try_h2 = 0;
            int try_cq1 = 0, try_cq2 = 0, try_ch1 = 0, try_ch2 = 0;
            bool taskQ1_flag, taskQ2_flag, taskH1_flag, taskH2_flag;
            DialogResult retdt = DialogResult.None;
           

            while (true)
            {
                //Application.DoEvents();
                switch (_vrStatemode)
                {
                    case StateMode.Init:
                        Thread.Sleep(500);
                        /////////初始化///////////
                        switch (initorder)
                        {
                            case 0:
                                Program.FormSerialPort1.Init();
                                Program.FormSerialPort2.Init();
                                Program.FormSerialPort3.Init();
                                Program.FormSerialPort4.Init();
                                Program.FormSerialPort5.Init();
                                Program.FormSerialPort6.Init();
                                initorder++;
                                break;
                            case 1:
                                Task task11 = new Task(() =>
                                {
                                    Program.FormAndscan1.Init();
                                });
                                Task task12 = new Task(() =>
                                {
                                    Program.FormAndscan2.Init();
                                });
                                Task task13 = new Task(() =>
                                {
                                    Program.FormAndscan3.Init();
                                });
                                Task task14 = new Task(() =>
                                {
                                    Program.FormAndscan4.Init();
                                });
                                task11.Start();task12.Start();
                                task13.Start();task14.Start();
                                Task.WaitAll(task11, task12, task13, task14);
                                initorder++;
                                break;
                            case 2:
                                Program.FormPLCscan.Init();
                                initorder++;
                                break;
                            case 3:
                                vrStatemode = StateMode.InitOver;
                                initorder = 0;
                                break;
                        }
                        break;
                        //////////////////////////////////////
                    case StateMode.InitDebug:
                        Thread.Sleep(500);
                        //vrStatemode = StateMode.InitOver;  //手动调试,模拟初始化成功.
                        break;
                    case StateMode.InitOver:
                        Thread.Sleep(500);
                        vrStatemode = StateMode.Login;
                        break;
                    case StateMode.Login:
                        Thread.Sleep(500);
                        if (Program.FormLogIn.LogInFlag)
                        {
                            vrStatemode = StateMode.LogSuccess;
                        }
                        //_vrStatemode = StateMode.AutoRun;
                        break;
                    case StateMode.LogSuccess:
                        Thread.Sleep(500);
                        vrStatemode = StateMode.PointCheck;
                        break;
                    case StateMode.PointCheck:
                        Thread.Sleep(500);
                        if ((点检.pointcheck1 & 点检.pointcheck2& 点检.pointcheck3 & 点检.pointcheck4)||点检.pointcheckOK)
                        {
                            
                        }
                        break;
                    case StateMode.StartCheck1:
                        Thread.Sleep(500);
                        if (开机验证.StartCheck1)
                        {
                            vrStatemode = StateMode.StartCheck2;
                        }
                        break;
                    case StateMode.StartCheck2:
                        Thread.Sleep(500);
                        if (开机验证.StartCheck2)
                        {
                           
                        }
                        break;
                    case StateMode.ReadyToAuto:
                        Thread.Sleep(500);
                        break;
                    case StateMode.AutoRun:
                        Thread.Sleep(10);
                        if (主界面.dt.Rows.Count == 0 && retdt == DialogResult.None &&autorun_first)
                        {
                            autorun_first = false;
                            Program.FormMain1.Invoke(new Action(() =>
                            {
                                //retdt = MessageBox.Show("是否导入上次数据？\r\n--注意：请确保整机已经处于清料完毕状态下,选择否！", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
                                retdt = DialogResult.Yes;
                                if (retdt == DialogResult.Yes)
                                {
                                    retdt = DialogResult.None;
                                    主界面.dt.Clear();
                                    try
                                    {
                                        主界面.dt.ReadXml("F:\\表格临时数据\\dt.xml");
                                    }
                                    catch
                                    {
                                        主界面.dt.Clear();
                                        主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");
                                    }
                                }
                                else
                                {
                                    Save.SaveTxt("F:\\8.txt", "0,0", false);
                                    Save.SaveTxt("F:\\9.txt", "0,0", false);
                                    Save.SaveTxt("F:\\10.txt","0,0", false);
                                    Save.SaveTxt("F:\\11.txt", "0,0", false);
                                }
                            }));
                        }

                        //try
                        //{
                        if (Program.FormPLCscan.前电子秤1清零信号)
                        {
                            if (task1 != null)
                            {
                                if (!task1.IsCompleted)
                                    //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                    break;
                            }
                            Program.FormPLCscan.前电子秤1清零信号 = false;
                            task1 = new Task(new Action(() =>
                           {
                               taskQ1_flag = false;
                               Q_1++;
                               if (Q_1 >= 设置界面.清零周期)
                               {
                                   Q_1 = 0;
                                   //////////////Program.FormAndscan1.SendMessage("R\r\n");
                                   //////////////Thread.Sleep(2000);   //////////////////////?????????????????电子秤 2 秒时间清零???????????????????
                                   //////////////try
                                   //////////////{
                                   //////////////    Program.FormAndscan1.SendBackMessage("S\r\n");
                                   //////////////    OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1监测信号名称, 5);
                                   //////////////    onLogEvent(new OnLogEventArgs("LOG:前电子秤1清零！"));
                                   //////////////}
                                   //////////////catch
                                   //////////////{
                                   //////////////    onLogEvent(new OnLogEventArgs("ERR:前电子秤1清零失败！"));
                                   //////////////}
                                   try
                                   {
                                       Program.FormAndscan1.ClearV();
                                       OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1监测信号名称, 5);
                                       try_cq1 = 0;
                                       onLogEvent(new OnLogEventArgs("LOG:前电子秤1清零！"));
                                   }
                                   catch(Exception exp)
                                   {
                                       try_cq1++;
                                       if (try_cq1 <= 4)
                                       {
                                           task1.Wait(500);
                                           Program.FormPLCscan.前电子秤1清零信号 = true;
                                           onLogEvent(new OnLogEventArgs("WARNING:前电子秤1清零失败！" + exp.Message + "[第" + try_cq1 + "次尝试]"));

                                       }
                                       else
                                       {
                                           OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1监测信号名称, -1);
                                           onLogEvent(new OnLogEventArgs("WARNING:前电子秤1清零失败！[第" + (try_cq1 - 1) + "次尝试]"));
                                           try_cq1 = 0;
                                       }
                                   }
                               }
                               else
                               {
                                   OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1监测信号名称, 5);
                               }
                               taskQ1_flag = true;
                           }));
                            task1.Start();
                        }



                            if (Program.FormPLCscan.前电子秤2清零信号 )
                            {
                                if (task2 != null)
                                {
                                    if (!task2.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                Program.FormPLCscan.前电子秤2清零信号 = false;
                                 task2 = new Task(new Action(() =>
                                {
                                    taskQ2_flag = false;
                                    Q_2++;
                                    if (Q_2 >= 设置界面.清零周期)
                                    {
                                        Q_2 = 0;
                                        ////////Program.FormAndscan2.SendMessage("R\r\n");
                                        ////////Thread.Sleep(2000);   //////////////////////????????????????????????????????????
                                        ////////try
                                        ////////{
                                        ////////    Program.FormAndscan2.SendBackMessage("S\r\n");
                                        ////////    OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2监测信号名称, 5);
                                        ////////    onLogEvent(new OnLogEventArgs("LOG:前电子秤2清零！"));

                                        ////////}
                                        ////////catch
                                        ////////{
                                        ////////    onLogEvent(new OnLogEventArgs("ERR:前电子秤2清零失败！"));
                                        ////////}
                                        try
                                        {
                                            Program.FormAndscan2.ClearV();
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2监测信号名称, 5);
                                            try_cq2 = 0;
                                            onLogEvent(new OnLogEventArgs("LOG:前电子秤2清零！"));
                                        }
                                        catch(Exception exp)
                                        {
                                            try_cq2++;
                                            if (try_cq2 <= 4)
                                            {
                                                task2.Wait(500);
                                                Program.FormPLCscan.前电子秤2清零信号 = true;
                                                onLogEvent(new OnLogEventArgs("WARNING:前电子秤2清零失败！" + exp.Message + "[第" + try_cq2 + "次尝试]"));

                                            }
                                            else
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2监测信号名称, -1);
                                                onLogEvent(new OnLogEventArgs("WARNING:前电子秤2清零失败！[第" + (try_cq2 - 1) + "次尝试]"));
                                                try_cq2 = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2监测信号名称, 5);
                                    }
                                    taskQ2_flag = true;
                                }));
                                task2.Start();
                            }

                            if (Program.FormPLCscan.后电子秤1清零信号 )
                            {
                                if (task3 != null)
                                {
                                    if (!task3.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                Program.FormPLCscan.后电子秤1清零信号 = false;
                                task3 = new Task(new Action(() =>
                                {
                                    H_1++;
                                    if (H_1 >= 设置界面.清零周期)
                                    {
                                        H_1 = 0;
                                        //////////Program.FormAndscan3.SendMessage("R\r\n");
                                        //////////Thread.Sleep(2000);   //////////////////////????????????????????????????????????
                                        //////////try
                                        //////////{
                                        //////////    Program.FormAndscan3.SendBackMessage("S\r\n");
                                        //////////    OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1监测信号名称, 5);
                                        //////////    onLogEvent(new OnLogEventArgs("LOG:后电子秤1清零！"));
                                        //////////}
                                        //////////catch
                                        //////////{
                                        //////////    onLogEvent(new OnLogEventArgs("ERR:后电子秤1清零失败！"));
                                        //////////}
                                        try
                                        {
                                            Program.FormAndscan3.ClearV();
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1监测信号名称, 5);
                                            try_ch1 = 0;
                                            onLogEvent(new OnLogEventArgs("LOG:后电子秤1清零！"));
                                        }
                                        catch(Exception exp)
                                        {
                                            try_ch1++;
                                            if (try_ch1 <= 4)
                                            {
                                                task3.Wait(500);
                                                Program.FormPLCscan.后电子秤1清零信号 = true;
                                                onLogEvent(new OnLogEventArgs("WARNING:后电子秤1清零失败！" + exp.Message + "[第" + try_ch1 + "次尝试]"));

                                            }
                                            else
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1监测信号名称, -1);
                                                onLogEvent(new OnLogEventArgs("WARNING:后电子秤1清零失败！[第" + (try_ch1 - 1) + "次尝试]"));
                                                try_ch1 = 0;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1监测信号名称, 5);
                                    }
                                    taskH1_flag = true;
                                }));
                                task3.Start();
                            }

                            if (Program.FormPLCscan.后电子秤2清零信号 )
                            {
                                if (task4 != null)
                                {
                                    if (!task4.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                Program.FormPLCscan.后电子秤2清零信号 = false;
                                task4 = new Task(new Action(() =>
                                {
                                    H_2++;
                                    if (H_2 >= 设置界面.清零周期)
                                    {
                                        H_2 = 0;
                                        ////////////Program.FormAndscan4.SendMessage("R\r\n");
                                        ////////////Thread.Sleep(2000);   //////////////////////????????????????????????????????????
                                        ////////////try
                                        ////////////{
                                        ////////////    Program.FormAndscan4.SendBackMessage("S\r\n");
                                        ////////////    OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, 5);
                                        ////////////    onLogEvent(new OnLogEventArgs("LOG:后电子秤2清零！"));
                                        ////////////}
                                        ////////////catch
                                        ////////////{
                                        ////////////    onLogEvent(new OnLogEventArgs("ERR:后电子秤2清零失败！"));
                                        ////////////}
                                        try
                                        {
                                            Program.FormAndscan4.ClearV();
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, 5);
                                            try_ch2 = 0;
                                            onLogEvent(new OnLogEventArgs("LOG:后电子秤2清零！"));
                                        }
                                        catch(Exception exp)
                                        {
                                            try_ch2++;
                                            if (try_ch2 <= 4)
                                            {
                                                task4.Wait(500);
                                                Program.FormPLCscan.后电子秤2清零信号 = true;
                                                onLogEvent(new OnLogEventArgs("WARNING:后电子秤2清零失败！" + exp.Message + "[第" + try_ch2 + "次尝试]"));

                                            }
                                            else
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, -1);
                                                onLogEvent(new OnLogEventArgs("WARNING:后电子秤2清零失败！[第" + (try_ch2 - 1) + "次尝试]"));
                                                try_ch2 = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, 5);
                                    }
                                    taskH2_flag = true;
                                }));
                                task4.Start();
                            }
                            if (Program.FormPLCscan.前电子秤1触发信号 )
                            {
                                if (task1 != null)
                                {
                                    if (!task1.IsCompleted)
                                        break;
                                }
                                //bool ret = OmroPLCvar1.WriteVarible(OmroPLCvar1.VarListName[i], 2);//读取到信号1后置位为2，表示正在处理PLC请求
                                ///////////////////////////////**********************************************************************
                                //if (!ret)
                                //    return;
                                if (task5 != null)
                                {
                                    if (!task5.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                Program.FormPLCscan.前电子秤1触发信号 = false;

                                Console.WriteLine(Program.stopwatch.Elapsed.TotalMilliseconds.ToString());
                                Program.stopwatch.Restart();
                                task5 = new Task(new Action(() =>
                                    {
                                        try
                                        {
                                            string ret1 = Program.FormAndscan1.SendBackMessage("S\r\n"); //当电子秤1被触发时，读取电子秤
                                            Q_weight1 = float.Parse(ret1);
                                            

                                            
                                            
                                            object obj_ret1 = new object();
                                            Console.WriteLine(Program.stopwatch.Elapsed.TotalMilliseconds.ToString());
                                            //Program.stopwatch.Reset();
                                            //Program.stopwatch1.Start();
                                            OmroPLCvar.ReadSingleVarible(PLC扫描.前电子秤1数据内存名称[0], ref obj_ret1);
                                            Cell_ID1 = obj_ret1 as string;
                                            Program.stopwatch.Stop();
                                            Console.WriteLine("ForeAND1:" + Program.stopwatch.Elapsed.TotalMilliseconds.ToString());
                                            try_q1 = 0;
                                            if (Q_weight1 <= FormMain.dianchi_max && Q_weight1 >= FormMain.dianchi_min)
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1数据内存名称[1], "6");//写入plc数据内存ok

                                            }
                                            else
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1数据内存名称[1], "-10");//写入plc数据内存ng
                                            }
                                            Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                            {
                                                //if (!主界面.dt.Rows.Contains(指令执行.Cell_ID1))
                                                //{
                                                //    主界面.dt.Rows.Add(主界面.dt.Rows.Count.ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID1, 指令执行.Q_weight1.ToString("0.000"), "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);
                                                //    Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count -1;
                                                //    Program.FormHome.autoResize();
                                                //    主界面.SavedtToCSVFile(主界面.dt, path);
                                                //}
                                                //else
                                                //{
                                                //    主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(指令执行.Cell_ID1))


                                                //    if (onLogEvent != null)
                                                //        onLogEvent(new OnLogEventArgs("LOG:上料位电芯编码" + 指令执行.Cell_ID1 + "已存在！"));

                                                //    Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                                //    {
                                                //        Program.FormHome.dataGridView1.DataSource = new DataTable();
                                                //        主界面.dt.Clear();
                                                //        Program.FormHome.dataGridView1.DataSource = 主界面.dt;
                                                //    }));
                                                //    int m = 0;
                                                //    while (File.Exists(path))
                                                //    {
                                                //        m++;
                                                //        path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
                                                //    }
                                                //}






                                                if (主界面.dt.Rows.Contains(指令执行.Cell_ID1))
                                                {
                                                    onLogEvent(new OnLogEventArgs("WARNING:上料位电芯编码" + 指令执行.Cell_ID1 + "重复!"));
                                                    DialogResult ret = DialogResult.None;
                                                    Program.FormMain1.Invoke(new Action(() =>
                                                        {
                                                            if (!autoSelect)
                                                            {
                                                                ret = MessageBox.Show("电芯编码" + 指令执行.Cell_ID1 + "重复!点击YES将自动保存当前数据表并新建一张数据表，点击NO将在当前数据表上覆盖电芯编码，是否新建数据表？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
                                                            }
                                                        }));
                                                   
                                                    if ((ret == DialogResult.Yes && !autoSelect) || (autoCreate && autoSelect))
                                                    {
                                                        Program.FormHome.dataGridView1.DataSource = new DataTable();
                                                        主界面.dt.Clear();
                                                        Program.FormHome.dataGridView1.DataSource = 主界面.dt;
                                                        int m = 0;
                                                        while (File.Exists(path))
                                                        {
                                                            m++;
                                                            path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
                                                        }
                                                    }
                                                    if ((ret == DialogResult.No && !autoSelect) || (!autoCreate && autoSelect))
                                                    {
                                                        主界面.dt.Rows.Remove(主界面.dt.Rows.Find(指令执行.Cell_ID1));
                                                    }
                                                }
                                                if (Q_weight1 <= FormMain.dianchi_max && Q_weight1 >= FormMain.dianchi_min)
                                                {
                                                    //OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1数据内存名称[1], "6");//写入plc数据内存ok
                                                    主界面.dt.Rows.Add((主界面.dt.Rows.Count + 1).ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID1, 指令执行.Q_weight1.ToString("0.000"), "1", "", "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);

                                                }
                                                else
                                                {
                                                   // OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤1数据内存名称[1], "-10");//写入plc数据内存ng
                                                    主界面.dt.Rows.Add((主界面.dt.Rows.Count + 1).ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID1, 指令执行.Q_weight1.ToString("0.000"), "1", "称重NG", "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);
                                                }

                                                Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count - 1;
                                                Program.FormHome.autoResize();
                                                主界面.SavedtToCSVFile(主界面.dt, path);
                                                主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");

                                                if (Program.FormHome.dataGridView1.Rows.Count > 设置界面.表格最大)
                                                {
                                                    OmroPLCvar.WriteSingleVarible(PLC扫描.PLC清数据监测信号名称, 3);
                                                }

                                            }));
                                            主界面.前1_mfg信号 = true;
                                            Program.FormPLCscan.OmroPLCvar前电子秤1监测实例.Finished();
                                        }
                                        catch (Exception exp)
                                        {
                                            try_q1++;
                                            if (try_q1 <= 3)
                                            {
                                                onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + try_q1 + "次尝试]"));
                                                Program.FormPLCscan.前电子秤1触发信号 = true;
                                            }
                                            else
                                            {
                                                Program.FormPLCscan.OmroPLCvar前电子秤1监测实例.ErrOccured();
                                                onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + (try_q1 - 1) + "次尝试]"));
                                                try_q1 = 0;
                                            }
                                        }
                                    }));
                                task5.Start();
                                
                            }
                            if (Program.FormPLCscan.前电子秤2触发信号)
                            {
                                if (task2 != null)
                                {
                                    if (!task2.IsCompleted)
                                        break;
                                }
                                if (task6 != null)
                                {
                                    if (!task6.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                Program.FormPLCscan.前电子秤2触发信号 = false;
                                Console.WriteLine(Program.stopwatch1.Elapsed.TotalMilliseconds.ToString());
                                Program.stopwatch1.Restart();
                                task6 = new Task(new Action(() =>
                                 {
                                     try
                                     {
                                         string ret2 = Program.FormAndscan2.SendBackMessage("S\r\n"); //当电子秤2被触发时，读取电子秤
                                        //OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2数据内存名称[1], ret2);
                                         Q_weight2 = float.Parse(ret2);
                                         

                                         object obj_ret2 = new object();
                                         Console.WriteLine(Program.stopwatch1.Elapsed.TotalMilliseconds.ToString());
                                         //Program.stopwatch1.Restart();
                                         OmroPLCvar.ReadSingleVarible(PLC扫描.前电子秤2数据内存名称[0], ref obj_ret2);
                                         Cell_ID2 = obj_ret2 as string;
                                         //OmroPLCvar.WriteVarible(Program.FormPLCscan.OmroPLCvar1.VarListName[1], 3);
                                         
                                         Program.stopwatch1.Stop();
                                         Console.WriteLine("ForeAND2:" + Program.stopwatch1.Elapsed.TotalMilliseconds.ToString());
                                         try_q2 = 0;
                                         //if (!主界面.dt.Rows.Contains(指令执行.Cell_ID2))
                                         //{
                                         //    Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                         //       {
                                         //           主界面.dt.Rows.Add(主界面.dt.Rows.Count.ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID2, 指令执行.Q_weight2.ToString("0.000"), "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);
                                         //           Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count - 1;
                                         //           Program.FormHome.autoResize();
                                         //           主界面.SavedtToCSVFile(主界面.dt,path);
                                         //       }));
                                         //}
                                         //else
                                         //{
                                         //    if (onLogEvent != null)
                                         //        onLogEvent(new OnLogEventArgs("LOG:上料位电芯编码" + 指令执行.Cell_ID2 + "已存在！" ));
                                         //    int m = 0;
                                         //    while (File.Exists(path))
                                         //    {
                                         //        m++;
                                         //        path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
                                         //    }
                                         //    Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                         //    {
                                         //        Program.FormHome.dataGridView1.DataSource = new DataTable();
                                         //        主界面.dt.Clear();
                                         //        Program.FormHome.dataGridView1.DataSource = 主界面.dt;
                                         //    }));       
                                         //}
                                         if (Q_weight2 <= FormMain.dianchi_max && Q_weight2 >= FormMain.dianchi_min)
                                         {
                                             OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2数据内存名称[1], "6");//写入plc数据内存ok
                                         }
                                         else
                                         {
                                             OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2数据内存名称[1], "-10");//写入plc数据内存ng
                                         }

                                         Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                           {
                                               if (主界面.dt.Rows.Contains(指令执行.Cell_ID2))
                                               {
                                                   onLogEvent(new OnLogEventArgs("WARNING:上料位电芯编码" + 指令执行.Cell_ID2 + "重复!"));
                                                   DialogResult ret = DialogResult.None;
                                                   if (!autoSelect)
                                                   {
                                                       ret = MessageBox.Show("电芯编码" + 指令执行.Cell_ID2 + "重复!点击YES将自动保存当前数据表并新建一张数据表，点击NO将在当前数据表上覆盖电芯编码，是否新建数据表？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
                                                   }
                                                   if ((ret == DialogResult.Yes && !autoSelect) || (autoCreate && autoSelect))
                                                   {
                                                       Program.FormHome.dataGridView1.DataSource = new DataTable();
                                                       主界面.dt.Clear();
                                                       Program.FormHome.dataGridView1.DataSource = 主界面.dt;
                                                       int m = 0;
                                                       while (File.Exists(path))
                                                       {
                                                           m++;
                                                           path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
                                                       }
                                                   }
                                                   if ((ret == DialogResult.No && !autoSelect) || (!autoCreate && autoSelect))
                                                   {
                                                       主界面.dt.Rows.Remove(主界面.dt.Rows.Find(指令执行.Cell_ID2));
                                                   }
                                               }
                                               if (Q_weight2 <= FormMain.dianchi_max && Q_weight2 >= FormMain.dianchi_min)
                                               {
                                                   //OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2数据内存名称[1], "6");//写入plc数据内存ok
                                                   主界面.dt.Rows.Add((主界面.dt.Rows.Count + 1).ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID2, 指令执行.Q_weight2.ToString("0.000"), "2", "", "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);
                                               }
                                               else
                                               {
                                                   //OmroPLCvar.WriteSingleVarible(PLC扫描.前电子秤2数据内存名称[1], "-10");//写入plc数据内存ng
                                                   主界面.dt.Rows.Add((主界面.dt.Rows.Count + 1).ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID2, 指令执行.Q_weight2.ToString("0.000"), "2", "称重NG", "", "", "", "", "", "", 开机验证.电解液号, 指令执行.弹夹条码);
                                               }
                                              
                                               Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count - 1;
                                               Program.FormHome.autoResize();
                                               主界面.SavedtToCSVFile(主界面.dt, path);
                                               主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");

                                               //if (Program.FormHome.dataGridView1.Rows.Count > 设置界面.表格最大)
                                               //{
                                               //    OmroPLCvar.WriteSingleVarible(PLC扫描.PLC清数据监测信号名称, 3);
                                               //}
                                           }));

                                         主界面.前2_mfg信号 = true;
                                         Program.FormPLCscan.OmroPLCvar前电子秤2监测实例.Finished();
                                     }
                                     catch (Exception exp)
                                     {
                                         try_q2++;
                                         if (try_q2 <= 3)
                                         {
                                             onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + try_q2 + "次尝试]"));
                                             Program.FormPLCscan.前电子秤2触发信号 = true;
                                         }
                                         else
                                         {
                                             Program.FormPLCscan.OmroPLCvar前电子秤2监测实例.ErrOccured();
                                             onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + (try_q2 - 1) + "次尝试]"));
                                             try_q2 = 0;

                                         }
                                     }
                                 }));
                                task6.Start();
                                
                            }
                            if (Program.FormPLCscan.后电子秤1触发信号 )
                            {
                                if (task3 != null)
                                {
                                    if (!task3.IsCompleted)
                                        break;
                                }
                                if (task7 != null)
                                {
                                    if (!task7.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                                
                                Program.FormPLCscan.后电子秤1触发信号 = false;
                                task7 = new Task(new Action(() =>
                                {
                                    try
                                    {
                                        Console.WriteLine(Program.stopwatch2.Elapsed.TotalMilliseconds.ToString());
                                        Program.stopwatch2.Restart();
                                        string ret3 = Program.FormAndscan3.SendBackMessage("S\r\n"); //当电子秤3被触发时，读取电子秤
                                        //OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1数据内存名称[5], ret3);
                                        H_weight1 = float.Parse(ret3);
                                        object obj_ret3 = new object();
                                        Console.WriteLine(Program.stopwatch2.Elapsed.TotalMilliseconds.ToString());
                                        //Program.stopwatch2.Restart();
                                        OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[0], ref obj_ret3);
                                        Cell_ID3 = obj_ret3 as string;
                                        
                                        //OmroPLCvar.WriteVarible(Program.FormPLCscan.OmroPLCvar1.VarListName[2], 3);
                                       
                                        try_h1 = 0;
                                        int findline = 主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(指令执行.Cell_ID3));

                                        if (findline >= 0)
                                        {
                                            Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                                {
                                                    主界面.dt.Rows[findline][6] = 指令执行.H_weight1.ToString("0.000");
                                                    主界面.dt.Rows[findline][7] = "1";
                                                    try
                                                    {
                                                       float qzhuye= float.Parse(主界面.dt.Rows[findline][4].ToString());
                                                       Inject_A = (指令执行.H_weight1 - qzhuye);
                                                       主界面.dt.Rows[findline][8] = Inject_A.ToString("0.000");
                                                       //OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1数据内存名称[6], Inject_A);
                                                       if (Inject_A > FormMain.zhuye_max || Inject_A < FormMain.zhuye_min)
                                                       {
                                                           主界面.dt.Rows[findline][9] = "NG";
                                                           OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1数据内存名称[7], "NG");
                                                       }
                                                       else
                                                       {
                                                           主界面.dt.Rows[findline][9] = "OK";
                                                           OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1数据内存名称[7], "OK");
                                                       }
                                                       Program.FormPLCscan.OmroPLCvar后电子秤1监测实例.Finished();
                                                       Program.stopwatch2.Stop();
                                                       Console.WriteLine("BehindAND1:" + Program.stopwatch2.Elapsed.TotalMilliseconds.ToString());
                                                      
                                                    }
                                                    catch
                                                    {
                                                        
                                                        主界面.dt.Rows[findline][6] = "????";
                                                        主界面.dt.Rows[findline][7] = "????";
                                                    }

                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[1], ref obj_ret3);
                                                    Needle1_A = (int)obj_ret3;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[2], ref obj_ret3);
                                                    Needle2_A = (int)obj_ret3;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[3], ref obj_ret3);
                                                    Press_A = (float)obj_ret3;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[4], ref obj_ret3);
                                                    Temp_A = (float)obj_ret3;

                                                    主界面.dt.Rows[findline][10] = 指令执行.Needle1_A.ToString() + "_" + 指令执行.Needle2_A.ToString();

                                                    switch (Needle2_A)
                                                    {
                                                        case 1:
                                                            //Program.FormChart.userChart1.ListX[2].Add(x1);
                                                            //Program.FormChart.userChart1.ListY[2].Add(Inject_A);
                                                            //Program.FormChart.userChart1.fresh();
                                                            object[] listline1 = new object[2] { x1, Inject_A };
                                                            chartlist0.Add(listline1);
                                                            if (chartlist0.Count > txtcount)
                                                            {
                                                                chartlist0.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液1.txt", Inject_A.ToString(), true);
                                                            if (Press_A != 0 && Temp_A != 0)
                                                            {
                                                                Save.SaveTxt("F:\\8.txt", chartlist0.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk1.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk1.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk1.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk1.txt", Inject_A.ToString(), true);
                                                            }
                                                            if (Inject_A < FormMain.zhuye_max && Inject_A > FormMain.zhuye_min)
                                                            {
                                                                cpklist1.Add(Inject_A);
                                                               
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环1.txt", Inject_A.ToString(), true);
                                                            }
                                                            x1++;
                                                            break;
                                                        case 2:
                                                            //Program.FormChart.userChart1.ListX[3].Add(x2);
                                                            //Program.FormChart.userChart1.ListY[3].Add(Inject_A);
                                                            //Program.FormChart.userChart1.fresh();
                                                            object[] listline2 = new object[2] { x2, Inject_A };
                                                            chartlist1.Add(listline2);
                                                            if (chartlist1.Count > txtcount)
                                                            {
                                                                chartlist1.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液2.txt", Inject_A.ToString(), true);
                                                            if (Press_A != 0 && Temp_A != 0)
                                                            {
                                                                Save.SaveTxt("F:\\9.txt", chartlist1.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk2.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk2.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk2.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk2.txt", Inject_A.ToString(), true);
                                                            }

                                                            if (Inject_A < FormMain.zhuye_max && Inject_A > FormMain.zhuye_min)
                                                            {
                                                                cpklist2.Add(Inject_A);
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环2.txt", Inject_A.ToString(), true);
                                                            }
                                                            x2++;
                                                            break;
                                                        case 3:
                                                            //Program.FormChart.userChart1.ListX[4].Add(x3);
                                                            //Program.FormChart.userChart1.ListY[4].Add(Inject_A);
                                                            //Program.FormChart.userChart1.fresh();
                                                             object[] listline3 = new object[2] { x3, Inject_A };
                                                            chartlist2.Add(listline3);
                                                            if (chartlist2.Count > txtcount)
                                                            {
                                                                chartlist2.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液3.txt", Inject_A.ToString(), true);
                                                            if (Press_A != 0 && Temp_A != 0)
                                                            {
                                                                Save.SaveTxt("F:\\10.txt", chartlist2.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk3.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk3.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk3.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk3.txt", Inject_A.ToString(), true);
                                                            }

                                                            if (Inject_A < FormMain.zhuye_max && Inject_A > FormMain.zhuye_min)
                                                            {
                                                                cpklist3.Add(Inject_A);
                                                               
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环3.txt", Inject_A.ToString(), true);
                                                            }

                                                            x3++;
                                                           // Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                        case 4:
                                                            //Program.FormChart.userChart1.ListX[5].Add(x4);
                                                            //Program.FormChart.userChart1.ListY[5].Add(Inject_A);
                                                            //Program.FormChart.userChart1.fresh();
                                                            object[] listline4 = new object[2] { x4, Inject_A };
                                                            chartlist3.Add(listline4);
                                                            if (chartlist3.Count > txtcount)
                                                            {
                                                                chartlist3.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液4.txt", Inject_A.ToString(), true);
                                                            if (Press_A != 0 && Temp_A != 0)
                                                            {
                                                                Save.SaveTxt("F:\\11.txt", chartlist3.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk4.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk4.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk4.txt", Inject_A.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk4.txt", Inject_A.ToString(), true);
                                                            }
                                                            
                                                            if (Inject_A < FormMain.zhuye_max && Inject_A > FormMain.zhuye_min)
                                                            {
                                                                cpklist4.Add(Inject_A);
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环4.txt", Inject_A.ToString(), true);
                                                            }
                                                            x4++;
                                                            //Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                    }

                                                    主界面.dt.Rows[findline][11] = 指令执行.Press_A.ToString("0.00");
                                                    主界面.dt.Rows[findline][12] = 指令执行.Temp_A.ToString("0.0");
                                                    主界面.SavedtToCSVFile(主界面.dt, path);
                                                    主界面.dt.WriteXml( "F:\\表格临时数据\\dt.xml");

                                                    Save.SaveTxt("D:\\TransfData\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss_" + 主界面.dt.Rows[findline][3].ToString()) + ".dbf", "序号\tPC用户名\t时间\t生产型号\t电解液批号\t条码\t前称读数\t后称读数\t注液量\tOK/NG\t注液针头\t封装压力\t封装温度\t弹夹条码\t员工工号\t设备编号\r\n" +
                                                                                      主界面.dt.Rows[findline][0].ToString() + "\t" + 设置界面.机台号 + "\t" + 主界面.dt.Rows[findline][1].ToString() + "\t" + 主界面.dt.Rows[findline][2].ToString() + "\t" +
                                                                                      主界面.dt.Rows[findline][13].ToString() + "\t" + 主界面.dt.Rows[findline][3].ToString() + "\t" + 主界面.dt.Rows[findline][4].ToString() + "\t" + 主界面.dt.Rows[findline][6].ToString() + "\t" +
                                                                                      主界面.dt.Rows[findline][8].ToString() + "\t" + 主界面.dt.Rows[findline][9].ToString() + "\t" + 主界面.dt.Rows[findline][10].ToString() + "\t" + 主界面.dt.Rows[findline][11].ToString() + "\t" +
                                                                                      主界面.dt.Rows[findline][12].ToString() + "\t" + 主界面.dt.Rows[findline][14].ToString() + "\t" + 登录.员工号 + "\t" + 设置界面.机台号 + "\r\n", true);
                                                }));
                                            //Application.DoEvents();
                                        }
                                        else
                                        {
                                            k++;
                                           
                                            //Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                            //    {
                                            //        主界面.dt.Rows.Add(主界面.dt.Rows.Count.ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID3 + "?" + k.ToString(), "?????", 指令执行.H_weight1.ToString("0.000"), "??", "??", 指令执行.Needle1_A.ToString() + "_" + 指令执行.Needle2_A.ToString(), Press_A.ToString(), Temp_A.ToString(), 开机验证.电解液号, 指令执行.弹夹条码);
                                            //        Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count - 1;
                                            //        主界面.SavedtToCSVFile(主界面.dt, path);
                                            //        主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");
                                            //    }));
                                             Program .FormMain1.Invoke(new Action(() =>
                                            {
                                                DialogResult xiliaoret = MessageBox.Show("下料位编码" + 指令执行.Cell_ID3 + "未匹配到对应上料位编码，该错误可能是由误清料引起，点击确定后，下料条码信息将丢失，是否继续？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                                                if (xiliaoret == DialogResult.Yes)
                                                {
                                                    Program.FormPLCscan.OmroPLCvar后电子秤1监测实例.Finished();
                                                    if (onLogEvent != null)
                                                        onLogEvent(new OnLogEventArgs("WARNING:下料位电芯编码" + 指令执行.Cell_ID3 + "未找到对应值！已经忽略该条码信息！"));
                                                }
                                                else
                                                {
                                                    if (onLogEvent != null)
                                                        onLogEvent(new OnLogEventArgs("ERR:下料位电芯编码" + 指令执行.Cell_ID3 + "未找到对应值！"));
                                                }
                                            }));
                                            //Application.DoEvents();
                                        }

                                        主界面.后1_mfg信号 = true;
                                    }
                                    catch (Exception exp)
                                    {
                                        try_h1++;
                                        if (try_h1 <= 3)
                                        {
                                            onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + try_h1 + "次尝试]"));
                                            Program.FormPLCscan.后电子秤1触发信号 = true;
                                        }
                                        else
                                        {
                                            Program.FormPLCscan.OmroPLCvar后电子秤1监测实例.ErrOccured();
                                            onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + (try_h1 - 1) + "次尝试]"));
                                            try_h1 = 0;
                                        }
                                    }
                                }));
                                task7.Start();
                                
                            }
                            if (Program.FormPLCscan.后电子秤2触发信号)
                            {
                                if (task4 != null)
                                {
                                    if (!task4.IsCompleted)
                                        break;
                                }
                                if (task8 != null)
                                {
                                    if (!task8.IsCompleted)
                                        //onLogEvent(new OnLogEventArgs("WARNING:上一任务未完成！"));
                                        break;
                                }
                               
                                Program.FormPLCscan.后电子秤2触发信号 = false;
                                task8= new Task(new Action(() =>
                                    {
                                        try
                                        {
                                            Console.WriteLine(Program.stopwatch3.Elapsed.TotalMilliseconds.ToString());
                                            Program.stopwatch3.Restart();

                                            string ret4 = Program.FormAndscan4.SendBackMessage("S\r\n"); //当电子秤4被触发时，读取电子秤
                                            //OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2数据内存名称[5], ret4);
                                            H_weight2 = float.Parse(ret4);
                                            object obj_ret4 = new object();
                                            Console.WriteLine(Program.stopwatch3.Elapsed.TotalMilliseconds.ToString());
                                            //Program.stopwatch3.Restart();
                                            OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[0], ref obj_ret4);
                                            Cell_ID4 = obj_ret4 as string;


                                            //OmroPLCvar.WriteVarible(Program.FormPLCscan.OmroPLCvar1.VarListName[3], 3);

                                            try_h2 = 0;


                                            int findline = 主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(指令执行.Cell_ID4));

                                            if (findline >= 0)
                                            {
                                                Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                                {
                                                    主界面.dt.Rows[findline][6] = 指令执行.H_weight2.ToString("0.000");
                                                    主界面.dt.Rows[findline][7] = "2";
                                                    try
                                                    {
                                                        float qzhuye = float.Parse(主界面.dt.Rows[findline][4].ToString());
                                                        Inject_B = (指令执行.H_weight2 - qzhuye);
                                                        //OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2数据内存名称[6], Inject_B);
                                                        主界面.dt.Rows[findline][8] = Inject_B.ToString("0.000");

                                                        if (Inject_B > FormMain.zhuye_max || Inject_B < FormMain.zhuye_min)
                                                        {
                                                            主界面.dt.Rows[findline][9] = "NG";
                                                            OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2数据内存名称[7], "NG");
                                                        }
                                                        else
                                                        {
                                                            主界面.dt.Rows[findline][9] = "OK";
                                                            OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2数据内存名称[7], "OK");
                                                        }

                                                        Program.FormPLCscan.OmroPLCvar后电子秤2监测实例.Finished();
                                                        Program.stopwatch3.Stop();
                                                        Console.WriteLine("BehindAND2:" + Program.stopwatch3.Elapsed.TotalMilliseconds.ToString());

                                                    }
                                                    catch
                                                    {
                                                        主界面.dt.Rows[findline][6] = "????";
                                                        主界面.dt.Rows[findline][7] = "????";
                                                    }

                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[1], ref obj_ret4);
                                                    Needle1_B = (int)obj_ret4;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[2], ref obj_ret4);
                                                    Needle2_B = (int)obj_ret4;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[3], ref obj_ret4);
                                                    Press_B = (float)obj_ret4;
                                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[4], ref obj_ret4);
                                                    Temp_B = (float)obj_ret4;

                                                    主界面.dt.Rows[findline][10] = 指令执行.Needle1_B.ToString() + "_" + 指令执行.Needle2_B.ToString();

                                                    switch (Needle2_B)
                                                    {
                                                        case 1:
                                                            //Program.FormChart.userChart1.ListX[2].Add(x1);
                                                            //Program.FormChart.userChart1.ListY[2].Add(Inject_B);
                                                            //Program.FormChart.userChart1.fresh();
                                                            object[] listline1 = new object[2] { x1, Inject_B };
                                                            chartlist0.Add(listline1);
                                                            if (chartlist0.Count > txtcount)
                                                            {
                                                                chartlist0.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液1.txt", Inject_B.ToString(), true);
                                                            if (Press_B != 0 && Temp_B != 0)
                                                            {
                                                                Save.SaveTxt("F:\\8.txt", chartlist0.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk1.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk1.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk1.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk1.txt", Inject_B.ToString(), true);
                                                            }

                                                            if (Inject_B < FormMain.zhuye_max && Inject_B > FormMain.zhuye_min)
                                                            {
                                                                cpklist1.Add(Inject_B);
                                                                
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环1.txt", Inject_B.ToString(), true);
                                                            }
                                                            x1++;
                                                            //Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                        case 2:
                                                            //Program.FormChart.userChart1.ListX[3].Add(x2);
                                                            //Program.FormChart.userChart1.ListY[3].Add(Inject_B);
                                                            //Program.FormChart.userChart1.fresh();
                                                            object[] listline2 = new object[2] { x2, Inject_B };
                                                            chartlist1.Add(listline2);
                                                            if (chartlist1.Count > txtcount)
                                                            {
                                                                chartlist1.RemoveAt(0);
                                                            }
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液2.txt", Inject_B.ToString(), true);
                                                            if (Press_B != 0 && Temp_B != 0)
                                                            {
                                                                Save.SaveTxt("F:\\9.txt", chartlist1.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk2.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk2.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk2.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk2.txt", Inject_B.ToString(), true);
                                                            }
                                                            
                                                            if (Inject_B < FormMain.zhuye_max && Inject_B > FormMain.zhuye_min)
                                                            {
                                                                cpklist2.Add(Inject_B);
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环2.txt", Inject_B.ToString(), true);
                                                            }
                                                            x2++;
                                                            //Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                        case 3:
                                                            //Program.FormChart.userChart1.ListX[4].Add(x3);
                                                            //Program.FormChart.userChart1.ListY[4].Add(Inject_B);
                                                            //Program.FormChart.userChart1.fresh();
                                                             object[] listline3 = new object[2] { x3, Inject_B };
                                                            chartlist2.Add(listline3);
                                                            if (chartlist2.Count > txtcount)
                                                            {
                                                                chartlist2.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液3.txt", Inject_B.ToString(), true);
                                                            if (Press_B != 0 && Temp_B != 0)
                                                            {
                                                                Save.SaveTxt("F:\\10.txt", chartlist2.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk3.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk3.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk3.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk3.txt", Inject_B.ToString(), true);
                                                            }
                                                            if (Inject_B < FormMain.zhuye_max && Inject_B > FormMain.zhuye_min)
                                                            {
                                                                cpklist3.Add(Inject_B);
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环3.txt", Inject_B.ToString(), true);
                                                            }

                                                            x3++;
                                                            //Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                        case 4:
                                                            //Program.FormChart.userChart1.ListX[5].Add(x4);
                                                            //Program.FormChart.userChart1.ListY[5].Add(Inject_B);
                                                            //Program.FormChart.userChart1.fresh();

                                                            object[] listline4 = new object[2] { x4, Inject_B };
                                                            chartlist3.Add(listline4);
                                                            if (chartlist3.Count > txtcount)
                                                            {
                                                                chartlist3.RemoveAt(0);
                                                            }
                                                            
                                                            Save.SaveTxt("F:\\注液数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_注液4.txt", Inject_B.ToString(), true);
                                                            if (Press_B != 0 && Temp_B != 0)
                                                            {
                                                                Save.SaveTxt("F:\\11.txt", chartlist3.ToArray(), false);
                                                                Save.SaveTxt("F:\\当前CPK数据\\" + DateTime.Now.ToString("yyyyMMddHHmm") + "_cpk4.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk4.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\周CPK数据\\" + "第" + FormMain.weeks.ToString() + "周" + "_cpk4.txt", Inject_B.ToString(), true);
                                                                Save.SaveTxt("F:\\月CPK数据\\" + DateTime.Now.ToString("yyyyMM") + "_cpk4.txt", Inject_B.ToString(), true);
                                                            }
                                                            if (Inject_B < FormMain.zhuye_max && Inject_B > FormMain.zhuye_min)
                                                            {
                                                                cpklist4.Add(Inject_B);
                                                                Save.SaveTxt("F:\\闭环数据\\" + "_闭环4.txt", Inject_B.ToString(), true);
                                                            }
                                                            x4++;
                                                            //Program.FormChart.userChart1.IntervalX = Program.FormChart.userChart1.TotalCount / 20;
                                                            break;
                                                    }

                                                    主界面.dt.Rows[findline][11] = 指令执行.Press_B.ToString("0.00");
                                                    主界面.dt.Rows[findline][12] = 指令执行.Temp_B.ToString("0.0");
                                                    主界面.SavedtToCSVFile(主界面.dt, path);
                                                    主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");

                                                    Save.SaveTxt("D:\\TransfData\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss_" + 主界面.dt.Rows[findline][3].ToString()) + ".dbf", "序号\tPC用户名\t时间\t生产型号\t电解液批号\t条码\t前称读数\t后称读数\t注液量\tOK/NG\t注液针头\t封装压力\t封装温度\t弹夹条码\t员工工号\t设备编号\r\n" +
                                                                                     主界面.dt.Rows[findline][0].ToString() + "\t" + 设置界面.机台号 + "\t" + 主界面.dt.Rows[findline][1].ToString() + "\t" + 主界面.dt.Rows[findline][2].ToString() + "\t" +
                                                                                     主界面.dt.Rows[findline][13].ToString() + "\t" + 主界面.dt.Rows[findline][3].ToString() + "\t" + 主界面.dt.Rows[findline][4].ToString() + "\t" + 主界面.dt.Rows[findline][6].ToString() + "\t" +
                                                                                     主界面.dt.Rows[findline][8].ToString() + "\t" + 主界面.dt.Rows[findline][9].ToString() + "\t" + 主界面.dt.Rows[findline][10].ToString() + "\t" + 主界面.dt.Rows[findline][11].ToString() + "\t" +
                                                                                     主界面.dt.Rows[findline][12].ToString() + "\t" + 主界面.dt.Rows[findline][14].ToString() + "\t" + 登录.员工号 + "\t" + 设置界面.机台号 + "\r\n", true);
                                                }));
                                                //Application.DoEvents();
                                            }
                                            else
                                            {
                                                k++;
                                               


                                                //Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                                //{
                                                   // 主界面.dt.Rows.Add(主界面.dt.Rows.Count.ToString(), DateTime.Now.ToString(), 开机验证.物料编码, 指令执行.Cell_ID4 + "?" + k.ToString(), "?????", 指令执行.H_weight2.ToString("0.000"), "??", "??", 指令执行.Needle1_B.ToString() + "_" + 指令执行.Needle2_B.ToString(), Press_B.ToString(), Temp_B.ToString(), 开机验证.电解液号, 指令执行.弹夹条码);
                                                    //Program.FormHome.dataGridView1.FirstDisplayedScrollingRowIndex = 主界面.dt.Rows.Count - 1;
                                                    //主界面.SavedtToCSVFile(主界面.dt, path);
                                                    //主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");
                                                //}));
                                                //Application.DoEvents();
                                                Program .FormMain1.  Invoke(new Action(() =>
                                                    {
                                                       DialogResult xiliaoret= MessageBox.Show("下料位编码" + 指令执行.Cell_ID4 + "未匹配到对应上料位编码，该错误可能是由误清料引起，点击确定后，下料条码信息将丢失，是否继续？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                                                       if (xiliaoret == DialogResult.Yes)
                                                       {
                                                           Program.FormPLCscan.OmroPLCvar后电子秤2监测实例.Finished();
                                                           if (onLogEvent != null)
                                                               onLogEvent(new OnLogEventArgs("WARNING:下料位电芯编码" + 指令执行.Cell_ID4 + "未找到对应值！已经忽略该条码信息！"));
                                                       }
                                                        else
                                                       {
                                                           if (onLogEvent != null)
                                                               onLogEvent(new OnLogEventArgs("ERR:下料位电芯编码" + 指令执行.Cell_ID4 + "未找到对应值！"));
                                                       }
                                                    }));
                                            }

                                            主界面.后2_mfg信号 = true;
                                        }
                                        catch (Exception exp)
                                        {
                                            try_h2++;
                                            if (try_h2 <= 3)
                                            {
                                                onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + try_h2 + "次尝试]"));
                                                Program.FormPLCscan.后电子秤2触发信号 = true;
                                            }
                                            else
                                            {
                                                Program.FormPLCscan.OmroPLCvar后电子秤2监测实例.ErrOccured();
                                                onLogEvent(new OnLogEventArgs("WARNING:" + exp.Message + "[第" + (try_h2 - 1) + "次尝试]"));
                                                try_h2 = 0;
                                            }
                                        }
                                    }));
                                task8.Start();
                                
                            }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                            if (Program.FormPLCscan.PLC报警触发信号)
                            {
                                Program.FormPLCscan.PLC报警触发信号 = false;
                                Task task = new Task(new Action(() =>
                                   {
                                       object info = new object();
                                       OmroPLCvar.ReadSingleVarible(PLC扫描.PLC报警数据内存名称, ref info);
                                       int[] PlcCode = info as int[];
                                       for (int i = 0; i < PlcCode.Count(); i++)
                                       {
                                           if (PlcCode[i] != 0)
                                           {
                                               Program.FormSaveTODatabase.TaskSaveDataToAccess(PlcCode[i].ToString());
                                           }
                                       }
                                       Program.FormPLCscan.OmroPLCvarPLC报警监测实例.Finished();
                                   }));
                                task.Start();
                            }
                            //if (Program.FormPLCscan.PLCMFG触发信号)
                            //{
                            //    Console.WriteLine("PLC_Send_receive_time:" + Program.stopwatch_PLCsendandreceive.Elapsed.TotalMilliseconds.ToString() + " ms");
                            //    Program.stopwatch_PLCsendandreceive.Restart();
                            //    Program.FormPLCscan.PLCMFG触发信号 = false;
                            //    Program.FormPLCscan.OmroPLCvarPLCMFG监测实例.Finished();
                            //    Console.WriteLine("PLC_Send_receive_time:" + Program.stopwatch_PLCsendandreceive.Elapsed.TotalMilliseconds.ToString()+" ms");
                            //}
                            if (true)  //心跳信号,1秒写0,一秒写1
                            {
                                if (task9 == null)
                                {
                                    task9 = new Task(new Action(() =>
                                    {
                                        Thread.Sleep(1000);
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.PLC心跳数据内存名称, 0);
                                        Thread.Sleep(1000);
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.PLC心跳数据内存名称, 1);
                                    }));
                                }

                                if (task9.IsCompleted || task9.Status == TaskStatus.Created)
                                {
                                    //Program.FormPLCscan.PLC心跳触发信号 = false;
                                    task9 = new Task(new Action(() =>
                                    {
                                        Thread.Sleep(1000);
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.PLC心跳数据内存名称, 0);
                                        Thread.Sleep(1000);
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.PLC心跳数据内存名称, 1);
                                    }));
                                    task9.Start();
                                }
                            }

                            if (Program.FormPLCscan.PLC扫电芯码触发信号)
                            {
                                Program.FormPLCscan.PLC扫电芯码触发信号 = false;
                                if (task10 != null)
                                {
                                    if (!task10.IsCompleted)
                                        break;
                                }
                                task10 = new Task(new Action(() =>
                                    {
                                        Program.FormMain1.BeginInvoke(new Action(() =>
                                        {
                                            object dianxinma = new object();
                                            OmroPLCvar.ReadSingleVarible(PLC扫描.PLC扫电芯码数据内存名称[0], ref dianxinma);
                                            I_Cell_ID = dianxinma as string;
                                            Program.FormMain1.label47.Text = 指令执行.I_Cell_ID;
                                            if (!I_Cell_ID.Contains(FormMain.tiaomaqianjiwei))
                                            {
                                                OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫电芯码数据内存名称[1], 7);  //扫码条码前几位不匹配
                                                onLogEvent(new OnLogEventArgs("WARNING:条码前几位不匹配！"));
                                                //OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫电芯码数据内存名称[1], 7);
                                            }
                                        }));
                                        FormMain.I_Cell_flag = true;
                                    }));
                                task10.Start();
                            }


                            if (Program.FormPLCscan.PLC扫弹夹码触发信号)
                            {
                                if (task11 != null)
                                {
                                    if (!task11.IsCompleted)
                                        break;
                                }
                                Program.FormPLCscan.PLC扫弹夹码触发信号 = false;
                                task11 = new Task(new Action(() =>
                                {
                                    try
                                    {
                                        object danjiama = new object();
                                        OmroPLCvar.ReadSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[0], ref danjiama);
                                        I_Box_ID = danjiama as string;
                                        指令执行.弹夹条码 = I_Box_ID;
                                        string ret_box = Program.FormATLscan.SendBackMessageIntime(I_Box_ID + "," + I_Box_ID, 600);         //发送两个相同的弹夹条码,600ms后获取返回值
                                        //ret_box = "OK";
                                        if (ret_box.Contains("NG") && ret_box.Contains("E0001"))
                                        {
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[1], 2);   //弹夹超时报警
                                            onLogEvent(new OnLogEventArgs("WARNING:MFG信息-弹夹超时！"));
                                        }
                                        else if (ret_box.Contains("NG") && ret_box.Contains("E0002"))
                                        {
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[1], 3);   //弹夹超时报警
                                            onLogEvent(new OnLogEventArgs("WARNING:MFG信息-流拉卡工序错误！"));
                                        }
                                        else if (ret_box.Contains("NG") && ret_box.Contains("E0003"))
                                        {
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[1], 4);   //弹夹超时报警
                                            onLogEvent(new OnLogEventArgs("WARNING:MFG信息-托盘编号不存在！"));
                                        }
                                        else if (ret_box.Contains(I_Box_ID))
                                        {
                                            string[] retadd_box = ret_box.Split(',');
                                            I_Box_ID = retadd_box[0];
                                            指令执行.弹夹条码 = I_Box_ID;
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[1], 5);   //弹夹ok
                                            onLogEvent(new OnLogEventArgs("LOG:MFG信息-弹夹验证通过，弹夹码为：" + I_Box_ID));
                                        }
                                        else
                                        {
                                            throw new Exception("弹夹验证：MFG反馈意外的字符—" + ret_box);
                                        }
                                        FormMain.I_Box_flag = true;
                                        atemp = 0;
                                    }
                                    catch (Exception e)
                                    {
                                        Program.FormPLCscan.PLC扫弹夹码触发信号 = true;
                                        if(atemp>=3)
                                        {
                                            atemp = 0;
                                            Program.FormPLCscan.PLC扫弹夹码触发信号 = false;
                                            OmroPLCvar.WriteSingleVarible(PLC扫描.PLC扫弹夹码数据内存名称[1], 1);   //弹夹验证通信出错
                                            onLogEvent(new OnLogEventArgs("WARNING:(异常错误)" + e.Message));
                                        }
                                        atemp++;
                                    }
                                }));
                                task11.Start();
                            }
                            if (Program.FormPLCscan.PLC相机NG触发信号)
                            {
                                if (task12 != null)
                                {
                                    if (!task12.IsCompleted)
                                        break;
                                }
                                Program.FormPLCscan.PLC相机NG触发信号 = false;
                                task12 = new Task(new Action(() =>
                                    {
                                        object obj_ret_cng = new object();
                                        OmroPLCvar.ReadSingleVarible(PLC扫描.PLC相机NG数据内存名称[0], ref obj_ret_cng);
                                        string cameraNG_cell = obj_ret_cng as string;
                                        int findline = 主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(cameraNG_cell));
                                        if (findline >= 0)
                                        {
                                            Program.FormHome.dataGridView1.BeginInvoke(new Action(() =>
                                            {
                                                主界面.dt.Rows[findline][6] = "拍照NG";
                                            }));
                                        }
                                    }));
                                task12.Start();
                            }

                            if (Program.FormPLCscan.拉膜1NG信号)
                            {
                                Program.FormPLCscan.拉膜1NG信号 = false;
                                task13 = new Task(new Action(() =>
                                    {
                                        object obj_ret_cng1 = new object();
                                        OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤1数据内存名称[0], ref obj_ret_cng1);
                                        string lamoNG_cell1 = obj_ret_cng1 as string;
                                        int findline = 主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(lamoNG_cell1));
                                        
                                        if (findline >= 0)
                                        {
                                            Program.FormHome.dataGridView1.Invoke(new Action(() =>
                                            {
                                                主界面.dt.Rows[findline][6] = "1拉膜NG";
                                                //Program.FormPLCscan.OmroPLCvar后电子秤1监测实例.Finished();
                                            }));
                                        }
                                        OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤1监测信号名称, 3);
                                    }));
                                task13.Start();
                            }

                            if (Program.FormPLCscan.拉膜2NG信号)
                            {
                                Program.FormPLCscan.拉膜2NG信号 = false;
                                task14 = new Task(new Action(() =>
                                {
                                    object obj_ret_cng2 = new object();
                                    OmroPLCvar.ReadSingleVarible(PLC扫描.后电子秤2数据内存名称[0], ref obj_ret_cng2);
                                    string lamoNG_cell2 = obj_ret_cng2 as string;
                                    int findline = 主界面.dt.Rows.IndexOf(主界面.dt.Rows.Find(lamoNG_cell2));
                                    if (findline >= 0)
                                    {
                                        Program.FormHome.dataGridView1.Invoke(new Action(() =>
                                        {
                                            主界面.dt.Rows[findline][6] = "2拉膜NG";
                                            //OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, 0);
                                            //Program.FormPLCscan.OmroPLCvar后电子秤2监测实例.Finished();
                                        }));
                                    }
                                    OmroPLCvar.WriteSingleVarible(PLC扫描.后电子秤2监测信号名称, 3);
                                }));
                                task14.Start();
                            }

                            if (Program.FormPLCscan.PLC清数据信号)
                            {
                                Program.FormPLCscan.PLC清数据信号 = false;
                                Program.FormHome.dataGridView1.Invoke(new Action(() =>
                                {
                                    主界面.dt.Clear();                                                 //清表格
                                    主界面.dt.WriteXml("F:\\表格临时数据\\dt.xml");//清后台表格
                                    File.Delete("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk1.txt");
                                    File.Delete("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk2.txt");
                                    File.Delete("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk3.txt");
                                    File.Delete("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk4.txt");//清CPK
                                    Save.SaveTxt("F:\\8.txt", "0,0", false);
                                    Save.SaveTxt("F:\\9.txt", "0,0", false);
                                    Save.SaveTxt("F:\\10.txt", "0,0", false);
                                    Save.SaveTxt("F:\\11.txt", "0,0", false);//清曲线
                                    chartlist0 = new List<object[]>();
                                    chartlist1 = new List<object[]>();
                                    chartlist2 = new List<object[]>();
                                    chartlist3 = new List<object[]>();//清曲线数据
                                    Save.ClearTxt("F:\\闭环数据\\" + "_闭环1.txt");     
                                    Save.ClearTxt("F:\\闭环数据\\" + "_闭环2.txt");     
                                    Save.ClearTxt("F:\\闭环数据\\" + "_闭环3.txt");     
                                    Save.ClearTxt("F:\\闭环数据\\" + "_闭环4.txt");     //清补偿
                                    int m = 0;
                                    while (File.Exists(path))        //新建后台csv
                                    {
                                        m++;
                                        path = @"F:\表格数据\saveDate" + DateTime.Now.ToString("_yyyyMMdd") + "_" + m.ToString() + ".csv";
                                    }


                                }));
                                OmroPLCvar.WriteSingleVarible(PLC扫描.PLC清数据监测信号名称, 0);
                            }


                            break;
                    case StateMode.ErrPC:
                        Thread.Sleep(500);
                        initorder = 0;
                        break;
                    case StateMode.Stop:
                        Thread.Sleep(500);
                        break;
                }
            }
        }
    }
}
