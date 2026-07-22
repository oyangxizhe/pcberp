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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;



namespace WPSS.SellManage
{
    public partial class CuReconcileT : System.Web.UI.Page
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
        StringBuilder sqb = new StringBuilder();
        #region nature
        private string _SEKEY;
        public string SEKEY
        {
            set { _SEKEY = value; }
            get { return _SEKEY; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _SSKEY;
        public string SSKEY
        {
            set { _SSKEY = value; }
            get { return _SSKEY; }

        }
        private string _SESRID;
        public string SESRID
        {
            set { _SESRID = value; }
            get { return _SESRID; }

        }
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }

        }
        private string _CRID;
        public string CRID
        {
            set { _CRID = value; }
            get { return _CRID; }

        }

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
        private int _CIRCULATION_COUNT;
        public int CIRCULATION_COUNT
        {
            set { _CIRCULATION_COUNT = value; }
            get { return _CIRCULATION_COUNT; }

        }

        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }

        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }
        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        #endregion
        PrintSellTableBill print = new PrintSellTableBill();
        CORDER corder = new CORDER();
        int i;
        CORDER_RECONCILE cr = new CORDER_RECONCILE();
        CORDER_RECONCILE corder_reconcile = new CORDER_RECONCILE();


        public static string[] NEWID = new string[] { "", "" };
        public static string[] str2 = new string[] { "" };
        string KEY;
        public double NOTAX_AMOUNT { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {

                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    CheckBox2.AutoPostBack = true;
                    CheckBox3.AutoPostBack = true;
                    try
                    {
                        if(Request.QueryString["CRID"]!=null)
                        Text1.Value = Request.QueryString["CRID"].ToString();
                        bind();
                    }
                    catch (Exception)
                    {

                        if (NEWID[0] != "")
                        {
                            Text1.Value = NEWID[0];
                            bind();
                        }
                    }
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
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
            if (Text1.Value != "")
            {
                dt = corder_reconcile.RETURN_PG_AND_RETURN_DT2(Text1.Value, "", "", "", "", "", "", false, true);
                if (dt.Rows.Count > 1)
                {
                    Text2.Value = dt.Rows[0]["客户名称"].ToString();
                    //Response.Write( corder_reconcile.MESSAGE);
                }
                else if (Text2.Value != "")
                {

                    dt = corder_reconcile.RETURN_PG_AND_RETURN_DT(Text2.Value, "", "", "未对账", "", "", false, true);
                    x.Value = "1";
                    x2.Value = "1";
                }
            }
          

            if (dt.Rows.Count > 1)
            {
                x.Value = "1";
                x2.Value = "1";
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            btnSure.ForeColor = System.Drawing.Color.Blue;
            status();
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
            }
            GridView1.DataBind();
           
        }
        #endregion
        #region status
        protected void status()
        {
            string s1 = bc.getOnlyString("SELECT STATUS FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'");
            if (s1 == "RECONCILE" || s1 == "INVOICE")
            {
                Submit5.Value = "已对帐";


            }
            else
            {
                Submit5.Value = "确认对帐";


            }
        }
        #endregion
        protected void btnSure_Click(object sender, EventArgs e)
        {

            if (!bc.exists("SELECT * FROM CUSTOMERINFO_MST WHERE CNAME='" + Text2.Value + "'"))
            {
                hint.Value = "该客户名称不存在于系统中！";
                return;

            }
            bind();
        }
        protected void ClearText()
        {

            x2.Value = "";
            Text2.Value = "";
           
        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {


        }
        #region juage_ABSTRACT_NOEMPTY()
        private bool juage_ABSTRACT_NOEMPTY()
        {
            bool b = false;
           
            

            return b;

        }
        #endregion
        #region JUAGE_IFEXISTS_SELECT()
        private bool JUAGE_IFEXISTS_SELECT()
        {
            bool b = false;
            for (int k = 0; k < GridView1.Rows.Count - 1; k++)
            {
                CheckBox chb = ((CheckBox)GridView1.Rows[k].Cells[0].FindControl("CheckBox1"));
                if (chb.Checked)
                {
                    b = true;
                    break;
                }
            }
            if (b == false)
            {
                hint.Value = "无选中项！";
            }
            return b;
        }
        #endregion
        private bool juage()
        {
            bool b = false;
            string s1 = bc.getOnlyString("SELECT STATUS FROM [CU_RECONCILE_DET] WHERE CRID='" + Text1.Value + "'");
            if (s1 == "RECONCILE")
            {
                hint.Value = "此对账单已对账，不允许修改";
                b = true; 
            }

            return b;

        }
        #region add
        private void add()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace("/", "-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            DataTable dtt = cr.DT_EMPTY();
            bc.getcom("DELETE CU_RECONCILE_DET WHERE CRID='"+Text1 .Value +"'");
            NOTAX_AMOUNT = 0;
            HttpCookie cookie = Request.Cookies["cookiename"];
            MAKERID = cookie.Values["EMID"].ToString();
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
            {
                CheckBox chb = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                SESRID = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox3")).Text;/**/
                string NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox202")).Text;/**/
                string v1 = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox4")).Text;/**/
                double d1 = Convert.ToDouble(v1);
           
                if (chb.Checked)
                {
                    
                    SSKEY = NO;

                    SQlcommandE_DET(cr.sqlo);
                    sqb = new StringBuilder();
                    if (SESRID.Substring(0, 2) == "SE")
                    {
                        sqb.AppendFormat("UPDATE SELLTABLE_DET SET STATUS='SAVE' WHERE SEID='" + SESRID  + "'");
                    }
                    else
                    {
                        sqb.AppendFormat("UPDATE SELLRETURN_DET SET STATUS='SAVE' WHERE SRID='" + SESRID  + "'");
                    }
                    NOTAX_AMOUNT = NOTAX_AMOUNT + d1;
                    bc.getcom(sqb.ToString());
                    IFExecution_SUCCESS = true;
                }
            }
            if (!bc.exists("SELECT * FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'"))
            {
                return;

            }
            if (!bc.exists("SELECT CRID FROM CU_RECONCILE_MST WHERE CRID='" + Text1.Value + "'"))
            {
                SQlcommandE_MST(cr.sqlt);
                IFExecution_SUCCESS = true;
            }
            else
            {
                SQlcommandE_MST(cr.sqlth+" WHERE CRID='"+Text1 .Value +"'");
                IFExecution_SUCCESS = true;
            }
            bind();
        }
        #endregion
        #region  SQlcommandE_DET
        protected void SQlcommandE_DET(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            KEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM CU_RECONCILE_DET", "CRKEY", "CR");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@CRKEY", SqlDbType.VarChar, 20).Value = KEY;
            sqlcom.Parameters.Add("@CRID", SqlDbType.VarChar, 20).Value = Text1.Value;
            sqlcom.Parameters.Add("@SSKEY", SqlDbType.VarChar, 20).Value = SSKEY;
            sqlcom.Parameters.Add("@STATUS", SqlDbType.VarChar, 20).Value = "N";
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();

        }
        #endregion
        #region  SQlcommandE_MST
        protected void SQlcommandE_MST(string sql)
        {
            //bc.Show("OKW" + INVOICE_NO + INVOICE_NOTAX_AMOUNT + INVOICE_TAX_AMOUNT + AMOUNT);
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace("/", "-");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@CRID", SqlDbType.VarChar, 20).Value = Text1.Value;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='" + Text2.Value + "'");
            sqlcom.Parameters.Add("@NOTAX_AMOUNT", SqlDbType.VarChar, 20).Value = NOTAX_AMOUNT;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
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

          
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                ClearText();
                Text1.Value = cr.GETID();
                bind();

            }
            else if (submit.ID == "Submit2")
            {
                if (juage_ABSTRACT_NOEMPTY())
                {
                }
                else if (!JUAGE_IFEXISTS_SELECT())
                {

                }
                else if (juage())
                {

                }
                else
                {
                    add();
                }

                try
                {


                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                if (Request.QueryString["aore"].ToString() == "0")
                {

                    Response.Redirect("../SellManage/cureconcile.aspx");
                }
                else
                {

                    //Response.Write("<script>window.opener=null;window.close();</script>");
                    Response.Write("<script lanuage=javascript>");
                    Response.Write("window.parent.close()");
                    Response.Write("</script>");
                }
                try
                {


                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);

                    //Response.Redirect("../SellManage/cureconcile.aspx" + n2);

                }
            }
            else if (submit.ID == "Submit4")
            {
                dt = corder_reconcile.RETURN_PG_AND_RETURN_DT2(Text1.Value, "", "", "", "", "", "", false, true);

                /*if (Text1.Value == "")
               {
                   hint.Value = "客户名称不能为空";
                   return;
               }
               else if (!bc.exists("select * from customerinfo_mst where cname='" + Text1.Value + "'"))
               {
                   hint.Value = "客户名称不存在系统中";
                   return;
               }
              else if (StartDate.Value == "")
               {
                   hint.Value = "账期不能为空";
                   return;
               }*/
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(corder_reconcile.MESSAGE);
                    string v1 = bc.getOnlyString("select path from model_path where name='ORDER_RECONCILE'");
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
                    }

                    ExcelPrint_for_epplus(dt, v1, v2);
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
                else
                {
                    hint.Value = "没有可打印数据";
                }
            }
            else if (submit.ID == "Submit5")
            {
                try
                {
                    hint.Value = "";
                    if (!bc.exists("SELECT * FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'"))
                    {
                        hint.Value = "先保存单据才能点击对账";
                        return;
                    }
                    string v1 = bc.getOnlyString("SELECT STATUS FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'");
                    if (v1 == "N")
                    {
                        sqb = new StringBuilder();
                        sqb.AppendFormat("UPDATE CU_RECONCILE_DET SET STATUS='RECONCILE' WHERE CRID='" + Text1.Value + "'");
                        dt = corder_reconcile.RETURN_PG_AND_RETURN_DT2(Text1.Value, "", "", "", "", "", "", false, true);
                        DataTable dtx = bc.RETURN_NOHAVE_REPEAT_DT(dt, "销货销退单号");

                        foreach (DataRow dr in dtx.Rows)
                        {
                            if (dr["VALUE"].ToString().Substring(0, 2) == "SE")
                            {
                                sqb.AppendFormat(";UPDATE SELLTABLE_DET SET STATUS='RECONCILE' WHERE SEID='" + dr["VALUE"].ToString() + "'");
                            }
                            else
                            {
                                sqb.AppendFormat(";UPDATE SELLRETURN_DET SET STATUS='RECONCILE' WHERE SRID='" + dr["VALUE"].ToString() + "'");
                            }

                        }
                        //Response.Write(corder_reconcile .MESSAGE );
                        basec.getcoms(sqb.ToString());
                        status();
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
                if (!bc.exists("SELECT * FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'"))
                {
                    hint.Value = "先保存单据才能点击对账";
                    return;
                }
                string v1 = bc.getOnlyString("SELECT STATUS FROM CU_RECONCILE_DET WHERE CRID='" + Text1.Value + "'");
                if (v1 == "RECONCILE")
                {
                    sqb = new StringBuilder();
                    sqb.AppendFormat("UPDATE CU_RECONCILE_DET SET STATUS='N' WHERE CRID='" + Text1.Value + "'");
                    corder_reconcile = new CORDER_RECONCILE();
                    dt = corder_reconcile.RETURN_PG_AND_RETURN_DT2(Text1.Value, "", "", "", "", "", "", false, true);
                    DataTable dtx = bc.RETURN_NOHAVE_REPEAT_DT(dt, "销货销退单号");
                    foreach (DataRow dr in dtx.Rows)
                    {
                        if (dr["VALUE"].ToString().Substring(0, 2) == "SE")
                        {
                            sqb.AppendFormat(";UPDATE SELLTABLE_DET SET STATUS='SAVE' WHERE SEID='" + dr["VALUE"].ToString() + "'");
                        }
                        else
                        {
                            sqb.AppendFormat(";UPDATE SELLRETURN_DET SET STATUS='SAVE' WHERE SRID='" + dr["VALUE"].ToString() + "'");
                        }
                    }
                    //Response.Write(sqb.ToString());
                    basec.getcoms(sqb.ToString());
                    status();
                }
                else if (v1 == "INVOICE")
                {
                    hint.Value = "状态为已开票不能还原";
                }
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            

        }
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

           
         
         
        }
        #region gridview deleting
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

           
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


 
        }
        #region ExcelPrint
        public void ExcelPrint_old(DataTable dt2, string Printpath, string savepath)
        {
            int i;
           /* Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            int m = dt2.Rows.Count - 1;
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

            worksheet.Cells[1, "A"] = dt2.Rows[0]["公司名称"].ToString();
            worksheet.Cells[4, "G"] = dt2.Rows[0]["公司名称"].ToString();
            worksheet.Cells[5, "G"] = dt2.Rows[0]["公司联系人"].ToString();
            worksheet.Cells[6, "G"] = dt2.Rows[0]["公司电话"].ToString();
            worksheet.Cells[7, "G"] = dt2.Rows[0]["公司邮箱"].ToString();

            worksheet.Cells[2, "A"] = DateTime.Now.ToString("yyyy") + "年" + DateTime.Now.AddMonths(-1).ToString("MM") + "月" + "对账单";
            worksheet.Cells[4, "C"] = dt2.Rows[0]["客户名称"].ToString();
            worksheet.Cells[5, "C"] = dt2.Rows[0]["联系人"].ToString();
            worksheet.Cells[6, "C"] = dt2.Rows[0]["联系电话"].ToString();
            worksheet.Cells[7, "C"] = dt2.Rows[0]["EMAIL"].ToString();
            worksheet.Cells[41, "I"] = dt2.Rows[0]["制单人"].ToString();
            for (i = 0; i < m; i++)
            {
                worksheet.Cells[11 + 1 * i, "A"] = (i + 1).ToString();
                worksheet.Cells[11 + 1 * i, "B"] = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "/");
                worksheet.Cells[11 + 1 * i, "C"] = dt2.Rows[i]["销货销退单号"].ToString();
                worksheet.Cells[11 + 1 * i, "D"] = dt2.Rows[i]["品名"].ToString();
                worksheet.Cells[11 + 1 * i, "E"] = dt2.Rows[i]["客户料号"].ToString();
                worksheet.Cells[11 + 1 * i, "F"] = dt2.Rows[i]["客户订单号"].ToString();
                worksheet.Cells[11 + 1 * i, "G"] = dt2.Rows[i]["销货销退数量"].ToString();
                worksheet.Cells[11 + 1 * i, "H"] = dt2.Rows[i]["销售单价"].ToString();
                worksheet.Cells[11 + 1 * i, "I"] = dt2.Rows[i]["工程费"].ToString();
                worksheet.Cells[11 + 1 * i, "J"] = dt2.Rows[i]["未税金额"].ToString();
                worksheet.PrintOut(1, 1, 1, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
        // 首先需要安装 EPPlus NuGet 包
// Install-Package EPPlus




public void ExcelPrint_for_epplus(DataTable dt2, string Printpath, string savepath)
    {
            //导出模版路径
            string filePath2 = Server.MapPath("/Print_Model/客户对账单.xlsx");
            // 设置Excel文件路径
            string v1 = "CuReconcile_" + DateTime.Now.ToString("yyyyMMddHHmmssfff").Replace("-", "/") + ".xlsx";
            string filePath = Server.MapPath("/outputfile/" + v1);

            // 创建一个新的Excel包

            FileInfo file = new FileInfo(filePath2);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                //获取Excel中的第n张表：

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                worksheet.Cells[1, 1].Value = dt2.Rows[0]["公司名称"].ToString(); // A1
                worksheet.Cells[4, 7].Value = dt2.Rows[0]["公司名称"].ToString(); // G4
                worksheet.Cells[5, 7].Value = dt2.Rows[0]["公司联系人"].ToString(); // G5
                worksheet.Cells[6, 7].Value = dt2.Rows[0]["公司电话"].ToString(); // G6
                worksheet.Cells[7, 7].Value = dt2.Rows[0]["公司邮箱"].ToString(); // G7

                // 对账单标题
                worksheet.Cells[2, 1].Value = DateTime.Now.ToString("yyyy") + "年" +
                                              DateTime.Now.AddMonths(-1).ToString("MM") + "月" + "对账单"; // A2

                // 客户信息
                worksheet.Cells[4, 3].Value = dt2.Rows[0]["客户名称"].ToString(); // C4
                worksheet.Cells[5, 3].Value = dt2.Rows[0]["联系人"].ToString(); // C5
                worksheet.Cells[6, 3].Value = dt2.Rows[0]["联系电话"].ToString(); // C6
                worksheet.Cells[7, 3].Value = dt2.Rows[0]["EMAIL"].ToString(); // C7

                // 制单人
                worksheet.Cells[41, 9].Value = dt2.Rows[0]["制单人"].ToString(); // I41

                // 4. 填充数据行（从第11行开始）
                int rowCount = dt2.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    int currentRow = 11 + i; // 从第11行开始
                    if (i < rowCount - 1)
                    {
                        worksheet.Cells[currentRow, 1].Value = (i + 1).ToString();                    // A列 - 序号
                        worksheet.Cells[currentRow, 2].Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "/"); // B列 - 日期
                        worksheet.Cells[currentRow, 3].Value = dt2.Rows[i]["销货销退单号"].ToString(); // C列
                        worksheet.Cells[currentRow, 4].Value = dt2.Rows[i]["品名"].ToString();         // D列
                        worksheet.Cells[currentRow, 5].Value = dt2.Rows[i]["客户料号"].ToString();     // E列
                        worksheet.Cells[currentRow, 6].Value = dt2.Rows[i]["客户订单号"].ToString();   // F列
                        worksheet.Cells[currentRow, 7].Value = dt2.Rows[i]["销货销退数量"].ToString(); // G列
                        worksheet.Cells[currentRow, 8].Value = dt2.Rows[i]["销售单价"].ToString();     // H列
                        worksheet.Cells[currentRow, 9].Value = dt2.Rows[i]["工程费"].ToString();       // I列
                        worksheet.Cells[currentRow, 10].Value = dt2.Rows[i]["未税金额"].ToString();    // J列
                    }
                    else
                    {
                        worksheet.Cells[currentRow, 10].Value = dt2.Rows[i]["未税金额"].ToString();    // J列
                    }
        
                
                
                }


                // 保存Excel文件
                package.SaveAs(filePath);
                //MessageBox.Show("写入到Excel文件成功！！！");
                Response.Redirect("/outputfile/" + v1);//将文件输出到浏览器供用户下载
            }

        }
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
           

                   
           
        }

        protected void btnReductionReconcile_Click(object sender, EventArgs e)
        {

        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked)
            {
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;

                }
            }
            else
            {
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;

                }

            }
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked)
            {
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox cbx = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                    if (cbx.Checked)
                    {
                        ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
                    }
                    else
                    {
                        ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;

                    }

                }
            }
            else
            {
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox cbx = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                    if (cbx.Checked)
                    {
                        ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
                    }
                    else
                    {
                        ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;

                    }

                }

            }
        }
   
    }
}
