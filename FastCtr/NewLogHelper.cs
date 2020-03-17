using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FastCtr
{
    public enum MsgType
    {
        Alarm,
        Info,
    }

    public partial class NewLogHelper : UserControl
    {
        Log_Helper log_helper = new Log_Helper("");
        string m_filepath = new DirectoryInfo("../../../Logs/").FullName;
        //string m_filepath = "log";
        [Category("新增")]
        public string FilePath
        {
            get
            {
                return m_filepath;

            }
            set
            {
                m_filepath = value;
            }
        }

        public Log_Helper Log_helper
        {
            get
            {
                return log_helper;
            }
        }

        public void Log(string data, MsgType msgType, bool isShown)
        {
            log_helper.DispProcess(data, msgType.ToString(), isShown);
        }
        
        public NewLogHelper()
        {
            InitializeComponent();
            log_helper = new Log_Helper(m_filepath);
            log_helper.eventDispProcess += new Log_Helper.delegateDispProcess(log_helper_eventDispProcess);
            this.Dock = DockStyle.Fill;
        }

        private void log_helper_eventDispProcess(string msg, Color foreColor)
        {
            if (this.Parent != null)
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    updataRichText(txtProcessDisp, 20);
                    txtProcessDisp.Focus();
                    txtProcessDisp.SelectionColor = ForeColor;
                    txtProcessDisp.AppendText(msg);

                    //滚动条到最下面
                    txtProcessDisp.Select(txtProcessDisp.Text.Length, 0);
                    txtProcessDisp.ScrollToCaret();
                }));
            }
        }

        /// <summary>
        /// 限制richTextbox的行数
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="maxLinds"></param>
        private void updataRichText(System.Windows.Forms.RichTextBox richTextBox, int maxLinds)
        {
            if (richTextBox.Lines.Length >= maxLinds)
            {
                richTextBox.SelectionStart = 0;
                richTextBox.SelectionLength = richTextBox.Text.IndexOf("\n") + 1;
                richTextBox.ReadOnly = false;
                richTextBox.SelectedText = "";
                richTextBox.ReadOnly = true;
                //光标聚焦在最后一行
                richTextBox.Select(richTextBox.Text.Length, 0);
                richTextBox.ScrollToCaret();
            }
        }

        private void NewLogHelper_Load(object sender, EventArgs e)
        {
           
        }
    }
}
