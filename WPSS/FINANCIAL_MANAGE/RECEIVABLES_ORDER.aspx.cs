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
using System.IO;
using System.Diagnostics;

namespace WPSS.FINANCIAL_MANAGE
{
    public partial class RECEIVABLES_ORDER : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        CRECEIVABLES_ORDER cRECEIVABLES_order= new CRECEIVABLES_ORDER();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();
        StringBuilder sqb;
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        CEDIT_RIGHT cedit_right = new CEDIT_RIGHT();
        String sql = @"select ROID 收款单号,D.CName 客户名称,
A.RCID AS 销货单号,
A.RECEIVABLES_ORDER_AMOUNT 收款金额,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.RECEIVABLES_ORDER_MAKERID ) AS 应收人,
A.RECEIVABLES_ORDER_DATE AS 收款日期,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS 制单人,
A.DATE AS 制单日期 from RECEIVABLES_ORDER A
LEFT JOIN SellTable_DET B ON A.RCID=B.SEID
LEFT JOIN Order_DET C ON B.ORID=C.ORID AND B.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Response.Expires = 0;
            if (va.returnb() == true)
            Response.Redirect("\\Default.aspx");
            Bind();
        }
        #region Bind()
        private void Bind()
        {

            hint.Value = "";
            x.Value = "";
            GridView1.PageSize = 10;
            select();
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

            sqb = new StringBuilder(sql);
            sqb.AppendFormat(" where ROID LIKE  '%" + Text1.Value + "%' AND  CNAME LIKE '%" + Text3.Value + "%' AND RCID LIKE '%" + Text4.Value + "%'");
          dt = bc.getdt(sqb.ToString());
            //dt = bc.getdt(sql);
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {

                //hint.Value = "找不到所要搜索项！";
                GridView1.DataSource = null;

            }
            nextpage();
        }
        #endregion
   
        private void clear()
        {

            dt = null;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Text1.Value = "";
         

        }
        #region showdata
        protected void showdata(string sqlo)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            dt = bc.getdt(cRECEIVABLES_order.sql + sqlo);
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {

                //hint.Value  = "找不到所要搜索项！";
                GridView1.DataSource = null;

            }

        }
        #endregion
 
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "收款单号" };
            GridView1.DataBind();
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

            string varID = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
            RECEIVABLES_ORDERT.IDO = varID;
            RECEIVABLES_ORDERT.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../FINANCIAL_MANAGE/RECEIVABLES_ORDERT.aspx"+n2);
          

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string sql2;
            hint.Value = "";
            string id = GridView1.DataKeys[e.RowIndex][0].ToString();
            sql2 = "DELETE FROM RECEIVABLES_ORDER WHERE ROID='" + id + "'";
            basec.getcoms(sql2);
            GridView1.EditIndex = -1;
            Bind();
            try
            {
         

            }
            catch (Exception)
            {


            }

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
                        Bind();
                    }
                    else
                    {
                        hint.Value = "没有找到记录";
                    }
                }
            }
            catch (Exception)
            {
                //opAndvalidate.Show("输入格式不正确，请检查！");
            }

            #endregion
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                hint.Value = "";
                RECEIVABLES_ORDERT.IDO = cRECEIVABLES_order.GETID();
                RECEIVABLES_ORDERT.ADD_OR_UPDATE = "ADD";
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../FINANCIAL_MANAGE/RECEIVABLES_ORDERT.aspx" + n2);
            }
            else if (submit.ID == "Submit2")
            {
                /*sqb = new StringBuilder(sql);
                sqb.AppendFormat(" where ROID LIKE  '%" + Text1.Value + "%' AND  CNAME LIKE '%" + Text3.Value + "%' AND RCID LIKE '%" + Text4.Value + "%'");
                GridView1.DataSource = null;
                dt = bc.getdt(sqb.ToString());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                nextpage();*/
                Bind();
            }
            else if (submit.ID == "Submit3")
            {
           
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

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Bind();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
