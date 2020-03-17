using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseEvent
{
    public delegate void TaskEventHandler(object sender);
    public delegate void LogEventHandler(LogEventArgs e);


    public class LogEventArgs
    {
        public readonly string LogStr;
        public LogEventArgs(string str)
        {
            LogStr = DateTime.Now.ToString("【yyyy-MM-dd HH:mm:ss】") + " " + str;
        }
    }

    public class VarChangeEventArgs
    {
        public readonly int Ret;
        public VarChangeEventArgs(int ret)
        {
            Ret = ret;
        }
    }
}
