using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO.Ports;

namespace FastCtr
{


    [Serializable]
    public struct NewUDPSetting
    {
        public string local_ip;
        public int local_port;
        public string romote_ip;
        public int romote_port;
    }
    public partial class NewUDP : UserControl
    {
        enum Mode
        {
            Listening,
            Stopping
        }
        public NewUDP()
        {
            InitializeComponent();
            NewUDPSetting set = new NewUDPSetting();
            set.local_ip = "0.0.0.0";
            set.romote_ip = "0.0.0.0";
            set.local_port = 0;
            set.romote_port = 0;
            try
            {
                set = (NewUDPSetting)FastData.SaveStatic.ReadBinF(Name);
            }
            catch (Exception exp)
            {
                MessageBox.Show(Name + ": " + exp.ToString());
            }
            _local_ip = set.local_ip;
            _local_port = set.local_port;
            _localIpep = new IPEndPoint(IPAddress.Parse(_local_ip), _local_port); // 本机IP和监听端口号
            _udpclient = new UdpClient(_localIpep);
            _romote_ip = set.romote_ip;
            _romote_port = set.romote_port;
            _romoteIpep = new IPEndPoint(IPAddress.Parse(_romote_ip), _romote_port);
            
        }
        /// <summary>
        /// 用于UDP发送的网络服务类
        /// </summary>
        IPEndPoint _localIpep;
        IPEndPoint _romoteIpep;
        UdpClient _udpclient;
        /// <summary>
        /// 开关：在监听UDP报文阶段为true，否则为false
        /// </summary>
   
        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        /// 
        string _local_ip = "0.0.0.0";
        int _local_port = 60000;
        string _romote_ip = "0.0.0.0";
        int _romote_port = 50000;
        string[] ContempData =new string[2] { "", "" } ;
        /// <summary>
        /// 设置本地IP
        /// </summary>
        /// 
        AutoResetEvent resetevent = new AutoResetEvent(false);
        Mode mode = Mode.Stopping;

        IPEndPoint refIpep = new IPEndPoint(0, 0);
       
        Mode MODE
        {
            get
            {
                return mode;
            }
        }



        public string Local_IP
        {
            get
            {
                return _local_ip;
            }
            set
            {
                _local_ip = value;
            }
        }
        /// <summary>
        /// 设置本地端口
        /// </summary>
        public int Local_Port
        {
            get
            {
                return _local_port;
            }
            set
            {
                _local_port = value;
               
                

            }
        }

        /// <summary>
        /// 设置远程IP
        /// </summary>
        public string Romote_IP
        {
            get
            {
                return _romote_ip;
            }
            set
            {
                _romote_ip = value;
                
            }
        }
        /// <summary>
        /// 设置远程端口
        /// </summary>
        public int Romote_Port
        {
            get
            {
                return _romote_port;
            }
            set
            {
                _romote_port = value;
                
            }
        }





        public NewUDP(string local_ip, int local_port, string romote_ip, int romote_port)
        {
            InitializeComponent();
            ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            _local_ip = local_ip;
            _local_port = local_port;
            _localIpep = new IPEndPoint(IPAddress.Parse(_local_ip), _local_port); // 本机IP和监听端口号
            _udpclient = new UdpClient(_localIpep);

            _romote_ip = romote_ip;
            _romote_port = romote_port;
            _romoteIpep = new IPEndPoint(IPAddress.Parse(_romote_ip), _romote_port);
        }

        


        public NewUDP(string name)
        {
            InitializeComponent();
            Name = name;
            NewUDPSetting set = new NewUDPSetting();
            set.local_ip = "0.0.0.0";
            set.romote_ip = "0.0.0.0";
            set.local_port = 0;
            set.romote_port = 0;
            try
            {
                set = (NewUDPSetting)FastData.SaveStatic.ReadBinF(name);
            }
            catch (Exception exp)
            {
                MessageBox.Show(name + ": " + exp.ToString());
            }

            _local_ip = set.local_ip;
            _local_port = set.local_port;
            _localIpep = new IPEndPoint(IPAddress.Parse(_local_ip), _local_port); // 本机IP和监听端口号
            _udpclient = new UdpClient(_localIpep);
            _romote_ip = set.romote_ip;
            _romote_port = set.romote_port;
            _romoteIpep = new IPEndPoint(IPAddress.Parse(_romote_ip), _romote_port);
        }


      


        /// <summary>
        /// 返回值是一个数组，第一位表示返回的主机号，第二位表示返回的字符串。超时时间设置为<=0则无限等待。
        /// </summary>
        /// <param name="sendContent"></param>
        /// <param name="millisecondsTimeOut"></param>
        /// <returns></returns>
        public string[] SendBackMessageInstant(string sendContent, int millisecondsTimeOut)
        {
            byte[] data = Encoding.UTF8.GetBytes(sendContent);
            int n = data.Count();
            resetevent.Reset();
            _udpclient.Send(data, n, _romoteIpep);

            if (millisecondsTimeOut <= 0)
            {
                resetevent.WaitOne();
                return ContempData;
            }
            else
            {
                if (resetevent.WaitOne(millisecondsTimeOut))
                {
                    return ContempData;
                }
                else
                {

                    return null;
                }
            }
        }

