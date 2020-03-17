using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace FastCtr
{
    [Serializable]
   public struct NewSerialPortSetting
    {
        public string _port;
        public int _baudrate ;
        public Parity _parity ;
        public int _databit ;
        public StopBits _stopbit ;
    }



    public partial class NewSerialPort : UserControl
    {
        private SerialPort SerialPort;
        AutoResetEvent resetevent = new AutoResetEvent(false);
        private NewSerialPortSetting _setting;
        string ContempData = "";
      
        public NewSerialPortSetting Setting
        {
            get
            {
                return _setting;
            }
            set
            {
                _setting = value;
            }
        
        }
        






        public bool IsOpen
        {
            get
            {
                if (SerialPort == null)
                    return false;
                return SerialPort.IsOpen;
            }
        }

        public NewSerialPort()
        {
            InitializeComponent();
            //SerialPort _serialPort = new SerialPort(setting._port, setting._baudrate, setting._parity, setting._databit, setting._stopbit);
           // NewserialPort = _serialPort;
        }
        //从本地导入串口配置
        public NewSerialPort(string name)
        {
            InitializeComponent();
            Name = name;
            try
            {
                Setting = (NewSerialPortSetting)FastData.SaveStatic.ReadBinF(Name);
            }
            catch
            {
                MessageBox.Show(Name+"--串口配置失败");
                _setting = new NewSerialPortSetting();
                _setting._port = "COM1";
                _setting._baudrate = 9600;
                _setting._databit = 7;
                _setting._stopbit = StopBits.One;
                _setting._parity = Parity.Even;
            }
            SerialPort _serialPort = new SerialPort(Setting._port, Setting._baudrate, Setting._parity, Setting._databit, Setting._stopbit);
            SerialPort = _serialPort;
            //SerialPort.DataReceived += new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
        }



        public NewSerialPort(string _port, int _baudrate, Parity _parity, int _databit, StopBits _stopbit)
        {
            InitializeComponent();
            SerialPort _serialPort = new SerialPort(_port, _baudrate, _parity, _databit, _stopbit);
            SerialPort = _serialPort;
            //SerialPort.DataReceived += new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
        }





        public void Open()
        {
            if (SerialPort != null)
            {
                if (SerialPort.IsOpen)
                {
                    Close();
                    SerialPort.Open();
                }
                else
                {
                    SerialPort.Open();
                }
                SerialPort.DataReceived += new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
            }
        }

        public void Close()
        {
            if (SerialPort != null)
            {
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                    SerialPort.DataReceived -= new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
                }
            }
        }


        public void SendMessage(string sendContent)
        {
            if (!SerialPort.IsOpen)
            {
                return;
            }
            SerialPort.ReadExisting();  //清除串口数据
            Thread.Sleep(10);
            SerialPort.Write(sendContent);
        }


        [Obsolete]
        public string SendBackMessageIntime(string sendContent, int millisecondsTimeWait)
        {
            if (millisecondsTimeWait >= 0)
            {
                if (!SerialPort.IsOpen)
                {
                    return null;
                }
                SerialPort.ReadExisting();  //清除串口数据
                Thread.Sleep(10);
                SerialPort.Write(sendContent);
                Thread.Sleep(millisecondsTimeWait);
                ContempData = SerialPort.ReadExisting();
                return ContempData;
            }
            else
            {
                if (!SerialPort.IsOpen)
                {
                    return null;
                }
                SerialPort.ReadExisting();  //清除串口数据
                Thread.Sleep(10);
                return null;
            }
        }

        private void NewserialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (mode == Mode.debug)
                ContempData = SerialPort.ReadExisting();
            //else
                resetevent.Set();
        }

        //超时后返回空值null  参数millisecondsTimeOut<0时，永远等待
        public string SendBackMessageInstant(string sendContent,int millisecondsTimeOut)
        {
            if (!SerialPort.IsOpen)
            {
                return null;
            }
            SerialPort.ReadExisting();    //清除串口数据
            resetevent.Reset();
            Thread.Sleep(10);
            //SerialPort.DataReceived += new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
            SerialPort.Write(sendContent);
            if (millisecondsTimeOut >= 0)
            {
                if (!resetevent.WaitOne(millisecondsTimeOut))
                {
                    //SerialPort.DataReceived -= new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
                    return null;
                }
                else
                {
                   // SerialPort.DataReceived -= new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
                    //ContempData = SerialPort.ReadExisting();
                    return ContempData;
                }
            }
            else
            {
                if (!resetevent.WaitOne())
                {
                   // SerialPort.DataReceived -= new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
                    return null;
                }
                else
                {
                   // SerialPort.DataReceived -= new SerialDataReceivedEventHandler(NewserialPort_DataReceived);
                   // ContempData = SerialPort.ReadExisting();
                    return ContempData;
                }
            }
        }


        private void NewSerialPort_Load(object sender, EventArgs e)
        {
            
            comboBox1.DataSource = SerialPort.GetPortNames();
            comboBox2.SelectedIndex = 5;
            comboBox3.DataSource = Enum.GetNames(typeof(Parity));
            comboBox4.SelectedIndex = 0;
            comboBox5.DataSource = Enum.GetNames(typeof(StopBits));
            comboBox5.SelectedIndex = 1;
            try
            {
                NewSerialPortSetting setting = (NewSerialPortSetting)FastData.SaveStatic.ReadBinF(this.Name);
                comboBox1.SelectedItem = setting._port;
                comboBox2.SelectedItem = setting._baudrate.ToString();
                comboBox3.SelectedItem = setting._parity.ToString();
                comboBox4.SelectedItem = setting._databit.ToString();
                comboBox5.SelectedItem = setting._stopbit.ToString();
            }
            catch { }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            FastData.SaveStatic.SaveBinF(Name, Setting);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "打开")
            {
                try
                {
                    _setting._port = comboBox1.Text;
                    _setting._baudrate = int.Parse(comboBox2.Text);
                    _setting._parity = (Parity)Enum.Parse(typeof(Parity), comboBox3.Text);
                    _setting._databit = int.Parse(comboBox4.Text);
                    _setting._stopbit = (StopBits)Enum.Parse(typeof(StopBits), comboBox5.Text);
                    SerialPort _serialPort = new SerialPort(_setting._port, _setting._baudrate, _setting._parity, _setting._databit, _setting._stopbit);
                    SerialPort = _serialPort;
                    Open();
                    if (SerialPort.IsOpen)
                    {
                        label5.BackColor = Color.Red;
                        tableLayoutPanel1.Enabled = false;
                        button2.Text = "关闭";
                        label8.Text = _setting._port;
                    }
                }
                catch
                {
                    label5.BackColor = Color.Black;
                }
            }
            else
            {
                try
                {
                    Close();
                    label5.BackColor = Color.Black;
                    tableLayoutPanel1.Enabled = true;
                    button2.Text = "打开";
                    label8.Text = _setting._port;
                }
                catch
                {
                    label5.BackColor = Color.Yellow;
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (!SerialPort.IsOpen)
            {
                return ;
            }
            //textBox1.Text = SendBackMessageInstant(textBox2.Text, -1);
            SendMessage(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            fld.Description = "选择存放目录";
            if (fld.ShowDialog() == DialogResult.OK)
            {
                string path = fld.SelectedPath;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            _setting._port = comboBox1.Text;
            _setting._baudrate = int.Parse(comboBox2.Text);
            _setting._parity = (Parity)Enum.Parse(typeof(Parity), comboBox3.Text);
            _setting._databit = int.Parse(comboBox4.Text);
            _setting._stopbit = (StopBits)Enum.Parse(typeof(StopBits), comboBox5.Text);
            SerialPort _serialPort = new SerialPort(Setting._port, Setting._baudrate, Setting._parity, Setting._databit, Setting._stopbit);
            SerialPort = _serialPort;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsOpen)
            {
                label5.BackColor = Color.LimeGreen;
                button2.Text = "关闭";
                label8.Text = Setting._port;
                textBox1.Text = ContempData;
            
             
            }
            else
            {
                label5.BackColor = Color.Gray;
                button2.Text = "打开";
            }
        }

        private void NewSerialPort_ParentChanged(object sender, EventArgs e)
        {

            if (Application.StartupPath.Contains("Debug"))
            {
                if (this.ParentForm != null)
                {
                    ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                    ParentForm.AutoSize = true;
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
            this.ParentForm.Controls.Clear();
        }


        private void button4_Click(object sender, EventArgs e)
        {
         
        }
    }
}
