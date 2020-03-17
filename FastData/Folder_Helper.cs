using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastData
{
    /// <summary>
    /// 这是关于文件夹的操作
    /// </summary>
    public class Folder_Helper
    {
        //Directory.CreateDirectory(string path);//在指定路径中创建所有目录和子目录，除非已经存在
        //Directory.Delete(string path);//从指定路径删除空目录
        //Directory.Delete(string path, bool recursive);//布尔参数为true可删除非空目录
        //Directory.Exists(string path);//确定路径是否存在
        //Directory.GetCreationTime(string path);//获取目录创建日期和时间
        //Directory.GetCurrentDirectory();//获取应用程序当前的工作目录
        //Directory.GetDirectories(string path);//返回指定目录所有子目录名称，包括路径
        //Directory.GetFiles(string path);//获取指定目录中所有文件的名称，包括路径
        //Directory.GetFileSystemEntries(string path);//获取指定路径中所有的文件和子目录名称
        //Directory.GetLastAccessTime(string path);//获取上次访问指定文件或目录的时间和日期
        //Directory.GetLastWriteTime(string path);//返回上次写入指定文件或目录的时间和日期
        //Directory.GetParent(string path);//检索指定路径的父目录，包括相对路径和绝对路径
        //Directory.Move(string soureDirName, string destName);//将文件或目录及其内容移到新的位置
        //Directory.SetCreationTime(string path);//为指定的目录或文件设置创建时间和日期
        //Directory.SetCurrentDirectory(string path);//将应用程序工作的当前路径设为指定路径
        //Directory.SetLastAccessTime(string path);//为指定的目录或文件设置上次访问时间和日期
        //Directory.SetLastWriteTime(string path);//为指定的目录和文件设置上次访问时间和日期
        ///******************************************************************************************///
        /// ******************************************************************************************///


        /// <summary>
        /// 判断文件是否存在，如果不存在，就创建；反之。
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))//判断文件夹是否存在 
            {
                Directory.CreateDirectory(path);//不存在则创建文件夹 
            }
        }

        /// <summary>
        /// 获得文件夹下的所有目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFolderFile(string path)
        {
            string[] fileName;
            if (Directory.Exists(path))//判断文件夹是否存在 
            {
                fileName = Directory.GetFiles(path);
                return fileName;
            }
            else
            {
                Exception ex = new Exception("指定路径下文件夹不存在");
                throw (ex);
            }
        }


        /// <summary>
        /// 打开指定位置的文件夹
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        public static void OpenFolder(string folderPath)
        {
            System.Diagnostics.Process.Start(folderPath, "ExpLore");
        }

    }
}