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
namespace UIform
{
   
    public partial class FormMain : Form
    {
        StartControl.Welcom wel = new StartControl.Welcom();
        Global g = new Global();

        public FormMain()
        {
            wel.Start();
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            wel.Stop();
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
    }
}
