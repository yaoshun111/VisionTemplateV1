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
        public bool Check()
        {
            bool ifexist = false;
            Mutex mutex = new Mutex(true, "mycode", out ifexist);
            if (!ifexist)
            {
                MessageBox.Show("程序运行中！", "提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
