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
using XizheC;

namespace WPSS
{
    public partial class VIEW : System.Web.UI.Page
    {
        basec bc = new basec();
        DataTable dt = new DataTable();
        WPSS.Validate va = new Validate();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (va.returnb() == true)
            {
                Response.Redirect("Default.aspx");
            }
            Bind();
        }
        protected void Bind()
        {
          
            if (Request.QueryString["PARENT_NODEID"]!= null)
            {
               
                dt = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='"+Request .QueryString ["usid"].ToString ()+
                    "' AND PARENT_NODEID='" + Request.QueryString["PARENT_NODEID"].ToString() + "' ORDER BY NODEID ASC");
            }
            else
            {

                dt = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + Request.QueryString["usid"].ToString() + "' AND PARENT_NODEID='18' ORDER BY NODEID ASC");
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            if (Request.QueryString["usid"] != null)
            {
                usid.Value = Request.QueryString["usid"].ToString();
            }
         

        }
    }
}
