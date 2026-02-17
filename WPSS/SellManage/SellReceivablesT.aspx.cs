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




namespace WPSS.SellManage
{
    public partial class SellReceivablesT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dtx3 = new DataTable();
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
        public static string[] GETID = new string[] { "" };
        public static string[] str2 = new string[] { "" };
        string SEKEY;
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
                        if (NEWID[0] != "")
                        {
                            Text1.Value = NEWID[0];
                            NEWID[0] = "";

                        }
                        else
                        {
                            Assignment();

                        }
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
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            string v1 = bc.getOnlyString("SELECT ADD_NEW FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='销货单'");
            string v2 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='销货单'");
            if (v1 == "Y")
            {
                btnAdd.Visible = true;
                Label2.Visible = true;

            }
            else
            {
                btnAdd.Visible = false;
                Label2.Visible = false;


            }
            if (v1 == "Y" || v2 == "Y")
            {
                btnSave.Visible = true;
                Label3.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                Label3.Visible = false;
            }
   
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
            emid.Value = varMakerID;
            CUKEY.Value = "";
            ControlFileDisplay.Value = "";
            GridView1.DataSource = as1(Text1.Value, Text2.Value);
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();

            dt4 = print.ask(Text1.Value);
            if (dt4.Rows.Count > 0)
            {
                basec.GenerateColumns(GridView2, dt4).DataSource = dt4;//因为要使用自定义标题列名称，所以要产生列名,使其有HeaderText属性
                basec.GenerateColumns(GridView2, dt4).DataBind();
                x.Value = Convert.ToString(1);
            }
            DataTable dtx4 = basec.getdts(@"
SELECT 
A.SEID,
A.ORID,
SUM(C.MRcount*B.sellunitprice+B.URGENT),
SUM((C.MRcount*B.sellunitprice+B.URGENT)*B.taxrate/100),
SUM((C.MRcount*B.sellunitprice+B.URGENT)*(1+B.taxrate/100)) 
FROM SELLTABLE_DET A 
LEFT JOIN ORDER_DET B ON A.ORID=B.ORID AND A.SN=B.SN
LEFT JOIN MATERE C ON A.SEKEY=C.MRKEY 
WHERE A.SEID='" + Text1.Value + "' AND A.ORID='" + Text2.Value + "' GROUP BY A.ORID,A.SEID ");

            if (dtx4.Rows.Count > 0)
            {
                string v8 = dtx4.Rows[0][2].ToString();
                string v9 = dtx4.Rows[0][3].ToString();
                string v10 = dtx4.Rows[0][4].ToString();
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
B.OLDFILENAME AS OLDFILENAME FROM 
ORDER_DET A 
LEFT JOIN WAREFILE B ON A.WAREID=B.WAREID 
WHERE A.ORID='" + Text2.Value + "' AND B.FLKEY IS NOT NULL ORDER BY A.WAREID,B.FLKEY,B.OLDFILENAME";
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
            if (bc.exists("SELECT * FROM ORDER_MST WHERE ORID='" + Text2.Value + "'"))
            {
                x2.Value = "exists";
            }

            dtx3 = print.ask(Text1.Value);
            if (dtx3.Rows.Count > 0)
            {
                Text3.Value = dtx3.Rows[0]["销货日期"].ToString();
                Text4.Value = dtx3.Rows[0]["销货员工号"].ToString();
                Label1.Text = dtx3.Rows[0]["销货员"].ToString();
                Text6.Value = dtx3.Rows[0]["联系人"].ToString();
                Text10.Value = dtx3.Rows[0]["联系电话"].ToString();
                Text11.Value = dtx3.Rows[0]["送货地址"].ToString();
                CUKEY.Value = dtx3.Rows[0]["CUKEY"].ToString();
                Text12.Value = dtx3.Rows[0]["快递单号"].ToString();
            }
            else
            {
                DataTable dtx8 = basec.getdts(@"
SELECT 
B.CUKEY 
FROM ORDER_MST A
LEFT JOIN CUSTOMERINFO_MST 
B ON A.CUID=B.CUID
WHERE A.ORID='" + Text2.Value + "'");
                if (dtx8.Rows.Count > 0)
                {

                    CUKEY.Value = dtx8.Rows[0]["CUKEY"].ToString();
                }
               
                Text3.Value = DateTime.Now.ToString("yyy-MM-dd");
                Text4.Value = varMakerID;
                Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");
            }
            btnSure.ForeColor = System.Drawing.Color.Blue;

            string s1 = bc.getOnlyString("SELECT STATUS FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "'");
             if (s1== "RECONCILE" || s1=="INVOICE")
            {
                btnReconcile.Text = "已对帐";
                btnReconcile.Enabled = false;
                btnReductionReconcil.Enabled = true;

            }
            else
            {
                btnReconcile.Text = "确认对帐";
                btnReconcile.Enabled = true;
                btnReductionReconcil.Enabled = false;

            }
             DataTable dtxx;
             dtxx = bc.getdt("select * from set_showname");
             if (dtxx.Rows.Count > 0)
             {
                 for (int i = 0; i < GridView1.Columns.Count; i++)
                 {
                     if (GridView1.Columns[i].HeaderText == "料号")
                     {
                         GridView1.Columns[i].HeaderText = dtxx.Rows[0]["co_wareid"].ToString();
                     }
                     if (GridView1.Columns[i].HeaderText == "品名")
                     {
                         GridView1.Columns[i].HeaderText = dtxx.Rows[0]["wname"].ToString();
                     }
                     if (GridView1.Columns[i].HeaderText == "客户料号")
                     {
                         GridView1.Columns[i].HeaderText = dtxx.Rows[0]["cwareid"].ToString();
                     }
                 }
                 for (int i = 0; i < GridView2.Columns.Count; i++)
                 {
                     if (GridView2.Columns[i].HeaderText == "料号")
                     {
                         GridView2.Columns[i].HeaderText = dtxx.Rows[0]["co_wareid"].ToString();
                     }
                     if (GridView2.Columns[i].HeaderText == "品名")
                     {
                         GridView2.Columns[i].HeaderText = dtxx.Rows[0]["wname"].ToString();
                     }
                     if (GridView2.Columns[i].HeaderText == "客户料号")
                     {
                         GridView2.Columns[i].HeaderText = dtxx.Rows[0]["cwareid"].ToString();
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
            Text1.Value = GETID[0];
            string s = bc.getOnlyString("SELECT ORID FROM SELLTABLE_DET WHERE SEID='" + GETID[0] + "'");
            GETID[0] = "";
            Text2.Value = s;
            Text5.Value = bc.getOnlyString("SELECT CNAME FROM CUSTOMERINFO_MST A LEFT JOIN ORDER_DET B ON A.CUID=B.CUID WHERE B.ORID='" + s + "'");
            #endregion
        }
        #endregion
        protected void btnSure_Click(object sender, EventArgs e)
        {

            if (!bc.exists("SELECT * FROM ORDER_MST WHERE ORID='" + Text2.Value + "'"))
            {
                hint.Value = "该订单号不存在于系统中！";
                return;

            }
            bind();
        }

        #region ask
        private DataTable ask(string v1, string v2)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("单位", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("未销货数量", typeof(decimal), "订单数量-累计销货数量");
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("库存数量", typeof(decimal));
            dtt.Columns.Add("销货数量", typeof(decimal));
            dtt.Columns.Add("本销货单累计销货数量", typeof(decimal));
            dtt.Columns.Add("工程费", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal), "(销售单价*销货数量)+工程费");
            dtt.Columns.Add("税额", typeof(decimal), "(销售单价*销货数量+工程费)*税率/100");
            dtt.Columns.Add("含税金额", typeof(decimal), "(销售单价*销货数量+工程费)*(1+税率/100)");
           


            DataTable dtx1 = bc.getdt("SELECT * FROM ORDER_DET WHERE ORID='" + v2 + "'");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["订单号"] = dtx1.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["品号"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["单位"] = dtx2.Rows[0]["UNIT"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["订单数量"] = dtx1.Rows[i]["OCOUNT"].ToString();
                    dr["销售单价"] = dtx1.Rows[i]["SELLUNITPRICE"].ToString();
                    dr["税率"] = dtx1.Rows[i]["TAXRATE"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["本销货单累计销货数量"] = 0;
                    dr["工程费"] = dtx1.Rows[i]["URGENT"].ToString();
                    dtt.Rows.Add(dr);

                    DataTable dtx6 = bc.getmaxstoragecount(dtx1.Rows[i]["WAREID"].ToString());
                    if (dtx6.Rows.Count > 0)
                    {
                        dr["仓库"] = dtx6.Rows[0]["仓库"].ToString();
                        dr["批号"] = dtx6.Rows[0]["批号"].ToString();
                        dr["库存数量"] = dtx6.Rows[0]["库存数量"].ToString();

                    }
                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT 
FROM SELLTABLE_DET A 
LEFT JOIN MATERE 
B ON A.SEKEY=B.MRKEY  
WHERE  A.ORID='" + v2 + "' GROUP BY A.ORID,A.SN,B.WAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx4.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销货数量"] = dtx4.Rows[i]["MRCOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx5 = bc.getdt(@"
SELECT 
A.ORID AS ORID,
A.SEID AS SEID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT
FROM SELLTABLE_DET A 
LEFT JOIN  MATERE B
ON A.SEKEY=B.MRKEY 
WHERE  A.ORID='" + v2 + "' AND A.SEID='" + v1 + "' GROUP BY A.ORID,A.SEID,A.SN,B.WAREID");
            if (dtx5.Rows.Count > 0)
            {
                for (i = 0; i < dtx5.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx5.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx5.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["本销货单累计销货数量"] = dtx5.Rows[i]["MRCOUNT"].ToString();
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
                dtt.Rows[i]["销货数量"] = dtt.Rows[i]["未销货数量"].ToString();
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
            Text6.Value = "";
            Text10.Value = "";
            Text11.Value = "";
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
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
          
            try
            {

                add();
                if (IFExecution_SUCCESS == true)
                {
                    bind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #region add
        protected void add()
        {

            hint.Value = "";
            string sql2 = "SELECT * FROM ORDER_DET WHERE ORID='" + Text2.Value + "'";
            dt1 = basec.getdts(sql2);
            string s1 = bc.getOnlyString("SELECT STATUS FROM [SellTable_DET] WHERE SEID='" + Text1.Value + "'");

            if (dt1.Rows.Count > 0)
            {

                int count = dt1.Rows.Count;
                if (s1 == "SAVE")
                {
                    hint.Value = "此销货单存在客户对账单，不允许修改";
                }
                else if (s1 == "RECONCILE")
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
                else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "'"))
                {
                    hint.Value = "销货员工工号不存在于系统中！";

                }
                else if (SEKEY == "Exceed Limited")
                {
                    hint.Value = "编码超出限制！";

                }
                else if(juage_if_write (count))/*先判断符合条件写入主表，因为明细表有外键约束，所以要先将主表信息写入*/
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
        #endregion
        private bool juage_if_write(int count)
        {
            bool b = false;
            for(int k = 0; k < count; k++)
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
                if (decimal .Parse (SECOUNT )!=0)
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
                sqb.AppendFormat(" COURIER_NUMBER");
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
                sqb.AppendFormat("'{0}')", Text12.Value);
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
                sqb.AppendFormat(" where seid='{0}'", Text1.Value);
                IFExecution_SUCCESS = true;
            }
            for (k = 0; k < count; k++)
            {
                string SECOUNT = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
                if (ac1(k) == 0)
                {

                }
                else if (decimal.Parse(SECOUNT) == 0)/*不定入销货数量为0的项*/
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
                    string DELIVERYDATE = bc.getOnlyString("SELECT DELIVERYDATE FROM ORDER_DET WHERE ORID='" + Text2.Value + "' AND SN='" + SN + "'");

                    string FREECOUNT = ((TextBox)GridView1.Rows[k].Cells[16].FindControl("TextBox17")).Text;
                    if (string.IsNullOrEmpty(FREECOUNT))
                    {
                        FREECOUNT = Convert.ToString(0);
                    }
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
                    sqb.AppendFormat("'{0}',", Text2.Value);
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
                    DataTable dtx6 = bc.getmaxstoragecount(WAREID);
                    if (dtx6.Rows.Count > 0)
                    {
                        for (int n = 0; n < count; n++)
                        {
                            if (((TextBox)GridView1.Rows[n].Cells[1].FindControl("TextBox2")).Text == WAREID)
                            {
                                ((TextBox)GridView1.Rows[n].Cells[11].FindControl("TextBox12")).Text = dtx6.Rows[0]["仓库"].ToString();
                                ((TextBox)GridView1.Rows[n].Cells[12].FindControl("TextBox13")).Text = dtx6.Rows[0]["批号"].ToString();
                                ((TextBox)GridView1.Rows[n].Cells[12].FindControl("TextBox14")).Text = dtx6.Rows[0]["库存数量"].ToString();
                            }
                        }
                    }
                    else
                    {

                        for (int n = 0; n < count; n++)
                        {
                            if (((TextBox)GridView1.Rows[n].Cells[1].FindControl("TextBox2")).Text == WAREID)
                            {
                                ((TextBox)GridView1.Rows[n].Cells[11].FindControl("TextBox12")).Text = "";
                                ((TextBox)GridView1.Rows[n].Cells[12].FindControl("TextBox13")).Text = "";
                                ((TextBox)GridView1.Rows[n].Cells[12].FindControl("TextBox14")).Text = "";
                            }
                        }
                    }
                }

            }/*under FOR OUTSIDE*/
          
            if (sqb.ToString().Length > 0)
            {
                bc.getcom(sqb.ToString());
                corder.UPDATE_ORDER_STATUS(Text2.Value);
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
            if (FREECOUNT == "")
            {

                FREECOUNT = Convert.ToString(0);
            }
            if (SECOUNT == "")
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 销货数量不能为空！", k + 1);

            }
            /*else if (decimal .Parse (SECOUNT )==0)
            {
                x = 0;
                hint.Value = string.Format("项次 {0} 销货数量不能为0！", k + 1);

            }*/
            else if (bc.yesno(SECOUNT) == 0 || bc.yesno(FREECOUNT) == 0)
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
            if (bc.exists("SELECT * FROM SELLTABLE_DET WHERE SEID='" + s1 + "'"))
            {
                string s3 = bc.getOnlyString("SELECT ORID FROM SELLTABLE_DET WHERE SEID='" + s1 + "'");
                if (s3 != s2)
                {
                    hint.Value = "同一个销货单下面只能出现一个订单号!";
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

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM SELLTABLE_MST", "SEID", "SE");
            bind();
        }
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../SellManage/SELLTABLE.aspx" + n2);
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

            else if (v1 != "Y")
            {
                hint.Value = "您无删除权限！";
            }
            else if (bc.JuageDeleteCount_IFMoreThanTOTAL_ACTUAL_SELLCOUNT(Text2.Value, Text1.Value))
            {
                hint.Value = bc.ErrowInfo;
            }
            else
            {
                basec.getcoms(sql3);
                basec.getcoms("DELETE  MATERE WHERE MATEREID='" + Text1.Value + "' AND SN='" + ID + "'");
                if (!bc.exists("SELECT * FROM SELLTABLE_DET WHERE SEID='" + Text1.Value + "'"))
                {
                    basec.getcoms(sql2);
                }
                corder.UPDATE_ORDER_STATUS(Text2.Value);
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



        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {


            PrintSellTableBill print = new PrintSellTableBill();
            DataTable dt = print.asko(Text1.Value);
            string v1 = bc.getOnlyString("select path from model_path where name='SELLTABLE'");
            string v2 = bc.getOnlyString("select path from model_path where name='savepath'");
            if (v1 == "")
            {
                Response.Write("找不到打印模版路径");
                return;
            }
            else if(v2=="")
            {
                Response.Write("找不到打印模版存储路径");
                return;
            }
              try
                {
                    ExcelPrint(dt,v1,v2);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
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
            /*worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            application.Visible = false;/*140323 use printpreview false to true1/2*/
            /*application.ExtendList = false;
            application.DisplayAlerts = false;
            application.AlertBeforeOverwriting = false;
            string v1 = DateTime.Now.ToString("yyyyMMddHHmmss").Replace("-", "/") + ".xls";
            string v2 = savepath + v1;
            worksheet.Cells[1, "A"] = dt2.Rows[i]["公司名称"].ToString();
            worksheet.Cells[3, "A"] = "公司地址：" + dt2.Rows[i]["公司地址"].ToString() + " " + "电话：" + dt2.Rows[i]["公司电话"].ToString() + " " + "传真：" + dt2.Rows[i]["公司传真"].ToString();
            for (i = 0; i < m; i++)
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
                worksheet.Cells[12 + 2 * i, "D"] = dt2.Rows[i]["客户料号"].ToString();
                worksheet.Cells[12 + 2 * i, "E"] = dt2.Rows[i]["料号"].ToString();
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

        protected void btnReconcile_Click(object sender, EventArgs e)
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

        protected void btnReductionReconcile_Click(object sender, EventArgs e)
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
