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

namespace WPSS.PruductTrace
{
    public partial class step : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        protected string M_str_sql = @"
SELECT
distinct(STID)
,STEP_NAME
,ROW_NUMBER() OVER (ORDER BY {0} {1} )  AS  序号
,convert(varchar(10),a.MDATE,111) as 创建时间,
(select uname from userinfo where usid=a.makerid) as 用户名
,case when if_delete=0 then '删除'
else '还原'
end as 删除状态
,case when if_delete=0 then 'return confirm(''您确定要删除该条数据吗?'')'
else 'return confirm(''您确定要还原该条数据吗?'')'
end as 执行方法
 FROM step_mst A ";
    
        protected string M_str_sql1;
       
        StringBuilder sqb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    this.Page.Response.Expires = 0;
                    Bind();
                }
            }
            try
            {
                if (Request.QueryString["come"] != null)
                {
                    come.Value = Request.QueryString["come"];//采购单请求调用供应商信息
                }
            }
            catch (Exception)
            {


            }
            try
            {
                if (Request.QueryString["parent_nodeid"] != null)
                {
                    parent_nodeid.Value = Request.QueryString["parent_nodeid"];
                }
                if (Request.QueryString["nodeid"]!= null)
                {
                    nodeid.Value = Request.QueryString["nodeid"];
                }
            }
            catch (Exception)
            {

            }
            try
            {
      
              
            }
            catch (Exception)
            {

                Response.Redirect("/default.aspx");
            }  
        }
        #region Bind()
        private void Bind()
        {
            prompt.Visible = false;
            sqb = new StringBuilder(string.Format(M_str_sql, "a." + DropDownList8.SelectedValue, DropDownList9.SelectedValue));
            sqb.AppendFormat(" where  (a.step_name like '%{0}%' or stid like '%{0}%') ", Text1.Value);
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat(" and if_delete='1'");//是否删除
            }
            else
            {
                sqb.AppendFormat(" and if_delete='0'");//是否删除
            }

            if (TextBox1.Text != "" && TextBox2.Text != "")
            {
                sqb.AppendFormat(" and convert(varchar(10),a.mdate,120)>='{0}' and convert(varchar(10),a.mdate,120)<='{1}'", TextBox1.Text, TextBox2.Text);//制单日期
            }
            sqb.AppendFormat("   order by {0} {1} ", "convert(varchar(10),"+DropDownList8.SelectedValue+",111)", DropDownList9.SelectedValue);
            if (sqb.ToString().Length > 0)
            {
                dt = bc.getdt(sqb.ToString());

                if (DropDownList5.Text == "全部")
                {
                    GridView1.PageSize = dt.Rows.Count;
                }
                else
                {
                    GridView1.PageSize = Convert.ToInt32(DropDownList5.Text);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                prompt.Value = "没有找到记录";
                prompt.Visible = true;
            }
            nextpage();
            try
            {

            }
            catch (Exception ex)
            {
                prompt.Visible = true;
                prompt.Value = ex.Message;
            }
        }
        #endregion

        #region nextpage()
        protected void nextpage()
        {


            lblRecordCount.Text = "记录总数" + dt.Rows.Count + "条";
            lblPageCount.Text = "总页数" + (GridView1.PageCount).ToString() + "页";
            lblCurrentIndex.Text = "当前页第" + ((GridView1.PageIndex) + 1).ToString() + "页";
            if (dt.Rows.Count > 0)
            {
                if (GridView1.PageIndex == 0)
                {
                    btnFirst.Enabled = false;
                    btnPrev.Enabled = false;
                }
                else
                {
                    btnFirst.Enabled = true;
                    btnPrev.Enabled = true;
                }
                if (GridView1.PageIndex == GridView1.PageCount - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }

                // 计算生成分页页码,分别为："首 页" "上一页" "下一页" "尾 页"
                btnFirst.CommandName = "1";
                btnPrev.CommandName = (GridView1.PageIndex == 0 ? "1" : GridView1.PageIndex.ToString());

                btnNext.CommandName = (GridView1.PageCount == 1 ? GridView1.PageCount.ToString() : (GridView1.PageIndex + 2).ToString());
                btnLast.CommandName = GridView1.PageCount.ToString();
            }
            else
            {
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }

        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind();

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropdownlist = (DropDownList)sender;
            if (dropdownlist.ID == "DropDownList8")
            {
                Bind();

            }
            else if (dropdownlist.ID == "DropDownList9")
            {
                Bind();
            }
        }
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        protected void submit1_Click(object sender, EventArgs e)
        {
          
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
        
            if (submit.ID == "Submit1")
            {
                Bind();
            }
            else if (submit.ID == "Submit2")
            {
                Response.Redirect("/producttrace/stept.aspx?parent_nodeid=" + Request.QueryString["parent_nodeid"]);
            }
            else if (submit.Value == "修改")
            {
               
                int row = ((GridViewRow)((HtmlInputSubmit)sender).NamingContainer).RowIndex;
                Label l1 = (Label)GridView1.Rows[row].FindControl("L1");
                Response.Redirect("/producttrace/stept.aspx?id=" + l1.Text + "&parent_nodeid=" + Request.QueryString["parent_nodeid"]);
            }
            else if (submit.Value == "删除")
            {
                int row = ((GridViewRow)((HtmlInputSubmit)sender).NamingContainer).RowIndex;
                Label l1 = (Label)GridView1.Rows[row].FindControl("L1");
                basec.getcoms("update step_mst set if_delete=1,delete_date=getdate() where stid='" + l1.Text + "'");
                GridView1.EditIndex = -1;
                Bind();
            }
            else if (submit.Value == "还原")
            {
                int row = ((GridViewRow)((HtmlInputSubmit)sender).NamingContainer).RowIndex;
                Label l1 = (Label)GridView1.Rows[row].FindControl("L1");
                basec.getcoms("update step_mst set if_delete=0,delete_date=null where stid='" + l1.Text + "'");
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
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eeeeee',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }
         
            //e.Row.Attributes.Add("style", "height:0px");//这里设置GridView的行高
        }


        protected void PageButton_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
            Bind();
        }

        protected void btngo_Click(object sender, EventArgs e)
        {
            #region btngo
            try
            {
                if (txtNum.Text == "")
                {
                    //opAndvalidate.Show("页数不能为空");
                }
                else
                {
                    int vargo = Convert.ToInt32(txtNum.Text);
                    if (vargo <= GridView1.PageCount)
                    {
                        GridView1.PageIndex = Convert.ToInt32(txtNum.Text) - 1;
                        
                    }
                    else
                    {

                       prompt.Value= "索引超出范围'";
                    }
                }
            }
            catch (Exception)
            {
                //opAndvalidate.Show("输入格式不正确，请检查！");
            }

            #endregion
            Bind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
   
            Response.Redirect("../BaseInfo/suppliert.aspx" );
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
     
             
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Bind();
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }

    }
}
