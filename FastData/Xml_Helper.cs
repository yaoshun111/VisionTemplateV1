using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FastData
{
    public class Xml_Helper
    {
        private string _readData;
        private string _writeData;
        XmlDocument xDoc = new XmlDocument();  //实例化  xmlDecument类

        ///********************************创建和删除xml文件****************************************///
        ///*****************************************************************************************///


        /// <summary>
        /// 创建Xml文件，仅添加根节点Data,后续节点另外添加
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void CreateXml(string filePath)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在       
                {
                    //xml文件已经存在，无需重复创建
                }
                else
                {
                    XmlDeclaration declaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"); //创建一个声明xml文档所需要的语法的变量
                    xDoc.AppendChild(declaration); //将声明好的变量 添加到xml文件的末尾     

                    //一个xml文档，必须要有一个更元素
                    //创建根节点Data                     //第一级节点，又称根节点
                    //XmlElement elem = xDoc.CreateElement("Data");
                    XmlNode elem = xDoc.CreateNode("element", "Data", "");

                    //把根节点添加到xml文档中
                    xDoc.AppendChild(elem);   //创建Xml文件时，必须要写出他的根节点，不然会报错
                    xDoc.Save(filePath + ".xml");//将生成好的xml文件保存到指定位置
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 删除指定路径下的xml文件
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteXml(string filePath)
        {
            if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在       
            {
                File.Delete(filePath + ".xml");
            }
            else
            {

            }
        }




        ///********************************创建和删除相关节点***********************************************///
        ///************************************************************************************************///


        /// <summary>
        /// 添加不同等级的节点
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="parentNodeName">父节点名称</param>
        /// <param name="nodeName">将要添加的节点名称</param>
        public void CreateNode(string filePath, string parentNodeName, string nodeName)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在       
                {
                    //xml文件已经存在，无需重复创建
                }
                else
                {
                    //说明xml文件不存在
                    throw new Exception("找不到指定的xml文件，请检查");
                }
                xDoc.Load(filePath + ".xml");  //加载文件
                XmlElement xmlElem = xDoc.DocumentElement;//获取根节点                             
                XmlElement elem1 = xDoc.CreateElement(nodeName); //添加子节点为元素                    

                if (xmlElem.Name.ToString() == parentNodeName)
                {
                    //如果父节点就是根节点，就不做判断处理
                    xmlElem.AppendChild(elem1);
                }
                else
                {
                    XmlNodeList bodyNode = xmlElem.GetElementsByTagName(parentNodeName);//取节点名bodyXmlNode
                    if (!(bodyNode.Count > 0))//用于检查父节点是否在xml文件中存在
                    {
                        //说明父节点不存在
                        throw new Exception("父节点不存在，请检查");
                    }
                    else
                    {
                        //将子节点，放置到对应父节点的下面              
                        bodyNode.Item(0).AppendChild(elem1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //创建完以后记得保存，不然不会自动保存的
                xDoc.Save(filePath + ".xml");
            }

        }



        /// <summary>
        /// 删除所有节点
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        public void DeleteAllNode(string filePath)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在       
                {
                    //xml文件已经存在，无需重复创建
                }
                else
                {
                    //说明xml文件不存在
                    throw new Exception("找不到指定的xml文件，请检查");
                }

                XmlNode node = xDoc.DocumentElement;//获得根节点
                node.RemoveAll(); //删除该节点下的所有节点和属性
                xDoc.Save(filePath + ".xml"); //一定要记得保存文件
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// 删除指定名称的节点
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="nodeName">要删除的节点的名称</param>
        public void DeleteNode(string filePath, string nodeName)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在       
                {
                    //xml文件已经存在，无需重复创建
                }
                else
                {
                    //说明xml文件不存在
                    throw new Exception("找不到指定的xml文件，请检查");
                }

                // 加载指定的XML数据
                xDoc.Load(filePath + ".xml");
                XmlNodeList nodes = xDoc.SelectNodes("//" + nodeName);//使用XPath定位要删除的节点
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    nodes[i].ParentNode.RemoveChild(nodes[i]);
                }
                xDoc.Save(filePath + "xml");//保存文件
            }
            catch (Exception)
            {
                throw;
            }
        }





        ///********************************读写xml文件***********************************************///
        ///******************************************************************************************///



        /// <summary>
        /// 读取XML文件指定节点名称的值
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="cmbName">要读取值的节点名称</param>
        public string ReadXml(string filePath, string cmbName)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在  
                {
                    ///文件存在
                }
                else
                {
                    //说明xml文件不存在
                    throw new Exception("找不到指定的xml文件，请检查");
                }

                xDoc.Load(filePath + ".xml");  //加载文件
                XmlElement node = (XmlElement)xDoc.SelectSingleNode("//" + cmbName);  //指定节点名称
                _readData = node.InnerText;//将节点中的值反馈出来

                //相对于把值写入到节点中，读取他的值，实际上就是一个逆过程
                return _readData;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// 向XML文件写入指定节点名称的值
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="cmbName">要写入值的节点名称</param>
        /// <param name="setParameter">要写入的值</param>
        public bool WriteXml(string filePath, string cmbName, string setParameter)
        {
            try
            {
                if (File.Exists(filePath + ".xml"))   //判断Xml文件是否存在  
                {
                    ///文件存在
                }
                else
                {
                    //说明xml文件不存在
                    throw new Exception("找不到指定的xml文件，请检查");
                }
                xDoc.Load(filePath + ".xml");  //加载文件
                XmlElement node = (XmlElement)xDoc.SelectSingleNode("//" + cmbName);  //指定节点名称
                node.InnerText = setParameter;
                xDoc.Save(filePath + ".xml");
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

    }
}
