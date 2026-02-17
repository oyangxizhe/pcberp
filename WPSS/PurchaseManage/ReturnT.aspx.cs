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

namespace WPSS.PurchaseManage
{
    public partial class ReturnT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dtx3 = new DataTable();
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
        basec bc = new basec();
        CPURCHASE cpurchase = new CPURCHASE();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();
        WPSS.Validate va = new Validate();
        int i;


        string sqlo = @"
SELECT
A.PUID AS PUID,
A.SN AS SN,
B.WAREID AS WAREID,
D.PCount AS PCOUNT,
D.PurchaseUnitPrice AS  PURCHASEUNITPRICE,
D.TAXRATE AS TAXRATE,
C.STORAGENAME AS STORAGENAME,
B.BATCHID AS BATCHID,
SUM(B.GECOUNT) AS GECOUNT FROM PURCHASEGODE_DET A 
LEFT JOIN GODE B ON A.PGKEY=B.GEKEY
LEFT JOIN STORAGEINFO C ON B.STORAGEID=C.STORAGEID
LEFT JOIN Purchase_DET D ON D.PUID=A.PUID AND D.SN=A.SN 
";



        public static string[] NEWID = new string[] { "", "" };
        public static string[] GETID = new string[] { "" };
        public static string[] str2 = new string[] { "" };
        string REKEY;
        int j;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        Text1.Value = IDO;
                        Assignment();
                        bind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.Redirect("/default.aspx");
            }
        }
        #region bind
        protected void bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
            x.Value = "";
            x2.Value = "";
            CUKEY.Value = "";
            ControlFileDisplay.Value = "";
            GridView1.DataSource = as1(Text1.Value, Text2.Value);
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();

       

            string sql1 = new CRETURN ().sql  + " WHERE A.PUID='" + Text2.Value + "' AND A.REID='" + Text1.Value + "'";
            dt4 = basec.getdts(sql1);
            if (dt4.Rows.Count > 0)
            {
                basec.GenerateColumns(GridView2, dt4).DataSource = dt4;//因为要使用自定义标题列名称，所以要产生列名,使其有HeaderText属性
                basec.GenerateColumns(GridView2, dt4).DataBind();
                x.Value = Convert.ToString(1);
            }
            DataTable dtx4 = basec.getdts(@"
SELECT 
A.REID,
SUM(A.NOTAX_AMOUNT),
SUM(A.TAX_AMOUNT),
SUM(A.AMOUNT)
FROM Return_DET A 
WHERE A.REID='" + Text1.Value + "'  GROUP BY A.REID ");

            if (dtx4.Rows.Count > 0)
            {
                string v8 = dtx4.Rows[0][1].ToString();
                string v9 = dtx4.Rows[0][2].ToString();
                string v10 = dtx4.Rows[0][3].ToString();
                Text7.Value = string.Format("{0:F2}", Convert.ToDouble(v8));
                Text8.Value = string.Format("{0:F2}", Convert.ToDouble(v9));
                Text9.Value = string.Format("{0:F2}", Convert.ToDouble(v10));
                x.Value = Convert.ToString(1);
            }
            else
            {
                Text7.Value = "";
                Text8.Value = "";
                Text9.Value = "";

            }
            string sql3 = @"
SELECT 
DISTINCT(A.WAREID) AS WAREID,
B.FLKEY AS FLKEY,
B.OLDFILENAME AS OLDFILENAME
FROM PURCHASE_DET A LEFT JOIN WAREFILE B  ON A.WAREID=B.WAREID 
WHERE A.PUID='" + Text2.Value + "' AND B.FLKEY IS NOT NULL  ORDER BY A.WAREID,B.FLKEY,B.OLDFILENAME";
            dt = basec.getdts(sql3);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataKeyNames = new string[] { "FLKEY" };
                GridView3.DataBind();
                ControlFileDisplay.Value = Convert.ToString(1);

            }
            else
            {

                GridView3.DataSource = null;
            }
            if (bc.exists("SELECT * FROM PURCHASE_MST WHERE PUID='" + Text2.Value + "'"))
            {
                x2.Value = "exists";
            }

            dtx3 = basec.getdts(new CRETURN() .sql + " where A.REID='" + Text1.Value + "'");
            if (dtx3.Rows.Count > 0)
            {
                Text3.Value = dtx3.Rows[0]["退货日期"].ToString();
                Text4.Value = dtx3.Rows[0]["退货员工号"].ToString();
                Label1.Text = dtx3.Rows[0]["退货员"].ToString();

            }
            else
            {

                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 10, 10);
                string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
                Text3.Value = DateTime.Now.ToString("yyy-MM-dd");
                Text4.Value = varMakerID;
                Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");
            }
            btnSure.ForeColor = System.Drawing.Color.Blue;
            string s1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + Text1.Value + "'");
            if (s1 == "RECONCILE" || s1 == "INVOICE")
            {
                Submit4.Value = "已对帐";

            }
            else
            {
                Submit4.Value = "确认对帐";

            }
            DataTable dtx;
            dtx = bc.getdt("select * from set_showname");
            if (dtx.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (GridView1.Columns[i].HeaderText == "料号")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "品名")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["wname"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "客户料号")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["cwareid"].ToString();
                    }
                }
                for (int i = 0; i < GridView2.Columns.Count; i++)
                {
                    if (GridView2.Columns[i].HeaderText == "料号")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "品名")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["wname"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "客户料号")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["cwareid"].ToString();
                    }
                }
            }
            GridView1.DataBind();
            GridView2.DataBind();
        }
        #endregion
        #region assignment
        protected void Assignment()
        {
            #region Assignment
         
            string s = bc.getOnlyString("SELECT PUID FROM Return_DET WHERE REID='" +Text1 .Value + "'");
            Text2.Value = s;
            Text5.Value = bc.getOnlyString("SELECT SNAME FROM SUPPLIERINFO_MST A LEFT JOIN PURCHASE_DET B ON A.SUID=B.SUID WHERE B.PUID='" + s + "'");
            #endregion
        }
        #endregion
        protected void btnSure_Click(object sender, EventArgs e)
        {

            if (!bc.exists("SELECT * FROM PURCHASE_MST WHERE PUID='" + Text2.Value + "'"))
            {
                hint.Value = "该采购号不存在于系统中！";
                return;

            }
            else if (as1(Text1.Value, Text2.Value).Rows.Count > 0)
            {

                bind();
            }
            else
            {
                hint.Value = "该采购单无可退货的数据！";

            }
          
        }

        #region ask
        private DataTable ask(string v1, string v2)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("采购号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("单位", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("采购单价", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("采购数量", typeof(decimal));
            dtt.Columns.Add("累计采购入库数量", typeof(decimal));
            dtt.Columns.Add("累计退货数量", typeof(decimal));
            dtt.Columns.Add("可退货数量", typeof(decimal), "累计采购入库数量-累计退货数量");
            dtt.Columns.Add("入库仓库", typeof(string));
            dtt.Columns.Add("入库批号", typeof(string));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("库存数量", typeof(decimal));
            dtt.Columns.Add("退货数量", typeof(decimal));
            dtt.Columns.Add("本退货单累计退货数量", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal), "采购单价*退货数量");
            dtt.Columns.Add("税额", typeof(decimal), "采购单价*退货数量*税率/100");
            dtt.Columns.Add("含税金额", typeof(decimal), "采购单价*退货数量*(1+税率/100)");

            dtt.Columns.Add("累计退货未税金额", typeof(decimal));
            dtt.Columns.Add("累计退货税额", typeof(decimal));
            dtt.Columns.Add("累计退货含税金额", typeof(decimal));
            dtt.Columns.Add("可退货未税金额", typeof(decimal), "采购单价*累计采购入库数量-累计退货未税金额");
            dtt.Columns.Add("可退货税额", typeof(decimal), "采购单价*累计采购入库数量*税率/100-累计退货税额");
            dtt.Columns.Add("可退货含税金额", typeof(decimal), "采购单价*累计采购入库数量*(1+税率/100)-累计退货含税金额");
            dtt.Columns.Add("退货未税金额", typeof(decimal));
            dtt.Columns.Add("退货税额", typeof(decimal));
            dtt.Columns.Add("退货含税金额", typeof(decimal));
            dtt.Columns.Add("项次1", typeof(string));
            DataTable dtx1 = bc.getdt(sqlo + @" WHERE A.PUID ='" + v2 + 
                "' GROUP BY A.PUID,A.SN,B.WAREID,D.PCOUNT,D.PurchaseUnitPrice,D.TAXRATE,C.STORAGENAME,B.BATCHID");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["采购号"] = dtx1.Rows[i]["PUID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["项次1"] = Convert.ToString(i + 1);
                    dr["品号"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["单位"] = dtx2.Rows[0]["UNIT"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["采购数量"] = dtx1.Rows[i]["PCOUNT"].ToString();
                    if (string.IsNullOrEmpty(dtx1.Rows[i]["PURCHASEUNITPRICE"].ToString()))
                    {
                        dr["采购单价"] = 0;
                    }
                    else
                    {
                        dr["采购单价"] = dtx1.Rows[i]["PURCHASEUNITPRICE"].ToString();
                    }

                    dr["税率"] = dtx1.Rows[i]["TAXRATE"].ToString();
                    dr["累计采购入库数量"] = 0;
                    dr["累计退货数量"] = 0;
                    dr["本退货单累计退货数量"] = 0;

                    dr["累计退货未税金额"] = 0;
                    dr["累计退货税额"] = 0;
                    dr["累计退货含税金额"] = 0;
                    dr["入库仓库"] = dtx1.Rows[i]["STORAGENAME"].ToString();
                    dr["入库批号"] = dtx1.Rows[i]["BATCHID"].ToString();
                    dtt.Rows.Add(dr);


                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT A.PUID AS PUID,A.SN AS SN,B.WAREID AS WAREID,C.STORAGENAME AS STORAGENAME,B.BATCHID AS BATCHID,SUM(B.GECOUNT) AS GECOUNT FROM PURCHASEGODE_DET A 
LEFT JOIN GODE B ON A.PGKEY=B.GEKEY
LEFT JOIN STORAGEINFO C ON B.STORAGEID=C.STORAGEID
WHERE  A.PUID='" + v2 + "' GROUP BY A.PUID,A.SN,B.WAREID,C.STORAGENAME,B.BATCHID");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购号"].ToString() == dtx4.Rows[i]["PUID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString() && dtt.Rows[j]["入库仓库"].ToString() == dtx4.Rows[i]["STORAGENAME"].ToString() && dtt.Rows[j]["入库批号"].ToString() == dtx4.Rows[i]["BATCHID"].ToString())
                        {
                            dtt.Rows[j]["累计采购入库数量"] = dtx4.Rows[i]["GECOUNT"].ToString();

                            break;
                        }

                    }
                }

            }
            DataTable dtx6 = bc.getdt(@"
  SELECT A.PUID AS PUID,A.SN AS SN,B.WAREID AS WAREID,D.PCount AS PCOUNT,D.PurchaseUnitPrice AS 
 PURCHASEUNITPRICE,D.TAXRATE AS TAXRATE,C.STORAGENAME AS STORAGENAME,
 B.BATCHID AS BATCHID,SUM(B.MRCount) AS MRCOUNT FROM RETURN_DET A 
LEFT JOIN MateRe  B ON A.REKEY=B.MRKEY
LEFT JOIN STORAGEINFO C ON B.STORAGEID=C.STORAGEID
LEFT JOIN Purchase_DET D ON D.PUID=A.PUID AND D.SN=A.SN 
 WHERE A.PUID ='" + v2 + "' GROUP BY A.PUID,A.SN,B.WAREID,D.PCOUNT,D.PurchaseUnitPrice,D.TAXRATE,C.STORAGENAME,B.BATCHID");
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购号"].ToString() == dtx6.Rows[i]["PUID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx6.Rows[i]["SN"].ToString() && dtt.Rows[j]["入库仓库"].ToString() == dtx6.Rows[i]["STORAGENAME"].ToString() && dtt.Rows[j]["入库批号"].ToString() == dtx6.Rows[i]["BATCHID"].ToString())
                        {
                            dtt.Rows[j]["累计退货数量"] = dtx6.Rows[i]["MRCOUNT"].ToString();
                            break;
                        }

                    }

                }

            }
            DataTable dtx5 = bc.getdt(@"
  SELECT A.PUID AS PUID,A.REID AS REID,A.SN AS SN,B.WAREID AS WAREID,D.PCount AS PCOUNT,D.PurchaseUnitPrice AS 
 PURCHASEUNITPRICE,D.TAXRATE AS TAXRATE,C.STORAGENAME AS STORAGENAME,
 B.BATCHID AS BATCHID,SUM(B.MRCount) AS MRCOUNT FROM RETURN_DET A 
LEFT JOIN MateRe  B ON A.REKEY=B.MRKEY
LEFT JOIN STORAGEINFO C ON B.STORAGEID=C.STORAGEID
LEFT JOIN Purchase_DET D ON D.PUID=A.PUID AND D.SN=A.SN  WHERE  A.PUID='" + v2 +
"' AND A.REID='" + v1 + "'  GROUP BY A.PUID,A.REID,A.SN,B.WAREID,D.PCOUNT,D.PurchaseUnitPrice,D.TAXRATE,C.STORAGENAME,B.BATCHID");
            if (dtx5.Rows.Count > 0)
            {
                for (i = 0; i < dtx5.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购号"].ToString() == dtx5.Rows[i]["PUID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx5.Rows[i]["SN"].ToString() && dtt.Rows[j]["入库仓库"].ToString() == dtx5.Rows[i]["STORAGENAME"].ToString() && dtt.Rows[j]["入库批号"].ToString() == dtx5.Rows[i]["BATCHID"].ToString())
                        {
                            dtt.Rows[j]["本退货单累计退货数量"] = dtx5.Rows[i]["MRCOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx7 = bc.getdt(@"
 SELECT A.PUID AS PUID,A.SN AS SN,B.WAREID AS WAREID,D.PCount AS PCOUNT,D.PurchaseUnitPrice AS 
 PURCHASEUNITPRICE,D.TAXRATE AS TAXRATE,C.STORAGENAME AS STORAGENAME,
 B.BATCHID AS BATCHID,SUM(B.MRCount) AS MRCOUNT,SUM(A.NOTAX_AMOUNT) AS NOTAX_AMOUNT,
 SUM(A.TAX_AMOUNT) AS TAX_AMOUNT,SUM(A.AMOUNT) AS AMOUNT FROM RETURN_DET A 
LEFT JOIN MateRe  B ON A.REKEY=B.MRKEY
LEFT JOIN STORAGEINFO C ON B.STORAGEID=C.STORAGEID
LEFT JOIN Purchase_DET D ON D.PUID=A.PUID AND D.SN=A.SN 
 WHERE A.PUID ='" + v2 + "' GROUP BY A.PUID,A.SN,B.WAREID,D.PCOUNT,D.PurchaseUnitPrice,D.TAXRATE,C.STORAGENAME,B.BATCHID");
            if (dtx7.Rows.Count > 0)
            {
                for (i = 0; i < dtx7.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购号"].ToString() == dtx7.Rows[i]["PUID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx7.Rows[i]["SN"].ToString() && dtt.Rows[j]["入库仓库"].ToString() == dtx7.Rows[i]["STORAGENAME"].ToString() && dtt.Rows[j]["入库批号"].ToString() == dtx7.Rows[i]["BATCHID"].ToString())
                        {
                            dtt.Rows[j]["累计退货未税金额"] = dtx7.Rows[i]["NOTAX_AMOUNT"].ToString();
                            dtt.Rows[j]["累计退货税额"] = dtx7.Rows[i]["TAX_AMOUNT"].ToString();
                            dtt.Rows[j]["累计退货含税金额"] = dtx7.Rows[i]["AMOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            return dtt;

        }
        #endregion
        #region as1
        private DataTable as1(string v1, string v2)
        {
            DataTable dtt = ask(v1, v2);
            for (i = 0; i < dtt.Rows.Count; i++)
            {
                dtt.Rows[i]["退货数量"] = dtt.Rows[i]["可退货数量"].ToString();

                dtt.Rows[i]["退货未税金额"] = dtt.Rows[i]["可退货未税金额"].ToString();
                dtt.Rows[i]["退货税额"] = dtt.Rows[i]["可退货税额"].ToString();
                dtt.Rows[i]["退货含税金额"] = dtt.Rows[i]["可退货含税金额"].ToString();
            }
            return dtt;
        }
        #endregion
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Label1.Text = "";

        }

        #region add
        protected void add()
        {

            hint.Value = "";
            string sql2 = sqlo + " WHERE A.PUID='" + Text2.Value + "' GROUP BY A.PUID,A.SN,B.WAREID,D.PCOUNT,D.PurchaseUnitPrice,D.TAXRATE,C.STORAGENAME,B.BATCHID";
            dt1 = basec.getdts(sql2);
             string s1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + Text1.Value + "'");

            if (dt1.Rows.Count > 0)
            {

                int count = dt1.Rows.Count;
                if (s1 == "SAVE")
                {
                    hint.Value = "此退货单存在供应商对账单，不允许修改";
                }
                else if (s1 == "RECONCILE")
                {
                    hint.Value = "此退货单已对账，不允许修改";
                }
                else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(Text1.Value, ""))
                {
                    hint.Value = "此退货单已经存在应付账单不允许修改";
                }

                else if (!ac0(Text1.Value, Text2.Value))
                {

                }
                else if (Text4.Value == "")
                {
                    hint.Value = "工号不能为空！";

                }
                
                else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "'"))
                {
                    hint.Value = "退货员工工号不存在于系统中！";

                }
                else if (REKEY == "Exceed Limited")
                {
                    hint.Value = "编码超出限制！";

                }

                else
                {
                    add2(count);
                }

            }

        }
        #endregion
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

            for (k = 0; k < count; k++)
            {
                if (ac1(k) == 0)
                {

                }
                else
                {

                    REKEY = bc.numYMD(20, 12, "000000000001", "select * from Return_DET", "REKEY", "RE");
                    string SRCOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
                    string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[11].FindControl("TextBox12")).Text;
                    string STORAGEID = bc.getOnlyString("SELECT STORAGEID FROM STORAGEINFO WHERE STORAGENAME='" + STORAGENAME + "'");
                    string BATCHID = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;
                    string SN = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                    string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                    string REMARK = ((TextBox)GridView1.Rows[k].Cells[15].FindControl("TextBox16")).Text;
                    string v1 = ((TextBox)GridView1.Rows[k].Cells[22].FindControl("TextBox23")).Text;
                    string v2 = ((TextBox)GridView1.Rows[k].Cells[23].FindControl("TextBox24")).Text;
                    string v3 = ((TextBox)GridView1.Rows[k].Cells[24].FindControl("TextBox25")).Text;
                    string v41 = ((TextBox)GridView1.Rows[k].Cells[25].FindControl("TextBox26")).Text;
                    string v4 = bc.getOnlyString("SELECT STORAGEID FROM STORAGEINFO WHERE STORAGENAME='" + v41 + "'");
                    string v5 = ((TextBox)GridView1.Rows[k].Cells[26].FindControl("TextBox27")).Text;

                    basec.getcoms("INSERT INTO Return_DET(REKEY,REID,PUID,SN,REMARK,NOTAX_AMOUNT,TAX_AMOUNT,AMOUNT,"
              + "Year,Month,Day,STATUS) values('" + REKEY + "','" + Text1.Value
              + "','" + Text2.Value + "','" + SN + "','" + REMARK + "','" + v1 + "','" + v2 + "','" + v3 + "','" + year + "','" + month + "','" + day + "','N')");

                    basec.getcoms("INSERT INTO GODE(GEKEY,GODEID,SN,WAREID,"
           + "GECOUNT,STORAGEID,BATCHID,Date,MakerID,Year,Month,Day) values('" + REKEY + "','" + Text1.Value
           + "','" + SN + "','" + WAREID + "','" + SRCOUNT + "','" + STORAGEID + "','" + BATCHID + "','" + varDate +
           "','" + varMakerID + "','" + year + "','" + month + "','" + day + "')");


                    basec.getcoms("INSERT INTO MATERE(MRKEY,MATEREID,SN,WAREID,"
 + "MRCOUNT,STORAGEID,BATCHID,Date,MakerID,Year,Month,Day) values('" + REKEY + "','" + Text1.Value
 + "','" + SN + "','" + WAREID + "','" + SRCOUNT + "','" + v4 + "','" + v5 + "','" + varDate +
 "','" + varMakerID + "','" + year + "','" + month + "','" + day + "')");
                    


                }




            }

            cpurchase.UPDATE_PURCHASE_STATUS(Text2.Value);
            if (!bc.exists("SELECT REID FROM Return_DET WHERE REID='" + Text1.Value + "'"))
            {
                return;

            }
            if (!bc.exists("SELECT REID FROM Return_MST WHERE REID='" + Text1.Value + "'"))
            {

                basec.getcoms("INSERT INTO Return_MST(REID,Return_DATE,Return_ID,MAKERID,DATE,"
+ "Year,Month,Day) values('" + Text1.Value + "','" + Text3.Value
+ "','" + Text4.Value + "','" + varMakerID + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
                IFExecution_SUCCESS = true;


            }
            else
            {
                basec.getcoms("UPDATE Return_MST SET Return_DATE='" + Text3.Value + "',Return_ID='" + Text4.Value +
                    "',MAKERID='" + varMakerID + "',DATE='" + varDate + "' WHERE REID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;

            }

            bind();
        }
        #endregion
        #region ac1()
        private int ac1(int k)
        {
            int x = 1;
            string SRCOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
            string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[11].FindControl("TextBox12")).Text;
            string BATCHID = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;
            string NOSRCOUNT = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
            string STORAGECOUNT = ((TextBox)GridView1.Rows[k].Cells[13].FindControl("TextBox14")).Text;
            string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
            string k1 = bc.CheckingWareidAndStorage(WAREID, STORAGENAME, BATCHID);

            string k2 = ((TextBox)GridView1.Rows[k].Cells[19].FindControl("TextBox20")).Text;
            string k3 = ((TextBox)GridView1.Rows[k].Cells[20].FindControl("TextBox21")).Text;
            string k4 = ((TextBox)GridView1.Rows[k].Cells[21].FindControl("TextBox22")).Text;

            string k5 = ((TextBox)GridView1.Rows[k].Cells[22].FindControl("TextBox23")).Text;
            string k6 = ((TextBox)GridView1.Rows[k].Cells[23].FindControl("TextBox24")).Text;
            string k7 = ((TextBox)GridView1.Rows[k].Cells[24].FindControl("TextBox25")).Text;
            if (SRCOUNT == "")
            {

                x = 0;
                hint.Value = "数量不能为空！";

            }
            else if (bc.yesno(SRCOUNT) == 0)
            {
                x = 0;
                hint.Value = "数量只能输入数字！";


            }
            else if (decimal.Parse(NOSRCOUNT) == 0)
            {

                x = 0;
                hint.Value = "可退货数量为0时不能做退货！";


            }
            else if (decimal.Parse(k2) == 0)
            {

                x = 0;
                hint.Value = "可退货未税金额为0时不能做退货！";


            }
            else if (decimal.Parse(k3) == 0)
            {

                x = 0;
                hint.Value = "可退货税额为0时不能做退货！";


            }
            else if (decimal.Parse(k4) == 0)
            {

                x = 0;
                hint.Value = "可退货含税金额为0时不能做退货！";


            }
            else if (decimal.Parse(k5) > decimal.Parse(k2))
            {
                x = 0;
                hint.Value = "退货未税金额不能大于可退货未税金额！";


            }
            else if (decimal.Parse(k6) > decimal.Parse(k3))
            {
                x = 0;
                hint.Value = "退货税额不能大于可退货税额！";


            }
            else if (decimal.Parse(k7) > decimal.Parse(k4))
            {
                x = 0;
                hint.Value = "退货含税金额不能大于可退货含税金额！";


            }
            else if (STORAGENAME == "")
            {
                x = 0;
                hint.Value = "仓库不能为空！";


            }
            else if (!bc.exists("SELECT * FROM STORAGEINFO WHERE STORAGENAME='" + STORAGENAME + "'"))
            {
                x = 0;
                hint.Value = "该仓库不存在于系统中！";


            }
            else if (BATCHID == "")
            {
                x = 0;
                hint.Value = "批号不能为空！";


            }

            else if (decimal.Parse(SRCOUNT) > decimal.Parse(NOSRCOUNT))
            {
                x = 0;
                hint.Value = "退货数量不能大于可退货数量！";


            }



            return x;

        }
        #endregion

        private bool ac0(string s1, string s2)
        {
            bool c = true;
            if (bc.exists("SELECT * FROM Return_DET WHERE REID='" + s1 + "'"))
            {
                string s3 = bc.getOnlyString("SELECT PUID FROM Return_DET WHERE REID='" + s1 + "'");
                if (s3 != s2)
                {
                    hint.Value = "同一个退货单下面只能出现一个采购号!";
                    c = false;
                }
            }
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
                addNEW();
            }
            else if (submit.ID == "Submit2")
            {
                try
                {

                    add();
                    if (IFExecution_SUCCESS == true)
                    {
                        bind();
                    }
                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../PurchaseManage/Return.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                try
                {
                    hint.Value = "";
                    string v1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + Text1.Value + "'");
                    if (v1 == "N")
                    {
                        basec.getcoms("UPDATE RETURN_DET SET STATUS='RECONCILE' WHERE REID='" + Text1.Value + "'");
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
            else if (submit.ID == "Submit5")
            {
                hint.Value = "";
                string v1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + Text1.Value + "'");
                if (v1 == "RECONCILE")
                {
                    basec.getcoms("UPDATE RETURN_DET SET STATUS='N' WHERE REID='" + Text1.Value + "'");
                    bind();
                }
                else if (v1 == "INVOICE")
                {
                    hint.Value = "状态为已开票不能还原";
                }
            }
            else if (submit.ID == "Submit6")
            {

            }
        }


        protected void addNEW()
        {
            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM Return_MST", "REID", "RE");
            bind();


        }

        #region gridview deleting
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 10, 10);
                string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='退货单'");
                string sql2, sql3;
                hint.Value = "";
                string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
                sql2 = "DELETE FROM Return_MST WHERE REID='" + Text1.Value + "'";
                sql3 = "DELETE FROM Return_DET WHERE REID='" + Text1.Value + "' AND SN='" + ID + "'";
                string s1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + Text1.Value + "'");
                if (s1 == "SAVE")
                {
                    hint.Value = "此退货单存在供应商对账单，不允许删除";
                }
                else if (s1 == "RECONCILE")
                {
                    hint.Value = "此退货单已对账，不允许删除";
                }
                else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(Text1.Value, ""))
                {
                    hint.Value = "此退货单已经存在应付账单不允许删除";
                }
                /*else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }*/
                else
                {
                    basec.getcoms(sql3);
                    basec.getcoms("DELETE  GODE WHERE GODEID='" + Text1.Value + "' AND SN='" + ID + "'");
                    basec.getcoms("DELETE  MATERE WHERE MATEREID='" + Text1.Value + "' AND SN='" + ID + "'");
                    if (!bc.exists("SELECT * FROM Return_DET WHERE REID='" + Text1.Value + "'"))
                    {
                        basec.getcoms(sql2);
                    }
                    cpurchase.UPDATE_PURCHASE_STATUS(Text2.Value);
                    GridView1.EditIndex = -1;
                    bind();

                }

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

        protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
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

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string vard1 = Text1.Value;
            String[] Carstr = new string[] { vard1 };
            //WPSS.ReportManage.CRVReturn .Array [0]= Carstr[0];
            Response.Redirect("../ReportManage/CRVReturn.aspx");
        }
        protected void excelprint()
        {

           
        }


        

    }
}
