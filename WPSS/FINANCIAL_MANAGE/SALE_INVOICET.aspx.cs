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
    public partial class SALE_INVOICET : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        basec bc = new basec();
        CSELLRETURN csellreturn = new CSELLRETURN();
        CRECEIVABLES creceivables = new CRECEIVABLES();
        CSALE_INVOICE csale_invoicet = new CSALE_INVOICE();
        CORDER_RECONCILE cr = new CORDER_RECONCILE();
        Validate va = new Validate();
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        int i;
        #region nature
        private string _SEKEY;
        public string SEKEY
        {
            set { _SEKEY = value; }
            get { return _SEKEY; }

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
            get {  return  _SSKEY; }

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
        private string _REID;
        public string REID
        {
            set { _REID = value; }
            get { return _REID; }

        }
        private string _SRKEY;
        public string SRKEY
        {
            set { _SRKEY = value; }
            get { return _SRKEY; }

        }
        private string _RCID;
        public string RCID
        {
            set { _RCID = value; }
            get { return _RCID; }

        }
        private string _SKU;
        public string SKU
        {
            set { _SKU = value; }
            get { return _SKU; }

        }
  
        private decimal _TAX_RATE;
        public decimal TAX_RATE
        {
            set { _TAX_RATE = value; }
            get { return _TAX_RATE; }

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
        private  int  _CIRCULATION_COUNT;
        public  int  CIRCULATION_COUNT
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
        private string _INVOICE_NOTAX_AMOUNT;
        public string INVOICE_NOTAX_AMOUNT
        {
            set { _INVOICE_NOTAX_AMOUNT = value; }
            get { return _INVOICE_NOTAX_AMOUNT; }

        }
        private string _INVOICE_TAX_AMOUNT;
        public string INVOICE_TAX_AMOUNT
        {
            set { _INVOICE_TAX_AMOUNT = value; }
            get { return _INVOICE_TAX_AMOUNT; }

        }
        private string _INVOICE_HAVETAX_AMOUNT;
        public string INVOICE_HAVETAX_AMOUNT
        {
            set { _INVOICE_HAVETAX_AMOUNT = value; }
            get { return _INVOICE_HAVETAX_AMOUNT; }

        }
        
        private string _INVOICE_NO;
        public string INVOICE_NO
        {
            set { _INVOICE_NO = value; }
            get { return _INVOICE_NO; }

        }
        #endregion
        string  KEY;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckBox2.AutoPostBack = true;
                CheckBox3.AutoPostBack = true;
                currentdate();
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
        private bool juageo()
        {
            bool b = false;
      
            return b;
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
        #region bind
        protected void bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = "成功产生应收账款作业" ;
            }
            else
            {
                hint.Value = "";
            }
            x.Value = "";
            x1.Value = "";
            RDID.Value = "";
            COKEY.Value = "";
            GridView1.DataSource = cr.RETURN_PG_AND_RETURN_DT();
 
            GridView1.DataBind();
            //Response.Write(cr.MESSAGE);

            if (cr.RETURN_PG_AND_RETURN_DT().Rows.Count > 0)
            {
                x.Value = "1";

            }
        
        }
        #endregion
        #region JUAGE_IFEXISTS_SELECT()
        private bool JUAGE_IFEXISTS_SELECT()
        {
            bool b = false;
            for (int k = 0; k <GridView1 .Rows .Count-1 ; k++)
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

        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../main.aspx" + n2);
        }
        #region btnReconcile_Click
        protected void btnReconcile_Click(object sender, EventArgs e)
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
        #endregion
        private bool juage()
        {
            bool b = false;
             if (Text4.Value == "")
            {
                hint.Value = "工号不能为空！";
                b = true;

            }
            else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "'"))
            {
                hint.Value = "员工工号不存在于系统中！";
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
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace ("/","-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            DataTable dtt = creceivables.DT_EMPTY();
            DataTable dt1 = cr.DT_EMPTY();
            string s = "";
            for (int i = 0; i < GridView1.Rows.Count-1; i++)
            {
              
                CheckBox chb = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                ORID = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;/**/
                SN = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox2")).Text;/**/
                SESRID  = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox3")).Text;/**/
                INVOICE_NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox12")).Text;/**/
                INVOICE_NOTAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox13")).Text;/**/
                INVOICE_TAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox14")).Text;/**/
                INVOICE_HAVETAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox15")).Text;/**/
                CNAME = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox18")).Text;/**/
                string NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox202")).Text;/**/
                MAKERID = varMakerID;
                CUID = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CUID='" + CNAME + "'");
                if (chb.Checked)
                {
                   
                    MAKERID = varMakerID;
                    SSKEY = NO;

                    CUID = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CUID='" + CNAME + "'");
                    dt = bc.GET_DT_TO_DV_TO_DT(dtt, "", "客户名称='" + CNAME + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dtt.NewRow();
                        dr["客户名称"] = CNAME;
                        dtt.Rows.Add(dr);
                        SQlcommandE_DET(creceivables.sqlo);

                        if (SESRID.Substring(0, 2) == "SE")
                        {
                            basec.getcoms("UPDATE SELLTABLE_DET SET STATUS='INVOICE' WHERE SEKEY='" + SSKEY  + "'");
                        }
                        else
                        {
                            basec.getcoms("UPDATE SELLRETURN_DET SET STATUS='INVOICE' WHERE SRKEY='" + SSKEY  + "'");
                        }
                        IFExecution_SUCCESS = true;
                    }
                    else
                    {

                        DataRow dr = dtt.NewRow();
                        RCID = creceivables.GETID();
                        dr["客户名称"] = CNAME;
                        s = RCID;
                        dtt.Rows.Add(dr);
                        SQlcommandE_MST(creceivables.sqlt);
                        SQlcommandE_DET(creceivables.sqlo);
                        if (SESRID.Substring(0, 2) == "SE")
                        {
                            basec.getcoms("UPDATE SELLTABLE_DET SET STATUS='INVOICE' WHERE SEKEY='" + SSKEY  + "'");
                        }
                        else
                        {
                            basec.getcoms("UPDATE SELLRETURN_DET SET STATUS='INVOICE' WHERE SRKEY='" + SSKEY  + "'");
                        }
                        IFExecution_SUCCESS = true;
                    }
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='INVOICE' WHERE ORID='" + ORID + "'");//更新订单状态为已经产生发票
                }
            }
            bind();
        }
        #endregion


        #region juage_ABSTRACT_NOEMPTY()
        private bool  juage_ABSTRACT_NOEMPTY()
        {
            bool b = false;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            DataTable dtt = creceivables.DT_EMPTY();
            for (int i = 0; i < GridView1.Rows.Count-1; i++)/*最后一行为金额TOTAL不纳入计算*/
            {
                CheckBox chb = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                INVOICE_NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox12")).Text;/**/
                INVOICE_NOTAX_AMOUNT =((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox13")).Text;/**/
                INVOICE_TAX_AMOUNT =((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox14")).Text;/**/
                INVOICE_HAVETAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox15")).Text;/**/
                CNAME = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox18")).Text;/**/
              
                if (chb.Checked == false)
                {
                }
                else
                {
                    dt = bc.GET_DT_TO_DV_TO_DT(dtt, "", "客户名称='" +CNAME  + "'");
                    if (dt.Rows.Count > 0)
                    {
                      
                    }
                    else
                    {

                        if(INVOICE_NO =="")
                        {
                            hint.Value = "项次"+Convert .ToString(i+1)+"发票号码不能为空";
                            b = true;
                            break;
                        }
                        else if (bc.exists("SELECT * FROM RECEIVABLES_MST WHERE INVOICE_NO='" + INVOICE_NO + "'"))
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "发票号码已经存在";
                            b = true;
                            break;
                        }
                        else if(INVOICE_NOTAX_AMOUNT =="")
                        {
                            hint.Value = "项次"+Convert .ToString(i+1)+"发票未税金额不能为空";
                            b = true;
                            break;
                        }
                        else if (bc.yesno(INVOICE_NOTAX_AMOUNT ) == 0)
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "金额只能输入数字";
                            b = true;
                            break;

                        }
                        else if(INVOICE_TAX_AMOUNT =="")
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "发票税额不能为空";
                            b = true;
                            break;
                        }
                        else if (bc.yesno(INVOICE_TAX_AMOUNT ) == 0)
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "金额只能输入数字";
                            b = true;
                            break;

                        }
                        else if (INVOICE_HAVETAX_AMOUNT == "")
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "发票含税金额不能为空";
                            b = true;
                            break;
                        }
                        else if (bc.yesno(INVOICE_HAVETAX_AMOUNT) == 0)
                        {
                            hint.Value = "项次" + Convert.ToString(i + 1) + "金额只能输入数字";
                            b = true;
                            break;

                        }
                        else 
                        {
                        DataRow dr = dtt.NewRow();
                        dr["客户名称"] = CNAME;
                        dtt.Rows.Add(dr);
                        }
                     

                    }

                }
            }
       
            return b;

        }
        #endregion
        #region  SQlcommandE_DET
        protected void SQlcommandE_DET(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace ("/","-");
            KEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM RECEIVABLES_DET", "RCKEY", "RC");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@RCKEY", SqlDbType.VarChar, 20).Value = KEY;
            sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = RCID;
            sqlcom.Parameters.Add("@SSKEY", SqlDbType.VarChar, 20).Value = SSKEY;
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
            sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = RCID;
            sqlcom.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar, 20).Value = INVOICE_NO;
            sqlcom.Parameters.Add("@INVOICE_NOTAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_NOTAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_TAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_TAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_HAVETAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_HAVETAX_AMOUNT;
            sqlcom.Parameters.Add("@RECEIVABLES_MAKERID", SqlDbType.VarChar, 20).Value = Text4.Value;
            sqlcom.Parameters.Add("@RECEIVABLES_DATE", SqlDbType.VarChar, 20).Value = Text3.Value;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = ORID;
            sqlcom.Parameters.Add("@SEID", SqlDbType.VarChar, 20).Value = SESRID;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region CheckBox2_CheckedChanged
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
        #endregion
        #region checkbox3
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
        #endregion
    }
}
