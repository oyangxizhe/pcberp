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


namespace WPSS.UserManage
{
    public partial class LIMITE_SEARCHT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        CLIMITE_SEARCH climite_search = new CLIMITE_SEARCH();
        protected string M_str_sql = @"
SELECT 
A.USID AS USID,
A.UNAME AS UNAME,
B.ENAME AS ENAME,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=C.MAKERID) AS MAKER,
C.DATE AS DATE 
FROM  USERINFO  A
LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID 
LEFT JOIN RIGHTLIST C ON A.USID=C.USID

";
        private static string _NUMID;
        public static string NUMID
        {
            set { _NUMID = value; }
            get { return _NUMID; }

        }

        private static string _ADD_OR_UPDATE;
        public static string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }

        }
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _USID;
        public string USID
        {
            set { _USID = value; }
            get { return _USID; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    Bind();
                }
                if (va.returnb() == true)
                    Response.Redirect("\\Default.aspx");
            }
            catch (Exception)
            {


            }
          
            
        }
        public  void Bind()
        {

            hint.Value = "";
            Text1.Value = NUMID;
          
            DataTable dtt = new DataTable();
            dtt.Columns.Add("选择", typeof(bool));
            dtt.Columns.Add("客户ID", typeof(string));
            dtt.Columns.Add("客户名称", typeof(string));
            dtt.Columns.Add("供应商ID", typeof(string));
            dtt.Columns.Add("供应商名称", typeof(string));
            DataTable dtx1=basec.getdts("SELECT * FROM CUSTOMERINFO_MST ");
            foreach (DataRow dr in dtx1.Rows )
            {
                DataRow dr1 = dtt.NewRow();
                dr1["选择"] = false;
                dr1["客户ID"] = dr["CUID"].ToString();
                dr1["客户名称"] = dr["CNAME"].ToString();
                dtt.Rows.Add(dr1);
            }

            dt = basec.getdts(climite_search.sqlth + " where B.UNAME='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {
                Label1.Text = dt.Rows[0]["UNAME"].ToString();
                foreach (DataRow dr in dt.Rows)
                {

                    foreach (DataRow dr1 in dtt.Rows)
                    {

                        if (dr["CUID_OR_SUID"].ToString() == dr1["客户ID"].ToString())
                        {
                            dr1["选择"] = true;
                            break;
                        }
                    }
                }
            }
            GridView1.DataSource = dtt;
            GridView1.DataBind();

            DataTable dty = new DataTable();
            dty.Columns.Add("选择", typeof(bool));
            dty.Columns.Add("供应商ID", typeof(string));
            dty.Columns.Add("供应商名称", typeof(string));
            DataTable dtx2= basec.getdts("SELECT * FROM SUPPLIERINFO_MST");
            foreach (DataRow dr in dtx2.Rows)
            {
                DataRow dr1 = dty.NewRow();
                dr1["选择"] = false;
                dr1["供应商ID"] = dr["SUID"].ToString();
                dr1["供应商名称"] = dr["SNAME"].ToString();
                dty.Rows.Add(dr1);
            }

            dt = basec.getdts(climite_search.sqlth + " where B.UNAME='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataRow dr1 in dty.Rows)
                    {
                        if (dr["CUID_OR_SUID"].ToString() == dr1["供应商ID"].ToString())
                        {
                            dr1["选择"] = true;
                            break;
                        }
                    }
                }
            }
            GridView2.DataSource = dty;
            GridView2.DataBind();
            NUMID = "";/*load after release value*/
        }
        protected void ClearText()
        {
            Text1.Value = "";
            Label1.Text = "";
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                Clear();
                Bind();
            }
            else if (submit.ID == "Submit2")
            {
                try
                {
                    save();
                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../UserManage/LIMITE_SEARCH.aspx" + n2);
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
        protected void Clear()
        {
           
            Text1.Value = "";
            Label1.Text = "";

    

        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {


        }
        #region save
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
            #region Save
            if (!juage1())
            {

            }
            else
            {
                USID =bc.getOnlyString("SELECT USID FROM USERINFO WHERE UNAME='" + Text1.Value + "'");
                basec.getcoms("DELETE LIMITE_SEARCH WHERE USID='"+USID+"'");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    {
                        string CUID = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                        SQlcommandE(climite_search .sqlo, CUID);
                    }

                }
                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    if (((CheckBox)GridView2.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    {
                        string SUID = ((TextBox)GridView2.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                        SQlcommandE(climite_search.sqlo, SUID);
                    }
                }
                NUMID = Text1.Value;/*set release value*/
                Bind();
            }
            #endregion
        }
        #endregion
        #region juage1()
        private bool juage1()
        {

            bool ju = true;
            if (Text1 .Value =="")
            {
                ju = false;
                hint.Value = "用户名不能为空！";

            }
            else if (!bc.exists("SELECT * FROM USERINFO WHERE UNAME='" + Text1.Value + "'"))
            {
                ju = false;
                hint.Value = "用户名在系统中不存在！";

            }
            return ju;
        }
        #endregion
        #region SQlcommandE
        protected void SQlcommandE(string sql,string CUID_OR_SUID)
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
            sqlcom.Parameters.Add("@USID", SqlDbType.VarChar, 20).Value = USID;
            sqlcom.Parameters.Add("@CUID_OR_SUID", SqlDbType.VarChar, 20).Value = CUID_OR_SUID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

        }


     
    }
}
