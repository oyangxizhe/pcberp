using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using XizheC;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using OfficeOpenXml;

namespace WPSS.SellManage
{
    public partial class SellTableT : System.Web.UI.Page
    {

        DataTable dt;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5;
        DataTable dt6;
        List<string> list;
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        StringBuilder sqb;
     
        private static string _IDO;
        public static string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private static string _ADD_OR_UPDATE;
        public static string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        PrintSellTableBill print = new PrintSellTableBill();
        CORDER corder = new CORDER();
        CRECEIVABLES creceivables = new CRECEIVABLES();
        int i;
        public static string[] NEWID = new string[] { "", "" };
        public static string[] GETID = new string[] { "", "" };
        public static string[] str2 = new string[] { "" };
        string SEKEY;
        private string emid;
        private string usid;
        private string ename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
                usid = Request.Cookies["cookiename"].Values["usid"].ToString();
                emid = Request.Cookies["cookiename"].Values["emid"].ToString();
                ename = Request.Cookies["cookiename"].Values["ename"].ToString();
                jemid.Value = emid;
                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    if (NEWID[0] != "")
                    {
                        Text1.Value = NEWID[0];
                        NEWID[0] = "";
                    }
                    else
                    {
                        Text1.Value = GETID[0];
                        Text2.Value = GETID[1];
                    }
                 
