using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using System.IO;
using Timer = System.Windows.Forms.Timer;

namespace UIform
{

   
    public partial class FormMain : Form
    {
        StartControl.Welcom wel = new StartControl.Welcom();
        
        Global g = new Global();
        Timer OpacyTimer = new Timer();

        public FormMain()
        {
            wel.TopMost = true;
            OpacyTimer.Tick += new EventHandler(OpacyTimer_Tick);
            OpacyTimer.Interval = 20;
            wel.Start();
            Opacity = 0;
            InitializeComponent();
            OpacyTimer.Start();

        }
        private void OpacyTimer_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {
                OpacyTimer.Stop();
                OpacyTimer.Tick -= new EventHandler(OpacyTimer_Tick);
            }
            else
            {
                Opacity += 0.2;
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Add(Global.loghelper);//添加日志窗口到窗体
            Thread.Sleep(1900);
            wel.Stop();
            


            //  
            //界面初始化结束后
            //自动程序开始启动

        }

        private void button3_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.setting);

        }

        private void button8_Click(object sender, EventArgs e)
        {

            newPanel1.Show(Global.mainform);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.dataForm);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.login);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            newPanel1.Show(Global.product);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           

        }
    }
}
