using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMRON.Compolet.CIP;
using System.Collections;
using System.Threading;

namespace FastCtr
{


    public partial class OmroPLC : UserControl
    {
        ///public CIPPlcCompolet plcCompolet = new NJCompolet();
        public CIPPlcCompolet PlcCompolet;
        Mode mode = Mode.Stopping;
        public List<string> listvarname = new List<string>();
        public Hashtable ContempData = new Hashtable();
        AutoResetEvent resetevent = new AutoResetEvent(false);
        public OmroPLCSetting Setting = new OmroPLCSetting();
        TimeSpan tsp = new TimeSpan(0, 0, 0);
        public int circletime = 100;
        InfoForm infoform;
        object obj_lock = new object();

        public static Dictionary<string, Task> TaskDic = new Dictionary<string, Task>();

        enum Mode
        {
            Listening,
            Stopping
        }
        public OmroPLC(string name)
        {
            InitializeComponent();
            Name = name;


        }
        public bool Connect()
        {
            PlcCompolet.Active = true;
            if (PlcCompolet.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DisConnect()
        {
            PlcCompolet.Active = false;
            if (!PlcCompolet.IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读单个变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varname"></param>
        /// <returns></returns>
        public T ReadVariable<T>(string varname)
        {
            lock (obj_lock)
            {
                if (PlcCompolet.IsConnected)
                {
                    try
                    {
                        var a = (T)PlcCompolet.ReadVariable(varname);
                        return (T)a;
                    }
                    catch
                    {
                        try
                        {
                            var a = (T)PlcCompolet.ReadVariable(varname);
                            return (T)a;
                        }
                        catch
                        {
                            var a = (T)PlcCompolet.ReadVariable(varname);
                            return (T)a;
                        }
                    }
                }
                else
                {
                    throw new Exception("PLC断开连接！");
                }
            }
        }

        private Hashtable TestMultiVariable(string[] varnames)
        {
            lock (obj_lock)
            {
                try
                {
                    Hashtable hashtable = PlcCompolet.ReadVariableMultiple(varnames);
                    return hashtable;
                }
                catch
                {
                    try
                    {
                        Hashtable hashtable = PlcCompolet.ReadVariableMultiple(varnames);
                        return hashtable;
                    }
                    catch
                    {
                        Hashtable hashtable = PlcCompolet.ReadVariableMultiple(varnames);
                        return hashtable;
                    }
                }
            }
        }


        /// <summary>
        /// 读多个变量
        /// </summary>
        /// <param name="varnames"></param>
        /// <returns></returns>
        private Hashtable ReadMultiVariable(string[] varnames)
        {
            lock (obj_lock)
            {
                try
                {
                    Hashtable hashtable = PlcCompolet.ReadVariableMultiple(varnames);
                    if (infoform != null)
                    {
                        this.Invoke(new Action(() =>
                        {
                            infoform.Close();
                            infoform = null;
                        }));
                    }
                    return hashtable;
                }
                catch
                {
                    try
                    {
                        Hashtable hashtable = PlcCompolet.ReadVariableMultiple(varnames);
                        if (infoform != null)
                        {
                            this.Invoke(new Action(() =>
                            {
                                infoform.Close();
                                infoform = null;
                            }));
                        }
                        return hashtable;
                    }
                    catch
                    {
                        if (infoform == null)
                        {
                            infoform = new InfoForm("PLC通讯异常，尝试恢复中...");
                            this.BeginInvoke(new Action(() =>
                            {
                                infoform.StartPosition = FormStartPosition.CenterScreen;
                                infoform.ShowDialog();
                            }));
                        }
                        Thread.Sleep(200);
                        return ReadMultiVariable(varnames);
                    }
                }
            }
        }

        /// <summary>
        /// 写单个变量
        /// </summary>
        /// <param name="varname"></param>
        /// <param name="value"></param>
        public void WriteVariable(string varname, object value)
        {
            lock (obj_lock)
            {
                if (PlcCompolet.IsConnected)
                {
                    try
                    {
                        PlcCompolet.WriteVariable(varname, value);
                    }
                    catch
                    {
                        try
                        {
                            PlcCompolet.WriteVariable(varname, value);
                        }
                        catch
                        {
                            PlcCompolet.WriteVariable(varname, value);
                        }
                    }
                }
                else
                {
                    throw new Exception("PLC断开连接！");
                }
                //if (PlcCompolet != null)
                //{
                //    try
                //    {
                //        PlcCompolet.WriteVariable(varname, value);
                //        if (infoform != null)
                //        {
                //            this.Invoke(new Action(() =>
                //            {
                //                infoform.Close();
                //                infoform = null;
                //            }));
                //        }
                //    }
                //    catch
                //    {
                //        try
                //        {
                //            PlcCompolet.WriteVariable(varname, value);
                //            if (infoform != null)
                //            {
                //                this.Invoke(new Action(() =>
                //                {
                //                    infoform.Close();
                //                    infoform = null;
                //                }));
                //            }
                //        }
                //        catch
                //        {
                //            if (infoform == null)
                //            {
                //                infoform = new InfoForm("PLC通讯异常，尝试恢复中...");
                //                this.BeginInvoke(new Action(() =>
                //                {
                //                    infoform.StartPosition = FormStartPosition.CenterScreen;
                //                    infoform.ShowDialog();
                //                }));
                //            }
                //            Thread.Sleep(200);
                //            WriteVariable(varname, value);
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// 循环监听PLC变量 - - 无回调
        /// </summary>
        public void StartReceivingMessage()
        {
            if (mode != Mode.Listening && PlcCompolet.IsConnected)
            {
                Hashtable hashtable = TestMultiVariable(listvarname.ToArray());
                //int n = listvarname.Count();
                //for (int i = 0; i < n; i++)
                //{
                //    hashtable.Add(listvarname[i], 0);
                //}
                ContempData = hashtable;
                Task task = new Task(() =>
                {
                    mode = Mode.Listening;
                    DateTime time = DateTime.Now;
                    while (mode == Mode.Listening)
                    {
                        Thread.Sleep(circletime);
                        hashtable = ReadMultiVariable(listvarname.ToArray());
                        ContempData = hashtable;
                        tsp = DateTime.Now - time;
                        time = DateTime.Now;
                        //resetevent.Set();
                    }
                }, TaskCreationOptions.LongRunning);
                task.Start();
            }
        }

        /// <summary>
        ///  循环监听PLC变量 - -有回调
        /// </summary>
        /// <param name="PlcReceivingCallback"></param>
        public void StartReceivingMessage(Action<string, object> PlcReceivingCallback)
        {
            if (mode != Mode.Listening && PlcCompolet.IsConnected)
            {
                Hashtable hashtable = TestMultiVariable(listvarname.ToArray());
                //int n = listvarname.Count();
                //for (int i = 0; i < n; i++)
                //{
                //    hashtable.Add(listvarname[i], 0);
                //}
                ContempData = hashtable;
                Task task = new Task(() =>
                {
                    mode = Mode.Listening;
                    DateTime time = DateTime.Now;
                    while (mode == Mode.Listening)
                    {
                        Thread.Sleep(circletime);
                        hashtable = ReadMultiVariable(listvarname.ToArray());
                        foreach (string name in listvarname)
                        {
                            if (!ContempData[name].Equals(hashtable[name]))
                            {
                                if (TaskDic.ContainsKey(name + "=" + hashtable[name]))
                                {
                                    if (TaskDic[name + "=" + hashtable[name]].IsCompleted || TaskDic[name].IsCanceled || TaskDic[name].IsFaulted)
                                    {
                                        Task callbacktask = new Task(() =>
                                          {
                                              PlcReceivingCallback(name, hashtable[name]);
                                          }, TaskCreationOptions.LongRunning);
                                        TaskDic.Remove(name + "=" + hashtable[name]);
                                        TaskDic.Add(name + "=" + hashtable[name], callbacktask);
                                        callbacktask.Start();
                                    }
                                }
                                else
                                {
                                    Task callbacktask = new Task(() =>
                                    {
                                        PlcReceivingCallback(name, hashtable[name]);
                                        //TaskDic.Remove(name + "=" + hashtable[name]);
                                    }, TaskCreationOptions.LongRunning);
                                    TaskDic.Add(name + "=" + hashtable[name], callbacktask);
                                    callbacktask.Start();
                                }
                            }
                        }
                        ContempData = hashtable;
                        tsp = DateTime.Now - time;
                        time = DateTime.Now;
                        //resetevent.Set();
                    }
                }, TaskCreationOptions.LongRunning);
                task.Start();
            }
            else
            {
                throw new Exception("监听模式启动失败！");
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void StopReceivingMessage()
        {
            mode = Mode.Stopping;
        }



        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PlcCompolet != null)
            {
                if (PlcCompolet.IsConnected)
                {
                    label11.Text = "PLC连接成功！";
                    label11.BackColor = Color.LimeGreen;
                }
                else
                {
                    label11.Text = "PLC断开连接！";
                    label11.BackColor = Color.Red;
                }
            }
            else
            {
                label11.Text = "PLC未初始化！";
                label11.BackColor = Color.Gray;
            }

            if (mode == Mode.Listening)
            {
                groupBox1.Enabled = false;
                groupBox5.Enabled = false;
                button5.Enabled = false;
                label12.Text = "监听中";
                label12.BackColor = Color.LimeGreen;
                for (int i = 0; i < listvarname.Count; i++)
                {
                    string var = listView1.Items[i].SubItems[0].Text;
                    listView1.Items[var].SubItems[1].Text = ContempData[var].ToString();
                }
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox5.Enabled = true;
                button5.Enabled = true;
                label12.Text = "不监听";
                label12.BackColor = Color.Red;
            }


            Dictionary<string, Task> taskdic = TaskDic;
            List<string> tasknames = taskdic.Keys.ToList();
            for (int i = 0; i < tasknames.Count; i++)
            {
                if (!doubleBufferListView1.Items.ContainsKey(tasknames[i]))
                {
                    doubleBufferListView1.Items.Add(tasknames[i], tasknames[i], 0);
                    doubleBufferListView1.Items[tasknames[i]].SubItems.Add("");
                }
                else
                {
                    if (taskdic.ContainsKey(tasknames[i]))
                    {
                        if (taskdic[tasknames[i]].IsCompleted)
                        {
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].Text = "空闲";
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].BackColor = Color.Blue;
                        }
                        else if (taskdic[tasknames[i]].IsFaulted)
                        {
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].Text = "错误";
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].BackColor = Color.Yellow;
                        }
                        else
                        {
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].Text = "忙碌";
                            doubleBufferListView1.Items[tasknames[i]].SubItems[1].BackColor = Color.Red;
                        }
                    }
                }
            }

        }

        private void OmroPLC_ParentChanged(object sender, EventArgs e)
        {
            if (Application.StartupPath.Contains("Debug"))
            {
                if (ParentForm != null)
                {
                    ParentForm.AutoSize = true;
                    ParentForm.Text = Name;
                    timer1.Start();
                    timer2.Start();
                    ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                }
                else
                {
                    timer1.Stop();
                    timer2.Stop();
                }
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentForm.Controls.Clear();
        }

        private void OmroPLC_Load(object sender, EventArgs e)
        {
            if (Application.StartupPath.Contains("Debug"))
            {
                comboBox1.Items.AddRange(new object[2] { ConnectionType.Class3, ConnectionType.UCMM });
                comboBox3.Items.AddRange(new object[2] { true, false });
                try
                {
                    comboBox1.SelectedItem = Setting.ConnectionType;
                    textBox4.Text = Setting.ReceiveTimeLimit.ToString();
                    comboBox3.SelectedItem = Setting.UseRoutePath;
                    textBox6.Text = Setting.PeerAddress;
                    textBox7.Text = Setting.LocalPort.ToString();
                    comboBox2.Items.AddRange(listvarname.ToArray());
                    //listvarname = Setting.listvar;
                    textBox2.Text = Setting.circletime.ToString();
                    foreach (string var in listvarname)
                    {
                        listView1.Items.Add(var, var, 0);
                        listView1.Items[var].SubItems.Add("0");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Setting.ConnectionType = (ConnectionType)comboBox1.SelectedItem;
                Setting.ReceiveTimeLimit = long.Parse(textBox4.Text);
                Setting.UseRoutePath = (bool)comboBox3.SelectedItem;
                Setting.PeerAddress = textBox6.Text;
                Setting.LocalPort = int.Parse(textBox7.Text);
                Setting.circletime = int.Parse(textBox2.Text);

                PlcCompolet.ConnectionType = Setting.ConnectionType;
                PlcCompolet.ReceiveTimeLimit = Setting.ReceiveTimeLimit;
                PlcCompolet.UseRoutePath = Setting.UseRoutePath;
                PlcCompolet.LocalPort = Setting.LocalPort;
                circletime = Setting.circletime;
            }
            catch
            {
                MessageBox.Show("输入信息有误！");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Setting.ConnectionType = (ConnectionType)comboBox1.SelectedItem;
                Setting.ReceiveTimeLimit = long.Parse(textBox4.Text);
                Setting.UseRoutePath = (bool)comboBox3.SelectedItem;
                Setting.PeerAddress = textBox6.Text;
                Setting.LocalPort = int.Parse(textBox7.Text);
                Setting.circletime = circletime;
                FastData.SaveStatic.SaveBinF(Name, Setting);
            }
            catch
            {
                MessageBox.Show("保存到本地失败！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisConnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Text = ContempData[comboBox2.Text].ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                if (!listvarname.Contains(textBox3.Text))
                {
                    listvarname.Add(textBox3.Text);
                    Setting.listvar = listvarname;
                    comboBox2.Items.Add(textBox3.Text);
                    listView1.Items.Add(textBox3.Text, textBox3.Text, 0);
                    listView1.Items[textBox3.Text].SubItems.Add("");
                }
                else
                {
                    MessageBox.Show("变量已存在！");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                listvarname.Remove(textBox3.Text);
                Setting.listvar = listvarname;
                listView1.SelectedItems[0].Remove();
                textBox3.Text = string.Empty;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (PlcCompolet.IsConnected)
                WriteVariable(comboBox2.Text, textBox5.Text);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                StartReceivingMessage();
            }
            catch (Exception exp)
            {
                MessageBox.Show("监听失败！" + exp);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            StopReceivingMessage();
        }



        public void ListViewBindDataTable(ListView listview, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    item.SubItems.Add(dt.Rows[i][j].ToString());
                }
                listview.Items.Add(item);
            }
        }



        private void listValueShow()
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label10.Text = Math.Round(tsp.TotalMilliseconds, 0).ToString() + " 毫秒";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems != null)
            //    textBox3.Text = listView1.SelectedItems[0].Text;
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            textBox3.Text = e.Item.SubItems[0].Text;
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(listvarname.ToArray());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                TestMultiVariable(listvarname.ToArray());
                MessageBox.Show("测试OK！");
            }
            catch
            {
                MessageBox.Show("测试失败！");
            }
        }
    }


    [Serializable]
    public struct OmroPLCSetting
    {
        public ConnectionType ConnectionType;
        public long ReceiveTimeLimit;
        public bool UseRoutePath;
        public string PeerAddress;
        public int LocalPort;
        public List<string> listvar;
        public int circletime;
    }


    public class NjOmroPLC : OmroPLC
    {
        public NjOmroPLC(string name) :
            base(name)
        {
            try
            {
                PlcCompolet = new NJCompolet();
                Setting = (OmroPLCSetting)FastData.SaveStatic.ReadBinF(Name);
                PlcCompolet.ConnectionType = Setting.ConnectionType;
                PlcCompolet.ReceiveTimeLimit = Setting.ReceiveTimeLimit;
                PlcCompolet.UseRoutePath = Setting.UseRoutePath;
                PlcCompolet.PeerAddress = Setting.PeerAddress;
                PlcCompolet.LocalPort = Setting.LocalPort;
                listvarname = Setting.listvar;
                circletime = Setting.circletime;
            }
            catch (Exception exp)
            {
                Setting.ConnectionType = ConnectionType.UCMM;
                Setting.ReceiveTimeLimit = 200;
                Setting.UseRoutePath = false;
                Setting.PeerAddress = "0.0.0.0";
                Setting.LocalPort = 0;
                Setting.listvar = new List<string>();
                circletime = 100;
                MessageBox.Show(Name + ": PLC配置参数初始化失败！\r\n" + exp);
            }
        }

    }


    public class DoubleBufferListView : ListView
    {
        public DoubleBufferListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }

}
