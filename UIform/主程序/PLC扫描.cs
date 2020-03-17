using OMRON.Compolet.CIP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Variables;
using OmroPlcVar;
using System.Collections;
using myEvent;
namespace VisionFram3
{
    public partial class PLC扫描 : Form
    {
        //public static event OnExceptionVEventHandler onExceptionV;
        public static event OnLogEventHandler onLogEvent;
        public OmroPLCvar OmroPLCvar前电子秤1监测实例;
        public OmroPLCvar OmroPLCvar前电子秤2监测实例;
        public OmroPLCvar OmroPLCvar后电子秤1监测实例;
        public OmroPLCvar OmroPLCvar后电子秤2监测实例;
        public bool 前电子秤1触发信号 = false;
        public bool 前电子秤2触发信号 = false;
        public bool 后电子秤1触发信号 = false;
        public bool 后电子秤2触发信号 = false;

        public bool 前电子秤1清零信号 = false;
        public bool 前电子秤2清零信号 = false;
        public bool 后电子秤1清零信号 = false;
        public bool 后电子秤2清零信号 = false;

        public bool 拉膜1NG信号 = false;
        public bool 拉膜2NG信号 = false;


        public static string 前电子秤1监测信号名称 = "K_A_action";
        public static string 前电子秤2监测信号名称 = "K_B_action";
        public static string 后电子秤1监测信号名称 = "L_A_action";
        public static string 后电子秤2监测信号名称 = "L_B_action";

        public static List<string> 前电子秤1数据内存名称 = new List<string>() 
        {
             "K_A_ID",          //被称重的电芯条码
             "K_A_result"       //称重数据
        };

        public static List<string> 前电子秤2数据内存名称 = new List<string>() 
        {
            "K_B_ID",          //被称重的电芯条码
            "K_B_result"       //称重数据
        };


        public static List<string> 后电子秤1数据内存名称 = new List<string>() 
        {
           // "K_info[36].ID",          //被称重的电芯条码
           "PC_info_Lay_A.ID",
            "PC_info_Lay_A.Needle1",  //针头1
            "PC_info_Lay_A.Needle2",  //针头2
            "PC_info_Lay_A.Press",    //封装压力
            "PC_info_Lay_A.Temp",     //封装温度

            "PC_info_Lay_A.H_weight",  //称重数据
            "PC_info_Lay_A.Inject",    //注液量
            "PC_info_Lay_A.OK_NG"      //注液OK/NG
        };

        public static List<string> 后电子秤2数据内存名称 = new List<string>() 
        {
            //"K_info[37].ID",          //被称重的电芯条码
            "PC_info_Lay_B.ID",
            "PC_info_Lay_B.Needle1",  //针头1
            "PC_info_Lay_B.Needle2",  //针头2
            "PC_info_Lay_B.Press",    //封装压力
            "PC_info_Lay_B.Temp",     //封装温度

            "PC_info_Lay_B.H_weight",  //称重数据
            "PC_info_Lay_B.Inject",    //注液量
            "PC_info_Lay_B.OK_NG"      //注液OK/NG
        };


        public OmroPLCvar OmroPLCvarPLC报警监测实例;
        public bool PLC报警触发信号 = false;
        public static string PLC报警监测信号名称 = "Alarm_tr";
        public static string PLC报警数据内存名称 = "Alarm_NO";

        //public OmroPLCvar OmroPLCvarPLCMFG监测实例;
        //public bool PLCMFG触发信号 = false;
        //public static string PLCMFG监测信号名称 = "PC_info_tr";
        //public static List<string> PLCMFG数据内存名称 = new List<string>() 
        //{ 
        //    "PC_info.ID",              //电芯条码 
        //    "PC_info.Q_weight",        //前称重
        //    "PC_info.H_weight",        //后称重
        //    "PC_info.Inject",          //注液量
        //    "PC_info.OK_NG",           //注液OK/NG
        //    "PC_info.Needle1",         //针头1
        //    "PC_info.Needle2",         //针头2
        //    "PC_info.Press",           //封装压力
        //    "PC_info.Temp",            //封装温度
        //};

