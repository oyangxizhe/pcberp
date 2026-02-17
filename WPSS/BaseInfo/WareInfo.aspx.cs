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
using WPSS.UserManage;


namespace WPSS.BaseInfo
{
    public partial class WareInfo : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        public string sql = @"
SELECT A.WAREID AS WAREID,A.WNAME AS WNAME,
A.CO_WAREID AS CO_WAREID,A.CWAREID AS CWAREID,
A.SPEC AS SPEC,A.UNIT AS UNIT,A.CUID AS CUID,
B.CNAME AS CNAME,
C.SELLUNITPRICE AS SELLUNITPRICE,
D.PurchaseUnitPrice,
A.[SET_LEN] AS SET长,
A.[SET_WIDTH] AS SET宽,
A.[SET_COMPOSING] AS SET排版数,
A.CWAREID AS CWAREID,(SELECT ENAME FROM EMPLOYEEINFO 
WHERE EMID=A.MAKERID) AS MAKER,
SUBSTRING(A.DATE,1,10) AS DATE,
C.REMARK AS REMARK,
CASE WHEN A.ACTIVE='Y' THEN '正常'
WHEN A.ACTIVE='HOLD' THEN 'HOLD'
ELSE '作废'
END  AS ACTIVE,
A.TERMINAL AS TERMINAL
,DBO.RETURNWAREMAXCOUNTSTORAGENAME(A.WAREID) AS MaxStorageName
,DBO.RETURNWAREMAXCOUNTBATCHID(A.WAREID) AS MaxStorageBatchID
,DBO.RETURNWAREMAXSTORAGECOUNT(A.WAREID) AS MaxStorageCount
FROM  WAREINFO A
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN SELLUNITPRICE C ON A.WAREID=C.WAREID
LEFT JOIN PurchaseUnitPrice D ON A.WareID=D.WareID
";
        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        int i;
        StringBuilder sqb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"] != null)
            {
                //StartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd").Replace("/", "-");
                //EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-");
                //CheckBox1.Checked = true;
                try
                {
                    if (Request.QueryString["obj1"]!= null)
                    {
                        hint1.Value = Request.QueryString["obj1"].ToString();
                    }
                }
                catch (Exception)
                {

                }
                try
                {
                    if (Request.QueryString["come"] != null)
                    {
                        come.Value = Request.QueryString["come"].ToString();
                    }
                }
                catch (Exception)
                {

                }
                Bind();
            }
            else
            {
                //Response.Redirect("/default.aspx");
            }
        }
        #region Bind()
        private void Bind()
        {

            x.Value = "";
            x1.Value = "";
            hint.Value = "";
            string v6 = "", v7 = "";
            string v1 = Text1.Value;
            string v2 = StartDate.Value;
            string v3 = EndDate.Value;
            string v9 = Text2.Value;
            string v10 = Text3.Value;
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
            sqb = new StringBuilder(sql);
            sqb.AppendFormat(@" where B.CNAME like '%" + v1 + "%' AND A.CWAREID like '%" + v10 +"%'  AND A.WNAME like '%" + v9 +
                "%' ");
            if (Request.QueryString["CUID"] != null)
            {
                sqb.AppendFormat(" AND  A.CUID LIKE '%" + Request.QueryString["CUID"] + "%'");
            }
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            dt = bc.getdt("select * from SCOPE_OF_AUTHORIZATION a left join userinfo b on a.usid=b.usid " +
                "where a.usid='" + n2 + "'");//控制显示当前用户数据还是组权限或是所有用户
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["scope"].ToString() == "N")
                {
                    sqb.AppendFormat(" and a.MAKERID ='" + varMakerID + "'");
                }
                if (dt.Rows[0]["scope"].ToString() == "GROUP")
                {

                    sqb.AppendFormat(" and a.MAKERID IN (select EMID from SCOPE_OF_AUTHORIZATION a left join userinfo b " +
                        "on a.usid = b.usid where user_group = '"+ dt.Rows[0]["user_group"].ToString()+"')");
                }
               
            }
            if (CheckBox1.Checked)
            {
                sqb.AppendFormat(" AND A.DATE BETWEEN  '" + v6 + "'AND '" + v7 + "' ");

            }
            if (DropDownList1.Text == "正常" || DropDownList1.Text == "")
            {
                sqb.AppendFormat("AND A.ACTIVE='Y' ");
            }
            else if (DropDownList1.Text == "Hold")
            {
                sqb.AppendFormat("AND A.ACTIVE='HOLD' ");
            }
            else if (DropDownList1.Text == "作废")
            {
                sqb.AppendFormat("AND A.ACTIVE='N' ");
            }
            sqb.AppendFormat("order by A.DATE DESC");
            dt = bc.getdt(sqb.ToString());
            if (dt.Rows.Count > 0)
            {
                x.Value = Convert.ToString(1);
                x1.Value = Convert.ToString(1);
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
            nextpage();
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

            GridView1.DataKeyNames = new string[] { "WAREID" };
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
                string var1 = bc.numYM(9, 4, "0001", "SELECT * FROM WareINFO", "WAREID", "9");
                WareInfoT.str1[0] = var1;

                /*purchaseunitprice*/
                string a = bc.numYM(10, 4, "0001", "select * from PurchaseUnitPrice", "PPID", "PP");
                if (a == "Exceed limited")
                {

                    hint.Value = "编码超出限制！";
                }
                else
                {
                    WareInfoT.str1[1] = a;

                }
                /*purchaseunitprice*/

                /*sellunitprice*/
                string a1 = bc.numYM(10, 4, "0001", "select * from SellUnitPrice", "SPID", "SP");
                if (a1 == "Exceed limited")
                {

                    hint.Value = "编码超出限制！";
                }
                else
                {
                    WareInfoT.str1[2] = a1;

                }
                /*sellunitprice*/

                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../BaseInfo/WareInfoT.aspx" + n2);

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
        protected void btnAdd_Click(object sender, EventArgs e)
        {


        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            Bind();

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string varWareID = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
            String[] str = new string[] { varWareID };
            WPSS.BaseInfo.WareInfoT.strE[0] = str[0];
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../BaseInfo/WareInfoT.aspx" + n2);

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string id = GridView1.DataKeys[e.RowIndex][0].ToString();
                if (bc.JuageIfAllowDeleteWareID(id))
                {
                    hint.Value = bc.ErrowInfo;
                }
                else
                {
                    string sql = "SELECT * FROM WAREFILE WHERE WAREID='" + id + "'";
                    DataTable dt1 = basec.getdts(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        for (i = 0; i < dt1.Rows.Count; i++)
                        {
                            string FilePath = bc.getOnlyString("SELECT PATH FROM WAREFILE WHERE FLKEY='" + dt1.Rows[i]["FLKEY"].ToString() + "'");
                            string s1 = Server.MapPath(FilePath);
                            if (File.Exists(s1))
                            {
                                File.Delete(s1);
                            }
                        }
                    }
                    string strSql = "DELETE FROM WAREFILE WHERE WAREID='" + id + "'";
                    basec.getcoms(strSql);
                    string strSql1 = "DELETE FROM WareInfo WHERE WAREID='" + id + "'";
                    basec.getcoms(strSql1);
                    GridView1.EditIndex = -1;
                    Bind();
                }
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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {

            }
            else
            {


            }
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

        protected void DropDownList2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
