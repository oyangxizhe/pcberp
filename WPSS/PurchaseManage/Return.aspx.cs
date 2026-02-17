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

namespace WPSS.PurchaseManage
{
    public partial class Return : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();
        CPURCHASE cpurchase = new CPURCHASE();
        basec bc = new basec();
        protected string M_str_sql = @"select DISTINCT(A.REID) AS REID,B.PUID AS PUID,D.SNAME AS SNAME,A.Return_DATE AS Return_DATE,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.Return_ID )  AS Return_ID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER
,A.DATE AS DATE FROM Return_MST A
LEFT JOIN Return_DET B ON A.REID=B.REID
LEFT JOIN PURCHASE_DET C ON B.PUID=C.PUID AND B.SN=C.SN 
LEFT JOIN SUPPLIERINFO_MST D ON C.SUID=D.SUID";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (va.returnb() == true)
                Response.Redirect("\\Default.aspx");
            bind();
        }
        #region bind()
        private void bind()
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            dt1 = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='退货单'");
            if (dt1.Rows.Count > 0)
            {

                if (dt1.Rows[0]["SEARCH"].ToString() == "Y")
                {
                    Submit2.Visible = true;
                }
                else
                {
                    Submit2.Visible = false;

                }
                if (dt1.Rows[0]["ADD_NEW"].ToString() == "Y")
                {
                    Submit1.Visible = true;
                }
                else
                {
                    Submit1.Visible = false;

                }
            }
            try
            {
                hint.Value = "";
                x.Value = "";
                GridView1.PageSize = 15;
                GridView1.Width = Unit.Percentage(80);
                select();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region select()
        protected void select()
        {

            string v5 = "", v6 = "";
            string v1 = StartDate.Value;
            string v2 = EndDate.Value;
            if (!bc.juagedate(v1, v2))
            {
                hint.Value = bc.ErrowInfo;
                return;
            }
            if (v1 != "" && v2 != "")
            {
                DateTime v3 = Convert.ToDateTime(v1);
                DateTime v4 = Convert.ToDateTime(v2);
                v5 = v3.ToString("yyyy-MM-d") + " 00:00:00";
                v6 = v4.ToString("yyyy-MM-d") + " 23:59:59";

            }

            if (Text1.Value != "" && StartDate.Value == "" && EndDate.Value == "")
            {


                M_str_sql1 = M_str_sql + " where A.REID like '%" + Text1.Value + "%'";
                dt = basec.getdts(M_str_sql1);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    hint.Value = "没有找到记录";
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                }

            }
            else if (Text1.Value == "" && StartDate.Value != "" && EndDate.Value != "")
            {
                M_str_sql1 = M_str_sql + " where A.DATE BETWEEN  '" + v5 + "'AND '" + v6 + "'";
                dt = basec.getdts(M_str_sql1);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    hint.Value = "没有找到记录";


                }


            }
            else if (Text1.Value != "" && StartDate.Value != "" && EndDate.Value != "")
            {
                M_str_sql1 = M_str_sql + " where A.DATE BETWEEN  '" + v5 + "'AND '" + v6 + "' AND A.REID LIKE '%" + Text1.Value + "%'";
                dt = basec.getdts(M_str_sql1);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    hint.Value = "没有找到记录";


                }
            }
            else
            {
                dt = basec.getdts(M_str_sql);
             


            }
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;

            }
            nextpage();
        }
        #endregion
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "REID" };
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
            bind();

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string varID = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
            ReturnT.IDO = varID;
            ReturnT.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../PurchaseManage/ReturnT.aspx" + n2);

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 10, 10);
                string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='退货单'");
                string sql2, sql3;
                hint.Value = "";
                string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
                sql2 = "DELETE FROM Return_MST WHERE REID='" + ID + "'";
                sql3 = "DELETE FROM Return_DET WHERE REID='" + ID + "' ";
                string s2 = bc.getOnlyString("SELECT PUID FROM Return_DET WHERE REID='" + ID + "'");
                string s1 = bc.getOnlyString("SELECT STATUS FROM RETURN_DET WHERE REID='" + ID + "'");
                if (s1 == "SAVE")
                {
                    hint.Value = "此退货单存在供应商对账单，不允许删除";
                }
                else if (s1 == "RECONCILE")
                {
                    hint.Value = "此退货单已对账，不允许删除";
                }
                else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(ID , ""))
                {
                    hint.Value = "此退货单已经存在应付账单不允删除";
                }
                else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }
                else
                {
                    basec.getcoms(sql3);
                    basec.getcoms("DELETE GODE WHERE GODEID='" + ID + "'");
                    basec.getcoms("DELETE MATERE WHERE MATEREID='" + ID + "'");
                    basec.getcoms(sql2);
                    cpurchase.UPDATE_PURCHASE_STATUS(s2);
                    GridView1.EditIndex = -1;
                    bind();

                }

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
            bind();
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
                        bind();
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
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                hint.Value = "";
                string var2 = bc.numYM(10, 4, "0001", "SELECT * FROM Return_MST", "REID", "RE");
                ReturnT.IDO = var2;
                ReturnT.ADD_OR_UPDATE = "ADD";
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../PurchaseManage/ReturnT.aspx" + n2);
            }
            else if (submit.ID == "Submit2")
            {
                bind();
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
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
          
        }

    }
}