        public void SendMessage(string sendContent)
        {
            byte[] data = Encoding.UTF8.GetBytes(sendContent);
            int n = data.Count();
            _udpclient.Send(data, n, _romoteIpep);

        }


        public string ReceiveMessage(ref IPEndPoint _romoteIpep)
        {
            byte[] bytRecv = _udpclient.Receive(ref refIpep);
            string message = Encoding.UTF8.GetString(bytRecv, 0, bytRecv.Length);
            ContempData = new string[2] { refIpep.ToString(), message };
            return message;
        }


        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="callback"></param>
        public void StartReceivingMessage(Action<string[]> callback)
        {
            Task task = new Task(() =>
            {
                mode = Mode.Listening;
                byte[] data = Encoding.UTF8.GetBytes("  StartReceiving");
                int n = data.Count();
                _udpclient.Send(data, n, _local_ip, _local_port);
                while (mode == Mode.Listening)
                {
                    string message = ReceiveMessage(ref refIpep);
                    ContempData = new string[2] { refIpep.ToString(), message };
                    resetevent.Set();
                    callback(ContempData);
                }
                
            }, TaskCreationOptions.LongRunning);
            task.Start();
        }

        public void StartReceivingMessage()
        {
            Task task = new Task(() =>
            {
                mode = Mode.Listening;
                byte[] data = Encoding.UTF8.GetBytes("  StartReceiving");
                int n = data.Count();
                _udpclient.Send(data, n, _local_ip, _local_port);
                while (mode == Mode.Listening)
                {
                    string message = ReceiveMessage(ref refIpep);
                    ContempData = new string[2] { refIpep.ToString(), message };
                    resetevent.Set();
                }

            }, TaskCreationOptions.LongRunning);
            task.Start();
        }



        public void StopReceivingMessage()
        {
            mode = Mode.Stopping;
            Thread.Sleep(10);
            byte[] data = Encoding.UTF8.GetBytes("  StopReceiving");
            int n = data.Count();
            _udpclient.Send(data, n, _local_ip, _local_port);
        }

        

        public static string[] GetLocalIp()
        {
            List<string> IPList = new List<string>();
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    IPList.Add(AddressIP);
                }
            }
            return IPList.ToArray();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewUDPSetting set = new NewUDPSetting();
            set.local_ip = textBox1.Text;
            set.local_port = int.Parse(textBox3.Text);
            set.romote_ip = textBox2.Text;
            set.romote_port = int.Parse(textBox4.Text);
            FastData.SaveStatic.SaveBinF(Name, set);
        }

        private void NewUDP_Load(object sender, EventArgs e)
        {

            if (Application.StartupPath.Contains("Debug"))
            {
                
                textBox1.Text = _local_ip;
                textBox3.Text = _local_port.ToString();
                textBox2.Text = _romote_ip;
                textBox4.Text = _romote_port.ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == Mode.Stopping)
                StartReceivingMessage();
            else
                StopReceivingMessage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _local_ip = textBox1.Text;
                _local_port = int.Parse(textBox3.Text);
                _romote_ip = textBox2.Text;
                _romote_port = int.Parse(textBox4.Text);
                _localIpep = new IPEndPoint(IPAddress.Parse(_local_ip), _local_port);

                _udpclient.Close();
                _udpclient = new UdpClient(_localIpep);
                _romoteIpep = new IPEndPoint(IPAddress.Parse(_romote_ip), _romote_port);
            }
            catch
            {
                MessageBox.Show("输入信息有错误！");
            }

        }

        private void NewUDP_ParentChanged(object sender, EventArgs e)
        {
            if (Application.StartupPath.Contains("Debug"))
            {
                if (this.ParentForm != null)
                {
                    ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                    ParentForm.AutoSize = true;
                    ParentForm.StartPosition = FormStartPosition.CenterScreen;
                    ParentForm.Text = Name;
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox5.Text = "远程端口：" + ContempData[0] + " :" + ContempData[1];
                label11.Text = _udpclient.Client.LocalEndPoint.ToString();
                label12.Text = _romoteIpep.ToString();
                if (mode == Mode.Listening)
                {
                    button1.Text = "停止监听";
                }
                else
                {
                    button1.Text = "开始监听";
                }

                if (mode == Mode.Stopping)
                {
                    tableLayoutPanel1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    tableLayoutPanel1.Enabled = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            SendMessage(textBox6.Text);
            
        }
        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).Controls.Clear();
        }
    }
}
