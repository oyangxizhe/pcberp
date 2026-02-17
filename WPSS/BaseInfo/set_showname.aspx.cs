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
using System.Net;
using System.Text;
using XizheC;

namespace WPSS.BaseInfo
{
    public partial class set_showname : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        protected string M_str_sql = @"SELECT (SELECT ENAME FROM EMPLOYEEINFO
WHERE EMID=A.MAKERID ) AS MAKER,* from set_showname A ";
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
                
                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    Bind();
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
        #region Bind()
        private void Bind()
        {
            dt = bc.getdt(M_str_sql);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            try
            {
              
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region select()
        protected void select()
        {

   
           
        }
        #endregion

      

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind();

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            try
            {
               
                TextBox co_wareid = (TextBox)GridView1.Rows[GridView1.SelectedIndex].Cells[0].FindControl("t1");
                TextBox wname = (TextBox)GridView1.Rows[GridView1.SelectedIndex].Cells[0].FindControl("t2");
                TextBox cwareid = (TextBox)GridView1.Rows[GridView1.SelectedIndex].Cells[0].FindControl("t3");
                string emid = Request.Cookies["cookiename"].Values["emid"].ToString();
                if (co_wareid.Text  == "" || wname.Text == "" || cwareid.Text == "")
                {
                    hint.Value = "栏位显示名称不能输入空值";
                }
                else
                {
                    bc.getcom("update set_showname set co_wareid='" + co_wareid.Text + "',wname='" + wname.Text + "',cwareid='" + cwareid.Text + "',makerid='" + emid + "',mdate=getdate()");
                    IFExecution_SUCCESS = true;
                }
            }
            catch (Exception ex)
            {
                IFExecution_SUCCESS = false;
                Response.Write(ex.Message);
            }
            if (IFExecution_SUCCESS)
            {
                Bind();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
         
         

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


        protected void PageButton_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
            Bind();
        }


        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Bind();
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
          
        }
    }
}
