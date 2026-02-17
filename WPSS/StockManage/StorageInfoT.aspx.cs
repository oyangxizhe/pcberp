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

namespace WPSS.StockManage
{
    public partial class StorageInfoT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();

        string v2;
        public static string[] str1 = new string[] { "" };
        public static string[] strE = new string[] { "" };
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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (va.returnb() == true)
                Response.Redirect("\\Default.aspx");
            bind();

        }
        protected void bind()
        {

            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
            if (!IsPostBack)
            {


                Text1.Value = IDO;
                    dt = basec.getdts("select * from StorageINFO where STORAGEID='" + Text1.Value + "'");
                    if (dt.Rows.Count > 0)
                    {

                        Text2.Value = dt.Rows[0]["STORAGENAME"].ToString();

                    }
            }
        }
        protected void ClearText()
        {
            Text2.Value = "";
        }

        protected void save()
        {
           
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");

            v2 = bc.getOnlyString("SELECT STORAGENAME FROM StorageINFO WHERE  STORAGEID='" + Text1.Value + "'");
            if (!bc.exists("SELECT SToRAGEID FROM StorageINFO WHERE STORAGEID='" + Text1.Value + "'"))
            {
                if (bc.exists("select * from StorageINFO where STORAGENAME='" + Text2.Value + "'"))
                {

                    hint.Value = "该仓库名称已经存在了！";

                }
                else
                {
                    basec.getcoms("insert into StorageINFO(STORAGEID,STORAGENAME,Date,MakerID,Year,Month) values('" + Text1.Value + "','" + Text2.Value +
                        "','" + varDate + "','" + varMakerID + "','" + year + "','" + month + "')");
                    IFExecution_SUCCESS = true;


                }
            }
            else if (v2 != Text2.Value)
            {
                if (bc.exists("select * from StorageINFO where storageName='" + Text2.Value + "'"))
                {
                    hint.Value = "该仓库名称已经存在了！";
                }
                else
                {

                    basec.getcoms("UPDATE StorageINFO SET STORAGENAME='" + Text2.Value + "',MAKERID='" + varMakerID +
                 "',DATE='" + varDate + "'WHERE STORAGEID='" + Text1.Value + "'");
                    IFExecution_SUCCESS = true;


                }

            }
            else
            {
                basec.getcoms("UPDATE StorageINFO SET STORAGENAME='" + Text2.Value + "',MAKERID='" + varMakerID +
              "',DATE='" + varDate + "'WHERE STORAGEID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;


            }


        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                add();
            }
            else if (submit.ID == "Submit2")
            {
                try
                {
                    save();
                    if (IFExecution_SUCCESS == true && ADD_OR_UPDATE == "ADD")
                    {
                        add();
                    }
                    else if (IFExecution_SUCCESS == true)
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
                Response.Redirect("../StockManage/StorageINFO.aspx" + n2);
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
        protected void add()
        {

            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM StorageINFO", "STORAGEID", "ST");
            bind();
            ADD_OR_UPDATE = "ADD";


        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            


        }

        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}
