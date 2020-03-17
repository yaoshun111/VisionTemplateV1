using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionFram3
{
    public partial class 行消息 : UserControl
    {
        //public new  event MouseEventHandler Click;
        public int Index { set { label1.Text = value.ToString(); } get { return int.Parse(label1.Text); } }
        public string Content { set { textBox1.Text = value.ToString(); } get { return textBox1.Text; } }
        public string Code { set { label2.Text = value.ToString(); } get { return label2.Text; } }
        public FlowLayoutPanel flowLayoutPanel;
        public 行消息(int index,string code, string content)
        {
            InitializeComponent();
            Index = index;
            Content = content;
            Code = code;
        }
        public void AddTo(FlowLayoutPanel _flowLayoutPanel)
        {
            _flowLayoutPanel.Controls.Add(this);
            _flowLayoutPanel.Controls.SetChildIndex(this, 0);
            flowLayoutPanel = _flowLayoutPanel;
        }

        public 行消息()
        {
            InitializeComponent();
        }

    


        private void 增加删除行消息_Resize_1(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("确认清除错误？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
            if (ret == DialogResult.OK)
                flowLayoutPanel.Controls.Remove(this);
        }



    }
}
