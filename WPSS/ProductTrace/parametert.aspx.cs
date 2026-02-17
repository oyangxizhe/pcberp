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
using System.Text;

namespace WPSS.ProductTrace
{
    public partial class parametert : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        StringBuilder sqb;
        public static string[] str1 = new string[] { "" };
        public static string[] strE = new string[] { "" };
        public bool IFExecution_SUCCESS { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Xizhe ERP";
            Label2.Text = "添加参数信息";
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    Text1.Focus();
                    try
                    {
                        id.Value = Request.QueryString["id"];
                        Label2.Text = "修改参数信息";
                    }
                    catch (Exception)
                    {

                    }
                    Bind();

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
        protected void Bind()
        {
            prompt.Visible = false;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {

                prompt.Visible = true;
                prompt.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            dt = basec.getdts("select * from parameter where id='" + id.Value  + "'");
            if (dt.Rows.Count > 0)
            {
                Text1.Value = dt.Rows[0]["parameter_name"].ToString();
                Text2.Value = dt.Rows[0]["unit"].ToString();
            }  
        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            HtmlInputSubmit submit = (HtmlInputSubmit)sender;
            if (submit.Value == "提交")
            {
                if (juage())
                {
                }
                else
                {
                    save();
                    Text1.Focus();
                }
            }
            else if (submit.Value == "上一页")
            {
                Response.Write("<script language=javascript>history.go(-2);</script>");
            }
            else if (submit.ID == "Submit3")
            {
                Response.Redirect("/ProductTrace/parameter.aspx?parent_nodeid=" + Request.QueryString["parent_nodeid"] + "&nodeid=" + Request.QueryString["nodeid"]);
            }
            try
            {


            }
            catch (Exception ex)
            {
                prompt.Value = ex.Message;
            }
        }
        protected void ClearText()
        {
            Text1.Value = "";
            Text2.Value = "";
        }
        #region juage()
        private bool juage()
        {
            string[] a = { "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            Text1.Style["background-color"] = "#ffffff";
            Text1.Style["color"] = "#595d5a";
            bool b = false;
            if (Text1.Value == "")
            {
                b = true;
                prompt.Value = "参数不能为空！";
                Text1.Style["background-color"] = "#e04c64";
                Text1.Style["color"] = "#ffffff";
                Text1.Focus();
                a[0] = prompt.Value;
            }
            if (a[0]!= "")
            {
                prompt.Value = a[0];
                Text1.Focus();
            }
            if (prompt.Value != "")
            {
                prompt.Visible = true;

            }
            return b;
        }
        #endregion
        #region save
        protected void save()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            string varMakerID = Request.Cookies["cookiename"].Values["usid"].ToString();
            string v2 = bc.getOnlyString("SELECT parameter_name FROM parameter WHERE  id='" + id.Value + "'");
            sqb = new StringBuilder();
            if (id.Value == "")
            {

                if (bc.exists("select * from parameter where parameter_name='" + Text1.Value + "'"))
                {
                    prompt.Value = "该参数已经存在了！";
                }
                else
                {
              
                    sqb.AppendFormat("INSERT INTO parameter");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("paid");
                    sqb.AppendFormat(",parameter_name");
                    sqb.AppendFormat(",unit");
                    sqb.AppendFormat(",MakerID");
                    sqb.AppendFormat(",MDate");
                    sqb.AppendFormat(",if_delete");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'", new basec().numYM_NEW(10,4,"0001","parameter","PAID","PA"));
                    sqb.AppendFormat(",'{0}'", Text1.Value);
                    sqb.AppendFormat(",'{0}'", Text2.Value);
                    sqb.AppendFormat(",'{0}'", Request.Cookies["cookiename"].Values["usid"].ToString());
                    sqb.AppendFormat(",getdate()");
                    sqb.AppendFormat(",0");
                    sqb.AppendFormat(")");
                }
            }
            else if (v2 != Text1.Value)
            {
                if (bc.exists("select * from parameter where parameter_name='" + Text1.Value + "'"))
                {
                    prompt.Value = "该参数已经存在了！";
                }
                else
                {
                   
                    sqb.AppendFormat("UPDATE parameter");
                    sqb.AppendFormat(" SET");
                    sqb.AppendFormat(" parameter_name='{0}'", Text1.Value);
                    sqb.AppendFormat(" ,unit='{0}'", Text2.Value);
                    sqb.AppendFormat(" where id='" + id.Value + "'");
                }
            }
            else
            {
                
                sqb.AppendFormat("UPDATE parameter");
                sqb.AppendFormat(" SET");
                sqb.AppendFormat(" parameter_name='{0}'", Text1.Value);
                sqb.AppendFormat(" ,unit='{0}'", Text2.Value);
                sqb.AppendFormat(" where id='" + id.Value + "'");
            }
          
            try
            {
                if (sqb.ToString().Length > 0)
                {
                    bc.getcom(sqb.ToString());
                    IFExecution_SUCCESS = true; ;
                }
            }
            catch (Exception ex)
            {
                prompt.Visible = true;
                prompt.Value = ex.Message;
            }

            if (IFExecution_SUCCESS && id.Value == "")//清空栏位继续添加
            {
                ClearText();
                Bind();

            }
            else if (IFExecution_SUCCESS)
            {
                Bind();
            }

        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../WareNature/Spec.aspx"+n2);
        }
    }
}
