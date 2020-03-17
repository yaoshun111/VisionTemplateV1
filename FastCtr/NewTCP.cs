using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastCtr
{
    [Serializable]
    struct NewTCPSetting
    {
        public string ip;
        public int port;
    }

    public partial class NewTCP : UserControl
    {
        Socket socketC = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        NewTCPSetting setting = new NewTCPSetting();
        IPEndPoint remoteIpep;
        string ContempData = string.Empty;
        Mode mode = Mode.Stopping;
        AutoResetEvent resetevent = new AutoResetEvent(false);
        private bool monitorStatus;
        /// <summary>
        /// 监听的状态
        /// </summary>
        public bool MonitorStatus { get => monitorStatus; set => monitorStatus = value; }

        public NewTCP()
        {
            InitializeComponent();
            MonitorStatus = false;//监听状态默认为假
        }
        enum Mode
        {
            Listening,
            Stopping
        }

        public NewTCP(string name)
        {
            InitializeComponent();
            Name = name;
            setting.ip = "0.0.0.0";
            setting.port = 0;
            try
            {
                setting = (NewTCPSetting)FastData.SaveStatic.ReadBinF(name);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            remoteIpep = new IPEndPoint(IPAddress.Parse(setting.ip), setting.port);
        }

        /// <summary>
        /// 连接
        /// </summary>
        public bool Connect()
        {
            try
            {
                socketC = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketC.Connect(remoteIpep);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (socketC.Connected)
            {
                socketC.Disconnect(true);
                socketC.Close();
            }
        }

        /// <summary>
        /// 反馈当前TCP的连接状态
        /// </summary>
        /// <returns></returns>
        public bool ConnectionStatus()
        {
            return socketC.Connected;
        }

        /// <summary>
        /// TCP 重连
        /// </summary>
        public void Reconnection()
        {
            if (!socketC.Connected)
            {
                Connect();
            }
        }

        /// <summary>
        /// 发送一次信息  
        /// </summary>
        /// <param name="sendContent"></param>
        public void SendMessage(string sendContent)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(sendContent);
                socketC.Send(data);
            }
            catch (Exception)
            {
                throw new Exception("TCP发送失败！");
            }

        }

        /// <summary>
        /// 接受一次信息
        /// </summary>
        /// <returns></returns>
        public string ReceiveMessage()
        {
            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "监测线程";
            }
            byte[] bytRecv = new byte[1024];
            socketC.Receive(bytRecv);
            List<byte> byt = new List<byte>();
            foreach (byte a in bytRecv)
            {
                if (a != 0)
                {
                    byt.Add(a);
                }
            }
            string message = Encoding.UTF8.GetString(byt.ToArray(), 0, byt.Count);
            ContempData = message;
            return message;
        }


        /// <summary>
        /// 发送消息并接收返回值，有超时
        /// </summary>
        /// <param name="sendContent"></param>
        /// <param name="millisecondsTimeOut"></param>
        /// <returns></returns>
        public string SendBackMessageInstant(string sendContent, int millisecondsTimeOut)
        {
            byte[] data = Encoding.UTF8.GetBytes(sendContent);
            resetevent.Reset();
            socketC.Send(data);
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

        /// <summary>
        /// 持续接收返回值
        /// open后就需要打开
        /// </summary>
        public void StartReceivingMessage()
        {
            MonitorStatus = true;
            Task task = new Task(() =>
            {
                mode = Mode.Listening;
                byte[] data = Encoding.UTF8.GetBytes(socketC.LocalEndPoint.ToString() + ": StartReceiving");
                socketC.Send(data);
                while (mode == Mode.Listening)
                {
                    string message = ReceiveMessage();
                    ContempData = message;
                    resetevent.Set();
                }
            }, TaskCreationOptions.LongRunning);
            task.Start();
        }


        public void StartReceivingMessage(Action<string> callback)
        {
            Task task = new Task(() =>
            {
                mode = Mode.Listening;
                byte[] data = Encoding.UTF8.GetBytes(socketC.LocalEndPoint.ToString() + ": StartReceiving");
                socketC.Send(data);
                while (mode == Mode.Listening)
                {
                    string message = ReceiveMessage();
                    ContempData = message;
                    resetevent.Set();
                    callback(ContempData);
                }
            }, TaskCreationOptions.LongRunning);
            task.Start();
        }


        /// <summary>
        /// 关闭持续接收信息的线程
        /// </summary>
        public void StopReceivingMessage()
        {         
            mode = Mode.Stopping;
            System.Threading.Thread.Sleep(10);
            byte[] data = Encoding.UTF8.GetBytes("StopReceiving");
            socketC.Send(data);
            MonitorStatus = false;
        }





        /// <summary>
        /// 获取本地 IP
        /// </summary>
        /// <returns></returns>
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

        private void NewTCP_Load(object sender, EventArgs e)
        {
            if (Application.StartupPath.Contains("Debug"))
            {
                textBox2.Text = setting.ip;
                textBox4.Text = setting.port.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                setting.ip = textBox2.Text;
                setting.port = int.Parse(textBox4.Text);
                remoteIpep = new IPEndPoint(IPAddress.Parse(setting.ip), setting.port);
            }
            catch
            {
                MessageBox.Show("输入信息有误！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FastData.SaveStatic.SaveBinF(Name, setting);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(button5.Text=="打开端口")
            {
                Connect();
                button5.Text = "关闭端口";
            }
            else
            {
                DisConnect();
                button5.Text = "打开端口";
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisConnect();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "开始监听")
            {
                StartReceivingMessage();
                button8.Text = "停止监听";
            }
            else
            {
                StopReceivingMessage();
                button8.Text = "开始监听";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StopReceivingMessage();
        }

        private void NewTCP_ParentChanged(object sender, EventArgs e)
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

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).Controls.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = ContempData;
                if (mode == Mode.Listening)
                {
                    button1.Text = "停止监听";
                    label6.BackColor = Color.LimeGreen;
                }
                else
                {
                    button1.Text = "开始监听";
                    label6.BackColor = Color.Red;
                }

                if (mode == Mode.Stopping)
                {
                    textBox2.Enabled = true;
                    textBox4.Enabled = true;
                    button3.Enabled = true;
                }
                else
                {
                    textBox2.Enabled = false;
                    textBox4.Enabled = false;
                    button3.Enabled = false;
                }

                if (ConnectionStatus())
                {
                    label5.Text = "最近一次已连接";
                    label5.BackColor = Color.LimeGreen;
                    button8.Enabled = true;
                }
                else
                {
                    label5.Text = "最后一次连接失败";
                    label5.BackColor = Color.Red;
                    button8.Enabled = false;
                }


                if (mode == Mode.Listening)
                {
                    button8.Text = "停止监听";
                    label6.Text = "监听中";
                    label6.BackColor = Color.LimeGreen;
                    button5.Enabled = false;
                }
                else
                {
                    button8.Text = "开始监听";
                    label6.Text = "不监听";
                    label6.BackColor = Color.Red;
                    button5.Enabled = true;
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage(textBox3.Text);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
