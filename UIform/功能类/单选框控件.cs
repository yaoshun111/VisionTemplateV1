using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionFram3.功能类
{
    public partial class 单选框控件 : UserControl
    {
        public 单选框控件()
        {
            InitializeComponent();
        }

        private void 单选框控件_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel1.Location = this.Location;
            flowLayoutPanel1.Size = this.Size;
        }



    }
}
