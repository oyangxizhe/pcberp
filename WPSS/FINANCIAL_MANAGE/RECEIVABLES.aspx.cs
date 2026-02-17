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
    public partial class RECEIVABLES : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        CRECEIVABLES cRECEIVABLES = new CRECEIVABLES();

        protected string M_str_sql1;
        WPSS.Validate va = new Validate();
        CEDIT_RIGHT cedit_right = new CEDIT_RIGHT();
        StringBuilder sqb;

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

            if (Request.QueryString["code"] != null)     //code不为null表示收款单作业要查询未收款的应收单号
            {
                if (!String.IsNullOrEmpty("code"))
                {
                    dt = bc.getdt("exec ReturnReceivables '" + Text1.Value + "','" + Text2.Value + "','" + Text3.Value + "',1");
                }
            }
            else
            {
                dt = bc.getdt("exec ReturnReceivables '" + Text1.Value + "','" + Text2.Value + "','" + Text3.Value + "',0");
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
            try
            {
           
        
            }
            catch (Exception)
            {

            }
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

            GridView1.DataKeyNames = new string[] { "应收单号" };
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
            RECEIVABLEST.IDO = varID;
            RECEIVABLEST.ADD_OR_UPDATE = "UPDATE";
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../FINANCIAL_MANAGE/RECEIVABLEST.aspx"+n2);
          

        }
        #region delete
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string sql2, sql3;
            hint.Value = "";
            string id = GridView1.DataKeys[e.RowIndex][0].ToString();
           
            if (bc.exists("select * from RECEIVABLES_ORDER where RCID='" + id + "'"))
             {

                 hint.Value = "该应收单有了收款单不允许删除！";
             }
            
             else
             {

                 dt = basec.getdts("SELECT * FROM RECEIVABLES_DET WHERE RCID='" + id + "'");
                 if (dt.Rows.Count > 0)
                 {
                     foreach (DataRow dr in dt.Rows)
                     {
                         if (dr["SSKEY"].ToString().Substring(0, 2) == "SE")
                         {
                             basec.getcoms(@"UPDATE SELLTABLE_DET SET STATUS='RECONCILE' WHERE SEKEY='" + dr["SSKEY"].ToString() + "'");
                         }
                         else
                         {
                             basec.getcoms(@"UPDATE SELLRETURN_DET SET STATUS='RECONCILE' WHERE SRKEY='" + dr["SSKEY"].ToString() + "'");
                         }
                     }

                 }
                 string v1 = bc.getOnlyString("SELECT ARID FROM RECEIVABLES_MST WHERE RCID='" + id + "'");
                 if (!string.IsNullOrEmpty(v1))
                 {
                     basec.getcoms("UPDATE ADVANCE_RECEIVABLES SET IF_ALREADY_USE='N' WHERE ARID='" + v1 + "'");
                 }
                 string sql = @"
 SELECT
 DISTINCT(C.ORID) AS ORID,
 C.SN AS SN
 FROM 
 RECEIVABLES_DET A 
 LEFT JOIN SELLTABLE_DET B ON A.SSKEY =B.SEKEY 
 LEFT JOIN ORDER_DET C ON B.SN=C.SN AND B.ORID=C.ORID 
 WHERE A.RCID='" + id + "' AND SUBSTRING (A.SSKEY,1,2)='SE'  ";
                 dt = bc.getdt(sql);
                 dt = bc.getdt(sql);
                 foreach (DataRow dro in dt.Rows)
                 {

                     basec.getcoms("UPDATE ORDER_DET SET IF_HAVE_INVOICE='N' WHERE ORID='" + dro["ORID"].ToString() + "' AND SN='" + dro["SN"].ToString() + "'");
                     basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='CLOSE' WHERE ORID='" + dro["ORID"].ToString() + "'");//还原订单状态

                }
                 sql2 = "DELETE FROM RECEIVABLES_MST WHERE RCID='" + id + "'";
                 sql3 = "DELETE FROM RECEIVABLES_DET WHERE RCID='" + id + "'";
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


        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

    }
}
