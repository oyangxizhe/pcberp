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
    public partial class OrderSearch : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dtx4 = new DataTable();
        basec bc = new basec();
        StringBuilder sqb;
        int i, j;



        string sqlo = @"
select A.ORID AS 订单号,
A.SN as 项次,
E.WareID as WAREID,
B.CO_WAREID AS 料号,
B.WNAME AS 品名,
B.SPEC,
B.PLANK_THICKNESS,
B.CWAREID AS 客户料号,
RTRIM(CONVERT(DECIMAL(18,2),C.SELLUNITPRICE)) AS 销售单价,
RTRIM(CONVERT(DECIMAL(18,2),C.TAXRATE )) AS 税率,
RTRIM(CONVERT(DECIMAL(18,2),C.OCount)) as 订单数量 ,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.MRCount))) as 累计销货数量 ,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.MRCOUNT*C.SELLUNITPRICE))) AS 未税金额,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.MRCOUNT*C.SELLUNITPRICE*C.TAXRATE/100) )) AS 税额,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.MRCOUNT*C.SELLUNITPRICE*(1+C.TAXRATE/100)) )) AS 含税金额,
C.CUID as 客户代码,
D.CName as 客户 ,
F.ORDERDATE AS 订单日期,
F.DATE AS 制单日期,
F.ORDERSTATUS_MST AS 状态,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SALEID )  AS 业务员
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN ORDER_MST F ON C.ORID=F.ORID
";
        string sqlt = @"
GROUP BY A.ORID,A.SN,E.WAREID,B.CO_WAREID,B.WNAME,B.SPEC,B.PLANK_THICKNESS,B.CWAREID,
C.SELLUNITPRICE,C.TAXRATE,C.OCOUNT,C.CUID,D.CNAME,F.ORDERDATE,F.DATE,F.SALEID,F.ORDERSTATUS_MST ORDER BY A.ORID,A.SN,F.DATE ASC
";

        protected string M_str_sql = @"
