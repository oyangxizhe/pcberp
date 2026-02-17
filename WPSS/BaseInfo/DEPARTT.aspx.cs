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


namespace WPSS.BaseInfo
{
    public partial class DEPARTT : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Text1.Value = IDO;
                Bind();
            }
            Text2.Focus();
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

            dt = basec.getdts("select * from DEPART where DEID='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {

                Text2.Value = dt.Rows[0]["DEPART"].ToString();

            }
        }

        protected void ClearText()
        {
            Text2.Value = "";


        }
        #region juage()
        private bool juage()
        {

            bool b = true;

            return b;
        }
        #endregion


        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {


            add();

        }
        private void add()
        {

            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM DEPART", "DEID", "DE");
            Bind();
            ADD_OR_UPDATE = "ADD";

        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                hint.Value = "";
                if (juage())
                {
                    save();
                    if (IFExecution_SUCCESS == true && ADD_OR_UPDATE == "ADD")
                    {
                        add();
                    }
                    else if (IFExecution_SUCCESS == true)
                    {
                        Bind();
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        #region save
        protected void save()
        {
            hint.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace ("/","-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");

            string v2 = bc.getOnlyString("SELECT DEPART FROM DEPART WHERE  DEID='" + Text1.Value + "'");
            if (!bc.exists("SELECT DEID FROM DEPART WHERE DEID='" + Text1.Value + "'"))
            {
                if (bc.exists("select * from DEPART where DEPART='" + Text2.Value + "'"))
                {

                    hint.Value = "该属性已经存在了！";

                }
                else
                {
                    basec.getcoms("insert into DEPART(DEID,DEPART,"
              + "Date,MakerID,Year,Month) values('" + Text1.Value
              + "','" + Text2.Value + "','" + varDate
              + "','" + varMakerID + "','" + year + "','" + month + "')");
                    IFExecution_SUCCESS = true;


                }
            }
            else if (v2 != Text2.Value)
            {
                if (bc.exists("select * from DEPART where DEPART='" + Text2.Value + "'"))
                {
                    hint.Value = "该属性已经存在了！";
                }
                else
                {

                    basec.getcoms("UPDATE DEPART SET DEPART='" + Text2.Value + "',MAKERID='" + varMakerID +
                        "',DATE='" + varDate + "' WHERE DEID='" + Text1.Value + "'");
                    IFExecution_SUCCESS = true;


                }

            }
            else
            {
                basec.getcoms("UPDATE DEPART SET DEPART='" + Text2.Value + "',MAKERID='" + varMakerID +
                     "',DATE='" + varDate + "' WHERE DEID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;



            }


        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../BaseInfo/DEPART.aspx" + n2);
        }
    }
}
