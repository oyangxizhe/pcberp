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

namespace WPSS.BaseInfo
{
    public partial class QUALITY_INFOT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();

        WPSS.Validate va = new Validate();
        int i;
        DataTable dto = new DataTable();
     
        private static string _NUMID;
        public static string NUMID
        {
            set { _NUMID = value; }
            get { return _NUMID; }

        }

        private static string _ADD_OR_UPDATE;
        public static string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }

        }
        private static bool _IFExecutionSUCCESS;
        public static bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        CQUALITY_INFO cquality_info = new CQUALITY_INFO();
        #region page_load
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        Bind1();
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
        #endregion
        #region Bind1()
        protected void Bind1()
        {
            hint.Value = "";
            getbinddata();
            Text1.Value = NUMID;
            dt = basec.getdts(cquality_info .sql +" WHERE A.QUID='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {

                Text2.Value = dt.Rows[0]["WAREID"].ToString();
                Text3.Value = dt.Rows[0]["CNAME"].ToString();
                Text4.Value = dt.Rows[0]["CWAREID"].ToString();
                Text5.Value = dt.Rows[0]["WNAME"].ToString();
                Text6.Value = dt.Rows[0]["CO_WAREID"].ToString();
                Text7.Value = dt.Rows[0]["SNAME"].ToString();
            
                Text8.Value = dt.Rows[0]["INITIAL_DATE"].ToString();
                Text9.Value = dt.Rows[0]["RECENTLY_DATE"].ToString();
                Text10.Value = dt.Rows[0]["HAPPEN_DATE"].ToString();
                DropDownList1.Text = dt.Rows[0]["HAPPEN_PLACE"].ToString();
                DropDownList2.Text = dt.Rows[0]["DEFECT_NAME"].ToString();
                TextBox1.Text = dt.Rows[0]["DEFECT_DETAIL"].ToString();
                Text11.Value = dt.Rows[0]["TOTAL_COUNT"].ToString();
                Text12.Value = dt.Rows[0]["DEFECT_COUNT"].ToString();
                Text13.Value = dt.Rows[0]["DEFECT_PERIOD"].ToString();
                Text14.Value = dt.Rows[0]["DEFECT_PROPOTION"].ToString();
                DropDownList3.Text = dt.Rows[0]["DEFECT_PROCESS_MODE"].ToString();
                Text15.Value = dt.Rows[0]["DEFECT_LOSS"].ToString();
                TextBox2.Text = dt.Rows[0]["SOLUTION"].ToString();
                Text16.Value = dt.Rows[0]["MAKER"].ToString();
                Text17.Value = dt.Rows[0]["DATE"].ToString();
            }
            else
            {
                bind2();

            }
        }
        #endregion
        #region bind2
        protected void bind2()
        {
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            Text8.Value = DateTime.Now.ToString("MM-dd");
            Text9.Value = DateTime.Now.ToString("MM-dd");
            Text10.Value = DateTime.Now.ToString("MM-dd");
            DropDownList1.Text = "";
            DropDownList2.Text = "";
            TextBox1.Text = "";
            Text11.Value = "";
            Text12.Value = "";
            Text13.Value = "";
            Text14.Value = "";
            DropDownList3.Text = "";
            Text15.Value = "";
            TextBox1.Text = "";
            Text16.Value = "";
            Text17.Value = "";
        }
        #endregion
        #region getBindData()
        protected void getbinddata()
        {

            dto = SqlDT.SqlDTM("DEFECT", "DEFECT_NAME");
            if (DropDownList2.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList2.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {

                    DropDownList2.Items.Add(dr1[0].ToString());
                }
            }
        }
        #endregion
        #region bind
        protected void Bind()
        {
            DataList1.DataSource = dtx();
            DataList1.DataBind();
            DataTable dt1 = basec.getdts("SELECT * FROM WAREFILE WHERE WAREID='" + Text1.Value + "'");
            GridView1.DataSource = dt1;
            GridView1.DataKeyNames = new string[] { "FLKEY" };
            GridView1.DataBind();
            DataTable dtxx;
            dtxx = bc.getdt("select * from set_showname");
            if (dtxx.Rows.Count > 0)
            {
                Label1.Text = dtxx.Rows[0]["co_wareid"].ToString();
                Label2.Text = dtxx.Rows[0]["wname"].ToString();
                Label3.Text = dtxx.Rows[0]["cwareid"].ToString();
            }
        }
        #endregion
        protected DataTable dtx()
        {
            dt.Columns.Add("C", typeof(string));
            for (i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["C"] = Convert.ToString(i);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #region ClearText()
        protected void ClearText()
        {

            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            Text8.Value = "";
            Text9.Value = "";
            Text10.Value  = "";
            DropDownList1.Text = "正常";
            TextBox1.Text = "";
            bind2();

        }
        #endregion
        #region btnonloadfile
        protected void btnOnloadFile_Click(object sender, EventArgs e)
        {

            try
            {
                CFileInfo cf = new CFileInfo();
                cf.OnloadFile(Text1.Value);
                hint.Value = cf.ErrowInfo;
                Bind();
            }
            catch (Exception)
            {

            }

        }
        #endregion
        #region gridview_rowdatebound

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
        #endregion
        #region GridView1_RowDeleting
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id = GridView1.DataKeys[e.RowIndex][0].ToString();
                string FilePath = bc.getOnlyString("SELECT PATH FROM WAREFILE WHERE FLKEY='" + id + "'");
                string s1 = Server.MapPath(FilePath);
                if (File.Exists(s1))
                {
                    File.Delete(s1);
                }
                string strSql = "DELETE FROM WAREFILE WHERE FLKEY='" + id + "'";
                basec.getcoms(strSql);
                GridView1.EditIndex = -1;
                Bind();
            }
            catch (Exception)
            {


            }
        }
        #endregion
        #region  GridView1_SelectedIndexChanged
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string v1 = GridView1.DataKeys[GridView1.SelectedIndex].Values[0].ToString();
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
        #endregion
        #region  btnAdd_Click
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
           
            try
            {
                add();

            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region add()
        protected void add()
        {


            ClearText();
            GENERATE_ID();
        }
        #endregion
        #region GENERATE_ID()
        protected void GENERATE_ID()
        {


            CQUALITY_INFO cquality_info = new CQUALITY_INFO();
            if (cquality_info.GETID == "Exceed limited")
            {

                hint.Value = "编码超出限制！";
            }
            else
            {
               
                Text1.Value = cquality_info.GETID;
            }
     
            Bind();
            ADD_OR_UPDATE = "ADD";


        }
        #endregion
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
          
            try
            {
                save();
                if (ADD_OR_UPDATE == "ADD" && IFExecution_SUCCESS == true)
                {
                    add();
                }
            }
            catch (Exception)
            {

            }
        }
        #region save()
        protected void save()
        {
            hint.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            if (ac1() == 0)
            {
                IFExecution_SUCCESS = false;
            }
            else if (!bc.exists("SELECT * FROM QUALITY_INFO WHERE QUID='" + Text1.Value + "'"))
            {
                SQlcommandE(cquality_info .sqlo);
                Bind1();
                IFExecution_SUCCESS = true;
            }
            else
            {
                SQlcommandE(cquality_info .sqlt + " WHERE QUID='" + Text1.Value + "'");
                Bind1();
                IFExecution_SUCCESS = true;
            }


        }
        #endregion
        #region ac1()
        private int ac1()
        {

            int x = 1;
            if (Text3.Value == "")
            {
                x = 0;
                hint.Value = "该客户不能为空！";

            }
            else  if (!bc.exists("select * from customerinfo_MST where CNAME='" + Text3.Value + "'"))
            {
                x = 0;
                hint.Value = "该客户不存在于系统中！";

            }
            else if (Text4.Value == "")
            {
                x = 0;
                hint.Value = "该客户料号不能为空！";

            }
           else if (Text4.Value !="" && !bc.exists("select * from WAREINFO where CWAREID='" + Text4.Value + "'"))
           {
               x = 0;
               hint.Value = "该客户料号不存在于系统中！";

           }
            else if (Text11.Value !="" &&  bc.yesno(Text11.Value) == 0)
            {
                x = 0;
                hint.Value = "总数量只能输入数字！";

            }
           else if (Text12.Value != "" && bc.yesno(Text12.Value) == 0)
           {
               x = 0;
               hint.Value = "异常数量只能输入数字！";

           }
           else if (Text13.Value != "" && bc.yesno(Text13.Value) == 0)
           {
               x = 0;
               hint.Value = "异常周期只能输入数字！";

           }
           else if (Text15.Value != "" && bc.yesno(Text15.Value) == 0)
           {
               x = 0;
               hint.Value = "异常损失RMB只能输入数字！";

           }
          
            return x;

        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../quality_manage/quality_info.aspx" + n2);
        }
        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@QUID", SqlDbType.VarChar, 20).Value = Text1.Value;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = Text2.Value;
            sqlcom.Parameters.Add("@SUID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT SUID FROM SUPPLIERINFO_MST WHERE SNAME='" + Text7.Value + "'");
            sqlcom.Parameters.Add("@INITIAL_DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@RECENTLY_DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@HAPPEN_DATE", SqlDbType.VarChar, 20).Value = Text10.Value;
            sqlcom.Parameters.Add("@HAPPEN_PLACE", SqlDbType.VarChar, 20).Value = DropDownList1.Text;
            sqlcom.Parameters.Add("@DEFECT_NAME", SqlDbType.VarChar, 20).Value = DropDownList2.Text;
            sqlcom.Parameters.Add("@DEFECT_DETAIL", SqlDbType.VarChar, 1000).Value = TextBox1.Text;
            if (Text11.Value == "")
            {
                sqlcom.Parameters.Add("@TOTAL_COUNT", SqlDbType.VarChar, 20).Value = DBNull.Value;
            }
            else
            {
                sqlcom.Parameters.Add("@TOTAL_COUNT", SqlDbType.VarChar, 20).Value = Text11.Value;
            }
            if (Text12.Value == "")
            {
                sqlcom.Parameters.Add("@DEFECT_COUNT", SqlDbType.VarChar, 20).Value = DBNull.Value;
            }
            else
            {
                sqlcom.Parameters.Add("@DEFECT_COUNT", SqlDbType.VarChar, 20).Value = Text12.Value;
            }
            sqlcom.Parameters.Add("@DEFECT_PERIOD", SqlDbType.VarChar, 20).Value = Text13.Value;
            sqlcom.Parameters.Add("@DEFECT_PROCESS_MODE", SqlDbType.VarChar, 20).Value = DropDownList3.Text;
            sqlcom.Parameters.Add("@DEFECT_LOSS", SqlDbType.VarChar, 20).Value = Text15.Value;
            sqlcom.Parameters.Add("@SOLUTION", SqlDbType.VarChar, 1000).Value = TextBox2.Text;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion

        protected void btnReconcile_Click(object sender, EventArgs e)
        {
            Text2.Value = "";
            Text5.Value = "";
            Text9.Value = "";
            GENERATE_ID();
        }

    }
}
