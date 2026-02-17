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
    public partial class XSite : System.Web.UI.MasterPage
    {
        basec bc = new basec();
        public string[] v1 = new string[] { "" };
        public string[] v2 = new string[] { "" };
        public string USID { set; get; }
        DataTable dt = new DataTable();
        DataTable dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
 
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() !=null)
                {
                    
                    
                    if (!IsPostBack)
                    {
                        /*  L1.Text = bc.getOnlyString("SELECT B.ENAME FROM USERINFO A LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID WHERE USID='" + v1[0] + "'");
                       L2.Text = bc.getOnlyString("SELECT B.DEPART FROM USERINFO A LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID WHERE USID='" + v1[0] + "'");
                       L3.Text = DateTime.Now.ToShortDateString().Replace("/", "-");
                       Label1.Text = bc.getOnlyString("SELECT [COName] FROM [CompanyInfo_MST]") + "ERP系统";
                       //DataSet ds = bc.getds("select * from RightList where USID='" + v1[0] + "' order by NodeID ASC ", "RightList");
                       //this.ViewState["ds"] = ds;*/

                        usido.Value = Request.Cookies["cookiename"].Values["usid"].ToString();
                        DataTable dtx1 = bc.getdt("SELECT * FROM USERINFO WHERE USID='" + usido.Value + "'");
                        if (dtx1.Rows.Count > 0)
                        {
                            Label1.Text = "XizheMS 欢迎您，" + dtx1.Rows[0]["uname"].ToString() + " ";
                            //Label3.Text = "职位：" + dtx1.Rows[0]["position"].ToString();
                        }
                        DataTable dt = bc.getdt("select * from rightlist where PARENT_NODEID=0 and USID='" + usido.Value + "' order by nodeid asc ");
                        DataTable dtx = new DataTable();
                        dtx.Columns.Add("NODEID", typeof(string));
                        dtx.Columns.Add("IMAGE_URL", typeof(string));
                        dtx.Columns.Add("NODE_NAME", typeof(string));
                        dtx.Columns.Add("TEMP", typeof(string));
                        dtx.Columns.Add("RIGHT_NODEID", typeof(string));
                        dtx.Columns.Add("RIGHT_URL", typeof(string));
                        foreach (DataRow dr in dt.Rows)
                        {
                            DataRow dr1 = dtx.NewRow();
                            dr1["NODEID"] = dr["NODEID"].ToString();
                            dr1["IMAGE_URL"] = dr["IMAGE_URL"].ToString();
                            dr1["NODE_NAME"] = dr["NODE_NAME"].ToString();
                            dr1["TEMP"] = dr["NODEID"].ToString();

                            if (dr["NODEID"].ToString() == "1")//首页不加载子结点
                            {
                                dr1["RIGHT_NODEID"] = 0;
                                dr1["RIGHT_URL"] = dr["url"].ToString();
                            }
                            else
                            {
                                dt1 = bc.getdt("SELECT TOP 1 * FROM RIGHTLIST WHERE USID='" + Request.Cookies["cookiename"].Values["usid"].ToString() +
                    "'AND PARENT_NODEID='" + dr["NODEID"].ToString() + "'ORDER BY NODEID ASC");
                                if (dt1.Rows.Count > 0)//取出该父节点下该用户有权限的子节点用于在点父节点时默认加载一支作业显示
                                {
                                    dr1["RIGHT_NODEID"] = dt1.Rows[0]["nodeid"].ToString();
                                    dr1["RIGHT_URL"] = dt1.Rows[0]["url"].ToString();

                                }
                            }
                            dtx.Rows.Add(dr1);
                        }
                        DataList1.DataSource = dtx;
                        DataList1.DataBind();
                    }
                }
                Label2.Text = "Copyright©2009-"+DateTime.Now.Year+" Suzhou Usable Software Co., Ltd. All rights reserved.";
            }
            catch (Exception)
            {
              
                Response.Redirect("/default.aspx");
            }
       
     
            try
            {
                if (Request.QueryString["parent_nodeid"]!=null )
                {
                    parent_nodeid.Value = Request.QueryString["parent_nodeid"];
                }
                if (Request.QueryString["nodeid"]!=null )
                {
                    nodeid.Value = Request.QueryString["nodeid"];
                }
                Bind();
            }
            catch (Exception)
            {

            }

        }
        protected void Bind()
        {
            if (Request.QueryString["PARENT_NODEID"] != null)
            {
                dt = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + Request.Cookies["cookiename"].Values["usid"].ToString() +
                       "'AND PARENT_NODEID='" + Request.QueryString["PARENT_NODEID"].ToString() + "'ORDER BY NODEID ASC");  
            }
            else
            {
  
            }
            DataList2.DataSource = dt;
            DataList2.DataBind();

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            HtmlInputSubmit submit = (HtmlInputSubmit)sender;
            string condition="";
            if (RadioButton1.Checked == true)
            {
                if (DropDownList1.Text == "产品编号")
                {
                    condition = "1";
                }
                else if (DropDownList1.Text == "产品名称")
                {
                    condition = "2";
                }
                else if (DropDownList1.Text == "供应商编号")
                {
                    condition = "3";
                }
                else if (DropDownList1.Text == "供应商名称")
                {
                    condition = "4";
                }
                Response.Redirect("/baseinfo/wareinfo.aspx?parent_nodeid=17&nodeid=18&keyword=" + Text1.Value + "&conditon=" + condition+"&select=1");
            }
            else
            {
           
                 if (DropDownList1.Text == "供应商编号")
                {
                    condition = "1";
                }
                else if (DropDownList1.Text == "供应商名称")
                {
                    condition = "2";
                }
                 Response.Redirect("/baseinfo/supplier.aspx?/baseinfo/supplier.aspx?parent_nodeid=14&nodeid=15&keyword=" + Text1.Value + "&conditon=" + condition+"&select=2");
            }
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            string[] loaditem={"产品编号","产品名称","供应商编号","供应商名称"};
            string[] loaditem1 = {"供应商编号", "供应商名称" };
            RadioButton radionbutton = (RadioButton)sender;
            DropDownList1.Items.Clear();
       
            if (radionbutton.ID == "RadioButton1")
            {
                for (int i = 0; i < loaditem.Length; i++)
                {
                    DropDownList1.Items.Add(loaditem[i]);
                }
            }
            else
            {
                for (int i = 0; i < loaditem1.Length; i++)
                {
                    DropDownList1.Items.Add(loaditem1[i]);
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["cookiename"];
            if(cookie!=null)
            { 
            bc.getcom(@"UPDATE AUTHORIZATION_USER SET STATUS='N' ,LEAVE_DATE=getdate() WHERE AUID='" + cookie.Values["auid"].ToString() + "'");
            cookie.Expires = DateTime.Now.AddDays(-365);
            Response.Cookies.Add(cookie);
            ClearClientPageCache();
            }
            Response.Redirect("/Default.aspx"); 
        }
        public void ClearClientPageCache()
        {
            //清除浏览器缓存 
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
        }
    }
}