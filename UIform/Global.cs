using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastCtr;

namespace UIform
{
    public  class Global
    {
        public static DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\..\", Application.StartupPath));

        public static SettingForm setting = SettingForm.GetSingle();
        public static LogIn login = new LogIn();
        public static ProductType product = new ProductType();
        public static 主界面 mainform =主界面.GetSingle();
       
        public static DataForm dataForm = new DataForm();
        public static NewLogHelper loghelper = new NewLogHelper();

      
    }
}
       

    

