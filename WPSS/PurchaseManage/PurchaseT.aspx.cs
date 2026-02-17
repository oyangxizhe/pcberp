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
using System.Net;
using System.Text;
using XizheC;
using System.IO;
using OfficeOpenXml;

namespace WPSS.PurchaseManage
{
    public partial class PurchaseT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        basec bc = new basec();
        PrintPurchaseBill print = new PrintPurchaseBill();
        WPSS.Validate va = new Validate();
        int i;


        public static string[] str1 = new string[] { "", "" };
        public static string[] strE = new string[] { "" };
        public static string[] str2 = new string[] { "" };
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
        private static int _CIRCULATION_COUNT;
        public static int CIRCULATION_COUNT
        {
            set { _CIRCULATION_COUNT = value; }
            get { return _CIRCULATION_COUNT; }

        }
        string PUKEY, sql;
     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"] != null)
            {

                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    CIRCULATION_COUNT = 4;
                    Text1.Value = IDO;
                    //Text1.Value = "PU18050001";
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
        #region bind
        protected void bind()
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);

            string v1 = bc.getOnlyString("SELECT ADD_NEW FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购单'");
            string v2 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购单'");
            if (v1 == "Y")
            {
                Submit1.Visible = true;
             
            }
            else
            {
                Submit1.Visible = false;
            }
            if (v1 == "Y" || v2 == "Y")
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
            x.Value = "";
            x1.Value = "";
            RDID.Value = "";
            COKEY.Value = "";
            GridView1.DataSource = dtx();
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();
            dt1 = print.ask(Text1.Value);
            GridView2.DataSource = dt1;
            GridView2.DataKeyNames = new string[] { "索引" };
            GridView2.DataBind();

            DataTable dtx4 = print.asktotal(Text1.Value);
            if (dtx4.Rows.Count > 0)
            {
                string v8 = dtx4.Rows[dtx4.Rows.Count - 1]["未税金额"].ToString();
                string v9 = dtx4.Rows[dtx4.Rows.Count - 1]["税额"].ToString();
                string v10 = dtx4.Rows[dtx4.Rows.Count - 1]["含税金额"].ToString();
                if (!string.IsNullOrEmpty(v9))
                {
                    Text7.Value = string.Format("{0:F2}", Convert.ToDouble(v8));
                    Text8.Value = string.Format("{0:F2}", Convert.ToDouble(v9));
                    Text9.Value = string.Format("{0:F2}", Convert.ToDouble(v10));
                }
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
FROM PURCHASE_DET A LEFT JOIN WAREFILE B 
ON A.WAREID=B.WAREID " + " WHERE A.PUID='" + Text1.Value + "' AND B.FLKEY IS NOT NULL ORDER BY A.WAREID,B.FLKEY,B.OLDFILENAME";
            dt = basec.getdts(sql3);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataKeyNames = new string[] { "FLKEY" };
                GridView3.DataBind();
                x1.Value = Convert.ToString(1);
            }
            else
            {

                GridView3.DataSource = null;
            }
            string s1 = bc.getOnlyString("SELECT PURCHASESTATUS_MST FROM PURCHASE_MST WHERE PUID='" + Text1.Value + "'");
            if (s1 == "RECONCILE")
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
            dt = print.asktotal(Text1.Value);
            if (dt.Rows.Count > 0)
            {

                Text2.Value = dt.Rows[0]["供应商代码"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["供应商代码"].ToString()))
                {
                    currentdate();
                }
                else
                {
                    Text3.Value = dt.Rows[0]["采购日期"].ToString();
                    Text4.Value = dt.Rows[0]["采购员工号"].ToString();
                    Label1.Text = dt.Rows[0]["采购员姓名"].ToString();
                }
                Text5.Value = dt.Rows[0]["供应商名称"].ToString();
                Text6.Value = dt.Rows[0]["地址"].ToString();