select A.SEKEY AS 索引,A.ORID AS 订单号,A.SEID AS 销货单号,A.SN as 项次,E.WareID as WAREID,
B.CO_WAREID AS 料号,B.WNAME AS 品名,B.CWAREID AS 客户料号,C.OCOUNT AS 订单数量,
RTRIM(CONVERT(DECIMAL(18,2),C.SELLUNITPRICE)) AS 销售单价,RTRIM(CONVERT(DECIMAL(18,2),C.TAXRATE )) AS 税率,
RTRIM(CONVERT(DECIMAL(18,2),E.MRCount)) as 销货数量 ,RTRIM(CONVERT(DECIMAL(18,2),E.MRCOUNT*C.SELLUNITPRICE)) AS 未税金额,
RTRIM(CONVERT(DECIMAL(18,2),E.MRCOUNT*C.SELLUNITPRICE*C.TAXRATE/100)) AS 税额,
RTRIM(CONVERT(DECIMAL(18,2),E.MRCOUNT*C.SELLUNITPRICE*(1+C.TAXRATE/100))) AS 含税金额,C.CUID as 客户代码,
D.CName as 客户名称 ,F.ORDERDATE AS 订单日期,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SALEID )  AS 业务员,
F.DATE AS 订单制单日期,G.SELLDATE AS 销货日期,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=G.SELLERID )  AS 销货员
,E.DATE AS 销货制单日期,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=E.MAKERID )  AS 销货制单人
from SELLTABLE_DET A              
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN ORDER_MST F ON C.ORID=F.ORID 
LEFT JOIN SELLTABLE_MST G ON A.SEID=G.SEID";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                CheckBox1.Checked = true;
                DataTable dtx;
                dtx = bc.getdt("select * from set_showname");
                if (dtx.Rows.Count > 0)
                {
                    Label3.Text = dtx.Rows[0]["wname"].ToString();
                    Label4.Text = dtx.Rows[0]["cwareid"].ToString();
                }
            }
             //if (va.returnb() == true)
            //Response.Redirect("\\Default.aspx");

        }
        #region bind()
        private void bind()
        {
            hint.Value = "";
            x.Value = "";
            x1.Value = "";
            GridView1.AllowPaging = true;
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
            sqb = new StringBuilder();
            if (CheckBox1.Checked)
            {
            
                sqb.Append(@" where    D.CNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
                    "%'  AND F.ORDERDATE BETWEEN  '" + v6 + "'AND '" + v7 + "' AND B.WNAME like '%" + v9 +
                    "%' AND A.ORID LIKE '%" + v12 + "%'");
            }

            else
            {
                sqb.Append(@" where    D.CNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
              "%'  AND B.WNAME like '%" + v9 + "%' AND A.ORID LIKE '%" + v12 + "%'");
            }
            if (DropDownList1.Text == "已出货")
            {
       
                sqb.Append(" AND F.ORDERStatus_MST='CLOSE' ");
            }
            else if (DropDownList1.Text == "部分出货")
            {
                sqb.Append(" AND F.ORDERStatus_MST='PROGRESS' ");
            }
            else if (DropDownList1.Text == "Delay")
            {
                sqb.Append(" AND F.ORDERStatus_MST='DELAY'");
            }
            else if (DropDownList1.Text == "Open")
            {
                sqb.Append(" AND F.ORDERStatus_MST='OPEN'  ");
            }
            if (!String.IsNullOrEmpty(Text5.Value))
                sqb.AppendFormat(" AND  PLANK_THICKNESS LIKE '%" + Text5.Value + "%'");
            if (!String.IsNullOrEmpty(Text6.Value))
                sqb.AppendFormat(" AND  SPEC LIKE '%" + Text6.Value + "%'");
            dt = ask(sqb.ToString());
            if (dt.Rows.Count > 1)
            {
                x.Value = Convert.ToString(1);
                basec.GenerateColumns(GridView1, dt).DataSource = dt;//因为要使用自定义标题列名称，所以要产生列名,使其有HeaderText属性
                basec.GenerateColumns(GridView1, dt).DataBind();
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
                hint.Value = "没有找到记录！";
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
      
        #region showdatao
        protected void showdatao(string sql)
        {

            dt = bc.getdt(M_str_sql+sql );
            if (dt.Rows.Count > 0)
            {

                x1.Value = Convert.ToString(1);
                x.Value = Convert.ToString(1);
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }

        }
        #endregion
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "订单号" };
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
        #region ask
        private DataTable ask(string sql)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("状态", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            //dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("板厚", typeof(string));
            dtt.Columns.Add("铜厚", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("未销货数量", typeof(decimal), "订单数量-累计销货数量");
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            dtt.Columns.Add("订单日期", typeof(string));
            dtt.Columns.Add("需求日期", typeof(string));
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
         

            DataTable dtx1 = bc.getdt(@"
SELECT A.ORID AS ORID,A.SN AS SN,A.WAREID AS WAREID,A.SELLUNITPRICE AS SELLUNITPRICE,
A.TAXRATE AS TAXRATE,A.NEEDDATE AS NEEDDATE,A.OCOUNT AS OCOUNT,D.CUID AS CUID,D.CNAME AS CNAME,
E.PHONE AS PHONE,E.ADDRESS AS ADDRESS,B.CWAREID,
CASE WHEN F.ORDERStatus_MST='CLOSE' THEN '已出货'
WHEN F.ORDERStatus_MST='PROGRESS' THEN '部分出货'
WHEN F.ORDERStatus_MST='DELAY' THEN 'Delay'
WHEN F.ORDERStatus_MST='INVOICE' THEN '已开票'
ELSE 'Open'
END  AS ORDERStatus_MST,
F.DATE AS DATE,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID) AS MAKER,
F.ORDERDATE 
FROM ORDER_DET A 
LEFT JOIN ORDER_MST F ON A.ORID=F.ORID 
LEFT JOIN CUSTOMERINFO_MST D ON A.CUID=D.CUID
LEFT JOIN CUSTOMERINFO_DET E ON D.CUKEY=E.CUKEY
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID" + sql+" ORDER BY F.ORDERDATE ASC");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["订单号"] = dtx1.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["ID"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    //dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["板厚"] = dtx2.Rows[0]["PLANK_THICKNESS"].ToString();
                    dr["铜厚"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["订单数量"] = dtx1.Rows[i]["OCOUNT"].ToString();
                    dr["销售单价"] = dtx1.Rows[i]["SELLUNITPRICE"].ToString();
                    dr["税率"] = dtx1.Rows[i]["TAXRATE"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["订单日期"] = dtx1.Rows[i]["ORDERDATE"].ToString();
                    dr["需求日期"] = dtx1.Rows[i]["NEEDDATE"].ToString();
                    dr["客户代码"] = dtx1.Rows[i]["CUID"].ToString();
                    dr["客户"] = dtx1.Rows[i]["CNAME"].ToString();
                    dr["制单人"] = dtx1.Rows[i]["MAKER"].ToString();
                    dr["制单日期"] = dtx1.Rows[i]["DATE"].ToString();
                    dr["状态"] = dtx1.Rows[i]["ORDERSTATUS_MST"].ToString();
                    dtt.Rows.Add(dr);

                }

            }

            DataTable dtx41 = bc.getdt(sqlo + sql + sqlt);
            if (dtx41.Rows.Count > 0)
            {
                for (i = 0; i < dtx41.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx41.Rows[i]["订单号"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx41.Rows[i]["项次"].ToString())
                        {
                            dtt.Rows[j]["累计销货数量"] = dtx41.Rows[i]["累计销货数量"].ToString();
                            decimal d1 = decimal.Parse(dtt.Rows[j]["销售单价"].ToString());
                            decimal d2 = decimal.Parse(dtx41.Rows[i]["累计销货数量"].ToString());
                            decimal d3 = decimal.Parse(dtt.Rows[j]["税率"].ToString());
                            dtt.Rows[j]["未税金额"] = (d1 * d2).ToString("#0.00");
                            dtt.Rows[j]["税额"] = (d1 * d2 * d3 / 100).ToString("#0.00");
                            dtt.Rows[j]["含税金额"] = (d1 * d2 * (1 + d3 / 100)).ToString("#0.00");
                            break;
                        }

                    }
                }

            }
            DataRow dr1 = dtt.NewRow();
            dr1["订单号"] = "合计";
            dr1["订单数量"] = dtt.Compute("SUM(订单数量)", "");
            dr1["累计销货数量"] = dtt.Compute("SUM(累计销货数量)", "");
            dr1["未销货数量"] = dtt.Compute("SUM(订单数量)-SUM(累计销货数量)", "");
            dr1["未税金额"] = dtt.Compute("SUM(未税金额)", "");
            dr1["税额"] = dtt.Compute("SUM(税额)", "");
            dr1["含税金额"] = dtt.Compute("SUM(含税金额)", "");
            dtt.Rows.Add(dr1);
            return dtt;
        }
        #endregion
  
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

      

        }
        protected void PageButton_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
            bind();
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {

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
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            bind();
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
        {//base.VerifyRenderingInServerForm(control);
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
    }
}
