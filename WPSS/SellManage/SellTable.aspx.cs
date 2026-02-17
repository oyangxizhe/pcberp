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
    public partial class SellTable : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        CORDER corder = new CORDER();
        CRECEIVABLES creceivables = new CRECEIVABLES();
        //按实际销货来收款，当累计收款金额=应收金额才算已收款
        /* protected string sql = @"
 with ds1 as (SELECT
 A.SEID AS SEID,
 B.ORID AS ORID,
 B.SN AS SN,
 D.CNAME AS CNAME,
 E.CUSTOMERORID AS CUSTOMERORID,
 (SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.SELLERID )  AS SELLER,
 (SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
 CASE WHEN E.ORDERStatus_MST='CLOSE' THEN '已出货'
 WHEN E.ORDERStatus_MST='PROGRESS' THEN '部分出货'
 WHEN E.ORDERStatus_MST='DELAY' THEN 'Delay'
 WHEN E.ORDERStatus_MST='OPEN' THEN 'Open'
 WHEN E.ORDERStatus_MST='INVOICE' THEN '已开票'
 END  AS ORDERStatus_MST,
 C.DeliveryDate AS DELIVERYDATE,
 F.WName AS WNAME,
 F.CWareID AS CWAREID,
 F.SPEC,
 F.PLANK_THICKNESS,
 A.DATE AS DATE,
 A.SELLDATE,
 G.MRCOUNT,
 H.INVOICE_NO 发票号码,
 H.INVOICE_HAVETAX_AMOUNT,
 H.INVOICE_HAVETAX_AMOUNT AS 应收金额,
 ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE SEID=A.SEID)),0) AS 已收金额,
 isnull(H.INVOICE_HAVETAX_AMOUNT,0)-ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER 
 WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE SEID=A.SEID)),0) AS 未收金额,
 CASE WHEN ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER
  WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE SEID=A.SEID)),0)=0 THEN '未收款'
 WHEN ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER 
 WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE SEID=A.SEID)),0)=H.INVOICE_HAVETAX_AMOUNT THEN '已收款'
 ELSE '部分收款'
 END AS 收款状态 
 FROM SELLTABLE_MST A
 LEFT JOIN SELLTABLE_DET B ON A.SEID=B.SEID
 LEFT JOIN ORDER_DET C ON B.ORID=C.ORID AND B.SN=C.SN 
 LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
 LEFT JOIN Order_MST E ON C.ORID =E.ORID 
 LEFT JOIN WareInfo F ON C.WareID =F.WareID
 LEFT JOIN MATERE G ON B.SEKEY=G.MRKEY 
 LEFT JOIN RECEIVABLES_MST H ON A.SEID=H.SEID
 )
 select * from ds1 
 ";*/
        
        protected string sql = @"
with ds1 as (SELECT
A.SEID AS SEID,
a.makerid,
B.ORID AS ORID,
B.SN AS SN,
D.CNAME AS CNAME,
E.CUSTOMERORID AS CUSTOMERORID,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.SELLERID )  AS SELLER,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID ) AS MAKER,
CASE WHEN E.ORDERStatus_MST='CLOSE' THEN '已出货'
WHEN E.ORDERStatus_MST='PROGRESS' THEN '部分出货'
WHEN E.ORDERStatus_MST='DELAY' THEN 'Delay'
WHEN E.ORDERStatus_MST='OPEN' THEN 'Open'
WHEN E.ORDERStatus_MST='INVOICE' THEN '已开票'
END  AS ORDERStatus_MST,
C.DeliveryDate AS DELIVERYDATE,
F.WName AS WNAME,
F.CWareID AS CWAREID,
F.SPEC,
F.PLANK_THICKNESS,
A.DATE AS DATE,
A.SELLDATE,
G.MRCOUNT,
isnull(CAST(ROUND((G.MRCOUNT*C.SELLUNITPRICE+C.URGENT)*(1+C.TAXRATE/100),2) AS DECIMAL(18,2)),0) AS 含税金额,
ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (Select RCID from RECEIVABLES_ORDER where RCID=a.SEID)),0) AS 已收金额,
CASE WHEN ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER
 WHERE RCID IN (SELECT SEID FROM SELLTABLE_MST WHERE SEID=A.SEID)),0)=0 THEN '未收款'
ELSE '已收款'
END AS 收款状态 
FROM SELLTABLE_MST A
LEFT JOIN SELLTABLE_DET B ON A.SEID=B.SEID
LEFT JOIN ORDER_DET C ON B.ORID=C.ORID AND B.SN=C.SN 
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN Order_MST E ON C.ORID =E.ORID 
LEFT JOIN WareInfo F ON C.WareID =F.WareID
LEFT JOIN MATERE G ON B.SEKEY=G.MRKEY 

)