                Text10.Value = dt.Rows[0]["联系人"].ToString();
                Text11.Value = dt.Rows[0]["电话"].ToString();
                if (dt.Rows[0]["收货人"].ToString() == "")
                {
                    contactinfo();
                }
                else
                {
                    Text12.Value = dt.Rows[0]["收货人"].ToString();
                    Text13.Value = dt.Rows[0]["收货地址"].ToString();
                    Text14.Value = dt.Rows[0]["公司名称"].ToString();
                    Text15.Value = dt.Rows[0]["公司联系人"].ToString();
                    Text16.Value = dt.Rows[0]["公司电话"].ToString();
                    RDID.Value = dt.Rows[0]["RDID"].ToString();
                    COKEY.Value = dt.Rows[0]["COKEY"].ToString();

                }

            }
            else
            {
                contactinfo();
                currentdate();
            }

            string sql4 = @"
SELECT 
A.FLKEY AS FLKEY,
A.WAREID AS WAREID,
A.OLDFILENAME AS OLDFILENAME,
A.PATH AS PATH 
FROM PRINT_FOR_PURCHASE A WHERE A.WAREID='" + Text1.Value + "' ORDER BY DATE DESC";
            dt = basec.getdts(sql4);
            if (dt.Rows.Count > 0)
            {
               
                GridView4.DataSource = dt;
                GridView4.DataKeyNames = new string[] { "FLKEY" };
                GridView4.DataBind();
                x1.Value = Convert.ToString(1);
            }
            else
            {
              
                GridView4.DataSource = null;
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

        protected void contactinfo()
        {
            dt7 = basec.getdts("SELECT * FROM RECEIVINGANDDELIVERY WHERE STATUS='Y'");
            if (dt7.Rows.Count > 0)
            {
                Text12.Value = dt7.Rows[0]["CONTACT"].ToString();
                Text13.Value = dt7.Rows[0]["ADDRESS"].ToString();
                RDID.Value = dt7.Rows[0]["RDID"].ToString();

            }

            dt8 = bc.getdt(@"select  B.COKEY AS COKEY,A.COID AS COID,A.CONAME AS CONAME,B.PHONE AS PHONE,B.FAX AS FAX,
B.EMAIL AS MAIL,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID )  AS MAKER,
A.DATE AS DATE,B.ADDRESS AS ADDRESS,B.CONTACT AS CONTACT from 
COMPANYINFO_MST A LEFT JOIN COMPANYINFO_DET B ON A.COKEY=B.COKEY");
            if (dt8.Rows.Count > 0)
            {
                Text14.Value = dt8.Rows[0]["CONAME"].ToString();
                Text15.Value = dt8.Rows[0]["CONTACT"].ToString();
                Text16.Value = dt8.Rows[0]["PHONE"].ToString();
                COKEY.Value = dt8.Rows[0]["COKEY"].ToString();

            }


        }
        protected void currentdate()
        {

            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            Text3.Value = DateTime.Now.ToString("yyy-MM-dd");
            Text4.Value = varMakerID;
            Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");

        }
        #region dtx
        protected DataTable dtx()
        {

            DataTable dt4 = new DataTable();
            dt4.Columns.Add("项次", typeof(string));
            dt4.Columns.Add("品号", typeof(string));
            dt4.Columns.Add("料号", typeof(string));
            dt4.Columns.Add("客户料号", typeof(string));
            dt4.Columns.Add("品名", typeof(string));
            dt4.Columns.Add("规格", typeof(string));
            dt4.Columns.Add("单位", typeof(string));
            dt4.Columns.Add("采购数量", typeof(string));
            dt4.Columns.Add("税率", typeof(string));
            dt4.Columns.Add("需求日期", typeof(string));
            dt4.Columns.Add("工程费", typeof(string));
            dt4.Columns.Add("备注", typeof(string));
            string v11 = bc.getOnlyString("SELECT SOURCESTATUS FROM PURCHASE_MST WHERE PUID='" + Text1.Value + "'");
            if (!string.IsNullOrEmpty(v11))
            {

                dt4 = basec.getdts(@"SELECT ROW_NUMBER() OVER (ORDER BY a.pukey ASC)  AS  项次,A.WAREID AS 品号,B.WNAME AS 品名,B.CO_WAREID AS 料号,B.CWAREID AS 客户料号,B.SPEC AS 规格,B.UNIT AS 单位,A.PCOUNT AS 采购数量,
A.TAXRATE AS 税率,A.NEEDDATE AS 需求日期,A.REMARK AS 备注 FROM PURCHASE_DET A LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID 
WHERE A.PUID='" + Text1.Value + "'");
             
                if (dt4.Rows.Count > 0)
                {
                    CIRCULATION_COUNT = dt4.Rows.Count;
                }

            }
            else
            {
                for (i = 1; i <= CIRCULATION_COUNT ; i++)
                {
                    DataRow dr = dt4.NewRow();
                    dr["项次"] = i;
                    dr["税率"] = 13;
                    dr["需求日期"] = DateTime.Now.ToString("yyy-MM-dd");
                    dr["备注"] = "";
                    dt4.Rows.Add(dr);

                }

            }
            return dt4;
        }
        #endregion
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            Text8.Value = "";
            Text9.Value = "";
            Text10.Value = "";
            Text11.Value = "";
            Text12.Value = "";
            Text13.Value = "";
            Label1.Text = "";
          

        }
        #region add
        protected void add()
        {
            hint.Value = "";
            string v11 = bc.getOnlyString("SELECT SOURCESTATUS FROM PURCHASE_MST WHERE PUID='" + Text1.Value + "'");
          

            if (bc.exists("select * from purchasegode_DET where PUID='" + Text1.Value + "'"))
            {

                hint.Value = "该采购单有了采购入库单不允许修改！";
            }
            else if (!bc.exists("SELECT * FROM SUPPLIERINFO_MST WHERE SUID='" + Text2.Value + "'"))
            {
                hint.Value = "供应商代码不存在于系统中！";
                return;

            }
            else if (Text4.Value == "")
            {
                hint.Value = "工号不能为空！";

            }
            else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "'"))
            {
                hint.Value = "员工工号不存在于系统中！";

            }
            else if (Text12.Value == "")
            {
                hint.Value = "收货人不能为空！";

            }
            else if (Text15.Value == "")
            {
                hint.Value = "公司联系人不能为空！";

            }
            else if (ac1() == 0)
            {

            }
            else if (!ac0(Text1.Value, Text2.Value))
            {

            }
            else if (PUKEY == "Exceed Limited")
            {
                hint.Value = "编码超出限制！";

            }

            else
            {
                if (!string.IsNullOrEmpty(v11))
                {

                    string sql2 = "DELETE FROM PURCHASE_MST WHERE PUID='" + Text1.Value + "'";
                    string sql3 = "DELETE FROM PURCHASE_DET WHERE PUID='" + Text1.Value + "'";
                    basec.getcoms(sql2);
                    basec.getcoms(sql3);
                }
                add2();
            }
          

        }
        #endregion

        #region add2()
        private void add2()
        {

            int k;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");

          

            for (k = 0; k <CIRCULATION_COUNT ; k++)
            {
                int s2;
                string SN;
                string v1 = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                string v2 = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                string v3 = ((TextBox)GridView1.Rows[k].Cells[2].FindControl("TextBox3")).Text;
                string v4 = ((TextBox)GridView1.Rows[k].Cells[3].FindControl("TextBox4")).Text;
                string v5 = ((TextBox)GridView1.Rows[k].Cells[4].FindControl("TextBox5")).Text;
                string v6 = ((TextBox)GridView1.Rows[k].Cells[5].FindControl("TextBox6")).Text;
                string v7 = ((TextBox)GridView1.Rows[k].Cells[6].FindControl("TextBox7")).Text;
                string v8 = ((TextBox)GridView1.Rows[k].Cells[7].FindControl("TextBox8")).Text;
                string v9 = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
                string v10 = ((TextBox)GridView1.Rows[k].Cells[9].FindControl("TextBox10")).Text;
                if (v1 != "")
                {

                    PUKEY = bc.numYMD(20, 12, "000000000001", "select * from PURCHASE_DET", "PUKEY", "PU");
                    DataTable dty = bc.getdt("SELECT * FROM PURCHASE_DET WHERE PUID='" + Text1.Value + "'");
                    if (dty.Rows.Count > 0)
                    {
                        s2 = Convert.ToInt32(dty.Rows.Count) + 1;
                    }
                    else
                    {
                        s2 = 1;
                    }
                    SN = Convert.ToString(s2);
                    basec.getcoms(@"INSERT INTO PURCHASE_DET(PUKEY,PUID,SN,WAREID,PCOUNT,PurchaseUNITPRICE,
           TAXRATE,SUID,NEEDDATE,PURCHASESTATUS_DET,URGENT,REMARK,YEAR,MONTH,DAY) 
                                 VALUES ('" + PUKEY + "','" + Text1.Value + "','" + SN + "','" + v1 + "','" + v5 +
                                  "','" + v6 + "','" + v7 + "','" + Text2.Value + "','" + v8 + "','OPEN','" + v9 +
                                  "','"+v10+"','" + year + "','" + month + "','" + day + "')");
                  
                }
            }
            if (!bc.exists("SELECT * FROM PURCHASE_DET WHERE PUID='" + Text1.Value + "'"))
            {
                return;

            }
            if (!bc.exists("SELECT PUID FROM PURCHASE_MST WHERE PUID='" + Text1.Value + "'"))
            {

                basec.getcoms("INSERT INTO PURCHASE_MST(PUID,SUID,"
          + "PDATE,PURID,PURCHASESTATUS_MST,Date,RDID,MakerID,Year,Month,Day,COKEY) values('" + Text1.Value
          + "','" + Text2.Value + "','" + Text3.Value + "','" + Text4.Value + "','OPEN','" + varDate + "','" + RDID.Value + "','" + varMakerID +
          "','" + year + "','" + month + "','" + day + "','" + COKEY.Value + "')");
                IFExecution_SUCCESS = true;


            }
            else
            {
                basec.getcoms("UPDATE PURCHASE_MST SET SUID='" + Text2.Value + "',PDATE='" + Text3.Value +
                    "',PURID='" + Text4.Value + "',RDID='" + RDID.Value + "',MAKERID='" + varMakerID +
                    "',DATE='" + varDate + "',COKEY='" + COKEY.Value + "' WHERE PUID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;

            }
         
        
            bind();
        }
        #endregion

        private bool ac0(string s1, string s2)
        {
            bool c = true;
            if (bc.exists("SELECT * FROM PURCHASE_DET WHERE PUID='" + s1 + "'"))
            {
                string s3 = bc.getOnlyString("SELECT SUID FROM PURCHASE_DET WHERE PUID='" + s1 + "'");
                if (!string.IsNullOrEmpty (s3)  && s3 != s2)
                {
                    hint.Value = "同一个采购单下面只能出现一个供应商代码!";
                    c = false;
                }
            }
            return c;

        }
        #region ac1()
        private int ac1()
        {

            int x = 1;
            
            for (int k = 0; k < CIRCULATION_COUNT ; k++)
            {

                string v1 = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                string v2 = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                string v3 = ((TextBox)GridView1.Rows[k].Cells[2].FindControl("TextBox3")).Text;
                string v4 = ((TextBox)GridView1.Rows[k].Cells[3].FindControl("TextBox4")).Text;
                string v5 = ((TextBox)GridView1.Rows[k].Cells[4].FindControl("TextBox5")).Text;
                string v6 = ((TextBox)GridView1.Rows[k].Cells[5].FindControl("TextBox6")).Text;
                string v7 = ((TextBox)GridView1.Rows[k].Cells[6].FindControl("TextBox7")).Text;
                string v8 = ((TextBox)GridView1.Rows[k].Cells[7].FindControl("TextBox8")).Text;
                string v9 = ((TextBox)GridView1.Rows[k].Cells[7].FindControl("TextBox9")).Text;
                DateTime temp = DateTime.MinValue;
                //bc.Show(v3 + "," + v4 + "," + v5 + "," + v6 + "," + v7);
                if (v1 == "")
                {

                }
                else if (!bc.exists("select * from WAREinfo where WAREid='" + v1 + "' AND ACTIVE='Y'"))
                {
                    x = 0;
                    hint.Value = "该品号不存在于系统中或状态不为正常！";

                }

                else if (v5 == "")
                {

                    x = 0;
                    hint.Value = "采购数量不能为空！";
                    break;
                }
                else if (bc.yesno(v5) == 0)
                {
                    x = 0;
                    hint.Value = "数量只能输入数字！";
                    break;

                }
                else if (v6 == "")
                {
                    x = 0;
                    hint.Value = "采购单价不能为空！";
                    break;

                }
                else if (bc.yesno(v6) == 0)
                {
                    x = 0;
                    hint.Value = "单价只能输入数字！";
                    break;

                }

                else if (v7 == "")
                {
                    x = 0;
                    hint.Value = "税率不能为空！";
                    break;

                }
                else if (bc.yesno(v7) == 0)
                {
                    x = 0;
                    hint.Value = "税率只能输入数字！";
                    break;

                }
                else if (v8 == "")
                {
                    x = 0;
                    hint.Value = "需求日期不能为空！";
                    break;

                }
                else if (!DateTime.TryParse(v8, out temp))
                {
                    x = 0;
                    hint.Value = "需求日期格式不正确！";
                    break;

                }
                else if (bc.yesno(v9) == 0)
                {
                    x = 0;
                    hint.Value = "工程费只能输入数字！";
                    break;

                }
            }
            return x;

        }
        #endregion
        private bool juage(string s1, string s2, string filed)
        {
            bool a = true;
            string w1 = bc.getOnlyString("select " + filed + " from WAREinfo where WAREid='" + s1 + "'");
            if (!string.IsNullOrEmpty(w1))
            {
                if (w1 != s2)
                {
                    a = false;
                }
            }
            return a;

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
        #region gridview2 delete
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 10, 10);
                string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购单'");
                string[] str = new string[] { "" };
                string sql1;
                hint.Value = "";
                string id = GridView2.DataKeys[e.RowIndex][0].ToString();
                str[0] = id;
                sql1 = "DELETE FROM PURCHASE_DET WHERE PUKEY='" + id + "'";
                DataTable dt = new DataTable();
            
                if (bc.exists("select * from purchasegode_det where PUID='" + Text1.Value + "'"))
                {
                    hint.Value = "该采购单已经在采购入库单中存在不允许删除！";
                }
                else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }
                else if (bc.juageOne("SELECT * FROM PURCHASE_DET WHERE PUID='" + Text1.Value + "'"))
                {

                    basec.getcoms(sql1);
                    sql = "DELETE PURCHASE_MST WHERE PUID='" + Text1.Value + "'";
                    basec.getcoms(sql);
                    GridView2.EditIndex = -1;
                    bind();

                }
                else
                {
                    basec.getcoms(sql1);
                    dt = bc.getdt("select * from purchase_det where puid='" + Text1.Value + "'");
                    StringBuilder sqb = new StringBuilder();
                    if(dt.Rows.Count>0)
                    {
                        for(int i=0;i<dt.Rows.Count;i++)
                        {
                          sqb.AppendFormat("update purchase_det set sn={0} where puid='{1}' and sn={2};",i+1,Text1.Value, dt.Rows[i]["sn"].ToString());//删除项次后项次重排序
                        }
                    }
                    bc.getcom(sqb.ToString());
                    GridView2.EditIndex = -1;
                    bind();


                }

            }
            catch (Exception)
            {


            }
        }
        #endregion


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
                Response.Redirect("../PurchaseManage/Purchase.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                PrintPurchaseBill print = new PrintPurchaseBill();
                DataTable dt = print.asko(Text1.Value);
                /*string v1 = bc.getOnlyString("select path from model_path where name='purchase2'");
                string v2 = bc.getOnlyString("select path from model_path where name='savepath'");
                //导出模版路径
                string filePath2 = Server.MapPath("/print_model/ERP订单格式_model.xls");
                // 设置Excel文件路径
                string output = "purchase_order_" + DateTime.Now.ToString("yyyyMMddHHmmssfff").Replace("-", "/") + ".xlsx";
                string savePath = Server.MapPath("/outputfile/" + output);
                string print_path= Server.MapPath(v1);
                if (v1 == "")
                {
                    Response.Write("找不到打印模版路径");
                    return;
                }
                else if (v2 == "")
                {
                    Response.Write("找不到打印模版存储路径");
                    return;
                }
                //ExcelPrint(dt, v1, v2);*/
                //Response.Write(print.sqlo1+" WHERE A.PUID='"+Text1.Value+"' ORDER BY A.PUKEY ASC<br>");
                ExcelPrint_for_EPPlus(dt);
                try
                {

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        private void addNEW()
        {
        

            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM PURCHASE_MST", "PUID", "PU");
            bind();

        }



        protected void btnReconcile_Click(object sender, EventArgs e)
        {

            try
            {
                reconcile();

            }
            catch (Exception)
            {

            }
        }
        #region rconcile
        protected void reconcile()
        {

            hint.Value = "";
            sql = @"SELECT A.PUID AS PUID,A.SN AS SN,B.WAREID AS WAREID,C.PCOUNT AS PCOUNT,SUM(B.GECOUNT) AS GECOUNT FROM PURCHASEGODE_DET A 
LEFT JOIN GODE B ON A.PGKEY=B.GEKEY 
LEFT JOIN PURCHASE_DET C ON C.PUID=A.PUID AND C.SN=A.SN WHERE  A.PUID='" + Text1.Value + "' GROUP BY A.PUID,A.SN,B.WAREID,C.PCOUNT";
            DataTable dt2 = basec.getdts(sql);
            if (dt2.Rows.Count > 0)
            {
                if (bc.JuageOrderOrPurchaseStatus(Text1.Value, 1))
                {

                    basec.getcoms("UPDATE PURCHASE_MST SET PURCHASESTATUS_MST='RECONCILE' WHERE PUID='" + Text1.Value + "'");
                    bind();
                }
                else
                {

                    hint.Value = "此采购单没有结案，不允许确认对帐!";
                }

            }
            else
            {
                hint.Value = "没有此采购单的入库记录!";
            }

        }
        #endregion
        protected void btnReductionReconcile_Click(object sender, EventArgs e)
        {
            try
            {
                basec.getcoms("UPDATE PURCHASE_MST SET PURCHASESTATUS_MST='CLOSE' WHERE PUID='" + Text1.Value + "'");
                bind();
            }
            catch (Exception)
            {

            }
        }
        protected void btnLinkButton3_Click(object sender, EventArgs e)
        {
            upload_download("PrintModelForPurchase_o.xls");
        }
        protected void btnLinkButton4_Click(object sender, EventArgs e)
        {

            upload_download("PrintModelForPurchase_t.xls");
        }
        protected void btnLinkButton5_Click(object sender, EventArgs e)
        {

            upload_download("PrintModelForPurchase_th.xls");
        }
        private void upload_download(string filename)
        {
            PrintPurchaseBill print = new PrintPurchaseBill();
            string PRINT_ID = bc.numYMD(20, 12, "000000000001", "select * from TEMP_PRINT_FOR_PURCHASE", "PRINT_ID", "PN");
            print.PRINT_ID = PRINT_ID;
            DataTable dtn = print.asko(Text1.Value);
            DataTable dt = new DataTable();

            string sql4 = @"
SELECT 
A.FLKEY AS FLKEY,
A.WAREID AS WAREID,
A.OLDFILENAME AS OLDFILENAME,
A.PATH AS PATH 
FROM PRINT_FOR_PURCHASE A WHERE A.WAREID='" + Text1.Value + "' ORDER BY DATE DESC";
            dt = basec.getdts(sql4);
            if (dt.Rows.Count > 0)
            {

                GridView4.DataSource = dt;
                GridView4.DataKeyNames = new string[] { "FLKEY" };
                GridView4.DataBind();
                x1.Value = Convert.ToString(1);
            }
            else
            {
              
                GridView4.DataSource = null;
            }
            DataTable dt1 = bc.getdt("SELECT PRINT_ID,采购单号 FROM TEMP_PRINT_FOR_PURCHASE WHERE STATUS='N' GROUP BY PRINT_ID,采购单号");
            dt = bc.getdt("SELECT * FROM TEMP_PRINT_FOR_PURCHASE  WHERE STATUS='N' ORDER BY PRINT_ID ASC");
     
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {

                    DataTable dtx = bc.GET_DT_TO_DV_TO_DT(dt, "", "PRINT_ID='" + dr["PRINT_ID"].ToString() + "'");


                    bc.ExcelPrint(dtx, "采购单", "/pss/PrintFile/");


                    if (bc.IFExecution_SUCCESS)
                    {
                        basec.getcoms("UPDATE  TEMP_PRINT_FOR_PURCHASE SET STATUS='Y' WHERE PRINT_ID='" + dr["PRINT_ID"].ToString() + "'");
                    }
                }
            }
            else
            {
                hint.Value = "无数据可打印";
            }
            try
            {
       
            }
            catch (Exception)
            {

            }


        }
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {

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
            string v2 = savepath + v1;*/

            /*
                Microsoft.Office.Interop.Excel.Range m_objRange = worksheet.get_Range(worksheet.Cells[46, "E"],worksheet.Cells[46, "E"]);
            m_objRange.Select();

            float PicLeft, PicTop, PicWidth, PicHeight;　　　　//距离左边距离，顶部距离，图片宽度、高度
            PicTop = Convert.ToSingle(m_objRange.Top);
            PicWidth = Convert.ToSingle(m_objRange.MergeArea.Width);
            PicHeight = Convert.ToSingle(m_objRange.Height);
            PicWidth = Convert.ToSingle(m_objRange.Width) ;
            PicLeft = Convert.ToSingle(m_objRange.Left);
            //worksheet.Cells[2, "H"] = "PICTOP="+PicTop+"WIDTH="+PicWidth +"HEIGHT="+PicHeight +"LEFT="+PicLeft ;
            try//对没有图片的会报错所以加下面的出错机制
            {
                //Microsoft.Office.Interop.Excel.Pictures pict = (Microsoft.Office.Interop.Excel.Pictures)worksheet.Pictures(Type.Missing);
                //pict.Insert(Server.MapPath(dt.Rows[i]["path_initial"].ToString().Replace("..", "")));
                worksheet.Shapes.AddPicture(Server.MapPath("/image/x1.jpg"),
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoCTrue, PicLeft, PicTop, 192,208);
            }
            catch (Exception)
            {
            }*/


            /*worksheet.Cells[1, "A"] = dt2.Rows[i]["公司名称"].ToString();
            worksheet.Cells[6, "B"] = dt2.Rows[i]["公司名称"].ToString();
            for (i = 0; i < m; i++)
            {
                worksheet.Cells[4, 2] = dt2.Rows[i]["采购日期"].ToString();
                worksheet.Cells[4, 11] = dt2.Rows[i]["采购单号"].ToString();

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
                //worksheet.Cells[35, 7] = dt2.Rows[i]["付款方式"].ToString();
                worksheet.Cells[35, 9] = dt2.Rows[i]["付款条件"].ToString();
                worksheet.Cells[55, 9] = dt2.Rows[i]["供应商名称"].ToString();
                worksheet.Cells[57, 1] = dt2.Rows[i]["采购日期"].ToString();

                worksheet.Cells[17 + 2 * i, 2] = dt2.Rows[i]["客户料号"].ToString();
                worksheet.Cells[17 + 2 * i, 4] = dt2.Rows[i]["品名"].ToString();
                worksheet.Cells[17 + 2 * i, 6] = dt2.Rows[i]["采购数量"].ToString();
                worksheet.Cells[17 + 2 * i, 8] = dt2.Rows[i]["采购单价"].ToString();
                worksheet.Cells[17 + 2 * i, 9] = dt2.Rows[i]["未税金额"].ToString();
                worksheet.Cells[17 + 2 * i, 10] = dt2.Rows[i]["含税金额"].ToString();
                worksheet.Cells[17 + 2 * i, 11] = dt2.Rows[i]["需求日期"].ToString();
                worksheet.Cells[17 + 2 * i, 12] = dt2.Rows[i]["备注"].ToString();
                workbook.SaveAs(v2);
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
            string filePath2 = Server.MapPath("/Print_Model/ERP采购订单格式_model.xlsx");
            // 设置Excel文件路径
            string v1 = "purchase_order_" + DateTime.Now.ToString("yyyyMMddHHmmssfff").Replace("-", "/") + ".xlsx";
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    worksheet.Cells[4, 2].Value= dt.Rows[i]["采购日期"].ToString();
                    worksheet.Cells[4, 11].Value= dt.Rows[i]["采购单号"].ToString();
                    worksheet.Cells[6,2].Value = dt.Rows[i]["公司名称"].ToString();
                    worksheet.Cells[6, 8].Value= dt.Rows[i]["公司联系人"].ToString();
                    worksheet.Cells[6, 12].Value= dt.Rows[i]["公司电话"].ToString();
                    worksheet.Cells[8, 2].Value= dt.Rows[i]["供应商名称"].ToString();
                    worksheet.Cells[8, 8].Value= dt.Rows[i]["联系人"].ToString();
                    worksheet.Cells[8, 12].Value= dt.Rows[i]["电话"].ToString();
                    worksheet.Cells[9, 2].Value= dt.Rows[i]["地址"].ToString();
                    worksheet.Cells[11, 2].Value= dt.Rows[i]["收货地址"].ToString();
                    worksheet.Cells[11, 8].Value= dt.Rows[i]["收货人"].ToString();
                    worksheet.Cells[11, 12].Value= dt.Rows[i]["收货人电话"].ToString();
                    worksheet.Cells[27, 9].Value= dt.Rows[dt.Rows.Count - 1]["合计含税金额"].ToString();
                    //worksheet.Cells[35, 7].Value= dt.Rows[i]["付款方式"].ToString();
                    worksheet.Cells[35, 9].Value= dt.Rows[i]["付款条件"].ToString();
                    worksheet.Cells[55, 9].Value= dt.Rows[i]["供应商名称"].ToString();
                    worksheet.Cells[57, 1].Value= dt.Rows[i]["采购日期"].ToString();

                    worksheet.Cells[17 + 2 * i, 2].Value= dt.Rows[i]["客户料号"].ToString();
                    worksheet.Cells[17 + 2 * i, 4].Value= dt.Rows[i]["品名"].ToString();
                    worksheet.Cells[17 + 2 * i, 6].Value= dt.Rows[i]["采购数量"].ToString();
                    worksheet.Cells[17 + 2 * i, 8].Value= dt.Rows[i]["采购单价"].ToString();
                    worksheet.Cells[17 + 2 * i, 9].Value= dt.Rows[i]["未税金额"].ToString();
                    worksheet.Cells[17 + 2 * i, 10].Value= dt.Rows[i]["含税金额"].ToString();
                    worksheet.Cells[17 + 2 * i, 11].Value= dt.Rows[i]["需求日期"].ToString();
                    worksheet.Cells[17 + 2 * i, 12].Value= dt.Rows[i]["备注"].ToString();

                }

                // 保存Excel文件
                package.SaveAs(filePath);
                //MessageBox.Show("写入到Excel文件成功！！！");
                Response.Redirect("/outputfile/" + v1);//将文件输出到浏览器供用户下载
            }

        }
        #endregion
        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string v1 = GridView4.DataKeys[GridView4.SelectedIndex].Values[0].ToString();
            string FilePath = bc.getOnlyString("SELECT PATH FROM PRINT_FOR_PURCHASE WHERE FLKEY='" + v1 + "'");
            FileInfo file = new FileInfo(Server .MapPath( "/pss/PrintFile/"+FilePath));
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
            else
            {
                bc.Show("/PrintFile/"+FilePath );

            }
        }

    

    }
}
