﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastCtr;
using HalconTest;

namespace UIform
{
    public class Global
    {
        public static DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}..\..\..\", Application.StartupPath));

        public static DirectoryInfo di2 = new DirectoryInfo(string.Format(@"{0}..\", Global.di.FullName));

        public static string m_sConfigPath = Global.di2.FullName + "Config";

        public static string m_sLiaoHaoPath = m_sConfigPath + "\\LiaoHao";
        public static string ContempProductType = "111111";

        public static SettingForm settingForm = SettingForm.GetSingle();
        public static LogIn loginForm = new LogIn();
        public static ProductType productForm = new ProductType();
        public static 主界面 mainform = 主界面.GetSingle();
        public static DataForm dataForm = new DataForm();
        public static NewLogHelper loghelper = new NewLogHelper();

        
      


    }
}




