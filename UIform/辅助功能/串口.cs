//using SerialPortLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionFram3
{
    public partial class 串口 : Form
    {
        public object lock_obj = new object();
     
        public string FName = "";
        private int _isreceive;
        //public static int I = 0;
 
        public event EventHandler formSendMessageSuccess;
        public event Action<Exception> formException;
        //public static event OnExceptionVEventHandler onExceptionV;

        private  object sendmessage_Block = new object();
        public int IsReceived { get { return _isreceive; } set { _isreceive = value; } }
        public 串口(string name,string EndChar,bool isautoreceive)
        {
            InitializeComponent();
            this.Text = name;
            FName = name;
         
        }

        public 串口(string name,bool isautoreceive)
        {
            InitializeComponent();
      
        }

    
        public void Init()
        {
            try
            {
                saveVarible saveVarible1 = new saveVarible();
                SAVE.ReadVar(FName, ref saveVarible1);
                if (saveVarible1.comboBox1select > comboBox1.Items.Count - 1)
                {
                    return;
                }
                try
                {
                    comboBox1.SelectedIndex = saveVarible1.comboBox1select;
                    comboBox2.SelectedIndex = saveVarible1.comboBox2select;
                    comboBox3.SelectedIndex = saveVarible1.comboBox3select;
                    comboBox4.SelectedIndex = saveVarible1.comboBox4select;
                    comboBox5.SelectedIndex = saveVarible1.comboBox5select;
                    SetSerialPort();
                }
                catch
                {
                    this.Invoke(new Action(() =>
                    {
                        comboBox1.SelectedIndex = saveVarible1.comboBox1select;
                        comboBox2.SelectedIndex = saveVarible1.comboBox2select;
                        comboBox3.SelectedIndex = saveVarible1.comboBox3select;
                        comboBox4.SelectedIndex = saveVarible1.comboBox4select;
                        comboBox5.SelectedIndex = saveVarible1.comboBox5select;
                        SetSerialPort();
                    }));
                }
                TASKconnect();
             
            }
            catch (Exception exp)
            {
              
            }
        }
        private void SetSerialPort()
        {
            //SimpleSerialPort1.SetPort(comboBox1.Text);
            try
            {
               
               
            }
            catch (Exception exp)
            {
              
            }
        }

        private void menuButton1_SelectedChanged(object sender, EventArgs e)
        {
            if (menuButton1.ControlText == "打开串口")
            {
                try
                {
                    TASKconnect();
                }
                catch 
                {}
            }
            else
            {
                TASKdisconnect();
            }
            menuButton1.Selected = false;
        }

        public void TASKconnect()
        {

            Task task = new Task(() =>
            {
                
            });
            task.Start();
            int chaoshi = 0;
            int max = 15;
       
          
        }
        public void TASKdisconnect()
        {
         
        }

        public void SendMessageString(string message)
        {
          
        }

        public void TASKsendMessageString(string message)
        {
            //Task task = new Task(new Action(() =>
                        //task.Start();
        }


        //public string TASKsendBackMessageString(string message)
        //{
        //    Task task = new Task(new Action(() =>
        //    {
        //        SimpleSerialPort1.SendMessageString(message);
        //    }));
        //    task.Start();
        //}


        private void 串口_Load(object sender, EventArgs e)
        {
           
          
        }
      

     
        private void SimpleSerialPort1_MessageSendSuccess(object sender, EventArgs e)
        {
            menuButton3.Selected = false;
            if (formSendMessageSuccess != null)
            {
                formSendMessageSuccess(this, EventArgs.Empty);
            }
        }
        private void SimpleSerialPort1_OnException(Exception e)
        {
            if (formException != null)
            {
                formException(e);
            }
            //if (onExceptionV != null)
            //{
            //    onExceptionV(new OnExceptionVEventArgs(this.Text + e.Message));
            //}
        }

        private void menuButton3_SelectedChanged(object sender, EventArgs e)
        {
            TASKsendMessageString(textBox2.Text);
        }

        private void menuButton4_SelectedChanged(object sender, EventArgs e)
        {
           
        }

        private void 串口_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason==CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            object a = comboBox1.SelectedItem;
            comboBox1.DataSource = SerialPort.GetPortNames();
            comboBox1.SelectedItem = a;
        }
    }
   
}
