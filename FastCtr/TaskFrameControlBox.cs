using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastCtr
{
    public partial class TaskFrameControlBox : UserControl
    {
        TaskFrameGlobal Global;
        public TaskFrameControlBox()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.TaskFrameCtr_Load);
            taskActionCtr.OnFunctionTaskStarted += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskStarted);
           // taskActionCtr.OnFunctionTaskOvered += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskOvered);
            taskActionCtr.OnFunctionTaskFaulted += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskFaulted);
            taskActionCtr.OnFunctionTaskInitialed += new FunctionTaskEvent(taskActionCtr1_OnFunctionTaskInitialed);
        }
    
        public  string TaskText
        {
            get
            {
                taskActionCtr.Text = Name;
                return Name;
            }
            set
            {
                taskActionCtr.Text = Name;
            }
        }

        private void TaskFrameCtr_Load(object sender, EventArgs e)
        {
            taskActionCtr.Name = Name;
            taskActionCtr.Text = Name;
            taskActionCtr.UIshow(button1);
        }

        public void Init()
        {
            try
            {
                taskActionCtr.Init();
                comboBox2.Items.Clear();
               
                
                foreach (Control ctr in this.Parent.Controls)
                {
                    if (ctr.GetType() == typeof(TaskFrameGlobal))
                    {
                        try
                        {
                            taskActionCtr.SetProperty("Global", ctr);
                        }
                        catch(Exception exp)
                        {
                            throw new Exception("设置全局变量失败！" + exp.Message);
                        }
                    }
                }
                comboBox2.Items.Add("Local");
                comboBox2.Items.Add("Global");
                comboBox2.SelectedIndex = 0;
            }
            catch(Exception exp)
            {
                MessageBox.Show(this.Name + "初始化失败！" + exp.Message);
                return;
            }
        }


        private void taskActionCtr1_OnFunctionTaskStarted(object sender, FunctionTaskEventArgs args)
        {
            taskActionCtr.UIshow(button1);
        }

        private void taskActionCtr1_OnFunctionTaskOvered(object sender, FunctionTaskEventArgs args)
        {
            taskActionCtr.UIshow(button1);
        }

        private void taskActionCtr1_OnFunctionTaskFaulted(object sender, FunctionTaskEventArgs args)
        {
            taskActionCtr.UIshow(button1);
        }


        private void taskActionCtr1_OnFunctionTaskInitialed(object sender, FunctionTaskEventArgs args)
        {
            taskActionCtr.UIshow(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (button2.Text == "启动")
                {
                    button2.Text = "停止";
                    taskActionCtr.Start();
                }
                else
                {
                    button2.Text = "启动";
                    taskActionCtr.Start();
                }



            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Local")
            {
                try
                {
                    Type type = Type.GetType(textBox2.Text);
                    if (type.BaseType == typeof(Enum))
                    {
                        var value = Enum.Parse(type, comboBox3.Text);
                        taskActionCtr.SetVarible(comboBox1.Text, value);
                    }
                    else
                    {
                        var value = Convert.ChangeType(comboBox3.Text, type);
                        taskActionCtr.SetVarible(comboBox1.Text, value);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            if (comboBox2.Text == "Global")
            {
                //try
                //{
                    Type type = Type.GetType(textBox2.Text);
                    if (type.BaseType == typeof(Enum))
                    {
                        var value = Enum.Parse(type, comboBox3.Text);
                         Global.taskGlobalCtr.SetVarible(comboBox1.Text, value);
                    }
                    else
                    {
                        var value = Convert.ChangeType(comboBox3.Text, type);
                        //taskActionCtr.SetVarible(Global.taskGlobalCtr.TaskInstance,comboBox1.Text, value);
                        Global.taskGlobalCtr.SetVarible(comboBox1.Text, value);
                    }
                //}
                //catch (Exception exp)
                //{
                //    MessageBox.Show(exp.Message);
                //}
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Local")
            {
                comboBox1.DataSource = null;
                comboBox1.DataSource =this. taskActionCtr.GetAllVaribles();
            }
            if (comboBox2.Text == "Global" )
            {
                comboBox1.DataSource = null;
                Global = taskActionCtr.GetProperty("Global") as TaskFrameGlobal;
                comboBox1.DataSource = Global.taskGlobalCtr.GetAllVaribles();
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.DataSource != null)
            {
                if (comboBox2.Text == "Local")
                {
                    comboBox3.DataSource = null;
                    comboBox3.Items.Clear();
                    var value = taskActionCtr.GetVarible(comboBox1.Text);
                    Type t = value.GetType();
                    if (t.BaseType == typeof(Enum))
                    {
                        var d = t.GetEnumNames();
                        comboBox3.DataSource = d;
                    }
                    if (t == typeof(bool))
                    {
                        comboBox3.Items.AddRange(new object[2] { false, true });

                    }


                    comboBox3.Text = value.ToString();
                    textBox2.Text = value.GetType().ToString();
                }
                if (comboBox2.Text == "Global")
                {
                    comboBox3.DataSource = null;
                    comboBox3.Items.Clear();
                    var value = Global.taskGlobalCtr.GetVarible(comboBox1.Text);
                    Type t = value.GetType();
                    if (t.BaseType == typeof(Enum))
                    {
                        var d = t.GetEnumNames();
                        comboBox3.DataSource = d;
                    }
                    if (t == typeof(bool))
                    {
                        comboBox3.Items.AddRange(new object[2] { false, true });

                    }
                    comboBox3.Text = value.ToString();
                    textBox2.Text = value.GetType().ToString();
                }
            }
            else
            {
                textBox2.Text = "";
                comboBox3.DataSource= null;
                comboBox3.Text = "";
          
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (taskActionCtr.State == StateType.faulted)
            {
                Form form = new Form();
                form.Text = this.Name + ":错误消息";
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                TextBox txt = new TextBox();
                txt.ReadOnly = true;
                txt.Dock = DockStyle.Fill;
                txt.Multiline = true;
                txt.Width = 500;
                txt.Height = 300;
                txt.ScrollBars = ScrollBars.Both;
                txt.Text = taskActionCtr.Exp.InnerException.ToString();
                form.Controls.Add(txt);
                form.ShowDialog();
            }
        }

        private void TaskFrameControlBox_Load(object sender, EventArgs e)
        {
            taskActionCtr.Name = this.Name;
            taskActionCtr.Text = this.Name;
        }
    }
}
