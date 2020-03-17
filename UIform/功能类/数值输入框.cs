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
    public partial class 数值输入框 : UserControl
    {
        public event MouseEventHandler onInvalidate;
        public 数值输入框()
        {
            InitializeComponent();
        }

        public  double Value
        {
            get
            {
                return double.Parse(textBox1.Text);
            }
            set
            {
                textBox1.Text = value.ToString();
            }
        }

        private void textBox1_SizeChanged(object sender, EventArgs e)
        {
            this.Size = textBox1.Size;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                label1.Focus();
            }
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if (((e.KeyChar < '0') && (e.KeyChar != '.') && (e.KeyChar != '-')) || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (onInvalidate != null)
                onInvalidate(null, null);
        }



    }
}
