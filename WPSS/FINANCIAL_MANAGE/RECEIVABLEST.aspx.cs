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

namespace WPSS.FINANCIAL_MANAGE
{
    public partial class RECEIVABLEST : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        basec bc = new basec();
       
        WPSS.Validate va = new Validate();
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
        decimal d1 = 0;
        decimal d2 = 0;
        decimal d3 = 0;
        decimal d4 = 0;
        CRECEIVABLES cRECEIVABLES = new CRECEIVABLES();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {

                Text1.Value = IDO;
                bind();
                
            }
            try
            {
              
            }
            catch (Exception)
            {


            }
            if (va.returnb() == true)
            Response.Redirect("\\Default.aspx");
        }
        #region bind
        protected void bind()
        {
            d1 = 0;
            d2 = 0;
            d3 = 0;
            d4 = 0;
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
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
            /*GridView1.DataSource = dtx();
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();*/
            dt = bc.getdt("exec ReturnReceivablesDetail '" + Text1.Value + "','',''");
            if (dt.Rows.Count > 0)
            {
                x.Value = "1";
                GridView2.DataSource = dt;
                GridView2.DataKeyNames = new string[] { "应收索引" };
                GridView2.DataBind();
                Text2.Value = dt.Rows[0]["客户代码"].ToString();
                Text3.Value = dt.Rows[0]["客户名称"].ToString();
                Text4.Value = dt.Rows[0]["应收日期"].ToString();
                Text5.Value = dt.Rows[0]["应收人工号"].ToString();
                Label1.Text = dt.Rows[0]["应收人"].ToString();
                Text6.Value = dt.Rows[0]["发票号码"].ToString();
                Text10.Value = dt.Rows[0]["发票未税金额"].ToString();
                Text11.Value = dt.Rows[0]["发票税额"].ToString();
                Text12.Value = dt.Rows[0]["发票含税金额"].ToString();
                Text15.Value = dt.Rows[0]["扣款项目"].ToString();
                Text16.Value = dt.Rows[0]["扣款金额"].ToString();

                Text13.Value = dt.Rows[0]["预收单号"].ToString();
                Text14.Value = dt.Rows[0]["预收金额"].ToString();
            }

            DataTable dtx4 = DT(dt);
            if (dtx4.Rows.Count > 0)
            {
                string v8 = dtx4.Compute("SUM(未税金额)", "").ToString();
                string v9 = dtx4.Compute("SUM(税额)", "").ToString();
                string v10 = dtx4.Compute("SUM(含税金额)", "").ToString();
                if (!string.IsNullOrEmpty(v9))
                {
                    Text7.Value = string.Format("{0:F2}", Convert.ToDouble(v8));
                    Text8.Value = string.Format("{0:F2}", Convert.ToDouble(v9));
                    Text9.Value = string.Format("{0:F2}", Convert.ToDouble(v10));
                    d1 = decimal.Parse(Text9.Value);
                }
                x.Value = Convert.ToString(1);
            }
            else
            {
                Text7.Value = "";
                Text8.Value = "";
                Text9.Value = "";

            }

            if (Text14.Value != "")
            {
                d2 = decimal.Parse(Text14.Value);
            }
            if (Text16.Value != "")
            {
                d3 = decimal.Parse(Text16.Value);
            }
             
          Text17.Value = (d1 - d2-d3).ToString("0.00");
        }
        #endregion
        public DataTable DT(DataTable dt)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("应收单号", typeof(string));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            foreach(DataRow dr in dt.Rows)
            {
                DataRow dr1 = dtt.NewRow();
                dr1["应收单号"] = dr["应收单号"];
                dr1["未税金额"] = dr["未税金额"];
                dr1["税额"] = dr["税额"];
                dr1["含税金额"] = dr["含税金额"];
                dtt.Rows.Add(dr1);
            }
            return dtt;
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
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {

            }
            else if (submit.ID == "Submit2")
            {
                add();
                if (IFExecution_SUCCESS == true)
                {
                    bind();
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
                Response.Redirect("../FINANCIAL_MANAGE/RECEIVABLES.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {

            }
            else if (submit.ID == "Submit5")
            {

            }
            else if (submit.ID == "Submit6")
            {

            }
        }
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
            Text11.Value = "";
            Label1.Text = "";
        }
        #region add
        protected void add()
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            hint.Value = "";
            d1 = decimal.Parse(Text9.Value);
            if (!string.IsNullOrEmpty(Text14.Value) && bc.yesno(Text14.Value) == 1)
            {
                d2 = decimal.Parse(Text14.Value );
            }
            if (!string.IsNullOrEmpty(Text16.Value) && bc.yesno(Text16.Value) == 1)
            {
                d3 = decimal.Parse(Text16.Value);
            }
            d4 = d1 - d2 - d3;
            if (bc.exists("SELECT * FROM RECEIVABLES_ORDER WHERE RCID='"+Text1.Value +"'"))
            {
                hint.Value = "此应收账款单已经存在收款单 不允许保存";
            }
            else if (Text13.Value !="" && !bc.exists("SELECT * FROM ADVANCE_RECEIVABLES  WHERE ARID='" + Text13.Value + "'"))
            {
                hint.Value = "预收款单号不存在于系统中！";

            }
            else if (Text13.Value != "" && bc.exists("SELECT * FROM RECEIVABLES_MST  WHERE RCID!='" + Text1.Value + "' AND ARID='" + Text13.Value + "'"))
            {
                hint.Value = "预收款单已在其它应收款单中存在";

            }
            else if (Text16.Value != ""  && bc.yesno (Text16.Value )==0)
            {
                hint.Value = "扣款金额不为空时需输入数字金额！";

            }
            else if (Text16.Value != "" && Text15.Value =="")
            {
                hint.Value = "扣款金额不为空时需输入扣款项目！";

            }
            else if (Text7.Value != Text10.Value)
            {
                hint.Value = "本应收账款单累计销货未税金额-本应收账款单累计销退未税金额="+Text7.Value +"需等于发票未税金额 "+Text10.Value ;
            }
            else if (Text8.Value != Text11.Value)
            {
                hint.Value = "本应收账款单累计销货税额-本应收账款单累计销退税额="+Text8.Value +"需等于发票税额 "+Text11.Value ;
            }
            else if (Text9.Value != Text12.Value)
            {
                hint.Value = "本应收账款单累计销货含税金额-本应收账款单累计销退含税金额="+Text9.Value +"需等于发票含税金额 "+Text12.Value ;
            }
            else if (d4< 0)
            {
                hint.Value = "实际应收账款金额=含税金额 "+d1+" - 预收款金额 "+d2 +" - 扣款金额 "+d3+" = "+d4+"不能小余0";
            }
          
            else
            {
             
                Text17.Value = d4.ToString("0.00");
                string v1 = bc.getOnlyString("SELECT ARID FROM RECEIVABLES_MST WHERE RCID='" + Text1.Value + "'");
              
                if (!string.IsNullOrEmpty(v1) && Text13.Value != v1)
                {
                    if (JUAGE_ARID_AND_INVOICE_IF_SAME_CUID (Text3.Value ,Text13.Value ))
                    {
                      

                    }
                   else  if (Text13.Value != "" && bc.exists("SELECT * FROM RECEIVABLES_MST  WHERE ARID='" + Text13.Value + "'"))
                    {
                        hint.Value = "预收款单号已被其它应收账款单使用！";

                    }
                    else
                    {
                       
                            basec.getcoms(@"UPDATE RECEIVABLES_MST SET ARID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                                    "',CUTPAYMENT_AMOUNT='"+Text16 .Value +"' WHERE RCID='" + Text1.Value + "'");
                        
                        basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='N' WHERE ARID='" + v1 + "'");
                        basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='Y' WHERE ARID='" + Text13.Value + "'");
                        IFExecution_SUCCESS = true;
                        bind();
                    }
                }
                else if (!string.IsNullOrEmpty(v1) && Text13.Value == v1)
                {
                    
                 
                    basec.getcoms(@"UPDATE RECEIVABLES_MST SET ARID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                                "',CUTPAYMENT_AMOUNT='"+Text16.Value +"' WHERE RCID='" + Text1.Value + "'");
                    basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='N' WHERE ARID='" + v1 + "'");
                    basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='Y' WHERE ARID='" + Text13.Value + "'");
                    IFExecution_SUCCESS = true;
                    bind();
                }
                else
                {

                    if (JUAGE_ARID_AND_INVOICE_IF_SAME_CUID(Text3.Value, Text13.Value))
                    {


                    } 
                    else  if (Text13 .Value !="" &&  bc.exists("SELECT * FROM RECEIVABLES_MST  WHERE ARID='" + Text13.Value + "'"))
                    {
                        hint.Value = "预收款单号已被其它应收账款单使用！";

                    }
                    else
                    {
                         basec.getcoms(@"UPDATE RECEIVABLES_MST SET ARID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                           "',CUTPAYMENT_AMOUNT='" + Text16.Value + "' WHERE RCID='" + Text1.Value + "'");
                        basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='Y' WHERE ARID='" + Text13.Value + "'");
                        IFExecution_SUCCESS = true;
                        bind();
                    }
                }


            }
       

        }
        #endregion


        private bool JUAGE_ARID_AND_INVOICE_IF_SAME_CUID(string INVOICE_CNAME, string ARID)
        {
            bool b = false;
            string s2 = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='"+INVOICE_CNAME +"'");
            string s3 = bc.getOnlyString("SELECT CUID FROM ADVANCE_RECEIVABLES WHERE ARID='" + ARID + "'");
            if (!string.IsNullOrEmpty(s3) && s3 != s2)
            {
                hint.Value = "收款单发票号码所属客户名称与预收款客户名称不一致";
                b = true;
            }
            return b;
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

          
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
           
            addNEW();
        }
        private void addNEW()
        {

        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

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



        }
        #endregion
        protected void btnReductionReconcile_Click(object sender, EventArgs e)
        {
            try
            {
                basec.getcoms("UPDATE RECEIVABLES_MST SET RECEIVABLESSTATUS_MST='CLOSE' WHERE PUID='" + Text1.Value + "'");
                bind();
            }
            catch (Exception)
            {

            }
        }

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string vard1 = Text1.Value;
            String[] Carstr = new string[] { vard1 };
        
            Response.Redirect("../ReportManage/CRVPrintBill.aspx");
            //excelprint();


        }

 
    }
}
