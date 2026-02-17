using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using XizheC;

namespace WPSS
{
    public partial class Main : System.Web.UI.Page
    {
        WPSS.Validate va = new Validate();
        string UNAME = WPSS._Default.UNAME ;
        string ENAME = WPSS._Default.ENAME ;
        string USID = WPSS._Default.USID;
        basec bc = new basec();
        public static string[] v1 = new string[] { "" };
        public static string[] v2 = new string[] { "" };
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Xizhe ERP";
            if (bc.getOnlyString("SELECT  [COName] FROM [CompanyInfo_MST]").IndexOf("宜优捷") == -1)//当前三家客户只有宜优捷有公司LOGO
            {
                p1.Visible = false;
            }
            else
            {
                p1.Visible = true;
            }
            if (va.returnb() == true)
            {
                Response.Redirect("Default.aspx");
            }
            else if (Session["USID"].ToString() != Request.QueryString["USID"])
            {
                Response.Redirect("Default.aspx");
            }
            else
            {

                v1[0] = Request.QueryString["USID"];
              
                L3.Text = DateTime.Now.ToShortDateString().Replace("/", "-");
                Label1.Text = bc.getOnlyString("SELECT  [COName] FROM [CompanyInfo_MST]") + "ERP网络平台";

                //DataSet ds = bc.getds("select * from RightList where USID='" + v1[0] + "' order by NodeID ASC ", "RightList");
                //this.ViewState["ds"] = ds;

                usido.Value = USID;

               
                Label1.ForeColor = Color.FromName("#4D784D");
              
                L3.ForeColor = Color.FromName("#4D784D");

                DataTable dtx1 = bc.getdt("SELECT * FROM USERINFO a left join EmployeeInfo b on a.EMID =b.EMID  WHERE USID='" + v1[0] + "'");
                if (dtx1.Rows.Count > 0)
                {
                    Label2.Text = "帐号：" + dtx1.Rows[0]["uname"].ToString();
                    Label3.Text = "职位：" + dtx1.Rows[0]["position"].ToString();
                }



                DataTable dt = bc.getdt("select * from rightlist where PARENT_NODEID=0 and USID='" + v1[0] + "' ");
                DataTable dtx = new DataTable();
                dtx.Columns.Add("NODEID", typeof(string));
                dtx.Columns.Add("IMAGE_URL", typeof(string));
                dtx.Columns.Add("NODE_NAME", typeof(string));
                dtx.Columns.Add("TEMP", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dtx.NewRow();
                    dr1["NODEID"] = dr["NODEID"].ToString();
                    dr1["IMAGE_URL"] = dr["IMAGE_URL"].ToString();
                    dr1["NODE_NAME"] = dr["NODE_NAME"].ToString();
                    dr1["TEMP"] = dr["NODEID"].ToString() + "1";
                    dtx.Rows.Add(dr1);
                }
                DataList1.DataSource = dtx;
                DataList1.DataBind();

            }
        }
  
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bc.getcom(@"UPDATE AUTHORIZATION_USER SET STATUS='N' ,LEAVE_DATE='" + varDate + "'WHERE AUID='" + Session["AUID"].ToString() + "'");

            Session["UName"] = null;
            Session["USID"] = null;
            Session["Pwd"] = null;
            ClearClientPageCache();
           Response.Redirect("Default.aspx"); 

        }
        public void ClearClientPageCache() 
        { 
        //清除浏览器缓存 
       HttpCookie cookie = new HttpCookie("cookiename");
       cookie.Expires = DateTime.Now.AddDays(-365);
       Response.Cookies.Add(cookie);
　　   Response.Buffer = true; 
       Response.ExpiresAbsolute = DateTime.Now.AddDays(-1); 
       Response.Cache.SetExpires(DateTime.Now.AddDays(-1)); 
       Response.Expires = 0; 
       Response.CacheControl = "no-cache"; 
       Response.Cache.SetNoStore(); 
}

    
    }
}
