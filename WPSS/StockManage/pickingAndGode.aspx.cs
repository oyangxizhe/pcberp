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

namespace WPSS.StockManage
{
    public partial class pickingAndGode : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtx4 = new DataTable();
        basec bc = new basec();

        WPSS.Validate va = new Validate();
        StringBuilder sqb;
        string sql = @"SELECT 
A.MPID 开料单号 ,
[dbo].[returnMISCPickingString](a.MPID,'wname') 开料品名,
[dbo].[returnMISCPickingString](a.MPID,'SPEC') 开料规格,
[dbo].[returnMISCPickingString](a.MPID,'PLANK_THICKNESS') PLANK_THICKNESS,
[dbo].[returnMISCPickingString](a.MPID,'CWAREID') 开料客户料号,
[dbo].[returnMISCPickingString](a.MPID,'MRCOUNT') 开料数量,
PICKING_DATE AS 开料日期,
PICKING_MAKERID AS 开料员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=PICKING_MAKERID )  AS 开料员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=MAKERID )  AS 制单人,
A.DATE AS 制单日期,
b.MGID 入库单号 ,
[dbo].[returnMISCGodeString](a.MPID,'wname') 入库品名,
[dbo].[returnMISCGodeString](a.MPID,'SPEC') 入库规格,
[dbo].[returnMISCGodeString](a.MPID,'PLANK_THICKNESS') PLANK_THICKNESS,
[dbo].[returnMISCGodeString](a.MPID,'CWAREID') 入库客户料号,
[dbo].[returnMISCGodeString](a.MPID,'GECOUNT') 入库数量,
case when STATUS IS NULL THEN 'open'
ELSE '已入库'
END AS 状态
FROM MISC_PICKING_MST a left join 
MISC_GODE_MST b on a.MPID =b.MPID ";
        int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {

                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    /*try
                    {
                        if (Request.QueryString["obj1"].ToString() != null)
                        {
                            hint1.Value = Request.QueryString["obj1"].ToString();

                        }
                    }
                    catch (Exception)
                    {

                    }*/
                    /*try
                    {

                        if (Request.QueryString["come"].ToString() != null)
                        {

                            come.Value = Request.QueryString["come"].ToString();
                        }
                    }//Response.Write(ex.Message);
                    catch (Exception)
                    {

                    }*/
                    //bind();
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
        #region bind()
        private void bind()
        {

            hint.Value = "";
            x.Value = "";
            x1.Value = "";
            GridView1.PageSize = 10;
            string v6 = "", v7 = "";
           // string v1 = Text1.Value;/*cname*/
            //string v2 = Text2.Value;/*storagename*/
            //string v3 = Text3.Value;/*batchid*/
            //string v4 = Text4.Value;/*wname*/
            //string v5 = Text5.Value;/*cwareid*/
            //dt = bc.getstoragecountNew("", Text4.Value, "", Text5.Value, "", Text2.Value, Text3.Value, Text1.Value,Text6.Value,Text7.Value);
            string v2 = StartDate.Value;
            string v3 = EndDate.Value;
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
                v6 = v4.ToString("yyyy-MM-dd");
                v7 = v5.ToString("yyyy-MM-dd");

            }
            sqb = new StringBuilder(sql);
            sqb.AppendFormat(" where a.mpid like '%" + Text1.Value + "%' and b.mgid like '%" + Text2.Value + "%'");
      
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat("  AND a.DATE>='" + v6 + "' AND  a.DATE<= '" + v7 + "'");
            }
            dt = bc.getdt(sqb.ToString ());
            if (dt.Rows.Count > 0)
            {
                x.Value = "1";
                GridView1.DataSource = dt;
                GridView1.DataBind();
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

                hint.Value = "没有找到记录";
                x.Value = "";
            }
            bind2();
            nextpage();
            try
            {


            }
            catch (Exception)
            {

            }
        }

        #endregion
        private void bind2()
        {

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
                Label1.Text = dtx.Rows[0]["wname"].ToString();
                Label2.Text = dtx.Rows[0]["cwareid"].ToString();
            }

        }
  
        private void clear()
        {

            dt = null;
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }


        #region nextpage()
        protected void nextpage()
        {


            GridView1.DataKeyNames = new string[] { "开料单号" };
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


        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit2")
            {
                bind();
            }
            else if (submit.ID == "Submit3")
            {
                toexel();
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            bind();
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
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
    }
}