SELECT (SELECT SUM(含税金额) from ds1 as ds11 where ds11.SEID=ds1.SEID group by SEID ) 合计含税金额,* from ds1  ";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        String usid;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"] != null)
                {
                    usid = Request.Cookies["cookiename"].Values["usid"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.Redirect("/default.aspx");
            }
            try
            {
                if(Request.QueryString["code"]!=null)
                {
                    code.Value = "0";
                }
            }
            catch(Exception )
            {

            }
            if (!IsPostBack)
            {
                Title = "Xizhe ERP";
                //StartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd").Replace("/", "-");
               // EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                //CheckBox1.Checked = true;
                bind();
            }
        
        }
        #region bind()
        private void bind()
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            dt1 = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + usid + "' AND NODE_NAME='销货单'");
            if (dt1.Rows.Count > 0)
            {

                if (dt1.Rows[0]["SEARCH"].ToString() == "Y")
                {
                    Submit3.Visible = true;
                    
           
                }
                else
                {
                    Submit3.Visible = false;


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
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                hint.Value = "";
                //string var2 = bc.numYM(10, 4, "0001", "SELECT * FROM SELLTABLE_MST", "SEID", "SE"); 
                //SellTableT.NEWID[0] = var2;
                WPSS.SellManage.SellTableT.GETID[0] = "";//清空之前可能存在的数据,避免带出之前的单号
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../SELLManage/SELLTABLET.aspx" + n2);
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
                v6 = v4.ToString("yyyy-MM-dd");
                v7 = v5.ToString("yyyy-MM-dd");

            }
            StringBuilder sqb = new StringBuilder(sql);
            sqb.AppendFormat(@" where    CNAME like '%" + v1 + "%' AND CWAREID like '%" + v10 +
          "%'  AND WNAME like '%" + v9 + "%' AND ORID LIKE '%" + v12 + "%' AND SEID LIKE '%" + v13 + "%'");
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
                sqb.AppendFormat(" AND SELLDATE BETWEEN  '" + v6 + "'AND '" + v7 + "' ");

            }
            if (DropDownList1.Text == "已出货")

            {
                sqb.AppendFormat(" AND ORDERStatus_MST='已出货'");
            }
            else if (DropDownList1.Text == "部分出货")
            {
                sqb.AppendFormat(" AND ORDERStatus_MST='部分出货'");
            }
            else if (DropDownList1.Text == "Delay")
            {
                sqb.AppendFormat(" AND ORDERStatus_MST='Delay'");
            }
            else if (DropDownList1.Text == "Open")
            {
                sqb.AppendFormat(" AND ORDERStatus_MST='Open'");
            }
            else if (DropDownList1.Text == "已开票")
            {
                sqb.AppendFormat(" AND ORDERStatus_MST='已开票'");
            }
            if (DropDownList4.Text == "已收款")
            {
                sqb.AppendFormat(" AND 收款状态='已收款'");
            }
            else if (DropDownList4.Text == "部分收款")
            {
                sqb.AppendFormat("  AND 收款状态='部分收款'");
            }
            else if (DropDownList4.Text == "未收款")
            {
                sqb.AppendFormat("  AND 收款状态='未收款'");
            }
            if (!String.IsNullOrEmpty(Text6.Value))
                sqb.AppendFormat(" AND  PLANK_THICKNESS LIKE '%" + Text6.Value + "%'");
            if (!String.IsNullOrEmpty(Text7.Value))
                sqb.AppendFormat(" AND  SPEC LIKE '%" + Text7.Value + "%'");
            //请求来自收款单，不显示已收款的销货单号
            if(code.Value=="0")
            {
                sqb.AppendFormat(" and 收款状态='未收款'");
            }
            sqb.AppendFormat("  order by SELLDATE DESC");
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

            nextpage();
        }
        #endregion
        private void clear()
        {

            
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Text1.Value = "";
            Text2.Value = "";
            Text3.Value = "";

        }

        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "SEID" };
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
            string[] str = new string[] { varID };
            DataTable dtt = bc.getdt("select * from SellTable_DET where seid='" + varID + "' order by orid,sn asc");
            WPSS.SellManage.SellTableT.GETID[0] = varID;
            WPSS.SellManage.SellTableT.GETID[1] = bc.Get_ORID_LIST(dtt);
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../SellManage/SellTableT.aspx"+n2);

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='销货单'");
            string sql2, sql3;
            hint.Value = "";
            string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
            sql2 = "DELETE FROM SELLTABLE_MST WHERE SEID='" + ID + "'";
            sql3 = "DELETE FROM SELLTABLE_DET WHERE SEID='" + ID + "' ";
            string s2 = bc.getOnlyString("SELECT ORID FROM SELLTABLE_DET WHERE SEID='" + ID + "'");
            string s1 = bc.getOnlyString("SELECT STATUS FROM [SellTable_DET] WHERE SEID='" + ID + "'");
            string nonull = bc.getOnlyString("SELECT B.ORID FROM SELLTABLE_DET A  LEFT JOIN SELLRETURN_DET B ON A.ORID=B.ORID AND A.SN=B.SN WHERE A.SEID='" + ID + "'");
            if (s1 == "SAVE")
            {
                hint.Value = "此销货单存在客户对账单，不允许删除";
            }
            else if (s1 == "RECONCILE")
            {
                hint.Value = "此销货单已对账，不允许删除";
            }
          /*  else if (creceivables.JUAGE_IF_EXISTS_SE_SERETURN(ID, ""))
            {
                hint.Value = "此销货单已经存在应收款单不允许删除";
            }*/
          else if(bc.exists("select * from RECEIVABLES_ORDER where rcid='"+ID+"'")==true)
            {
                hint.Value = "此销货单已经存在收款单不允许删除";
            }
            else if (bc.JuageDeleteCount_IFMoreThanTOTAL_ACTUAL_SELLCOUNT(s2, ID))
            {
                hint.Value = bc.ErrowInfo;
            }
            else if (v1 != "Y")
            {
                hint.Value = "您无删除权限！";
            }
            else
            {

                basec.getcoms(sql3);
                basec.getcoms("DELETE MATERE WHERE MATEREID='" + ID + "'");
                basec.getcoms(sql2);
                corder.UPDATE_ORDER_STATUS(s2);
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
