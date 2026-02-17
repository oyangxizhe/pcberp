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
using XizheC;
using System.IO;
using System.Diagnostics;

namespace WPSS.PurchaseManage
{
    public partial class PurchaseGodeT : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtx2 = new DataTable();
        DataTable dtx3 = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        CREQUEST_MONEY crequest_money = new CREQUEST_MONEY();
        CPURCHASE cpurchase = new CPURCHASE();
        int i;

        protected string M_str_sql = @"select A.PGKEY AS 索引,A.PGID AS 入库单号,A.PUID as 采购单号, A.SN as 项次,E.WareID as 品号,
B.CO_WAREID AS 料号,B.WNAME AS 品名,B.CWAREID AS 客户料号,C.PCOUNT AS 采购数量,E.GECount as 入库数量 ,E.BATCHID AS 批号,C.SUID as 供应商代码,"
            + "D.SName as 供应商名称 from PurchaseGode_DET A "
            + " LEFT JOIN PURCHASE_DET C ON A.PUID=C.PUID AND A.SN=C.SN"
            + " LEFT JOIN SUPPLIERINFO_MST D ON C.SUID=D.SUID"
            + " LEFT JOIN GODE E ON A.PGKEY=E.GEKEY"
            + " LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID";





        public static string[] NEWID = new string[] { "", "" };
        public static string[] GETID = new string[] { "" };
        public static string[] str2 = new string[] { "" };
        string PGKEY;
        int j;
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    Title = "Xizhe ERP";
                    if (NEWID[0] != "")
                    {
                        Text1.Value = NEWID[0];
                        NEWID[0] = "";
                    }
                    else
                    {
                        Assignment();
                    }
                    bind(); ;
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
        #region bind
        protected void bind()
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            string v1 = bc.getOnlyString("SELECT ADD_NEW FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购入库'");
            string v2 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购入库'");
            if (v1 == "Y")
            {
                Submit1.Visible = true;
     

            }
            else
            {
                Submit1.Visible = false;



            }
            if (v1 == "Y" || v2 == "Y")
            {
                Submit2.Visible = true;
            }
            else
            {
                Submit2.Visible = false;

            }
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
            x.Value = "";
            ControlFileDisplay.Value = "";
            GridView1.DataSource = as1(Text1.Value, Text2.Value);
            GridView1.DataKeyNames = new string[] { "项次" };
            GridView1.DataBind();


            string sql1 = new CPURCHASE_GODE ().sql  + " WHERE A.PUID='" + Text2.Value + "' AND A.PGID='" + Text1.Value + "'";
            dt4 = basec.getdts(sql1);
            if (dt4.Rows.Count > 0)
            {
                basec.GenerateColumns(GridView2, dt4).DataSource = dt4;//gridview自动生产列columns为0,所以要让它有列才能操作列名的自定义值
                basec.GenerateColumns(GridView2, dt4).DataBind();
                x.Value = Convert.ToString(1);
            }

