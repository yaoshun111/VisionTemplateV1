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

    }
}
