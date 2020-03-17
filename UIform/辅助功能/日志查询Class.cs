using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionFram3
{
    public class 日志查询Class
    {
        Task mytask;
        bool _inqury;
        private DateTime _historyAlarmStart;
        private DateTime _historyAlarmEnd;
        public event PropertyChangedEventHandler PropertyChanged;
     // private DataTable dt;
        private OleDbConnection Conn = null;
        string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\\操作日志\\logAccess.mdb";
        public 日志查询Class()
        {
            Init();
        }
        private void Init()
        {
            
        }
        private void InqureFromTo()
        {
            Conn = new OleDbConnection(connstr);
            Conn.Open();
            //DataSource = null;
            ////mythread.Start();
            ////Thread.Sleep(4000);
            ////historyAlarmStart = new DateTime(2018, 12, 1, 12, 1, 7, 23);
            //string strSql = "SELECT * FROM Alarm where 时间 between #" + historyAlarmStart.ToString("yyyy/MM/dd") + " 00:00:00" + "# and #" + _historyAlarmEnd.ToString("yyyy/MM/dd") + " 23:59:59" + "#";
            //OleDbCommand Cmd = new OleDbCommand(strSql, Conn);
            //OleDbDataAdapter adr = new OleDbDataAdapter();
            //adr.SelectCommand = Cmd;
            //dt = new DataTable();
            //dt.Clear();
            //adr.Fill(dt);
            //DataSource = dt;
            Inquery = false;
            Conn.Close();
        }

        /// <summary>
        /// //////属性控制接口//////////////////////
        /// </summary>
        public DateTime historyAlarmStart
        {
            get
            { return _historyAlarmStart; }
            set
            {
                _historyAlarmStart = value;
                OnPropertyChanged();
            }
        }
        public DateTime historyAlarmEnd
        {
            get
            { return _historyAlarmEnd; }
            set
            {
                _historyAlarmEnd = value;
                OnPropertyChanged();
            }
        }
        public bool Inquery
        {
            get
            {
                return _inqury;
            }
            set
            {
                _inqury = value;
                OnPropertyChanged();
                if (Inquery == true)
                {
                    //InqureFromTo();
                    mytask = new Task(InqureFromTo);
                    mytask.Start(); 
                }
            }
        }
     
//        public DataTable DataSource 
//        { 
//            get
//            {
//                return dt;
//            }
//            set
//            {
//                dt = value;
//                OnPropertyChanged();
//            }
//        }
///// <summary>
/// /////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public enum ExportDataType
        {
            EXCEL = 0,
            TXT = 1,
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