            string sql3 = @"SELECT DISTINCT(A.WAREID) AS WAREID,B.FLKEY AS FLKEY,B.OLDFILENAME AS OLDFILENAME FROM PURCHASE_DET A LEFT JOIN WAREFILE B 
            ON A.WAREID=B.WAREID " + " WHERE A.PUID='" + Text2.Value + "' AND B.FLKEY IS NOT NULL ORDER BY A.WAREID,B.FLKEY,B.OLDFILENAME";
            dt = basec.getdts(sql3);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataKeyNames = new string[] { "FLKEY" };
                GridView3.DataBind();
                ControlFileDisplay.Value = Convert.ToString(1);

            }
            else
            {

                GridView3.DataSource = null;
            }
            if (bc.exists("SELECT * FROM PURCHASE_MST WHERE PUID='" + Text2.Value + "'"))
            {
                x2.Value = "exists";
            }

            dtx3 = basec.getdts("SELECT * FROM PURCHASEGODE_MST where PGID='" + Text1.Value + "'");
            if (dtx3.Rows.Count > 0)
            {
                Text3.Value = dtx3.Rows[0]["GODEDATE"].ToString();
                Text4.Value = dtx3.Rows[0]["GODERID"].ToString();
                Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + dtx3.Rows[0]["GODERID"].ToString() + "'");
            }
            else
            {
              
                Text3.Value = DateTime.Now.ToString("yyy-MM-dd");
                Text4.Value = varMakerID;
                Label1.Text = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + varMakerID + "'");

            }

            btnSure.ForeColor = System.Drawing.Color.Blue;
            string s1 = bc.getOnlyString("SELECT STATUS FROM PURCHASEGODE_DET WHERE PGID='" + Text1.Value + "'");
            if (s1 == "RECONCILE" || s1 == "INVOICE")
            {
                Submit4.Value= "已对帐";
       

            }
            else
            {
                Submit4.Value= "确认对帐";
          
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
                for (int i = 0; i < GridView2.Columns.Count; i++)
                {
                    if (GridView2.Columns[i].HeaderText == "料号")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["co_wareid"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "品名")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["wname"].ToString();
                    }
                    if (GridView2.Columns[i].HeaderText == "客户料号")
                    {
                        GridView2.Columns[i].HeaderText = dtx.Rows[0]["cwareid"].ToString();
                    }
                }

            }
            GridView1.DataBind();//重新绑定使自定义栏位的值生效
            GridView2.DataBind();//重新绑定使自定义栏位的值生效
        }
        #endregion
        #region assignment
        protected void Assignment()
        {

            #region Assignment
            Text1.Value = GETID[0];
            string s = bc.getOnlyString("SELECT PUID FROM PURCHASEGODE_DET WHERE PGID='" + GETID[0] + "'");
            GETID[0] = "";
            Text2.Value = s;
            Text5.Value = bc.getOnlyString("SELECT SNAME FROM SUPPLIERINFO_MST A LEFT JOIN PURCHASE_DET B ON A.SUID=B.SUID WHERE B.PUID='" + s + "'");

            #endregion
        }
        #endregion
        protected void btnSure_Click(object sender, EventArgs e)
        {

            if (!bc.exists("SELECT * FROM PURCHASE_MST WHERE PUID='" + Text2.Value + "'"))
            {
                hint.Value = "该采购单号不存在于系统中！";
                return;

            }
            if (String.IsNullOrEmpty(bc.getOnlyString("SELECT SUID FROM PURCHASE_MST WHERE PUID='" + Text2.Value + "'")))
            {
                hint.Value = "该采购单号没有维护过供应商！";
                return;

            }
            bind();
        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                ClearText();
                Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM PURCHASEGODE_MST", "PGID", "PG");
                bind();
            }
            else if (submit.ID == "Submit2")
            {
                try
                {
                    add();
                    if (IFExecution_SUCCESS == true)
                    {
                        bind();
                    }
                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../PurchaseManage/PURCHASEGODE.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                try
                {
                    hint.Value = "";
                    string v1 = bc.getOnlyString("SELECT STATUS FROM PURCHASEGODE_DET WHERE PGID='" + Text1.Value + "'");
                    if (v1 == "N")
                    {
                        basec.getcoms("UPDATE PURCHASEGODE_DET SET STATUS='RECONCILE' WHERE PGID='" + Text1.Value + "'");
                        bind();
                    }
                    else
                    {

                        hint.Value = "状态不为开立不能对账";
                    }
                }
                catch (Exception ex)
                {
                    hint.Value = ex.Message;

                }
            }
            else if (submit.ID == "Submit5")
            {
                hint.Value = "";
                string v1 = bc.getOnlyString("SELECT STATUS FROM PURCHASEGODE_DET WHERE PGID='" + Text1.Value + "'");
                if (v1 == "RECONCILE")
                {
                    basec.getcoms("UPDATE PURCHASEGODE_DET SET STATUS='N' WHERE PGID='" + Text1.Value + "'");
                    bind();
                }
                else if (v1 == "INVOICE")
                {
                    hint.Value = "状态为已开票不能还原";
                }
            }


        }

        #region ask
        private DataTable ask(string v1, string v2)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("采购单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("品号", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("采购数量", typeof(decimal));
            dtt.Columns.Add("累计入库数量", typeof(decimal));
            dtt.Columns.Add("未入库数量", typeof(decimal), "采购数量-累计入库数量");
            dtt.Columns.Add("入库数量", typeof(decimal));
            dtt.Columns.Add("仓库", typeof(string));
            dtt.Columns.Add("批号", typeof(string));
            dtt.Columns.Add("本入库单累计入库数量", typeof(decimal));
            dtt.Columns.Add("板厚", typeof(string));
            dtt.Columns.Add("铜厚", typeof(string));
            DataTable dtx1 = bc.getdt("SELECT SN  AS  项次,* FROM PURCHASE_DET WHERE PUID='" + v2 + "'");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["采购单号"] = dtx1.Rows[i]["PUID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["项次"].ToString();
                    dr["品号"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();

                    dr["板厚"] = dtx2.Rows[0]["PLANK_THICKNESS"].ToString();
                    dr["铜厚"] = dtx2.Rows[0]["SPEC"].ToString();

                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["采购数量"] = dtx1.Rows[i]["PCOUNT"].ToString();
                    dr["累计入库数量"] = 0;
                    dr["本入库单累计入库数量"] = 0;
                    dtt.Rows.Add(dr);

                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT A.PUID AS PUID,A.SN AS SN,B.WAREID AS WAREID,CAST(ROUND(SUM(B.GECOUNT),2) AS DECIMAL(18,2)) AS GECOUNT FROM PURCHASEGODE_DET A 
LEFT JOIN GODE B ON A.PGKEY=B.GEKEY  WHERE  A.PUID='" + v2 + "' GROUP BY A.PUID,A.SN,B.WAREID");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购单号"].ToString() == dtx4.Rows[i]["PUID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计入库数量"] = dtx4.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx5 = bc.getdt(@"SELECT A.PUID AS PUID,A.PGID AS PGID,A.SN AS SN,B.WAREID AS WAREID,
CAST(ROUND(SUM(B.GECOUNT),2) AS DECIMAL(18,2)) AS GECOUNT FROM PURCHASEGODE_DET A 
LEFT JOIN GODE B ON A.PGKEY=B.GEKEY  WHERE  A.PUID='" + v2 + "' AND A.PGID='" + v1 + "' GROUP BY A.PUID,A.PGID,A.SN,B.WAREID");
            if (dtx5.Rows.Count > 0)
            {
                for (i = 0; i < dtx5.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购单号"].ToString() == dtx5.Rows[i]["PUID"].ToString() &&
                            dtt.Rows[j]["项次"].ToString() == dtx5.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["本入库单累计入库数量"] = dtx5.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            return dtt;
        }
        #endregion
        #region as1
        private DataTable as1(string v1, string v2)
        {
            DataTable dtt = ask(v1, v2);
            for (i = 0; i < dtt.Rows.Count; i++)
            {
                dtt.Rows[i]["入库数量"] = dtt.Rows[i]["未入库数量"].ToString();
            }
            return dtt;
        }
        #endregion
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Label1.Text = "";
        }

        #region add
        protected void add()
        {

            hint.Value = "";
            string sql2 = "SELECT * FROM PURCHASE_DET WHERE PUID='" + Text2.Value + "'";
            dt1 = basec.getdts(sql2);
            string s1 = bc.getOnlyString("SELECT STATUS FROM [PURCHASEGODE_DET] WHERE PGID='" + Text1.Value + "'");

            if (dt1.Rows.Count > 0)
            {

                int count = dt1.Rows.Count;
                if (s1 == "SAVE")
                {
                    hint.Value = "此采购入库单存在供应商对账单，不允许修改";
                }
                else  if (s1 == "RECONCILE")
                {
                    hint.Value = "此采购入库单已对账，不允许修改";
                }
                else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(Text1.Value, ""))
                {
                    hint.Value = "此入库单已经存在应付款单不允许删除";
                }

                else if (!ac0(Text1.Value, Text2.Value))
                {

                }
                else if (Text4.Value == "")
                {

                    hint.Value = "工号不能为空！";

                }
                else if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='" + Text4.Value + "'"))
                {


                    hint.Value = "入库员工工号不存在于系统中！";
                }
                else if (PGKEY == "Exceed Limited")
                {
                    hint.Value = "编码超出限制！";

                }

                else
                {
                    add2(count);
                }

            }

        }
        #endregion
        #region add2
        private void add2(int count)
        {

            int k;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");

            for (k = 0; k < count; k++)
            {
                if (ac1(k) == 0)
                {

                }
                else
                {
                    PGKEY = bc.numYMD(20, 12, "000000000001", "select * from PURCHASEGODE_DET", "PGKEY", "PG");
                    string PGCOUNT = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
                    string STORAGENAME = ((TextBox)GridView1.Rows[k].Cells[9].FindControl("TextBox10")).Text;
                    string STORAGEID = bc.getOnlyString("SELECT STORAGEID FROM STORAGEINFO WHERE STORAGENAME='" + STORAGENAME + "'");
                    string BATCHID = ((TextBox)GridView1.Rows[k].Cells[9].FindControl("TextBox11")).Text;
                    string SN = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                    string WAREID = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                    string REMARK = ((TextBox)GridView1.Rows[k].Cells[12].FindControl("TextBox13")).Text;

                    string FREECOUNT = ((TextBox)GridView1.Rows[k].Cells[13].FindControl("TextBox14")).Text;
                    if (string.IsNullOrEmpty(FREECOUNT))
                    {
                        FREECOUNT = Convert.ToString(0);
                    }
                    if (PGCOUNT == "")
                    {

                        PGCOUNT = Convert.ToString(0);

                    }
                    string NEEDDATE = bc.getOnlyString("SELECT NEEDDATE FROM PURCHASE_DET WHERE PUID='" + Text2.Value + "' AND SN='" + SN + "'");
                    basec.getcoms("INSERT INTO PURCHASEGODE_DET(PGKEY,PGID,PUID,SN,REMARK,"
              + "Year,Month,Day,STATUS) values('" + PGKEY + "','" + Text1.Value
              + "','" + Text2.Value + "','" + SN + "','" + REMARK + "','" + year + "','" + month + "','" + day + "','N')");

                    basec.getcoms("INSERT INTO GODE(GEKEY,GODEID,SN,WAREID,"
           + "GECOUNT,STORAGEID,BATCHID,Date,MakerID,Year,Month,Day,FREECOUNT) values('" + PGKEY + "','" + Text1.Value
           + "','" + SN + "','" + WAREID + "','" + PGCOUNT + "','" + STORAGEID + "','" + BATCHID + "','" + varDate +
           "','" + varMakerID + "','" + year + "','" + month + "','" + day + "','" + FREECOUNT + "')");



                }

            }/*under FOR OUTSIDE*/


            cpurchase.UPDATE_PURCHASE_STATUS(Text2.Value);
            if (!bc.exists("SELECT PGID FROM PURCHASEGODE_DET WHERE PGID='" + Text1.Value + "'"))
            {
                return;

            }
            if (!bc.exists("SELECT PGID FROM PURCHASEGODE_MST WHERE PGID='" + Text1.Value + "'"))
            {

                basec.getcoms("INSERT INTO PURCHASEGODE_MST(PGID,GODEDATE,GODERID,MAKERID,DATE,"
+ "Year,Month,Day) values('" + Text1.Value + "','" + Text3.Value
+ "','" + Text4.Value + "','" + varMakerID + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");

                IFExecution_SUCCESS = true;
            }
            else
            {
                basec.getcoms("UPDATE PURCHASEGODE_MST SET GODEDATE='" + Text3.Value + "',GODERID='" + Text4.Value +
                    "',MAKERID='" + varMakerID + "',DATE='" + varDate + "' WHERE PGID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;
            }
            bind();

        }
        #endregion

        #region ac1()
        private int ac1(int k)
        {
            int x = 1;
            string v1 = ((TextBox)GridView1.Rows[k].Cells[8].FindControl("TextBox9")).Text;
            string v2 = ((TextBox)GridView1.Rows[k].Cells[9].FindControl("TextBox10")).Text;
            string v3 = ((TextBox)GridView1.Rows[k].Cells[6].FindControl("TextBox7")).Text;
            string v4 = ((TextBox)GridView1.Rows[k].Cells[10].FindControl("TextBox11")).Text;
            string v5 = ((TextBox)GridView1.Rows[k].Cells[13].FindControl("TextBox14")).Text;
            if (v1 == "")
            {

                x = 0;
                hint.Value = "数量不能为空！";

            }
            else if (bc.yesno(v1) == 0 || bc.yesno(v5) == 0)
            {
                x = 0;
                hint.Value = "数量只能输入数字！";

            }
            else if (v2 == "")
            {
                x = 0;
                hint.Value = "仓库不能为空！";

            }
            else if (!bc.exists("SELECT * FROM STORAGEINFO WHERE STORAGENAME='" + v2 + "'"))
            {
                x = 0;
                hint.Value = "该仓库不存在于系统中！";


            }
            else if (v4 == "")
            {
                x = 0;
                hint.Value = "批号不能为空！";


            }
            else if (decimal.Parse(v1)<=0)
            {
                Response.Write(v1);
                x = 0;
                hint.Value = "入库数量需大于0！";


            }
            else if (v5!="" && decimal.Parse(v5) <= 0)
            {
                x = 0;
                hint.Value = "Free数量需大于0！";


            }
            else if (decimal.Parse(v1) > decimal.Parse(v3))
            {
                x = 0;
                hint.Value = "入库数量不能大于未入库数量！";


            }
            return x;

        }
        #endregion
        private bool ac0(string s1, string s2)
        {
            bool c = true;
            if (bc.exists("SELECT * FROM PURCHASEGODE_DET WHERE PGID='" + s1 + "'"))
            {
                string s3 = bc.getOnlyString("SELECT PUID FROM PURCHASEGODE_DET WHERE PGID='" + s1 + "'");
                if (s3 != s2)
                {
                    hint.Value = "同一个入库单下面只能出现一个采购单号!";
                    c = false;
                }
            }
            return c;

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

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string v1 = GridView3.DataKeys[GridView3.SelectedIndex].Values[0].ToString();
                string FilePath = bc.getOnlyString("SELECT PATH FROM WAREFILE WHERE FLKEY='" + v1 + "'");
                FileInfo file = new FileInfo(Server.MapPath(FilePath));
                if (file.Exists)
                {
                    Response.Clear();
                    string fileName = HttpUtility.UrlEncode(file.Name);
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream;charset=gb2312";
                    Response.Filter.Close();
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
            catch (Exception)
            {

            }
        }


        #region gridview deleting
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string v1 = bc.getOnlyString("SELECT DEL FROM RIGHTLIST WHERE USID='" + n2 + "' AND NODE_NAME='采购入库'");
            try
            {
                string sql2, sql3;
                hint.Value = "";
                string ID = GridView1.DataKeys[e.RowIndex][0].ToString();
                sql2 = "DELETE FROM PurchaseGode_MST WHERE PGID='" + Text1.Value + "'";
                sql3 = "DELETE FROM PurchaseGode_DET WHERE PGID='" + Text1.Value + "' AND SN='" + ID + "'";
                string s1 = bc.getOnlyString("SELECT STATUS FROM [PURCHASEGODE_DET] WHERE PGID='" + Text1.Value + "'");
                if (s1 == "SAVE")
                {
                    hint.Value = "此采购入库单存在供应商对账单，不允许删除";
                }
                else if (s1 == "RECONCILE")
                {
                    hint.Value = "此采购入库单已对账，不允许删除";
                }
                else if (bc.JuageDeleteCount_MoreThanStorageCount(Text1.Value))
                {

                    hint.Value = bc.ErrowInfo;
                }
                else if (bc.exists("SELECT * FROM RETURN_DET WHERE PUID='" + Text2.Value + "' AND SN='" + ID + "'"))
                {

                    hint.Value = "采购单 项次存在退货，不允许删除";
                }
                else if (crequest_money.JUAGE_IF_EXISTS_PG_RETURN(Text1.Value, ""))
                {
                    hint.Value = "此入库单已经存在应付款单不允许删除";
                }
                else if (v1 != "Y")
                {
                    hint.Value = "您无删除权限！";
                }
                else
                {
                    basec.getcoms(sql3);
                    basec.getcoms("DELETE GODE WHERE GODEID='" + Text1.Value + "' AND SN='" + ID + "'");
                    if (!bc.exists("SELECT * FROM PURCHASEGODE_DET WHERE PGID='" + Text1.Value + "'"))
                    {
                        basec.getcoms(sql2);
                    }
                    cpurchase.UPDATE_PURCHASE_STATUS(Text2.Value);
                    GridView1.EditIndex = -1;
                    bind();

                }

            }
            catch (Exception)
            {


            }
        }
        #endregion

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
