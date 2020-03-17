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
using DataAction;
using OmroPlcVar;
using System.Collections;

namespace FastCtr
{
    public partial class TaskFrameGlobal : UserControl
    {
        public Save G_Save ;
        public OmroPLCvar G_OmroPLCvar1;
        public Dictionary<string, TaskFrameCtr> G_TaskFrameCtr;
        public Dictionary<string, OmroPlcMultiVar> G_OmroPlcMultiVar;

        public TaskFrameGlobal()
        {
            InitializeComponent();
            G_Save = new Save();
            G_OmroPLCvar1 = new OmroPLCvar();
            G_TaskFrameCtr = new Dictionary<string, TaskFrameCtr>();
            G_OmroPlcMultiVar = new Dictionary<string, OmroPlcMultiVar>();
            this.Load += new System.EventHandler(this.TaskFrameGlobal_Load);
            taskGlobalCtr.OnFunctionTaskStarted += new FunctionTaskEvent(taskGlobalCtr_OnFunctionTaskStarted);
           // taskGlobalCtr.OnFunctionTaskOvered += new FunctionTaskEvent(taskGlobalCtr_OnFunctionTaskOvered);
            taskGlobalCtr.OnFunctionTaskFaulted += new FunctionTaskEvent(taskGlobalCtr_OnFunctionTaskFaulted);
            taskGlobalCtr.OnFunctionTaskInitialed += new FunctionTaskEvent(taskGlobalCtr_OnFunctionTaskInitialed);
        }

        private void TaskFrameGlobal_Load(object sender, EventArgs e)
        {
            taskGlobalCtr.Name = Name;
            taskGlobalCtr.Text = Name;
            taskGlobalCtr.UIshow(button1);
        }

        public void Init()
        {
            taskGlobalCtr.Init();
            comboBox2.DataSource = new string[1] { "Global" };
            MethodInfo[] methodinfos = taskGlobalCtr.GetMethods();
            listBox2.DataSource = methodinfos;
            foreach (Control ctr in this.Parent.Controls)
            {
                if (ctr.GetType() == typeof(TaskFrameCtr))
                {
                    if (!G_TaskFrameCtr.ContainsKey(ctr.Name))
                        G_TaskFrameCtr.Add(ctr.Name, (TaskFrameCtr)ctr);
                }
                if (ctr.GetType() == typeof(OmroPlcMultiVar))
                {
                    if (!G_OmroPlcMultiVar.ContainsKey(ctr.Name))
                        G_OmroPlcMultiVar.Add(ctr.Name, (OmroPlcMultiVar)ctr);
                }
            }
        }

        private void taskGlobalCtr_OnFunctionTaskStarted(object sender, FunctionTaskEventArgs args)
        {
            taskGlobalCtr.UIshow(button1);
        }

        private void taskGlobalCtr_OnFunctionTaskOvered(object sender, FunctionTaskEventArgs args)
        {
            taskGlobalCtr.UIshow(button1);
        }

        private void taskGlobalCtr_OnFunctionTaskFaulted(object sender, FunctionTaskEventArgs args)
        {
            taskGlobalCtr.UIshow(button1);
        }

        private void taskGlobalCtr_OnFunctionTaskInitialed(object sender, FunctionTaskEventArgs args)
        {
            taskGlobalCtr.UIshow(button1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Global")
            {
                listBox1.DataSource = this.taskGlobalCtr.GetAllVaribles();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            
            var value = taskGlobalCtr.GetVarible(listBox1.SelectedValue.ToString());
            textBox2.Text = value.GetType().FullName;
            Type t = value.GetType();
            if (t.BaseType == typeof(Enum))
            {
                var d = t.GetEnumNames();
                comboBox1.DataSource = d;
            }
            if (t == typeof(bool))
            {
                comboBox1.Items.AddRange(new object[2] { false, true });

            }


            comboBox1.Text = value.ToString();
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
                        var value = Enum.Parse(type, comboBox1.Text);
                        taskGlobalCtr.SetVarible(listBox1.Text, value);
                    }
                    else
                    {
                        var value = Convert.ChangeType(comboBox1.Text, type);
                        taskGlobalCtr.SetVarible(listBox1.Text, value);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            if (comboBox2.Text == "Global")
            {
                try
                {
                    Type type = Type.GetType(textBox2.Text);
                    if (type.BaseType == typeof(Enum))
                    {
                        var value = Enum.Parse(type, comboBox1.Text);
                        taskGlobalCtr.SetVarible(listBox1.Text, value);
                    }
                    else
                    {
                        var value = Convert.ChangeType(comboBox1.Text, type);
                        taskGlobalCtr.SetVarible(listBox1.Text, value);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public object G_GetVar(string _name)
        {

            return taskGlobalCtr.GetVarible(_name);

        }

        public void G_SetVar(string _name, object _value)
        {

            taskGlobalCtr.SetVarible(_name, _value);
        }

        public List<string> G_GetAllVars()
        {
            return taskGlobalCtr.GetAllVaribles();
        }

        public List<string> G_GetBoolVars()
        {
            return taskGlobalCtr.GetBoolVaribles();
        }

    }
}
