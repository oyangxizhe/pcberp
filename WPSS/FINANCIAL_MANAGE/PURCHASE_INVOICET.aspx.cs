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
    public partial class PURCHASE_INVOICET : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        basec bc = new basec();
        CPURCHASE cpurchase = new CPURCHASE();
        CRETURN creturn = new CRETURN();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();

        Validate va = new Validate();
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        int i;
        #region nature
        private string _PGKEY;
        public string PGKEY
        {
            set { _PGKEY = value; }
            get { return _PGKEY; }

        }
        private string _PRKEY;
        public string PRKEY
        {
            set { _PRKEY = value; }
            get {  return  _PRKEY; }

        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _PGID;
        public string PGID
        {
            set { _PGID = value; }
            get { return _PGID; }

        }
        private string _PUID;
        public string PUID
        {
            set { _PUID = value; }
            get { return _PUID; }

        }
        private string _REID;
        public string REID
        {
            set { _REID = value; }
            get { return _REID; }

        }
        private string _REKEY;
        public string REKEY
        {
            set { _REKEY = value; }
            get { return _REKEY; }

        }
        private string _RMID;
        public string RMID
        {
            set { _RMID = value; }
            get { return _RMID; }

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
        private string _SNAME;
        public string SNAME
        {
            set { _SNAME = value; }
            get { return _SNAME; }
        }
        private string _SUID;
        public string SUID
        {
            set { _SUID = value; }
            get { return _SUID; }
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
        public string PGREID { get; set; }
        PurchaseManage.PurchaseSearch psearch = new WPSS.PurchaseManage.PurchaseSearch();
        CPURCHASE_RECONCILE cr = new CPURCHASE_RECONCILE();
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
            Text3.Value = DateTime.Now.ToString("yyyy-MM-dd");
            Text4.Value = varMakerID;
            Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");

        }
        #region bind
        protected void bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = "成功产生应付款单" ;
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
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();

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
            DataTable dtt = crequest_money.DT_EMPTY();
            string s = "";
            for (int i = 0; i < GridView1.Rows.Count-1; i++)
            {

                CheckBox chb = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")); 
                PUID = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;/**/
                SN = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox2")).Text;/**/
                PGREID  = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox3")).Text;/**/
                INVOICE_NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox12")).Text;/**/
                INVOICE_NOTAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox13")).Text;/**/
                INVOICE_TAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox14")).Text;/**/
                INVOICE_HAVETAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox15")).Text;/**/
                SNAME = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox18")).Text;/**/
                string NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox202")).Text;/**/

                SUID = bc.getOnlyString("SELECT SUID FROM SUPPLIERINFO_MST WHERE SUID='" + SNAME + "'");
                if (chb.Checked)
                {

                    MAKERID = varMakerID;
                    PRKEY = NO;

                    SUID = bc.getOnlyString("SELECT SUID FROM SUPPLIERINFO_MST WHERE SUID='" + SNAME + "'");
                    dt = bc.GET_DT_TO_DV_TO_DT(dtt, "", "供应商名称='" + SNAME + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dtt.NewRow();
                        dr["供应商名称"] = SNAME;
                        dtt.Rows.Add(dr);
                        SQlcommandE_DET(crequest_money.sqlo);

                        if (PGREID.Substring(0, 2) == "PG")
                        {
                            basec.getcoms("UPDATE PURCHASEGODE_DET SET STATUS='INVOICE' WHERE PGKEY='" + PRKEY + "'");
                        }
                        else
                        {
                            basec.getcoms("UPDATE RETURN_DET SET STATUS='INVOICE' WHERE REKEY='" + PRKEY + "'");
                        }
                        IFExecution_SUCCESS = true;
                    }
                    else
                    {

                        DataRow dr = dtt.NewRow();
                        RMID = crequest_money.GETID();
                        dr["供应商名称"] = SNAME;
                        s = RMID;
                        dtt.Rows.Add(dr);
                        SQlcommandE_MST(crequest_money.sqlt);
                        SQlcommandE_DET(crequest_money.sqlo);
                        if (PGREID.Substring(0, 2) == "PG")
                        {
                            basec.getcoms("UPDATE PURCHASEGODE_DET SET STATUS='INVOICE' WHERE PGKEY='" + PRKEY + "'");
                        }
                        else
                        {
                            basec.getcoms("UPDATE RETURN_DET SET STATUS='INVOICE' WHERE REKEY='" + PRKEY + "'");
                        }
                        IFExecution_SUCCESS = true;
                    }
                }
            }
            bind();
        }
        #endregion
        private void UPDATE_PG_AND_RETURN_IF_HAVE_INVOICE()
        {
            SQlcommandE_DET(crequest_money.sqlo);
            basec.getcoms("UPDATE PURCHASE_DET SET IF_HAVE_INVOICE='Y' WHERE PUID='" + PUID + "' AND SN='" + SN + "'");
            basec.getcoms("UPDATE PURCHASEGODE_DET SET IF_HAVE_INVOICE='Y' WHERE PGKEY='" + PGKEY  + "'");
            DataTable dtx = bc.getdt(creturn.sql + " WHERE A.PUID='" + PUID + "' AND A.IF_HAVE_INVOICE='N'");
            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx.Rows)
                {
                    PRKEY = dr1["索引"].ToString();
                    SQlcommandE_DET(crequest_money.sqlo);
                    basec.getcoms("UPDATE RETURN_DET SET IF_HAVE_INVOICE='Y' WHERE REKEY='" + dr1["索引"].ToString() + "'");
                }

            }

        }
        

        #region juage_ABSTRACT_NOEMPTY()
        private bool  juage_ABSTRACT_NOEMPTY()
        {
            bool b = false;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            DataTable dtt = crequest_money.DT_EMPTY();
            for (int i = 0; i < GridView1.Rows.Count-1; i++)
            {
                CheckBox chb = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                INVOICE_NO = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox12")).Text;/**/
                INVOICE_NOTAX_AMOUNT =((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox13")).Text;/**/
                INVOICE_TAX_AMOUNT =((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox14")).Text;/**/
                INVOICE_HAVETAX_AMOUNT = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox15")).Text;/**/
                SNAME = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox18")).Text;/**/
              
                if (chb.Checked == false)
                {
                }
                else
                {
                    dt = bc.GET_DT_TO_DV_TO_DT(dtt, "", "供应商名称='" +SNAME  + "'");
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
                        else if (bc.exists("SELECT * FROM REQUEST_MONEY_MST WHERE INVOICE_NO='" + INVOICE_NO + "'"))
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
                        dr["供应商名称"] = SNAME;
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
            KEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM REQUEST_MONEY_DET", "RMKEY", "RM");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@RMKEY", SqlDbType.VarChar, 20).Value = KEY;
            sqlcom.Parameters.Add("@RMID", SqlDbType.VarChar, 20).Value = RMID;
            sqlcom.Parameters.Add("@PRKEY", SqlDbType.VarChar, 20).Value = PRKEY;
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
            sqlcom.Parameters.Add("@RMID", SqlDbType.VarChar, 20).Value = RMID;
            sqlcom.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar, 20).Value = INVOICE_NO;
            sqlcom.Parameters.Add("@INVOICE_NOTAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_NOTAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_TAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_TAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_HAVETAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_HAVETAX_AMOUNT;
            sqlcom.Parameters.Add("@REQUEST_MONEY_MAKERID", SqlDbType.VarChar, 20).Value = Text4.Value;
            sqlcom.Parameters.Add("@REQUEST_MONEY_DATE", SqlDbType.VarChar, 20).Value = Text3.Value;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@PGID", SqlDbType.VarChar, 20).Value = PGREID;
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
