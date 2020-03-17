using SerialPortLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using myEvent;
namespace VisionFram3
{
    public partial class 电子秤扫描 : Form
    {
       // public static event OnExceptionVEventHandler onExceptionV;
        public static event OnLogEventHandler onLogEvent;
        public string AName = "";
        public 串口 SerialPort;
        private string contempMessage = "";
        private bool sendbackflag = false;
        private EventWaitHandle waithandle = new EventWaitHandle(false, EventResetMode.AutoReset);
       private  object send_Block = new object();

       //private bool US_flag;
        public 电子秤扫描(string name,串口 serialPort)
        {
            InitializeComponent();
            this.Text = name;
            AName = name;
            SerialPort = serialPort;
            //SerialPort.formStatusChanged += new SerialPortLib.ConnectionStatusChangedEventHandler(SerialPort_formStatusChanged);
            SerialPort.formMessageReceived += new MessageReceivedEventHandler(SerialPort_formMessageReceived);
        }
        public void Init()
        {
            int chaoshi = 0;
            int max = 12;      //12秒超时检测，等待电子秤开机完毕
            lock (send_Block)
            {
                //SerialPort.TASKsendMessageString("ON\r\n");
              
                //if (onLogEvent != null)
                //    onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤开机中..."));
               
                //while (!SerialPort.IsReceived && chaoshi < max)
                //{
                //    SerialPort.TASKsendMessageString("S\r\n");
                //    Thread.Sleep(1000);
                //    chaoshi++;
                //}
                //SerialPort.IsReceived = false;

                try
                {
                    SendBackMessage("Q\r\n");
                    if (onLogEvent != null)
                        onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤已开机！"));
                }
                catch
                {
                    Thread.Sleep(300);
                    SerialPort.TASKsendMessageString("ON\r\n");
                    if (onLogEvent != null)
                        onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤开机中..."));
                    //Thread.Sleep(100);
                    while (SerialPort.IsReceived == 0 && chaoshi < max)
                    {
                        SerialPort.TASKsendMessageString("Q\r\n");
                        Thread.Sleep(1000);
                        chaoshi++;
                    }
                    if (SerialPort.IsReceived == -1)
                    {
                        SerialPort.IsReceived = 0;
                        //lock (eventBlock)
                        //{
                        if (onLogEvent != null)
                            onLogEvent(new OnLogEventArgs("ERR:" + this.Text + "电子称开机，但不稳定！"));
                        return;
                        //}
                    }
                    SerialPort.IsReceived = 0;
                }

            }
            if (chaoshi == max)
            {
                //lock (eventBlock)
                //{
                chaoshi = 0;
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("ERR:" + this.Text + "电子称开机超时！"));
                //}
            }
            else
            {
                //lock (eventBlock)
                //{
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤连接成功！"));
                //}
            }
        }

        public void Clear()
        {
            int chaoshi = 0;
            int max = 50;      //10秒超时检测，等待电子秤清零完毕
            lock (send_Block)
            {
                SerialPort.SendMessageString("R\r\n");

                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤清零中..."));
                SerialPort.IsReceived = 0;
                while (SerialPort.IsReceived == 0 && chaoshi < max)
                {
                    SerialPort.SendMessageString("Q\r\n");
                    Thread.Sleep(100);
                    chaoshi++;
                }
                if (SerialPort.IsReceived==-1)
                {
                    SerialPort.IsReceived = 0;
                    throw new Exception(this.Text + "获取到不稳定值，电子称清零失败！");
                }
                SerialPort.IsReceived = 0;
              
                if (chaoshi == max)
                {
                    chaoshi = 0;
                    throw new Exception(this.Text + "超时，电子称清零失败！");
                }
            }
        }

            
        public void ClearV()
        {
            int chaoshi = 0;
            //int max = 100;
            lock (send_Block)
            {
                SerialPort.SendMessageString("R\r\n");
                if (onLogEvent != null)
                    onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤清零中..."));
                SerialPort.IsReceived = 0;
                while ((SerialPort.IsReceived == 0 ))
                {
                    SerialPort.SendMessageString("Q\r\n");
                    Thread.Sleep(100);
                    chaoshi++;
                }
            }
        }

            //else
            //{
            //    //lock (eventBlock)
            //    //{
            //    if (onLogEvent != null)
            //        onLogEvent(new OnLogEventArgs("LOG:" + this.Text + "电子秤清零成功！"));
            //    //}
            //}
        



        private void SerialPort_formMessageReceived(object sender, MessageReceivedEventArgs args)
        {
           // if (onLogEvent != null)
              //  onLogEvent(new OnLogEventArgs("SIGNAL:" + this.Text + "=" + args.DataStr));
            contempMessage = "";
            if (args.DataStr.Contains("ST,"))
            {
                char[] chenzhong = args.DataStr.Split(',')[1].ToCharArray(0, 9);
                for(int i=0;i<9;i++)
                {
                    contempMessage += chenzhong[i];
                }
            }
            else if (args.DataStr.Contains("US,"))
            {
                //onLogEvent(new OnLogEventArgs("ERR:" + SerialPort.Text + "=" + args.DataStr + "非电子称数据！"));
                //return; 
                //US_flag = true;
            }
            

            if (sendbackflag)
            {
                sendbackflag = false;
                waithandle.Set();
                //Thread.Sleep(100);//***加延时，否则当主线程还没有完全阻塞解除，就执行Invoke会造成主线程堵死
            }
            onLogEvent(new OnLogEventArgs("SIGNAL:" + SerialPort.Text + "=" + contempMessage));
        }

        public void SendMessage(string message)
        {
            lock (send_Block)
            {
                SerialPort.TASKsendMessageString(message);
            }
        }

        public string SendBackMessage(string message)
        {
            //Thread.Sleep(10);
            lock (send_Block)
            {
                if (!sendbackflag)
                {
                    sendbackflag = true;
                    SerialPort.TASKsendMessageString(message);
                    bool re = waithandle.WaitOne(5000);  //10秒中延时，等待串口数据返回值。
                    if (!re)
                    {
                        sendbackflag = false;
                        SerialPort.TASKsendMessageString("C\r\n");
                        throw new Exception(this.Text + "消息已发出，获取返回值超时！");
                    }
                }
                return contempMessage;
            }
        }

        public string SendBackMessageV(string message)
        {

           // string ret = SerialPort.ReadExisting();
            return "";
        }



    
        private void button1_Click(object sender, EventArgs e)
        {
            SerialPort.TASKsendMessageString("OFF\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SerialPort.TASKsendMessageString("ON\r\n");
        }

       


    }
}
