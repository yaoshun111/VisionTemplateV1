using HalconDotNet;
using HalconTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalconTest
{
    public partial class CameraOperator : Form
    {
        HObject hObject;
        Camera camera = new Camera();
        HalconTemplateCreator templateCreator = new HalconTemplateCreator();
        HalconLineGuageCreator lineCreatorLeft = new HalconLineGuageCreator();
        HalconLineGuageCreator lineCreatorUp = new HalconLineGuageCreator();
        HalconLineGuageCreator lineCreatorDown = new HalconLineGuageCreator();
        HalconLineGuageCreator lineCreatorRightUp = new HalconLineGuageCreator();
        HalconLineGuageCreator lineCreatorRightDown = new HalconLineGuageCreator();
        HalconCircleGuageCreator circleCreator1 = new HalconCircleGuageCreator();
        string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
        string productType = "";
        static CameraOperator cameraOperator = new CameraOperator();
        private CameraOperator()
        {
            InitializeComponent();
        }

        public static Form GetThis(string m_producttype)
        {
            ProductType = m_producttype;
            cameraOperator.TopLevel = false;
            cameraOperator.FormBorderStyle = FormBorderStyle.None;
            cameraOperator.Dock = DockStyle.Fill;
            cameraOperator.Show();
            return cameraOperator;
        }

        private static string ProductType
        {
            get
            {
                return cameraOperator.productType;
            }
            set
            {
                cameraOperator.productType = value;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            templateCreator.Image = hObject;
            form.Controls.Add(templateCreator);
            form.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            lineCreatorLeft.Image = hObject;
            lineCreatorLeft.SaveLinePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".dxf";
            lineCreatorLeft.SaveGuagePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".bin";
            form.Controls.Add(lineCreatorLeft);
            form.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            lineCreatorUp.Image = hObject;
            lineCreatorUp.SaveLinePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".dxf";
            lineCreatorUp.SaveGuagePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".bin";
            form.Controls.Add(lineCreatorUp);
            form.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            lineCreatorDown.Image = hObject;
            lineCreatorDown.SaveLinePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".dxf";
            lineCreatorDown.SaveGuagePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".bin";
            form.Controls.Add(lineCreatorDown);
            form.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            lineCreatorRightUp.Image = hObject;
            lineCreatorRightUp.SaveLinePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".dxf";
            lineCreatorRightUp.SaveGuagePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".bin";
            form.Controls.Add(lineCreatorRightUp);
            form.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            lineCreatorRightDown.Image = hObject;
            lineCreatorRightDown.SaveLinePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".dxf";
            lineCreatorRightDown.SaveGuagePath = di + @"型号\" + productType + @"\LineGuage\" + ((Button)sender).Text + ".bin";
            form.Controls.Add(lineCreatorRightDown);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Controls.Add(camera);
            form.Show();
        }



        private void 加载一张图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string initdi = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
            //ofd.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.InitialDirectory = initdi + "SavePic";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                HOperatorSet.ReadImage(out hObject, strFileName);
                HDevOpMultiWindowImpl impl = new HDevOpMultiWindowImpl(hSmartWindowControl1.HalconWindow);
                impl.DevDisplay(hObject);

                groupBox1.Enabled = true;
            }
        }

        private void 清除窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HDevOpMultiWindowImpl impl = new HDevOpMultiWindowImpl(hSmartWindowControl1.HalconWindow);
            impl.DevClearWindow();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = productType;
        }
    }
}