        public OmroPLCvar OmroPLCvarPLC心跳监测实例;
        public bool PLC心跳触发信号 = false;
        public static string PLC心跳监测信号名称 = "K_weigh_online";
        public static string PLC心跳数据内存名称 = "K_weigh_online";



        public static OmroPLCvar OmroPLCvarPLC扫电芯码监测实例;
        public bool PLC扫电芯码触发信号 = false;
        public static string PLC扫电芯码监测信号名称 = "K_cell_result";
        public static List<string> PLC扫电芯码数据内存名称 = new List<string>() 
        {
            "K_cell_ID",                //电芯条码
            "K_cell_result"             //读取电芯后判断结果
        };

        public static OmroPLCvar OmroPLCvarPLC扫弹夹码监测实例;
        public bool PLC扫弹夹码触发信号 = false;
        public static string PLC扫弹夹码监测信号名称 = "K_box_result";
        public static List<string> PLC扫弹夹码数据内存名称 = new List<string>() 
        {
            "K_box_ID",                //弹夹条码
            "K_box_result"             //读取弹夹后判断结果
        };

        public static OmroPLCvar OmroPLCvarPLC相机NG和设备启动监测实例;
        public bool PLC相机NG触发信号 = false;
        public bool PLC设备启动触发信号 = false;
        public static string PLC相机NG和设备启动监测信号名称 = "K_c_action";
        public static List<string> PLC相机NG数据内存名称 = new List<string>() 
        {
            "K_c_ID"
        };

        public static OmroPLCvar OmroPLCvarPLC清数据监测实例;
        public bool PLC清数据信号 = false;
        public static string PLC清数据监测信号名称 = "K_c_clear";


        public PLC扫描()
        {

            InitializeComponent();
            njCompolet1.ConnectionType = ConnectionType.UCMM;
            njCompolet1.ReceiveTimeLimit = 100;
            njCompolet1.UseRoutePath = false;
            njCompolet1.PeerAddress = "192.168.250.2";
            njCompolet1.LocalPort = 2;
            njCompolet1.Active = true;
            
            OmroPLCvar后电子秤2监测实例 = new OmroPLCvar(njCompolet1, 后电子秤2监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvar后电子秤2监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvar后电子秤2监测信号_OnChanged);
            OmroPLCvar前电子秤1监测实例 = new OmroPLCvar(njCompolet1, 前电子秤1监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvar前电子秤1监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvar前电子秤1监测信号_OnChanged);
            OmroPLCvar前电子秤2监测实例 = new OmroPLCvar(njCompolet1, 前电子秤2监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvar前电子秤2监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvar前电子秤2监测信号_OnChanged);
            OmroPLCvar后电子秤1监测实例 = new OmroPLCvar(njCompolet1, 后电子秤1监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvar后电子秤1监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvar后电子秤1监测信号_OnChanged);
           
            OmroPLCvarPLC报警监测实例 = new OmroPLCvar(njCompolet1, PLC报警监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC报警监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC报警监测信号_OnChanged);

            //OmroPLCvarPLCMFG监测实例 = new OmroPLCvar(njCompolet1, PLCMFG监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            //OmroPLCvarPLCMFG监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC数据监测信号_OnChanged);

            OmroPLCvarPLC心跳监测实例 = new OmroPLCvar(njCompolet1, PLC心跳监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC心跳监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC心跳监测信号_OnChanged);

            OmroPLCvarPLC扫电芯码监测实例 = new OmroPLCvar(njCompolet1, PLC扫电芯码监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC扫电芯码监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC扫电芯码监测信号_OnChanged);

            OmroPLCvarPLC扫弹夹码监测实例 = new OmroPLCvar(njCompolet1, PLC扫弹夹码监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC扫弹夹码监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC扫弹夹码监测信号_OnChanged);


            OmroPLCvarPLC相机NG和设备启动监测实例 = new OmroPLCvar(njCompolet1, PLC相机NG和设备启动监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC相机NG和设备启动监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC相机NG和设备启动监测实例_OnChanged);

            OmroPLCvarPLC清数据监测实例 = new OmroPLCvar(njCompolet1, PLC清数据监测信号名称);   //新建一个值检测实例，并绑定需要检测变量和Compolet到此实例
            OmroPLCvarPLC清数据监测实例.OnChanged += new OmroPLCvarChangedEventHandle(OmroPLCvarPLC清数据监测实例_OnChanged);
        
        }

