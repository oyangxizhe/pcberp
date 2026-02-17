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
using System.Security.Cryptography;


namespace WPSS.UserManage
{
    public partial class UserInfoT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        private static string _IDO;
        public static string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private static string _ADD_OR_UPDATE;
        public static string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
 
        CUSER cuser = new CUSER();
        protected string M_str_sql = @"
select A.USID AS USID,A.UNAME AS UNAME,A.EMID AS EMID,B.ENAME AS ENAME,A.PWD AS PWD,A.USER_GROUP AS USER_GROUP,
(SELECT ENAME FROM EMPLOYEEINFO  WHERE EMID=A.MAKERID) AS MAKER,A.DATE AS DATE from   USERINFO  A LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Text1.Value = IDO;
                Bind();
            }
           if (va.returnb() == true)
            Response.Redirect("\\Default.aspx"); 
        }
        protected void Bind()
        {
            
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
         
            dt = basec.getdts(M_str_sql + " where USID='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {

                Text2.Value = dt.Rows[0]["UNAME"].ToString();
                Text3.Value = dt.Rows[0]["EMID"].ToString();
                Label1.Text = dt.Rows[0]["ENAME"].ToString();
                Text5 .Value = dt.Rows[0]["USER_GROUP"].ToString();
            }
    
        }
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";
            Label1.Text = "";
            Text5.Value = "";
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                ClearText();
                Text1.Value = cuser.GETID();
            }
            else if (submit.ID == "Submit2")
            {
                save();
                try
                {



                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../UserManage/USERInfo.aspx" + n2);
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

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {


        }
        #region save
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
            string v2 = bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE  USID='" + Text1.Value + "'");
            Byte[] B = bc.GetMD5(Text4.Value);
            if (!juage1())
            {

            }
            else
            {
                cuser.PWD = Text4.Value;
                cuser.EMID = Text3.Value;
                cuser.MAKERID = varMakerID;
                cuser.USER_GROUP = Text5.Value;
                cuser.save("USERINFO", "USID", "UNAME",Text1.Value , Text2.Value , "用户ID", "用户名", "EMID", "", Text3 .Value , "", "工号");
                if (cuser.IFExecution_SUCCESS)
                {
                    IFExecution_SUCCESS = cuser.IFExecution_SUCCESS;
                    add();
                    Bind();
               

                }
                else
                {
                    hint.Value  = cuser.ErrowInfo;
                }
            }
          

        }
        #endregion
        private void add()
        {

            Text1.Value = cuser.GETID();
            ClearText();

        }
        #region juage1()
        private bool juage1()
        {

            bool ju = true;
            if (!bc.exists("SELECT * FROM EMPLOYEEINFO WHERE EMID='"+Text3.Value +"'"))
            {
                ju = false;
                hint.Value = "员工工号在系统中不存在！";
         
            }
            else if (Text4 .Value =="")
            {
                ju = false;
                hint.Value = "密码不能为空！";

            }
            else if (bc.checkEmail(Text4.Value) == false)
            {
                ju = false;
                hint.Value = "密码只能输入数字字母的组合！";

            }
            else if (Text4.Value.Length < 8)
            {
                ju = false;
                hint.Value = "密码长度需大于8位！";

            }
            else if (!bc.checkNumber (Text4 .Value ))
            {
                ju = false;
                hint.Value = "密码需是数字与字母的组合！";

            }
            else if (!bc.checkLetter(Text4.Value))
            {
                ju = false;
                hint.Value = "密码需是数字与字母的组合！";

            }
            else if (Text5 .Value == "")
            {
                ju = false;
                hint.Value = "用户组不能为空！";

            }
            return ju;

        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}
