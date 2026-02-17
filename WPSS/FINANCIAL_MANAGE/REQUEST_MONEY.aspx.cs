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
    public partial class REQUEST_MONEY : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();

        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        CEDIT_RIGHT cedit_right = new CEDIT_RIGHT();

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
            if (Request.QueryString["code"] != null)     //code不为null表示付款单作业要查询未付款的应付单号
            {
                if (!String.IsNullOrEmpty("code"))
                {
                    dt = bc.getdt("exec ReturnPayment '" + Text1.Value + "','" + Text2.Value + "','" + Text3.Value + "',1");
                }
            }
            else
            {
                dt = bc.getdt("exec ReturnPayment '" + Text1.Value + "','" + Text2.Value + "','" + Text3.Value + "',0");
            }
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
  
 
        #region nextpage()
        protected void nextpage()
        {

            GridView1.DataKeyNames = new string[] { "应付单号" };
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
            REQUEST_MONEYT.IDO = varID;
            REQUEST_MONEYT.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../FINANCIAL_MANAGE/REQUEST_MONEYT.aspx"+n2);
          

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string sql2, sql3;
            hint.Value = "";
            string id = GridView1.DataKeys[e.RowIndex][0].ToString();
           
            if (bc.exists("select * from PAYMENT_ORDER where RMID='" + id + "'"))
             {

                 hint.Value = "该应付款单有了付款单不允许删除！";
             }
            
             else
             {

                 dt = basec.getdts("SELECT * FROM REQUEST_MONEY_DET WHERE RMID='" + id + "'");
                 if (dt.Rows.Count > 0)
                 {
                     foreach (DataRow dr in dt.Rows)
                     {
                         if (dr["PRKEY"].ToString().Substring(0, 2) == "PG")
                         {
                             basec.getcoms(@"UPDATE PURCHASEGODE_DET SET STATUS='RECONCILE' WHERE PGKEY='" + dr["PRKEY"].ToString() + "'");
                         }
                         else
                         {
                             basec.getcoms(@"UPDATE RETURN_DET SET STATUS='RECONCILE' WHERE REKEY='" + dr["PRKEY"].ToString() + "'");
                         }
                     }

                 }
                 string v1 = bc.getOnlyString("SELECT APID FROM REQUEST_MONEY_MST WHERE RMID='" + id + "'");
                 if (!string.IsNullOrEmpty(v1))
                 {
                     basec.getcoms("UPDATE ADVANCE_PAYMENT SET IF_ALREADY_USE='N' WHERE APID='" + v1 + "'");
                 }
                 string sql = @"
 SELECT
 DISTINCT(C.PUID) AS PUID,
 C.SN AS SN
 FROM 
 REQUEST_MONEY_DET A 
 LEFT JOIN PURCHASEGODE_DET B ON A.PRKEY =B.PGKEY 
 LEFT JOIN PURCHASE_DET C ON B.SN=C.SN AND B.PUID=C.PUID 
 WHERE A.RMID='" + id + "' AND SUBSTRING (A.PRKEY,1,2)='PG'";
                 dt = bc.getdt(sql);
                 foreach (DataRow dro in dt.Rows)
                 {

                     basec.getcoms("UPDATE PURCHASE_DET SET IF_HAVE_INVOICE='N' WHERE PUID='" + dro["PUID"].ToString() + "' AND SN='" + dro["SN"].ToString() + "'");


                 }
                 sql2 = "DELETE FROM REQUEST_MONEY_MST WHERE RMID='" + id + "'";
                 sql3 = "DELETE FROM REQUEST_MONEY_DET WHERE RMID='" + id + "'";
                 basec.getcoms(sql2);
                 basec.getcoms(sql3);

                 GridView1.EditIndex = -1;
                 Bind();

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
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            hint.Value = "";
            string var2 = bc.numYM(10, 4, "0001", "SELECT * FROM REQUEST_MONEY_Mst", "PUID", "PU");
            REQUEST_MONEYT.IDO = var2;
            REQUEST_MONEYT.ADD_OR_UPDATE = "ADD";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../FINANCIAL_MANAGE/REQUEST_MONEYT.aspx"+n2);
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {

            }
            else if (submit.ID == "Submit2")
            {
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


    }
}
