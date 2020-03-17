using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIform
{
    public partial class 用户注册 : Form
    {
        public 用户注册()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("输入框不能为空！","message");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两次密码不一致！","message");
                return;
            }
    
            this.Close();
            MessageBox.Show("注册成功", "message");

        }

        private void 用户注册_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "操作员";
        }
    }
}
