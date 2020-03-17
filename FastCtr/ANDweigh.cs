using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FastCtr
{
    public partial class ANDweigh : UserControl
    {
        private NewSerialPort ANDserial;
       
        public ANDweigh()
        {
            InitializeComponent();
            
        }

        private void ANDweigh_Load(object sender, EventArgs e)
        {

        }

        public ANDweigh(NewSerialPort serialport)
        {
            
            InitializeComponent();
            ANDserial = serialport;
            Name = ANDserial.Name;
    
        }

        public void PowerON()
        {
            if (ANDserial == null)
                return;
            if (ANDserial.IsOpen)
            {
                ANDserial.SendMessage("ON\r\n");
                Thread.Sleep(5000);   //停留5秒钟开机
            }
        }


        public bool IsPowerON()
        {
            if (GetWeightInstant(1000) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void PowerOFF()
        {
            if (ANDserial == null)
                return;
            if (ANDserial.IsOpen)
                ANDserial.SendMessage("OFF\r\n");
        }

        /// <summary>
        /// 获得及时称重值
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string GetWeightInstant(int timeout)
        {
            if (ANDserial == null)
                return null;
            string message = ANDserial.SendBackMessageInstant("Q\r\n", timeout);
            if (message == null)
            {
                return null;
            }
            else
            {
                return ConvertString(message);
            }
        }

        /// <summary>
        /// 获取稳定称重值
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string GetWeightStable(int timeout)
        {
            if (ANDserial == null)
                return null;
            string message = ANDserial.SendBackMessageInstant("S\r\n", timeout);
            if (message == null)
            {
                return null;
            }
            else
            {
                return ConvertString(message);
            }
        }

        /// <summary>
        /// 清零-有超时（不建议使用）
        /// </summary>
        /// <param name="milliSenconds"></param>
        /// <returns></returns>
        public bool Zero(int milliSenconds)
        {
            int timeout = 0;
            if (ANDserial == null)
                return false;
            ANDserial.SendMessage("R\r\n");
            while (String.IsNullOrEmpty(ANDserial.SendBackMessageInstant("Q\r\n", 2000)) && timeout < milliSenconds / 100)
            {
                Thread.Sleep(5);
                timeout++;
            }
            if (timeout >= milliSenconds / 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 清零，不带超时（常用）
        /// </summary>
        public void ZeroV()
        {
           
            if (ANDserial == null)
                return ;
            ANDserial.SendMessage("R\r\n");
            while (String.IsNullOrEmpty(ANDserial.SendBackMessageInstant("Q\r\n", 2000)))
            {
                Thread.Sleep(5);
               
            }
        }

        private string ConvertString(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;
            string ret = "";
            if (content.Contains("ST,") || content.Contains("US,"))
            {
                char[] chenzhong = content.Split(',')[1].ToCharArray(0, 9);
                for (int i = 0; i < 9; i++)
                {
                    ret += chenzhong[i];
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

     
        private void btnSwitchOn_Click(object sender, EventArgs e)
        {
            PowerON();
        }

        private void btnSwitchOff_Click(object sender, EventArgs e)
        {
            PowerOFF();
        }

        private void btnRealTimeWeight_Click(object sender, EventArgs e)
        {
            string ret = GetWeightInstant(1000);
            if (!string.IsNullOrEmpty(ret))
                textBox1.Text = ret;
            else
                textBox1.Text = "err";
        }

        private void btnStableTimeWeight_Click(object sender, EventArgs e)
        {
            string ret = GetWeightStable(3000);
            if (!string.IsNullOrEmpty(ret))
                textBox2.Text = ret;
            else
                textBox2.Text = "err";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ZeroV();
        }

        private void btnSerialPortSetting_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.AutoSize = true;
            form.Text = ANDserial.Name;
            form.Controls.Add(ANDserial);
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Form)sender).Controls.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ANDserial.IsOpen)
            {
                this.BackColor = Color.GreenYellow;
                label5.Text = ANDserial.Setting._port + "-已打开";
            }
            else
            {
                this.BackColor = Color.LightGray;
                label5.Text = "已关闭";
            }
        }

        private void ANDweigh_ParentChanged(object sender, EventArgs e)
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
            ParentForm.Controls.Clear();
        }
    }
}
