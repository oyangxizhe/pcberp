using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;

using System.Security.Cryptography;
using System.Collections.Generic;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace XizheC
{
    public class basec
    {
        private DataTable dt;
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        int i, j;
        StringBuilder sqb;
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public SqlConnection getcon()
        {

            string M_str_sqlcon = ConfigurationManager.AppSettings["ConnectionDB"].ToString();
            SqlConnection myCon = new SqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion
        public Microsoft.Practices.EnterpriseLibrary.Data.Database getdb()
        {
           Microsoft.Practices.EnterpriseLibrary.Data.Database db =
               new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConfigurationManager.AppSettings["ConnectionDB"].ToString());

            return db;
        }
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>

        #endregion
 
        #region  执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getcom(string M_str_sqlstr)
        {
            SqlConnection sqlcon = this.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        #endregion
        #region getcoms
        public static void getcoms(string M_str_sqlstr)
        {
            basec bc = new basec();
            SqlConnection sqlcon = bc.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();
        }
        #endregion
        public int totalBiteSize(String str)
        {

            int charNum = 0; //统计字节位数
            char[] _charArray = str.ToCharArray();
            for (int i = 0; i < _charArray.Length; i++)
            {
                char _eachChar = _charArray[i];
                if (_eachChar >= 0x4e00 && _eachChar <= 0x9fa5) //判断中文字符
                    charNum += 2;
                else
                    charNum += 1;
            }
            return charNum;
        }
      
        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public DataSet getds(string M_str_sqlstr, string M_str_table)
        {
            SqlConnection sqlcon = this.getcon();
            SqlDataAdapter sqlda = new SqlDataAdapter(M_str_sqlstr, sqlcon);
            DataSet myds = new DataSet();
            sqlda.Fill(myds, M_str_table);
            return myds;
        }
        #endregion
        public string Get_ORID_LIST(DataTable dt)
        {
            string getSelectOrid = null;
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!list.Contains(dr["ORID"].ToString()))
                    {
                        list.Add(dr["ORID"].ToString());
                    }
                }
                foreach (string listitem in list)
                {
                    if (getSelectOrid == null)
                    {
                        getSelectOrid = "'" + listitem + "'";
                    }
                    else
                    {
                        getSelectOrid = getSelectOrid + "," + "'" + listitem + "'";

                    }
                }
            }
            return getSelectOrid;
        }
        #region  创建SqlDataReader对象
        /// <summary>
        /// 创建一个SqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回SqlDataReader对象</returns>
        public SqlDataReader getread(string M_str_sqlstr)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, sqlcon);
            sqlcon.Open();
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
        #endregion
        #region 编号YM_NEW
        public string numYM_NEW(int digit, int wcodedigit, string wcode, string TABLE_NAME, string tbColumns, string prifix)
        {
            string year, month;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            string P_str_Code, r;
            int P_int_Code;

            sqb = new StringBuilder();
            sqb.AppendFormat("SELECT TOP 1 " + tbColumns + " FROM " + TABLE_NAME + " ");
            sqb.AppendFormat(" WHERE SUBSTRING (" + tbColumns + ",3,2)=SUBSTRING (convert(varchar(4),DATEPART (YY,getdate()),111),3,2)");
            sqb.AppendFormat(" and SUBSTRING (" + tbColumns + ",5,2)=SUBSTRING (convert(varchar(10),getdate(),111),6,2) ORDER BY " + tbColumns + " DESC");
            DataTable dt = this.getdt(sqb.ToString());
            if (dt.Rows.Count > 0)
            {
                P_str_Code = Convert.ToString(dt.Rows[0][tbColumns]);
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(digit - wcodedigit, wcodedigit)) + 1;
                r = prifix + P_str_Code.Substring(prifix.Length, 4) + P_int_Code.ToString().PadLeft(wcodedigit, '0');//在流水码的前面补0直到长度达到指定长度
            }
            else
            {
                r = prifix + year + month + wcode;

            }

            return r;
        }
        #endregion
        #region 编号YMD_NEW
        public string numYMD_NEW(int digit, int wcodedigit, string wcode, string TABLE_NAME, string tbColumns, string prifix)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string P_str_Code, r;
            int P_int_Code;

            sqb = new StringBuilder();
            sqb.AppendFormat("SELECT TOP 1 " + tbColumns + " FROM " + TABLE_NAME + " ");
            sqb.AppendFormat(" WHERE SUBSTRING (" + tbColumns + ",3,2)=SUBSTRING (convert(varchar(4),DATEPART (YY,getdate()),111),3,2)");
            sqb.AppendFormat(" and SUBSTRING (" + tbColumns + ",5,2)=SUBSTRING (convert(varchar(10),getdate(),111),6,2) ");
            sqb.AppendFormat(" and SUBSTRING (" + tbColumns + ",7,2)=SUBSTRING (convert(varchar(10),getdate(),111),9,2) ORDER BY " + tbColumns + " DESC");
            DataTable dt = this.getdt(sqb.ToString());
            if (dt.Rows.Count > 0)
            {
                P_str_Code = Convert.ToString(dt.Rows[0][tbColumns]);
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(digit - wcodedigit, wcodedigit)) + 1;
                r = prifix + P_str_Code.Substring(prifix.Length, 6) + P_int_Code.ToString().PadLeft(wcodedigit, '0');//在流水码的前面补0直到长度达到指定长度
            }
            else
            {
                r = prifix + year + month + day + wcode;

            }
            return r;
        }
        #endregion
        #region 编号num_NEW
        public string num_NEW(int digit, int wcodedigit, string wcode, string table_name, string tbColumns, string prifix)
        {
            string year, month;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            string P_str_Code, r, sql1;
            sql1 = "select top 1 " + tbColumns + " from " + table_name + " ORDER BY " + tbColumns + " DESC";
            int P_int_Code;
            DataTable dt = this.getdt(sql1);
            if (dt.Rows.Count > 0)
            {
                P_str_Code = dt.Rows[0][tbColumns].ToString();
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(digit - wcodedigit, wcodedigit)) + 1;
                r = prifix + P_int_Code.ToString().PadLeft(wcodedigit, '0');//在流水码的前面补0直到长度达到指定长度
            }
            else
            {
                r = prifix + wcode;
            }
            return r;
        }
        #endregion
        #region GetIP4Address //取得IPV4地址
        public string GetIP4Address()
        {
            string IPV4 = "";
            string hostName = Dns.GetHostName();
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);
            foreach (IPAddress IPA in addressList)
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IPV4 = IPA.ToString();
                }
            }
            return IPV4;
        }
        #endregion
        #region GetComputerName //取得本地计算机名
        public string GetComputerName()
        {
            string hostName = "";
            hostName = Dns.GetHostName();
            return hostName;
        }
        #endregion
        public DataTable table(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcmd = new SqlCommand(M_str_sql, sqlcon);
            sqlcon.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sqlcon.Close();
            GC.Collect();
            return dt;
        }
        public static GridView GenerateColumns(GridView gv, DataTable dt)
        {
            // 把GridView的自动产生列设置为false,否则会出现重复列
            gv.AutoGenerateColumns = false;

            // 清空所有的Columns
            gv.Columns.Clear();

            // 遍历DataTable 的每个Columns,然后添加到GridView中去
            foreach (DataColumn item in dt.Columns)
            {
                BoundField col = new BoundField();
                col.HeaderText = item.ColumnName;
                col.DataField = item.ColumnName;
                col.Visible = true;
                gv.Columns.Add(col);
            }
            return gv;
        }
        public DataTable getdt(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public static DataTable getdts(string M_str_sql)
        {
            basec bc = new basec();
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public SqlDataAdapter getda(string M_str_sql)
        {
            SqlConnection sqlcon = this.getcon();
            SqlCommand sqlcom = new SqlCommand(M_str_sql, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            return da;


        }
        #region 编号YM
        public string numYM(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql + " WHERE YEAR='" + year + "' AND  MONTH='" + month + "'";
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + year + month + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + year + month + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号YMD
        public string numYMD(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql + " WHERE YEAR='" + year + "' AND  MONTH='" + month + "' AND DAY='" + day + "'";
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + year + month + day + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + year + month + day + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号NOYMD
        public string numNOYMD(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {

            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql;
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }


            }
            else
            {
                r = prifix + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号Restriction
        public string Restriction(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql;
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + year + month + day + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + year + month + day + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region 编号YMCU
        public string numYMCU(int digit, int wcodedigit, string wcode, string sql, string tbColumns, string prifix)
        {
            string year, month;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            string P_str_Code, t, r, sql1, q = "";
            int P_int_Code, w, w1;

            sql1 = sql;
            SqlDataReader sqlread = this.getread(sql1);
            DataTable dt = this.getdt(sql1);
            sqlread.Read();
            if (sqlread.HasRows)
            {
                P_str_Code = Convert.ToString(dt.Rows[(dt.Rows.Count - 1)][tbColumns]);
                w1 = digit - wcodedigit;
                P_int_Code = Convert.ToInt32(P_str_Code.Substring(w1, wcodedigit)) + 1;
                t = Convert.ToString(P_int_Code);
                w = wcodedigit - t.Length;
                if (w >= 0)
                {
                    while (w >= 1)
                    {
                        q = q + "0";
                        w = w - 1;

                    }
                    r = prifix + q + P_int_Code;
                }
                else
                {
                    r = "Exceed Limited";

                }

            }
            else
            {
                r = prifix + wcode;
            }
            sqlread.Close();
            return r;
        }
        #endregion
        #region yesno
        public int yesno(string vars)
        {
            int k = 1;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46)
                {
                    k = 1;
                }
                else
                {
                    k = 0; break;
                }

            }

            return k;

        }
        #endregion

        #region yesno1
        public int yesno1(string vars)
        {
            int k = 1;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57)
                {
                    k = 1;
                }
                else
                {
                    k = 0; break;
                }

            }

            return k;

        }
        #endregion
        #region checkphone
        public bool checkphone(string vars)
        {
            bool k = true;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46 || p == 45)
                {

                }
                else
                {
                    k = false;
                    break;
                }

            }

            return k;

        }
        #endregion
        #region checkEMAIL
        public bool checkEmail(string vars)
        {
            bool k = true;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p == 46 || p >= 64 && p <= 90 || p >= 97 && p <= 122)
                {

                }
                else
                {
                    k = false;
                    break;
                }

            }
            return k;
        }
        #endregion
        #region checkNumber
        public bool checkNumber(string vars)
        {
            bool k = false;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57)
                {
                    k = true;
                    break;
                }
            }
            return k;
        }
        #endregion
        #region checkLetter
        public bool checkLetter(string vars)
        {
            bool k = false;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 65 && p <= 90 || p >= 97 && p <= 122)
                {
                    k = true;
                    break;
                }
            }
            return k;
        }
        #endregion
        #region getstoragetable
        public DataTable getstoragetable()
        {
            DataTable dtk = new DataTable();
            dtk.Columns.Add("品号", typeof(string));
            dtk.Columns.Add("料号", typeof(string));
            dtk.Columns.Add("客户名称", typeof(string));
            dtk.Columns.Add("品名", typeof(string));
            dtk.Columns.Add("客户料号", typeof(string));
            dtk.Columns.Add("规格", typeof(string));
            dtk.Columns.Add("单位", typeof(string));
            dtk.Columns.Add("仓库", typeof(string));
            dtk.Columns.Add("批号", typeof(string));
            dtk.Columns.Add("库存数量", typeof(string));
            return dtk;
        }
        #endregion
        #region checkingWareidAndstorage
        public string CheckingWareidAndStorage(string wareid, string storageType, string batchID, string SKU)
        {
            string storagecount = "A";
            DataTable dt = this.getstoragecountNew();
            DataTable dtu1 = new DataTable();
            DataRow[] dr = dt.Select("品号= '" + wareid + "' and 仓库名称='" + storageType + "' and 批号='" + batchID + "' ");
            if (dr.Length > 0)
            {
                storagecount = dr[0]["库存数量"].ToString();
            }
            return storagecount;
        }
        #endregion
        public DataTable getstoragecountNew()
        {
            DataTable dt = getdt("exec ReturnStorage '','','','','','','','','',''");
            return dt;

        }
  


        #region juagestoragecount
        public bool JuageDeleteCount_MoreThanStorageCount(string GodEID)
        {
            int i;
            bool z = false;
            DataTable dt6 = this.getdt(@"
select A.WAREID AS WAREID,A.STORAGEID AS STORAGEID,B.STORAGENAME AS STORAGENAME,A.BATCHID AS BATCHID,
CASE WHEN SUM(A.FREECOUNT) IS NOT NULL THEN SUM(A.GECOUNT)+CAST(ROUND(SUM(A.FREECOUNT),2) AS DECIMAL(18,2))
ELSE SUM(A.GECOUNT)
END 
 AS GECOUNTANDFREECOUNT FROM GODE A LEFT JOIN STORAGEINFO B ON 
A.STORAGEID=B.STORAGEID  WHERE A.GODEID='" + GodEID + "' GROUP BY A.WAREID,A.STORAGEID,B.STORAGENAME,"
           + " A.BATCHID ORDER BY A.WAREID,A.STORAGEID,A.BATCHID ASC");
            if (dt6.Rows.Count > 0)
            {
                for (i = 0; i < dt6.Rows.Count; i++)
                {
                    string c1, c2, c3;
                    c1 = dt6.Rows[i]["WAREID"].ToString();
                    c2 = dt6.Rows[i]["STORAGENAME"].ToString();
                    c3 = dt6.Rows[i]["BATCHID"].ToString();
                    DataRow[] dr = this.getstoragecountNew().Select("品号='" + c1 + "' and 仓库名称='" + c2 + "'AND 批号='" + c3 + "'");
                    if (dr.Length > 0)
                    {
                        if (decimal.Parse(dr[0]["库存数量"].ToString()) < decimal.Parse(dt6.Rows[i]["GECOUNTANDFREECOUNT"].ToString()))
                        {

                            ErrowInfo = "品号:" + dt6.Rows[i][0].ToString() + " 库存不足，不允许编辑或删除该单据";
                            z = true;
                            break;
                        }
                    }
                    else
                    {

                        ErrowInfo = "品号:" + dt6.Rows[i][0].ToString() + " 库存不足，不允许编辑或删除该单据";
                        z = true;
                        break;
                    }

                }
            }
            return z;
        }
        #endregion
        #region JuageDeleteCount_MoreThanStorageCount
        public bool JuageDeleteCount_IFMoreThanTOTAL_ACTUAL_SELLCOUNT(string ORID, string SEID)
        {
            int i;
            bool b = false;
            CORDER corder = new CORDER();
            DataTable dt = GET_DT_TO_DV_TO_DT(corder.GET_TOTAL_ORDER(), "", "订单号='" + ORID + "'");
            DataTable dtx5 = this.getdt(@"
SELECT 
A.ORID AS ORID,
A.SEID AS SEID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT
FROM SELLTABLE_DET A 
LEFT JOIN  MATERE B
ON A.SEKEY=B.MRKEY 
WHERE  A.ORID='" + ORID + "' AND A.SEID='" + SEID + "' GROUP BY A.ORID,A.SEID,A.SN,B.WAREID");
            if (dt.Rows.Count > 0)
            {
                if (dtx5.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        if (b)
                            break;
                        for (j = 0; j < dtx5.Rows.Count; j++)
                        {
                            if (dt.Rows[i]["订单号"].ToString() == dtx5.Rows[j]["ORID"].ToString() && dt.Rows[i]["项次"].ToString() == dtx5.Rows[j]["SN"].ToString())
                            {
                                if (decimal.Parse(dtx5.Rows[j]["MRCOUNT"].ToString()) > decimal.Parse(dt.Rows[i]["实际累计销货数量"].ToString()))
                                {
                                    ErrowInfo = "项次：" + dt.Rows[i]["项次"].ToString() + " 本销货单累计删除数量：" + dtx5.Rows[j]["MRCOUNT"].ToString() +
                                        " 大于订单该项次实际累计销货数量 " + dt.Rows[i]["实际累计销货数量"].ToString() + " 不允许删除";
                                    b = true;
                                    break;


                                }
                            }
                        }
                    }

                }
            }
            return b;
        }
        #endregion
        #region juagedate
        public bool juagedate(string StartDate, string EndDate)
        {
            bool b = true;
            if (StartDate != "" && EndDate != "")
            {
                if (Convert.ToDateTime(StartDate) <= Convert.ToDateTime(EndDate))
                {

                }
                else
                {
                    ErrowInfo = "截止日期需大于起始日期！";
                    return false;

                }
            }
            return b;
        }
        #endregion
        #region JuageCurrentDateIFAboveDeliveryDate
        public bool JuageCurrentDateIFAboveDeliveryDate(string CurrentDate, string DeliveryDate)
        {
            bool b = false;
            if (CurrentDate != "" && DeliveryDate != "")
            {
                if (Convert.ToDateTime(CurrentDate) >= Convert.ToDateTime(DeliveryDate).AddDays(+1))
                {
                    return true;
                }

            }
            return b;
        }
        #endregion
        #region JuageCurrentDateIFAboveDeliveryDate
        public bool JuageCurrentDateIFAboveDeliveryDate(string ORIDorPUID, int OrderOrPurchase)
        {
            bool b = false;
            DataTable dt = new DataTable();
            if (OrderOrPurchase == 0)
            {
                dt = this.getdt("SELECT * FROM ORDER_DET WHERE ORID='" + ORIDorPUID + "'");
                foreach (DataRow dr in dt.Rows)
                {

                    if (Convert.ToDateTime(DateTime.Now.ToString()) >= Convert.ToDateTime(dr["DELIVERYDATE"].ToString()).AddDays(+1))
                    {
                        b = true;
                        break;
                    }
                }

            }
            else
            {
                dt = this.getdt("SELECT * FROM PURCHASE_DET WHERE PUID='" + ORIDorPUID + "'");
                foreach (DataRow dr in dt.Rows)
                {

                    if (Convert.ToDateTime(DateTime.Now.ToString()) >= Convert.ToDateTime(dr["NEEDDATE"].ToString()).AddDays(+1))
                    {
                        b = true;
                        break;
                    }

                }

            }

            return b;

        }
        #endregion
        #region exists
        public bool exists(string sql)
        {
            DataTable dtx1 = this.getdt(sql);
            if (dtx1.Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion
        #region getstoragecount_MRP
        public DataTable getstoragecount_MRP()
        {
            int s1, s2;
            DataTable dtk = this.getstoragetable();
            DataTable dtk1 = new DataTable();
            DataTable dtk2 = new DataTable();
            string sqlk1 = @"
SELECT
A.WAREID AS WAREID,
B.WNAME AS WNAME,
B.CWAREID AS CWAREID,
D.CNAME AS CNAME,
A.SKU AS SKU,
CAST(ROUND(SUM(A.GECOUNT),2) AS DECIMAL(18,2))
AS GECOUNT,
CASE WHEN SUM(A.FREECOUNT) IS NOT NULL THEN CAST(ROUND(SUM(A.FREECOUNT),2) AS DECIMAL(18,2))
ELSE 0
END 
AS FREECOUNT,
CASE WHEN SUM(A.FREECOUNT) IS NOT NULL THEN SUM(A.GECOUNT)+CAST(ROUND(SUM(A.FREECOUNT),2) AS DECIMAL(18,2))
ELSE SUM(A.GECOUNT)
END 
AS GECOUNTANDFREECOUNT
 FROM GODE A
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID 
LEFT JOIN STORAGEINFO C ON C.STORAGEID=A.STORAGEID 
LEFT JOIN CUSTOMERINFO_MST D ON B.CUID =D.CUID 
WHERE C.IFMRP_CALCULATE='Y'
GROUP  BY 
A.WAREID,
B.WNAME,
B.CWAREID ,
D.CNAME,
A.SKU  
ORDER BY 
A.WAREID

";

            string sqlk2 = @"
SELECT 
A.WAREID AS WAREID,
A.SKU AS SKU,
CAST(ROUND(SUM(A.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT,
CASE WHEN SUM(A.FREECOUNT) IS NOT NULL THEN CAST(ROUND(SUM(A.FREECOUNT),2) AS DECIMAL(18,2))
ELSE 0
END 
AS FREECOUNT,
CASE WHEN SUM(A.FREECOUNT) IS NOT NULL THEN SUM(A.MRCOUNT)+CAST(ROUND(SUM(A.FREECOUNT),2) AS DECIMAL(18,2))
ELSE SUM(A.MRCOUNT)
END 
AS MRCOUNTANDFREECOUNT
FROM MATERE A
LEFT JOIN STORAGEINFO B ON A.STORAGEID=B.STORAGEID
WHERE B.IFMRP_CALCULATE='Y'
GROUP BY
WAREID,
SKU 
ORDER BY 
WAREID

";

            dtk1 = this.getdt(sqlk1);
            dtk2 = this.getdt(sqlk2);

            for (s1 = 0; s1 < dtk1.Rows.Count; s1++)
            {
                decimal d1 = 0;
                string z = "";
                decimal dec1 = 0;
                for (s2 = 0; s2 < dtk2.Rows.Count; s2++)
                {
                    string v1 = dtk1.Rows[s1]["WAREID"].ToString();
                    string v4 = dtk2.Rows[s2]["WAREID"].ToString();
                    if (v1 == v4)
                    {

                        dec1 = (decimal.Parse(dtk1.Rows[s1]["GECOUNTANDFREECOUNT"].ToString())) - (decimal.Parse(dtk2.Rows[s2]["MRCOUNTANDFREECOUNT"].ToString()));
                        z = Convert.ToString(dec1);
                        break;
                    }
                }
                if (z != "")
                {

                    d1 = decimal.Parse(z);
                }
                else
                {
                    d1 = decimal.Parse(dtk1.Rows[s1]["GECOUNTANDFREECOUNT"].ToString());
                }
                if (d1 != 0)
                {
                    DataRow dr = dtk.NewRow();
                    dr["品号"] = dtk1.Rows[s1]["WAREID"].ToString();
                    dr["品名"] = dtk1.Rows[s1]["WNAME"].ToString();
                    DataTable dtx2 = this.getdt("select * from wareinfo where wareid='" + dtk1.Rows[s1]["WAREID"].ToString() + "'");
                    dr["规格"] = dtx2.Rows[0]["Spec"].ToString();
                    dr["库存单位"] = dtk1.Rows[s1]["SKU"].ToString();
                    dr["库存数量"] = d1;
                    dr["客户名称"] = dtk1.Rows[s1]["CNAME"].ToString();
                    dr["客户料号"] = dtk1.Rows[s1]["CWAREID"].ToString();
                    dtk.Rows.Add(dr);

                }
            }
            return dtk;
        }
        #endregion
        #region maxstoragecount
        public DataTable getmaxstoragecount(string wareid)
        {
            DataTable dt = this.getstoragecountNew();
            DataTable dtu1 = new DataTable();
            DataRow[] dr = dt.Select("品号='" + wareid + "'");
            if (dr.Length > 0)
            {
                DataTable dtu = this.getstoragetable();
                for (i = 0; i < dr.Length; i++)
                {
                    DataRow dr1 = dtu.NewRow();
                    dr1["品号"] = dr[i]["品号"].ToString();
                    dr1["品名"] = dr[i]["品名"].ToString();
                    dr1["仓库"] = dr[i]["仓库"].ToString();
                    dr1["批号"] = dr[i]["批号"].ToString();
                    dr1["库存数量"] = dr[i]["库存数量"].ToString();
                    dtu.Rows.Add(dr1);

                }
                string s1 = "";
                string s2 = "";/*13111501*/
                decimal c1 = 0;
                decimal n = 0;
                string n1 = "";
                string n2 = "";/*13111501*/
                if (dtu.Rows.Count == 1)
                {

                    s1 = dtu.Rows[0]["仓库"].ToString();
                    s2 = dtu.Rows[0]["批号"].ToString();
                    c1 = decimal.Parse(dtu.Rows[0]["库存数量"].ToString());
                }
                else
                {
                    for (int j = 0; j < dtu.Rows.Count; j++)
                    {

                        decimal c2 = decimal.Parse(dtu.Rows[j]["库存数量"].ToString());

                        if (n > c2)
                        {

                        }
                        else if (n == c2)
                        {


                        }
                        else
                        {
                            n = c2;
                            n1 = dtu.Rows[j]["仓库"].ToString();
                            n2 = dtu.Rows[j]["批号"].ToString();

                        }
                    }
                    s1 = n1;
                    s2 = n2;/*13111501*/
                    c1 = n;
                }
                dtu1 = this.getstoragetable();
                DataRow dr2 = dtu1.NewRow();
                dr2["品号"] = dtu.Rows[0]["品号"].ToString();
                dr2["品名"] = dtu.Rows[0]["品名"].ToString();
                dr2["仓库"] = s1;
                dr2["批号"] = s2;
                dr2["库存数量"] = c1;
                dtu1.Rows.Add(dr2);
            }
            return dtu1;
        }
        public DataTable getstoragecountNew(string wareid, string wname, string co_wareid, string cwareid, string storageId, string storageName, 
            string batchId, string cname,string plankThickness, string spec)
        {
            sqb = new StringBuilder("exec ReturnStorage ");
            sqb.AppendFormat("'{0}',", wareid);
            sqb.AppendFormat("'{0}',", wname);
            sqb.AppendFormat("'{0}',", co_wareid);
            sqb.AppendFormat("'{0}',", cwareid);
            sqb.AppendFormat("'{0}',", storageId);
            sqb.AppendFormat("'{0}',", storageName);
            sqb.AppendFormat("'{0}',", batchId);
            sqb.AppendFormat("'{0}',", cname);
            sqb.AppendFormat("'{0}',",plankThickness );
            sqb.AppendFormat("'{0}'", spec);
            DataTable dt = getdt(sqb.ToString());
            return dt;
        }
       /* public DataTable getstoragecountNew(string wareid, string wname, string co_wareid, string cwareid, string storageId, string storageName, string batchId, string cname)
        {
            sqb = new StringBuilder("exec ReturnStorage ");
            sqb.AppendFormat("'{0}',", wareid);
            sqb.AppendFormat("'{0}',", wname);
            sqb.AppendFormat("'{0}',", co_wareid);
            sqb.AppendFormat("'{0}',", cwareid);
            sqb.AppendFormat("'{0}',", storageId);
            sqb.AppendFormat("'{0}',", storageName);
            sqb.AppendFormat("'{0}',", batchId);
            sqb.AppendFormat("'{0}'", cname);
            DataTable dt = getdt(sqb.ToString());
            return dt;
        }*/
        #endregion
        #region getstorageid
        public string getstorageid(string storagetype)
        {
            string storageid = "";
            DataTable dtx3 = this.getdt("select * from tb_storageinfo where storagetype='" + storagetype + "'");
            if (dtx3.Rows.Count > 0)
            {
                storageid = dtx3.Rows[0][0].ToString();
            }

            return storageid;
        }
        #endregion
        #region checkingWareidAndstorage
        public string CheckingWareidAndStorage(string wareid, string storageType, string batchID)
        {
            string storagecount = null;
            DataTable dt = this.getstoragecountNew(wareid, "", "", "", "", storageType, batchID, "","","");
            if (dt.Rows.Count > 0)
                storagecount = dt.Rows[0]["库存数量"].ToString();
            return storagecount;
        }
        #endregion
        #region getOnlyString
        public string getOnlyString(string sql)
        {
            string s2 = "";
            DataTable dtu2 = this.getdt(sql);
            if (dtu2.Rows.Count > 0)
            {
                s2 = dtu2.Rows[0][0].ToString();

            }


            return s2;
        }
        #endregion

      


        #region getprintinfo
        public DataTable getPrintInfo()
        {

            DataTable dtt = new DataTable();
            dtt.Columns.Add("销货单号", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("套件", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("细节", typeof(string));
            dtt.Columns.Add("皮种", typeof(string));
            dtt.Columns.Add("颜色", typeof(string));
            dtt.Columns.Add("线色", typeof(string));
            dtt.Columns.Add("海棉厚度", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("折扣率", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("销货数量", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal), "销售单价*折扣率*销货数量");
            dtt.Columns.Add("税额", typeof(decimal), "销售单价*折扣率*销货数量*税率/100");
            dtt.Columns.Add("含税金额", typeof(decimal), "销售单价*折扣率*销货数量*(1+税率/100)");
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户", typeof(string));
            dtt.Columns.Add("电话", typeof(string));
            dtt.Columns.Add("地址", typeof(string));
            dtt.Columns.Add("订货日期", typeof(string));
            dtt.Columns.Add("交货日期", typeof(string));
            dtt.Columns.Add("加急否", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            return dtt;


        }
        #endregion
        #region ask
        public DataTable ask(string sqlcondition, int GROUP, int printselltable)
        {

            string M_str_sql1 = @"select ORID ,SN ,WareID ,OCOUNT ,ORDERDATE,DELIVERYDATE,URGENT,SELLUNITPRICE,DISCOUNTRATE,TAXRATE,CUID FROM TB_ORDER";

            DataTable dtt = this.getPrintInfo();
            DataTable dtx6 = this.getdt(sqlcondition);
            if (dtx6.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtx6.Rows.Count; i1++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["销货单号"] = dtx6.Rows[i1]["SEID"].ToString();
                    dr["订单号"] = dtx6.Rows[i1]["ORID"].ToString();
                    if (printselltable == 1)
                    {

                    }
                    else
                    {
                        dr["项次"] = dtx6.Rows[i1]["SN"].ToString();
                    }
                    dr["品号"] = dtx6.Rows[i1]["WAREID"].ToString();
                    DataTable dtx2 = this.getdt("select * from tb_wareinfo where wareid='" + dtx6.Rows[i1]["WAREID"].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    if (GROUP == 0)
                    {
                        dr["销货数量"] = dtx6.Rows[i1]["SECOUNT"].ToString();
                        dr["制单人"] = dtx6.Rows[i1]["MAKER"].ToString();
                        dr["制单日期"] = dtx6.Rows[i1]["DATE"].ToString();
                    }
                    else
                    {
                        dr["销货数量"] = dtx6.Rows[i1][3].ToString();

                    }

                    dtt.Rows.Add(dr);

                }
            }

            DataTable dtx4 = this.getdt(M_str_sql1);
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtt.Rows.Count; i++)
                {
                    for (int j = 0; j < dtx4.Rows.Count; j++)
                    {
                        if (printselltable == 1)
                        {
                            if (dtt.Rows[i]["订单号"].ToString() == dtx4.Rows[j]["ORID"].ToString() && dtt.Rows[i]["品号"].ToString() == dtx4.Rows[j]["WAREID"].ToString())
                            {
                                dtt.Rows[i]["订单数量"] = dtx4.Rows[j]["OCOUNT"].ToString();
                                dtt.Rows[i]["订货日期"] = dtx4.Rows[j]["ORDERDATE"].ToString();
                                dtt.Rows[i]["交货日期"] = dtx4.Rows[j]["DELIVERYDATE"].ToString();
                                dtt.Rows[i]["加急否"] = dtx4.Rows[j]["URGENT"].ToString();
                                dtt.Rows[i]["销售单价"] = dtx4.Rows[j]["SELLUNITPRICE"].ToString();
                                dtt.Rows[i]["折扣率"] = dtx4.Rows[j]["DISCOUNTRATE"].ToString();
                                dtt.Rows[i]["税率"] = dtx4.Rows[j]["TAXRATE"].ToString();
                                dtt.Rows[i]["客户代码"] = dtx4.Rows[j]["CUID"].ToString();
                                DataTable dtx7 = this.getdt("select * from tb_customerinfo where cuid='" + dtx4.Rows[j]["CUID"].ToString() + "'");
                                dtt.Rows[i]["客户"] = dtx7.Rows[0]["CNAME"].ToString();
                                dtt.Rows[i]["电话"] = dtx7.Rows[0]["PHONE"].ToString();
                                dtt.Rows[i]["地址"] = dtx7.Rows[0]["ADDRESS"].ToString();
                                break;
                            }
                        }
                        else
                        {

                            if (dtt.Rows[i]["订单号"].ToString() == dtx4.Rows[j]["ORID"].ToString() && dtt.Rows[i]["项次"].ToString() == dtx4.Rows[j]["SN"].ToString())
                            {
                                dtt.Rows[i]["订单数量"] = dtx4.Rows[j]["OCOUNT"].ToString();
                                dtt.Rows[i]["订货日期"] = dtx4.Rows[j]["ORDERDATE"].ToString();
                                dtt.Rows[i]["交货日期"] = dtx4.Rows[j]["DELIVERYDATE"].ToString();
                                dtt.Rows[i]["加急否"] = dtx4.Rows[j]["URGENT"].ToString();
                                dtt.Rows[i]["销售单价"] = dtx4.Rows[j]["SELLUNITPRICE"].ToString();
                                dtt.Rows[i]["折扣率"] = dtx4.Rows[j]["DISCOUNTRATE"].ToString();
                                dtt.Rows[i]["税率"] = dtx4.Rows[j]["TAXRATE"].ToString();
                                dtt.Rows[i]["客户代码"] = dtx4.Rows[j]["CUID"].ToString();
                                DataTable dtx7 = this.getdt("select * from tb_customerinfo where cuid='" + dtx4.Rows[j]["CUID"].ToString() + "'");
                                dtt.Rows[i]["客户"] = dtx7.Rows[0]["CNAME"].ToString();
                                dtt.Rows[i]["电话"] = dtx7.Rows[0]["PHONE"].ToString();
                                dtt.Rows[i]["地址"] = dtx7.Rows[0]["ADDRESS"].ToString();
                                break;
                            }

                        }

                    }
                }
            }
            return dtt;
        }
        #endregion
        public DataTable asko(string sql, int Need)
        {
            DataTable dt = new DataTable();

            /*销货单打印数据含项次*/
            string s31 = @"
select A.SEID AS 销货单号,A.ORID AS 订单号,G.ORDERDATE AS 订货日期,C.DELIVERYDATE AS 交货日期,C.URGENT AS 加急否,
A.SN as 项次,E.WareID as 品号,
B.WNAME AS 品名,B.SPEC as 规格,B.UNIT as 单位,B.CWAREID AS 客户料号,
C.SELLUNITPRICE AS 销售单价,C.TAXRATE AS 税率,
SUM(E.MRCount) as 销货数量 ,SUM(E.MRCOUNT*C.SELLUNITPRICE) AS 未税金额,SUM(E.MRCOUNT*C.SELLUNITPRICE*C.TAXRATE/100) 
AS 税额,SUM(E.MRCOUNT*C.SELLUNITPRICE*(1+C.TAXRATE/100)) AS 含税金额,C.CUID as 客户代码,
D.CName as 客户 ,H.PHONE AS 电话,H.ADDRESS AS 地址,F.SELLDATE AS 销货日期,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SELLERID )  AS 销货员
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN ORDER_MST G ON A.ORID=G.ORID
LEFT JOIN CUSTOMERINFO_DET H ON D.CUKEY=H.CUKEY";

            string s32 = @" GROUP BY A.SEID,A.ORID,A.SN,E.WAREID,B.WNAME,B.SPEC,B.UNIT,B.CWAREID,
C.SELLUNITPRICE,C.TAXRATE,C.CUID,D.CNAME,F.SELLDATE,F.SELLERID,G.ORDERDATE,C.DELIVERYDATE,C.URGENT,H.PHONE,H.ADDRESS ORDER BY A.SEID,A.SN";

            if (Need == 2)
            {
                dt = this.getdt(s31 + sql + s32);


            }
            return dt;
        }
        #region PrintOrder
        public DataTable PrintOrder(string sqlcondition)
        {

            string M_str_sql1 = @"select * FROM TB_ORDER";
            DataTable dtt = this.getPrintInfo();
            DataTable dtx6 = this.getdt(M_str_sql1 + sqlcondition);
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();

                    dr["订单号"] = dtx6.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx6.Rows[i]["SN"].ToString();
                    dr["品号"] = dtx6.Rows[i]["WAREID"].ToString();
                    DataTable dtx2 = this.getdt("select * from tb_wareinfo where wareid='" + dtx6.Rows[i]["WAREID"].ToString() + "'");
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["套件"] = dtx2.Rows[0]["ExternalM"].ToString();
                    dr["型号"] = dtx2.Rows[0]["TYPE"].ToString();
                    dr["细节"] = dtx2.Rows[0]["DETAIL"].ToString();
                    dr["皮种"] = dtx2.Rows[0]["Leather"].ToString();
                    dr["颜色"] = dtx2.Rows[0]["COLOR"].ToString();
                    dr["线色"] = dtx2.Rows[0]["StitchingC"].ToString();
                    dr["海棉厚度"] = dtx2.Rows[0]["Thickness"].ToString();
                    dr["订单数量"] = dtx6.Rows[i]["OCOUNT"].ToString();
                    dr["订货日期"] = dtx6.Rows[i]["ORDERDATE"].ToString();
                    dr["交货日期"] = dtx6.Rows[i]["DELIVERYDATE"].ToString();
                    dr["加急否"] = dtx6.Rows[i]["URGENT"].ToString();
                    dr["客户代码"] = dtx6.Rows[i]["CUID"].ToString();
                    DataTable dtx7 = this.getdt("select * from tb_customerinfo where cuid='" + dtx6.Rows[i]["CUID"].ToString() + "'");
                    dr["客户"] = dtx7.Rows[0]["CNAME"].ToString();
                    dr["电话"] = dtx7.Rows[0]["PHONE"].ToString();
                    dr["地址"] = dtx7.Rows[0]["ADDRESS"].ToString();
                    dtt.Rows.Add(dr);

                }
            }
            return dtt;
        }
        #endregion
        public bool juageValueLimits(string[] arr, string b)
        {
            DataTable dtzz = new DataTable();
            dtzz.Columns.Add("X", typeof(string));

            for (i = 0; i < arr.Length; i++)
            {
                DataRow dr = dtzz.NewRow();
                dr["X"] = arr[i];
                dtzz.Rows.Add(dr);
            }
            DataRow[] dr1 = dtzz.Select("X='" + b + "'");
            bool b1 = true;
            if (dr1.Length > 0)
            {

            }
            else
            {
                b1 = false;


            }
            return b1;
        }
        public bool juageValueLimits(string sql, string b)
        {
            DataTable dt = this.getdt(sql);
            DataTable dtzz = new DataTable();
            dtzz.Columns.Add("X", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtzz.NewRow();
                    dr["X"] = dt.Rows[i][0].ToString();
                    dtzz.Rows.Add(dr);
                }
            }
            DataRow dr1 = dtzz.NewRow();
            dr1["X"] = "";
            dtzz.Rows.Add(dr1);
            DataRow[] dr2 = dtzz.Select("X='" + b + "'");
            bool b1 = true;
            if (dr2.Length > 0)
            {

            }
            else
            {
                b1 = false;


            }
            return b1;
        }
        #region GetFileName
        public string GetFileName(string sql, string field)
        {
            string v2 = "";
            DataTable dt1 = this.getdt(sql);
            if (dt1.Rows.Count > 0)
            {
                string v1 = dt1.Rows[0][field].ToString();
                for (int j = v1.Length - 1; j >= 0; j--)
                {
                    if (v1[j] != '-')
                    {
                        v2 = v1[j] + v2;
                    }
                    else
                    {
                        break;

                    }
                }
            }
            return v2;
        }
        #endregion
        #region DelImagesFile
        public void DelImagesFile(string path, string sql, string field)
        {
            DataTable dt1 = this.getdt(sql);
            if (dt1.Rows.Count > 0)
            {
                for (i = 0; i < dt1.Rows.Count; i++)
                {
                    string v1 = dt1.Rows[i][field].ToString();
                    string v2 = "";
                    for (int j = v1.Length - 1; j >= 0; j--)
                    {
                        if (v1[j] != '-')
                        {
                            v2 = v1[j] + v2;
                        }
                        else
                        {
                            break;

                        }
                    }
                    string path2 = path + v2;
                    string path3 = path + "50x50-" + v2;
                    string path4 = path + "150x150-" + v2;
                    if (File.Exists(path2))
                    {
                        File.Delete(path2);
                    }
                    if (File.Exists(path3))
                    {
                        File.Delete(path3);
                    }
                    if (File.Exists(path4))
                    {
                        File.Delete(path4);
                    }
                }
            }
        }
        #endregion

        #region GetStorageCOID
        public DataTable GetStorageCOID(string v1)
        {
            DataTable dtk = new DataTable();
            dtk.Columns.Add("WAREID", typeof(string));
            dtk.Columns.Add("COID", typeof(string));
            dtk.Columns.Add("COLOR", typeof(string));
            dtk.Columns.Add("COLORIMAS", typeof(string));

            string sql = @"
SELECT A.WAREID,A.COID,B.COLOR,SUM(A.GECOUNT) ,SUM(A.MRCOUNT),
SUM(A.GECOUNT)-SUM(A.MRCOUNT)  FROM TB_SIZEMANAGE A
LEFT JOIN TB_COLOR B ON A.COID=B.COID
LEFT JOIN TB_SIZE C ON A.SIID=C.SIID
WHERE A.WAREID='" + v1 + "' GROUP BY A.WAREID,A.COID,B.COLOR HAVING SUM(A.GECOUNT)-SUM(A.MRCOUNT)>0 ORDER BY A.WAREID,A.COID,B.COLOR";

            string sql2 = @"SELECT A.WAREID,A.COID,B.COLOR,A.COLORIMAS FROM TB_SELECTCOLOR A
LEFT JOIN TB_COLOR B ON A.COID=B.COID
WHERE A.WAREID='" + v1 + "'";
            DataTable dt = this.getdt(sql);
            DataTable dt2 = this.getdt(sql2);
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dtk.NewRow();
                    dr["Wareid"] = dt.Rows[i][0].ToString();
                    dr["COID"] = dt.Rows[i][1].ToString();
                    dr["COLOR"] = dt.Rows[i][2].ToString();
                    dtk.Rows.Add(dr);
                }
                for (int i1 = 0; i1 < dtk.Rows.Count; i1++)
                {

                    if (dt2.Rows.Count > 0)
                    {
                        for (j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (dtk.Rows[i1]["wareid"].ToString() == dt2.Rows[j][0].ToString() &&
                                dtk.Rows[i1]["COID"].ToString() == dt2.Rows[j][1].ToString())
                            {

                                dtk.Rows[i1]["COLORIMAS"] = dt2.Rows[j][3].ToString();
                                break;
                            }


                        }

                    }

                }
            }
            return dtk;
        }
        #endregion



        #region addwater_nm
        /**/
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        protected void AddWater(string Path, string Path_sy)
        {
            string addText = "1";
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 60);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Green);

            g.DrawString(addText, f, b, 35, 35);
            g.Dispose();

            image.Save(Path_sy);
            image.Dispose();
        }
        #endregion

        #region addwaterpic_nm
        /**/
        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        /// <param name="Path_sypf">水印图片路径</param>
        protected void AddWaterPic(string Path, string Path_syp, string Path_sypf)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            image.Save(Path_syp);
            image.Dispose();
        }
        #endregion

        #region makethumbnail_nm
        /**/
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        /// 

        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion
        public void Show(string MessageInfo)
        {
            HttpContext.Current.Response.Write("<script language=javascript>alert('" + MessageInfo + "')</script>");
        }


        public void ShowP(string values, string PageURL)
        {
            HttpContext.Current.Response.Write("<script>alert('" + values + "');window.location.href='" + PageURL + "'</script>");
            HttpContext.Current.Response.End();
        }
        public bool juageOne(string sql)
        {
            bool b = false;
            DataTable dt = this.getdt(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    b = true;
                }
            }
            return b;
        }
        #region ExcelPrint
        public void ExcelPrint(DataTable dt2, string BillName, string Printpath)
        {
            int j = 0;
            int n = 0;
            string path = "";
            SaveFileDialog sfdg = new SaveFileDialog();
            //sfdg.DefaultExt = @"D:\xls";
            sfdg.Filter = "Excel(*.xls)|*.xls";
            sfdg.RestoreDirectory = true;
            sfdg.FileName = Printpath;
            //this.Show(sfdg.FileName);
            sfdg.CreatePrompt = true;
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace("/", "-");

            int m = dt2.Rows.Count;
            for (i = 0; i < m; i++)
            {

                if (BillName == "采购单")
                {

                    if (n == 0)
                    {

                        path = "PrintModelForPurchase_o.xls";
                        n = 1;
                    }
                    else if (n == 1)
                    {
                        path = "PrintModelForPurchase_t.xls";
                        n = 2;
                    }
                    else if (n == 2)
                    {
                        path = "PrintModelForPurchase_th.xls";
                        n = 3;
                    }
                    else if (n == 3)
                    {
                        path = "PrintModelForPurchase_f.xls";

                    }
                    string v1 = dt2.Rows[i]["PRINT_ID"].ToString() + "_" + DateTime.Now.ToString("yyyyMMdd HHmmss");
                    //string v1 = dt2.Rows[i]["PRINT_ID"].ToString();
                    File.Copy(Printpath + "PrintModelForPurchase_o.xls", Printpath + v1 + ".xls", true);

                    StringBuilder sqlb = new StringBuilder();
                    string FLKEY = this.numYMD(20, 12, "000000000001", "select * from PRINT_FOR_PURCHASE", "FLKEY", "PT");
                    sqlb.AppendFormat("INSERT INTO PRINT_FOR_PURCHASE(FLKEY,WAREID,OLDFILENAME,PATH,DATE,YEAR,MONTH,DAY)");
                    sqlb.AppendFormat("VALUES (");
                    sqlb.AppendFormat("'{0}','{1}',", FLKEY, dt2.Rows[i]["采购单号"].ToString(), v1 + ".xls");
                    sqlb.AppendFormat("'{0}','{1}',", dt2.Rows[i]["PRINT_ID"].ToString(), v1 + ".xls");
                    sqlb.AppendFormat("'{0}','{1}',", varDate, year);
                    sqlb.AppendFormat("'{0}','{1}'", month, day);
                    sqlb.AppendFormat(")");

                    basec.getcoms(sqlb.ToString());

                    workbook = application.Workbooks.Open(Printpath + v1 + ".xls", Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);/* 13 to parameter 15 */
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                    application.Visible = false;/*140323 use printpreview false to true1/2*/
                    application.ExtendList = false;
                    application.DisplayAlerts = false;
                    application.AlertBeforeOverwriting = false;
                    if (j == 0)
                    {
                        worksheet.Cells[4, 2] = "";
                        worksheet.Cells[4, 11] = "";
                        worksheet.Cells[6, 2] = "";
                        worksheet.Cells[6, 8] = "";
                        worksheet.Cells[6, 12] = "";
                        worksheet.Cells[8, 2] = "";
                        worksheet.Cells[8, 8] = "";
                        worksheet.Cells[8, 12] = "";
                        worksheet.Cells[9, 2] = "";
                        worksheet.Cells[11, 2] = "";
                        worksheet.Cells[11, 8] = "";
                        worksheet.Cells[11, 12] = "";

                        worksheet.Cells[27, 9] = "";
                        worksheet.Cells[35, 7] = "";
                        worksheet.Cells[35, 9] = "";
                        worksheet.Cells[55, 9] = "";
                        worksheet.Cells[57, 1] = "";
                        for (int s1 = 17; s1 <= 26; s1++)
                        {

                            worksheet.Cells[s1, 1] = "";
                            worksheet.Cells[s1, 2] = "";
                            worksheet.Cells[s1, 4] = "";
                            worksheet.Cells[s1, 6] = "";
                            worksheet.Cells[s1, 7] = "";
                            worksheet.Cells[s1, 8] = "";
                            worksheet.Cells[s1, 9] = "";
                            worksheet.Cells[s1, 10] = "";
                            worksheet.Cells[s1, 11] = "";
                            worksheet.Cells[s1, 12] = "";
                            s1 = s1 + 1;

                        }

                    }

                    worksheet.Cells[4, 2] = dt2.Rows[i]["采购日期"].ToString();
                    worksheet.Cells[4, 11] = dt2.Rows[i]["采购单号"].ToString();
                    worksheet.Cells[6, 2] = dt2.Rows[i]["公司名称"].ToString();
                    worksheet.Cells[6, 8] = dt2.Rows[i]["公司联系人"].ToString();
                    worksheet.Cells[6, 12] = dt2.Rows[i]["公司电话"].ToString();
                    worksheet.Cells[8, 2] = dt2.Rows[i]["供应商名称"].ToString();
                    worksheet.Cells[8, 8] = dt2.Rows[i]["联系人"].ToString();
                    worksheet.Cells[8, 12] = dt2.Rows[i]["电话"].ToString();
                    worksheet.Cells[9, 2] = dt2.Rows[i]["地址"].ToString();
                    worksheet.Cells[11, 2] = dt2.Rows[i]["收货地址"].ToString();
                    worksheet.Cells[11, 8] = dt2.Rows[i]["收货人"].ToString();
                    worksheet.Cells[11, 12] = dt2.Rows[i]["收货人电话"].ToString();
                    worksheet.Cells[27, 9] = dt2.Rows[dt2.Rows.Count - 1]["合计含税金额"].ToString();
                    worksheet.Cells[35, 7] = dt2.Rows[i]["付款方式"].ToString();
                    worksheet.Cells[35, 9] = dt2.Rows[i]["付款条件"].ToString();
                    worksheet.Cells[55, 9] = dt2.Rows[i]["供应商名称"].ToString();
                    worksheet.Cells[57, 1] = dt2.Rows[i]["采购日期"].ToString();

                    worksheet.Cells[17, 1] = dt2.Rows[i]["项次"].ToString();
                    worksheet.Cells[17, 2] = dt2.Rows[i]["客户料号"].ToString();
                    worksheet.Cells[17, 4] = dt2.Rows[i]["品名"].ToString();
                    worksheet.Cells[17, 6] = dt2.Rows[i]["采购数量"].ToString();
                    worksheet.Cells[17, 8] = dt2.Rows[i]["采购单价"].ToString();
                    worksheet.Cells[17, 9] = dt2.Rows[i]["未税金额"].ToString();
                    worksheet.Cells[17, 10] = dt2.Rows[i]["含税金额"].ToString();
                    worksheet.Cells[17, 11] = dt2.Rows[i]["需求日期"].ToString();
                    worksheet.Cells[17, 12] = dt2.Rows[i]["备注"].ToString();
                    if (i + 1 < m)
                    {
                        worksheet.Cells[19, 1] = dt2.Rows[i + 1]["项次"].ToString();
                        worksheet.Cells[19, 2] = dt2.Rows[i + 1]["客户料号"].ToString();
                        worksheet.Cells[19, 4] = dt2.Rows[i + 1]["品名"].ToString();
                        worksheet.Cells[19, 6] = dt2.Rows[i + 1]["采购数量"].ToString();
                        worksheet.Cells[19, 8] = dt2.Rows[i + 1]["采购单价"].ToString();
                        worksheet.Cells[19, 9] = dt2.Rows[i + 1]["未税金额"].ToString();
                        worksheet.Cells[19, 10] = dt2.Rows[i + 1]["含税金额"].ToString();
                        worksheet.Cells[19, 11] = dt2.Rows[i + 1]["需求日期"].ToString();
                        worksheet.Cells[19, 12] = dt2.Rows[i + 1]["备注"].ToString();
                    }
                    if (i + 2 < m)
                    {
                        worksheet.Cells[21, 1] = dt2.Rows[i + 2]["项次"].ToString();
                        worksheet.Cells[21, 2] = dt2.Rows[i + 2]["客户料号"].ToString();
                        worksheet.Cells[21, 4] = dt2.Rows[i + 2]["品名"].ToString();
                        worksheet.Cells[21, 6] = dt2.Rows[i + 2]["采购数量"].ToString();
                        worksheet.Cells[21, 8] = dt2.Rows[i + 2]["采购单价"].ToString();
                        worksheet.Cells[21, 9] = dt2.Rows[i + 2]["未税金额"].ToString();
                        worksheet.Cells[21, 10] = dt2.Rows[i + 2]["含税金额"].ToString();
                        worksheet.Cells[21, 11] = dt2.Rows[i + 2]["需求日期"].ToString();
                        worksheet.Cells[21, 12] = dt2.Rows[i + 2]["备注"].ToString();
                    }
                    if (i + 3 < m)
                    {
                        worksheet.Cells[23, 1] = dt2.Rows[i + 3]["项次"].ToString();
                        worksheet.Cells[23, 2] = dt2.Rows[i + 3]["客户料号"].ToString();
                        worksheet.Cells[23, 4] = dt2.Rows[i + 3]["品名"].ToString();
                        worksheet.Cells[23, 6] = dt2.Rows[i + 3]["采购数量"].ToString();
                        worksheet.Cells[23, 8] = dt2.Rows[i + 3]["采购单价"].ToString();
                        worksheet.Cells[23, 9] = dt2.Rows[i + 3]["未税金额"].ToString();
                        worksheet.Cells[23, 10] = dt2.Rows[i + 3]["含税金额"].ToString();
                        worksheet.Cells[23, 11] = dt2.Rows[i + 3]["需求日期"].ToString();
                        worksheet.Cells[23, 12] = dt2.Rows[i + 3]["备注"].ToString();
                    }
                    if (i + 4 < m)
                    {
                        worksheet.Cells[25, 1] = dt2.Rows[i + 4]["项次"].ToString();
                        worksheet.Cells[25, 2] = dt2.Rows[i + 4]["客户料号"].ToString();
                        worksheet.Cells[25, 4] = dt2.Rows[i + 4]["品名"].ToString();
                        worksheet.Cells[25, 6] = dt2.Rows[i + 4]["采购数量"].ToString();
                        worksheet.Cells[25, 8] = dt2.Rows[i + 4]["采购单价"].ToString();
                        worksheet.Cells[25, 9] = dt2.Rows[i + 4]["未税金额"].ToString();
                        worksheet.Cells[25, 10] = dt2.Rows[i + 4]["含税金额"].ToString();
                        worksheet.Cells[25, 11] = dt2.Rows[i + 4]["需求日期"].ToString();
                        worksheet.Cells[25, 12] = dt2.Rows[i + 4]["备注"].ToString();
                    }

                    workbook.Save();
                    //worksheet.PrintPreview(false);/*140323 use printpreview false to true2/2*/
                    //worksheet.PrintOut(1, 1, 1, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);/无需打印
                    //csharpExcelPrint(sfdg.FileName);
                    i = i + 4;

                    //workbook.Save();
                    //csharpExcelPrint(sfdg.FileName);
                }/*above purchase*/
                else
                {
                    if (n == 0)
                    {
                        path = "PrintModelForSellTable_o.xls";
                        n = 1;
                    }
                    else if (n == 1)
                    {
                        path = "PrintModelForSellTable_t.xls";
                        n = 2;
                    }

                    workbook = application.Workbooks.Open(path, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);/* 13 to parameter 15 */
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                    application.Visible = false;/*140323 use printpreview false to true1/2*/
                    application.ExtendList = false;
                    application.DisplayAlerts = false;
                    application.AlertBeforeOverwriting = false;
                    if (j == 0)
                    {
                        worksheet.Cells[6, 3] = "";
                        worksheet.Cells[6, 10] = "";
                        worksheet.Cells[7, 3] = "";
                        worksheet.Cells[7, 10] = "";
                        worksheet.Cells[8, 3] = "";
                        worksheet.Cells[8, 10] = "";
                        worksheet.Cells[9, 3] = "";

                        worksheet.Cells[22, 7] = "";
                        worksheet.Cells[22, 8] = "";
                        worksheet.Cells[57, 7] = "";
                        worksheet.Cells[57, 8] = "";

                        worksheet.Cells[41, 3] = "";
                        worksheet.Cells[41, 10] = "";
                        worksheet.Cells[42, 3] = "";
                        worksheet.Cells[42, 10] = "";
                        worksheet.Cells[43, 3] = "";
                        worksheet.Cells[43, 10] = "";
                        worksheet.Cells[44, 3] = "";
                        for (int s1 = 12; s1 <= 20; s1++)
                        {

                            worksheet.Cells[s1, 1] = "";
                            worksheet.Cells[s1, 2] = "";
                            worksheet.Cells[s1, 4] = "";
                            worksheet.Cells[s1, 5] = "";
                            worksheet.Cells[s1, 7] = "";
                            worksheet.Cells[s1, 8] = "";
                            worksheet.Cells[s1, 9] = "";
                            worksheet.Cells[s1, 10] = "";
                            worksheet.Cells[s1, 12] = "";
                            s1 = s1 + 1;

                        }
                        for (int s1 = 47; s1 <= 55; s1++)
                        {

                            worksheet.Cells[s1, 1] = "";
                            worksheet.Cells[s1, 2] = "";
                            worksheet.Cells[s1, 4] = "";
                            worksheet.Cells[s1, 5] = "";
                            worksheet.Cells[s1, 7] = "";
                            worksheet.Cells[s1, 8] = "";
                            worksheet.Cells[s1, 9] = "";
                            worksheet.Cells[s1, 10] = "";
                            worksheet.Cells[s1, 12] = "";
                            s1 = s1 + 1;

                        }

                    }
                    worksheet.Cells[6, 3] = dt2.Rows[i]["客户名称"].ToString();
                    worksheet.Cells[6, 10] = dt2.Rows[i]["销货单号"].ToString();
                    worksheet.Cells[7, 3] = dt2.Rows[i]["送货地址"].ToString();
                    worksheet.Cells[7, 10] = dt2.Rows[i]["联系人"].ToString();
                    worksheet.Cells[8, 3] = dt2.Rows[i]["联系电话"].ToString();
                    worksheet.Cells[8, 10] = dt2.Rows[i]["销货日期"].ToString();
                    worksheet.Cells[9, 3] = dt2.Rows[i]["客户订单号"].ToString();

                    worksheet.Cells[41, 3] = dt2.Rows[i]["客户名称"].ToString();
                    worksheet.Cells[41, 10] = dt2.Rows[i]["销货单号"].ToString();
                    worksheet.Cells[42, 3] = dt2.Rows[i]["送货地址"].ToString();
                    worksheet.Cells[42, 10] = dt2.Rows[i]["联系人"].ToString();
                    worksheet.Cells[43, 3] = dt2.Rows[i]["联系电话"].ToString();
                    worksheet.Cells[43, 10] = dt2.Rows[i]["销货日期"].ToString();
                    worksheet.Cells[44, 3] = dt2.Rows[i]["客户订单号"].ToString();

                    worksheet.Cells[22, 7] = dt2.Rows[m]["合计销货数量"].ToString();
                    worksheet.Cells[22, 8] = dt2.Rows[m]["合计FREE数量"].ToString();
                    worksheet.Cells[57, 7] = dt2.Rows[m]["合计销货数量"].ToString();
                    worksheet.Cells[57, 8] = dt2.Rows[m]["合计FREE数量"].ToString();

                    worksheet.Cells[12, 1] = dt2.Rows[i]["项次"].ToString();
                    worksheet.Cells[12, 2] = dt2.Rows[i]["客户料号"].ToString();
                    worksheet.Cells[12, 4] = dt2.Rows[i]["品名"].ToString();
                    worksheet.Cells[12, 5] = dt2.Rows[i]["料号"].ToString();
                    worksheet.Cells[12, 7] = dt2.Rows[i]["销货数量"].ToString();
                    worksheet.Cells[12, 8] = dt2.Rows[i]["FREE数量"].ToString();
                    /*worksheet.Cells[12, 9] = dt2.Rows[i]["计量单位"].ToString();*/
                    worksheet.Cells[12, 10] = dt2.Rows[i]["批号"].ToString();
                    worksheet.Cells[12, 12] = dt2.Rows[i]["备注"].ToString();


                    if (i + 1 < m)
                    {
                        worksheet.Cells[14, 1] = dt2.Rows[i + 1]["项次"].ToString();
                        worksheet.Cells[14, 2] = dt2.Rows[i + 1]["客户料号"].ToString();
                        worksheet.Cells[14, 4] = dt2.Rows[i + 1]["品名"].ToString();
                        worksheet.Cells[14, 5] = dt2.Rows[i + 1]["料号"].ToString();
                        worksheet.Cells[14, 7] = dt2.Rows[i + 1]["销货数量"].ToString();
                        worksheet.Cells[14, 8] = dt2.Rows[i + 1]["FREE数量"].ToString();
                        /*worksheet.Cells[14, 9] = dt2.Rows[i+1]["计量单位"].ToString();*/
                        worksheet.Cells[14, 10] = dt2.Rows[i + 1]["批号"].ToString();
                        worksheet.Cells[14, 14] = dt2.Rows[i + 1]["备注"].ToString();
                    }
                    if (i + 2 < m)
                    {
                        worksheet.Cells[16, 1] = dt2.Rows[i + 2]["项次"].ToString();
                        worksheet.Cells[16, 2] = dt2.Rows[i + 2]["客户料号"].ToString();
                        worksheet.Cells[16, 4] = dt2.Rows[i + 2]["品名"].ToString();
                        worksheet.Cells[16, 5] = dt2.Rows[i + 2]["料号"].ToString();
                        worksheet.Cells[16, 7] = dt2.Rows[i + 2]["销货数量"].ToString();
                        worksheet.Cells[16, 8] = dt2.Rows[i + 2]["FREE数量"].ToString();
                        /*worksheet.Cells[16, 9] = dt2.Rows[i+2]["计量单位"].ToString();*/
                        worksheet.Cells[16, 10] = dt2.Rows[i + 2]["批号"].ToString();
                        worksheet.Cells[16, 16] = dt2.Rows[i + 2]["备注"].ToString();
                    }
                    if (i + 3 < m)
                    {
                        worksheet.Cells[18, 1] = dt2.Rows[i + 3]["项次"].ToString();
                        worksheet.Cells[18, 2] = dt2.Rows[i + 3]["客户料号"].ToString();
                        worksheet.Cells[18, 4] = dt2.Rows[i + 3]["品名"].ToString();
                        worksheet.Cells[18, 5] = dt2.Rows[i + 3]["料号"].ToString();
                        worksheet.Cells[18, 7] = dt2.Rows[i + 3]["销货数量"].ToString();
                        worksheet.Cells[18, 8] = dt2.Rows[i + 3]["FREE数量"].ToString();
                        /*worksheet.Cells[18, 9] = dt2.Rows[i+3]["计量单位"].ToString();*/
                        worksheet.Cells[18, 10] = dt2.Rows[i + 3]["批号"].ToString();
                        worksheet.Cells[18, 18] = dt2.Rows[i + 3]["备注"].ToString();
                    }
                    if (i + 4 < m)
                    {
                        worksheet.Cells[20, 1] = dt2.Rows[i + 4]["项次"].ToString();
                        worksheet.Cells[20, 2] = dt2.Rows[i + 4]["客户料号"].ToString();
                        worksheet.Cells[20, 4] = dt2.Rows[i + 4]["品名"].ToString();
                        worksheet.Cells[20, 5] = dt2.Rows[i + 4]["料号"].ToString();
                        worksheet.Cells[20, 7] = dt2.Rows[i + 4]["销货数量"].ToString();
                        worksheet.Cells[20, 8] = dt2.Rows[i + 4]["FREE数量"].ToString();
                        /*worksheet.Cells[20, 9] = dt2.Rows[i+4]["计量单位"].ToString();*/
                        worksheet.Cells[20, 10] = dt2.Rows[i + 4]["批号"].ToString();
                        worksheet.Cells[20, 20] = dt2.Rows[i + 4]["备注"].ToString();

                    }
                    if (i + 5 < m)
                    {
                        worksheet.Cells[47, 1] = dt2.Rows[i + 5]["项次"].ToString();
                        worksheet.Cells[47, 2] = dt2.Rows[i + 5]["客户料号"].ToString();
                        worksheet.Cells[47, 4] = dt2.Rows[i + 5]["品名"].ToString();
                        worksheet.Cells[47, 5] = dt2.Rows[i + 5]["料号"].ToString();
                        worksheet.Cells[47, 7] = dt2.Rows[i + 5]["销货数量"].ToString();
                        worksheet.Cells[47, 8] = dt2.Rows[i + 5]["FREE数量"].ToString();
                        /*worksheet.Cells[47, 9] = dt2.Rows[i+5]["计量单位"].ToString();*/
                        worksheet.Cells[47, 10] = dt2.Rows[i + 5]["批号"].ToString();
                        worksheet.Cells[47, 47] = dt2.Rows[i + 5]["备注"].ToString();

                    }
                    if (i + 6 < m)
                    {
                        worksheet.Cells[49, 1] = dt2.Rows[i + 6]["项次"].ToString();
                        worksheet.Cells[49, 2] = dt2.Rows[i + 6]["客户料号"].ToString();
                        worksheet.Cells[49, 4] = dt2.Rows[i + 6]["品名"].ToString();
                        worksheet.Cells[49, 5] = dt2.Rows[i + 6]["料号"].ToString();
                        worksheet.Cells[49, 7] = dt2.Rows[i + 6]["销货数量"].ToString();
                        worksheet.Cells[49, 8] = dt2.Rows[i + 6]["FREE数量"].ToString();
                        /*worksheet.Cells[49, 9] = dt2.Rows[i+5]["计量单位"].ToString();*/
                        worksheet.Cells[49, 10] = dt2.Rows[i + 6]["批号"].ToString();
                        worksheet.Cells[49, 49] = dt2.Rows[i + 6]["备注"].ToString();

                    }
                    if (i + 7 < m)
                    {
                        worksheet.Cells[51, 1] = dt2.Rows[i + 7]["项次"].ToString();
                        worksheet.Cells[51, 2] = dt2.Rows[i + 7]["客户料号"].ToString();
                        worksheet.Cells[51, 4] = dt2.Rows[i + 7]["品名"].ToString();
                        worksheet.Cells[51, 5] = dt2.Rows[i + 7]["料号"].ToString();
                        worksheet.Cells[51, 7] = dt2.Rows[i + 7]["销货数量"].ToString();
                        worksheet.Cells[51, 8] = dt2.Rows[i + 7]["FREE数量"].ToString();
                        /*worksheet.Cells[51, 9] = dt2.Rows[i+5]["计量单位"].ToString();*/
                        worksheet.Cells[51, 10] = dt2.Rows[i + 7]["批号"].ToString();
                        worksheet.Cells[51, 51] = dt2.Rows[i + 7]["备注"].ToString();

                    }
                    if (i + 8 < m)
                    {
                        worksheet.Cells[53, 1] = dt2.Rows[i + 8]["项次"].ToString();
                        worksheet.Cells[53, 2] = dt2.Rows[i + 8]["客户料号"].ToString();
                        worksheet.Cells[53, 4] = dt2.Rows[i + 8]["品名"].ToString();
                        worksheet.Cells[53, 5] = dt2.Rows[i + 8]["料号"].ToString();
                        worksheet.Cells[53, 7] = dt2.Rows[i + 8]["销货数量"].ToString();
                        worksheet.Cells[53, 8] = dt2.Rows[i + 8]["FREE数量"].ToString();
                        /*worksheet.Cells[53, 9] = dt2.Rows[i+5]["计量单位"].ToString();*/
                        worksheet.Cells[53, 10] = dt2.Rows[i + 8]["批号"].ToString();
                        worksheet.Cells[53, 53] = dt2.Rows[i + 8]["备注"].ToString();

                    }
                    if (i + 9 < m)
                    {
                        worksheet.Cells[55, 1] = dt2.Rows[i + 9]["项次"].ToString();
                        worksheet.Cells[55, 2] = dt2.Rows[i + 9]["客户料号"].ToString();
                        worksheet.Cells[55, 4] = dt2.Rows[i + 9]["品名"].ToString();
                        worksheet.Cells[55, 5] = dt2.Rows[i + 9]["料号"].ToString();
                        worksheet.Cells[55, 7] = dt2.Rows[i + 9]["销货数量"].ToString();
                        worksheet.Cells[55, 8] = dt2.Rows[i + 9]["FREE数量"].ToString();
                        /*worksheet.Cells[55, 9] = dt2.Rows[i+5]["计量单位"].ToString();*/
                        worksheet.Cells[55, 10] = dt2.Rows[i + 9]["批号"].ToString();
                        worksheet.Cells[55, 55] = dt2.Rows[i + 9]["备注"].ToString();

                    }
                    workbook.Save();
                    //worksheet.PrintPreview(false);/*140323 use printpreview false to true2/2*/
                    worksheet.PrintOut(1, 1, 1, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //csharpExcelPrint(sfdg.FileName);
                    i = i + 9;
                    //workbook.Save();
                    //csharpExcelPrint(sfdg.FileName);

                }
            }
            IFExecution_SUCCESS = true;
            application.Quit();
            worksheet = null;
            workbook = null;
            application = null;
            GC.Collect();
        }
        #endregion


        #region csharpExcelPrint
        private void csharpExcelPrint(string path)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = path;
            p.StartInfo.Verb = "print";
            p.Start();
        }
        #endregion
        public bool JuageSourceStatus(string ORID)
        {

            bool b1 = false;
            string v11 = this.getOnlyString(@"SELECT A.SOURCESTATUS FROM PURCHASE_MST A LEFT JOIN ORDER_MST B 
            ON A.PUID=B.PUID WHERE B.ORID='" + ORID + "'");
            if (!string.IsNullOrEmpty(v11))
            {

                b1 = true;

            }
            return b1;

        }
        public byte[] GetMD5(string Password)
        {
            byte[] Encrypt = HashAlgorithm.Create().ComputeHash(Encoding.Unicode.GetBytes(Password));
            return Encrypt;
        }
        #region JuageOrderPurchaseStatus
        public bool JuageOrderOrPurchaseStatus(string ORIDorPUID, int OrderOrPurchase)
        {
            bool b = true;
            DataTable dt = new DataTable();
            if (OrderOrPurchase == 0)
            {
                dt = this.getdt("SELECT * FROM ORDER_DET WHERE ORID='" + ORIDorPUID + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ORDERSTATUS_DET"].ToString() != "CLOSE")
                    {
                        b = false;
                        break;

                    }
                }

            }
            else
            {
                dt = this.getdt("SELECT * FROM PURCHASE_DET WHERE PUID='" + ORIDorPUID + "'");
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["PURCHASESTATUS_DET"].ToString() != "CLOSE")
                    {
                        b = false;
                        break;

                    }

                }

            }

            return b;

        }
        #endregion

        #region JuageIfAllowDelete
        public bool JuageIfAllowDeleteEMID(string EMID)
        {
            bool b = false;
            if (this.exists("SELECT * FROM COMPANYINFO_MST WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在公司信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM COMPANYINFO_DET WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在公司信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM CUSTOMERINFO_MST WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在客户信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM CUSTOMERINFO_DET WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在客户信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM GODE WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在入库信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM MATERE WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在销货信息中使用了，不允许删除！";

            }

            else if (this.exists("SELECT * FROM ORDER_MST WHERE MAKERID='" + EMID + "' OR SALEID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在订单信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM PURCHASE_MST WHERE MAKERID='" + EMID + "' OR PURID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在采购信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM PURCHASEGODE_MST WHERE MAKERID='" + EMID + "' OR GODERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在入库信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM PURCHASEUNITPRICE WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在采购核价信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM RIGHTLIST WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在权限信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM SELLTABLE_MST WHERE MAKERID='" + EMID + "' OR SELLERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在销货信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM SELLUNITPRICE WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在销售核价信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM STORAGEINFO WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在仓库信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM SUPPLIERINFO_MST WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在供应商信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM SUPPLIERINFO_DET WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在供应商信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM USERINFO WHERE EMID='" + EMID + "' OR MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在用户信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM WAREFILE WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在品号相关文件信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM WAREINFO WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在品号信息中使用了，不允许删除！";

            }
            if (this.exists("SELECT * FROM LIMITE_SEARCH WHERE MAKERID='" + EMID + "'"))
            {
                b = true;
                ErrowInfo = "该工号已经在核价权限过滤信息中使用了，不允许删除！";
            }

            return b;
        }
        #endregion

        #region JuageIfAllowDeleteWareID
        public bool JuageIfAllowDeleteWareID(string WareID)
        {
            bool b = false;
            if (this.exists("SELECT * FROM ORDER_DET WHERE WareID='" + WareID + "' "))
            {
                b = true;
                ErrowInfo = "该品号已经在订单信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM PURCHASE_DET WHERE WareID='" + WareID + "'"))
            {
                b = true;
                ErrowInfo = "该品号已经在采购信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM SELLUNITPRICE WHERE WareID='" + WareID + "'"))
            {
                b = true;
                ErrowInfo = "该品号已经在销售核价信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM PURCHASEUNITPRICE WHERE WareID='" + WareID + "'"))
            {
                b = true;
                ErrowInfo = "该品号已经在采购核价信息中使用了，不允许删除！";

            }
            else if (this.exists("SELECT * FROM QUALITY_INFO WHERE WareID='" + WareID + "'"))
            {
                b = true;
                ErrowInfo = "该品号已经在品质履历信息中使用了，不允许删除！";

            }
            return b;
        }
        #endregion
        #region RETURN_NOHAVE_REPEAT_DT
        public DataTable RETURN_NOHAVE_REPEAT_DT(DataTable dt, string COLUMN_NAME)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("VALUE", typeof(string));
            if (dt.Rows.Count > 0)
            {
                dt = GET_DT_TO_DV_TO_DT(dt, "", COLUMN_NAME + " IS NOT NULL");
                foreach (DataRow dr1 in dt.Rows)
                {
                    if (dr1[COLUMN_NAME].ToString() == "")
                    {

                    }
                    else
                    {
                        DataTable dtt1 = GET_DT_TO_DV_TO_DT(dtt, "", string.Format("VALUE='{0}'", dr1[COLUMN_NAME].ToString()));
                        if (dtt1.Rows.Count > 0)
                        {

                        }
                        else
                        {
                            DataRow dr = dtt.NewRow();
                            dr["VALUE"] = dr1[COLUMN_NAME].ToString();
                            dtt.Rows.Add(dr);
                        }
                    }
                }
            }

            return dtt;
        }
        #endregion
        public bool JuagePrintModelIfExists(int n, string PrintBillName)
        {
            bool b = true;


            string[] SellTableBill = new string[] { "PrintModelForSellTable_o.xls", "PrintModelForSellTable_t.xls" };
            string[] PurchaseBill = new string[] { "PrintModelForPurchase_o.xls", "PrintModelForPurchase_t.xls", "PrintModelForPurchase_th.xls", "PrintModelForPurchase_f.xls" };
            if (PrintBillName == "SE")
            {
                for (i = 0; i < n; i++)
                {
                    if (!File.Exists("d:\\" + SellTableBill[i]))
                    {

                        ErrowInfo = "指定路径不存在打印模版！" + SellTableBill[i];
                        b = false;
                        break;
                    }
                }
            }
            else
            {

                for (i = 0; i < n; i++)
                {

                    if (!File.Exists("d:\\" + PurchaseBill[i]))
                    {

                        ErrowInfo = "指定路径不存在打印模版！" + PurchaseBill[i];
                        b = false;
                        break;
                    }
                }


            }
            return b;
        }
        #region  GET_IFExecutionSUCCESS_HINT_INFO
        public string GET_IFExecutionSUCCESS_HINT_INFO(bool SET_IFExecutionSUCCESS)
        {
            string v = "";
            if (SET_IFExecutionSUCCESS == true)
            {

                v = "已保存成功!";
            }
            return v;
        }
        #endregion
        #region GET_DT_TO_DV_TO_DT
        public DataTable GET_DT_TO_DV_TO_DT(DataTable dt, string Sort, string RowFilter)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = RowFilter;
            dv.Sort = Sort;
            dt = dv.ToTable();
            return dt;
        }
        #endregion
    }
}
