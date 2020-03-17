using HslCommunication.LogNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCtr
{
    public class Log_Helper
    {

        private ILogNet logNet;
        public object lock_log = new object();//上锁

        public Log_Helper(string path)
        {
            logNet = new LogNetFileSize(path, 2 * 1024 * 1024);
        }

        public void logWriteInfo(string data)
        {
            logNet.WriteInfo(data);
        }

        /// <summary>
        ///  界面显示消息，msgType为类别，Info，Alarm
        /// </summary>
        /// <param name="data">消息内容</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="isShown">是否在主界面显示？</param>
        public void DispProcess(string data, string msgType, bool isShown)
        {
            lock (lock_log)
            {
                string tempData = data;
                try
                {
                    if (eventDispProcess != null)
                    {
                        if (msgType == "Info")
                        {
                            tempData = "[消息]" + DateTime.Now.ToString() + "  " + data + "\n";
                            if (isShown) { eventDispProcess(tempData, Color.Black); }
                        }
                        else if (msgType == "Alarm")
                        {
                            tempData = "[错误]" + DateTime.Now.ToString() + "  " + data + "\n";
                            if (isShown) { eventDispProcess(tempData, Color.Red); }
                        }
                        tempData = DateTime.Now.ToString() + "  " + data;
                        logNet.WriteInfo(data);
                    }
                }
                catch (Exception ex)
                {
                    logNet.WriteWarn(ex.Message + "DispProcess函数处");
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        public delegate void delegateDispProcess(string msg, Color foreColor);
        public event delegateDispProcess eventDispProcess;//消息显示事件
    }
}
