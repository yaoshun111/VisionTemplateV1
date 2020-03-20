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
using System.Threading;

namespace HalconTest
{
    public partial class HalconCircleGuageCreator : UserControl
    {
        public HDevEngine engine;
        public HDevProgram program;
        public HDevProgramCall programCall;
        public HDevOpMultiWindowImpl impl;
        public string mPath = new DirectoryInfo("../../../dll/HalconProgram/TemplateCreator.hdev").FullName;
        public HObject Image;
        public HalconCircleGuageCreator()
        {
            InitializeComponent();
            hWindowControltemp.HMouseWheel += new HalconDotNet.HMouseEventHandler(hWindowControl1_HMouseWheel);

        }

        public HalconCircleGuageCreator(HDevOpMultiWindowImpl _impl)
        {
            InitializeComponent();
            impl = _impl;
            hWindowControltemp.HMouseWheel += new HalconDotNet.HMouseEventHandler(hWindowControl1_HMouseWheel);

        }

        public void SetTemplateOriginPath(string path)
        {
            engine.SetGlobalCtrlVarTuple("TemplateOriginPath", path);
        }

        public void SetTemplateSavePath(string path)
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("TemplateSavePath", path);
        }

        public void SetLineSavePath(string path)
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("LineSavePath", path);
        }

        public void SetCircleSavePath(string path)
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("CircleSavePath", path);
        }



        public void GetContempPic()
        {
            Image = engine.GetGlobalIconicVarObject("ContempPic");
        }

        public void StopTask()
        {
            if(engine!=null)
            engine.SetGlobalCtrlVarTuple("StopTask", 0);
        }

        public int GetTaskStatus()
        {
            if (engine != null)
                return engine.GetGlobalCtrlVarTuple("StopTask");
            else
                return 0;

        }


        public void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            GetContempPic();
            zoom.Zoom.mousewheel(hWindowControltemp.HalconWindow, Image, e);
        }

        public void Excute()
        {
            if (programCall.IsInitialized())
            {
                programCall.Execute();
            }
        }

        public void StartDrawTemplate()
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("StartDrawTemplate", 1);
        }


        public void StartDrawLine()
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("StartDrawLine", 1);
        }

        public void StartDrawCircle()
        {
            if (engine != null)
                engine.SetGlobalCtrlVarTuple("StartDrawCircle", 1);
        }

     
     

        public void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (GetTaskStatus() == 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                string initdi = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
                //ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
                ofd.CheckPathExists = true;
                ofd.CheckFileExists = true;
                ofd.InitialDirectory = initdi + "SavePic";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    engine = new HDevEngine();
                    program = new HDevProgram(mPath);
                    programCall = new HDevProgramCall(program);
                    if (impl == null)
                        impl = new HDevOpMultiWindowImpl(hWindowControltemp.HalconWindow);
                    engine.SetHDevOperators(impl);
                    //string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
                    //string aa = di + "Template";
                    //if (!Directory.Exists(aa))
                    //    Directory.CreateDirectory(aa);

                    string strFileName = ofd.FileName;
                    SetTemplateOriginPath(strFileName);
                    Excute();
                }
            }
            else//如已经在运行，将任务先停掉，然后再调用
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
                ofd.CheckPathExists = true;
                ofd.CheckFileExists = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    StopTask();
                    Thread.Sleep(500);

                    engine = new HDevEngine();
                    program = new HDevProgram(mPath);
                    programCall = new HDevProgramCall(program);
                    if (impl == null)
                        impl = new HDevOpMultiWindowImpl(hWindowControltemp.HalconWindow);

                    engine.SetHDevOperators(impl);
                    //string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
                    //string aa = di + "Template";
                    //if (!Directory.Exists(aa))
                    //    Directory.CreateDirectory(aa);
                    //textBox1.Text = aa + "\\template.tif";

                    string strFileName = ofd.FileName;
                    SetTemplateOriginPath(strFileName);
                    Excute();
                }
            }
            button1.Enabled = true;
        }

  





        public void ParentForm_FormClosing(object sender, EventArgs e)
        {
            StopTask();
            ParentForm.Controls.Clear();
        }

        private void HalconTemplateCreator_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                ParentForm.Text = "GenTemplate";
                ParentForm.AutoSize = true;
                ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
        }

        private void HalconTemplateCreator_Load(object sender, EventArgs e)
        {
           
        }

    

      

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                string path = Path.GetDirectoryName(textBox3.Text);
                if (Directory.Exists(path))
                {
                    StartDrawCircle();
                }
                else
                {
                    MessageBox.Show("路径不存在");
                }
            }
            else
            {
                MessageBox.Show("请设置路径！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
            string aa = di + "型号";
            if (!Directory.Exists(aa))
                Directory.CreateDirectory(aa);
            sfd.InitialDirectory = aa; //设置初始路径
            sfd.Filter = "DXF文件(*.dxf)|*.dxf|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            sfd.FilterIndex = 0; //设置默认显示文件类型
            sfd.Title = "保存圆形"; //获取或设置文件对话框标题
            sfd.AddExtension = true;
            //sfd.FileName = "circle";
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径
                textBox3.Text = localFilePath;
            }
        }

     

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            SetCircleSavePath(textBox3.Text);
        }
    }
}
