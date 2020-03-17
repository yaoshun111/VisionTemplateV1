using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DataAction;
using System.IO;

namespace MyControl
{
    public partial class UserChart : UserControl
    {
        CheckBox[] ck = new CheckBox[11];
        ColorDialog cd = new ColorDialog();
        TableLayoutPanel tp = new TableLayoutPanel();
        Button[] bt = new Button[11];
        Form setchart = new Form();
        FlowLayoutPanel fl = new FlowLayoutPanel();
        bool Flag = false;
   
        private string[] lineName = new string[11] { "", "", "", "", "", "", "", "", "", "", "" };
        public bool follow = true;
        float X = 0f;
        private Color c_color1 = Color.Black;
        private Color c_color2 = Color.Black;
        private Color c_color3 = Color.Black;
        private Color c_color4 = Color.Black;
        private Color c_color5 = Color.Black;
        private Color c_color6 = Color.Black;
        private Color c_color7 = Color.Black;
        private Color c_color8 = Color.Black;
        private Color c_color9 = Color.Black;
        private Color c_color10 = Color.Black;
        private Color c_color11 = Color.Black;
        private float upvalue = 2f;
        private float downvalue = 8f;
        //private float midvalue = 0f;
        private bool upshow = true;
        private bool downshow = true;
        private bool midshow = true;



        private List<float>[] listX = new List<float>[8];
        private List<Single>[] listY = new List<Single>[8];

        private string[] listdatabinPath = new string[8] { "", "", "", "", "", "", "", "" };

        private bool isSecondaryY = true;
        private double maxY = 10d;
        private double minY = 0d;
        private float limitCount = 1f;
        private float minX = 0f;
        private double intervalY = 0.1d;
        private int showCount = 20;
        private int chartCount = 40;
        private int labelYcount = 10;
        private int intervalX = 1;
        private bool[] visible = new bool[8] { true, true, true, true, true, true, true, true };

        [Description("设置线条名称")]
        [Category("数据线名称设置")]
        public string[] LineName
        {
            get
            {
                return lineName;
            }
            set
            {
                lineName = value;
            }
        }

        [Description("设置线条1颜色")]
        [Category("数据线颜色设置")]
        public Color C_color1 { set { c_color1 = value; } get { return c_color1; } }
        [Description("设置线条2颜色")]
        [Category("数据线颜色设置")]
        public Color C_color2 { set { c_color2 = value; } get { return c_color2; } }
        [Description("设置线条3颜色")]
        [Category("数据线颜色设置")]
        public Color C_color3 { set { c_color3 = value; } get { return c_color3; } }
        [Description("设置线条4颜色")]
        [Category("数据线颜色设置")]
        public Color C_color4 { set { c_color4 = value; } get { return c_color4; } }
        [Description("设置线条5颜色")]
        [Category("数据线颜色设置")]
        public Color C_color5 { set { c_color5 = value; } get { return c_color5; } }
        [Description("设置线条6颜色")]
        [Category("数据线颜色设置")]
        public Color C_color6 { set { c_color6 = value; } get { return c_color6; } }
        [Description("设置线条7颜色")]
        [Category("数据线颜色设置")]
        public Color C_color7 { set { c_color7 = value; } get { return c_color7; } }
        [Description("设置线条8颜色")]
        [Category("数据线颜色设置")]
        public Color C_color8 { set { c_color8 = value; } get { return c_color8; } }

        [Category("标准线颜色设置")]
        [Description("标准下线颜色")]
        public Color C_colorLow { set { c_color9 = value; chart1.Series["low"].Color = c_color9; } get { return c_color9; } }
        [Category("标准线颜色设置")]
        [Description("标准上线颜色")]
        public Color C_colorHigh { set { c_color10 = value; chart1.Series["high"].Color = c_color10; } get { return c_color10; } }
        [Category("标准线颜色设置")]
        [Description("标准中间线颜色")]
        public Color C_colorMid { set { c_color11 = value; chart1.Series["mid"].Color = c_color11; } get { return c_color11; } }

