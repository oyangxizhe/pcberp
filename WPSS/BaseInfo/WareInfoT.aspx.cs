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
using System.Text;

namespace WPSS.BaseInfo
{
    public partial class WareInfoT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        
        WPSS.Validate va = new Validate();
        int i;
        public static string[] str1 = new string[] { "","","" };
        public static string[] strE = new string[] { "" };
        public string oldid { get; set; }
        DataTable dto = new DataTable();
        StringBuilder sqb = new StringBuilder();
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

  
            try
            {

                if (Request.Cookies["cookiename"] != null)
                {

                    if (bc.getOnlyString("SELECT  [COName] FROM [CompanyInfo_MST]").IndexOf("宜优捷") == -1)//当前三家客户只有宜优捷有公司LOGO
                    {
                        p1.Visible = false;
                    }
                    else
                    {
                        p1.Visible = true;
                    }
                    if (!IsPostBack)
                    {

                        Bind1();
                        Bind();
                    }
                }
                else
                {
                    Response.Redirect("/default.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                
            }
        }
        protected void Bind1()
        {
            hint.Value = "";
            getbinddata();
            if (str1[0] != "")
            {
                Text1.Value = str1[0];
                x2.Value = str1[1];
                x3.Value = str1[2];
                str1[0] = "";
                str1[1] = "";
                str1[2] = "";

            }
            else
            {

                Text1.Value = strE[0];
                strE[0] = "";
                dt = basec.getdts("select * from WareInfo where WAREID='" + Text1.Value + "'");
                if (dt.Rows.Count > 0)
                {

                    Text1.Value = dt.Rows[0]["WAREID"].ToString();
                    Text2.Value = dt.Rows[0]["CO_WAREID"].ToString();
                    Text3.Value = dt.Rows[0]["WNAME"].ToString();
                    Text4.Value = dt.Rows[0]["CWAREID"].ToString();
                    DropDownList1.Text = dt.Rows[0]["SPEC"].ToString();
                    Text5.Value = bc.getOnlyString("SELECT CNAME FROM CUSTOMERINFO_MST WHERE CUID='" + dt.Rows[0]["CUID"].ToString() + "'");
                    Text6.Value = dt.Rows[0]["LINEA_PN"].ToString();
                    Text7.Value = dt.Rows[0]["LINEB_PN"].ToString();
                    Text8.Value = dt.Rows[0]["LINEC_PN"].ToString();
                    Text9.Value = dt.Rows[0]["LINED_PN"].ToString();
                    DropDownList2.Text = dt.Rows[0]["PLANK_TYPE"].ToString();
                    DropDownList3.Text = dt.Rows[0]["PLANK_THICKNESS"].ToString();
                    DropDownList4.Text = dt.Rows[0]["PLANK_TOLERANCE"].ToString();
                    DropDownList5.Text = dt.Rows[0]["PANEL"].ToString();
                    Text10.Value = dt.Rows[0]["PCS_LEN"].ToString();
                    Text11.Value = dt.Rows[0]["PCS_WIDTH"].ToString();
                    Text12.Value = dt.Rows[0]["SET_LEN"].ToString();
                    Text13.Value = dt.Rows[0]["SET_WIDTH"].ToString();
                    Text14.Value = dt.Rows[0]["SET_COMPOSING"].ToString();
                    DropDownList6.Text = dt.Rows[0]["SURFACE_TREATMENT"].ToString();
                    DropDownList7.Text = dt.Rows[0]["SURFACE_THICKNESS"].ToString();
                    DropDownList8.Text = dt.Rows[0]["SOLDER_MASK"].ToString();
                    DropDownList9.Text = dt.Rows[0]["CHARACTER_COLOR"].ToString();
                    Text15.Value = dt.Rows[0]["PANEL_NEED"].ToString();
                    DropDownList10.Text = dt.Rows[0]["IMPEDANCE"].ToString();
                    DropDownList11.Text = dt.Rows[0]["ASSIGN_STACKUP"].ToString();
                    DropDownList12.Text = dt.Rows[0]["CORE_COPPER"].ToString();
                    DropDownList13.Text = dt.Rows[0]["OUT_COPPER"].ToString();
                    DropDownList14.Text = dt.Rows[0]["CIRCUIT_SPEC"].ToString();
                    DropDownList15.Text = dt.Rows[0]["THICKNESS_COPPER"].ToString();
                    DropDownList16.Text = dt.Rows[0]["BGA_DESIGN"].ToString();
                    Text16.Value = dt.Rows[0]["BGA_PAD"].ToString();
                    DropDownList17.Text = dt.Rows[0]["COPPER_NEED"].ToString();
                    DropDownList18.Text = dt.Rows[0]["MINIMUM_HOLE"].ToString();
                    Text17.Value = dt.Rows[0]["PCS_COUNT"].ToString();
                    DropDownList19.Text = dt.Rows[0]["MOLDING_STYLE"].ToString();
                    DropDownList20.Text = dt.Rows[0]["MOLDING_TOLERANCE"].ToString();
                    Text18.Value = dt.Rows[0]["VCUT_SET"].ToString();
                    DropDownList21.Text = dt.Rows[0]["VCUT_ANGLE"].ToString();
                    DropDownList27.Text = dt.Rows[0]["VCUT_DISABLED"].ToString();
                    DropDownList22.Text = dt.Rows[0]["IF_HYPOTENUSE"].ToString();
                    DropDownList23.Text = dt.Rows[0]["HYPOTENUSE_ANGLE"].ToString();
                    Text20.Value = dt.Rows[0]["DEPTH_NEED"].ToString();
                    DropDownList24.Text = dt.Rows[0]["TEST_STYLE"].ToString();
                    TextBox1.Text = dt.Rows[0]["REMARK"].ToString();
                    DropDownList26.Text = dt.Rows[0]["BIGANDSMALL_PANEL"].ToString();
                    Text19 .Value  = dt.Rows[0]["TERMINAL"].ToString();
                    Text21.Value = bc.getOnlyString("select flow_name from flow_mst where flid='" + dt.Rows[0]["flid"].ToString() + "'");
                    if (dt.Rows[0]["ACTIVE"].ToString() == "Y")
                    {
                        DropDownList25.Text = "正常";
                    }
                    else if (dt.Rows[0]["ACTIVE"].ToString() == "HOLD")
                    {
                        DropDownList25.Text = "Hold";
                    }
                    else
                    {
                        DropDownList25.Text = "作废";
                    }
                }

            }
     

        }
        #region getBindData()
        protected void getbinddata()
        {


            dto = SqlDT.SqlDTM("SPEC", "SPEC");
            if (DropDownList1.Items.Count-1 != dto.Rows.Count)
            {
                DropDownList1.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                  
                    DropDownList1.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("PLANK_TYPE", "PLANK_TYPE");
            if (DropDownList2.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList2.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList2.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("PLANK_THICKNESS", "PLANK_THICKNESS");
            if (DropDownList3.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList3.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList3.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("PLANK_TOLERANCE", "PLANK_TOLERANCE");
            if (DropDownList4.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList4.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList4.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("PANEL", "PANEL");
            if (DropDownList5.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList5.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList5.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("SURFACE_TREATMENT", "SURFACE_TREATMENT");
            if (DropDownList6.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList6.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList6.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("SURFACE_THICKNESS", "SURFACE_THICKNESS");
            if (DropDownList7.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList7.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList7.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("SOLDER_MASK", "SOLDER_MASK");
            if (DropDownList8.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList8.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList8.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("CHARACTER_COLOR", "CHARACTER_COLOR");
            if (DropDownList9.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList9.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList9.Items.Add(dr1[0].ToString());

                }
            }

            dto = SqlDT.SqlDTM("CORE_COPPER", "CORE_COPPER");
            if (DropDownList12.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList12.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList12.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("OUT_COPPER", "OUT_COPPER");
            if (DropDownList13.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList13.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList13.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("CIRCUIT_SPEC", "CIRCUIT_SPEC");
            if (DropDownList14.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList14.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList14.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("COPPER_NEED", "COPPER_NEED");
            if (DropDownList17.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList17.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList17.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("MINIMUM_HOLE", "MINIMUM_HOLE");
            if (DropDownList18.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList18.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList18.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("MOLDING_STYLE", "MOLDING_STYLE");
            if (DropDownList19.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList19.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList19.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("MOLDING_TOLERANCE", "MOLDING_TOLERANCE");
            if (DropDownList20.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList20.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList20.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("VCUT_ANGLE", "VCUT_ANGLE");
            if (DropDownList21.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList21.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList21.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("HYPOTENUSE_ANGLE", "HYPOTENUSE_ANGLE");
            if (DropDownList23.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList23.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList23.Items.Add(dr1[0].ToString());

                }
            }
            //dto = SqlDT.SqlDTM("IMPEDANCE", "IMPEDANCE");
            dto = bc.getdt("SELECT * FROM IMPEDANCE ORDER BY IMID DESC ");
            if (DropDownList10.Items.Count != dto.Rows.Count)
            {
                //DropDownList10.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList10.Items.Add(dr1["IMPEDANCE"].ToString());

                }
            }
            //dto = SqlDT.SqlDTM("ASSIGN_STACKUP", "ASSIGN_STACKUP");
            dto = bc.getdt("SELECT * FROM ASSIGN_STACKUP ORDER BY ASID DESC ");
            if (DropDownList11.Items.Count != dto.Rows.Count)
            {
                //DropDownList11.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList11.Items.Add(dr1["ASSIGN_STACKUP"].ToString());

                }
            }
            //dto = SqlDT.SqlDTM("THICKNESS_COPPER", "THICKNESS_COPPER");
            dto = bc.getdt("SELECT * FROM THICKNESS_COPPER ORDER BY TCID DESC ");
            if (DropDownList15.Items.Count != dto.Rows.Count)
            {
                //DropDownList15.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList15.Items.Add(dr1["THICKNESS_COPPER"].ToString());

                }
            }
            //dto = SqlDT.SqlDTM("BGA_DESIGN", "BGA_DESIGN");
            dto = bc.getdt("SELECT * FROM BGA_DESIGN ORDER BY BDID DESC ");
            if (DropDownList16.Items.Count != dto.Rows.Count)
            {
                //DropDownList16.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList16.Items.Add(dr1["BGA_DESIGN"].ToString());

                }
            }
            //dto = SqlDT.SqlDTM("IF_HYPOTENUSE", "IF_HYPOTENUSE");
            dto = bc.getdt("SELECT * FROM IF_HYPOTENUSE ORDER BY IHID DESC ");
            if (DropDownList22.Items.Count != dto.Rows.Count)
            {
                //DropDownList22.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList22.Items.Add(dr1["IF_HYPOTENUSE"].ToString());

                }
            }
            dto = SqlDT.SqlDTM("BIGANDSMALL_PANEL", "BIGANDSMALL_PANEL");
            if (DropDownList26.Items.Count-1 != dto.Rows.Count)
            {
                DropDownList26.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList26.Items.Add(dr1[0].ToString());

                }
            }
            dto = SqlDT.SqlDTM("VCUT_DISABLED", "VCUT_DISABLED");
            if (DropDownList27.Items.Count - 1 != dto.Rows.Count)
            {
                DropDownList27.Items.Add("");
                foreach (DataRow dr1 in dto.Rows)
                {
                    DropDownList27.Items.Add(dr1[0].ToString());

                }
            }

        }
        #endregion
        protected void Bind()
        {
            DataList1.DataSource = dtx();
            DataList1.DataBind();
            DataTable dt1 = basec.getdts("SELECT * FROM WAREFILE WHERE WAREID='" + Text1.Value + "'");
            GridView1.DataSource = dt1;
            GridView1.DataKeyNames = new string[] { "FLKEY" };
            GridView1.DataBind();

            DataTable dtx1;
            dtx1 = bc.getdt("select * from set_showname");
            if (dtx1.Rows.Count > 0)
            {
                Label1.Text = dtx1.Rows[0]["co_wareid"].ToString();
                Label2.Text = dtx1.Rows[0]["wname"].ToString();
                Label3.Text = dtx1.Rows[0]["cwareid"].ToString();
            }

        }
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
            DropDownList1.Text= "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            Text8.Value = "";
            Text9.Value = "";
            DropDownList2.Text= "";
            DropDownList3.Text= "";
            DropDownList4.Text = "";
            DropDownList5.Text = "";
            Text10.Value = "";
            Text11.Value = "";
            Text12.Value = "";
            Text13.Value = "";
            Text14.Value = "";
            DropDownList6.Text = "";
            DropDownList7.Text = "";
            DropDownList8.Text = "";
            DropDownList9.Text = "";
            Text15.Value = "";
            DropDownList10.Text = "NO";
            DropDownList11.Text = "NO";
            DropDownList12.Text = "";
            DropDownList13.Text = "";
            DropDownList14.Text = "";
            DropDownList15.Text = "NO";
            DropDownList16.Text = "NO";
            Text16.Value = "";
            DropDownList17.Text = "";
            DropDownList18.Text = "";
            Text17.Value = "";
            DropDownList19.Text = "";
            DropDownList20.Text = "";
            Text18.Value = "";
            DropDownList21.Text = "";
            DropDownList22.Text = "NO";
            DropDownList23.Text = "";
            Text20.Value = "";
            DropDownList24.Text  = "飞针";
            TextBox1.Text = "";
            DropDownList25.Text = "正常";
            DropDownList26.Text = "";
            DropDownList27.Text = "";
            Text19.Value = "";
            Text21.Value = "";
        }
        #endregion
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

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
    


          
        }
        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                try
                {

                    ClearText();
                    Text1.Value = bc.numYM(9, 4, "0001", "SELECT * FROM WAREINFO", "WAREID", "9");

                    /*purchaseunitprice*/
                    string a = bc.numYM(10, 4, "0001", "select * from PurchaseUnitPrice", "PPID", "PP");
                    if (a == "Exceed limited")
                    {

                        hint.Value = "编码超出限制！";
                    }
                    else
                    {
                        x2.Value = a;

                    }
                    /*purchaseunitprice*/

                    /*sellunitprice*/
                    string a1 = bc.numYM(10, 4, "0001", "select * from SellUnitPrice", "SPID", "SP");
                    if (a1 == "Exceed limited")
                    {

                        hint.Value = "编码超出限制！";
                    }
                    else
                    {
                        x3.Value = a1;

                    }
                    /*sellunitprice*/
                    Bind();
                }
                catch (Exception)
                {
                }
            }
            else if (submit.ID == "Submit2")
            {
                save(false);
                try
                {

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../BaseInfo/WareInfo.aspx" + n2);
            }
            else if (submit.ID == "Submit4")
            {
                oldid = Text1.Value;
                string var1 = bc.numYM(9, 4, "0001", "SELECT * FROM WareINFO", "WAREID", "9");
                Text1.Value = var1;

                /*purchaseunitprice*/
                string a = bc.numYM(10, 4, "0001", "select * from PurchaseUnitPrice", "PPID", "PP");
                if (a == "Exceed limited")
                {

                    hint.Value = "编码超出限制！";
                }
                else
                {
                    x2.Value = a;

                }
                /*purchaseunitprice*/

                /*sellunitprice*/
                string a1 = bc.numYM(10, 4, "0001", "select * from SellUnitPrice", "SPID", "SP");
                if (a1 == "Exceed limited")
                {

                    hint.Value = "编码超出限制！";
                }
                else
                {
                    x3.Value = a1;

                }
                save(true);

                dt = bc.getdt("select * from WareFile where WareID='" + oldid + "'");
                sqb = new StringBuilder();
                string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
                string year, month, day;
                year = DateTime.Now.ToString("yy");
                month = DateTime.Now.ToString("MM");
                day = DateTime.Now.ToString("dd");
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            string v1 = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WAREFILE", "FLKEY", "FL");
                            basec.getcoms(@"INSERT INTO WAREFILE(FLKEY,WAREID,OLDFILENAME,PATH,DATE,YEAR,MONTH,DAY) VALUES 
('" + v1 + "','" + Text1.Value + "','" + dr["oldfilename"].ToString() + "','" + dr["path"].ToString() + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
                        }

                    }
                    IFExecution_SUCCESS = true;
                }
                catch (Exception)
                {
                    IFExecution_SUCCESS = false;

                }

                if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
                {
                    hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
                }
                else
                {
                    hint.Value = "";
                }
                try
                {

                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit5")
            {

            }
            else if (submit.ID == "Submit6")
            {

            }
        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

       
        }
        protected void save(bool ifcopy)
        {
            hint.Value = "";
            string sql;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            if (ac1() == 0)
            {
                return;
            }
           else  if (!bc.exists("SELECT WAREID FROM WAREINFO WHERE WAREID='" + Text1.Value + "'"))
            {
               
                SqlConnection sqlcon = bc.getcon();
                 sql = @"INSERT INTO WAREINFO(
WAREID,
CO_WAREID,
WNAME,
CWAREID,
SPEC,
CUID,
LINEA_PN,
LINEB_PN,
LINEC_PN,
LINED_PN,
PLANK_TYPE,
PLANK_THICKNESS,
PLANK_TOLERANCE,
PANEL,
PCS_LEN,
PCS_WIDTH,
SET_LEN,
SET_WIDTH,
SET_COMPOSING,
SURFACE_TREATMENT,
SURFACE_THICKNESS,
SOLDER_MASK,
CHARACTER_COLOR,
PANEL_NEED,
IMPEDANCE,
ASSIGN_STACKUP,
CORE_COPPER,
OUT_COPPER,
CIRCUIT_SPEC,
THICKNESS_COPPER,
BGA_DESIGN,
BGA_PAD,
COPPER_NEED,
MINIMUM_HOLE,
PCS_COUNT,
MOLDING_STYLE,
MOLDING_TOLERANCE,
VCUT_SET,
VCUT_ANGLE,
VCUT_DISABLED,
IF_HYPOTENUSE,
HYPOTENUSE_ANGLE,
DEPTH_NEED,
TEST_STYLE,
REMARK,
DATE,
MAKERID,
YEAR,
ACTIVE,
MONTH,
BIGANDSMALL_PANEL,
TERMINAL
,flid
)
VALUES 
(
@WAREID,
@CO_WAREID,
@WNAME,
@CWAREID,
@SPEC,
@CUID,
@LINEA_PN,
@LINEB_PN,
@LINEC_PN,
@LINED_PN,
@PLANK_TYPE,
@PLANK_THICKNESS,
@PLANK_TOLERANCE,
@PANEL,
@PCS_LEN,
@PCS_WIDTH,
@SET_LEN,
@SET_WIDTH,
@SET_COMPOSING,
@SURFACE_TREATMENT,
@SURFACE_THICKNESS,
@SOLDER_MASK,
@CHARACTER_COLOR,
@PANEL_NEED,
@IMPEDANCE,
@ASSIGN_STACKUP,
@CORE_COPPER,
@OUT_COPPER,
@CIRCUIT_SPEC,
@THICKNESS_COPPER,
@BGA_DESIGN,
@BGA_PAD,
@COPPER_NEED,
@MINIMUM_HOLE,
@PCS_COUNT,
@MOLDING_STYLE,
@MOLDING_TOLERANCE,
@VCUT_SET,
@VCUT_ANGLE,
@VCUT_DISABLED,
@IF_HYPOTENUSE,
@HYPOTENUSE_ANGLE,
@DEPTH_NEED,
@TEST_STYLE,
@REMARK,
@DATE,
@MAKERID,
@YEAR,
@ACTIVE,
@MONTH,
@BIGANDSMALL_PANEL,
@TERMINAL,
@flid
)

";
                SQlcommandE(sql);
                if (DropDownList25.Text == "正常")
                {
                /*purchaseunitprice 1/1*/
               
                    if (!bc.exists("SELECT * FROM PURCHASEUNITPRICE WHERE PPID='" + x2.Value + "'"))
                    {
                        basec.getcoms(@"insert into PurchaseUnitPrice(PPID,WAREID,MakerID,
Date,Year,Month) values('" + x2.Value + "','" + Text1.Value + "','" + varMakerID + "', '" + varDate +
             "','" + year + "','" + month + "')");
                    }
                    /*purchaseunitprice 1/1*/

                    /*sellunitprice 1/1*/

                    if (!bc.exists("SELECT * FROM SELLUNITPRICE WHERE SPID='" + x3.Value + "'"))
                    {
                        basec.getcoms(@"insert into SELLUNITPRICE(SPID,WAREID,MakerID,
Date,Year,Month) values('" + x3.Value + "','" + Text1.Value + "','" + varMakerID + "', '" + varDate +
             "','" + year + "','" + month + "')");
                    }
                    /*sellunitprice 1/1*/
                }
                IFExecution_SUCCESS = true;
            }
            else
            {
                SqlConnection sqlcon = bc.getcon();
                sql = @"UPDATE WAREINFO SET 

CO_WAREID=@CO_WAREID,
WNAME=@WNAME,
CWAREID=@CWAREID,
SPEC=@SPEC,
CUID=@CUID,
LINEA_PN=@LINEA_PN,
LINEB_PN=@LINEB_PN,
LINEC_PN=@LINEC_PN,
LINED_PN=@LINED_PN,
PLANK_TYPE=@PLANK_TYPE,
PLANK_THICKNESS=@PLANK_THICKNESS,
PLANK_TOLERANCE=@PLANK_TOLERANCE,
PANEL=@PANEL,
PCS_LEN=@PCS_LEN,
PCS_WIDTH=@PCS_WIDTH,
SET_LEN=@SET_LEN,
SET_WIDTH=@SET_WIDTH,
SET_COMPOSING=@SET_COMPOSING,
SURFACE_TREATMENT=@SURFACE_TREATMENT,
SURFACE_THICKNESS=@SURFACE_THICKNESS,
SOLDER_MASK=@SOLDER_MASK,
CHARACTER_COLOR=@CHARACTER_COLOR,
PANEL_NEED=@PANEL_NEED,
IMPEDANCE=@IMPEDANCE,
ASSIGN_STACKUP=@ASSIGN_STACKUP,
CORE_COPPER=@CORE_COPPER,
OUT_COPPER=@OUT_COPPER,
CIRCUIT_SPEC=@CIRCUIT_SPEC,
THICKNESS_COPPER=@THICKNESS_COPPER,
BGA_DESIGN=@BGA_DESIGN,
BGA_PAD=@BGA_PAD,
COPPER_NEED=@COPPER_NEED,
MINIMUM_HOLE=@MINIMUM_HOLE,
PCS_COUNT=@PCS_COUNT,
MOLDING_STYLE=@MOLDING_STYLE,
MOLDING_TOLERANCE=@MOLDING_TOLERANCE,
VCUT_SET=@VCUT_SET,
VCUT_ANGLE=@VCUT_ANGLE,
VCUT_DISABLED=@VCUT_DISABLED,
IF_HYPOTENUSE=@IF_HYPOTENUSE,
HYPOTENUSE_ANGLE=@HYPOTENUSE_ANGLE,
DEPTH_NEED=@DEPTH_NEED,
TEST_STYLE=@TEST_STYLE,
REMARK=@REMARK,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
ACTIVE=@ACTIVE, 
BIGANDSMALL_PANEL=@BIGANDSMALL_PANEL,
TERMINAL=@TERMINAL,
flid=@flid
WHERE WAREID='" + Text1.Value +"'";
                SQlcommandE(sql);
                IFExecution_SUCCESS = true;
            }
            if(ifcopy==false ) 
            {
                if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
                {
                    hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
                }
                else
                {
                    hint.Value = "";
                }
            }
          
           
        }
        #region ac1()
        private int ac1()
        {

            int x = 1;
            if (Text3.Value == "")
            {
                x = 0;
                hint.Value = "品名不能为空！";
            }
            else  if (Text5.Value == "")
            {
                x = 0;
                hint.Value = "该客户名称不能为空！";
            }
            else if (!bc.exists("select * from customerinfo_MST where cname='" + Text5.Value + "'"))
                {
                    x = 0;
                    hint.Value = "该客户名称不存在于系统中！";

                }
            else if (bc.yesno(Text10.Value ) ==0)
            {
                x = 0;
                hint.Value = "该属性只能输入数字！";

            }
  
            else if (bc.yesno(Text11.Value) ==0)
            {
                x = 0;
                hint.Value = "该属性只能输入数字！";

            }

            else if (Text12.Value == "")
            {

                x = 0;
                hint.Value = "set长不能为空！";

            }
            else if (Text13.Value == "")
            {

                x = 0;
                hint.Value = "set宽不能为空！";

            }
            else if (Text14.Value == "")
            {

                x = 0;
                hint.Value = "set排版数不能为空！";

            }
            else if (bc.yesno(Text12.Value) == 0)
            {
                x = 0;
                hint.Value = "该属性只能输入数字！";

            }
            else if (bc.yesno(Text13.Value) == 0)
            {
                x = 0;
                hint.Value = "该属性只能输入数字！";

            }
            else if (bc.yesno(Text14.Value) == 0)
            {
                x = 0;
                hint.Value = "该属性只能输入数字！";

            }
     
            else if (bc.yesno(Text17.Value) == 0)
            {
                x = 0;
                hint.Value = "PCS 内孔数只能输入数字！";

            }
     
            else if (bc.yesno(Text18.Value) == 0)
            {
                x = 0;
                hint.Value = " V-cut set刀数只能输入数字！";

            }
            return x;

        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

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
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = Text1.Value;
            sqlcom.Parameters.Add("@CO_WAREID", SqlDbType.VarChar, 100).Value = Text2.Value;
            sqlcom.Parameters.Add("@WNAME", SqlDbType.VarChar, 200).Value = Text3.Value;
            sqlcom.Parameters.Add("@CWAREID", SqlDbType.VarChar, 200).Value = Text4.Value;
            sqlcom.Parameters.Add("@SPEC", SqlDbType.VarChar, 20).Value = DropDownList1.Text;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='" + Text5.Value + "'");
            sqlcom.Parameters.Add("@LINEA_PN", SqlDbType.VarChar, 20).Value = Text6.Value;
            sqlcom.Parameters.Add("@LINEB_PN", SqlDbType.VarChar, 20).Value = Text7.Value;
            sqlcom.Parameters.Add("@LINEC_PN", SqlDbType.VarChar, 20).Value = Text8.Value;
            sqlcom.Parameters.Add("@LINED_PN", SqlDbType.VarChar, 20).Value = Text9.Value;
            sqlcom.Parameters.Add("@PLANK_TYPE", SqlDbType.VarChar, 20).Value = DropDownList2.Text;
            sqlcom.Parameters.Add("@PLANK_THICKNESS", SqlDbType.VarChar, 20).Value = DropDownList3.Text;
            sqlcom.Parameters.Add("@PLANK_TOLERANCE", SqlDbType.VarChar, 20).Value = DropDownList4.Text;
            sqlcom.Parameters.Add("@PANEL", SqlDbType.VarChar, 20).Value = DropDownList5.Text;
            sqlcom.Parameters.Add("@PCS_LEN", SqlDbType.VarChar, 20).Value = Text10.Value;
            sqlcom.Parameters.Add("@PCS_WIDTH", SqlDbType.VarChar, 20).Value = Text11.Value;
            sqlcom.Parameters.Add("@SET_LEN", SqlDbType.VarChar, 20).Value = Text12.Value;
            sqlcom.Parameters.Add("@SET_WIDTH", SqlDbType.VarChar, 20).Value = Text13.Value;
            sqlcom.Parameters.Add("@SET_COMPOSING", SqlDbType.VarChar, 20).Value = Text14.Value;
            sqlcom.Parameters.Add("@SURFACE_TREATMENT", SqlDbType.VarChar, 20).Value = DropDownList6.Text;
            sqlcom.Parameters.Add("@SURFACE_THICKNESS", SqlDbType.VarChar, 20).Value = DropDownList7.Text;
            sqlcom.Parameters.Add("@SOLDER_MASK", SqlDbType.VarChar, 20).Value = DropDownList8.Text;
            sqlcom.Parameters.Add("@CHARACTER_COLOR", SqlDbType.VarChar, 20).Value = DropDownList9.Text;
            sqlcom.Parameters.Add("@PANEL_NEED", SqlDbType.VarChar, 20).Value = Text15.Value;
            sqlcom.Parameters.Add("@IMPEDANCE", SqlDbType.VarChar, 20).Value = DropDownList10.Text;
            sqlcom.Parameters.Add("@ASSIGN_STACKUP", SqlDbType.VarChar, 20).Value = DropDownList11.Text;
            sqlcom.Parameters.Add("@CORE_COPPER", SqlDbType.VarChar, 20).Value = DropDownList12.Text;
            sqlcom.Parameters.Add("@OUT_COPPER", SqlDbType.VarChar, 20).Value = DropDownList13.Text;
            sqlcom.Parameters.Add("@CIRCUIT_SPEC", SqlDbType.VarChar, 20).Value = DropDownList14.Text;
            sqlcom.Parameters.Add("@THICKNESS_COPPER", SqlDbType.VarChar, 20).Value = DropDownList15.Text;
            sqlcom.Parameters.Add("@BGA_DESIGN", SqlDbType.VarChar, 20).Value = DropDownList16.Text;
            sqlcom.Parameters.Add("@BGA_PAD", SqlDbType.VarChar, 20).Value = Text16.Value;
            sqlcom.Parameters.Add("@COPPER_NEED", SqlDbType.VarChar, 20).Value = DropDownList17.Text;
            sqlcom.Parameters.Add("@MINIMUM_HOLE", SqlDbType.VarChar, 20).Value = DropDownList18.Text;
            sqlcom.Parameters.Add("@PCS_COUNT", SqlDbType.VarChar, 20).Value = Text17.Value;
            sqlcom.Parameters.Add("@MOLDING_STYLE", SqlDbType.VarChar, 20).Value = DropDownList19.Text;
            sqlcom.Parameters.Add("@MOLDING_TOLERANCE", SqlDbType.VarChar, 20).Value = DropDownList20.Text;
            sqlcom.Parameters.Add("@VCUT_SET", SqlDbType.VarChar, 20).Value = Text18.Value;
            sqlcom.Parameters.Add("@VCUT_ANGLE", SqlDbType.VarChar, 20).Value = DropDownList21.Text;
            sqlcom.Parameters.Add("@VCUT_DISABLED", SqlDbType.VarChar, 20).Value = DropDownList27.Text;
            sqlcom.Parameters.Add("@IF_HYPOTENUSE", SqlDbType.VarChar, 20).Value = DropDownList22.Text;
            sqlcom.Parameters.Add("@HYPOTENUSE_ANGLE", SqlDbType.VarChar, 20).Value = DropDownList23.Text;
            sqlcom.Parameters.Add("@DEPTH_NEED", SqlDbType.VarChar, 20).Value = Text20.Value;
            sqlcom.Parameters.Add("@TEST_STYLE", SqlDbType.VarChar, 20).Value = DropDownList24.Text;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = TextBox1.Text;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = varMakerID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@BIGANDSMALL_PANEL", SqlDbType.VarChar, 20).Value = DropDownList26.Text;
            sqlcom.Parameters.Add("@TERMINAL", SqlDbType.VarChar, 1000).Value = Text19.Value;
            if (DropDownList25.Text == "正常")
            {
                sqlcom.Parameters.Add("@ACTIVE", SqlDbType.VarChar, 20).Value = "Y";
            }
            else if (DropDownList25.Text == "Hold")
            {
                sqlcom.Parameters.Add("@ACTIVE", SqlDbType.VarChar, 20).Value = "HOLD";
            }
            else
            {
                sqlcom.Parameters.Add("@ACTIVE", SqlDbType.VarChar, 20).Value = "N";

            }
            sqlcom.Parameters.Add("@flid", SqlDbType.VarChar, 20).Value = bc.getOnlyString("select flid from flow_mst where flow_name='"+Text21.Value +"'");
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion

        protected void btnReconcile_Click(object sender, EventArgs e)
        {

        }
        protected void reconcile()
        {


           
        }
        private void edit()
        {



           


        }

      }
}
