using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace FastCtr
{
    public partial class TaskFrameUI : UserControl
    {
        Assembly UIAssembly;
        TaskActionCtr tac = new TaskActionCtr();
        public TaskFrameUI()
        {
            InitializeComponent();
            tac.Name = " 界面";
            tac.OnFunctionTaskStarted += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskStarted);
           // tac.OnFunctionTaskOvered += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskOvered);
            tac.OnFunctionTaskFaulted += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskFaulted);
            tac.OnFunctionTaskInitialed += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskInitialed);
        }
        private void label1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择文件";
            dialog.Filter = "exe文件(*.exe)|*.exe|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                DataAction.SaveStatic.SaveBin("UIpath", file);

                UIAssembly = Assembly.LoadFile(file);
                MethodInfo fg = UIAssembly.EntryPoint;
                try
                {
                    tac.Function = new FunctionHandle(() =>
                    {
                        fg.Invoke(null, null);
                    });
              
                }
                catch
                {
                    label1.BackColor = Color.Silver;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (tac.State == StateType.faulted)
            {
                Form form = new Form();
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                form.Text =this.Name+ ":错误消息";
                TextBox txt = new TextBox();
                txt.Dock = DockStyle.Fill;
                txt.ReadOnly = true;
                txt.Multiline = true;
                txt.Width = 500;
                txt.Height = 300;
                txt.ScrollBars = ScrollBars.Both;
                txt.Text = tac.Exp.InnerException.ToString();
                form.Controls.Add(txt);
                form.Show();
            }
        }


        public void Init()
        {
            string file = DataAction.SaveStatic.ReadBin("UIpath") as string;
            if(file==null)
            {
                MessageBox.Show(this.Name + "未找到文件路径!");
                return;
            }
            if (!File.Exists(file))
            {
                MessageBox.Show(this.Name + "文件" + file + "不存在！");
                return;
            }
            UIAssembly = Assembly.LoadFile(file);
            MethodInfo fg = UIAssembly.EntryPoint;
            try
            {
                //tac = new TaskActionCtr();
                tac.Function = new FunctionHandle(() =>
                {
                    fg.Invoke(null, null);
                });
               // label1.BackColor = Color.Red;
            }
            catch
            {
                label1.BackColor = Color.Silver;
            }


            Type t = UIAssembly.GetType("UIform.IOGlobal");
            FieldInfo fInfo = t.GetField("Global");
            foreach (Control ctr in this.Parent.Controls)
            {
                if (ctr.GetType() == typeof(TaskFrameGlobal))
                {
                    TaskFrameGlobal ctrg = ctr as TaskFrameGlobal;

                    fInfo.SetValue(null, ctrg);
                }
            }




        }


        private void button1_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tac.Start();
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.Message);
            }
        }



        private void taskActionCtr1_OnFunctionTaskStarted(object sender, FunctionTaskEventArgs args)
        {
            tac.UIshow(label1);
        }

        private void taskActionCtr1_OnFunctionTaskOvered(object sender, FunctionTaskEventArgs args)
        {
            tac.UIshow(label1);
        }

        private void taskActionCtr1_OnFunctionTaskFaulted(object sender, FunctionTaskEventArgs args)
        {
            tac.UIshow(label1);
        }


        private void taskActionCtr1_OnFunctionTaskInitialed(object sender, FunctionTaskEventArgs args)
        {
            tac.UIshow(label1);
        }



    }
}
