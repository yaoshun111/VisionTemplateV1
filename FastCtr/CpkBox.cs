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
using FastData;

namespace FastCtr
{
    public partial class CpkBox : UserControl
    {
        String _path;
        float _upper;
        float _lower;
        string _caption = "**CKP";
        public CpkBox()
        {
            InitializeComponent();
        }

        [Description("制程能力上限")]
        public float UpperLimit
        {
            get
            {
                return _upper;
            }
            set
            {
                _upper = value;
            }
        }

        [Description("制程能力下限")]
        public float LowerLimit
        {
            get
            {
                return _lower;
            }
            set
            {
                _lower = value;
            }
        }



        /// <summary>
        /// 数据源
        /// </summary>
        [Description("数据源路径")]
        public string PATH
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }


        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                label1.Text = _caption;
            }
        }

        public float Caculate()
        {
            CpkPro cpkpro = new CpkPro();
            string[] ret = SaveStatic.ReadTxt(_path);
            float[] retf = new float[ret.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                retf[i] = Convert.ToSingle(ret[i]);
            }
            return cpkpro.GetCPK(retf, _upper, _lower);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT文件(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
                _path = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Caculate().ToString();
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }
}
