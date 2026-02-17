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
    public partial class RECEIVABLES_ORDERT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        private string _ROKEY;
        public string ROKEY
        {
            set { _ROKEY = value; }
            get { return _ROKEY; }

        }
        private string _GEKEY;
        public string GEKEY
        {
            set { _GEKEY = value; }
            get { return _GEKEY; }

        }
        private string _MRKEY;
        public string MRKEY
        {
            set { _MRKEY = value; }
            get { return _MRKEY; }

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
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }

        WPSS.Validate va = new Validate();
        CRECEIVABLES creceivables = new CRECEIVABLES();
        CRECEIVABLES_ORDER CRECEIVABLES_ORDER = new CRECEIVABLES_ORDER();
    
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Text1.Value = IDO;
                Bind();
            }

            if (va.returnb() == true)
                Response.Redirect("\\Default.aspx");
        }
        protected void Bind()
        {

            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }

            dt = bc.getdt("exec ReturnReceivablesOrder '" + Text1.Value + "','','',''");
            if (dt.Rows.Count > 0)
            {
                Text3.Value = dt.Rows[0]["客户名称"].ToString();
                Text4.Value = dt.Rows[0]["发票号码"].ToString();
              

                Text2.Value = dt.Rows[0]["应收单号"].ToString();
                Text13.Value = dt.Rows[0]["收款金额"].ToString();
                Text14.Value = dt.Rows[0]["经手人工号"].ToString();
                Text15.Value = dt.Rows[0]["收款日期"].ToString();
                Label1.Text = dt.Rows[0]["经手人"].ToString();
                TextBox1.Text = dt.Rows[0]["备注"].ToString();
            }
            else
            {

                currentdate();
            }

        }
    
        protected void currentdate()
        {

            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            Text14.Value = varMakerID;
            Text15.Value =DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
            Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");
  

        }
        protected void ClearText()
        {
            Text1.Value = "";
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
      
            Text13.Value = "";
            TextBox1.Text = "";
            Label1.Text = "";
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                add();
            }
            else if (submit.ID == "Submit2")
            {
                save();
                if (IFExecution_SUCCESS == true)
                {
                    Bind();
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
                Response.Redirect("../financial_manage/RECEIVABLES_ORDER.aspx" + n2);
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
        protected void add()
        {

      
            ClearText();
            Text1.Value = CRECEIVABLES_ORDER.GETID();
            currentdate();
            Bind();
            ADD_OR_UPDATE = "ADD";

        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {



        }
        protected void save()
        {
            hint.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            
            ROKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM RECEIVABLES_ORDER", "ROKEY", "RO");
            string v1 = bc.getOnlyString("SELECT RECEIVABLES_ORDER_AMOUNT FROM RECEIVABLES_ORDER WHERE ROID='"+Text1.Value +"'");
            if (juage1())
            {

            }
            else if (ROKEY == "Exceed Limited")
            {
                hint.Value = "编码超出限制！";

            }
            else
            {
                GEKEY = ROKEY;
                if (!bc.exists("SELECT * FROM RECEIVABLES_ORDER WHERE ROID='" + Text1.Value + "'"))
                {
                 
                        SQlcommandE(CRECEIVABLES_ORDER.sqlo);
                        //SQlcommandE_GODE(CRECEIVABLES_ORDER.sqlt);
                        IFExecution_SUCCESS = true;
                    
                }
                else
                {

                    SQlcommandE(CRECEIVABLES_ORDER.sqlf + " WHERE ROID='" + Text1.Value + "'");
                    IFExecution_SUCCESS = true;
                    //SQlcommandE_GODE(CRECEIVABLES_ORDER.sqlfi + " WHERE GODEID='" + Text1.Value + "'");

                }
               
            }

        }
        #region juage1()
        private bool juage1()
        {
            decimal d1 = 0;
            DataTable dtx1 = bc.getdt(@"
SELECT 
RCID AS RCID,
SUM(RECEIVABLES_ORDER_AMOUNT) AS RECEIVABLES_ORDER_AMOUNT 
FROM RECEIVABLES_ORDER 
WHERE
RCID='" + Text2.Value  + "' AND ROID!='"+Text1.Value +"' GROUP BY RCID");
            if (dtx1.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtx1.Rows[0]["RECEIVABLES_ORDER_AMOUNT"].ToString()))
                {
                   d1=decimal.Parse(dtx1.Rows[0]["RECEIVABLES_ORDER_AMOUNT"].ToString());
           
                }

            }
    
    
            
            bool b = false;
            if (Text2.Value =="")
            {
                b = true;
                hint.Value = "销货单号不能为空！";
            }
            else if (!bc.exists("SELECT * FROM SELLTABLE_MST WHERE SEID='" + Text2.Value + "'"))
            {
                hint.Value = "销货单号不存在于系统中！";
                b = true;
            }
      
            else  if (Text13.Value =="")
            {
                b = true;
                hint.Value = "收款金额不能为空！";

            }
            else  if (bc.yesno(Text13.Value) == 0)
            {
                b = true;
                hint.Value = "收款金额只能输入数字！";

            }
  
            else if (Text14.Value == "")
            {
                hint.Value = "工号不能为空！";
                b = true;
            }
            else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text14.Value + "'"))
            {
                hint.Value = "员工工号不存在于系统中！";
                b = true;
            }
            return b;
        }
        #endregion

        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

        }
        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@ROKEY", SqlDbType.VarChar, 20).Value = ROKEY;
            sqlcom.Parameters.Add("@ROID", SqlDbType.VarChar, 20).Value = Text1.Value;
            sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = Text2.Value;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_MAKERID", SqlDbType.VarChar, 20).Value = Text14.Value;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_DATE", SqlDbType.VarChar, 20).Value = Text15.Value;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_AMOUNT", SqlDbType.VarChar, 20).Value = Text13.Value;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = TextBox1.Text;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
  
    
    }
}


