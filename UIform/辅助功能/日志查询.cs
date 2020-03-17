using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionFram3
{
    public partial class 日志查询 : Form
    {
        public 日志查询Class LogClass;
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime dateTimePicker1_Value { get { return dateTimePicker1.Value.Date; } set { this.Invoke((EventHandler)delegate { dateTimePicker1.Value = value; }); OnPropertyChanged(); } }
        public DateTime dateTimePicker2_Value { get { return dateTimePicker2.Value.Date; } set { this.Invoke((EventHandler)delegate { dateTimePicker2.Value = value; }); OnPropertyChanged(); } }
        public bool menuButton2_Selected { get { return menuButton2.Selected; } set { this.Invoke((EventHandler)delegate { menuButton2.Selected = value; }); OnPropertyChanged(); } }
        public object datagridview_datasource
        {
            get { return dataGridView1.DataSource; }set { dataGridView1.Invoke(new System.Action(delegate { dataGridView1.DataSource = value; })); }
            //绑定datagridview代码
        }
        public 日志查询()
        {

            //this.PropertyChanged = new PropertyChangedEventHandler(dtchangefunc);
            InitializeComponent();
            //this.dataGridView1.DataError += delegate(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e) { };
            LogClass = new 日志查询Class();
            MdataBing mdatabing = new MdataBing(this, LogClass);
            mdatabing.Bing("dateTimePicker1_Value", "historyAlarmStart", true);
            mdatabing.Bing("dateTimePicker2_Value", "historyAlarmEnd", true);
            mdatabing.Bing("menuButton2_Selected", "Inquery", true);
            mdatabing.Bing("datagridview_datasource", "DataSource", true);

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker1_Value = dateTimePicker1.Value;
            dateTimePicker2_Value = dateTimePicker2.Value;
        }
        private void menuButton2_SelectedChanged(object sender, EventArgs e)
        {
           //LogClass.historyAlarmStart = new DateTime(2018, 12, 19, 12, 1, 7, 23);
            if (!menuButton2.Selected)
            {
                return;
            }
            if (menuButton2.Selected)
            {
                if (dateTimePicker2_Value < dateTimePicker1_Value)
                {
                    MessageBox.Show("起始时间不能大于结束时间！", "警告");
                    menuButton2.Selected = false;
                    return;
                }
                menuButton2_Selected = menuButton2.Selected;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                dateTimePicker1_Value = DateTime.Now;
            }
            else
            {
                dateTimePicker1_Value = dateTimePicker1.Value;
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date > DateTime.Now.Date)
            {
                dateTimePicker2_Value = DateTime.Now;
            }
            else
            {
                dateTimePicker2_Value = dateTimePicker2.Value;
            }
            
        }
      
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

   
        private void menuButton4_SelectedChanged(object sender, EventArgs e)
        {
          
            
        }

        public void ToExcel(DataGridView dataGridView, string filepath)
        {

            //没有数据的话就不往下执行  
            if (dataGridView.Rows.Count == 0)
                return;
            //实例化一个Excel.Application对象  
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写  
            excel.Visible = true;

            //新增加一个工作簿，Workbook是直接保存，不会弹出保存对话框，加上Application会弹出保存对话框，值为false会报错  
            Workbook workbook = excel.Application.Workbooks.Add(true);
            Range allcolumn = workbook.ActiveSheet.Columns;

            //生成Excel中列头名称  
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                if (dataGridView.Columns[i].Visible == true)
                {
                    excel.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
                }
            }
            //把DataGridView当前页的数据保存在Excel中  
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                //System.Windows.Forms.Application.DoEvents();
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                        //if (dataGridView[j, i].ValueType == typeof(string))
                        //{
                        //    excel.Cells[i + 2, j + 1] = "'" + dataGridView[j, i].Value.ToString();
                        //}
                        //else
                        //{
                    excel.Cells[i + 2, j + 1] = dataGridView[j, i].Value.ToString();
                        //allcolumn.AutoFit();
                        //}
                }
                
            }
            allcolumn.AutoFit();
            //设置禁止弹出保存和覆盖的询问提示框  
            excel.DisplayAlerts = false;
            excel.AlertBeforeOverwriting = false;
            workbook.SaveAs(filepath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Close();
            //确保Excel进程关闭  
            excel.Quit();
            excel = null;
            GC.Collect();//如果不使用这条语句会导致excel进程无法正常退出，使用后正常退出
            MessageBox.Show(this, "文件已经成功导出！", "信息提示");
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (i != 3 )
                {
                    //this.Invoke((EventHandler)delegate
                    //{
                    //    dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                    //});
                    dataGridView1.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }
            }
        }

        private void 日志查询_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason==CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            this.Hide();
        }

      
    }
}


//=========================================================================================================================

