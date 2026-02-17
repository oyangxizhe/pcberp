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
    public partial class Purchase : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        protected string sql = @"
select 
A.PUID as PUID,
B.SUID AS SUID,
B.SName as SNAME,
A.PDATE AS PDATE,
C.WAREID AS WAREID,
CASE WHEN A.PurchaseStatus_MST='CLOSE' THEN '已入库'
WHEN A.PurchaseStatus_MST='PROGRESS' THEN '部分入库'
WHEN A.PurchaseStatus_MST='DELAY' THEN 'Delay'
ELSE 'Open'
END  AS PurchaseStatus_MST,
C.NEEDDATE AS NEEDDATE,
E.CNAME AS CNAME,
D.WNAME AS WNAME,
D.PLANK_THICKNESS,
D.SPEC,
D.CWAREID AS CWAREID,
C.PCOUNT AS PCOUNT,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.PURID ) AS PUR,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
A.DATE AS DATE from Purchase_MST A  
LEFT JOIN SUPPLIERINFO_MST B ON A.SUID=B.SUID
LEFT JOIN Purchase_DET C ON A.PUID =C.PUID 
LEFT JOIN WareInfo D ON C.WareID =D.WareID 
LEFT JOIN CustomerInfo_MST E ON D.CUID =E.CUID 


";

        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        public string EMID { get; set; }
        public string SUID { get; set; }
        StringBuilder sqb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {

                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        SUID = "";
                        /*try
                        {
                            if (Request.QueryString["come"].ToString()!= null)
                            {
                                come.Value = Request.QueryString["come"].ToString();
                            }
                        }
                        catch (Exception)
                        {
                        }*/
                        if (IsPostBack)
                        {
                            //StartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd").Replace("/", "-");
                            //EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                            CheckBox1.Checked = true;
                        }
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

            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            dt1 = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购单'");
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
                GridView1.PageSize = 10;
                if (Request.QueryString["SUID"] != null)
                {
                    EMID = Request.QueryString["EMID"].ToString();
                    SUID = Request.QueryString["SUID"].ToString();
                    select();

                }
                else if (Request.QueryString["emid"] != null)
                {


                    EMID = Request.QueryString["EMID"].ToString();
                    select();

                }
                else
                {
                    string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
                    EMID = varMakerID;
                    select();

                }
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
            string v11 = DropDownList1.Text;
            string v12 = Text4.Value;
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
            sqb.AppendFormat(" where D.CWAREID like '%" + v10 +"%'");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            dt = bc.getdt("select * from SCOPE_OF_AUTHORIZATION where usid='" + n2 + "'");//控制显示当前用户数据还是组权限或是所有用户
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["scope"].ToString() == "N")
                {
                    sqb.AppendFormat(" and a.MAKERID ='" + varMakerID + "'");
                }
            }
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat("  AND A.PDATE>='"+v6+"' AND  A.PDATE<= '" + v7 + "'");
            }
            sqb.AppendFormat(" AND D.WNAME like '%" + v9 + "%' ");
            sqb.AppendFormat(" AND A.PUID LIKE '%" + v12 + "%'");
            if (Text1.Value != "")
            {
                sqb.AppendFormat(" AND B.SNAME like '%" + v1 + "%' ");
            }
            if (!String.IsNullOrEmpty(SUID))
            {
                sqb.AppendFormat(" AND B.SUID like '%" + SUID + "%' ");
                sqb.AppendFormat(" AND  A.PurchaseStatus_MST NOT IN ('CLOSE')");
            }
            if (DropDownList1.Text == "已入库")
            {
                sqb.AppendFormat("  AND A.PurchaseStatus_MST='CLOSE'");
            }
            else if (DropDownList1.Text == "部分入库")
            {
                sqb.AppendFormat(" AND A.PurchaseStatus_MST='PROGRESS' ");
            }
            else if (DropDownList1.Text == "Delay")
            {
                sqb.AppendFormat(" AND A.PurchaseStatus_MST='DELAY' ");
            }
            else if (DropDownList1.Text == "Open")
            {
                sqb.AppendFormat("  AND A.PurchaseStatus_MST='OPEN'");
            }
            if (Text5.Value != null)
                sqb.AppendFormat(" AND  D.PLANK_THICKNESS LIKE '%" + Text5.Value + "%'");
            if (Text6.Value != null)
                sqb.AppendFormat(" AND  D.SPEC LIKE '%" + Text6.Value + "%'");
            sqb.AppendFormat(" order by A.DATE DESC");
            //Response.Write(sqb.ToString());
            dt = bc.getdt(sqb.ToString());
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);

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
                //hint.Value = "没有找到记录";
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
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
            GridView1.DataBind();
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
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "PUID" };
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
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                hint.Value = "";
                string var2 = bc.numYM(10, 4, "0001", "SELECT * FROM Purchase_Mst", "PUID", "PU");
                PurchaseT.IDO = var2;
                PurchaseT.ADD_OR_UPDATE = "ADD";
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../PurchaseManage/PurchaseT.aspx" + n2);
            }
            else if (submit.ID == "Submit2")
            {
                Bind();
            }
            else if (submit.ID == "Submit3")
            {
                toexel();
            }

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind();

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string varPUID = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
            PurchaseT.IDO = varPUID;
            PurchaseT.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../PurchaseManage/PurchaseT.aspx"+n2);
          

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string sql2, sql3;
                hint.Value = "";
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 10, 10);
                string id = GridView1.DataKeys[e.RowIndex][0].ToString();
                sql2 = "DELETE FROM PURCHASE_MST WHERE PUID='" + id + "'";
                sql3 = "DELETE FROM PURCHASE_DET WHERE PUID='" + id + "'";
                string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购单'");
                if (bc.exists("select * from PURCHASEGODE_DET where PUID='" + id + "'"))
                {

                    hint.Value = "该采购单有了采购入库单不允许删除！";
                }
                else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }
                else
                {

                    basec.getcoms(sql2);
                    basec.getcoms(sql3);
                    GridView1.EditIndex = -1;
                    Bind();

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


        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

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
