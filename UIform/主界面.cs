
using System;
//using myEvent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIform
{
    public partial class 主界面 : Form
    {
      
        public static DataTable dt = new DataTable("chart_dt");
        public static bool 前1_mfg信号 = false;
        public static bool 前2_mfg信号 = false;
        public static bool 后1_mfg信号 = false;
        public static bool 后2_mfg信号 = false;
        StartControl.Welcom wel = new StartControl.Welcom();
        public 主界面()
        {
            
            InitializeComponent();

            Dock = DockStyle.Fill;
            TopLevel = false;
            dt.Columns.Add("序号");
            dt.Columns.Add("时间");
            dt.Columns.Add("生产型号");
            dt.Columns.Add("条码");
            dt.Columns.Add("前称");
            dt.Columns.Add("NO1");
            dt.Columns.Add("后称");
            dt.Columns.Add("NO2");
            dt.Columns.Add("注液量");
            dt.Columns.Add("OK/NG");
            dt.Columns.Add("针头号");
            dt.Columns.Add("封装压力");
            dt.Columns.Add("封装温度");
            dt.Columns.Add("电解液型号");
            dt.Columns.Add("弹夹编码");
            //dt.PrimaryKey = new DataColumn[1] { dt.Columns[3] };
            dataGridView1.DataSource = dt;

        }



        private void button1_Click(object sender, EventArgs e)
        {
          
        }


        private void 主界面_Load(object sender, EventArgs e)
        {
          
        }


        public void autoResize()
        {
            for (int i = 0; i < 13; i++)
                dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.DisplayedCells);
        }



        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void 清除所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void 加载最后一次数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // 主界面.dt.ReadXml("F:\\表格临时数据\\dt.xml");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 主界面_Shown(object sender, EventArgs e)
        {
            wel.Start();
            autoResize();
            this.dataGridView1.EnableHeadersVisualStyles = false;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Pink;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                dc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
                dc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            }
            wel.Stop();
        }

    }
}
