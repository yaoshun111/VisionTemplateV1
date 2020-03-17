using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastData
{
    public static class SaveStatic
    {

        private static object lock_txt = new object();
        /// <summary>
        /// ini文件写入
        /// </summary>
        /// <param name="section">节点代号</param>
        /// <param name="key">关键字</param>
        /// <param name="val">值</param>
        /// <param name="filePath">ini文件路径</param>
        /// <returns>long</returns>
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        /// <summary>
        /// ini文件读取
        /// </summary>
        /// <param name="section">节点代号</param>
        /// <param name="key">关键字</param>
        /// <param name="def">""</param>
        /// <param name="retVal">temp</param>
        /// <param name="size">500</param>
        /// <param name="filePath">ini文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        ///  <param name="filePath">ini文件路径</param>
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public static void SaveIni(string filePath, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, filePath);
        }

        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public static string ReadIni(string filePath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, filePath);
            return temp.ToString();
        }


        public static void SaveTxt(string _path, string _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllText(_path, _contents + "\r\n", Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, _contents + "\r\n", Encoding.UTF8);
                }
            }
        }

        public static void SaveTxtT(string _path, string _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllText(_path, _contents + "\r\n", Encoding.Default);
                }
                else
                {
                    File.AppendAllText(_path, _contents + "\r\n", Encoding.Default);
                }
            }
        }

        /// <summary>
        /// 保存浮点数数组到指定文件位置
        /// </summary>
        /// <param name="_path">指定文件路径</param>
        /// <param name="_contents">浮点数数组</param>
        public static void SaveTxt(string _path, IEnumerable<float> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string[] uu = Array.ConvertAll<float, string>(_contents.ToArray(), Convert.ToString);
                if (!_append)
                {
                    File.WriteAllLines(_path, uu, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, uu, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存整形数组到指定文件路径
        /// </summary>
        /// <param name="_path">指定文件路径</param>
        /// <param name="_contents">整数型数组</param>
        public static void SaveTxt(string _path, IEnumerable<int> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string[] uu = Array.ConvertAll<int, string>(_contents.ToArray(), Convert.ToString);
                if (!_append)
                {
                    File.WriteAllLines(_path, uu, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, uu, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存字符串类型到指令文件位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents"></param>
        public static void SaveTxt(string _path, IEnumerable<string> _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllLines(_path, _contents, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, _contents, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存字符串数组类型到指定位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">字符串类型的数组，string[][]</param>
        public static void SaveTxt(string _path, IEnumerable<string[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (string[] a in _contents)
                {
                    str = str + string.Join("\t", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }
       

        /// <summary>
        /// 保存浮点数数组类型到指定位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">浮点数类型的数组，float[][]</param>
        public static void SaveTxt(string _path, IEnumerable<float[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (float[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存整数型数组类型到指定文件位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">整型数组类型额数组，int[][]</param>
        public static void SaveTxt(string _path, IEnumerable<int[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (int[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        public static void SaveTxt<T>(string _path, IEnumerable<T[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (T[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        public static void SaveTxt<T>(string _path, IEnumerable<T> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                str = string.Join(",", _contents) + "\r\n";
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// 保存datatable类型到逗号分隔符文件
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_dt"></param>
        /// <param name="_titleInclude">是否显示行标题</param>
        public static void SaveTxt(string _path, DataTable _dt, bool _titleInclude, bool _append)
        {
            lock (lock_txt)
            {
                if (!_titleInclude)
                {
                    int count = _dt.Rows.Count;
                    string[][] strArry = new string[count][];
                    for (int i = 0; i < count; i++)
                    {
                        strArry[i] = Array.ConvertAll(_dt.Rows[i].ItemArray, Convert.ToString);
                    }
                    SaveTxt(_path, strArry, _append);
                }
                else
                {
                    int count = _dt.Rows.Count + 1;
                    string[][] strArry = new string[count][];
                    strArry[0] = new string[_dt.Columns.Count];
                    for (int j = 0; j < _dt.Columns.Count; j++)
                    {
                        strArry[0][j] = _dt.Columns[j].ColumnName;
                    }
                    for (int i = 1; i < count; i++)
                    {
                        strArry[i] = Array.ConvertAll(_dt.Rows[i - 1].ItemArray, Convert.ToString);
                    }
                    SaveTxt(_path, strArry, _append);
                }
            }
        }

        /// <summary>
        /// 清除文本
        /// </summary>
        /// <param name="_path"></param>
        public static void ClearTxt(string _path)
        {
            lock (lock_txt)
            {
                File.WriteAllText(_path, "", Encoding.UTF8);
            }
        }

        /// <summary>
        /// 创建文本
        /// </summary>
        /// <param name="path"></param>
        public static void FileExist(string path)
        {
            lock (lock_txt)
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
            }
        }





        /// <summary>
        /// 读取单列TXT文件，返回string类型的数组
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static string[] ReadTxt(string _path)
        {
            lock (lock_txt)
            {
                return File.ReadAllLines(_path, Encoding.UTF8).ToArray();
            }
        }


        public static string ReadAllTxt(string _path)
        {
            lock (lock_txt)
            {
                return File.ReadAllText(_path, Encoding.UTF8);
            }
        }
        /// <summary>
        /// 读取带逗号分隔符的TXT文件，返回string[]类型的数组
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static string[][] ReadMultiTxt(string _path)
        {
            lock (lock_txt)
            {
                string[] strRow = File.ReadLines(_path, Encoding.Default).ToArray();
                if (strRow.Count() == 0)
                {
                    return null;
                }
                string[][] retstr = new string[strRow.Count()][];
                for (int i = 0; i < strRow.Count(); i++)
                {
                    retstr[i] = strRow[i].Split(',');
                }
                return retstr;
            }
        }

        public static string[][] ReadMultiTxtT(string _path)
        {
            lock (lock_txt)
            {
                string[] strRow = File.ReadLines(_path, Encoding.Default).ToArray();
                if (strRow.Count() == 0)
                {
                    return null;
                }
                string[][] retstr = new string[strRow.Count()][];
                for (int i = 0; i < strRow.Count(); i++)
                {
                    retstr[i] = strRow[i].Split('\t');
                }
                return retstr;
            }
        }

        public static void SaveBinF(string _name, object _obj)
        {
            string path = new DirectoryInfo("../../../ConfigurationData/BinFile/").FullName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path + _name + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _obj);
            stream.Close();
            stream.Dispose();
        }
        public static object ReadBinF(string _name)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string path = new DirectoryInfo("../../../ConfigurationData/BinFile/").FullName;
                path = path + _name + ".bin";
                Stream streamc = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                object ret = formatter.Deserialize(streamc);
                streamc.Close();
                streamc.Dispose();
                return ret;
            }
            catch (Exception exp)
            {

                throw new Exception("读取二进制文件" + _name + "失败！" + "错误信息：" + exp.Message);

            }
        }


        public static void SaveBin(string _name, object _obj)
        {
            if (false == System.IO.Directory.Exists("BinData"))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory("BinData");
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("BinData\\" + _name + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _obj);
            stream.Close();
            stream.Dispose();
        }

        public static object ReadBin(string _name)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream streamc = new FileStream("BinData\\" + _name + ".bin", FileMode.Open, FileAccess.Read, FileShare.None);
                object ret = formatter.Deserialize(streamc);
                streamc.Close();
                streamc.Dispose();
                return ret;
            }
            catch (Exception exp)
            {
                // return null;
                throw new Exception("读取二进制文件" + _name + "失败！" + "错误信息：" + exp.Message);
            }
        }

        public static Dictionary<string, string> ReadTxtToDictionary(string _name)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[][] ret = ReadMultiTxt(_name);
            for (int i = 0; i < ret.Count(); i++)
            {
                if (!dic.Keys.Contains(ret[i][0]))
                    dic.Add(ret[i][0], ret[i][1]);
            }
            return dic;
        }



        public static void ListViewBindDataTable(ListView listview, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    item.SubItems.Add(dt.Rows[i][j].ToString());
                }
                listview.Items.Add(item);
            }
        }


    }

    public class Save
    {

        private static object lock_txt = new object();


        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        ///  <param name="filePath">ini文件路径</param>
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void SaveIni(string filePath, string Section, string Key, string Value)
        {
            SaveStatic.WritePrivateProfileString(Section, Key, Value, filePath);
        }

        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string ReadIni(string filePath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = SaveStatic.GetPrivateProfileString(Section, Key, "", temp, 500, filePath);
            return temp.ToString();
        }


        public void SaveTxt(string _path, string _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllText(_path, _contents + "\r\n", Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, _contents + "\r\n", Encoding.UTF8);
                }
            }
        }

        public void SaveTxtT(string _path, string _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllText(_path, _contents + "\r\n", Encoding.Default);
                }
                else
                {
                    File.AppendAllText(_path, _contents + "\r\n", Encoding.Default);
                }
            }
        }

        /// <summary>
        /// 保存浮点数数组到指定文件位置
        /// </summary>
        /// <param name="_path">指定文件路径</param>
        /// <param name="_contents">浮点数数组</param>
        public void SaveTxt(string _path, IEnumerable<float> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string[] uu = Array.ConvertAll<float, string>(_contents.ToArray(), Convert.ToString);
                if (!_append)
                {
                    File.WriteAllLines(_path, uu, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, uu, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存整形数组到指定文件路径
        /// </summary>
        /// <param name="_path">指定文件路径</param>
        /// <param name="_contents">整数型数组</param>
        public void SaveTxt(string _path, IEnumerable<int> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string[] uu = Array.ConvertAll<int, string>(_contents.ToArray(), Convert.ToString);
                if (!_append)
                {
                    File.WriteAllLines(_path, uu, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, uu, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存字符串类型到指令文件位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents"></param>
        public void SaveTxt(string _path, IEnumerable<string> _contents, bool _append)
        {
            lock (lock_txt)
            {
                if (!_append)
                {
                    File.WriteAllLines(_path, _contents, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllLines(_path, _contents, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存字符串数组类型到指定位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">字符串类型的数组，string[][]</param>
        public void SaveTxt(string _path, IEnumerable<string[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (string[] a in _contents)
                {
                    str = str + string.Join("\t", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }


        public void SaveTxtC(string _path, IEnumerable<string[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (string[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// 保存浮点数数组类型到指定位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">浮点数类型的数组，float[][]</param>
        public void SaveTxt(string _path, IEnumerable<float[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (float[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }
        /// <summary>
        /// 保存整数型数组类型到指定文件位置
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_contents">整型数组类型额数组，int[][]</param>
        public void SaveTxt(string _path, IEnumerable<int[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (int[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        public void SaveTxt<T>(string _path, IEnumerable<T[]> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                foreach (T[] a in _contents)
                {
                    str = str + string.Join(",", a) + "\r\n";
                }
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        public void SaveTxt<T>(string _path, IEnumerable<T> _contents, bool _append)
        {
            lock (lock_txt)
            {
                string str = "";
                str = string.Join(",", _contents) + "\r\n";
                //Array.ConvertAll<string[], string>(_contents, Convert.ToString);
                //string.Join(",", _contents);
                if (!_append)
                {
                    File.WriteAllText(_path, str, Encoding.UTF8);
                }
                else
                {
                    File.AppendAllText(_path, str, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// 保存datatable类型到逗号分隔符文件
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_dt"></param>
        /// <param name="_titleInclude">是否显示行标题</param>
        public void SaveTxt(string _path, DataTable _dt, bool _titleInclude, bool _append)
        {
            lock (lock_txt)
            {
                if (!_titleInclude)
                {
                    int count = _dt.Rows.Count;
                    string[][] strArry = new string[count][];
                    for (int i = 0; i < count; i++)
                    {
                        strArry[i] = Array.ConvertAll(_dt.Rows[i].ItemArray, Convert.ToString);
                    }
                    SaveTxt(_path, strArry, _append);
                }
                else
                {
                    int count = _dt.Rows.Count + 1;
                    string[][] strArry = new string[count][];
                    strArry[0] = new string[_dt.Columns.Count];
                    for (int j = 0; j < _dt.Columns.Count; j++)
                    {
                        strArry[0][j] = _dt.Columns[j].ColumnName;
                    }
                    for (int i = 1; i < count; i++)
                    {
                        strArry[i] = Array.ConvertAll(_dt.Rows[i - 1].ItemArray, Convert.ToString);
                    }
                    SaveTxt(_path, strArry, _append);
                }
            }
        }

        public void ClearTxt(string _path)
        {
            lock (lock_txt)
            {
                File.WriteAllText(_path, "", Encoding.UTF8);
            }
        }







        /// <summary>
        /// 读取单列TXT文件，返回string类型的数组
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public string[] ReadTxt(string _path)
        {
            lock (lock_txt)
            {
                return File.ReadAllLines(_path, Encoding.UTF8).ToArray();
            }
        }


        public string ReadAllTxt(string _path)
        {
            lock (lock_txt)
            {
                return File.ReadAllText(_path, Encoding.UTF8);
            }
        }
        /// <summary>
        /// 读取带逗号分隔符的TXT文件，返回string[]类型的数组
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public string[][] ReadMultiTxt(string _path)
        {
            lock (lock_txt)
            {
                string[] strRow = File.ReadLines(_path, Encoding.UTF8).ToArray();
                if (strRow.Count() == 0)
                {
                    return null;
                }
                string[][] retstr = new string[strRow.Count()][];
                for (int i = 0; i < strRow.Count(); i++)
                {
                    retstr[i] = strRow[i].Split(',');
                }
                return retstr;
            }
        }

        public string[][] ReadMultiTxtT(string _path)
        {
            lock (lock_txt)
            {
                string[] strRow = File.ReadLines(_path, Encoding.Default).ToArray();
                if (strRow.Count() == 0)
                {
                    return null;
                }
                string[][] retstr = new string[strRow.Count()][];
                for (int i = 0; i < strRow.Count(); i++)
                {
                    retstr[i] = strRow[i].Split('\t');
                }
                return retstr;
            }
        }

        


        public void SaveBinF(string _name, object _obj)
        {
            if (false == System.IO.Directory.Exists("BinData"))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory("BinData");
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("BinData\\" + _name + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _obj);
            stream.Close();
            stream.Dispose();
        }
        public object ReadBinF(string _name)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream streamc = new FileStream("BinData\\" + _name + ".bin", FileMode.Open, FileAccess.Read, FileShare.None);
                object ret = formatter.Deserialize(streamc);
                streamc.Close();
                streamc.Dispose();
                return ret;
            }
            catch (Exception)
            {
                return null;
                // throw new Exception("读取二进制文件" + _name + "失败！" + "错误信息：" + exp.Message);
            }
        }


        public void SaveBin(string _name, object _obj)
        {
            if (false == System.IO.Directory.Exists("BinData"))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory("BinData");
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("BinData\\" + _name + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, _obj);
            stream.Close();
            stream.Dispose();
        }

        public object ReadBin(string _name)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream streamc = new FileStream("BinData\\" + _name + ".bin", FileMode.Open, FileAccess.Read, FileShare.None);
                object ret = formatter.Deserialize(streamc);
                streamc.Close();
                streamc.Dispose();
                return ret;
            }
            catch (Exception)
            {
                return null;
                // throw new Exception("读取二进制文件" + _name + "失败！" + "错误信息：" + exp.Message);
            }
        }
    }
    /// <summary>
    /// 数组整行转化为以指令字符为间隔的一条字符串
    /// </summary>
    public class Conver
    {
        public static string ConvertToString<T>(IEnumerable<T> _contents, string _separator)
        {
            return string.Join(_separator, _contents);
        }

        public static string ConvertToString<T>(IEnumerable<T>[] _contents, string _separatorRow, string _separatorCell)
        {
            string str = "";
            foreach (IEnumerable<T> a in _contents)
            {
                str = str + string.Join(_separatorCell, a) + _separatorRow;
            }
            return str;
        }



        public static string[][] ConvertDtToStringArry(DataTable _dt, string _separatorRow, string _separatorCell)
        {
            int count = _dt.Rows.Count;
            string[][] strArry = new string[count][];
            for (int i = 0; i < count; i++)
            {
                strArry[i] = Array.ConvertAll(_dt.Rows[i].ItemArray, Convert.ToString);
            }
            return strArry;
        }

        public static string[][] TransPosition<T>(T[][] _arry2)
        {
            string[][] arry = new string[_arry2[0].Count()][];
            for (int k = 0; k < _arry2[0].Count(); k++)
            {
                arry[k] = new string[_arry2.Count()];
            }

            for (int i = 0; i < _arry2.Count(); i++)
            {
                for (int j = 0; j < _arry2[i].Count(); j++)
                {
                    arry[j][i] = _arry2[i][j].ToString();
                }
            }
            return arry;
        }
    }

    public class Direct
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))//判断文件夹是否存在 
            {
                Directory.CreateDirectory(path);//不存在则创建文件夹 
            }
        }
    }


    public class FileAct
    {
        public static void TransfFile(string _SourceFile, string _DestFile)
        {
            //if (!File.Exists(_DestFile))
            //{
            //    File.AppendText(_DestFile);//如果接收的文件不存在，就创建它
            //}
            File.Move(_SourceFile, _DestFile);
        }
    }


    public class Exe
    {
        public string path = "";
        public Assembly ObjAssembly = null;
        public Exe(string _path)
        {
            path = _path;

        }

        public object GetVarible(string type, string name)
        {
            ObjAssembly = Assembly.LoadFile(path);
            FieldInfo property = ObjAssembly.GetType(type).GetField(name);
            var value = property.GetValue(type);
            return value;
        }

        public void SetVarible(string type, string name, object value)
        {
            ObjAssembly = Assembly.LoadFile(path);
            Type tp = ObjAssembly.GetType(type);
            FieldInfo fi = tp.GetField(name);
            fi.SetValue(null, value);
        }

    }

}