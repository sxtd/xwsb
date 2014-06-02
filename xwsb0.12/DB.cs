using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace xwsb0._11
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    public static class DB
    {
        /// <summary>
        /// 获得oracle连接
        /// </summary>
        /// <returns>返回oracle连接</returns>
        public static OracleConnection getConnection()
        {
            OracleConnection conn = null;
            try
            {
               string decryptStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
               string connStr = CryptClass.DecryptDES(decryptStr, "12345678");
                Console.WriteLine(connStr);
               conn = new OracleConnection(connStr);
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("连接数据库错误：" +oe);
            }
            return conn;
        }

        /// <summary>
        /// 获得所有筛选过的学生（xwzs_init_Temp）的数据，
        /// </summary>
        /// <returns>返回学生列表</returns>
        public static List<StuInit> getStuInitTempList()
        {
            List<StuInit> stuInitList = new List<StuInit>();

            OracleConnection conn = DB.getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getStuInitTemp";

            //输入为游标参数
            cmd.Parameters.Add("stu_cur", OracleDbType.RefCursor);
            cmd.Parameters["stu_cur"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StuInit si = new StuInit();
                    si.id = reader.GetString(reader.GetOrdinal("id"));
                    si.zslx = reader.GetString(reader.GetOrdinal("zslx"));
                    si.zsbh = reader.GetString(reader.GetOrdinal("zsbh"));
                    si.yxsdm = reader.GetString(reader.GetOrdinal("yxsdm"));
                    si.dwdm = reader.GetString(reader.GetOrdinal("dwdm"));
                    si.dwmc = reader.GetString(reader.GetOrdinal("dwmc"));
                    si.xkmlm = reader.GetString(reader.GetOrdinal("xkmlm"));
                    si.xkml = reader.GetString(reader.GetOrdinal("xkml"));
                    si.yjxkdm = reader.IsDBNull(reader.GetOrdinal("yjxkdm"))?"":reader.GetString(reader.GetOrdinal("yjxkdm"));
                    si.yjxk = reader.IsDBNull(reader.GetOrdinal("yjxk")) ? "" : reader.GetString(reader.GetOrdinal("yjxk"));
                    si.zydm = reader.GetString(reader.GetOrdinal("zydm"));
                    si.zymc = reader.GetString(reader.GetOrdinal("zymc"));
                    si.xh = reader.GetString(reader.GetOrdinal("xh"));
                    si.xm = reader.GetString(reader.GetOrdinal("xm"));
                    si.xb = reader.GetString(reader.GetOrdinal("xb"));
                    si.csrq = reader.GetString(reader.GetOrdinal("csrq"));
                    si.sfzh = reader.GetString(reader.GetOrdinal("sfzh"));
                    si.gb = reader.IsDBNull(reader.GetOrdinal("gb")) ? "" : reader.GetString(reader.GetOrdinal("gb"));
                    si.zyxwmc = reader.IsDBNull(reader.GetOrdinal("zyxwmc")) ? "" : reader.GetString(reader.GetOrdinal("zyxwmc"));
                    si.xxfs = reader.IsDBNull(reader.GetOrdinal("xxfs")) ? "" : reader.GetString(reader.GetOrdinal("xxfs"));
                    si.gclymc = reader.IsDBNull(reader.GetOrdinal("gclymc")) ? "" : reader.GetString(reader.GetOrdinal("gclymc"));
                    si.byyx = reader.IsDBNull(reader.GetOrdinal("byyx")) ? "" : reader.GetString(reader.GetOrdinal("byyx"));
                    si.lxsm = reader.GetString(reader.GetOrdinal("lxsm"));
                    si.dycs = reader.GetString(reader.GetOrdinal("dycs"));
                    si.dy = reader.GetString(reader.GetOrdinal("dy"));
                    si.zzrq = reader.GetString(reader.GetOrdinal("zzrq"));
                    si.printtime = reader.GetString(reader.GetOrdinal("printtime"));
                    si.printername = reader.GetString(reader.GetOrdinal("printername"));

                    stuInitList.Add(si);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("从数据库读取xwzs_init_temp，数据表错误:" + oe);
            }
            finally
            {
                conn.Close();
            }

            return stuInitList;
        }
        
        /// <summary>
        /// 获得导入的未筛选过的学生名单(xwzs_init)中的数据
        /// </summary>
        /// <returns></returns>
        public static List<StuInit> getStuInitList()
        {
            List<StuInit> stuInitList = new List<StuInit>();

            OracleConnection conn = DB.getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getStuInit";

            //输入为游标参数
            cmd.Parameters.Add("stu_cur", OracleDbType.RefCursor);
            cmd.Parameters["stu_cur"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StuInit si = new StuInit();
                    si.id = reader.GetString(reader.GetOrdinal("id"));
                    si.zslx = reader.GetString(reader.GetOrdinal("zslx"));
                    si.zsbh = reader.GetString(reader.GetOrdinal("zsbh"));
                    si.yxsdm = reader.GetString(reader.GetOrdinal("yxsdm"));
                    si.dwdm = reader.GetString(reader.GetOrdinal("dwdm"));
                    si.dwmc = reader.GetString(reader.GetOrdinal("dwmc"));
                    si.xkmlm = reader.GetString(reader.GetOrdinal("xkmlm"));
                    si.xkml = reader.GetString(reader.GetOrdinal("xkml"));
                    si.yjxkdm = reader.IsDBNull(reader.GetOrdinal("yjxkdm")) ? "" : reader.GetString(reader.GetOrdinal("yjxkdm")); ;
                    si.yjxk = reader.IsDBNull(reader.GetOrdinal("yjxk")) ? "" : reader.GetString(reader.GetOrdinal("yjxk")); ;
                    si.zydm = reader.GetString(reader.GetOrdinal("zydm"));
                    si.zymc = reader.GetString(reader.GetOrdinal("zymc"));
                    si.xh = reader.GetString(reader.GetOrdinal("xh"));
                    si.xm = reader.GetString(reader.GetOrdinal("xm"));
                    si.xb = reader.GetString(reader.GetOrdinal("xb"));
                    si.csrq = reader.GetString(reader.GetOrdinal("csrq"));
                    si.sfzh = reader.GetString(reader.GetOrdinal("sfzh"));
                    si.gb = reader.IsDBNull(reader.GetOrdinal("gb")) ? "" : reader.GetString(reader.GetOrdinal("gb")); ;
                    si.zyxwmc = reader.IsDBNull(reader.GetOrdinal("zyxwmc")) ? "" : reader.GetString(reader.GetOrdinal("zyxwmc")); ;
                    si.xxfs = reader.IsDBNull(reader.GetOrdinal("xxfs")) ? "" : reader.GetString(reader.GetOrdinal("xxfs")); ;
                    si.gclymc = reader.IsDBNull(reader.GetOrdinal("gclymc")) ? "" : reader.GetString(reader.GetOrdinal("gclymc")); ;
                    si.byyx = reader.IsDBNull(reader.GetOrdinal("byyx")) ? "" : reader.GetString(reader.GetOrdinal("byyx")); ;
                    si.lxsm = reader.GetString(reader.GetOrdinal("lxsm"));
                    si.dycs = reader.GetString(reader.GetOrdinal("dycs"));
                    si.dy = reader.GetString(reader.GetOrdinal("dy"));
                    si.zzrq = reader.GetString(reader.GetOrdinal("zzrq"));
                    si.printtime = reader.GetString(reader.GetOrdinal("printtime"));
                    si.printername = reader.GetString(reader.GetOrdinal("printername"));

                    stuInitList.Add(si);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("从数据库读取xwzs_init，数据表错误:" + oe);
            }
            finally
            {
                conn.Close();
            }

            return stuInitList;
        }

        /// <summary>
        /// 按照打印时间来筛选学生名单，将学生从xwzs_init中筛选出来并存放到xwzs_init_temp中
        /// </summary>
        /// <param name="starttime">起始时间，精确到毫秒</param>
        /// <param name="endtime">结束时间，精确到毫秒</param>
        /// <param name="firstFilter">是否是第一次筛选，如果是第一次筛选，需要对原来的xwzs_init_temp进行清空；
        /// 如果不是首次筛选，则直接在xwzs_init_temp中增加和减少</param>
        public static void FilterXwzsByPrinttime(string starttime, string endtime, bool firstFilter)
        {
            OracleConnection conn = getConnection();

            //第一次筛选，清空表xwzs_init_temp
            if (firstFilter) 
            {
                // xwzs_init_temp用于存储符合一定条件的上报学生名单
                //先清空表 xwzs_init_temp ，
                OracleCommand cleanTabCmd = new OracleCommand();
                cleanTabCmd.Connection = conn;
                cleanTabCmd.CommandType = CommandType.StoredProcedure;
                cleanTabCmd.CommandText = "xwsbPackage.cleanTab";
                cleanTabCmd.Parameters.Add("tableName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cleanTabCmd.Parameters["tableName"].Value = "xwzs_init_temp";
                try
                {
                    conn.Open();
                    cleanTabCmd.ExecuteNonQuery();
                }
                catch (OracleException oe)
                {
                    System.Windows.MessageBox.Show("清空xwzs_init_temp出错。" + oe);
                }
                finally
                {
                    conn.Close();
                }
            }

            //按照打印时间，将筛选出来的学生放入xwsb_init_temp中
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.filterXwzsByPrinttime";
            cmd.Parameters.Add("starttime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["starttime"].Value = starttime;
            cmd.Parameters.Add("endtime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["endtime"].Value = endtime;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("按照打印时间筛选学生名单错误：" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 按照打印时间和学号开头筛选学生
        /// </summary>
        /// <param name="starttime">起始时间，精确到毫秒</param>
        /// <param name="endtime">结束时间，精确到毫秒</param>
        /// <param name="xh">学号开头，要求4位</param>
        /// <param name="firstFilter">是否是首次筛选，如果是第一次筛选，需要对原来的xwzs_init_temp进行清空；
        /// 如果不是首次筛选，则直接在xwzs_init_temp中增加和减少</param>
        public static void FilterXwzsByPtXh(string starttime, string endtime,string xh, bool firstFilter)
        {
            string xhLike = xh + "%";

            OracleConnection conn = getConnection();

            //第一次筛选，清空表xwzs_init_temp
            if (firstFilter)
            {
                // xwzs_init_temp用于存储符合一定条件的上报学生名单
                //先清空表 xwzs_init_temp ，
                OracleCommand cleanTabCmd = new OracleCommand();
                cleanTabCmd.Connection = conn;
                cleanTabCmd.CommandType = CommandType.StoredProcedure;
                cleanTabCmd.CommandText = "xwsbPackage.cleanTab";
                cleanTabCmd.Parameters.Add("tableName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cleanTabCmd.Parameters["tableName"].Value = "xwzs_init_temp";
                try
                {
                    conn.Open();
                    cleanTabCmd.ExecuteNonQuery();
                }
                catch (OracleException oe)
                {
                    System.Windows.MessageBox.Show("清空xwzs_init_temp出错。" + oe);
                }
                finally
                {
                    conn.Close();
                }
            }

            //按照打印时间 + 学号，将筛选出来的学生放入xwsb_init_temp中
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.filterXwzsByPtXh";
            cmd.Parameters.Add("starttime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["starttime"].Value = starttime;
            cmd.Parameters.Add("endtime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["endtime"].Value = endtime;
            cmd.Parameters.Add("xhLike", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["xhLike"].Value = xhLike;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("按照打印时间和学号，筛选学生名单错误：" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 减少学生：按照打印时间删除xwzs_init_temp中的部分学生
        /// </summary>
        /// <param name="starttime">起始时间</param>
        /// <param name="endtime">结束时间</param>
        public static void FilterXwzsByPtDel(string starttime, string endtime)
        {
            OracleConnection conn = getConnection();

            //按照打印时间，xwsb_init_temp中符合条件的学生删除
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.filterXwzsByPtDel";
            cmd.Parameters.Add("starttime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["starttime"].Value = starttime;
            cmd.Parameters.Add("endtime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["endtime"].Value = endtime;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("从名单中减少学生，按照打印时间筛选错误：" + oe);
            }
            finally
            {
                conn.Close();
            }
 
        }

        /// <summary>
        /// 按照打印时间和学号开头，删除xwzs_init_temp中的部分学生
        /// </summary>
        /// <param name="starttime">起始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="xh">学号开头，4位</param>
        public static void FilterXwzsByPtXhDel(string starttime, string endtime, string xh)
        {
            string xhLike = xh + "%";

            OracleConnection conn = getConnection();


            //按照打印时间 + 学号，xwsb_init_temp中符合条件的学生删除
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.filterXwzsByPtXhDel";
            cmd.Parameters.Add("starttime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["starttime"].Value = starttime;
            cmd.Parameters.Add("endtime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["endtime"].Value = endtime;
            cmd.Parameters.Add("xhLike", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["xhLike"].Value = xhLike;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("从名单中减少学生，按照打印时间和学号，筛选出错：" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获得筛选后的学生数量，xwzs_init_temp的学生数量
        /// </summary>
        /// <returns>返回学生数量</returns>
        public static int getStuInitTempCount()
        {
            int stuInitCount = 0;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getStuInitCount";
            cmd.Parameters.Add("stu_count", OracleDbType.Int32);
            cmd.Parameters["stu_count"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteScalar();

                stuInitCount = Convert.ToInt32(cmd.Parameters["stu_count"].Value.ToString());
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取学生人数出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return stuInitCount;
        }

        /// <summary>
        /// 获得最终表（xwzs_final）中学生数量
        /// </summary>
        /// <returns>返回学生人数</returns>
        public static int getStuFinalCount()
        {
            int stuInitCount = 0;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getStuFinalCount";
            cmd.Parameters.Add("stu_count", OracleDbType.Int32);
            cmd.Parameters["stu_count"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteScalar();

                stuInitCount = Convert.ToInt32(cmd.Parameters["stu_count"].Value.ToString());
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取学生人数出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return stuInitCount;
        }

        /// <summary>
        /// 连接ACCESS数据库，包含所有要上报的学生
        /// </summary>
        /// <param name="dbPath">access数据库位置</param>
        /// <returns>返回access数据库中的所有数据表</returns>
        public static List<string> loadDbTable(string dbPath)
        {
            List<string> dbTableList = new List<string>();

            //string connAccessStr = ConfigurationManager.ConnectionStrings["connAccessStr"].ConnectionString;

            Console.WriteLine(dbPath);

            string path = dbPath.Replace("\\", "/");

            string connAccessStr = "Provider=Microsoft.Jet.OLEDB.4.0;data source=" +path;
            //创建OleDbConnection对象
            OleDbConnection oleConn = new OleDbConnection(connAccessStr);

            try
            {
                oleConn.Open();
                if (oleConn.State == ConnectionState.Open)
                {
                    System.Windows.MessageBox.Show("Access数据库的连接成功!", "Access数据库的连接");
                }
                else
                {
                    System.Windows.MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                    oleConn.Open();
                }
                DataTable dt = oleConn.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    if (row[3].ToString() == "TABLE")
                        dbTableList.Add(row[2].ToString());
                }
                

            }
            catch (OleDbException ode)
            {
                System.Windows.MessageBox.Show("访问Access数据库出错：" + ode);
            }
            finally
            {
                oleConn.Close();
            }

            return dbTableList;
        }

        /// <summary>
        /// 用户选择access中一个表后（该年所有需要上报的学生名单），将表中的数据存到数据库（xwzs_init）中
        /// </summary>
        /// <param name="dbPath">access文件位置</param>
        /// <param name="tableName">用户选择的access中的表名</param>
        /// <returns>返回本次导入数据库的学生数</returns>
        public static int loadXwzsFile(string dbPath,string tableName)
        {
            int xsmd_count = 0;
            OracleConnection conn = getConnection();

            //先将表xwzs_init清空
            OracleCommand cleanTableCmd = new OracleCommand();
            cleanTableCmd.Connection = conn;
            cleanTableCmd.CommandType = CommandType.StoredProcedure;
            cleanTableCmd.CommandText = "xwsbPackage.cleanXwzsInit";
            try
            {
                conn.Open();
                cleanTableCmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("清空xwzs_init出错：" + oe);
                return -1;
            }
            finally
            {
                conn.Close();
            }//清空xwzs_init

            //string connAccessStr = ConfigurationManager.ConnectionStrings["connAccessStr"].ConnectionString;
            //创建OleDbConnection对象
            string path = dbPath.Replace("\\", "/");
            string connAccessStr = "Provider=Microsoft.Jet.OLEDB.4.0;data source=" + path;
            OleDbConnection oleConn = new OleDbConnection(connAccessStr);
            OleDbCommand oleCmd = new OleDbCommand("select * from "+ tableName,oleConn);
            DataSet ds = new DataSet();
                
            //存入oracle数据库中
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure; //调用存储过程
            cmd.CommandText = "xwsbPackage.inertXsmdToTemp";

            try
            {
                oleConn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(oleCmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                conn.Open();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    //设置参数
                    cmd.Parameters.Add("v_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["v_id"].Value = row[0];
                    cmd.Parameters.Add("zslx", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zslx"].Value = row[1];
                    cmd.Parameters.Add("zsbh", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zsbh"].Value = row[2];
                    cmd.Parameters.Add("yxsdm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["yxsdm"].Value = row[3];
                    cmd.Parameters.Add("dwdm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["dwdm"].Value = row[4];
                    cmd.Parameters.Add("dwmc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["dwmc"].Value = row[5];
                    cmd.Parameters.Add("xkmlm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xkmlm"].Value = row[6];
                    cmd.Parameters.Add("xkml", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xkml"].Value = row[7];
                    cmd.Parameters.Add("yjkdm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["yjkdm"].Value = row[8];
                    cmd.Parameters.Add("yjxk", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["yjxk"].Value = row[9];
                    cmd.Parameters.Add("zydm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zydm"].Value = row[10];
                    cmd.Parameters.Add("zymc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zymc"].Value = row[11];
                    cmd.Parameters.Add("xh", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xh"].Value = row[12];
                    cmd.Parameters.Add("xm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xm"].Value = row[13];
                    cmd.Parameters.Add("xb", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xb"].Value = row[14];
                    cmd.Parameters.Add("csrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["csrq"].Value = row[15];
                    cmd.Parameters.Add("sfzh", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["sfzh"].Value = row[16];
                    cmd.Parameters.Add("gb", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["gb"].Value = row[17];
                    cmd.Parameters.Add("zyxwmc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zyxwmc"].Value = row[18];
                    cmd.Parameters.Add("xxfs", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["xxfs"].Value = row[19];
                    cmd.Parameters.Add("gclymc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["gclymc"].Value = row[20];
                    cmd.Parameters.Add("byyx", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["byyx"].Value = row[21];
                    cmd.Parameters.Add("lxsm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["lxsm"].Value = row[22];
                    cmd.Parameters.Add("dycs", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["dycs"].Value = row[23];
                    cmd.Parameters.Add("dy", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["dy"].Value = row[24];
                    cmd.Parameters.Add("zzrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zzrq"].Value = row[25];
                    cmd.Parameters.Add("printtime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["printtime"].Value = row[26];
                    cmd.Parameters.Add("printername", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["printername"].Value = row[27];

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear(); //很重要，因为插入不同记录时，使用的是同一个cmd，因此需要清空参数！！！

                    xsmd_count++;
                }
            }
            catch (OleDbException ode)
            {
                System.Windows.MessageBox.Show("访问access数据库出错" + ode);
                return -1;
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("访问access数据库出错" + oe);
                return -1;
            }
            finally
            {
                oleConn.Close();
                conn.Close();
            }
            return xsmd_count;
            }

        /// <summary>
        /// 将确定人数的临时表xwzs_init_temp，存入含最终字段的xwzs_final表中（先清空xwzs_final）
        /// </summary>
        public static void insertInitToFinal()
        {
            OracleConnection conn = getConnection();

            OracleCommand cleanTabCmd = new OracleCommand();
            cleanTabCmd.CommandType = CommandType.StoredProcedure;
            cleanTabCmd.CommandText = "xwsbPackage.cleanTab";
            cleanTabCmd.Connection = conn;

            cleanTabCmd.Parameters.Add("tableName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cleanTabCmd.Parameters["tableName"].Value = "xwzs_final";

            try
            {
                conn.Open();
                cleanTabCmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("清空xwzs_final出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.insertInitToFinal";
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("确认人数后的名单,转存最终名单错误：" + oe);
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// 获得xwzs_final中的学生名单
        /// </summary>
        /// <returns>返回含最终字段的学生名单列表</returns>
        public static List<StuFinal> getStuFinalList()
        {
            List<StuFinal> stuFinalList = new List<StuFinal>();

            OracleConnection conn = getConnection();
           
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getStuFinalList";

            cmd.Parameters.Add("stu_cur", OracleDbType.RefCursor);
            cmd.Parameters["stu_cur"].Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StuFinal sf = new StuFinal();

                    sf.xm = reader.IsDBNull(reader.GetOrdinal("xm"))?"":reader.GetString(reader.GetOrdinal("xm"));
                    sf.xmpy = reader.IsDBNull(reader.GetOrdinal("xmpy"))?"":reader.GetString(reader.GetOrdinal("xmpy"));
                    sf.xbm = reader.IsDBNull(reader.GetOrdinal("xbm"))?"":reader.GetString(reader.GetOrdinal("xbm"));
                    sf.xb = reader.IsDBNull(reader.GetOrdinal("xb"))?"":reader.GetString(reader.GetOrdinal("xb"));
                    sf.mzm = reader.IsDBNull(reader.GetOrdinal("mzm")) ? "" : reader.GetString(reader.GetOrdinal("mzm"));
                    sf.mz = reader.IsDBNull(reader.GetOrdinal("mz")) ? "" : reader.GetString(reader.GetOrdinal("mz"));
                    sf.zzmmm = reader.IsDBNull(reader.GetOrdinal("zzmmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmmm"));
                    sf.zzmm = reader.IsDBNull(reader.GetOrdinal("zzmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmm"));
                    sf.csrq = reader.IsDBNull(reader.GetOrdinal("csrq")) ? "" : reader.GetString(reader.GetOrdinal("csrq"));
                    sf.zjlxm = reader.IsDBNull(reader.GetOrdinal("zjlxm")) ? "" : reader.GetString(reader.GetOrdinal("zjlxm"));
                    sf.zjlx = reader.IsDBNull(reader.GetOrdinal("zjlx")) ? "" : reader.GetString(reader.GetOrdinal("zjlx"));
                    sf.zjhm = reader.IsDBNull(reader.GetOrdinal("zjhm")) ? "" : reader.GetString(reader.GetOrdinal("zjhm"));
                    sf.xwsydwm = reader.IsDBNull(reader.GetOrdinal("xwsydwm")) ? "" : reader.GetString(reader.GetOrdinal("xwsydwm"));
                    sf.xwsydw = reader.IsDBNull(reader.GetOrdinal("xwsydw")) ? "" : reader.GetString(reader.GetOrdinal("xwsydw"));
                    sf.xzxm = reader.IsDBNull(reader.GetOrdinal("xzxm")) ? "" : reader.GetString(reader.GetOrdinal("xzxm"));
                    sf.zxxm = reader.IsDBNull(reader.GetOrdinal("zxxm")) ? "" : reader.GetString(reader.GetOrdinal("zxxm"));
                    sf.pydwm = reader.IsDBNull(reader.GetOrdinal("pydwm")) ? "" : reader.GetString(reader.GetOrdinal("pydwm"));
                    sf.pydw = reader.IsDBNull(reader.GetOrdinal("pydw")) ? "" : reader.GetString(reader.GetOrdinal("pydw"));
                    sf.xwlbm = reader.IsDBNull(reader.GetOrdinal("xwlbm")) ? "" : reader.GetString(reader.GetOrdinal("xwlbm"));
                    sf.xwlb = reader.IsDBNull(reader.GetOrdinal("xwlb")) ? "" : reader.GetString(reader.GetOrdinal("xwlb"));
                    sf.zydm = reader.IsDBNull(reader.GetOrdinal("zydm")) ? "" : reader.GetString(reader.GetOrdinal("zydm"));
                    sf.zymc = reader.IsDBNull(reader.GetOrdinal("zymc")) ? "" : reader.GetString(reader.GetOrdinal("zymc"));
                    sf.zszymc = reader.IsDBNull(reader.GetOrdinal("zszymc")) ? "" : reader.GetString(reader.GetOrdinal("zszymc"));
                    sf.ksh = reader.IsDBNull(reader.GetOrdinal("ksh")) ? "" : reader.GetString(reader.GetOrdinal("ksh"));
                    sf.rxny = reader.IsDBNull(reader.GetOrdinal("rxny")) ? "" : reader.GetString(reader.GetOrdinal("rxny"));
                    sf.xh = reader.IsDBNull(reader.GetOrdinal("xh")) ? "" : reader.GetString(reader.GetOrdinal("xh"));
                    sf.xz = reader.IsDBNull(reader.GetOrdinal("xz")) ? "" : reader.GetString(reader.GetOrdinal("xz"));
                    sf.byny = reader.IsDBNull(reader.GetOrdinal("byny")) ? "" : reader.GetString(reader.GetOrdinal("byny"));
                    sf.hxwrq = reader.IsDBNull(reader.GetOrdinal("hxwrq")) ? "" : reader.GetString(reader.GetOrdinal("hxwrq"));
                    sf.sfdexw = reader.IsDBNull(reader.GetOrdinal("sfdexw")) ? "" : reader.GetString(reader.GetOrdinal("sfdexw"));
                    sf.sffxxw = reader.IsDBNull(reader.GetOrdinal("sffxxw")) ? "" : reader.GetString(reader.GetOrdinal("sffxxw"));
                    sf.xwzsbh = reader.IsDBNull(reader.GetOrdinal("xwzsbh")) ? "" : reader.GetString(reader.GetOrdinal("xwzsbh"));
                    sf.zp = reader.IsDBNull(reader.GetOrdinal("zp")) ? "" : reader.GetString(reader.GetOrdinal("zp"));
                    sf.bz = reader.IsDBNull(reader.GetOrdinal("bz")) ? "" : reader.GetString(reader.GetOrdinal("bz"));

                    stuFinalList.Add(sf);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("获取学生最终名单错误：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return stuFinalList;
        }

        /// <summary>
        /// 获得性别码内容，xwzs_dm_xb中的内容
        /// </summary>
        /// <returns>返回性别链表</returns>
        public static List<Xb> getDmXb()
        {
            List<Xb> xbList = new List<Xb>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmXb";

            cmd.Parameters.Add("dm_xb", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Xb x = new Xb();
                    x.xb = reader.GetString(reader.GetOrdinal("xb"));
                    x.xbm = reader.GetString(reader.GetOrdinal("xbm"));
                    xbList.Add(x);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return xbList;
        }

        /// <summary>
        /// 获得名族码内容,xwzs_dm_mz
        /// </summary>
        /// <returns>返回民族链表</returns>
        public static List<Mz> getDmMz()
        {
            List<Mz> mzList = new List<Mz>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmMz";

            cmd.Parameters.Add("dm_mz", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mz x = new Mz();
                    x.mz = reader.GetString(reader.GetOrdinal("mz"));
                    x.mzm = reader.GetString(reader.GetOrdinal("mzm"));
                    mzList.Add(x);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return mzList;
        }

        /// <summary>
        /// 获得政治面貌码内容，xwzs_dm_zzmm
        /// </summary>
        /// <returns>返回政治面貌链表</returns>
        public static List<Zzmm> getDmZzmm()
        {
            List<Zzmm> zzmmList = new List<Zzmm>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmZzmm";

            cmd.Parameters.Add("dm_zzmm", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Zzmm x = new Zzmm();
                    x.zzmmm = reader.GetString(reader.GetOrdinal("zzmmm"));
                    x.zzmm = reader.GetString(reader.GetOrdinal("zzmm"));
                    zzmmList.Add(x);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return zzmmList;
        }

        /// <summary>
        /// 获得学位类别代码，xwzs_dm_xwlb
        /// </summary>
        /// <returns>学位类别链表</returns>
        public static List<Xwlb> getDmXwlb()
        {
            List<Xwlb> xwlbList = new List<Xwlb>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmXwlb";

            cmd.Parameters.Add("dm_zzmm", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Xwlb x = new Xwlb();
                    x.xwlb = reader.GetString(reader.GetOrdinal("xwlb"));
                    x.xwlbm = reader.GetString(reader.GetOrdinal("xwlbm"));
                    x.xwlbold = reader.IsDBNull(reader.GetOrdinal("xwlbold"))? "":reader.GetString(reader.GetOrdinal("xwlbold"));
                    xwlbList.Add(x);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return xwlbList;
        }

        /// <summary>
        /// 获得专业名称代码，xwzs_dm_zymc,该表从历次上报表中得来
        /// </summary>
        /// <returns>专业名称链表</returns>
        public static List<Zymc> getDmZymc()
        {
            List<Zymc> zymcList = new List<Zymc>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmZymc";

            cmd.Parameters.Add("dm_zymc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Zymc z = new Zymc();
                    z.zydm = reader.GetString(reader.GetOrdinal("zydm"));
                    z.zymc = reader.GetString(reader.GetOrdinal("zymc"));
                    z.zszymc = reader.IsDBNull(reader.GetOrdinal("zszymc")) ? "" : reader.GetString(reader.GetOrdinal("zszymc"));
                    zymcList.Add(z);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return zymcList;
        }

        /// <summary>
        /// 获得原始专业名称代码，xwzs_dm_zymc_init
        /// </summary>
        /// <returns>专业名称链表</returns>
        public static List<ZymcInit> getDmZymcInit()
        {
            List<ZymcInit> ziList = new List<ZymcInit>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getDmZymcInit";

            cmd.Parameters.Add("dm_zymcInit", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ZymcInit zi = new ZymcInit();
                    zi.zydm = reader.GetString(reader.GetOrdinal("zydm"));
                    zi.zymc = reader.GetString(reader.GetOrdinal("zymc"));
                    ziList.Add(zi);
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("读取代码表出错。" + oe);
            }
            finally
            {
                conn.Close();
            }

            return ziList;
        }

        /// <summary>
        /// 检测上报名单（xwzs_final）中，民族是否都在民族代码表中。
        /// </summary>
        /// <returns>布尔值，true为正确，false存在不正确字段</returns>
        public static bool testMz()
        {
            bool testMz = false;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.testFieldMz";

            cmd.Parameters.Add("testresult", OracleDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                //如果检测所有学生的mz都在dm_mz中，则无异常的民族字段
                if (Convert.ToInt32(cmd.Parameters["testresult"].Value.ToString()) == 0)
                {
                    testMz = true;
                }
                else
                {
                    testMz = false;
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("检测民族字段出错：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return testMz;
        }

        /// <summary>
        /// 检测上报名单（xwzs_final）中，政治面貌是否都在代码表中。
        /// </summary>
        /// <returns>布尔值，true为正确，false存在不正确字段</returns>
        public static bool testZzmm()
        {
            bool tz = false;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.testFieldZzmm";

            cmd.Parameters.Add("testresult", OracleDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                //如果检测所有学生的zzmm都在dm_zzmm中，则无异常的民族字段
                if (Convert.ToInt32(cmd.Parameters["testresult"].Value.ToString()) == 0)
                {
                    tz = true;
                }
                else
                {
                    tz = false;
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("检测政治面貌字段出错：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return tz;
        }

        /// <summary>
        /// 检测上报名单（xwzs_final）中，自设专业名称是否都在代码表中。
        /// </summary>
        /// <returns>布尔值，true为正确，false存在不正确字段</returns>
        public static bool testZymc()
        {
            bool tz = false;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.testFieldZymc";

            cmd.Parameters.Add("testresult", OracleDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                //如果检测所有学生的zszymc都在dm_zymc中，则无异常的民族字段
                if (Convert.ToInt32(cmd.Parameters["testresult"].Value.ToString()) == 0)
                {
                    tz = true;
                }
                else
                {
                    tz = false;
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("检测专业名称出错：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return tz;
        }

        /// <summary>
        /// 获得自设专业名称不在代码表（xwzs_dm_zymc）中的专业
        /// </summary>
        /// <returns>返回不在代码表中的自设专业名称表</returns>
        public static List<Zymc> getUnmatchedZymcList()
        {
            List<Zymc> zl = new List<Zymc>();

            OracleConnection conn = getConnection();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = "xwsbPackage.getUnmatchedZymcList";

            cmd.Parameters.Add("unmatchlist", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Zymc z = new Zymc();
                    z.zszymc = rd.GetString(rd.GetOrdinal("zszymc"));
                    zl.Add(z);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("查找不匹配的专业名称错误：" + oe);
            }finally
            {
                conn.Close();
            }

            return zl;

        }

        /// <summary>
        /// 为不在代码表中的自设专业名称确定专业名称、专业代码，然后添加到代码表中xwzs_dm_zymc
        /// </summary>
        /// <param name="unmatchedZymcList">不在专业代码表中的自设专业名称，确定了其专业名称和专业代码的链表</param>
        public static void setDmZymc(List<Zymc> unmatchedZymcList)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd =  new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.setDmZymc";

            try
            {
                conn.Open();
                for(int i= 0;i<unmatchedZymcList.Count;i++)
                {
                    Zymc z = unmatchedZymcList[i];
                        cmd.Parameters.Add("zydm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                        cmd.Parameters["zydm"].Value = z.zydm;
                        cmd.Parameters.Add("zymc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                        cmd.Parameters["zymc"].Value = z.zymc;
                        cmd.Parameters.Add("zszymc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                        cmd.Parameters["zszymc"].Value = z.zszymc;

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                }
            }
            catch(OracleException oe)
            {
                System.Windows.MessageBox.Show("更新上报名单中中专业名称和代码出错：" + oe) ;
            }finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 不在专业代码表中的自设专业名称的专业名称和专业代码，在窗口关闭时没有全部填写完成，则需要删除已填写的部分
        /// </summary>
        /// <param name="unmatchedZymcList">不在专业代码表中的自设专业名称</param>
        public static void deleteUnmatchedZymc(List<Zymc> unmatchedZymcList)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.deleteUnmatchedZymc";

            try
            {
                conn.Open();
                for (int i = 0; i < unmatchedZymcList.Count; i++)
                {
                    Zymc z = unmatchedZymcList[i];
                    cmd.Parameters.Add("zszymc", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["zszymc"].Value = z.zszymc;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("删除代码表中还没有确定专业代码的记录，出错：" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获得民族不在民族代码表中字段内容
        /// </summary>
        /// <returns>不匹配的民族字段</returns>
        public static List<FailMz> getUnmatchedMzList()
        {
            List<FailMz> fmList = new List<FailMz>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getUnmatchedMzList";

            cmd.Parameters.Add("failMz", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    FailMz fm = new FailMz();
                    fm.oldMz = rd.IsDBNull(rd.GetOrdinal("mz"))? "": rd.GetString(rd.GetOrdinal("mz"));
                    fm.newMz = "";
                    fmList.Add(fm);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("查找不匹配的名族列表出错：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return fmList;
        }

        /// <summary>
        /// 更新上报名单中的民族字段，使其在民族代码表中
        /// </summary>
        /// <param name="fzlist">包含旧的民族内容和新的民族内容的链表</param>
        public static void refreshXwzsMz(List<FailMz> fzlist)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.refreshXwzsMz";

            try
            {
                conn.Open();
                for (int i = 0; i < fzlist.Count; i++)
                {
                    FailMz fz = fzlist[i];

                    cmd.Parameters.Add("NewMz", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["NewMz"].Value = fz.newMz;
                    cmd.Parameters.Add("OldMz", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["OldMz"].Value = fz.oldMz;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("刷新学位上报最终名单xwzs_final民族字段，时出错:+" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获得政治面貌不在代码表中的字段内容
        /// </summary>
        /// <returns>写法不在代码表中的政治面貌</returns>
        public static List<FailZzmm> getUnmatchedZzmmList()
        {
            List<FailZzmm> fzList = new List<FailZzmm>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.getUnmatchedZzmmList";

            cmd.Parameters.Add("failZzmm", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    FailZzmm fz = new FailZzmm();
                    fz.oldZzmm = rd.IsDBNull(rd.GetOrdinal("zzmm")) ? "" : rd.GetString(rd.GetOrdinal("zzmm"));
                    fz.newZzmm = "";
                    fzList.Add(fz);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("查找不匹配的民族列表出错：" + oe);
            }
            finally
            {
                conn.Close();
            }

            return fzList;
        }

        /// <summary>
        /// 更新上报名单中的政治面貌字段，使其在代码表中
        /// </summary>
        /// <param name="fzlist">包含旧的政治面貌内容和新的政治面貌内容的链表</param>
        public static void refreshXwzsZzmm(List<FailZzmm> fzlist)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.refreshXwzsZzmm";

            try
            {
                conn.Open();
                for (int i = 0; i < fzlist.Count; i++)
                {
                    FailZzmm fz = fzlist[i];

                    cmd.Parameters.Add("newZzmm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["newZzmm"].Value = fz.newZzmm;
                    cmd.Parameters.Add("oldZzmm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["oldZzmm"].Value = fz.oldZzmm;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("刷新学位上报最终名单xwzs_final的政治面貌字段，时出错:+" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 清空所有获学位证书字段内容
        /// </summary>
        public static void cleanAllHxwrq()
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.cleanHxwrq";

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("清空获学位日期出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 统一设置获学位证书日期
        /// </summary>
        /// <param name="hxwrq">用户输入的获学位证书日期</param>
        public static void setAllHxwrq(string hxwrq)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.setAllHxwrq";

            cmd.Parameters.Add("hxwrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["hxwrq"].Value = hxwrq;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("统一设置获学位日期出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 分批填写获学位证书名单时，将excel导入数据库中xwzs_hxwrqmd中
        /// </summary>
        /// <param name="path">excel文件路径</param>
        /// <returns>是否导入成功</returns>
        public static bool loadHxwrqMd(string path)
        {
            bool success = false;

            string excelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+path+";Extended Properties='Excel 8.0;HDR=NO;IMEX=1; '";
            OleDbConnection excConn = new OleDbConnection(excelConnStr);
            string strCom = "SELECT * FROM [SQL Results$]";

            OracleConnection conn = getConnection();

            //清空存放上传名单的列表
            OracleCommand cleanTabCmd = new OracleCommand();
            cleanTabCmd.Connection = conn;
            cleanTabCmd.CommandType = CommandType.StoredProcedure;
            cleanTabCmd.CommandText = "xwsbPackage.cleanTab";

            cleanTabCmd.Parameters.Add("tableName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cleanTabCmd.Parameters["tableName"].Value = "xwzs_hxwrqmd";
            try
            {
                conn.Open();
                cleanTabCmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("清空xwzs_hxwrqmd出错：" + oe);
                success = false;
            }
            finally
            {
                conn.Close();
            }//清空xwzs_init


            //读excel，存储到数据库xwzs_hxwrqmd中
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure; //调用存储过程
            cmd.CommandText = "xwsbPackage.inertHxwrqMd";
            try
            {
                excConn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(strCom, excConn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                conn.Open();

                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    //设置参数
                    cmd.Parameters.Add("v_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["v_id"].Value = row[0];
                    cmd.Parameters.Add("v_xh", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["v_xh"].Value = row[1];
                    cmd.Parameters.Add("v_xm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["v_xm"].Value = row[2];

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear(); //很重要，因为插入不同记录时，使用的是同一个cmd，因此需要清空参数！！
                }

                 success = true;
            }
            catch (OleDbException ode)
            {
                System.Windows.MessageBox.Show("访问excel出错，请使用excel2003版本，并删除标题行" + ode);
                success = false;
            } catch (OracleException ex)
            {
                System.Windows.MessageBox.Show("txt文件存入数据库出错:" + ex);
                success = false;
            }

            finally
            {
                excConn.Close();
            }

            return success;

            //读.csv格式的数据问题
            //OracleConnection conn = getConnection();

            ////清空存放上传名单的列表
            //OracleCommand cleanTabCmd = new OracleCommand();
            //cleanTabCmd.Connection = conn;
            //cleanTabCmd.CommandType = CommandType.StoredProcedure;
            //cleanTabCmd.CommandText = "xwsbPackage.cleanTab";

            //cleanTabCmd.Parameters.Add("tableName", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            //cleanTabCmd.Parameters["tableName"].Value = "xwzs_hxwrqmd";
            //try
            //{
            //    conn.Open();
            //    cleanTabCmd.ExecuteNonQuery();
            //}
            //catch (OracleException oe)
            //{
            //    System.Windows.MessageBox.Show("清空xwzs_hxwrqmd出错：" + oe);
            //    success = false;
            //}
            //finally
            //{
            //    conn.Close();
            //}//清空xwzs_init

            ////读取txt文件，存储到数据库xwzs_init中
            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = conn;
            //cmd.CommandType = CommandType.StoredProcedure; //调用存储过程
            //cmd.CommandText = "xwsbPackage.inertHxwrqMd";

            //try
            //{
            //    StreamReader sr = new StreamReader(path, Encoding.GetEncoding("gb2312"));
            //    //数据流都txt文件，设置编码方式，以防乱码
            //    while (!sr.EndOfStream)
            //    {
            //        //读txt文件
            //        string strReadLine = sr.ReadLine();
            //        string[] strArray = strReadLine.Split(',');
            //        //System.Windows.MessageBox.Show(strArray.Length.ToString());
            //        //for (int i = 0; i < strArray.Length; i++)
            //        //    System.Windows.MessageBox.Show(strArray[i]);

            //        //存入数据库中
            //        try
            //        {
            //            conn.Open();

            //            //设置参数
            //            cmd.Parameters.Add("v_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            //            cmd.Parameters["v_id"].Value = strArray[0];
            //            cmd.Parameters.Add("v_xh", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            //            cmd.Parameters["v_xh"].Value = strArray[1];
            //            cmd.Parameters.Add("v_xm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            //            cmd.Parameters["v_xm"].Value = strArray[2];
                        
            //            cmd.ExecuteNonQuery();
            //            cmd.Parameters.Clear(); //很重要，因为插入不同记录时，使用的是同一个cmd，因此需要清空参数！！

            //            success = true;

            //        }
            //        catch (OracleException ex)
            //        {
            //            System.Windows.MessageBox.Show("txt文件存入数据库出错:" + ex);
            //            success = false;
            //        }
            //        finally
            //        {
            //            conn.Close();
            //        }//将txt每条记录插入xwzs_init中
            //    }//while end

            //}
            //catch (ArgumentException ae)
            //{
            //    System.Windows.MessageBox.Show("读取txt数据流参数错误:" + ae);
            //    success = false;
            //}
            //catch (IOException ioe)
            //{
            //    System.Windows.MessageBox.Show("读取txt数据流IO出错:" + ioe);
            //    success = false;

            //}
            //catch (NotSupportedException nse)
            //{
            //    System.Windows.MessageBox.Show("读取txt,类型不支持错误:" + nse);
            //    success = false;
            //}
            //catch (ObjectDisposedException ode)
            //{
            //    System.Windows.MessageBox.Show("读取txt, while (!sr.EndOfStream) 出错:" + ode);
            //    success = false;
            //}
            //catch (OutOfMemoryException ome)
            //{
            //    System.Windows.MessageBox.Show("Readline出错，内存不够：" + ome);
            //    success = false;
            //}
            //finally
            //{
            //    conn.Close(); 
            //}
            //return success;
        }

        /// <summary>
        /// 分批设定获学位日期时，设定导入名单（xwzs_hxwrqmd）的学生的获学位日期
        /// </summary>
        /// <param name="hxwrq">这批学生的获学位日期</param>
        public static void setHxwrqPart1(string hxwrq)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.setHxwrqPart1";

            cmd.Parameters.Add("hxwrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["hxwrq"].Value = hxwrq;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("按名单设置获学位日期出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 分批设定获学位日期，按打印时间和学号开头设定
        /// </summary>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="xhStart">学号开头</param>
        /// <param name="hxwrq">获学位日期</param>
        public static void setHxwrqPart2(string startTime, string endTime, string xhStart,string hxwrq)
        {
            xhStart += "%";

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.setHxwrqPart2";

            cmd.Parameters.Add("startTime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["startTime"].Value = startTime;
            cmd.Parameters.Add("endTime", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["endTime"].Value = endTime;
            cmd.Parameters.Add("xhStart", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["xhStart"].Value = xhStart;
            cmd.Parameters.Add("hxwrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["hxwrq"].Value = hxwrq;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("按打印时间和学号设置获学位日期出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 剩余部分学生的获学位日期
        /// </summary>
        /// <param name="hxwrq">获学位日期</param>
        public static void setHxwrqPart3(string hxwrq)
        {
            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.setHxwrqPart3";

            cmd.Parameters.Add("hxwrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["hxwrq"].Value = hxwrq;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("设置剩余学生的获学位日期出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 填充学位上报名单
        /// </summary>
        /// <param name="zjlxm">证件类型码</param>
        /// <param name="zjlx">证件类型</param>
        /// <param name="pydwm">培养单位码</param>
        /// <param name="pydw">培养单位</param>
        /// <param name="xzxm">校长姓名</param>
        /// <param name="zxxm">主席姓名</param>
        /// <returns>是否填充成功</returns>
        public static bool fillXwzs(string zjlxm, string zjlx,string pydwm, string pydw, string xzxm, string zxxm)
        {
            bool success = false;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.fillXwzs";

            cmd.Parameters.Add("zjlxm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["zjlxm"].Value = zjlxm;
            cmd.Parameters.Add("zjlx", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["zjlx"].Value = zjlx;
            cmd.Parameters.Add("pydwm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["pydwm"].Value = pydwm;
            cmd.Parameters.Add("pydw", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["pydw"].Value = pydw;
            cmd.Parameters.Add("xzxm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["xzxm"].Value = xzxm;
            cmd.Parameters.Add("zxxm", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["zxxm"].Value = zxxm;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("填充学位上报信息错误：" + oe);
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// 将学位上报的名单导出到DBF中
        /// </summary>
        /// <param name="path">DBF数据库所在的文件路径</param>
        /// <param name="fileName">数据表名</param>
        /// <param name="dataSet">要导出的名单链表</param>
        /// <returns>是否导出成功</returns>
        public static bool importDataIntoDBF(string path, string fileName, List<StuFinal> sflist)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBASE IV;";

            string filePath = path + "\\" + fileName;

            if (File.Exists(filePath))
            {

                System.IO.File.Delete(filePath);

            }

            string tableName = fileName.Substring(0, fileName.LastIndexOf("."));

            string createSql = "create table " + tableName + @" (xm varchar(50),xmpy varchar(50),xbm varchar(5), xb varchar(10),
                                mzm varchar(5), mz varchar(20), zzmmm varchar(5), zzmm varchar(30),csrq varchar(30), zjlxm varchar(10),
                                zjlx varchar(50), zjhm varchar(50), xwsydwm varchar(10), xwsydw varchar(50),xzxm varchar(30),
                                zxxm varchar(30), pydwm varchar(10), pydw varchar(50), xwlbm varchar(30), xwlb varchar(50),
                                zydm varchar(30), zymc varchar(100), zszymc varchar(100), ksh varchar(50), rxny varchar(20),
                                xh varchar(30), xz varchar(5), byny varchar(20), hxwrq varchar(20), sfdexw varchar(5), 
                                sffxxw varchar(5), xwzsbh varchar(30),zp varchar(30),bz varchar(30))";


            OleDbConnection con = new OleDbConnection(strConn);

            OleDbCommand cmd = new OleDbCommand();

            try
            {

                cmd.Connection = con;

                con.Open();

                cmd.CommandText = createSql;

                cmd.ExecuteNonQuery();//创建新表

                for (int i = 0; i < sflist.Count; i++)
                {
                    string insertSql = @"insert into " + tableName + " values( '" + sflist[i].xm + "', '" + sflist[i].xmpy + "','"
                        + sflist[i].xbm + "','"+ sflist[i].xb + "', '" + sflist[i].mzm  + "', '"+sflist[i].mz  + "', '" + sflist[i].zzmmm 
                        + "', '" + sflist[i].zzmm + "','" + sflist[i].csrq + "', '" + sflist[i].zjlxm 
                        + "', '" + sflist[i].zjlx  + "', '" + sflist[i].zjhm +  "', '" + sflist[i].xwsydwm 
                        + "', '" + sflist[i].xwsydw + "','" + sflist[i].xzxm + "','" + sflist[i].zxxm 
                        + "', '" + sflist[i].pydwm + "','" + sflist[i].pydw + "','" + sflist[i].xwlbm 
                        + "', '" + sflist[i].xwlb + "','" + sflist[i].zydm + "','" + sflist[i].zymc 
                        + "', '" + sflist[i].zszymc + "','" + sflist[i].ksh + "','" +  sflist[i].rxny 
                        + "', '" + sflist[i].xh + "','"  + sflist[i].xz + "','" + sflist[i].byny 
                        + "', '" + sflist[i].hxwrq + "','" +  sflist[i].sfdexw
                        + "', '" + sflist[i].sffxxw + "','" + sflist[i].xwzsbh + "','" + sflist[i].zp 
                        + "', '" + sflist[i].bz + "')";

                    cmd.CommandText = insertSql;

                    cmd.ExecuteNonQuery();
                }

                return true;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("保存为dbf文件出错：" + ex);
                return false;

            }
            finally
            {
                con.Close();
                
            }
        }

        /// <summary>
        /// 历次上报名单（dbf）存入数据库中（xwzs_pre）
        /// </summary>
        /// <param name="filePath">dbf文件路径</param>
        /// <returns>是否导入成功</returns>
        public static bool preXwzsToDB(string filePath)
        {
            bool success = false;

            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            string sbrq = fileName.Substring(0, fileName.IndexOf(".DBF"));

            //Oracle数据库的连接
            OracleConnection conn = getConnection();
            //检查数据库中表是否存在的cmd
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            ////将dbf中文件存储到oracle数据库中的cmd
            //OracleCommand insertCmd = new OracleCommand();
            //insertCmd.Connection = conn;

            //访问dbf的连接
            FileInfo fi = new FileInfo(filePath);
            string mulu = fi.DirectoryName;
            string filename = fi.Name;
            string table = filePath;
            string dbfConnStr = "Provider=VFPOLEDB.1;Data Source=" + mulu + ";Collating Sequence=MACHINE";
            OleDbConnection oleConn = new OleDbConnection(dbfConnStr);

            try
            {
                conn.Open();
                string tableExistSql = "select count(*) from user_tables where table_name = upper('xwzs_pre')";
                cmd.CommandText = tableExistSql;
                int tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (tableCount == 1)
                {
                    //System.Windows.MessageBox.Show("表存在");
                    string dropTableSql = "delete from xwzs_pre where sbrq = '" + sbrq + "'";
                    cmd.CommandText = dropTableSql;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    //表不存在时创建新表
                    string createTableSql = @"create table xwzs_pre  
(
  XM      VARCHAR2(40),
  XMPY    VARCHAR2(60),
  XBM     VARCHAR2(10),
  XB      VARCHAR2(10),
  MZM     VARCHAR2(10),
  MZ      VARCHAR2(30),
  ZZMMM   VARCHAR2(10),
  ZZMM    VARCHAR2(50),
  CSRQ    VARCHAR2(30),
  ZJLXM   VARCHAR2(10),
  ZJLX    VARCHAR2(50),
  ZJHM    VARCHAR2(50),
  XWSYDWM VARCHAR2(10),
  XWSYDW  VARCHAR2(50),
  XZXM    VARCHAR2(40),
  ZXXM    VARCHAR2(40),
  PYDWM   VARCHAR2(20),
  PYDW    VARCHAR2(50),
  XWLBM   VARCHAR2(10),
  XWLB    VARCHAR2(50),
  ZYDM    VARCHAR2(20),
  ZYMC    VARCHAR2(100),
  ZSZYMC  VARCHAR2(100),
  KSH     VARCHAR2(50),
  RXNY    VARCHAR2(20),
  XH      VARCHAR2(50),
  XZ      VARCHAR2(10),
  BYNY    VARCHAR2(20),
  HXWRQ   VARCHAR2(20),
  SFDEXW  VARCHAR2(10),
  SFFXXW  VARCHAR2(10),
  XWZSBH  VARCHAR2(50),
  ZP      VARCHAR2(100),
  BZ      VARCHAR2(160),
 SBRQ      VARCHAR2(50)
)";
                    cmd.CommandText = createTableSql;
                    cmd.ExecuteNonQuery();
                }

                //读dbf数据再存入oracle数据表中
                string sql = @"select * from " + sbrq;

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, oleConn);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    //将每行数据存到Oracle数据库中
                    cmd.Parameters.Add(":xm", OracleDbType.Varchar2).Value = row[0];
                    cmd.Parameters.Add(":xmpy", OracleDbType.Varchar2).Value = row[1];
                    cmd.Parameters.Add(":xbm", OracleDbType.Varchar2).Value = row[2];
                    cmd.Parameters.Add(":xb", OracleDbType.Varchar2).Value = row[3];
                    cmd.Parameters.Add(":mzm", OracleDbType.Varchar2).Value = row[4];
                    cmd.Parameters.Add(":mz", OracleDbType.Varchar2).Value = row[5];
                    cmd.Parameters.Add(":zzmmm", OracleDbType.Varchar2).Value = row[6];
                    cmd.Parameters.Add(":zzmm", OracleDbType.Varchar2).Value = row[7];
                    cmd.Parameters.Add(":csrq", OracleDbType.Varchar2).Value = row[8];
                    cmd.Parameters.Add(":zjlxm", OracleDbType.Varchar2).Value = row[9];
                    cmd.Parameters.Add(":zjlx", OracleDbType.Varchar2).Value = row[10];
                    cmd.Parameters.Add(":zjhm", OracleDbType.Varchar2).Value = row[11];
                    cmd.Parameters.Add(":xwsydwm", OracleDbType.Varchar2).Value = row[12];
                    cmd.Parameters.Add(":xwsydw", OracleDbType.Varchar2).Value = row[13];
                    cmd.Parameters.Add(":xzxm", OracleDbType.Varchar2).Value = row[14];
                    cmd.Parameters.Add(":zxxm", OracleDbType.Varchar2).Value = row[15];
                    cmd.Parameters.Add(":pydwm", OracleDbType.Varchar2).Value = row[16];
                    cmd.Parameters.Add(":pydw", OracleDbType.Varchar2).Value = row[17];
                    cmd.Parameters.Add(":xwlbm", OracleDbType.Varchar2).Value = row[18];
                    cmd.Parameters.Add(":xwlb", OracleDbType.Varchar2).Value = row[19];
                    cmd.Parameters.Add(":zydm", OracleDbType.Varchar2).Value = row[20];
                    cmd.Parameters.Add(":zymc", OracleDbType.Varchar2).Value = row[21];
                    cmd.Parameters.Add(":zszymc", OracleDbType.Varchar2).Value = row[22];
                    cmd.Parameters.Add(":ksh", OracleDbType.Varchar2).Value = row[23];
                    cmd.Parameters.Add(":rxny", OracleDbType.Varchar2).Value = row[24];
                    cmd.Parameters.Add(":xh", OracleDbType.Varchar2).Value = row[25];
                    cmd.Parameters.Add(":xz", OracleDbType.Varchar2).Value = row[26];
                    cmd.Parameters.Add(":byny", OracleDbType.Varchar2).Value = row[27];
                    cmd.Parameters.Add(":hxwrq", OracleDbType.Varchar2).Value = row[28];
                    cmd.Parameters.Add(":sfdexw", OracleDbType.Varchar2).Value = row[29];
                    cmd.Parameters.Add(":sffxxw", OracleDbType.Varchar2).Value = row[30];
                    cmd.Parameters.Add(":xwzsbh", OracleDbType.Varchar2).Value = row[31];
                    cmd.Parameters.Add(":zp", OracleDbType.Varchar2).Value = row[32];
                    cmd.Parameters.Add(":bz", OracleDbType.Varchar2).Value = row[33];
                    cmd.Parameters.Add(":sbrq", OracleDbType.Varchar2).Value = sbrq;

                    string insertSql = @"insert into xwzs_pre values(trim(:xm),trim(:xmpy),trim(:xbm),trim(:xb),
                                        trim(:mzm),trim(:mz),trim(:zzmmm),trim(:zzmm),trim(:csrq),trim(:zjlxm),trim(:zjlx),trim(:zjhm),
                                        trim(:xwsydwm),trim(:xwsydw),trim(:xzxm),trim(:zxxm),trim(:pydwm),trim(:pydw),trim(:xwlbm),
                                        trim(:xwlb),trim(:zydm),trim(:zymc),trim(:zszymc),trim(:ksh),trim(:rxny),trim(:xh),trim(:xz),
                                        trim(:byny),trim(:hxwrq),trim(:sfdexw),trim(:sffxxw),trim(:xwzsbh),trim(:zp),trim(:bz),:sbrq)";
                  
                    cmd.CommandText = insertSql;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    success = true;
                }


            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("历年数据导入数据库出错。" + oe);
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// 获得历次学位上报名单
        /// </summary>
        /// <param name="sbrq">历次上报日期</param>
        /// <returns>返回学生名单链表</returns>
        public static List<StuFinal> getPastXwsb(string sbrq)
        {
            List<StuFinal> pastXwsbList = new List<StuFinal>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from xwzs_pre where sbrq = '" + sbrq + "'";

            try
            {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StuFinal sf = new StuFinal();

                    sf.xm = reader.IsDBNull(reader.GetOrdinal("xm")) ? "" : reader.GetString(reader.GetOrdinal("xm"));
                    sf.xmpy = reader.IsDBNull(reader.GetOrdinal("xmpy")) ? "" : reader.GetString(reader.GetOrdinal("xmpy"));
                    sf.xbm = reader.IsDBNull(reader.GetOrdinal("xbm")) ? "" : reader.GetString(reader.GetOrdinal("xbm"));
                    sf.xb = reader.IsDBNull(reader.GetOrdinal("xb")) ? "" : reader.GetString(reader.GetOrdinal("xb"));
                    sf.mzm = reader.IsDBNull(reader.GetOrdinal("mzm")) ? "" : reader.GetString(reader.GetOrdinal("mzm"));
                    sf.mz = reader.IsDBNull(reader.GetOrdinal("mz")) ? "" : reader.GetString(reader.GetOrdinal("mz"));
                    sf.zzmmm = reader.IsDBNull(reader.GetOrdinal("zzmmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmmm"));
                    sf.zzmm = reader.IsDBNull(reader.GetOrdinal("zzmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmm"));
                    sf.csrq = reader.IsDBNull(reader.GetOrdinal("csrq")) ? "" : reader.GetString(reader.GetOrdinal("csrq"));
                    sf.zjlxm = reader.IsDBNull(reader.GetOrdinal("zjlxm")) ? "" : reader.GetString(reader.GetOrdinal("zjlxm"));
                    sf.zjlx = reader.IsDBNull(reader.GetOrdinal("zjlx")) ? "" : reader.GetString(reader.GetOrdinal("zjlx"));
                    sf.zjhm = reader.IsDBNull(reader.GetOrdinal("zjhm")) ? "" : reader.GetString(reader.GetOrdinal("zjhm"));
                    sf.xwsydwm = reader.IsDBNull(reader.GetOrdinal("xwsydwm")) ? "" : reader.GetString(reader.GetOrdinal("xwsydwm"));
                    sf.xwsydw = reader.IsDBNull(reader.GetOrdinal("xwsydw")) ? "" : reader.GetString(reader.GetOrdinal("xwsydw"));
                    sf.xzxm = reader.IsDBNull(reader.GetOrdinal("xzxm")) ? "" : reader.GetString(reader.GetOrdinal("xzxm"));
                    sf.zxxm = reader.IsDBNull(reader.GetOrdinal("zxxm")) ? "" : reader.GetString(reader.GetOrdinal("zxxm"));
                    sf.pydwm = reader.IsDBNull(reader.GetOrdinal("pydwm")) ? "" : reader.GetString(reader.GetOrdinal("pydwm"));
                    sf.pydw = reader.IsDBNull(reader.GetOrdinal("pydw")) ? "" : reader.GetString(reader.GetOrdinal("pydw"));
                    sf.xwlbm = reader.IsDBNull(reader.GetOrdinal("xwlbm")) ? "" : reader.GetString(reader.GetOrdinal("xwlbm"));
                    sf.xwlb = reader.IsDBNull(reader.GetOrdinal("xwlb")) ? "" : reader.GetString(reader.GetOrdinal("xwlb"));
                    sf.zydm = reader.IsDBNull(reader.GetOrdinal("zydm")) ? "" : reader.GetString(reader.GetOrdinal("zydm"));
                    sf.zymc = reader.IsDBNull(reader.GetOrdinal("zymc")) ? "" : reader.GetString(reader.GetOrdinal("zymc"));
                    sf.zszymc = reader.IsDBNull(reader.GetOrdinal("zszymc")) ? "" : reader.GetString(reader.GetOrdinal("zszymc"));
                    sf.ksh = reader.IsDBNull(reader.GetOrdinal("ksh")) ? "" : reader.GetString(reader.GetOrdinal("ksh"));
                    sf.rxny = reader.IsDBNull(reader.GetOrdinal("rxny")) ? "" : reader.GetString(reader.GetOrdinal("rxny"));
                    sf.xh = reader.IsDBNull(reader.GetOrdinal("xh")) ? "" : reader.GetString(reader.GetOrdinal("xh"));
                    sf.xz = reader.IsDBNull(reader.GetOrdinal("xz")) ? "" : reader.GetString(reader.GetOrdinal("xz"));
                    sf.byny = reader.IsDBNull(reader.GetOrdinal("byny")) ? "" : reader.GetString(reader.GetOrdinal("byny"));
                    sf.hxwrq = reader.IsDBNull(reader.GetOrdinal("hxwrq")) ? "" : reader.GetString(reader.GetOrdinal("hxwrq"));
                    sf.sfdexw = reader.IsDBNull(reader.GetOrdinal("sfdexw")) ? "" : reader.GetString(reader.GetOrdinal("sfdexw"));
                    sf.sffxxw = reader.IsDBNull(reader.GetOrdinal("sffxxw")) ? "" : reader.GetString(reader.GetOrdinal("sffxxw"));
                    sf.xwzsbh = reader.IsDBNull(reader.GetOrdinal("xwzsbh")) ? "" : reader.GetString(reader.GetOrdinal("xwzsbh"));
                    sf.zp = reader.IsDBNull(reader.GetOrdinal("zp")) ? "" : reader.GetString(reader.GetOrdinal("zp"));
                    sf.bz = reader.IsDBNull(reader.GetOrdinal("bz")) ? "" : reader.GetString(reader.GetOrdinal("bz"));
                    pastXwsbList.Add(sf);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("获取数据库中历年学生名单出错！" + oe);
            }
            finally
            {
                conn.Close();
            }
            return pastXwsbList;
        }

        /// <summary>
        /// 获得数据库中存储的不同上报时间
        /// </summary>
        /// <returns>所有上报时间链表</returns>
        public static List<string> getPastXwsbTableName()
        {
            List<string> pastXwsbTableName = new List<string>();

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select distinct sbrq from xwzs_pre";

            try {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string s = reader.GetString(0);
                    pastXwsbTableName.Add(s);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("初始化历年学位上报名单出错。" + oe);
            }
            finally
            {
                conn.Close();
            }
            return pastXwsbTableName;
        }

        /// <summary>
        /// 用历次上报名单的专业代码、专业名称、自设专业名称，刷新专业代码表
        /// </summary>
        /// <param name="tableName">历次上报名单的时间</param>
        /// <returns>专业代码表的更新条数</returns>
        public static int refreshDmZymc(string tableName)
        {
            int refreshCount = 0;

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.refreshDmZymc";

            cmd.Parameters.Add("v_sbrq", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters["v_sbrq"].Value = tableName;
            cmd.Parameters.Add("v_count", OracleDbType.Int32).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                refreshCount =Convert.ToInt32(cmd.Parameters["v_count"].Value.ToString());

            }catch(OracleException oe)
            {
                System.Windows.MessageBox.Show("更新专业代码表出错:" + oe);
            }finally
            {
                conn.Close();
            }
            return refreshCount;
        }

        /// <summary>
        /// 检测程序运行时所需的临时表是否在数据库中。如果不存在，则创建表
        /// xwzs_init:存储刚导入的所有学生名单，从中可以进行筛选
        /// xwzs_init_temp:存储进行筛选过的学生名单，确定人数后，将该表中的数据存入xwzs_final
        /// xwzs_final:含最终字段的学生名单，xwzs_final时xwzs_init与zfxfzb.xsjbxxb关联后的表
        /// xwzs_hxwrqmd:获学位日期名单，设置获学位证书时，可以上传名单来确定获学位日期。
        /// </summary>
        public static void TempTableInit()
        {
            string s_xwzsInitCount = "select count(*) from user_tables where TABLE_NAME = upper('xwzs_init')";
            string s_xwzsInitTempCount = "select count(*) from user_tables where TABLE_NAME = upper('xwzs_init_temp')";
            string s_xwzsFinalCount = "select count(*) from user_tables where TABLE_NAME = upper('xwzs_final')";
            string s_xwzsHxwrqMdCount = "select count(*) from user_tables where TABLE_NAME = upper('xwzs_hxwrqmd')";

            string s_createXwzsInit = @"create table XWZS_INIT
(
  ID          VARCHAR2(20),
  ZSLX        VARCHAR2(10),
  ZSBH        VARCHAR2(17),
  YXSDM       VARCHAR2(50),
  DWDM        VARCHAR2(5),
  DWMC        VARCHAR2(100),
  XKMLM       VARCHAR2(2),
  XKML        VARCHAR2(24),
  YJXKDM      VARCHAR2(4),
  YJXK        VARCHAR2(24),
  ZYDM        VARCHAR2(8),
  ZYMC        VARCHAR2(50),
  XH          VARCHAR2(20),
  XM          VARCHAR2(60),
  XB          VARCHAR2(2),
  CSRQ        VARCHAR2(50),
  SFZH        VARCHAR2(18),
  GB          VARCHAR2(50),
  ZYXWMC      VARCHAR2(40),
  XXFS        VARCHAR2(10),
  GCLYMC      VARCHAR2(50),
  BYYX        VARCHAR2(100),
  LXSM        VARCHAR2(20),
  DYCS        VARCHAR2(10),
  DY          VARCHAR2(5),
  ZZRQ        VARCHAR2(50),
  PRINTTIME   VARCHAR2(50),
  PRINTERNAME VARCHAR2(50)
)";

            string s_createXwzsInitTemp = @"create table XWZS_INIT_TEMP
(
  ID          VARCHAR2(20),
  ZSLX        VARCHAR2(10),
  ZSBH        VARCHAR2(17),
  YXSDM       VARCHAR2(50),
  DWDM        VARCHAR2(5),
  DWMC        VARCHAR2(100),
  XKMLM       VARCHAR2(2),
  XKML        VARCHAR2(24),
  YJXKDM      VARCHAR2(4),
  YJXK        VARCHAR2(24),
  ZYDM        VARCHAR2(8),
  ZYMC        VARCHAR2(50),
  XH          VARCHAR2(20),
  XM          VARCHAR2(60),
  XB          VARCHAR2(2),
  CSRQ        VARCHAR2(50),
  SFZH        VARCHAR2(18),
  GB          VARCHAR2(50),
  ZYXWMC      VARCHAR2(40),
  XXFS        VARCHAR2(10),
  GCLYMC      VARCHAR2(50),
  BYYX        VARCHAR2(100),
  LXSM        VARCHAR2(20),
  DYCS        VARCHAR2(10),
  DY          VARCHAR2(5),
  ZZRQ        VARCHAR2(50),
  PRINTTIME   VARCHAR2(50),
  PRINTERNAME VARCHAR2(50)
)";

            string s_createXwzsFinal = @"create table XWZS_FINAL
(
  XM      VARCHAR2(40),
  XMPY    VARCHAR2(60),
  XBM     VARCHAR2(2),
  XB      VARCHAR2(10),
  MZM     VARCHAR2(10),
  MZ      VARCHAR2(10),
  ZZMMM   VARCHAR2(10),
  ZZMM    VARCHAR2(50),
  CSRQ    VARCHAR2(30),
  ZJLXM   VARCHAR2(10),
  ZJLX    VARCHAR2(50),
  ZJHM    VARCHAR2(50),
  XWSYDWM VARCHAR2(10),
  XWSYDW  VARCHAR2(50),
  XZXM    VARCHAR2(20),
  ZXXM    VARCHAR2(20),
  PYDWM   VARCHAR2(20),
  PYDW    VARCHAR2(20),
  XWLBM   VARCHAR2(10),
  XWLB    VARCHAR2(50),
  ZYDM    VARCHAR2(20),
  ZYMC    VARCHAR2(100),
  ZSZYMC  VARCHAR2(100),
  KSH     VARCHAR2(50),
  RXNY    VARCHAR2(20),
  XH      VARCHAR2(50),
  XZ      VARCHAR2(10),
  BYNY    VARCHAR2(20),
  HXWRQ   VARCHAR2(20),
  SFDEXW  VARCHAR2(10),
  SFFXXW  VARCHAR2(10),
  XWZSBH  VARCHAR2(50),
  ZP      VARCHAR2(100),
  BZ      VARCHAR2(100)
)";

            string s_createHxwrqMd = @"create table XWZS_HXWRQMD
(
  ID VARCHAR2(10),
  XH VARCHAR2(50),
  XM VARCHAR2(50)
)";

            OracleConnection conn = getConnection();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            OracleCommand createCmd = new OracleCommand();
            createCmd.Connection = conn;

            int tableCount = 0;

            try
            {
                conn.Open();

                cmd.CommandText = s_xwzsInitCount;
                tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (tableCount == 0)
                {
                    createCmd.CommandText = s_createXwzsInit;
                    createCmd.ExecuteNonQuery();
                }

                cmd.CommandText = s_xwzsInitTempCount;
                tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (tableCount == 0)
                {
                    createCmd.CommandText = s_createXwzsInitTemp;
                    createCmd.ExecuteNonQuery();
                }

                cmd.CommandText = s_xwzsFinalCount;
                tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (tableCount == 0)
                {
                    createCmd.CommandText = s_createXwzsFinal;
                    createCmd.ExecuteNonQuery();
                }

                cmd.CommandText = s_xwzsHxwrqMdCount;
                tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (tableCount == 0)
                {
                    createCmd.CommandText = s_createHxwrqMd;
                    createCmd.ExecuteNonQuery();
                }

            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("初始化数据表出错：" + oe);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 检测本次上报的学生名单是否是首次上报，即不在历次上报名单中
        /// </summary>
        /// <returns>返回在历次上报名单中的学生，重复上报的学生链表</returns>
        public static List<StuFinal> testFirstReport()
        {
            List<StuFinal> doubleReportStuList= new List<StuFinal>();

            OracleConnection conn = getConnection();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "xwsbPackage.testFirstReport";

            cmd.Parameters.Add("notFirstReport_cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StuFinal sf = new StuFinal();

                    sf.xm = reader.IsDBNull(reader.GetOrdinal("xm")) ? "" : reader.GetString(reader.GetOrdinal("xm"));
                    sf.xmpy = reader.IsDBNull(reader.GetOrdinal("xmpy")) ? "" : reader.GetString(reader.GetOrdinal("xmpy"));
                    sf.xbm = reader.IsDBNull(reader.GetOrdinal("xbm")) ? "" : reader.GetString(reader.GetOrdinal("xbm"));
                    sf.xb = reader.IsDBNull(reader.GetOrdinal("xb")) ? "" : reader.GetString(reader.GetOrdinal("xb"));
                    sf.mzm = reader.IsDBNull(reader.GetOrdinal("mzm")) ? "" : reader.GetString(reader.GetOrdinal("mzm"));
                    sf.mz = reader.IsDBNull(reader.GetOrdinal("mz")) ? "" : reader.GetString(reader.GetOrdinal("mz"));
                    sf.zzmmm = reader.IsDBNull(reader.GetOrdinal("zzmmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmmm"));
                    sf.zzmm = reader.IsDBNull(reader.GetOrdinal("zzmm")) ? "" : reader.GetString(reader.GetOrdinal("zzmm"));
                    sf.csrq = reader.IsDBNull(reader.GetOrdinal("csrq")) ? "" : reader.GetString(reader.GetOrdinal("csrq"));
                    sf.zjlxm = reader.IsDBNull(reader.GetOrdinal("zjlxm")) ? "" : reader.GetString(reader.GetOrdinal("zjlxm"));
                    sf.zjlx = reader.IsDBNull(reader.GetOrdinal("zjlx")) ? "" : reader.GetString(reader.GetOrdinal("zjlx"));
                    sf.zjhm = reader.IsDBNull(reader.GetOrdinal("zjhm")) ? "" : reader.GetString(reader.GetOrdinal("zjhm"));
                    sf.xwsydwm = reader.IsDBNull(reader.GetOrdinal("xwsydwm")) ? "" : reader.GetString(reader.GetOrdinal("xwsydwm"));
                    sf.xwsydw = reader.IsDBNull(reader.GetOrdinal("xwsydw")) ? "" : reader.GetString(reader.GetOrdinal("xwsydw"));
                    sf.xzxm = reader.IsDBNull(reader.GetOrdinal("xzxm")) ? "" : reader.GetString(reader.GetOrdinal("xzxm"));
                    sf.zxxm = reader.IsDBNull(reader.GetOrdinal("zxxm")) ? "" : reader.GetString(reader.GetOrdinal("zxxm"));
                    sf.pydwm = reader.IsDBNull(reader.GetOrdinal("pydwm")) ? "" : reader.GetString(reader.GetOrdinal("pydwm"));
                    sf.pydw = reader.IsDBNull(reader.GetOrdinal("pydw")) ? "" : reader.GetString(reader.GetOrdinal("pydw"));
                    sf.xwlbm = reader.IsDBNull(reader.GetOrdinal("xwlbm")) ? "" : reader.GetString(reader.GetOrdinal("xwlbm"));
                    sf.xwlb = reader.IsDBNull(reader.GetOrdinal("xwlb")) ? "" : reader.GetString(reader.GetOrdinal("xwlb"));
                    sf.zydm = reader.IsDBNull(reader.GetOrdinal("zydm")) ? "" : reader.GetString(reader.GetOrdinal("zydm"));
                    sf.zymc = reader.IsDBNull(reader.GetOrdinal("zymc")) ? "" : reader.GetString(reader.GetOrdinal("zymc"));
                    sf.zszymc = reader.IsDBNull(reader.GetOrdinal("zszymc")) ? "" : reader.GetString(reader.GetOrdinal("zszymc"));
                    sf.ksh = reader.IsDBNull(reader.GetOrdinal("ksh")) ? "" : reader.GetString(reader.GetOrdinal("ksh"));
                    sf.rxny = reader.IsDBNull(reader.GetOrdinal("rxny")) ? "" : reader.GetString(reader.GetOrdinal("rxny"));
                    sf.xh = reader.IsDBNull(reader.GetOrdinal("xh")) ? "" : reader.GetString(reader.GetOrdinal("xh"));
                    sf.xz = reader.IsDBNull(reader.GetOrdinal("xz")) ? "" : reader.GetString(reader.GetOrdinal("xz"));
                    sf.byny = reader.IsDBNull(reader.GetOrdinal("byny")) ? "" : reader.GetString(reader.GetOrdinal("byny"));
                    sf.hxwrq = reader.IsDBNull(reader.GetOrdinal("hxwrq")) ? "" : reader.GetString(reader.GetOrdinal("hxwrq"));
                    sf.sfdexw = reader.IsDBNull(reader.GetOrdinal("sfdexw")) ? "" : reader.GetString(reader.GetOrdinal("sfdexw"));
                    sf.sffxxw = reader.IsDBNull(reader.GetOrdinal("sffxxw")) ? "" : reader.GetString(reader.GetOrdinal("sffxxw"));
                    sf.xwzsbh = reader.IsDBNull(reader.GetOrdinal("xwzsbh")) ? "" : reader.GetString(reader.GetOrdinal("xwzsbh"));
                    sf.zp = reader.IsDBNull(reader.GetOrdinal("zp")) ? "" : reader.GetString(reader.GetOrdinal("zp"));
                    sf.bz = reader.IsDBNull(reader.GetOrdinal("bz")) ? "" : reader.GetString(reader.GetOrdinal("bz"));
                    doubleReportStuList.Add(sf);
                }
            }
            catch (OracleException oe)
            {
                System.Windows.MessageBox.Show("检测是否有重复上报的学生出错:"+oe);
            }
            finally
            {
                conn.Close();
            }

            return doubleReportStuList;
        }

        /// <summary>
        /// 从本次上报名单中将重复上报的学生删除
        /// </summary>
        /// <returns>从本次名单中删除学生是否成功</returns>
        public static bool deleteDoubleReportStu()
        {
            bool success = false;

            OracleConnection conn = getConnection();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xwsbPackage.deleteDoubleReportStu";
                cmd.ExecuteNonQuery();
                success = true;

            }catch(OracleException oe)
            {
                System.Windows.MessageBox.Show("从本次上报名单中删除已上报学生名单出错："+oe);
                success = false;
            }finally
            {
                conn.Close();
            }

            return success;
        }
    }
}
