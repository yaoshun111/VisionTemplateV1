using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastData
{
    /// <summary>
    /// 这是一个由 瑞典MySQL AB 公司 开发的MySQL的数据库
    /// 使用前，需要先添加对应的mysql引用-->MySql.Data.6.9.12\lib\net45\MySql.Data.dll
    /// </summary>
    public class MySQL
    {
        private MySqlConnection conn;
        public object lock_sql = new object();//上锁

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="ConnStr">连接数据库的SQL 语句</param>
        /// 语句类似于："Database='DB';Data Source='localhost';User Id='root';Password='12345678';charset='utf8';pooling=true"
        /// <returns></returns>
        public bool SqlConn(string connStr)
        {
            lock (lock_sql)
            {
                try
                {
                    conn = new MySqlConnection(connStr);
                    if (!CheckStates())
                    {
                        conn.Open();
                        return CheckStates();//再检查一遍
                    }
                    else
                    {
                        throw new Exception("当前数据库处于连接状态，无需重复打开");
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        /// <summary>
        /// 关闭于数据库的连接
        /// </summary>
        public bool SqlClose()
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStates())//检查数据库当前的连接状态
                    {
                        conn.Close();
                        return !CheckStates();//这里取反了，所以，最后return的是true，就说明关闭成功了；反之。
                    }
                    else
                    {
                        throw new Exception("当前数据库处于关闭状态，无需重复关闭");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        /// <summary>
        /// 检查数据库的连接状态
        /// </summary>
        public bool CheckStates()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    //连接成功
                    return true;
                }
                else
                {
                    //连接失败
                    return false;
                }
            }
            catch (Exception)
            {
                //连接失败
                return false;
            }
          
        }

        /// <summary>
        /// 检查数据库的连接状态，如果没有连接，重连；反之
        /// </summary>
        /// <param name="connStr">连接数据库的sql语句</param>
        /// <returns></returns>
        public bool CheckStatesAndReconnection(string connStr)
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    //连接成功
                    return true;
                }
                else
                {
                    if (SqlConn(connStr))
                    {
                        //连接成功
                        return true;
                    }
                    else
                    {
                        //连接失败
                        return false;
                    }
                }
            }
            else
            {
                if (SqlConn(connStr))
                {
                    //连接成功
                    return true;
                }
                else
                {
                    //连接失败
                    return false;
                }
            }
        }







        //*********************************向数据库写入数据***********************************//
        //***********************************************************************************//
        //***********************************************************************************//


        /// <summary>
        /// 通过SQL语句向数据库中插入数据。
        /// 用这个的话，就需要自己写完整的sql语句，没有其他的两个方法用起来方便
        /// </summary>
        /// <param name="sql_command">要插入数据的sql命令语句</param>
        /// <param name="sql_connection">连接数据库的sql命令</param>
        /// <returns></returns>
        public bool InsertData(string sql_command, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库是否打开
                    {
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        return result == 1 ? true : false;
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// 通过SQL语句向数据库指定列插入数据
        /// 这些值是string类型的List数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnNames">对应列名称</param>
        /// <param name="values">对应要插入的值</param>    
        /// <param name="sql_connection">连接数据库的sql命令</param>   
        /// <returns></returns>
        public bool InsertGluingSystemData(string tableName, string[] columnNames, List<string> values, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库是否打开
                    {
                        string cNames = string.Empty;
                        for (int i = 0; i < columnNames.Length; i++)//这个for循环是为了将所有列名整合成一个字符串语句的。如下例
                        {
                            if (i == 0)
                            {
                                cNames += "(`" + columnNames[i] + "`,";
                            }
                            else if (i > 0 && i < columnNames.Length - 1)
                            {
                                cNames += "`" + columnNames[i] + "`,";
                            }
                            else
                            {
                                cNames += "`" + columnNames[i] + "`)";
                            }
                        }

                        //cNames = "(`生产时间`,`1号电子秤称重前重量 / g`,`1号秤判稳时间 / ms`,`1号秤称重结果`,`2号电子秤称重前重量 / g`,`2号秤判稳时间 / ms`,`2号秤称重结果`,`注液泵转速 / RPM`,`液体密度 / g/ml`,`注液量R / g`,`注液量L / g`)";
                        string sql = "INSERT INTO `db_m6r_database`.`" + tableName + "` " + cNames;
                        for (int i = 0; i < values.Count; i++)//这个for循环是为了补全sql语句的
                        {
                            if (i == 0)
                            {
                                sql = sql + "VALUES ('" + values[i].ToString() + "',";
                            }
                            else if (i == values.Count - 1)
                            {
                                sql = sql + String.Format("'{0}')", values[i].ToString());
                            }
                            else
                            {
                                sql = sql + String.Format("'{0}',", values[i].ToString());
                            }
                        }
                        MySqlCommand cmd = new MySqlCommand(sql, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result == 1 ? true : false;//判断写入是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        /// <summary>
        /// 通过SQL语句向数据库指定列插入数据
        /// 这些值可以是任意数据类型的数据集合，都存储在以values为名的list集合中
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnNames">对应列名称</param>
        /// <param name="values">对应要插入的值</param>
        /// <param name="sql_connection">连接数据库的sql命令</param>
        /// <returns></returns>
        public bool InsertGluingSystemData(string tableName, string[] columnNames, List<object> values, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库是否打开
                    {
                        string cNames = string.Empty;
                        for (int i = 0; i < columnNames.Length; i++)//这个for循环是为了将所有列名整合成一个字符串语句的。如下例
                        {
                            if (i == 0)
                            {
                                cNames += "(`" + columnNames[i] + "`,";
                            }
                            else if (i > 0 && i < columnNames.Length - 1)
                            {
                                cNames += "`" + columnNames[i] + "`,";
                            }
                            else
                            {
                                cNames += "`" + columnNames[i] + "`)";
                            }
                        }

                        //cNames = "(`生产时间`,`1号电子秤称重前重量 / g`,`1号秤判稳时间 / ms`,`1号秤称重结果`,`2号电子秤称重前重量 / g`,`2号秤判稳时间 / ms`,`2号秤称重结果`,`注液泵转速 / RPM`,`液体密度 / g/ml`,`注液量R / g`,`注液量L / g`)";
                        string sql = "INSERT INTO `db_m6r_database`.`" + tableName + "` " + cNames;
                        for (int i = 0; i < values.Count; i++)//这个for循环是为了补全sql语句的
                        {
                            if (i == 0)
                            {
                                sql = sql + "VALUES ('" + values[i].ToString() + "',";
                            }
                            else if (i == values.Count - 1)
                            {
                                sql = sql + String.Format("'{0}')", values[i].ToString());
                            }
                            else
                            {
                                sql = sql + String.Format("'{0}',", values[i].ToString());
                            }
                        }
                        MySqlCommand cmd = new MySqlCommand(sql, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result == 1 ? true : false;//判断写入是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }





        //*********************************检索数据库中数据***********************************//
        //***********************************************************************************//
        //***********************************************************************************//


        /// <summary>
        /// 根据sql语句，检索数据库中指定数据，定反馈到Dataset数据集中
        /// </summary>
        /// <param name="sql_command">用于检索的sql命令</param>
        /// <param name="sql_connection">连接数据库的sql命令</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql_command, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        //创建一个MySqlCommand对象
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        //清除参数
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    return null;
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 检索某列内一定范围的数据，并反馈到Dataset数据集中。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束为止</param>
        /// <param name="sql_connection">数据库连接的sql语句</param>
        /// <returns></returns>
        public DataSet GetDataSet(string tableName, string columnName, string start, string end, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        string sql_command = string.Format("SELECT * FROM  `{0}` WHERE `{1}` BETWEEN '{2}'AND '{3}'", tableName, columnName, start, end);//update的sql语句
                                                                                                                                                         //创建一个SqlCommand对象
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        //清除参数
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }





        /// <summary>
        /// 检索某列内某一项的数据，并反馈到Dataset数据集中。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="value">值</param>
        /// <param name="sql_connection">数据库连接的sql语句</param>
        /// <returns></returns>
        public DataSet GetDataSet(string tableName, string columnName, string value, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        string sql_command = string.Format("SELECT * FROM  `{0}` WHERE `{1}`  LIKE'{2}%'", tableName, columnName, value);//update的sql语句
                                                                                                                                         //创建一个SqlCommand对象
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        //清除参数
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }


        /// <summary>
        /// 根据分页来获取数据，返回DataSet数据
        /// </summary>
        /// <param name="strSql">Select字符串语句</param>
        /// <param name="pageSize">每页的行数</param>
        /// <returns></returns>
        public DataSet GetDataByPagination(string selectSql, int pageSize)
        {
            DataSet srcDataSet = new DataSet();
            try
            {
                using (MySqlDataAdapter command = new MySqlDataAdapter(selectSql, conn))
                {
                    command.Fill(srcDataSet, "ds");
                    int number = srcDataSet.Tables[0].Rows.Count / pageSize;
                    if ((srcDataSet.Tables[0].Rows.Count - number * pageSize) > 0)
                        number++;
                    DataTable[] dts = new DataTable[number];
                    int i = 0;
                    for (i = 0; i < number; i++)
                    {
                        dts[i] = srcDataSet.Tables[0].Clone();
                        dts[i].TableName = string.Format("dt{0}", i);
                        DataRow[] dataRows = srcDataSet.Tables[0].Rows.OfType<DataRow>().Skip(i * pageSize).Take(pageSize).ToArray();
                        dts[i] = dataRows.CopyToDataTable();
                    }
                    //i = 0;
                    //foreach (DataRow r in srcDataSet.Tables[0].Rows)
                    //{
                    //    dts[i / pageSize].Rows.Add(r.ItemArray);
                    //    i++;
                    //}
                    foreach (DataTable dt in dts)
                        srcDataSet.Tables.Add(dt);
                }

                return srcDataSet;
            }
            catch (MySqlException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        ///按照时间的检索方式，他同样可以扩展为检索某一列 某个范围内的 数据
        /// 
        //string startTime = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");//起始时间
        //string endTime = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");//结束时间
        //
        //string sql = string.Format("SELECT * FROM 表名 WHERE 列名 BETWEEN '{0}'AND '{1}'", startTime, endTime);//sql语句，检索两个时间段内的所有数据

        ///模糊搜索的方式，搜索某一项的所有数据
        //string sql = string.Format("SELECT * FROM 表名 WHERE  列名 LIKE'{0}%'", "检索项名称");







        //*********************************修改数据库中数据***********************************//
        //***********************************************************************************//
        //***********************************************************************************//


        /// <summary>
        /// 修改数据库中的数据
        /// 通用
        /// </summary>
        /// <param name="sql_command">用于检索的sql命令</param>
        /// <param name="sql_connection">连接数据库的sql命令</param>
        /// <returns></returns>
        public bool Update(string sql_command, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result >= 1 ? true : false;//判断写入是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }





        /// <summary>
        /// 修改数据库中的数据（整表操作）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column1Name">要修改值的列名</param>
        /// <param name="value1">要修改的值</param>
        /// <param name="sql_connection">连接数据库的sql语句</param>
        /// <returns></returns>
        public bool Update(string tableName, string column1Name, string value1, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        string sql_command = string.Format("update `{0}` set `{1}` = '{2}'", tableName, column1Name, value1);//update的sql语句
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result >= 1 ? true : false;//判断写入是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }






        /// <summary>
        /// 修改数据库中的数据（表内指定范围的数据操作）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column1Name">要修改值的列名</param>
        /// <param name="value1">要修改的值</param>
        /// <param name="column2Name">用于限定范围的列名</param>
        /// <param name="value2">用于限定范围的列名的值</param>
        /// <param name="sql_connection">连接数据库的sql语句</param>
        /// <returns></returns>

        public bool Update(string tableName, string column1Name, string value1, string column2Name, string value2, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        string sql_command = string.Format("update `{0}` set `{1}` = '{2}' where `{3}` ='{4}' ", tableName, column1Name, value1, column2Name, value2);//update的sql语句
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result >= 1 ? true : false;//判断写入是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        /// updata 的sql语句
        /// 
        ///  string sql = string.Format("update 表名 set 列名1 = 'value1' where 列名2 = 'value2' ");
        /// 说明：
        /// 检索“表”中，列名2='value2'的那些项，将它们的 列名1 的那些列的值，全部替换成'value1'。







        //*********************************删除数据库中数据***********************************//
        //***********************************************************************************//
        //***********************************************************************************//


        /// <summary>
        /// 删除数据中的数据
        /// 通用
        /// </summary>
        /// <param name="sql_command">删除的sql命令</param>
        /// <param name="sql_connection">连接数据库的sql语句</param>
        /// <returns></returns>
        public bool Delete(string sql_command, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result >= 1 ? true : false;//判断数据删除是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }



        /// <summary>
        /// 删除数据中的数据（指定列的同类数据）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="value">值</param>
        /// <param name="sql_connection">连接数据库的sql语句</param>
        /// <returns></returns>
        public bool Delete(string tableName, string columnName, string value, string sql_connection)
        {
            lock (lock_sql)
            {
                try
                {
                    if (CheckStatesAndReconnection(sql_connection))//检查数据库的连接状态
                    {
                        string sql_command = string.Format("delete from `{0}` where `{1}` ='{2}' ", tableName, columnName, value);//delete的sql语句

                        MySqlCommand cmd = new MySqlCommand(sql_command, conn);//数据写入数据库
                        int result = cmd.ExecuteNonQuery();//检查数据库内受影响的行数
                        cmd.Dispose();
                        return result >= 1 ? true : false;//判断数据删除是否成功
                    }
                    else
                    {
                        throw new Exception("当前数据库没有连接，请检查");
                    }
                }
                catch (Exception)
                {
                    //System.Windows.Forms.MessageBox.Show("MySqlHelper \r\n" + ex.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw;
                }
            }
        }



    }
}
