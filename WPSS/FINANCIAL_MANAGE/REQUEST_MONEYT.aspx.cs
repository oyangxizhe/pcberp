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
    public partial class REQUEST_MONEYT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        basec bc = new basec();
       
        WPSS.Validate va = new Validate();
        int i;


 
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
        string sql;
        decimal d1 = 0;
        decimal d2 = 0;
        decimal d3 = 0;
        decimal d4 = 0;
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();

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
            dt = bc.getdt("exec ReturnPaymentDetail '" + Text1.Value + "','',''");
            if (dt.Rows.Count > 0)
            {
                x.Value = "1";
                GridView2.DataSource = dt;
                GridView2.DataKeyNames = new string[] { "应付款索引" };
                GridView2.DataBind();
                Text2.Value = dt.Rows[0]["供应商代码"].ToString();
                Text3.Value = dt.Rows[0]["供应商"].ToString();
                Text4.Value = dt.Rows[0]["应付款日期"].ToString();
                Text5.Value = dt.Rows[0]["应付款人工号"].ToString();
                Label1.Text = dt.Rows[0]["应付款人"].ToString();
                Text6.Value = dt.Rows[0]["发票号码"].ToString();
                Text10.Value = dt.Rows[0]["发票未税金额"].ToString();
                Text11.Value = dt.Rows[0]["发票税额"].ToString();
                Text12.Value = dt.Rows[0]["发票含税金额"].ToString();
                Text15.Value = dt.Rows[0]["扣款项目"].ToString();
                Text16.Value = dt.Rows[0]["扣款金额"].ToString();

                Text13.Value = dt.Rows[0]["预付单号"].ToString();
                Text14.Value = dt.Rows[0]["预付金额"].ToString();
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
            if (!string.IsNullOrEmpty (Text14.Value))
            {
                d2 = decimal.Parse(Text14.Value);
            }
            if (!string.IsNullOrEmpty(Text16.Value))
            {
                d3 = decimal.Parse(Text16.Value);
            }

        
          Text17.Value = (d1 - d2-d3).ToString("0.00");
        }
        #endregion
        public DataTable DT(DataTable dt)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("应付单号", typeof(string));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            foreach (DataRow dr in dt.Rows)
            {
                DataRow dr1 = dtt.NewRow();
                dr1["应付单号"] = dr["应付单号"];
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
        #region dtx
        protected DataTable dtx()
        {
            CIRCULATION_COUNT = 4;
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
            dt4.Columns.Add("备注", typeof(string));
            string v11 = bc.getOnlyString("SELECT SOURCESTATUS FROM REQUEST_MONEY_MST WHERE PUID='" + Text1.Value + "'");
            if (!string.IsNullOrEmpty(v11))
            {

                dt4 = basec.getdts(@"SELECT A.SN AS 项次,A.WAREID AS 品号,B.WNAME AS 品名,B.CO_WAREID AS 料号,B.CWAREID AS 客户料号,B.SPEC AS 规格,B.UNIT AS 单位,A.PCOUNT AS 采购数量,
A.TAXRATE AS 税率,A.NEEDDATE AS 需求日期,A.REMARK AS 备注 FROM REQUEST_MONEY_DET A LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID 
WHERE A.PUID='" + Text1.Value + "'");
                if (dt4.Rows.Count > 0)
                {
                    CIRCULATION_COUNT = dt4.Rows.Count;
                }

            }
            else
            {
                for (i = 1; i <=CIRCULATION_COUNT ; i++)
                {
                    DataRow dr = dt4.NewRow();
                    dr["项次"] = i;
                    dr["税率"] = 17;
                    dr["需求日期"] = DateTime.Now.ToString("yyy-MM-dd");
                    dr["备注"] = "如有工程费用均分摊在单价中";
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
                d2 = decimal.Parse(Text14.Value);
            }
            if (!string.IsNullOrEmpty(Text16.Value) && bc.yesno(Text16.Value) == 1)
            {
                d3 = decimal.Parse(Text16.Value);
            }
            d4 = d1 - d2 - d3;
            if (bc.exists("SELECT * FROM PAYMENT_ORDER WHERE RMID='"+Text1.Value +"'"))
            {
                hint.Value = "此应付款单已经存在付款单 不允许保存";
            }
            else if (Text13.Value !="" && !bc.exists("SELECT * FROM ADVANCE_PAYMENT  WHERE APID='" + Text13.Value + "'"))
            {
                hint.Value = "预付款单号不存在于系统中！";

            }
            else if (Text13.Value !="" && bc.exists("SELECT * FROM REQUEST_MONEY_MST  WHERE RMID!='" + Text1.Value + "' AND APID='"+ Text13.Value +"'"))
            {
                hint.Value = "预付款单已在其它应付款单中存在";

            }
            else if (Text16.Value != "" && bc.yesno (Text16.Value )==0)
            {
                hint.Value = "扣款金额不为空时需输入数字金额！";

            }
            else if (Text16.Value != "" && Text15.Value =="")
            {
                hint.Value = "扣款金额不为空时需输入扣款项目！";

            }
            else if (Text7.Value != Text10.Value)
            {
                hint.Value = "本应付款单累计入库未税金额-本应付款单累计退货未税金额="+Text7.Value +"需等于发票未税金额 "+Text10.Value ;
            }
            else if (Text8.Value != Text11.Value)
            {
                hint.Value = "本应付款单累计入库税额-本应付款单累计退货税额="+Text8.Value +"需等于发票税额 "+Text11.Value ;
            }
            else if (Text9.Value != Text12.Value)
            {
                hint.Value = "本应付款单累计入库含税金额-本应付款单累计退货含税金额="+Text9.Value +"需等于发票含税金额 "+Text12.Value ;
            }
            else if (d4< 0)
            {
                hint.Value = "实际应付款金额=含税金额 "+d1+" - 预付款金额 "+d2 +" - 扣款金额 "+d3+" = "+d4+"不能小余0";
            }
          
            else
            {
              
           
                Text17.Value = d4.ToString("0.00");
                string v1 = bc.getOnlyString("SELECT APID FROM REQUEST_MONEY_MST WHERE RMID='" + Text1.Value + "'");
                if (!string.IsNullOrEmpty(v1) && Text13.Value != v1)
                {
                    if (JUAGE_APID_AND_INVOICE_IF_SAME_SUID(Text3.Value, Text13.Value))
                    {

                    }
                    else if (Text13.Value !="" && bc.exists("SELECT * FROM REQUEST_MONEY_MST  WHERE APID  IN ('" + Text13.Value + "')"))
                    {
                        hint.Value = "预付款单号已被其它应付款单使用！";

                    }
                    else
                    {
                        basec.getcoms(@"UPDATE REQUEST_MONEY_MST SET APID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                            "',CUTPAYMENT_AMOUNT='"+Text16.Value  + "' WHERE RMID='" + Text1.Value + "'");
                        basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='N' WHERE APID='" + v1 + "'");
                        basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='Y' WHERE APID='" + Text13.Value + "'");
                        IFExecution_SUCCESS = true;
                        bind();
                    }
                }
                else if (!string.IsNullOrEmpty(v1) && Text13.Value == v1)
                {
                    basec.getcoms(@"UPDATE REQUEST_MONEY_MST SET APID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                          "',CUTPAYMENT_AMOUNT='" + Text16.Value + "' WHERE RMID='" + Text1.Value + "'");
                    basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='N' WHERE APID='" + v1 + "'");
                    basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='Y' WHERE APID='" + Text13.Value + "'");
                    IFExecution_SUCCESS = true;
                    bind();
                }
                else
                {
                    if (JUAGE_APID_AND_INVOICE_IF_SAME_SUID(Text3.Value, Text13.Value))
                    {

                    }
                    else if (Text13.Value !="" && bc.exists("SELECT * FROM REQUEST_MONEY_MST  WHERE APID  IN ('" + Text13.Value + "')"))
                    {
                        hint.Value = "预付款单号已被其它应付款单使用！";

                    }
                    else
                    {

                        basec.getcoms(@"UPDATE REQUEST_MONEY_MST SET APID='" + Text13.Value + "',CUTPAYMENT_PROJECT='" + Text15.Value +
                             "',CUTPAYMENT_AMOUNT='" + Text16.Value + "' WHERE RMID='" + Text1.Value + "'");
                        basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='Y' WHERE APID='" + Text13.Value + "'");
                        IFExecution_SUCCESS = true;
                        bind();
                    }
                }


            }
       

        }
        #endregion

        private bool JUAGE_APID_AND_INVOICE_IF_SAME_SUID(string INVOICE_SNAME, string APID)
        {
            bool b = false;
            string s2 = bc.getOnlyString("SELECT SUID FROM SUPPLIERINFO_MST WHERE SNAME='" + INVOICE_SNAME + "'");
            string s3 = bc.getOnlyString("SELECT SUID FROM ADVANCE_PAYMENT WHERE APID='" + APID + "'");
            if (!string.IsNullOrEmpty(s3) && s3 != s2)
            {
                hint.Value = "应付款单发票号码所属供应商名称与预付款供应商名称不一致";
                b = true;
            }
            return b;
        }
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
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='录入采购单'");
            try
            {
                string[] str = new string[] { "" };
                string sql1;
                hint.Value = "";
                string id = GridView2.DataKeys[e.RowIndex][0].ToString();
                str[0] = id;
                sql1 = "DELETE FROM REQUEST_MONEY_DET WHERE PUKEY='" + id + "'";
                if (bc.exists("select * from REQUEST_MONEYgode_det where PUID='" + Text1.Value + "'"))
                {
                    hint.Value = "该采购单已经在采购入库单中存在不允许删除！";
                }
                else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }
                else if (bc.juageOne("SELECT * FROM REQUEST_MONEY_DET WHERE PUID='" + Text1.Value + "'"))
                {

                    basec.getcoms(sql1);
                    sql = "DELETE REQUEST_MONEY_MST WHERE PUID='" + Text1.Value + "'";
                    basec.getcoms(sql);
                    GridView2.EditIndex = -1;
                    bind();

                }
                else
                {

                    basec.getcoms(sql1);
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
                Response.Redirect("../FINANCIAL_MANAGE/REQUEST_MONEY.aspx" + n2);
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
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
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
                basec.getcoms("UPDATE REQUEST_MONEY_MST SET REQUEST_MONEYSTATUS_MST='CLOSE' WHERE PUID='" + Text1.Value + "'");
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
