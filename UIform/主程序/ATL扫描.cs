using SerialPortLib;
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
namespace VisionFram3
{
    public partial class ATL扫描 : Form
    {
        public 串口 SerialPort;
        //public static event OnExceptionVEventHandler onExceptionV;
        public static event OnLogEventHandler onLogEvent;
        private EventWaitHandle waithandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        private string contempMessage = "正在读取";
        private bool sendbackflag = false;


        public ATL扫描(string name, 串口 serialPort)
        {
            InitializeComponent();
            this.Text = name;
            SerialPort = serialPort;
            SerialPort.formMessageReceived += new SerialPortLib.MessageReceivedEventHandler(SerialPort_formMessageReceived);
        }

        public string ReadExisting()
        {
           return SerialPort.ReadExisting();
        }

      

        public void SendMessage(string message)
        {
            SerialPort.TASKsendMessageString(message);
        }
        /// <summary>
        /// 发送信息，在1秒内做出回应，否则报错返回值超时。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendBackMessageInstant(string message)
        {
            if (!sendbackflag)
            {
                contempMessage = "";
                sendbackflag = true;
                SerialPort.TASKsendMessageString(message);
                bool re = waithandle.WaitOne(2000);
                if (!re)
                {
                    sendbackflag = false;
                    MessageBox.Show("请确认MFG软件已经打开!", "提示");
                    throw new Exception(this.Text + "消息已发出，获取返回值超时！");
                    
                }
                return contempMessage;
            }
            else
            {
                return "正在读取";
            }
        }
        /// <summary>
        /// 当接收OK时返回OK，当接收到NG时，再次返回一个信号
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendBackMessageNGTwice(string message)
        {
            string ContempMessage = "";
            if (!sendbackflag)
            {
                contempMessage = "";
                sendbackflag = true;
                SerialPort.TASKsendMessageString(message);
                bool re = waithandle.WaitOne(1000);
                if (!re)
                {
                    sendbackflag = false;
                    MessageBox.Show("请确认MFG软件已经打开!", "提示");
                    throw new Exception(this.Text + "消息已发出，获取返回值超时！");
                }
                if (contempMessage == "OK")
                {
                    ContempMessage = contempMessage;
                    return ContempMessage;
                }
                if (contempMessage == "NG")
                {
                    ContempMessage = contempMessage;
                    sendbackflag = true;
                    bool re2 = waithandle.WaitOne(1000);
                    if (!re2)
                    {
                        sendbackflag = false;
                        MessageBox.Show("请确认MFG软件已经打开!", "提示");
                        throw new Exception(this.Text + "获取NG消息代码，返回值超时！");
                    }
                    ContempMessage = ContempMessage + contempMessage;
                }
                else
                {
                    if (contempMessage.Contains("NG"))
                    {
                        return contempMessage;
                    }
                    else
                    {
                        throw new Exception(this.Text + "无法解析指令:" + contempMessage + "！");
                    }
                }
                return ContempMessage;
            }
            else
            {
                return "正在读取";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendBackMessageLong(string message)
        {
            SerialPort.ReadExisting();
            if (!sendbackflag)
            {
                contempMessage = "";
                sendbackflag = true;
                SerialPort.TASKsendMessageString(message);
                bool re = waithandle.WaitOne(10*60000*6);             //换电解液超时设置10*6分钟，否则报错。
                if (!re)
                {
                    sendbackflag = false;
                    //MessageBox.Show("请确认MFG软件已经打开!", "提示");
                    throw new Exception(this.Text + "消息已发出，获取返回值超时！请确认MFG软件已经打开!");
                }
                return contempMessage;
            }
            else
            {
                return "正在读取";
            }
        }

        public string SendBackMessageIntime(string message, int millionsecond)
        {
            SerialPort.ReadExisting();
            SendMessage(message);
            Thread.Sleep(millionsecond);
            string ret = SerialPort.ReadExisting();
            if (ret == "")
            {
                //MessageBox.Show("请确认MFG软件已经打开!", "提示");
                throw new Exception(this.Text + "消息已发出，获取返回值超时,请确认MFG软件已经打开!");
            }
            else
            {
                return ret;
            }
        }

        public string SendBackMessageAny(string message,int millionSecond)
        {
            contempMessage = "";
            string ContempMessage = "";
            SerialPort.TASKsendMessageString(message);
            int i = 0;
            while (i < millionSecond/10)
            {
                i++;
                Thread.Sleep(10);
                if (!ContempMessage.Contains(contempMessage))
                {
                    ContempMessage += contempMessage;
                }
            }
            if (ContempMessage == "")
            {
                MessageBox.Show("请确认MFG软件已经打开!", "提示");
                throw new Exception(this.Text + "消息已发出，获取返回值超时！");
            }
            else
            {
                return ContempMessage;
            }
        }





    }
}
