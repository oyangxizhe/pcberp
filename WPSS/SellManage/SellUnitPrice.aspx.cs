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

namespace WPSS.SellManage
{
    public partial class SellUnitPrice : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        public string sql = @"
SELECT
A.WAREID AS WAREID,
B.CO_WAREID AS CO_WAREID,
B.WNAME AS WNAME,B.CWAREID AS CWAREID,
B.SPEC AS SPEC,
B.UNIT AS UNIT,
B.CUID AS CUID,
C.CNAME AS CNAME,
A.SPID AS SPID,
A.SELLUNITPRICE AS SELLUNITPRICE,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
A.DATE AS DATE 
FROM SELLUNITPRICE A
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN CUSTOMERINFO_MST C ON B.CUID=C.CUID
";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {

                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        Bind();
                    }
                }
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

            x.Value = "";
            hint.Value = "";
            GridView1.PageSize = 15;
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

            string v6 = "", v7 = "";
            string v1 = Text1.Value;
            string v2 = StartDate.Value;
            string v3 = EndDate.Value;
            string v9 = Text2.Value;
            string v10 = Text3.Value;
            string xi = "";
            dt = bc.getdt("SELECT * FROM LIMITE_SEARCH WHERE USID='" + _Default.USID + "' AND SUBSTRING(CUID_OR_SUID,1,2)='CU'");
            if (dt.Rows .Count >0)
            {
                xi =" AND B.CUID IN ("+ "SELECT CUID_OR_SUID FROM LIMITE_SEARCH WHERE USID='" + _Default.USID + 
                    "' AND SUBSTRING(CUID_OR_SUID,1,2)='CU'"+")";
            }
            if (!bc.juagedate(v2, v3))
            {
                hint.Value = bc.ErrowInfo;
                clear();
                return;
            }
            if (v2 != "" && v3 != "")
            {
                DateTime v4 = Convert.ToDateTime(v2);
                DateTime v5 = Convert.ToDateTime(v3);
                v6 = v4.ToString("yyyy-MM-dd") + " 00:00:00";
                v7 = v5.ToString("yyyy-MM-dd") + " 23:59:59";

            }
            if (CheckBox1.Checked)
            {
                showdata(@" where C.CNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
                    "%'  AND A.DATE BETWEEN  '" + v6 + "'AND '" + v7 + "' AND B.WNAME like '%" + v9 +
                    "%' " +xi);
            }

            else
            {
                if (CheckBox1.Checked )
                {
                    showdata(" WHERE DateDiff(day,A.DATE,getdate()) >-1 and DateDiff(day,A.DATE,getdate()) <+7 "+xi );


                }
                else
                {
                    showdata(@" where C.CNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
                  "%'  AND B.WNAME like '%" + v9 + "%'"+xi );


                }


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
            Text2.Value = "";
            Text3.Value = "";

        }
        #region showdata
        protected void showdata(string sqlo)
        {

            dt = bc.getdt(sql + sqlo + " AND B.ACTIVE='Y' order by A.DATE DESC");
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);
                if (DropDownList2.Text == "全部")
                {
                    GridView1.PageSize = dt.Rows.Count;
                }
                else
                {
                    GridView1.PageSize = Convert.ToInt32(DropDownList2.Text);
                }

            }
            else
            {
                //hint.Value = "没有找到记录";
            }
         
            GridView1.DataSource = dt;
            GridView1.DataBind();
            DataTable dtx;
            dtx = bc.getdt("select * from set_showname");
            if (dtx.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (GridView1.Columns[i].HeaderText == "料号")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "品名")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["wname"].ToString();
                    }
                    if (GridView1.Columns[i].HeaderText == "客户料号")
                    {
                        GridView1.Columns[i].HeaderText = dtx.Rows[0]["cwareid"].ToString();
                    }
                }
                Label3.Text = dtx.Rows[0]["wname"].ToString();
                Label4.Text = dtx.Rows[0]["cwareid"].ToString();
            }
        }
        #endregion
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "SPID" };
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

            string varCUID = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
            SellUnitPriceT.IDO = varCUID;
            SellUnitPriceT.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../SellManage/SellUnitPriceT.aspx"+n2);

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                hint.Value = "";
                string id = GridView1.DataKeys[e.RowIndex][0].ToString();
                string strSql1 = "DELETE FROM SellUnitPrice WHERE SPID='" + id + "'";
                basec.getcoms(strSql1);
                GridView1.EditIndex = -1;
                Bind();
            }
            catch (Exception)
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
                string a = bc.numYM(10, 4, "0001", "select * from SellUnitPrice", "SPID", "SP");
                if (a == "Exceed limited")
                {

                    hint.Value = "编码超出限制！";
                }
                else
                {
                    SellUnitPriceT.IDO = a;
                    SellUnitPriceT.ADD_OR_UPDATE = "ADD";

                }
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../SellManage/SellUnitPriceT.aspx" + n2);
            }
            else if (submit.ID == "Submit2")
            {
                Bind();
            }
            else if (submit.ID == "Submit3")
            {
                toexel();
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


        protected void btnToExcel_Click(object sender, ImageClickEventArgs e)
        {
            toexel();
        }
        private void toexel()
        {
            Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddHHssmm") + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {//base.VerifyRenderingInServerForm(control);
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }

    }
}
