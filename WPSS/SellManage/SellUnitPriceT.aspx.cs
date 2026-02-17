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

namespace WPSS.SellManage
{
    public partial class SellUnitPriceT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        PrintOfferBill print = new PrintOfferBill();
        string v1, v2;
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
        string sqlt = @"INSERT INTO OFFER(
OFID,
WAREID,
QUANTITY_UNITPRICE,
QUANTITY_COUNT,
SAMPLE_UNITPRICE,
SAMPLE_COUNT,
SMALLQUANTITY_UNITPRICE,
MUCH_PRICE,
UNIT_AREA,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES 
(
@OFID,
@WAREID,
@QUANTITY_UNITPRICE,
@QUANTITY_COUNT,
@SAMPLE_UNITPRICE,
@SAMPLE_COUNT,
@SMALLQUANTITY_UNITPRICE,
@MUCH_PRICE,
@UNIT_AREA,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)

";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {

                    if (!IsPostBack)
                    {
                        Title = "Xizhe ERP";
                        Text1.Value = IDO;
                        bind();
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
        private void bind()
        {
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }

          
                dt = basec.getdts(@"select A.WAREID AS WAREID,B.CO_WAREID AS CO_WAREID,B.WNAME AS WNAME,B.CWAREID AS CWAREID,C.CNAME AS CNAME,
A.SELLUNITPRICE AS SELLUNITPRICE ,A.REMARK AS REMARK,B.[SET_LEN] AS SET长,
B.[SET_WIDTH] as SET宽,
B.[SET_COMPOSING] as SET排版数 from SELLUNITPRICE A
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN CUSTOMERINFO_MST C ON C.CUID=B.CUID
where A.SPID='" + Text1.Value + "'");
                if (dt.Rows.Count > 0)
                {

                    Text2.Value = dt.Rows[0]["WAREID"].ToString();
                    Text3.Value = dt.Rows[0]["CO_WAREID"].ToString();
                    Text4.Value = dt.Rows[0]["WNAME"].ToString();
                    Text5.Value = dt.Rows[0]["CWAREID"].ToString();
                    Text6.Value = dt.Rows[0]["CNAME"].ToString();
                    Text7.Value = dt.Rows[0]["SELLUNITPRICE"].ToString();
                    TextBox1.Text = dt.Rows[0]["REMARK"].ToString();
                    Text16.Value = dt.Rows[0]["SET长"].ToString();
                    Text17.Value = dt.Rows[0]["SET宽"].ToString();
                    Text18.Value = dt.Rows[0]["SET排版数"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SELLUNITPRICE"].ToString()))
                    {
                        Text20.Value = (Convert.ToDouble(dt.Rows[0]["SELLUNITPRICE"].ToString()) * 1.13).ToString("0.0000");
                    }

                }
                DataTable dt2 = bc.getdt("SELECT * FROM OFFER WHERE WAREID='" + Text2.Value + "' ORDER BY DATE DESC");
                if (dt2.Rows.Count > 0)
                {


                    Text8.Value = dt2.Rows[0]["QUANTITY_UNITPRICE"].ToString();
                    Text9.Value = dt2.Rows[0]["QUANTITY_COUNT"].ToString();
                    Text10.Value = dt2.Rows[0]["SAMPLE_UNITPRICE"].ToString();
                    Text11.Value = dt2.Rows[0]["SAMPLE_COUNT"].ToString();
                    Text12.Value = dt2.Rows[0]["SMALLQUANTITY_UNITPRICE"].ToString();
                    Text13.Value = dt2.Rows[0]["SAMPLE_COUNT"].ToString() + "～" + dt2.Rows[0]["QUANTITY_COUNT"].ToString();
                    Text15.Value = dt2.Rows[0]["OFID"].ToString();
                    Text19.Value = dt2.Rows[0]["MUCH_PRICE"].ToString();
                    Text21.Value = dt2.Rows[0]["UNIT_AREA"].ToString();

                }
                DataTable dt3 = bc.getdt("SELECT B.PROJECT_COST AS PROJECT_COST FROM WAREINFO A LEFT JOIN SPEC B ON A.SPEC=B.SPEC WHERE A.WAREID='" + Text2.Value + "'");
                if (dt3.Rows.Count > 0)
                {

                    Text14.Value = dt3.Rows[0]["PROJECT_COST"].ToString();

                }
                DataTable dtx;
                dtx = bc.getdt("select * from set_showname");
                if (dtx.Rows.Count > 0)
                {
                    Label1.Text = dtx.Rows[0]["co_wareid"].ToString();
                    Label2.Text = dtx.Rows[0]["wname"].ToString();
                    Label3.Text = dtx.Rows[0]["cwareid"].ToString();
                }
        }
        protected void ClearText()
        {
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            TextBox1.Text = "";
            Text8.Value = "";
            Text9.Value = "";
            Text10.Value = "";
            Text11.Value = "";
            Text12.Value = "";
            Text13.Value = "";
            Text14.Value = "";
            Text15.Value = "";
            Text16.Value = "";
            Text17.Value = "";
            Text18.Value = "";
            Text19.Value = "";
            Text20.Value = "";
        }
        #region
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
            v1 = bc.getOnlyString("SELECT WAREID FROM SELLUNITPRICE WHERE  SPID='" + Text1.Value + "'");
            v2 = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='" + Text3.Value + "'");
            string v3 = bc.getOnlyString("SELECT CUID FROM SELLUNITPRICE WHERE  SPID='" + Text1.Value + "'");
            if (!juage1(v2))
            {

            }
            else if (!bc.exists("SELECT SPID FROM SELLUNITPRICE WHERE SPID='" + Text1.Value + "'"))
            {
                if (bc.exists("select * from SELLUNITPRICE where  WAREID='" + Text2.Value + "'"))
                {
                    hint.Value = "此品号客户已经有核价信息！";
                }
                else
                {
                    basec.getcoms(@"insert into SELLUNITPRICE(SPID,WAREID,SELLUNITPRICE,MakerID,
Date,Year,Month,Day,REMARK) values('" + Text1.Value + "','" + Text2.Value + "', '" + Text7.Value + "' ,'" + varMakerID + "','" + varDate +
                      "','" + year + "','" + month + "','" + day + "','" + TextBox1.Text + "')");
                    IFExecution_SUCCESS = true;
                }
            }
            else if (v1 != Text2.Value)
            {
                if (bc.exists("select * from SELLUNITPRICE where  WAREID='" + Text2.Value + "'"))
                {

                    hint.Value = "此品号客户已经有核价信息！";

                }
                else
                {

                    basec.getcoms("UPDATE SELLUNITPRICE SET WAREID='" + Text2.Value + "' ,SELLUNITPRICE='" + Text7.Value +
                        "',MAKERID='" + varMakerID + "',DATE='" + varDate + "',REMARK='" + TextBox1.Text + "' WHERE SPID='" + Text1.Value + "'");
                    IFExecution_SUCCESS = true;
                }
            }
            else
            {
                basec.getcoms("UPDATE SELLUNITPRICE SET WAREID='" + Text2.Value + "' ,SELLUNITPRICE='" + Text7.Value +
                         "',MAKERID='" + varMakerID + "',DATE='" + varDate + "',REMARK='" + TextBox1.Text + "' WHERE SPID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;
            }
        }
        #endregion
        private bool juage1(string v2)
        {
            bool ju = true;

            if (!bc.exists("SELECT * FROM WAREINFO WHERE WAREID='" + Text2.Value + "' AND ACTIVE='Y'"))
            {
                ju = false;
                hint.Value = "该品号不存在于系统或状态不为正常！";
            }

            else if (bc.yesno(Text7.Value) == 0)
            {
                ju = false;
                hint.Value = "单价只能输入数字！";
            }

            return ju;

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

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                add();
            }
            else if (submit.ID == "Submit2")
            {
                hint.Value = "";
                try
                {
                    if (Text20.Value != "" && bc.yesno(Text20.Value) == 0)
                    {

                        hint.Value = "含税单价只能输入数字！";
                    }
                    else if (Text7.Value == "" && Text20.Value != "")
                    {
                        Text7.Value = (Convert.ToDouble(Text20.Value) / 1.13).ToString("0.0000");
                    }
                    if (Text2.Value == "")
                    {
                        hint.Value = "ID不能为空！";
                    }
                    else if (Text7.Value == "")
                    {
                        hint.Value = "未税单价不能为空！";

                    }
                    else if (bc.yesno(Text7.Value) == 0)
                    {
                        hint.Value = "未税单价只能输入数字！";
                    }
                    else
                    {
                        save();

                        if (IFExecution_SUCCESS == true)
                        {
                            bind();

                        }
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
                Response.Redirect("../SELLManage/SELLUNITPRICE.aspx" + n2);
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

            hint.Value = "";
            ClearText();
            string a = bc.numYM(10, 4, "0001", "select * from SELLUNITPRICE", "SPID", "SP");
            if (a == "Exceed limited")
            {

                hint.Value = "编码超出限制！";
            }
            else
            {
                Text1.Value = a;

            }
            bind();
            ADD_OR_UPDATE = "ADD";


        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {

        }

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
        protected void Bind()
        {
            DataList1.DataSource = dtx();
            DataList1.DataBind();
            DataTable dt1 = basec.getdts("SELECT * FROM WAREFILE WHERE WAREID='" + Text1.Value + "'");
            GridView1.DataSource = dt1;
            GridView1.DataKeyNames = new string[] { "FLKEY" };
            GridView1.DataBind();
        }
        protected DataTable dtx()
        {
            dt.Columns.Add("C", typeof(string));
            for (int i = 0; i < 4; i++)
            {
                DataRow dr = dt.NewRow();
                dr["C"] = Convert.ToString(i);
                dt.Rows.Add(dr);
            }
            return dt;
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
        #region juage2
        protected bool juage2(DataTable dt)
        {
            bool b = false;
            if (string.IsNullOrEmpty(dt.Rows[0]["SET长"].ToString()))
            {
                b = true;
                hint.Value = "SET长为空！";

            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SET宽"].ToString()))
            {
                b = true;
                hint.Value = "SET宽为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SET排版数"].ToString()))
            {
                b = true;
                hint.Value = "SET排版数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["PCS内孔数"].ToString()))
            {
                b = true;
                hint.Value = "PCS内孔数为空！";
            }
            else if (bc.yesno(dt.Rows[0]["VCUT_SET刀数"].ToString()) == 0)
            {

                b = true;
                hint.Value = "V-cut set刀数需为数字！";

            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["VCUT_SET刀数"].ToString()))
            {
                b = true;
                hint.Value = "V-cut set刀数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["指定叠构基准"].ToString()))
            {
                b = true;
                hint.Value = "指定叠构基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["BGA设计基准"].ToString()))
            {
                b = true;
                hint.Value = "BGA设计基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["超大小板基准"].ToString()))
            {
                b = true;
                hint.Value = "超大小板基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["文字颜色基准"].ToString()))
            {
                b = true;
                hint.Value = "文字颜色基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["内外线路规格基准"].ToString()))
            {
                b = true;
                hint.Value = "内外线路规格基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["孔铜要求基准"].ToString()))
            {
                b = true;
                hint.Value = "孔铜要求基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["内层成品铜厚基准"].ToString()))
            {
                b = true;
                hint.Value = "内层成品铜厚基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["金手指斜边角度基准"].ToString()))
            {
                b = true;
                hint.Value = "金手指斜边角度基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["金手指斜边基准"].ToString()))
            {
                b = true;
                hint.Value = "金手指斜边基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["阻抗基准"].ToString()))
            {
                b = true;
                hint.Value = "阻抗基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["最小孔基准"].ToString()))
            {
                b = true;
                hint.Value = "最小孔基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["成型方式基准"].ToString()))
            {
                b = true;
                hint.Value = "成型方式基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["成型公差基准"].ToString()))
            {
                b = true;
                hint.Value = "成型公差基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["板材基准"].ToString()))
            {
                b = true;
                hint.Value = "板材基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["板厚公差基准"].ToString()))
            {
                b = true;
                hint.Value = "板厚公差基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["板子类型基准"].ToString()))
            {
                b = true;
                hint.Value = "板子类型基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["防焊颜色基准"].ToString()))
            {
                b = true;
                hint.Value = "防焊颜色基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["铜厚基准"].ToString()))
            {
                b = true;
                hint.Value = "铜厚基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["表面处理厚度基准"].ToString()))
            {
                b = true;
                hint.Value = "表面处理厚度基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["表面处理基准"].ToString()))
            {
                b = true;
                hint.Value = "表面处理基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["厚铜板基准"].ToString()))
            {
                b = true;
                hint.Value = "厚铜板基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["VCUT角度基准"].ToString()))
            {
                b = true;
                hint.Value = "VCUT角度基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["VCUT残厚基准"].ToString()))
            {
                b = true;
                hint.Value = "VCUT残厚基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["外层成品铜厚基准"].ToString()))
            {
                b = true;
                hint.Value = "外层成品铜厚基准为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["客户系数"].ToString()))
            {
                b = true;
                hint.Value = "客户系数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SAMPLE系数"].ToString()))
            {
                b = true;
                hint.Value = "SAMPLE系数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["小量系数"].ToString()))
            {
                b = true;
                hint.Value = "小量系数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["量产系数"].ToString()))
            {
                b = true;
                hint.Value = "量产系数为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["MOQ面积"].ToString()))
            {
                b = true;
                hint.Value = "MOQ面积为空！";
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["SAMPLE面积"].ToString()))
            {
                b = true;
                hint.Value = "SAMPLE面积为空！";
            }

            return b;


        }
        #endregion
        protected void jenerate_unitprice()
        {

            dt = print.ask(Text2.Value);
            if (dt.Rows.Count > 0)
            {

                if (juage2(dt))
                {

                }
                else
                {
                    decimal d1 = decimal.Parse(dt.Rows[0]["SET长"].ToString());
                    decimal d2 = decimal.Parse(dt.Rows[0]["SET宽"].ToString());
                    decimal d3 = decimal.Parse(dt.Rows[0]["SET排版数"].ToString());
                    decimal d4 = decimal.Parse(dt.Rows[0]["PCS内孔数"].ToString());
                    decimal d5 = decimal.Parse(dt.Rows[0]["VCUT_SET刀数"].ToString());
                    decimal d6 = decimal.Parse(dt.Rows[0]["指定叠构基准"].ToString());
                    decimal d7 = decimal.Parse(dt.Rows[0]["BGA设计基准"].ToString());
                    decimal d8 = decimal.Parse(dt.Rows[0]["超大小板基准"].ToString());
                    decimal d9 = decimal.Parse(dt.Rows[0]["文字颜色基准"].ToString());
                    decimal d10 = decimal.Parse(dt.Rows[0]["内外线路规格基准"].ToString());
                    decimal d11 = decimal.Parse(dt.Rows[0]["孔铜要求基准"].ToString());
                    decimal d12 = decimal.Parse(dt.Rows[0]["内层成品铜厚基准"].ToString());
                    decimal d13 = decimal.Parse(dt.Rows[0]["金手指斜边角度基准"].ToString());/*附加单价*/
                    decimal d14 = decimal.Parse(dt.Rows[0]["金手指斜边基准"].ToString());
                    decimal d15 = decimal.Parse(dt.Rows[0]["阻抗基准"].ToString());
                    decimal d16 = decimal.Parse(dt.Rows[0]["最小孔基准"].ToString());/*附加单价*/
                    decimal d17 = decimal.Parse(dt.Rows[0]["成型方式基准"].ToString());
                    decimal d18 = decimal.Parse(dt.Rows[0]["成型公差基准"].ToString());
                    decimal d19 = decimal.Parse(dt.Rows[0]["板材基准"].ToString());
                    decimal d20 = decimal.Parse(dt.Rows[0]["板厚基准"].ToString());
                    decimal d21 = decimal.Parse(dt.Rows[0]["板厚公差基准"].ToString());
                    decimal d22 = decimal.Parse(dt.Rows[0]["板子类型基准"].ToString());
                    decimal d23 = decimal.Parse(dt.Rows[0]["防焊颜色基准"].ToString());
                    decimal d24 = decimal.Parse(dt.Rows[0]["铜厚基准"].ToString());/*面积单价*/
                    decimal d25 = decimal.Parse(dt.Rows[0]["表面处理厚度基准"].ToString());
                    decimal d26 = decimal.Parse(dt.Rows[0]["表面处理基准"].ToString());/*面积单价*/
                    decimal d27 = decimal.Parse(dt.Rows[0]["厚铜板基准"].ToString());
                    decimal d28 = decimal.Parse(dt.Rows[0]["VCUT角度基准"].ToString());/*附加单价*/
                    decimal d29 = decimal.Parse(dt.Rows[0]["VCUT残厚基准"].ToString());/*附加单价*/
                    decimal d30 = decimal.Parse(dt.Rows[0]["外层成品铜厚基准"].ToString());

                    decimal d31 = d1 * d2 / 1000000 / d3 * (d24 + d26) * d6 * d7 * d8 * d9 * d10 * d11 * d12 * d14 * d15 * d17 * d18 *
                        d19 * d20 * d21 * d22 * d23 * d25 * d27 * d30 + d13 + d4 / 1000 * d16 + d5 / d3 * d28 + d5 / d3 * d29;  /*通用单价*/

                    decimal d32 = decimal.Parse(dt.Rows[0]["客户系数"].ToString());
                    decimal d33 = decimal.Parse(dt.Rows[0]["SAMPLE系数"].ToString());
                    decimal d34 = decimal.Parse(dt.Rows[0]["小量系数"].ToString());
                    decimal d35 = decimal.Parse(dt.Rows[0]["量产系数"].ToString());
                    decimal d36 = decimal.Parse(dt.Rows[0]["MOQ面积"].ToString());
                    decimal d37 = decimal.Parse(dt.Rows[0]["SAMPLE面积"].ToString());
                    decimal d38 = d31 * d32 * d35;/*量产单价*/

                    decimal d391 = d36 * 1000000 / (d1 * d2);
                    string v1 = d391.ToString("#0");
                    decimal d392 = decimal.Parse(v1);
                    decimal d39 = d392* d3; /*量产数量需取整数*/
                    decimal d40 = d31 * d32 * d33; /*SAMPLE单价*/
                    decimal d393 = d37 * 1000000 / (d1 * d2);
                    string v2 = d393.ToString("#0");
                    decimal d394 = decimal.Parse(v2);
                    decimal d41 = d394 * d3;/*SAMPLE数量需取整数*/
                    decimal d42 = d31 * d32 * d34;/*小量单价*/
                    decimal d43 = (d3 * d38 / d1 / d2) * 100;
                    Text8.Value = d38.ToString("#0.0000");
                    Text9.Value = d39.ToString("#0");
                    Text10.Value = d40.ToString("#0.0000");
                    Text11.Value = d41.ToString("#0");
                    Text12.Value = d42.ToString("#0.0000");
                    Text13.Value = d41.ToString("#0") + "～" + d39.ToString("#0");
                    Text14.Value = dt.Rows[0]["工程费"].ToString();
                    Text19.Value = d43.ToString("#0.0000");/*批量报价*/
                    string a = bc.numYMD(11, 4, "0001", "select * from OFFER", "OFID", "Q");
                    Text15.Value = a;
                    decimal d44 = 0;
                    decimal d45 = 0;
                    if (Text7.Value != "")
                    {
                        d44 = decimal.Parse(Text7.Value);
                    }
                    d45 = (d44 * d3 / d1 / d2)*100;
                    Text21.Value = d45.ToString("#0.0000");/*单位面积报价*/
                    if (a == "Exceed limited")
                    {

                        hint.Value = "编码超出限制！";
                    }
                    else
                    {
                        SqlConnection sqlcon = bc.getcon();
                        SQlcommandE(sqlt, a);

                    }
                }

            }


        }

        #region SQlcommandE
        protected void SQlcommandE(string sql, string a)
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
            sqlcom.Parameters.Add("@OFID", SqlDbType.VarChar, 20).Value = a;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = Text2.Value;
            sqlcom.Parameters.Add("@QUANTITY_UNITPRICE", SqlDbType.VarChar, 20).Value = Text8.Value;
            sqlcom.Parameters.Add("@QUANTITY_COUNT", SqlDbType.VarChar, 20).Value = Text9.Value;
            sqlcom.Parameters.Add("@SAMPLE_UNITPRICE", SqlDbType.VarChar, 20).Value = Text10.Value;
            sqlcom.Parameters.Add("@SAMPLE_COUNT", SqlDbType.VarChar, 20).Value = Text11.Value;
            sqlcom.Parameters.Add("@SMALLQUANTITY_UNITPRICE", SqlDbType.VarChar, 20).Value = Text12.Value;
            sqlcom.Parameters.Add("@MUCH_PRICE", SqlDbType.VarChar, 20).Value = Text19.Value;
            sqlcom.Parameters.Add("@UNIT_AREA", SqlDbType.VarChar, 20).Value = Text21.Value;
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

        protected void GenerateUNITPRICE_Click(object sender, EventArgs e)
        {
            hint.Value = "";
            try
            {
                jenerate_unitprice();
            }
            catch (Exception)
            {



            }

        }

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            string vard1 = Text15.Value;
            String[] Carstr = new string[] { vard1 };
        
            Response.Redirect("../ReportManage/CRVPrintBill.aspx");
            //excelprint();
        }

    }
}
