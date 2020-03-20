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

namespace FastCtr
{
    public partial class CameraParamSetPage : UserControl
    {
        
       public enum CalipTrans
        {
            positive,
            negtive,
            all,
        }
        [Serializable]
        public struct CameraSetParam
        {
            public int CalipWidth;
            public int CalipHeight;
            public int Calipoffset;
            public int CalipNum;
            public int CalipIgnore;
            public CalipTrans calipTrans;
            public bool IsSave;
            public int DaysNum;
            public string path;

            public int Exposure;
            public int Gain;
        }
        public CameraSetParam cameraSetParam = new CameraSetParam();
        public CameraParamSetPage()
        {
            InitializeComponent();
            if (Application.StartupPath.Contains("Debug"))
                InitUI();
        }

        public void InitUI()
        {
            try
            {
                cameraSetParam = (CameraSetParam)FastData.SaveStatic.ReadBinF("cameraSetParam");
            }
            catch(Exception exp)
            {
                MessageBox.Show("参数读取失败！" + exp);
            }
            widthTxt.Text = cameraSetParam.CalipWidth.ToString();
            heightTxt.Text = cameraSetParam.CalipHeight.ToString();
            IgnoreTxt.Text = cameraSetParam.CalipIgnore.ToString();
            offsetTxt.Text = cameraSetParam.Calipoffset.ToString();
            calipNumTxt.Text = cameraSetParam.CalipNum.ToString();
            transCom.Text = cameraSetParam.calipTrans.ToString();
            widthTxt.Text = cameraSetParam.CalipWidth.ToString();

            exporeNum.Text = cameraSetParam.Exposure.ToString();
            gainnum.Text = cameraSetParam.Gain.ToString();

            ifsavecheckBox1.Checked = cameraSetParam.IsSave;
            savepathtextBox9.Text = cameraSetParam.path;
            daysnumtextBox10.Text = cameraSetParam.DaysNum.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cameraSetParam.CalipWidth = int.Parse(widthTxt.Text);
                cameraSetParam.CalipHeight = int.Parse(heightTxt.Text);
                cameraSetParam.CalipIgnore = int.Parse(IgnoreTxt.Text);
                cameraSetParam.Calipoffset = int.Parse(offsetTxt.Text);
                cameraSetParam.CalipNum = int.Parse(calipNumTxt.Text);
                cameraSetParam.calipTrans = (CalipTrans)Enum.Parse(typeof(CalipTrans), transCom.Text);

                cameraSetParam.Exposure = int.Parse(exporeNum.Value.ToString());
                cameraSetParam.Gain = int.Parse(gainnum.Value.ToString());

                cameraSetParam.IsSave = ifsavecheckBox1.Checked;
                cameraSetParam.path = savepathtextBox9.Text;
                cameraSetParam.DaysNum = int.Parse(daysnumtextBox10.Text);
                FastData.SaveStatic.SaveBinF("cameraSetParam", cameraSetParam);
            }
            catch(Exception exp)
            {
                MessageBox.Show( "请检查参数格式！"+  exp.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            string di = new DirectoryInfo(string.Format("{0}../../../../", Application.StartupPath)).FullName;
            string aa = di + "Param";
            if (!Directory.Exists(aa))
                Directory.CreateDirectory(aa);
            fDialog.ShowNewFolderButton = true;
            fDialog.SelectedPath = aa;
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = fDialog.SelectedPath; //获得文件路径
                savepathtextBox9.Text = localFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cameraSetParam.CalipWidth = int.Parse(widthTxt.Text);
                cameraSetParam.CalipHeight = int.Parse(heightTxt.Text);
                cameraSetParam.CalipIgnore = int.Parse(IgnoreTxt.Text);
                cameraSetParam.Calipoffset = int.Parse(offsetTxt.Text);
                cameraSetParam.CalipNum = int.Parse(calipNumTxt.Text);
                cameraSetParam.calipTrans = (CalipTrans)Enum.Parse(typeof(CalipTrans), transCom.Text);

                cameraSetParam.Exposure = int.Parse(exporeNum.Value.ToString());
                cameraSetParam.Gain = int.Parse(gainnum.Value.ToString());

                cameraSetParam.IsSave = ifsavecheckBox1.Checked;
                cameraSetParam.path = savepathtextBox9.Text;
                cameraSetParam.DaysNum = int.Parse(daysnumtextBox10.Text);
                FastData.SaveStatic.SaveBinF("cameraSetParam", cameraSetParam);
                MessageBox.Show("保存成功！");
            }
            catch (Exception exp)
            {
                MessageBox.Show("保存失败！" + exp);
            }
        }

        private void CameraParamSetPage_Load(object sender, EventArgs e)
        {

        }
    }
}