        [Category("数据点绑定")]
        public List<float>[] ListX { set { listX = value; } get { return listX; } }
        [Category("数据点绑定")]
        public List<float>[] ListY { set { listY = value; } get { return listY; } }

        [Category("数据点绑定")]
        [Description("将某条曲线直接绑定到指定数据格式的txt文件（逗号分隔符文件）")]
        public string[] ListDataBinTOTxt
        {
            get
            {
                return listdatabinPath;
            }
            set
            {
                listdatabinPath = value;
            }
        }


        [Category("显示设置")]
        [Description("图表所能容纳的X轴上点的最大数量")]
        public int ChartCount
        {
            set
            {
                chartCount = value;
                //if (chartCount < showCount)
                //{
                //    ShowCount = chartCount;
                //}
            }
            get
            {
                return chartCount;
            }
        }
        [Category("显示设置")]
        [Description("图表X被展示的最小数量")]
        public float LimitCount
        {
            get
            {
                return limitCount;
            }
            set
            {
                limitCount = value;
                chart1.ChartAreas[0].AxisX.Maximum = minX + limitCount;
                //fresh();
                ZoomReset();
            }
        }
        [Category("显示设置")]
        [Description("图表X轴的起始点")]
        public float StartX
        {
            get
            {
                return minX;
            }
            set
            {
                minX = value;
                chart1.ChartAreas[0].AxisX.Minimum = minX;
                chart1.ChartAreas[0].AxisX.Maximum = minX + limitCount;
                ZoomReset();
                ShowCount = showCount;
                //fresh();
            }
        }



        [Category("坐标设置")]
        public int LabelYcount
        {
            set
            {
                labelYcount = value;
            }
            get
            {
                IntervalY = Math.Round((maxY - minY) / labelYcount, 3);
                return labelYcount;
            }
        }

        [Category("显示设置")]
        public bool UpLineShow
        {
            set
            {
                upshow = value;
                if (!upshow)
                {
                    chart1.Series["high"].Points.DataBindXY(new List<float>(), new List<float>());
                }
                else
                {
                    chart1.Series["high"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { upvalue, upvalue });
                    chart1.Series["high"].Color = c_color10;
                }
            }
            get
            {
                return upshow;
            }
        }
        [Category("显示设置")]
        public bool DownLineShow
        {
            get
            {
                return downshow;
            }
            set
            {
                downshow = value;
                if (!downshow)
                {
                    chart1.Series["low"].Points.DataBindXY(new List<float>(), new List<float>());
                }
                else
                {
                    chart1.Series["low"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { downvalue, downvalue });
                    chart1.Series["low"].Color = c_color9;
                }
            }
        }
        [Category("显示设置")]
        public bool MidLineShow
        {
            get
            {
                return midshow;
            }
            set
            {
                midshow = value;
                if (!midshow)
                {
                    chart1.Series["mid"].Points.DataBindXY(new List<float>(), new List<float>());
                }
                else
                {
                    chart1.Series["mid"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { (downvalue + upvalue) / 2, (downvalue + upvalue) / 2 });
                    chart1.Series["mid"].Color = c_color11;
                }
            }
        }
        [Category("标准线值设置")]
        public float UpValue
        {
            get { return upvalue; }
            set
            {
                upvalue = value;
                chart1.Series["high"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { upvalue, upvalue });
                chart1.Series["mid"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { (downvalue + upvalue) / 2, (downvalue + upvalue) / 2 });
                chart1.Series["high"].Color = c_color10;
            }
        }
        [Category("标准线值设置")]
        public float DownValue
        {
            get { return downvalue; }
            set
            {
                downvalue = value;
                chart1.Series["low"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { downvalue, downvalue });
                chart1.Series["mid"].Points.DataBindXY(new List<float>() { 0f, 1000f }, new List<float>() { (downvalue + upvalue) / 2, (downvalue + upvalue) / 2 });
                chart1.Series["low"].Color = c_color9;
            }
        }


