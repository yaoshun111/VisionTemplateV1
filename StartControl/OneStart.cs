using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StartControl
{
    public class OneStart
    {
        public static void Check(string flag)
        {
            bool ifexist = false;
            Mutex mutex = new Mutex(true, flag, out ifexist);
            if (!ifexist)
            {
                MessageBox.Show("程序运行中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mutex.Close();
                Thread.CurrentThread.Abort();
            }
        }
    }
}
