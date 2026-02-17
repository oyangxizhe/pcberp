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
    public partial class PurchaseGode : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        CPURCHASE cpurchase = new CPURCHASE();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();
        basec bc = new basec();
        protected string sql = @"
with ds1 as (
SELECT
A.PGID AS PGID,
A.GODEDATE AS GODEDATE,
a.makerid,
B.PUID AS PUID,
B.SN AS SN,
C.WAREID AS WAREID,
G.SNAME AS SNAME,
CASE WHEN F.PurchaseStatus_MST='CLOSE' THEN '已入库'
WHEN F.PurchaseStatus_MST='PROGRESS' THEN '部分入库'
WHEN F.PurchaseStatus_MST='DELAY' THEN 'Delay'
WHEN F.PurchaseStatus_MST='OPEN' THEN 'open'
WHEN F.PurchaseStatus_MST='INVOICE' THEN '已开票'
END  AS PurchaseStatus_MST,
C.NEEDDATE AS NEEDDATE,
D.PLANK_THICKNESS,
D.SPEC,
E.CNAME AS CNAME,D.WNAME AS WNAME,D.CWAREID AS CWAREID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.GODERID )  AS GODER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
H.GECOUNT AS GECOUNT,
A.DATE AS DATE,
I.INVOICE_NO 发票号码,
I.INVOICE_HAVETAX_AMOUNT AS 应付金额,
ISNULL((SELECT SUM(PAYMENT_ORDER_AMOUNT) FROM PAYMENT_ORDER WHERE RMID IN (SELECT RMID FROM REQUEST_MONEY_MST WHERE PGID=A.PGID)),0) AS 已付金额,
isnull(I.INVOICE_HAVETAX_AMOUNT,0)-ISNULL((SELECT SUM(PAYMENT_ORDER_AMOUNT) FROM PAYMENT_ORDER
WHERE RMID IN (SELECT RMID FROM REQUEST_MONEY_MST WHERE PGID=A.PGID)),0) AS 未付金额,
CASE WHEN ISNULL((SELECT SUM(PAYMENT_ORDER_AMOUNT) FROM PAYMENT_ORDER
 WHERE RMID IN (SELECT RMID FROM REQUEST_MONEY_MST WHERE PGID=A.PGID)),0)=0 THEN '未付款'