        public void Init()
        {
            saveVarible saveVarible1 = new saveVarible();
            SAVE.ReadVar(this.Text, ref saveVarible1);
            //ipInput1.ipText = saveVarible1.ip;
           


            int chaoshi = 0;
            int max = 4;
            while (!njCompolet1.IsConnected && chaoshi < max)
            {
                Thread.Sleep(1000);
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("LOG:PLC尝试连接" + chaoshi.ToString() + "次！"));
                chaoshi++;
            }
            if (chaoshi == max)
            {
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("ERR:PLC连接超时！"));
            }
            else
            {
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("LOG:PLC初始化成功！"));

                OmroPLCvar后电子秤1监测实例.Start();   //开启值扫描实例循环扫描
                OmroPLCvar后电子秤2监测实例.Start();   //开启值扫描实例循环扫描
                OmroPLCvar前电子秤1监测实例.Start();   //开启值扫描实例循环扫描
                OmroPLCvar前电子秤2监测实例.Start();   //开启值扫描实例循环扫描

                //OmroPLCvarPLCMFG监测实例.Start();   //开启值扫描实例循环扫描

                OmroPLCvarPLC报警监测实例.Start();   //开启值扫描实例循环扫描

                //OmroPLCvarPLC心跳监测实例.Start();   //开启值扫描实例循环扫描

                OmroPLCvarPLC相机NG和设备启动监测实例.Start();

                OmroPLCvarPLC清数据监测实例.Start();

