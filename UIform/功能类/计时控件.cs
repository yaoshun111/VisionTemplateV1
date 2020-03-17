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

namespace VisionFram3
{
    public partial class 计时控件 : UserControl
    {
        bool isStarted = false;
        TimeSpan tp;
        private DateTime timelog = new DateTime();
        public new event EventHandler Click;
        public bool IsStarted { get { return isStarted; } }
        public string Value { get { return button1.Text; } set { button1.Text = value; } }
        public double totalMinute { get { return tp.TotalMinutes; }  }
        public 计时控件()
        {
            InitializeComponent();
        }

        private void 计时控件_Load(object sender, EventArgs e)
        {
            //DateTime time1 = DateTime.Now;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            isStarted = true;
            DateTime timenow = DateTime.Now;
            tp = timenow - timelog;

            if (tp.TotalSeconds < 60)
            {
                button1.Text = ((int)(Math.Floor(tp.TotalSeconds))).ToString() + "秒前";
            }
            else if (tp.TotalMinutes < 60)
            {
                button1.Text = ((int)(Math.Floor(tp.TotalMinutes))).ToString() + "分钟前";
            }
            else if (tp.TotalHours < 24)
            {
                button1.Text = ((int)(Math.Floor(tp.TotalHours))).ToString() + "小时前";
            }
            else
            {
                button1.Text = ((int)(Math.Floor(tp.TotalDays))).ToString() + "天前";
            }
        }

        public void Start()
        {
            timelog = DateTime.Now;
            timer1.Start();
            isStarted = true;
        }

        public void ReStart()
        {
            Stop();
            Start();
        }

        public void Stop()
        {
            timer1.Stop();
            timelog = DateTime.Now;
            tp = DateTime.Now - timelog;
            isStarted = false;
            button1.Invoke(new Action(() =>
                {
                    button1.Text = "已停止";
                }));
        }

        private void 计时控件_SizeChanged(object sender, EventArgs e)
        {
            button1.Size = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Click != null)
            {
                Click(sender, e);
            }
        }

        


    }
}
