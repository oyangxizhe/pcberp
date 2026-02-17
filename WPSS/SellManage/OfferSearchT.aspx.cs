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

namespace WPSS.SellManage
{
    public partial class OfferSearchT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        PrintOfferBill print = new PrintOfferBill();
        COFFER_SEARCH coffer_search = new COFFER_SEARCH();
        string v1, v2;
        public static string[] str1 = new string[] { "" };
        public static string[] strE = new string[] { "" };
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        Text15.Value = IDO;
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
        private void bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }

          
                dt = basec.getdts(coffer_search .sql +" WHERE A.OFID='"+Text15.Value +"'");
                if (dt.Rows.Count > 0)
                {
                    

                    Text2.Value = dt.Rows[0]["ID"].ToString();
                    Text3.Value = dt.Rows[0]["料号"].ToString();
                    Text4.Value = dt.Rows[0]["品名"].ToString();
                    Text5.Value = dt.Rows[0]["客户料号"].ToString();
                    Text6.Value = dt.Rows[0]["客户"].ToString();
                    Text7.Value = dt.Rows[0]["销售单价"].ToString();
                    //TextBox1.Text = dt.Rows[0]["REMARK"].ToString();

                    Text8.Value = dt.Rows[0]["量产单价"].ToString();
                    Text9.Value = dt.Rows[0]["量产数量"].ToString();
                    Text10.Value = dt.Rows[0]["SAMPLE单价"].ToString();
                    Text11.Value = dt.Rows[0]["SAMPLE数量"].ToString();
                    Text12.Value = dt.Rows[0]["小量单价"].ToString();
                    Text13.Value = dt.Rows[0]["SAMPLE数量"].ToString() + "～" + dt.Rows[0]["量产数量"].ToString();
                    Text15.Value = dt.Rows[0]["报价单号"].ToString();
                    Text14.Value = dt.Rows[0]["工程费"].ToString();
                    TextBox1.Text = dt.Rows[0]["备注"].ToString();
                    Text16.Value = dt.Rows[0]["批量单价"].ToString();
                    Text17.Value = dt.Rows[0]["单位面积价格"].ToString();
                }
                DataTable dtx;
                dtx = bc.getdt("select * from set_showname");
                if (dtx.Rows.Count > 0)
                {
                    Label1.Text = dtx.Rows[0]["co_wareid"].ToString();
                    Label2.Text = dtx.Rows[0]["wname"].ToString();
                    Label3.Text = dtx.Rows[0]["cwareid"].ToString();
                }
        }
        protected void ClearText()
        {
           
        }
        #region
        protected void save()
        {
            hint.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            v1 = bc.getOnlyString("SELECT WAREID FROM SELLUNITPRICE WHERE  SPID='" + Text1.Value + "'");
            v2 = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='" + Text3.Value + "'");
            string v3 = bc.getOnlyString("SELECT CUID FROM SELLUNITPRICE WHERE  SPID='" + Text1.Value + "'");
            if (!juage1(v2))
            {

            }
            else
            {


            }
        }
        #endregion
        private bool juage1(string v2)
        {
            bool ju = true;

            if (!bc.exists("SELECT * FROM WAREINFO WHERE WAREID='" + Text2.Value + "' AND ACTIVE='Y'"))
            {
                ju = false;
                hint.Value = "该品号不存在于系统或状态不为正常！";
            }

            else if (bc.yesno(Text7.Value) == 0)
            {
                ju = false;
                hint.Value = "单价只能输入数字！";
            }

            return ju;

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

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {

            }
            else if (submit.ID == "Submit2")
            {
                hint.Value = "";
                if (Text2.Value == "")
                {
                    hint.Value = "ID不能为空！";
                }

                else
                {
                    SQlcommandE(coffer_search.sqlt + " WHERE OFID='" + Text15.Value + "'");

                    if (IFExecution_SUCCESS == true)
                    {
                        bind();

                    }
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
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../SELLManage/offersearch.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                bind();
                toexel();
            }
            else if (submit.ID == "Submit5")
            {

            }
            else if (submit.ID == "Submit6")
            {

            }
        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
         


        }
        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@OFID", SqlDbType.VarChar, 20).Value = Text15.Value;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = Text2.Value;
            sqlcom.Parameters.Add("@QUANTITY_UNITPRICE", SqlDbType.VarChar, 20).Value = Text8.Value;
            sqlcom.Parameters.Add("@QUANTITY_COUNT", SqlDbType.VarChar, 20).Value = Text9.Value;
            sqlcom.Parameters.Add("@SAMPLE_UNITPRICE", SqlDbType.VarChar, 20).Value = Text10.Value;
            sqlcom.Parameters.Add("@SAMPLE_COUNT", SqlDbType.VarChar, 20).Value = Text11.Value;
            sqlcom.Parameters.Add("@SMALLQUANTITY_UNITPRICE", SqlDbType.VarChar, 20).Value = Text12.Value;
            sqlcom.Parameters.Add("@PROJECT_COST", SqlDbType.VarChar, 20).Value = Text14.Value;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = TextBox1.Text;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@MUCH_PRICE", SqlDbType.VarChar, 20).Value = Text16.Value;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();

            IFExecution_SUCCESS = true;
        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
  
        }
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string vard1 = Text15.Value;
            String[] Carstr = new string[] { vard1 };
      
            Response.Redirect("../ReportManage/CRVPrintBill.aspx");
            //excelprint();
        }

        protected void btnToExcel_Click(object sender, ImageClickEventArgs e)
        {
          
        }
        private void toexel()
        {
            Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddHHssmm") + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView GridView1 = new GridView();
            basec.GenerateColumns(GridView1, dt).DataSource = dt;//因为要使用自定义标题列名称，所以要产生列名,使其有HeaderText属性
            basec.GenerateColumns(GridView1, dt).DataBind();
        
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

            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {//base.VerifyRenderingInServerForm(control);
        }

    }
}
