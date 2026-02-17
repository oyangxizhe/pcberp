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
    public partial class CuReconcile : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        CORDER corder = new CORDER();
        CRECEIVABLES creceivables = new CRECEIVABLES();
        protected string sql = @"
SELECT
A.SEID AS SEID,
B.ORID AS ORID,
B.SN AS SN,
D.CNAME AS CNAME,
A.SELLDATE AS SELLDATE,
E.CUSTOMERORID AS CUSTOMERORID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.SELLERID )  AS SELLER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
CASE WHEN E.ORDERStatus_MST='CLOSE' THEN '已出货'
WHEN E.ORDERStatus_MST='PROGRESS' THEN '部分出货'
WHEN E.ORDERStatus_MST='DELAY' THEN 'Delay'
ELSE 'Open'
END  AS ORDERStatus_MST,
C.DeliveryDate AS DELIVERYDATE,
F.WName AS WNAME,
F.CWareID AS CWAREID,
A.DATE AS DATE,
A.SELLDATE,
G.MRCOUNT 
FROM SELLTABLE_MST A
LEFT JOIN SELLTABLE_DET B ON A.SEID=B.SEID
LEFT JOIN ORDER_DET C ON B.ORID=C.ORID AND B.SN=C.SN 
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN Order_MST E ON C.ORID =E.ORID 
LEFT JOIN WareInfo F ON C.WareID =F.WareID
LEFT JOIN MATERE G ON B.SEKEY=G.MRKEY 
";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        CORDER_RECONCILE corder_reconcile = new CORDER_RECONCILE();
        StringBuilder sqb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                /*StartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd").Replace("/", "-");
                EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                CheckBox1.Checked = true;*/
                bind();
            }
            if (va.returnb() == true)
            Response.Redirect("\\Default.aspx");
        }
        #region bind()
        private void bind()
        {
            hint.Value = "";
            x.Value = "";
            x1.Value = "";
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
            string v1 = Text1.Value;/*sname*/
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

            showdata();
        }
        #endregion
        private void clear()
        {

            
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Text1.Value = "";
        

        }
        protected void showdata()
        {
            string sql = @"
SELECT 
A.CRID AS 对账单号,
A.CUID AS 客户代码,
B.CNAME AS 客户名称,
A.NOTAX_AMOUNT AS 未税金额,
round(cast(A.NOTAX_AMOUNT as float)*1.16,2) AS 含税金额,
CONVERT(VARCHAR(10),a.DATE,112) AS 对账日期
FROM CU_RECONCILE_MST A
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID";
            sqb = new StringBuilder(sql);
            sqb.AppendFormat(" WHERE B.CNAME LIKE  '%" + Text1.Value + "%'");
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat(" AND CONVERT(VARCHAR(10),A.Date,112)>= '" + StartDate.Value + "'  AND CONVERT(VARCHAR(10),A.Date,112)<='" + EndDate.Value + "'");
            }
            sqb.AppendFormat(" AND A.CRID LIKE '%{0}%'", Text2.Value);
            sqb.AppendFormat("  ORDER BY  A.CRID ASC");
            dt = bc.getdt(sqb.ToString());
            //Response.Write(sqb.ToString());
            if (dt.Rows.Count > 0)
            {
                x1.Value = Convert.ToString(1);
                x.Value = Convert.ToString(1);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //Text7.Value = dt.Rows[dt.Rows.Count - 1]["未税金额"].ToString();

                //Text9.Value = dt.Rows[dt.Rows.Count - 1]["含税金额"].ToString();
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

                hint.Value = "没有找到记录!";
                Text7.Value = "";
                Text8.Value = "";
                Text9.Value = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            nextpage();
        }
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "对账单号" };
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
            WPSS.SellManage.CuReconcileT.NEWID[0] = str[0];
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../SellManage/cureconcilet.aspx"+n2);

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='客户对账单'");

            hint.Value = "";
            string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
            sqb = new StringBuilder();
            sqb.AppendFormat("DELETE FROM CU_RECONCILE_MST WHERE CRID='" + ID + "'");
            sqb.AppendFormat(";DELETE FROM CU_RECONCILE_DET WHERE CRID='" + ID + "'");
            string s1 = bc.getOnlyString("SELECT STATUS FROM [CU_RECONCILE_DET] WHERE CRID='" + ID + "'");
            if (s1 == "RECONCILE")
            {
                hint.Value = "此客户对账单已对账，不允许删除";
            }
            /*else if (creceivables.JUAGE_IF_EXISTS_SE_SERETURN(ID, ""))
            {
                hint.Value = "此客户对账单已经存在应收款单不允许删除";
            }*/

            /*else if (v1 != "Y")
            {
                hint.Value = "您无删除权限！";
            }*/
            else
            {
                dt = corder_reconcile.RETURN_PG_AND_RETURN_DT2(ID , "", "", "", "", "", "", false, true);
                DataTable dtx = bc.RETURN_NOHAVE_REPEAT_DT(dt, "销货销退单号");
                foreach (DataRow dr in dtx.Rows)
                {
                    if (dr["VALUE"].ToString().Substring(0, 2) == "SE")
                    {
                        sqb.AppendFormat(";UPDATE SELLTABLE_DET SET STATUS='N' WHERE SEID='" + dr["VALUE"].ToString() + "'");
                    }
                    else
                    {
                        sqb.AppendFormat(";UPDATE SELLRETURN_DET SET STATUS='N' WHERE SRID='" + dr["VALUE"].ToString() + "'");
                    }
                }
                basec.getcoms(sqb.ToString ());
                GridView1.EditIndex = -1;
                bind();

            }
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
                string var2 = new CORDER_RECONCILE().GETID();
                CuReconcileT.NEWID[0] = var2;
                Response.Redirect("../SELLManage/cureconcilet.aspx?aore=0");
            }
            else if (submit.ID == "Submit2")
            {
                bind();
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
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {



         
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
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
            bind();
        }

    }
}