WHEN ISNULL((SELECT SUM(PAYMENT_ORDER_AMOUNT) FROM PAYMENT_ORDER
WHERE RMID IN (SELECT RMID FROM REQUEST_MONEY_MST WHERE PGID=A.PGID)),0)=I.INVOICE_HAVETAX_AMOUNT THEN '已付款'
ELSE '部分付款'
END AS 付款状态 
FROM PURCHASEGODE_MST A
LEFT JOIN PURCHASEGODE_DET B ON A.PGID=B.PGID
LEFT JOIN PURCHASE_DET C ON C.PUID=B.PUID AND C.SN=B.SN
LEFT JOIN WareInfo D ON C.WareID =D.WareID 
LEFT JOIN CustomerInfo_MST E ON D.CUID =E.CUID 
LEFT JOIN Purchase_MST F ON C.PUID =F.PUID 
LEFT JOIN SupplierInfo_MST G ON F.SUID =G.SUID
LEFT JOIN GODE H ON H.GEKEY=B.PGKEY 
LEFT JOIN REQUEST_MONEY_MST I ON A.PGID=I.PGID
)
select * from ds1 
";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();

        protected void Page_Load(object sender, EventArgs e)
        {
       
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {
                    Title = "Xizhe ERP";
                    if (!IsPostBack)
                    {
                        //StartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd").Replace("/", "-");
                        //EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                        //CheckBox1.Checked = true;
                        bind();
                    }
                }
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
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            dt1 = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购入库'");
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
            string v11 = DropDownList1.Text;
            string v12 = Text4.Value;
            string v13 = Text5.Value;
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
                v6 = v4.ToString("yyyy-MM-dd HH:mm:ss");
                v7 = v5.ToString("yyyy-MM-dd HH:mm:ss");

            }
            StringBuilder sqb = new StringBuilder(sql);
            sqb.AppendFormat(@"  where    SNAME like '%" + v1 + "%' AND CWAREID like '%" + v10 +
                    "%'  AND WNAME like '%" + v9 +
                    "%' AND PUID LIKE '%" + v12 + "%' AND PGID LIKE '%" + v13 + "%'");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            dt = bc.getdt("select * from SCOPE_OF_AUTHORIZATION where usid='" + n2 + "'");//控制显示当前用户数据还是组权限或是所有用户
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["scope"].ToString() == "N")
                {
                    sqb.AppendFormat(" and MAKERID ='" + varMakerID + "'");
                }
            }
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat(" AND GODEDATE BETWEEN  '" + v6 + "'AND '" + v7 + "' ");

            }
            if (DropDownList1.Text == "已出货")

            {
                sqb.AppendFormat(" AND  PurchaseStatus_MST='已出货'");
            }
            else if (DropDownList1.Text == "部分出货")
            {
                sqb.AppendFormat(" AND  PurchaseStatus_MST='部分出货'");
            }
            else if (DropDownList1.Text == "Delay")
            {
                sqb.AppendFormat(" AND  PurchaseStatus_MST='Delay'");
            }
            else if (DropDownList1.Text == "Open")
            {
                sqb.AppendFormat(" AND PurchaseStatus_MST='Open'");
            }
            else if (DropDownList1.Text == "已开票")
            {
                sqb.AppendFormat(" AND  PurchaseStatus_MST='已开票'");
            }
            if (DropDownList4.Text == "已付款")
            {
                sqb.AppendFormat(" AND 付款状态='已付款'");
            }
            else if (DropDownList4.Text == "部分付款")
            {
                sqb.AppendFormat("  AND 付款状态='部分付款'");
            }
            else if (DropDownList4.Text == "未付款")
            {
                sqb.AppendFormat("  AND 付款状态='未付款'");
            }
            if (Text6.Value != null)
                sqb.AppendFormat(" AND  PLANK_THICKNESS LIKE '%" + Text6.Value + "%'");
            if (Text7.Value != null)
                sqb.AppendFormat(" AND  SPEC LIKE '%" + Text7.Value + "%'");
            sqb.AppendFormat("  order by GODEDATE DESC");
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
            nextpage();
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

            GridView1.DataKeyNames = new string[] { "PGID" };
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
            String[] str = new string[] { varID };
            WPSS.PurchaseManage.PurchaseGodeT.GETID[0] = str[0];
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../PurchaseManage/PurchaseGodeT.aspx"+n2);

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购入库'");
            string sql2, sql3;
            hint.Value = "";
            string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
            sql2 = "DELETE FROM PurchaseGode_MST WHERE PGID='" + ID + "'";
            sql3 = "DELETE FROM PurchaseGode_DET WHERE PGID='" + ID + "' ";
            string s2 = bc.getOnlyString("SELECT PUID FROM PURCHASEGODE_DET WHERE PGID='" + ID + "'");
            string s1 = bc.getOnlyString("SELECT STATUS FROM [PURCHASEGODE_DET] WHERE PGID='" + ID + "'");
            if (s1 == "SAVE")
            {
                hint.Value = "此采购入库单存在供应商对账单，不允许删除";
            }
            else if (s1 == "RECONCILE")
            {
                hint.Value = "此采购入库单已对账，不允许删除";
            }
            else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(ID, ""))
            {
                hint.Value = "此入库单已经存在应付款单不允许删除";
            }

            else if (bc.JuageDeleteCount_MoreThanStorageCount(ID))
            {

                hint.Value = bc.ErrowInfo;
            }
            else if (bc.exists("SELECT * FROM RETURN_DET WHERE PUID='" + s2 + "'"))
            {

                hint.Value = "采购单存在退货，不允许删除";
            }
            else if (v1 != "Y")
            {
                hint.Value = "您无删除权限！";
            }
            else
            {
                basec.getcoms(sql3);
                basec.getcoms("DELETE GODE WHERE GODEID='" + ID + "'");
                basec.getcoms(sql2);
                cpurchase.UPDATE_PURCHASE_STATUS(s2);
                GridView1.EditIndex = -1;
                bind();

            }
            try
            {
    

            }
            catch (Exception ex)
            {
                hint.Value = ex.Message;

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
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                hint.Value = "";
                string var2 = bc.numYM(10, 4, "0001", "SELECT * FROM PURCHASEGODE_MST", "PGID", "PG");
                PurchaseGodeT.NEWID[0] = var2;
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../PurchaseManage/PurchaseGodeT.aspx" + n2);
            }
            else if (submit.ID == "Submit2")
            {
                bind();
            }
            else if (submit.ID == "Submit3")
            {
                toexel();
            }

        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
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
        {//base.VerifyRenderingInServerForm(control);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
    }
}