        [Category("显示设置")]
        public bool[] ItemVisible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        [Category("坐标设置")]
        public int IntervalX
        {
            set
            {
                intervalX = value;
                chart1.ChartAreas[0].AxisX.Interval = intervalX;
            }
            get
            {
                return intervalX;
            }
        }
        /// <summary>
        /// X轴最大显示的数量
        /// </summary>
        [Category("坐标设置")]
        [Description("图表中被展示的X轴数据点的个数")]
        public int ShowCount
        {
            set
            {
                showCount = value;
                chart1.ChartAreas[0].AxisX.ScaleView.Size = value;
            }
            get
            {
                return showCount;
            }
        }
        [Category("坐标设置")]
        public double MaxY
        {
            set
            {
                maxY = value;
                chart1.ChartAreas[0].AxisY.Maximum = maxY;
                chart1.ChartAreas[0].AxisY.Interval = Math.Round((maxY - minY) / labelYcount, 3);
            }
            get
            {
                return maxY;
            }
        }
        [Category("坐标设置")]
        public double MinY
        {
            set
            {
                minY = value;
                chart1.ChartAreas[0].AxisY.Minimum = minY;
                chart1.ChartAreas[0].AxisY.Interval = Math.Round((maxY - minY) / labelYcount, 3);
            }
            get
            {
                return minY;
            }
        }
        [Category("坐标设置")]
        public double IntervalY
        {
            set
            {
                intervalY = value;
                chart1.ChartAreas[0].AxisY.Interval = intervalY;
            }
            get
            {
                return intervalY;
            }
        }
        [Category("坐标设置")]
        public bool IsSecondaryY
        {
            set
            {
                isSecondaryY = value;
                if (isSecondaryY == true)
                {
                    chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                    chart1.Series[3].YAxisType = AxisType.Secondary;            //曲线4设置到第二轴(可以根据项目需要调节)
                }
                else
                {
                    chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    chart1.Series[3].YAxisType = AxisType.Primary;
                }
            }
            get { return isSecondaryY; }
        }
        /// <summary>
        /// //////////////////////////****************************************************
        /// </summary>
        public UserChart()
        {
            InitializeComponent();
            LimitCount = 1f;
            IntervalX = 2;
            IntervalY = 1;
            ChartCount = 500;
            ShowCount = 50;
            StartX = 0;
            C_colorLow = Color.Red;
            C_colorHigh = Color.Red;
            C_colorMid = Color.Lime;
            MaxY = 1;
            MinY = 0;
            UpValue = (float)(MinY + 0.8 * (maxY - minY));
            DownValue = (float)(MinY + 0.2 * (maxY - minY));
            LabelYcount = 10;
            DownLineShow = true;
            MidLineShow = true;
            UpLineShow = true;
            for (int i = 0; i < 8; i++)
            {
                lineName[i] = "line" + i.ToString();
                ListX[i] = new List<float>();
                ListY[i] = new List<float>();
            }
            lineName[8] = "下限线条";
            lineName[9] = "上限线条";
            lineName[10] = "中间线条";
            IsSecondaryY = false;


          // Save.SaveTxt(Application.StartupPath + "\\chartset.txt", "", false);
            //FileSystemWatcher fsw = new FileSystemWatcher(Path.GetDirectoryName(Application.StartupPath + "\\chartset.txt"), "*.txt");
            //fsw.EnableRaisingEvents = true;
            //fsw.NotifyFilter = NotifyFilters.LastWrite;
            //fsw.Changed += new FileSystemEventHandler(fsw_Changed);


        }

        public void ZoomReset()
        {
            chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        }


        public void InitChartset()
        {
            if (File.Exists(Application.StartupPath + "\\chartset.txt"))
            {
                FileSystemWatcher fsw = new FileSystemWatcher(Path.GetDirectoryName(Application.StartupPath + "\\chartset.txt"), "*.txt");
                fsw.EnableRaisingEvents = true;
                fsw.NotifyFilter = NotifyFilters.LastWrite;
                fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            }
            else
            {
                Save.SaveTxt(Application.StartupPath + "\\chartset.txt", "", false);
                FileSystemWatcher fsw = new FileSystemWatcher(Path.GetDirectoryName(Application.StartupPath + "\\chartset.txt"), "*.txt");
                fsw.EnableRaisingEvents = true;
                fsw.NotifyFilter = NotifyFilters.LastWrite;
                fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            }
            Flag = true;
        }

