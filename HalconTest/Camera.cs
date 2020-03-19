using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace HalconTest
{
    public partial class Camera : UserControl
    {
        Task task = new Task(() => { });
        public HDevEngine engine;
        public HDevProgram program;
        public HDevProgramCall programCall;
        public HDevOpMultiWindowImpl impl;
        public string Path = new DirectoryInfo("../../../dll/HalconProgram/Camera.hdev").FullName;
        public Camera()
        {
            InitializeComponent();
            hSmartWindowControlcamera.MouseWheel += new MouseEventHandler(hSmartWindowControl1_MouseWheel);
        }
        public Camera(HDevOpMultiWindowImpl _impl)
        {
            InitializeComponent();
            impl = _impl;
            hSmartWindowControlcamera.MouseWheel += new MouseEventHandler(hSmartWindowControl1_MouseWheel);
        }

        private void hSmartWindowControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }

        private void hSmartWindowControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            hSmartWindowControlcamera.HSmartWindowControl_MouseWheel(sender, e);
        }

        private void Camera_Load(object sender, EventArgs e)
        {

        }

        public void Excute()
        {
            if (task.Status == TaskStatus.Created || task.IsCompleted || task.IsFaulted || task.IsCanceled)
            {
                task = new Task(() =>
                {
                    engine = new HDevEngine();
                    program = new HDevProgram(Path);
                    programCall = new HDevProgramCall(program);
                    if (impl == null)
                        impl = new HDevOpMultiWindowImpl(hSmartWindowControlcamera.HalconWindow);
                    engine.SetHDevOperators(impl);
                    programCall.Execute();
                }, TaskCreationOptions.LongRunning);
                task.Start();
            }
        }

        public bool StopExcute()
        {
            if (engine!=null)
            {
                try
                {
                    engine.SetGlobalCtrlVarTuple("TaskWhile", 0);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Connect()
        {
            if (engine != null)
            {
                try
                {
                    engine.SetGlobalCtrlVarTuple("Id", 1);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            

        }

        public bool DisConnect()
        {
            if (engine != null)
            {
                try
                {
                    engine.SetGlobalCtrlVarTuple("Id", 4);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool ShotOne()
        {
            if (engine != null)
            {
                try
                {
                    engine.SetGlobalCtrlVarTuple("Id", 2);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void ShotContinue()
        {
            engine.SetGlobalCtrlVarTuple("Id", 3);
        }

        public void StopShot()
        {
            engine.SetGlobalCtrlVarTuple("Id", 0);
        }

        public void SavePic()
        {
            HObject retpic = engine.GetGlobalIconicVarObject("Image");
            SaveFileDialog sfd = new SaveFileDialog();
            string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
            string aa = di + "SavePic";
            if (!Directory.Exists(aa))
                Directory.CreateDirectory(aa);
            sfd.InitialDirectory = aa; //设置初始路径
            sfd.Filter = "BMP文件(*.bmp)|*.bmp|JPG文件(*.jpg)|*.jpg|TTIF文件(*.tif)|*.tif|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            sfd.FilterIndex = 0; //设置默认显示文件类型
            sfd.Title = "保存图片"; //获取或设置文件对话框标题
            sfd.RestoreDirectory = true;//设置对话框是否记忆上次打开的目录
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;
            //sfd.FileName = "";
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径
                HOperatorSet.WriteImage(retpic, "bmp", 0, localFilePath);
            }
        }

        private void Camera_ParentChanged(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                Parent.Text = "相机";
                button4.Text = "打开相机";
                button2.Text = "连续采集";
                label1.BackColor = Color.Red;
                ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                ParentForm.SizeChanged += new EventHandler(ParentForm_SizeChanged);
                ParentForm.Width = 1000;
                ParentForm.Height = 800;
                Dock = DockStyle.Fill;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentForm.Controls.Clear();
            StopExcute();
            
        }


        private void ParentForm_SizeChanged(object sender, EventArgs e)
        {
            //splitContainer1.SplitterDistance = 1000;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Excute();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "打开相机")
            {
                Connect();
                button4.Text = "关闭相机";
            }
            else
            {
                DisConnect();
                button4.Text = "打开相机";
            }
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
            ShotOne();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "连续采集")
            {
                button2.Text = "停止采集";
                ShotContinue();
            }
            else
            {
                button2.Text = "连续采集";
                StopShot();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (engine != null)
            {
                textBox1.Text = engine.GetGlobalCtrlVarTuple("ExceptionMessage").ToString();
                int[] Result = engine.GetGlobalCtrlVarTuple("IsOK").ToIArr();
                if (Result.Length > 0)
                {
                    switch (Result[1])
                    {
                        case 0:
                            label1.BackColor = Color.Red;
                            button2.Text = "连续采集";
                            button1.Enabled = false;
                            button2.Enabled = false;
                            break;
                        case 1:
                            label1.BackColor = Color.LimeGreen;
                            if (engine.GetGlobalCtrlVarTuple("Id") != 3 && task.Status == TaskStatus.Running)
                            {
                                button1.Enabled = true;
                            }
                            button2.Enabled = true;
                            break;
                    }
                }
                else
                {
                    label1.BackColor = Color.Red;
                }
                if (engine.GetGlobalCtrlVarTuple("Id") == 3 && task.Status == TaskStatus.Running)
                {
                    button1.Enabled = false;
                }

            }
            if (task.Status == TaskStatus.Running)
            {
                label2.BackColor = Color.LimeGreen;
            }
            else
            {
                label2.BackColor = Color.Red;
            }


            if(task.Status==TaskStatus.Running)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
            }

        }

        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePic();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void 清除窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOperatorSet.ClearWindow(hSmartWindowControlcamera.HalconWindow);
        }

        private void 切换到本窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            impl=new HDevOpMultiWindowImpl(hSmartWindowControlcamera.HalconWindow);
            engine.SetHDevOperators(impl);
        }
    }
}