                if (设置界面.电芯扫码)
                    OmroPLCvarPLC扫电芯码监测实例.Start();
                if (设置界面.弹夹扫码)
                    OmroPLCvarPLC扫弹夹码监测实例.Start();
            }
            
        }
        //PLC预设定值改变的时候发生事件
        private void OmroPLCvar前电子秤1监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    前电子秤1触发信号 = true;
                    Program.stopwatch.Reset();
                    Program.stopwatch.Start();
                    break;
                case 4:
                    前电子秤1清零信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvar前电子秤1监测实例.TickSign + "=" + e.Ret));
        }
        private void OmroPLCvar前电子秤2监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    前电子秤2触发信号 = true;
                    Program.stopwatch1.Reset();
            Program.stopwatch1.Start();
                    break;
                case 4:
                    前电子秤2清零信号 = true;
                    break;
            }
            if (onLogEvent != null)
            onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvar前电子秤2监测实例.TickSign + "=" + e.Ret));
        }
        private void OmroPLCvar后电子秤1监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    后电子秤1触发信号 = true;
                    Program.stopwatch2.Reset();
                    Program.stopwatch2.Start();
                    break;
                case 4:
                    后电子秤1清零信号 = true;
                    break;
                case -2:
                    拉膜1NG信号 = true;
                    break;
            }
            if (onLogEvent != null)
            onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvar后电子秤1监测实例.TickSign + "=" + e.Ret));
        }
        private void OmroPLCvar后电子秤2监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    后电子秤2触发信号 = true;
                    Program.stopwatch3.Reset();
                    Program.stopwatch3.Start();
                    break;
                case 4:
                    后电子秤2清零信号 = true;
                    break;
                case -2:
                    拉膜2NG信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvar后电子秤2监测实例.TickSign + "=" + e.Ret));
        }
        private void OmroPLCvarPLC报警监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    PLC报警触发信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC报警监测实例.TickSign + "=" + e.Ret));
        }

        //private void OmroPLCvarPLC数据监测信号_OnChanged(VarChangeEventArgs e)
        //{
        //    switch (e.Ret)
        //    {
        //        //未触发状态
        //        case 0:
        //            break;
        //        //plc触发PC动作执行
        //        case 1:
        //            PLCMFG触发信号 = true;
        //            break;
        //    }
        //    if (onLogEvent != null)
        //        onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLCMFG监测实例.TickSign + "=" + e.Ret));
        //}

        private void OmroPLCvarPLC心跳监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:

                    break;
                //plc触发PC动作执行
                case 1:
                    PLC心跳触发信号 = true;
                    break;                    
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC心跳监测实例.TickSign + "=" + e.Ret));
        }


        private void OmroPLCvarPLC扫电芯码监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 6:
                    PLC扫电芯码触发信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC扫电芯码监测实例.TickSign + "=" + e.Ret));
        }


        private void OmroPLCvarPLC扫弹夹码监测信号_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 6:
                    PLC扫弹夹码触发信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC扫弹夹码监测实例.TickSign + "=" + e.Ret));
        }

        private void OmroPLCvarPLC相机NG和设备启动监测实例_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 2:
                    PLC相机NG触发信号 = true;
                    break;
                case 10:
                    if (设置界面.允许PLC清零)
                        PLC设备启动触发信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC相机NG和设备启动监测实例.TickSign + "=" + e.Ret));
        }

        private void OmroPLCvarPLC清数据监测实例_OnChanged(VarChangeEventArgs e)
        {
            switch (e.Ret)
            {
                //未触发状态
                case 0:
                    break;
                //plc触发PC动作执行
                case 1:
                    if (设置界面.允许PLC清零)
                        PLC清数据信号 = true;
                    break;
            }
            if (onLogEvent != null)
                onLogEvent(new OnLogEventArgs("SIGNAL:" + OmroPLCvarPLC清数据监测实例.TickSign + "=" + e.Ret));
        }


        private void menuButton1_SelectedChanged(object sender, EventArgs e)
        {
            njCompolet1.PeerAddress = ipInput1.ipText;
            saveVarible saveVarible1=new saveVarible();
            saveVarible1.ip = ipInput1.ipText;
            SAVE.SaveVar(this.Text, saveVarible1);
            menuButton1.Selected = false;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[0], 1);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[1], 0);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[2], 0);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[3], 0);
            //OmroPLCvar前电子秤1监测信号.RetVals = ht;//添加keyvalue键值对，模拟PLC循环更新ht。
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[0], 0);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[1], 0);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[2], 0);
            //ht.Add(OmroPLCvar前电子秤1监测信号.VarListName[3], 1);
            //OmroPLCvar前电子秤1监测信号.RetVals = ht;//添加keyvalue键值对，模拟PLC循环更新ht。
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }           

        private void PLC扫描_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason==CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OmroPLCvar.WriteSingleVarible("K_A_action", 1);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OmroPLCvar.WriteSingleVarible("K_B_action", 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OmroPLCvar.WriteSingleVarible("L_A_action", 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OmroPLCvar.WriteSingleVarible("L_B_action", 1);
        }
        bool flag = false;
        private void button8_Click(object sender, EventArgs e)
        {
            if(flag)
            {
                OmroPLCvar.WriteSingleVarible("L_A_action", "66");
                flag = false;
            }
            else
            {
                OmroPLCvar.WriteSingleVarible("L_A_action", "88");
                flag = true;
            }
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                OmroPLCvar.WriteSingleVarible("L_B_action", "66");
                flag = false;
            }
            else
            {
                OmroPLCvar.WriteSingleVarible("L_B_action", "88");
                flag = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                OmroPLCvar.WriteSingleVarible("K_A_action", "66");
                flag = false;
            }
            else
            {
                OmroPLCvar.WriteSingleVarible("K_A_action", "88");
                flag = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                OmroPLCvar.WriteSingleVarible("K_B_action", "66");
                flag = false;
            }
            else
            {
                OmroPLCvar.WriteSingleVarible("K_B_action", "88");
                flag = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OmroPLCvar.WriteSingleVarible("产量.良率", 1);
            object ret=new object();
            OmroPLCvar.ReadSingleVarible("产量.良率", ref ret);
        }

    
    }
}