                    if(GETID[0]!="")
                    {
                        Text1.Value = GETID[0];
                        Text2.Value = GETID[1];
                    }
                   bind();
                }
            }
            try
            {

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.Redirect("/default.aspx");
            }

        }
        #region bin
        protected void bind()
        {


            x.Value = "";
            x2.Value = "";

            CUKEY.Value = "";
            btnSure.ForeColor = System.Drawing.Color.Blue;
            ControlFileDisplay.Value = "";
            Text3.Value = DateTime.Now.ToString("yyy-MM-dd");
            Text4.Value = emid;
            Label1.Text = ename;

            if (Text2.Value == "")
                return;

            sqb = new StringBuilder();
         
            //取得订单相关数据含累计销货数量，订单项最大库存数量及所在的仓库批号
            sqb.AppendFormat(print.sqlf, Text2.Value, Text1.Value);

            //取得销货单信息,含统计销货单合计未税金额等
            sqb.AppendFormat(print.sqlo + " WHERE A.SEID='" + Text1.Value + "' ORDER BY A.SEKEY ASC;");

            //取得订单附件
            sqb.AppendFormat(@"
SELECT 
DISTINCT(A.WAREID) AS WAREID,
B.FLKEY AS FLKEY,
B.OLDFILENAME AS OLDFILENAME FROM 
ORDER_DET A 
LEFT JOIN WAREFILE B ON A.WAREID=B.WAREID 
WHERE A.ORID IN ({0}) AND B.FLKEY IS NOT NULL ORDER BY A.WAREID,B.FLKEY,B.OLDFILENAME;", Text2.Value); ;

            //使用自定义标题显示功能
            sqb.AppendFormat("select * from set_showname;");

            //根据登录用户USID取得制单人员工号
            sqb.AppendFormat("SELECT * FROM USERINFO A LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID WHERE A.USID='" + usid + "';");

            //取得用户权限
            sqb.AppendFormat("SELECT * FROM RIGHTLIST WHERE USID='" + usid + "' AND NODE_NAME='销货单'");
            SqlConnection sqlcon = bc.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(sqb.ToString(), sqlcon);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcom);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);


            dt = dataSet.Tables[0];//取得订单相关数据含累计销货数量，订单项最大库存数量及所在的仓库批号
            DataTable dt1 = dataSet.Tables[1];  //取得销货单信息,含统计销货单合计未税金额等
            DataTable dt2 = dataSet.Tables[2];//取得订单附件
            DataTable dt3 = dataSet.Tables[3]; //使用自定义标题显示功能
            DataTable dt4 = dataSet.Tables[4]; //根据登录用户USID取得制单人员工号
            dt5 = dataSet.Tables[5];  //取得用户权限

            sqlcom.Dispose();
            sqlcon.Close();
            sqlcon.Dispose();

            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();
         
          
            if (dt.Rows.Count > 0)
            {
                x2.Value = "exists";
                CUKEY.Value = dt.Rows[0]["CUKEY"].ToString();
                Text5.Value = dt.Rows[0]["CNAME"].ToString();
                Text6.Value = dt.Rows[0]["CONTACT"].ToString();
                Text10.Value = dt.Rows[0]["PHONE"].ToString();
                Text11.Value = dt.Rows[0]["ADDRESS"].ToString();
            }

            if (dt1.Rows.Count > 0)
            {
                string v8 = dt1.Rows[0][2].ToString();
                string v9 = dt1.Rows[0][3].ToString();
                string v10 = dt1.Rows[0][4].ToString();
                Text7.Value = dt1.Compute("SUM(未税金额)", "").ToString();
                Text8.Value = dt1.Compute("SUM(税额)", "").ToString();
                Text9.Value = dt1.Compute("SUM(含税金额)", "").ToString();
                x.Value = Convert.ToString(1);
                Text3.Value = dt1.Rows[0]["销货日期"].ToString();
                Text4.Value = dt1.Rows[0]["销货员工号"].ToString();
                Label1.Text = dt1.Rows[0]["销货员"].ToString();
                Text6.Value = dt1.Rows[0]["联系人"].ToString();
                Text10.Value = dt1.Rows[0]["联系电话"].ToString();
                Text11.Value = dt1.Rows[0]["送货地址"].ToString();
                CUKEY.Value = dt1.Rows[0]["CUKEY"].ToString();
                Text12.Value = dt1.Rows[0]["快递单号"].ToString();
                //DropDownList5.Text = dt1.Rows[0]["公司抬头"].ToString();
                if (dt1.Rows[0]["状态"].ToString() == "RECONCILE" || dt1.Rows[0]["状态"].ToString() == "INVOICE")
                {
                    Submit5.Value= "已对帐";
                }
                else
                {
                    Submit5.Value = "确认对帐";
                }
                basec.GenerateColumns(GridView2, dt1).DataSource = dt1;//因为要使用自定义标题列名称，所以要产生列名,使其有HeaderText属性
                basec.GenerateColumns(GridView2, dt1).DataBind();
                x.Value = Convert.ToString(1);
            }
            else
            {
                Text7.Value = "";
                Text8.Value = "";
                Text9.Value = "";
                //DropDownList5.Text = "伟峰";
            }

            if (dt2.Rows.Count > 0)
            {
                GridView3.DataSource = dt2;
                GridView3.DataKeyNames = new string[] { "FLKEY" };
                GridView3.DataBind();
                ControlFileDisplay.Value = Convert.ToString(1);
            }
            else
            {
                GridView3.DataSource = null;
            }

            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (GridView1.Columns[i].HeaderText == "料号")
                    {
                        GridView1.Columns[i].HeaderText = dt3.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "品名")
                    {
                        GridView1.Columns[i].HeaderText = dt3.Rows[0]["wname"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "客户料号")
                    {
                        GridView1.Columns[i].HeaderText = dt3.Rows[0]["cwareid"].ToString();
                    }
                }
                for (int i = 0; i < GridView2.Columns.Count; i++)
                {
                    if (GridView2.Columns[i].HeaderText == "料号")
                    {
                        GridView2.Columns[i].HeaderText = dt3.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "品名")
                    {
                        GridView2.Columns[i].HeaderText = dt3.Rows[0]["wname"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "客户料号")
                    {
                        GridView2.Columns[i].HeaderText = dt3.Rows[0]["cwareid"].ToString();
                    }
                }
            }
            GridView1.DataBind();
            GridView2.DataBind();

            //根据登录用户USID取得制单人员工号
            if (dt4.Rows.Count > 0)
            {
                Text4.Value = emid;
                Label1.Text = dt4.Rows[0]["ename"].ToString();
            }

            //有关权限校验
            if (dt5.Rows[0]["ADD_NEW"].ToString() == "Y")
            {
                Submit1.Visible = true;
            }
            else
            {
                Submit1.Visible = false;
            }
            if (dt5.Rows[0]["ADD_NEW"].ToString() == "Y" || dt5.Rows[0]["EDIT"].ToString() == "Y")
            {
                Submit2.Visible = true;
            }
            else
            {
                Submit2.Visible = false;
            }

            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
        }
        #endregion

        protected void btnSure_Click(object sender, EventArgs e)
        {
            string[] array = Text2.Value.Split(',');
            List<string> listCname = new List<string>();
            List<string> listORID = new List<string>();
            string str = "";
            //去掉重复的订单号
            for (int i = 0; i < array.Length; i++)
            {
                if (!listCname.Contains(array[i].Substring(array[i].IndexOf('-') + 1, array[i].Length - 12)))
                {
                    listCname.Add(array[i].Substring(array[i].IndexOf('-') + 1, array[i].Length - 12));
                }
                if (!listORID.Contains(array[i].Substring(0, 10)))
                {
                    listORID.Add(array[i].Substring(0, 10));
                    str += "'" + array[i].Substring(0, 10) + "'" + ",";
                }
                if (listCname.Count > 1)
                {
                    hint.Value = "选取的订单号来自不同的客户 " + listCname[0] + "与" + listCname[1];
                    return;
                }
            }
            //去重后的订单号重新赋值给Text2
            if (Text2.Value.Length > 0)
                Text2.Value = str.Substring(0, str.Length - 1);
            bind();
        }

        protected void ClearText()
        {
            Text1.Value = "";
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Label1.Text = "";
            Text6.Value = "";
            Text10.Value = "";
            Text11.Value = "";
            cuid.Value = "";
            //DropDownList5.Text = "伟峰";
        }
        private void hint_bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }

        }



        private bool juage_if_write(int count)
        {
            bool b = false;
            for (int k = 0; k < count; k++)
            {
                if (ac1(k) == 0)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        private bool juage_if_write1(int count)/*写入数据至少有一项的销货数量是不为0的，因为若有多项，多次销，销过的为0是正常的，但又不能把销货数量为0的项写入表中*/
        {
            bool b = true;
            for (int k = 0; k < count; k++)
            {
                string SECOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
                string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[11].FindControl("TextBox12")).Text;
                string BATCHID = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;
                string NOSECOUNT = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
                string STORAGECOUNT = ((TextBox)GridView1.Rows[k].Cells[13].FindControl("TextBox14")).Text;
                string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                string k1 = bc.CheckingWareidAndStorage(WAREID, STORAGENAME, BATCHID);
                string FREECOUNT = ((TextBox)GridView1.Rows[k].Cells[16].FindControl("TextBox17")).Text;
                if (decimal.Parse(SECOUNT) != 0)
                {

                    b = false;
                    break;
                }
            }
            return b;
        }
        #region add2

        private void add2(int count)
        {

            int k;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            sqb = new StringBuilder();
      
            int i1;
            string SEKEY1 = bc.numYMD(20, 12, "000000000001", "select * from SELLTABLE_DET", "SEKEY", "SE");
            i1 = Convert.ToInt32(SEKEY1.Substring(8, 12));
            //建一个list去接住符合写入条件的订单号，然后更新订单状态，因为订单号可以是多个
            list = new List<string>();
            if (!bc.exists("SELECT SEID FROM SELLTABLE_MST WHERE SEID='" + Text1.Value + "'"))
            {
                sqb.AppendFormat(" ;INSERT INTO SELLTABLE_MST(");
                sqb.AppendFormat(" SEID,");
                sqb.AppendFormat(" SELLDATE,");
                sqb.AppendFormat(" SELLERID,");
                sqb.AppendFormat(" CUKEY,");
                sqb.AppendFormat(" MAKERID,");
                sqb.AppendFormat(" DATE,");
                sqb.AppendFormat(" Year,");
                sqb.AppendFormat(" Month,");
                sqb.AppendFormat(" Day,");
                sqb.AppendFormat(" STATUS,");
                sqb.AppendFormat(" COURIER_NUMBER,");
                sqb.AppendFormat(" company_title");
                sqb.AppendFormat(" )");
                sqb.AppendFormat(" values (");
                sqb.AppendFormat("'{0}',", Text1.Value);
                sqb.AppendFormat("'{0}',", Text3.Value);
                sqb.AppendFormat("'{0}',", Text4.Value);
                sqb.AppendFormat("'{0}',", CUKEY.Value);
                sqb.AppendFormat("'{0}',", varMakerID);
                sqb.AppendFormat("'{0}',", varDate);
                sqb.AppendFormat("'{0}',", year);
                sqb.AppendFormat("'{0}',", month);
                sqb.AppendFormat("'{0}',", day);
                sqb.AppendFormat("'{0}',", "N");
                sqb.AppendFormat("'{0}',", Text12.Value);
                //sqb.AppendFormat("'{0}')", DropDownList5.Text);
                IFExecution_SUCCESS = true;
            }
            else
            {
                sqb.AppendFormat(" ;UPDATE SELLTABLE_MST SET");
                sqb.AppendFormat(" SELLDATE='{0}',", Text3.Value);
                sqb.AppendFormat(" SELLERID='{0}',", Text4.Value);
                sqb.AppendFormat(" CUKEY='{0}',", CUKEY.Value);
                sqb.AppendFormat(" MAKERID='{0}',", varMakerID);
                sqb.AppendFormat(" DATE='{0}',", varDate);
                sqb.AppendFormat(" COURIER_NUMBER='{0}'", Text12.Value);
                sqb.AppendFormat(" company_title='{0}'", "company");
                sqb.AppendFormat(" where seid='{0}'", Text1.Value);
                IFExecution_SUCCESS = true;
            }

            for (k = 0; k < count; k++)
            {
                string SECOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
                if (SECOUNT == "0.00" || SECOUNT == "0")/*不销入销货数量为0的项*/
                {

                }
                else
                {

                    SEKEY = SEKEY1.Substring(0, 8) + i1.ToString().PadLeft(12, '0');
                    i1++;
                    string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[11].FindControl("TextBox12")).Text;
                    string STORAGEID = bc.getOnlyString("SELECT STORAGEID FROM STORAGEINFO WHERE STORAGENAME='" + STORAGENAME + "'");
                    string BATCHID = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;
                    string SN = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                    string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                    string REMARK = ((TextBox)GridView1.Rows[k].Cells[15].FindControl("TextBox16")).Text;
                    //string DELIVERYDATE = bc.getOnlyString("SELECT DELIVERYDATE FROM ORDER_DET WHERE ORID IN ('" + Text2.Value + "') AND SN='" + SN + "'");

                    string FREECOUNT = ((TextBox)GridView1.Rows[k].Cells[16].FindControl("TextBox17")).Text;
                    string ORID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox21")).Text;
                    if (string.IsNullOrEmpty(FREECOUNT))
                    {
                        FREECOUNT = Convert.ToString(0);
                    }
                    list.Add(ORID);
                    sqb.AppendFormat(" ;INSERT INTO SELLTABLE_DET(");
                    sqb.AppendFormat(" SEKEY,");
                    sqb.AppendFormat(" SEID,");
                    sqb.AppendFormat(" ORID,");
                    sqb.AppendFormat(" SN,");
                    sqb.AppendFormat(" REMARK,");
                    sqb.AppendFormat(" Year,");
                    sqb.AppendFormat(" Month,");
                    sqb.AppendFormat(" Day,");
                    sqb.AppendFormat(" STATUS");
                    sqb.AppendFormat(" )");
                    sqb.AppendFormat(" values (");
                    sqb.AppendFormat("'{0}',", SEKEY);
                    sqb.AppendFormat("'{0}',", Text1.Value);
                    sqb.AppendFormat("'{0}',", ORID);
                    sqb.AppendFormat("'{0}',", SN);
                    sqb.AppendFormat("'{0}',", REMARK);
                    sqb.AppendFormat("'{0}',", year);
                    sqb.AppendFormat("'{0}',", month);
                    sqb.AppendFormat("'{0}',", day);
                    sqb.AppendFormat("'{0}')", "N");

                    sqb.AppendFormat(" ;INSERT INTO MATERE(");
                    sqb.AppendFormat(" MRKEY,");
                    sqb.AppendFormat(" MATEREID,");
                    sqb.AppendFormat(" SN,");
                    sqb.AppendFormat(" WAREID,");
                    sqb.AppendFormat(" MRCOUNT,");
                    sqb.AppendFormat(" STORAGEID,");
                    sqb.AppendFormat(" BATCHID,");
                    sqb.AppendFormat(" Date,");
                    sqb.AppendFormat(" MakerID,");
                    sqb.AppendFormat(" Year,");
                    sqb.AppendFormat(" Month,");
                    sqb.AppendFormat(" Day,");
                    sqb.AppendFormat(" FREECOUNT");
                    sqb.AppendFormat(" )");
                    sqb.AppendFormat(" values (");
                    sqb.AppendFormat("'{0}',", SEKEY);
                    sqb.AppendFormat("'{0}',", Text1.Value);
                    sqb.AppendFormat("'{0}',", SN);
                    sqb.AppendFormat("'{0}',", WAREID);
                    sqb.AppendFormat("'{0}',", SECOUNT);
                    sqb.AppendFormat("'{0}',", STORAGEID);
                    sqb.AppendFormat("'{0}',", BATCHID);
                    sqb.AppendFormat("'{0}',", varDate);
                    sqb.AppendFormat("'{0}',", varMakerID);
                    sqb.AppendFormat("'{0}',", year);
                    sqb.AppendFormat("'{0}',", month);
                    sqb.AppendFormat("'{0}',", day);
                    sqb.AppendFormat("'{0}')", FREECOUNT);



                }

            }/*under FOR OUTSIDE*/

            if (sqb.ToString().Length > 0)
            {
                Database database = bc.getdb();
                using (DbConnection dbConnection = database.CreateConnection())
                {
                    dbConnection.Open();
                    DbTransaction dbTransaction = dbConnection.BeginTransaction();
                    System.Data.Common.DbCommand dbCommand = database.GetSqlStringCommand(sqb.ToString());
                    database.ExecuteNonQuery(dbCommand, dbTransaction);
                    try
                    {
                        dbTransaction.Commit();//执行成功就提交SQL,执行失败就回滚
                        IFExecution_SUCCESS = true;
                    }
                    catch (Exception ex)
                    {
                        dbTransaction.Rollback();//执行失败就回滚
                        IFExecution_SUCCESS = false;
                        hint.Value = ex.Message;
                        return;//执行失败回滚就不需要去执行更新订单状态的动作了
                    }
                }
     

                foreach (string orid in list)
                {
                    corder.UPDATE_ORDER_STATUS(orid);
                }
                //Response.Write(sqb.ToString());
                bind();
            }
        }
        #endregion
        #region ac1()
        private int ac1(int k)
        {
            int x = 1;
            string SECOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
            string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[11].FindControl("TextBox12")).Text;
            string BATCHID = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;
            string NOSECOUNT = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
            string STORAGECOUNT = ((TextBox)GridView1.Rows[k].Cells[13].FindControl("TextBox14")).Text;
            string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
            string k1 = bc.CheckingWareidAndStorage(WAREID, STORAGENAME, BATCHID);
            string FREECOUNT = ((TextBox)GridView1.Rows[k].Cells[16].FindControl("TextBox17")).Text;


            if (SECOUNT == "")
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 销货数量不能为空！", k + 1);

            }
            if (SECOUNT == "0.00" || SECOUNT == "0")
            {
                x = 1;
                return x;
            }
            if (FREECOUNT == "")
            {

                FREECOUNT = Convert.ToString(0);

            }
            if (!String.IsNullOrEmpty(SECOUNT) && bc.yesno(SECOUNT) == 0 || bc.yesno(FREECOUNT) == 0)
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 数量只能输入数字！", k + 1);

            }
            else if (STORAGENAME == "")
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 仓库不能为空！", k + 1);


            }
            else if (!bc.exists("SELECT * FROM STORAGEINFO WHERE STORAGENAME='" + STORAGENAME + "'"))
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 该仓库不存在于系统中！", k + 1);


            }
            else if (BATCHID == "")
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 批号不能为空！", k + 1);
            }

            else if (decimal.Parse(SECOUNT) > decimal.Parse(NOSECOUNT))
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 销货数量不能大于未销货数量！", k + 1);
            }
            else if (k1 != STORAGECOUNT)
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 选择的库存品号与此项次销货品号不一致！", k + 1);

            }
            else if (decimal.Parse(SECOUNT) + decimal.Parse(FREECOUNT) > decimal.Parse(STORAGECOUNT))
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 销货数量+FREE数量不能大于库存数量！", k + 1);
            }

            return x;

        }
        #endregion

        private bool ac0(string s1, string s2)
        {
            bool c = true;
            /*if (bc.exists("SELECT * FROM SELLTABLE_DET WHERE SEID='" + s1 + "'"))
            {
                string s3 = bc.getOnlyString("SELECT ORID FROM SELLTABLE_DET WHERE SEID='" + s1 + "'");
                if (s3 != s2)
                {
                    hint.Value = "同一个销货单下面只能出现一个订单号!";
                    c = false;
                }
            }*/
            return c;

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Attributes["style"] = "Cursor:pointer";

            }

        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }
        }
        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string v1 = GridView3.DataKeys[GridView3.SelectedIndex].Values[0].ToString();
                string FilePath = bc.getOnlyString("SELECT PATH FROM WAREFILE WHERE FLKEY='" + v1 + "'");
                FileInfo file = new FileInfo(Server.MapPath(FilePath));
                if (file.Exists)
                {
                    Response.Clear();
                    string fileName = HttpUtility.UrlEncode(file.Name);
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream;charset=gb2312";
                    Response.Filter.Close();
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                ClearText();
                //Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM SELLTABLE_MST", "SEID", "SE");
                bind();
            }
            else if (submit.ID == "Submit2")
            {
                if (Text1.Value == "")
                {
                    hint.Value = "单号不能为空！";
                    return;

                }
                if(bc.totalBiteSize(Text1.Value)>20)
                {
                    hint.Value = "单号长度汉字与字母级合不能大于20个字节！"+"当前长度："+bc.totalBiteSize(Text1.Value).ToString();
                    return;
                }
                /*if(bc.checkEmail(Text1.Value)==false)
                {
                    hint.Value = "单号只能输入字母与数字的组合！";
                    return;
                }*/
                hint.Value = "";
                //判断是否存在此订单号
                sqb = new StringBuilder(string.Format("SELECT * FROM ORDER_DET WHERE ORID IN ({0});", Text2.Value));
                //判断销货单状态
                sqb.Append("SELECT * FROM [SellTable_DET] WHERE SEID='" + Text1.Value + "';");
                //判断员工是否存在系统中
                sqb.Append("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "';");
                //判断销货单号是否已经存在系统
                sqb.AppendFormat("SELECT seid FROM SELLTABLE_MST WHERE SEID='" + Text1.Value + "'");
                SqlConnection con = bc.getcon();
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(sqb.ToString(), con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                con.Close();
                dt1 = ds.Tables[0];
                dt2 = ds.Tables[1];
                dt3 = ds.Tables[2];
                dt6 = ds.Tables[3];
                if (dt6.Rows.Count > 0)
                {
                    hint.Value = "此销货单号已经存在系统了，请使用另外的销货单号";
                    return;
                }
                if (dt1.Rows.Count > 0)
                {

                    int count = dt1.Rows.Count;
                    if (dt2.Rows.Count > 0 && dt2.Rows[0]["STATUS"].ToString() == "SAVE")
                    {
                        hint.Value = "此销货单存在客户对账单，不允许修改";
                    }
                    else if (dt2.Rows.Count > 0 && dt2.Rows[0]["STATUS"].ToString() == "RECONCILE")
                    {
                        hint.Value = "此销货单已对账，不允许修改";
                    }
                    else if (creceivables.JUAGE_IF_EXISTS_SE_SERETURN(Text1.Value, ""))
                    {
                        hint.Value = "此销货单已经存在应收款单不允许修改";
                    }
                    else if (!ac0(Text1.Value, Text2.Value))
                    {

                    }
                    else if (Text4.Value == "")
                    {
                        hint.Value = "工号不能为空！";

                    }
                
                    else if (dt3 == null)
                    {
                        hint.Value = "销货员工工号不存在于系统中！";

                    }
                    else if (SEKEY == "Exceed Limited")
                    {
                        hint.Value = "编码超出限制！";

                    }
                    else if (juage_if_write(count))/*先判断符合条件写入主表，因为明细表有外键约束，所以要先将主表信息写入*/
                    {

                    }
                    else if (juage_if_write1(count))/*写入数据至少有一项的销货数量是不为0的，因为若有多项，多次销，销过的为0是正常的，但又不能把销货数量为0的项写入表中*/
                    {
                        hint.Value = "至少有一项销货数量是不为0";
                    }
                    else
                    {
                        add2(count);
                    }

                }

            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../SellManage/SELLTABLE.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                PrintSellTableBill print = new PrintSellTableBill();
                DataTable dt = print.asko(Text1.Value);
                /*string v1 = bc.getOnlyString("select path from model_path where name='SELLTABLE' ");
                string v2 = bc.getOnlyString("select path from model_path where name='savepath'");
                if (v1 == "")
                {
                    Response.Write("找不到打印模版路径");
                    return;
                }
                else if (v2 == "")
                {
                    Response.Write("找不到打印模版存储路径");
                    return;
                }*/

                try
                {
                    //ExcelPrint(dt, v1, v2);
                    ExcelPrint_for_EPPlus(dt);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (submit.ID == "Submit5")
            {
                try
                {
                    hint.Value = "";
                    string v1 = bc.getOnlyString("SELECT STATUS FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "'");
                    if (v1 == "N")
                    {
                        basec.getcoms("UPDATE SELLTABLE_DET SET STATUS='RECONCILE' WHERE SEID='" + Text1.Value + "'");
                        bind();
                    }
                    else
                    {

                        hint.Value = "状态不为开立不能对账";
                    }
                }
                catch (Exception ex)
                {
                    hint.Value = ex.Message;

                }
            }
            else if (submit.ID == "Submit6")
            {
                hint.Value = "";
                string v1 = bc.getOnlyString("SELECT STATUS FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "'");
                if (v1 == "RECONCILE")
                {
                    basec.getcoms("UPDATE SELLTABLE_DET SET STATUS='N' WHERE SEID='" + Text1.Value + "'");
                    bind();
                }
                else if (v1 == "INVOICE")
                {
                    hint.Value = "状态为已开票不能还原";
                }
            }
        }


        #region gridview deleting
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='销货单'");
            string sql2, sql3;
            hint.Value = "";
            string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
            string ORID = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].FindControl("TextBox21")).Text;
            sql2 = "DELETE FROM SELLTABLE_MST WHERE SEID='" + Text1.Value + "'";
            sql3 = "DELETE FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "' AND SN='" + ID + "'";
            string s1 = bc.getOnlyString("SELECT STATUS FROM [SellTable_DET] WHERE SEID='" + Text1.Value + "'");
            string nonull = bc.getOnlyString("SELECT B.ORID FROM SELLTABLE_DET A  LEFT JOIN SELLRETURN_DET B ON A.ORID=B.ORID AND A.SN=B.SN WHERE A.SEID='" + Text1.Value + "'");
            if (s1 == "SAVE")
            {
                hint.Value = "此销货单存在客户对账单，不允许删除";
            }
            else if (s1 == "RECONCILE")
            {
                hint.Value = "此销货单已对账，不允许删除";
            }
            else if (creceivables.JUAGE_IF_EXISTS_SE_SERETURN(Text1.Value, ""))
            {
                hint.Value = "此销货单已经存在应收款单不允许删除";
            }
            else if (bc.exists("select * from RECEIVABLES_ORDER where rcid='" + Text1.Value+ "'") == true)
            {
                hint.Value = "此销货单已经存在收款单不允许删除";
            }
            else if (v1 != "Y")
            {
                hint.Value = "您无删除权限！";
            }
            else if (bc.JuageDeleteCount_IFMoreThanTOTAL_ACTUAL_SELLCOUNT(ORID, Text1.Value))
            {
                hint.Value = bc.ErrowInfo;
            }
            else
            {
                basec.getcoms(sql3);
                //库存要删除主库，因为库存只扣主库
                basec.getcoms("DELETE  MATERE WHERE MATEREID='" + Text1.Value + "' AND SN='" + ID + "'");
                if (!bc.exists("SELECT * FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "'"))
                {
                    basec.getcoms(sql2);
                }
                corder.UPDATE_ORDER_STATUS(ORID);
                GridView1.EditIndex = -1;
                bind();

            }
            try
            {


            }
            catch (Exception)
            {


            }
        }
        #endregion

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#C9D3E2',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }
        }

    
        #region ExcelPrint
        public void ExcelPrint(DataTable dt2, string Printpath, string savepath)
        {

            /*Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            int m = dt2.Rows.Count;
            workbook = application.Workbooks.Open(Printpath, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);/* 13 to parameter 15 */
           /* worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            application.Visible = true;/*140323 use printpreview false to true1/2*/
            /*application.ExtendList = false;
            application.DisplayAlerts = false;
            application.AlertBeforeOverwriting = false;
            string v1 = DateTime.Now.ToString("yyyyMMddHHmmss").Replace("-", "/") + ".xls";*/
            //string v2 = savepath + v1;
            // worksheet.Cells[1, "A"] = dt2.Rows[i]["公司名称"].ToString();
            //worksheet.Cells[3, "A"] = "公司地址：" + dt2.Rows[i]["公司地址"].ToString() + " " + "电话：" + dt2.Rows[i]["公司电话"].ToString() + " " + "传真：" + dt2.Rows[i]["公司传真"].ToString();
            /*for (i = 0; i < m; i++)
            {

                worksheet.Cells[6, 3] = dt2.Rows[i]["客户名称"].ToString();
                worksheet.Cells[6, 10] = dt2.Rows[i]["销货单号"].ToString();
                worksheet.Cells[7, 3] = dt2.Rows[i]["送货地址"].ToString();
                worksheet.Cells[7, 10] = dt2.Rows[i]["联系人"].ToString();
                worksheet.Cells[8, 3] = dt2.Rows[i]["联系电话"].ToString();
                worksheet.Cells[8, 10] = dt2.Rows[i]["销货日期"].ToString();
                worksheet.Cells[9, 3] = dt2.Rows[i]["客户订单号"].ToString();


                worksheet.Cells[22, 7] = dt2.Rows[i]["合计销货数量"].ToString();
                worksheet.Cells[22, 8] = dt2.Rows[i]["合计FREE数量"].ToString();




                worksheet.Cells[12 + 2 * i, "B"] = dt2.Rows[i]["品名"].ToString();
                worksheet.Cells[12 + 2 * i, "D"] = dt2.Rows[i]["板厚"].ToString();
                worksheet.Cells[12 + 2 * i, "E"] = dt2.Rows[i]["铜厚"].ToString();
                worksheet.Cells[12 + 2 * i, "F"] = dt2.Rows[i]["客户料号"].ToString();

                worksheet.Cells[12 + 2 * i, "G"] = dt2.Rows[i]["销货数量"].ToString();
                //worksheet.Cells[12 + 2 * i, "H"] = dt2.Rows[i]["计量单位"].ToString();
                worksheet.Cells[12 + 2 * i, "I"] = dt2.Rows[i]["销售单价"].ToString();
                worksheet.Cells[12 + 2 * i, "J"] = dt2.Rows[i]["批号"].ToString();
                worksheet.Cells[12 + 2 * i, "L"] = dt2.Rows[i]["备注"].ToString();


                //worksheet.PrintOut(1, 1, 1, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //csharpExcelPrint(sfdg.FileName);

                workbook.SaveAs(v2);
                //csharpExcelPrint(sfdg.FileName);

            }
            application.Quit();
            worksheet = null;
            workbook = null;
            application = null;
            GC.Collect();
            Response.Redirect("/PrintFile/" + v1);*/


        }
        #endregion
        #region ExcelPrint_for_EPPlus
        public void ExcelPrint_for_EPPlus(DataTable dt)
        {

            //导出模版路径
            string filePath2 = Server.MapPath("/Print_Model/ERP出货单格式_model.xlsx");
            // 设置Excel文件路径
            string v1 = "selltable_" + DateTime.Now.ToString("yyyyMMddHHmmssfff").Replace("-", "/") + ".xlsx";
            string filePath = Server.MapPath("/outputfile/" + v1);

            // 创建一个新的Excel包

            FileInfo file = new FileInfo(filePath2);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                //获取Excel中的第n张表：

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // 添加一个工作表
                //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                /*string[] headers = { "序号", "主机转速取整", "主机功率取整", "主机油耗率-右" };
                 //写入表头
                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[1, col].Value = headers[col - 1];
                }*/

                // 写入数据
                //Response.Write(dt.Rows.Count.ToString());
                //return;
                //if (dt.Rows.Count > 0)
                // Response.Write("have data");
                // else
                //Response.Write("NO DATA");
                //return;
            worksheet.Cells[1, 1].Value= dt.Rows[i]["公司名称"].ToString();
            worksheet.Cells[3, 1].Value = "公司地址：" + dt.Rows[i]["公司地址"].ToString() + " " + "电话：" + dt.Rows[i]["公司电话"].ToString() + " " + "传真：" + dt.Rows[i]["公司传真"].ToString();
            for (i = 0; i < dt.Rows.Count; i++)
            {

                worksheet.Cells[6, 3].Value= dt.Rows[i]["客户名称"].ToString();
                worksheet.Cells[6, 10].Value= dt.Rows[i]["销货单号"].ToString();
                worksheet.Cells[7, 3].Value= dt.Rows[i]["送货地址"].ToString();
                worksheet.Cells[7, 10].Value= dt.Rows[i]["联系人"].ToString();
                worksheet.Cells[8, 3].Value= dt.Rows[i]["联系电话"].ToString();
                worksheet.Cells[8, 10].Value= dt.Rows[i]["销货日期"].ToString();
                worksheet.Cells[9, 3].Value= dt.Rows[i]["客户订单号"].ToString();


                worksheet.Cells[22, 7].Value= dt.Rows[i]["合计销货数量"].ToString();
                worksheet.Cells[22, 8].Value= dt.Rows[i]["合计FREE数量"].ToString();




                worksheet.Cells[12 + 2 * i, 2].Value= dt.Rows[i]["品名"].ToString();
                worksheet.Cells[12 + 2 * i, 4].Value= dt.Rows[i]["板厚"].ToString();
                worksheet.Cells[12 + 2 * i, 5].Value= dt.Rows[i]["铜厚"].ToString();
                worksheet.Cells[12 + 2 * i, 6].Value= dt.Rows[i]["客户料号"].ToString();

                worksheet.Cells[12 + 2 * i, 7].Value= dt.Rows[i]["销货数量"].ToString();
                //worksheet.Cells[12 + 2 * i, "H"].Value= dt.Rows[i]["计量单位"].ToString();
                worksheet.Cells[12 + 2 * i, 9].Value= dt.Rows[i]["销售单价"].ToString();
                worksheet.Cells[12 + 2 * i, 10].Value= dt.Rows[i]["批号"].ToString();
                worksheet.Cells[12 + 2 * i, 11].Value= dt.Rows[i]["备注"].ToString();
            }

                // 保存Excel文件
                package.SaveAs(filePath);
                //MessageBox.Show("写入到Excel文件成功！！！");
                Response.Redirect("/outputfile/" + v1);//将文件输出到浏览器供用户下载
            }

        }
        #endregion
        protected void btnEXCEL_PRINT_Click(object sender, ImageClickEventArgs e)
        {
            /*string vard1 = Text1.Value;
            String[] Carstr = new string[] { vard1 };
            WPSS.ReportManage.CRVPrintBill.Array[0] = Carstr[0];
            Response.Redirect("../ReportManage/CRVPrintBill.aspx");*/
            try
            {

            }
            catch (Exception)
            {


            }


        }


        protected void btnCourier_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bc.exists("SELECT * FROM SELLTABLE_MST  WHERE SEID='" + Text1.Value + "'"))
                {
                    hint.Value = "先保存单据才能点击更新快递单号";
                    return;
                }
                hint.Value = "";
                bc.getcom("UPDATE SELLTABLE_MST SET COURIER_NUMBER='" + Text12.Value + "' WHERE SEID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;
                hint_bind();
            }
            catch (Exception)
            {

            }

        }

    }
}
