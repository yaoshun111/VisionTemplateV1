using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Access.Dao;
using System.IO;

namespace VisionFram3
{

    public partial class 存数据库 : Form
    {
        public event EventHandler DatebaseFinished;
        private OleDbConnection Conn = null;
        string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\操作日志\\logAccess.mdb";
        public bool IsFinished;
        private static object block = new object();
        
        public 存数据库()
        {
            InitializeComponent();
            DatebaseFinished = func_finished;
            Conn = new OleDbConnection(connstr);
            Conn.Open();
            string strSql = "DELETE 时间 from Alarm where 时间<#" + DateTime.Now.AddDays(-7) + "#";
            OleDbCommand Cmd = new OleDbCommand(strSql, Conn);
            Cmd.ExecuteNonQuery();
            Conn.Close();            
            DBEngineClass dbengine = new DBEngineClass();
            dbengine.CompactDatabase("F:\\操作日志\\logAccess.mdb", "F:\\操作日志\\myaccess2.mdb");
            File.Delete("F:\\操作日志\\logAccess.mdb");
            File.Move("F:\\操作日志\\myaccess2.mdb", "F:\\操作日志\\logAccess.mdb");
            File.Delete("F:\\操作日志\\myaccess2.mdb");
            Conn.Open();
        }
        private void SaveDataToAccess(string alarmNum)
        {
            try
            {
                //EventArgs arg = new PropertyChangedEventArgs
                string strSqlselect = "SELECT * FROM 代码信息 WHERE 代码='" + alarmNum + "'";
                OleDbCommand Cmdselect = new OleDbCommand(strSqlselect, Conn);
                OleDbDataReader reader = Cmdselect.ExecuteReader();
                reader.Read();

                if (DatebaseFinished != null)
                    DatebaseFinished(alarmNum + "\r\n" + "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + " " + reader.GetValue(1).ToString() + "\r\n" + reader.GetValue(2).ToString() + "\r\n" + reader.GetValue(3).ToString(), EventArgs.Empty);

                string strSql = "insert into Alarm (代码,时间,内容,类型,状态) values ('" + alarmNum + "',#" + DateTime.Now + "#,'" + "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + " " + reader.GetValue(1).ToString() + "','" + reader.GetValue(2).ToString() + "','" + reader.GetValue(3).ToString() + "')";
                OleDbCommand Cmd = new OleDbCommand(strSql, Conn);
                Cmd.ExecuteNonQuery();
                IsFinished = true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SaveInfoToAccess(string Info)
        {
            string TYPE;
            if (Info.Contains("ERR"))
            {
                TYPE = "报警";
            }
            else if (Info.Contains("LOG"))
            {
                TYPE = "日志";
            }
            else if (Info.Contains("WARNING"))
            {
                TYPE = "警告";
            }
            else
            {
                TYPE = "信号";
            }
            if (DatebaseFinished != null)
                DatebaseFinished("PC" + "\r\n" + Info + "\r\n" + TYPE + "\r\n" + "-", EventArgs.Empty);
            string strSql = "insert into Alarm (代码,时间,内容,类型,状态) values ('" + "PC" + "',#" + DateTime.Now + "#,'" + Info + "','" + TYPE + "','" + "-" + "')";
            OleDbCommand Cmd = new OleDbCommand(strSql, Conn);
            Cmd.ExecuteNonQuery();
            IsFinished = true;
         
           
        }

        private void menuButton1_SelectedChanged(object sender, EventArgs e)
        {
            Task TaskSaveDataToAccess = new Task(new Action(() => { SaveDataToAccess("A2222"); }));
            TaskSaveDataToAccess.Start();
        }

        public void TaskSaveDataToAccess(string alarmNum)
        {
            Task TaskSaveDataToAccess = new Task(new Action(() => 
            {
            //lock (block)
            //{
                SaveDataToAccess(alarmNum);
            //}
            }));
            TaskSaveDataToAccess.Start();
        }

        public void TaskSaveInfoToAccess(string Info)
        {
            Task TaskSaveInfoToAccess = new Task(new Action(() =>
            {
            //lock (block)
            //{
                string a = Thread.CurrentThread.Name;
                SaveInfoToAccess(Info);
            
            //}
            }));
            TaskSaveInfoToAccess.Start();
        }

        public virtual void func_finished(object sender, EventArgs e)
        {
            menuButton1.Selected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void 存数据库_Load(object sender, EventArgs e)
        {

        }
    }
}
