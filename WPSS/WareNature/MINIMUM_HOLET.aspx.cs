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


namespace WPSS.WareNature
{
    public partial class MINIMUM_HOLET : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        public static string[] str1 = new string[] { "" };
        public static string[] strE = new string[] { "" };
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                Bind();
            }

            if (va.returnb() == true)
                Response.Redirect("\\Default.aspx");
        }
        protected void Bind()
        {
          
            hint.Value = "";
            if (str1[0] != "")
            {
                Text1.Value = str1[0];
                str1[0] = "";
            }
            else
            {

                Text1.Value = strE[0];
                strE[0] = "";
                dt = basec.getdts("select * from MINIMUM_HOLE where MHID='" + Text1.Value + "'");
                if (dt.Rows.Count > 0)
                {

                    Text2.Value = dt.Rows[0]["MINIMUM_HOLE"].ToString();
                    Text3.Value = dt.Rows[0]["BASEVALUE"].ToString();
                }

            }

        }
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";

        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            btnSave.Enabled = true;
            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM MINIMUM_HOLE", "MHID", "MH");


        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                hint.Value = "";
                if (juage())
                {
                    save();
                }
            }
            catch (Exception)
            {

            }

        }
        #region juage()
        private bool juage()
        {

            bool b = true;
            if (bc.yesno(Text3.Value) == 0)
            {
                b = false;
                hint.Value = Text3.Value + "只能输入数字！";

            }
            return b;
        }
        #endregion
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

            string v2 = bc.getOnlyString("SELECT MINIMUM_HOLE FROM MINIMUM_HOLE WHERE  MHID='" + Text1.Value + "'");
            if (!bc.exists("SELECT MHID FROM MINIMUM_HOLE WHERE MHID='" + Text1.Value + "'"))
            {
                if (bc.exists("select * from MINIMUM_HOLE where MINIMUM_HOLE='" + Text2.Value + "'"))
                {

                    hint.Value = "该属性已经存在了！";

                }
                else
                {
                    basec.getcoms("insert into MINIMUM_HOLE(MHID,MINIMUM_HOLE,"
              + "Date,MakerID,Year,Month,BASEVALUE) values('" + Text1.Value
              + "','" + Text2.Value + "','" + varDate
              + "','" + varMakerID + "','" + year + "','" + month + "','" + Text3.Value + "')");


                }
            }
            else if (v2 != Text2.Value)
            {
                if (bc.exists("select * from MINIMUM_HOLE where MINIMUM_HOLE='" + Text2.Value + "'"))
                {
                    hint.Value = "该属性已经存在了！";
                }
                else
                {

                    basec.getcoms("UPDATE MINIMUM_HOLE SET MINIMUM_HOLE='" + Text2.Value + "',MAKERID='" + varMakerID +
                        "',DATE='" + varDate + "',BASEVALUE='" + Text3.Value +
                        "'WHERE MHID='" + Text1.Value + "'");

                }

            }
            else
            {
                basec.getcoms("UPDATE MINIMUM_HOLE SET MINIMUM_HOLE='" + Text2.Value + "',MAKERID='" + varMakerID +
                      "',DATE='" + varDate + "',BASEVALUE='" + Text3.Value +
                      "'WHERE MHID='" + Text1.Value + "'");


            }


        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 16, 16);
            Response.Redirect("../WareNature/MINIMUM_HOLE.aspx" + n2);
        }
    }
}