        public void fresh()
        {
            if(Flag)
            {
                Flag = false;
                flowLayoutPanel1.Controls.Clear();

                if (File.Exists(Application.StartupPath + "\\chartset.txt"))
                {
                    string[][] aa = Save.ReadMultiTxt(Application.StartupPath + "\\chartset.txt");
                    visible[0] = Convert.ToBoolean(aa[0][0]); visible[1] = Convert.ToBoolean(aa[1][0]); visible[2] = Convert.ToBoolean(aa[2][0]); visible[3] = Convert.ToBoolean(aa[3][0]);
                    visible[4] = Convert.ToBoolean(aa[4][0]); visible[5] = Convert.ToBoolean(aa[5][0]); visible[6] = Convert.ToBoolean(aa[6][0]); visible[7] = Convert.ToBoolean(aa[7][0]);
                    DownLineShow = Convert.ToBoolean(aa[8][0]); UpLineShow = Convert.ToBoolean(aa[9][0]); MidLineShow = Convert.ToBoolean(aa[10][0]);
                    c_color1 = Color.FromArgb(int.Parse(aa[0][1])); c_color2 = Color.FromArgb(int.Parse(aa[1][1])); c_color3 = Color.FromArgb(int.Parse(aa[2][1])); c_color4 = Color.FromArgb(int.Parse(aa[3][1]));
                    c_color5 = Color.FromArgb(int.Parse(aa[4][1])); c_color6 = Color.FromArgb(int.Parse(aa[5][1])); c_color7 = Color.FromArgb(int.Parse(aa[6][1])); c_color8 = Color.FromArgb(int.Parse(aa[7][1]));
                    C_colorLow = Color.FromArgb(int.Parse(aa[8][1])); C_colorHigh = Color.FromArgb(int.Parse(aa[9][1])); C_colorMid = Color.FromArgb(int.Parse(aa[10][1]));
                }
                else
                {
                    MessageBox.Show("文件chartset.txt丢失！");
                    return;
                }

                if (visible[0])
                {
                    chart1.Series["Series1"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";
                    lab1.BackColor = c_color1;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";
                    lab2.Text = lineName[0];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series1"].Enabled = false;
                }

                if (visible[1])
                {
                    chart1.Series["Series2"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";
                    lab1.BackColor = c_color2;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";
                    lab2.Text = lineName[1];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series2"].Enabled = false;
                }

                if (visible[2])
                {
                    chart1.Series["Series3"].Enabled = true;

                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color3;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[2];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series3"].Enabled = false;
                }
                if (visible[3])
                {
                    chart1.Series["Series4"].Enabled = true;

                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color4;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[3];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series4"].Enabled = false;
                }
                if (visible[4])
                {
                    chart1.Series["Series5"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color5;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[4];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series5"].Enabled = false;
                }
                if (visible[5])
                {
                    chart1.Series["Series6"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color6;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[5];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series6"].Enabled = false;
                }
                if (visible[6])
                {
                    chart1.Series["Series7"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color7;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[6];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series7"].Enabled = false;
                }

                if (visible[7])
                {
                    chart1.Series["Series8"].Enabled = true;
                    TableLayoutPanel tp = new TableLayoutPanel();
                    tp.AutoSize = true;
                    Label lab1 = new Label();
                    lab1.AutoSize = true;
                    lab1.Text = "    ";

                    lab1.BackColor = c_color8;
                    Label lab2 = new Label();
                    lab2.AutoSize = true;
                    lab2.Text = "    ";

                    lab2.Text = lineName[7];
                    tp.Controls.Add(lab1, 0, 0);
                    tp.Controls.Add(lab2, 1, 0);
                    flowLayoutPanel1.Controls.Add(tp);
                }
                else
                {
                    chart1.Series["Series8"].Enabled = false;
                }
            }

            if (listdatabinPath[0] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[0]) == null)
                {
                    return;
                }
                string[][] _d0 = Save.ReadMultiTxt(listdatabinPath[0]);
                string[][] d0 = Conver.TransPosition(_d0);
                listX[0] = Array.ConvertAll(d0[0].ToArray(), Convert.ToSingle).ToList();
                listY[0] = Array.ConvertAll(d0[1].ToArray(), Convert.ToSingle).ToList();
                chart1.Series["Series1"].Points.DataBindXY(listX[0], listY[0]);
                chart1.Series["Series1"].Color = c_color1;
            
            }

            if (listdatabinPath[1] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[1]) == null)
                {
                    return;
                }
                string[][] _d1 = Save.ReadMultiTxt(listdatabinPath[1]);
                string[][] d1 = Conver.TransPosition(_d1);
                listX[1] = Array.ConvertAll(d1[0].ToArray(), Convert.ToSingle).ToList();
                listY[1] = Array.ConvertAll(d1[1].ToArray(), Convert.ToSingle).ToList();
                chart1.Series["Series2"].Points.DataBindXY(listX[1], listY[1]);
                chart1.Series["Series2"].Color = c_color2;
               
            }

            if (listdatabinPath[2] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[2]) == null)
                {
                    return;
                }
               
                string[][] _d2 = Save.ReadMultiTxt(listdatabinPath[2]);
                string[][] d2 = Conver.TransPosition(_d2);
                listX[2] = Array.ConvertAll(d2[0].ToArray(), Convert.ToSingle).ToList();
                listY[2] = Array.ConvertAll(d2[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series3"].Points.DataBindXY(listX[2], listY[2]);
                chart1.Series["Series3"].Color = c_color3;
             
            }

            if (listdatabinPath[3] != "")
            {

                if (Save.ReadMultiTxt(listdatabinPath[3]) == null)
                {
                    return;
                }
                string[][] _d3 = Save.ReadMultiTxt(listdatabinPath[3]);
                string[][] d3 = Conver.TransPosition(_d3);
                listX[3] = Array.ConvertAll(d3[0].ToArray(), Convert.ToSingle).ToList();
                listY[3] = Array.ConvertAll(d3[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series4"].Points.DataBindXY(listX[3], listY[3]);
                chart1.Series["Series4"].Color = c_color4;
               
            }

            if (listdatabinPath[4] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[4]) == null)
                {
                    return;
                }
                string[][] _d4 = Save.ReadMultiTxt(listdatabinPath[4]);
                string[][] d4 = Conver.TransPosition(_d4);
                listX[4] = Array.ConvertAll(d4[0].ToArray(), Convert.ToSingle).ToList();
                listY[4] = Array.ConvertAll(d4[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series5"].Points.DataBindXY(listX[4], listY[4]);
                chart1.Series["Series5"].Color = c_color5;
               
            }

            if (listdatabinPath[5] != "")
            {

                if (Save.ReadMultiTxt(listdatabinPath[5]) == null)
                {
                    return;
                }
                string[][] _d5 = Save.ReadMultiTxt(listdatabinPath[5]);
                string[][] d5 = Conver.TransPosition(_d5);
                listX[5] = Array.ConvertAll(d5[0].ToArray(), Convert.ToSingle).ToList();
                listY[5] = Array.ConvertAll(d5[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series6"].Points.DataBindXY(listX[5], listY[5]);
                chart1.Series["Series6"].Color = c_color6;
              
            }

            if (listdatabinPath[6] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[6]) == null)
                {
                    return;
                }
                string[][] _d6 = Save.ReadMultiTxt(listdatabinPath[6]);
                string[][] d6 = Conver.TransPosition(_d6);
                listX[6] = Array.ConvertAll(d6[0].ToArray(), Convert.ToSingle).ToList();
                listY[6] = Array.ConvertAll(d6[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series7"].Points.DataBindXY(listX[6], listY[6]);
                chart1.Series["Series7"].Color = c_color7;
               
            }

            if (listdatabinPath[7] != "")
            {
                if (Save.ReadMultiTxt(listdatabinPath[7]) == null)
                {
                    return;
                }
                string[][] _d7 = Save.ReadMultiTxt(listdatabinPath[7]);
                string[][] d7 = Conver.TransPosition(_d7);
                listX[7] = Array.ConvertAll(d7[0].ToArray(), Convert.ToSingle).ToList();
                listY[7] = Array.ConvertAll(d7[1].ToArray(), Convert.ToSingle).ToList();

                chart1.Series["Series8"].Points.DataBindXY(listX[7], listY[7]);
                chart1.Series["Series8"].Color = c_color8;
            }


            float[] Xc = new float[8];
            for (int i = 0; i < 8; i++)
            {
                if (listX[i] != null)
                {
                    if (listX[i].Count > 0)
                    {
                        Xc[i] = listX[i][listX[i].Count - 1];
                    }
                }
            }
            X = Xc.Max();
            //X = listX[0].Count - 1;
            if (follow == true)
            {
                if (X > showCount)
                {
                    if (X > chartCount)
                    {
                        chart1.ChartAreas[0].AxisX.Minimum = X - chartCount;
                    }
                    chart1.ChartAreas[0].AxisX.Maximum = X;
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(X - showCount + 1, X);
                }
                else
                {
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.ChartAreas[0].AxisX.Maximum = showCount;
                    ZoomReset();
                    //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, totalCount-1);
                }
            }
            else
            {
                if (X <= showCount)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = showCount;
                    ZoomReset();
                    //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0, 50);
                }
                else
                {
                    chart1.ChartAreas[0].AxisX.Maximum = X;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btAutoClick();
        }
        public void btAutoClick()
        {
            if (follow)
            {
                follow = false;
                button2.Text = "×自动";
                button2.FlatAppearance.BorderColor = Color.White;
                button2.BackColor = Color.Gray;
            }
            else
            {
                follow = true;
                button2.Text = "√自动";
                button2.FlatAppearance.BorderColor = Color.Silver;
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            chart1.Focus();
            //this.label22.Location = new Point((int)chart1.ChartAreas[0].CursorX.Position, (int)chart1.ChartAreas[0].CursorY.Position);
            if (e.Button == MouseButtons.Left)
            {
                if (chart1.ChartAreas[0].CursorY.AxisType == AxisType.Primary)
                {
                    //if (e.Y + 46 > chart1.ChartAreas[0].AxisY.ScaleView.Position)
                    //{
                    label22.Visible = true;
                    this.label22.Location = new Point(e.X - 40, e.Y + 20);
                    label22.Text = "左游标\r\n" + chart1.ChartAreas[0].CursorX.Position.ToString() + "，" + chart1.ChartAreas[0].CursorY.Position.ToString();
                    //}
                }
                //if (chart1.ChartAreas[0].CursorY.AxisType == AxisType.Secondary)
                //{
                //    //if (e.Y + 46 > chart1.ChartAreas[0].AxisY2.ScaleView.Position)
                //    //{
                //    label22.Visible = true;
                //    this.label22.Location = new Point(e.X - 40, e.Y + 20);
                //    label22.Text = "右游标\r\n" + chart1.ChartAreas[0].CursorX.Position.ToString() + "，" + chart1.ChartAreas[0].CursorY.Position.ToString();
                //    //}
                //}
            }
        }
        private void chart1_MouseLeave(object sender, EventArgs e)
        {
            label22.Visible = false;
        }

        private void chart1_MouseEnter(object sender, EventArgs e)
        {
            label22.Visible = true;
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            float x0c, y0c, x1c, y1c;
            float scale;
            float xc = (float)chart1.ChartAreas[0].CursorX.Position;
            float yc = (float)chart1.ChartAreas[0].CursorY.Position;
            float x0 = (float)chart1.ChartAreas[0].AxisX.ScaleView.Position;
            float y0 = (float)chart1.ChartAreas[0].AxisY.ScaleView.Position;
            float x1 = (float)chart1.ChartAreas[0].AxisX.ScaleView.Size + x0;
            float y1 = (float)chart1.ChartAreas[0].AxisY.ScaleView.Size + y0;
            if (e.Delta < 0)
            {
                scale = 1.01f;
                x0c = xc - (xc - x0) * scale;
                y0c = yc - (yc - y0) * scale;
                x1c = xc + (x1 - xc) * scale;
                y1c = yc + (y1 - yc) * scale;
            }
            else
            {
                scale = 1.01f;
                x0c = xc - (xc - x0) / scale;
                y0c = yc - (yc - y0) / scale;
                x1c = xc + (x1 - xc) / scale;
                y1c = yc + (y1 - yc) / scale;
            }

            //if (x1c - x0c > 7)
            chart1.ChartAreas[0].AxisX.ScaleView.Position = x0c;
            chart1.ChartAreas[0].AxisX.ScaleView.Size = x1c - x0c;

            //if (y1c - y0c > 0.05)
            chart1.ChartAreas[0].AxisY.ScaleView.Position = y0c;
            chart1.ChartAreas[0].AxisY.ScaleView.Size = y1c - y0c;
        }

        private void UserChart_Load(object sender, EventArgs e)
        {
            //RegistChartsetTxtChange();
        }

        private void 切换第一轴游标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].CursorY.AxisType = AxisType.Primary;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Turquoise;
            chart1.ChartAreas[0].CursorX.LineColor = Color.Turquoise;
        }
        private void 切换第二轴游标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chart1.ChartAreas[0].CursorY.AxisType = AxisType.Secondary;
            //chart1.ChartAreas[0].CursorY.LineColor = Color.CornflowerBlue;
            //chart1.ChartAreas[0].CursorX.LineColor = Color.CornflowerBlue;
        }

        private void UserChart_Resize(object sender, EventArgs e)
        {
            chart1.Width = this.Width;
            chart1.Height = this.Height;
            button2.Location = new Point(this.Width - button2.Width, this.Height - button2.Height);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ck = new CheckBox[11];
            cd = new ColorDialog();
            bt = new Button[11];
            setchart = new Form();
            //setchart.Name = "uu";
            setchart.FormClosing += new FormClosingEventHandler(setchart_FormClosing);
            tp = new TableLayoutPanel();
            setchart.Text = "图表设置";
            tp.Dock = DockStyle.Fill;

            string[][] aa = Save.ReadMultiTxt(Application.StartupPath + "\\chartset.txt");

            for (int i = 0; i < 11; i++)
            {
                //tp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
                bt[i] = new Button();
                ck[i] = new CheckBox();
                ck[i].Checked = Convert.ToBoolean(aa[i][0]);
                ck[i].Text = lineName[i];
                bt[i].BackColor = Color.FromArgb(int.Parse(aa[i][1]));
                bt[i].Size = ck[i].Size;
                bt[i].Tag = i;
                bt[i].Click += new EventHandler(button_click);
                tp.Controls.Add(ck[i], 0, i);
                tp.Controls.Add(bt[i], 1, i);
            }
            setchart.Height = 500;
            setchart.Width = 400;
            setchart.Controls.Add(tp);
            //txtChangeFlag = true;
            setchart.TopMost = true;
            setchart.ShowDialog();
        }



        private void setchart_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("保存并更新信息？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 0);
            if (ret != DialogResult.OK)
            {
                return;
            }
            string[][] info = new string[11][];
            for (int i = 0; i < 11; i++)
            {
                info[i] = new string[2] { "", "" };
                info[i][0] = ck[i].Checked.ToString();
                info[i][1] = bt[i].BackColor.ToArgb().ToString();
            }
            Save.SaveTxtC(Application.StartupPath + "\\chartset.txt", info, false);
        }

        private void button_click(object sender, EventArgs e)
        {
            //MessageBox.Show("");
            int a = (int)(((Button)sender).Tag);
            if (cd.ShowDialog() == DialogResult.OK)
            {
                bt[a].BackColor = cd.Color;
            }
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Flag = true;
           
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

