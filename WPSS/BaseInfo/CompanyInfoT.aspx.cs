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
    public partial class CompanyInfoT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        int i;
        string v1, v2;
        int k;
        string c1, c2, c3, c4, c5, c6;
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
        StringBuilder sqb;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {
                bindo();

            }
            catch (Exception)
            {


            }
        }
        protected void bindo()
        {

            if (!IsPostBack)
            {
                //ViewState["pageindex"] = "0";
                Text1.Value = IDO;


                at(@"
select A.CONAME,B.CONTACT,B.PHONE,B.FAX,B.POSTCODE,B.EMAIL,B.ADDRESS,
case when if_default=1 then '已默认' else '未默认' end as 是否为默认公司 from COMPANYINFO_MST A 
LEFT JOIN COMPANYINFO_DET B ON A.COKEY=B.COKEY where A.COID='" + Text1.Value + "'", 0);


                GridView1.Width = Unit.Percentage(70);
                GridView2.Width = Unit.Percentage(80);
                GridView1.DataSource = dtx();
                GridView1.DataBind();
                bind();

            }

           if (va.returnb() == true)
               Response.Redirect("\\Default.aspx");


        }
        protected void at(string sql, int n)
        {

            dt = basec.getdts(sql);
            if (dt.Rows.Count > 0)
            {
                if (n == 0)
                {
                    Text2.Value = dt.Rows[0]["CONAME"].ToString();
                }
                Text3.Value = dt.Rows[0]["PHONE"].ToString();
                Text4.Value = dt.Rows[0]["FAX"].ToString();
                Text5.Value = dt.Rows[0]["POSTCODE"].ToString();
                Text6.Value = dt.Rows[0]["EMAIL"].ToString();
                Text8.Value = dt.Rows[0]["ADDRESS"].ToString();
                Text7.Value = dt.Rows[0]["CONTACT"].ToString();

                if (dt.Rows[0]["是否为默认公司"].ToString() == "已默认")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
            }


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
            DataTable dt1 = basec.getdts("SELECT case when if_default_contact=1 then '已默认' else '未默认' end 是否是默认联系人 ,* FROM COMPANYINFO_DET WHERE COID='" + Text1.Value + "'");

            GridView2.DataSource = dt1;
            GridView2.DataKeyNames = new string[] { "COKEY" };
            GridView2.DataBind();




            GridView1.DataSource = dtx();
            GridView1.DataBind();

        }
        #region dtx
        protected DataTable dtx()
        {

            DataTable dt4 = new DataTable();
            dt4.Columns.Add("项次", typeof(string));
            dt4.Columns.Add("联系人", typeof(string));
            dt4.Columns.Add("联系电话", typeof(string));
            dt4.Columns.Add("传真号码", typeof(string));
            dt4.Columns.Add("邮政编码", typeof(string));
            dt4.Columns.Add("EMAIL", typeof(string));
            dt4.Columns.Add("地址", typeof(string));
            for (i = 1; i <= 4; i++)
            {
                DataRow dr = dt4.NewRow();
                dr["项次"] = i;
                dt4.Rows.Add(dr);

            }


            return dt4;
        }
        #endregion

        protected void ClearText()
        {
            Text1.Value = "";
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            Text8.Value = "";


        }
        #region save
        protected void save()
        {
            hint.Value = "";
            IFExecution_SUCCESS = false;
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            v2 = bc.getOnlyString("SELECT CONAME FROM COMPANYINFO_MST WHERE  COID='" + Text1.Value + "'");
            if (Text2.Value == "")
            {
                hint.Value = "公司名称不能为空！";
            }
            else if (!bc.exists("SELECT COID FROM COMPANYINFO_MST WHERE COID='" + Text1.Value + "'"))
            {
                if (bc.exists("select * from COMPANYINFO_MST where CONAME='" + Text2.Value + "'"))
                {

                    hint.Value = "该公司名称已经存在了！";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    basec.getcoms("insert into COMPANYINFO_MST(COID,CONAME,"
              + "COKEY,Date,MakerID,Year,Month,Day) values('" + Text1.Value
              + "','" + Text2.Value + "','" + v1 + "','" + varDate
              + "','" + varMakerID + "','" + year + "','" + month + "','" + day + "')");
                    IFExecution_SUCCESS = true;


                }
            }
            else if (v2!= Text2.Value)
            {
                if (bc.exists("select * from COMPANYINFO_MST where CONAME='" + Text2.Value + "'"))
                {
                    hint.Value = "该公司名称已经存在了！";
                }
                else
                {

                    basec.getcoms("UPDATE COMPANYINFO_MST SET CONAME='" + Text2.Value + "' ,MAKERID='" + varMakerID + "',DATE='" + varDate + "'WHERE COID='" + Text1.Value + "'");
                    IFExecution_SUCCESS = true;
                }

            }
            else
            {
                basec.getcoms("UPDATE COMPANYINFO_MST SET CONAME='" + Text2.Value + "',MAKERID='" + varMakerID + "',DATE='" + varDate + "'WHERE COID='" + Text1.Value + "'");
                IFExecution_SUCCESS = true;

            }
            if (IFExecution_SUCCESS)
            {
                if (CheckBox1.Checked)
                {
                    //因为一个客户下有多家公司，打印时默认公司信息，地址，电话，传真带默认公司的
                    bc.getcom("update companyinfo_mst set if_default=0 where coname<>'" + Text2.Value + "';update companyinfo_mst set if_default=1 where coname='" + Text2.Value + "'");

                }
                else
                {
                    dt = bc.getdt("select * from companyinfo_mst where if_default=1");
                    if (dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        //若只有一家公司且客户没有选择默认，那么系统自动更新为默认
                        bc.getcom("update companyinfo_mst set if_default=1 where coname='" + Text2.Value + "'");

                    }

                }
                //默认主表与从表建立连接找到默认联系人
                sqb = new StringBuilder();
                sqb.AppendFormat(";update companyinfo_mst set cokey=(select top 1 cokey from companyinfo_det where coid='" + Text1.Value + "') where  coid='" + Text1.Value + "'");
                bc.getcom(sqb.ToString());
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

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                string id = GridView2.DataKeys[e.RowIndex][0].ToString();

                if (bc.exists("SELECT * FROM COMPANYINFO_MST WHERE COKEY='" + id + "'"))
                {
                    hint.Value = "该地址为该公司的默认地址，如果要删除改默认地址请指定其它地址为该公司的地址";
                }
                else if (bc.exists("SELECT * FROM PURCHASE_MST WHERE COKEY='" + id + "'"))
                {
                    hint.Value = "该公司联系人信息在采购单中已经存在不允许删除！";

                }
                else
                {

                    string strSql = "DELETE FROM COMPANYINFO_DET WHERE COKEY='" + id + "'";
                    basec.getcoms(strSql);
                    GridView2.EditIndex = -1;
                    bind();

                }
            }
            catch (Exception)
            {


            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {

                Label l1 = (Label)GridView2.Rows[GridView2.SelectedIndex].Cells[0].FindControl("l1");
                sqb = new StringBuilder();
                sqb.AppendFormat("update companyinfo_det set if_default_contact=0 where cokey<>'" + l1.Text + "'");
                sqb.AppendFormat(";update companyinfo_det set if_default_contact=1 where cokey='" + l1.Text + "'");
                sqb.AppendFormat(";update companyinfo_mst set cokey='" + l1.Text + "' where coid='" + Text1.Value + "'");
           
                if (sqb.ToString().Length > 0)
                {
                    bc.getcom(sqb.ToString());
                }

                IFExecution_SUCCESS = true;

            }
            catch (Exception)
            {
                IFExecution_SUCCESS = false;
            }
            if (IFExecution_SUCCESS)
            {
                bind();
            }

        }


        #region addADDRESS
        protected void AddContactInfo()
        {


            hint.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");


            for (k = 0; k < 4; k++)
            {

                c1 = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
                c2 = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
                c3 = ((TextBox)GridView1.Rows[k].Cells[2].FindControl("TextBox3")).Text;
                c4 = ((TextBox)GridView1.Rows[k].Cells[3].FindControl("TextBox4")).Text;
                c5 = ((TextBox)GridView1.Rows[k].Cells[4].FindControl("TextBox5")).Text;
                c6 = ((TextBox)GridView1.Rows[k].Cells[5].FindControl("TextBox6")).Text;

                if (c1!= "")
                {

                    if (juage1(k))
                    {

                        string key = bc.numYMD(20, 12, "000000000001", "SELECT * FROM COMPANYINFO_DET", "COKEY", "CU");
                        basec.getcoms(@"INSERT INTO COMPANYINFO_DET(COKEY,COID,CONTACT,PHONE,FAX,POSTCODE,EMAIL,ADDRESS,
                    MAKERID,DATE,YEAR,MONTH,DAY) VALUES 
                    ('" + key + "','" + Text1.Value + "','" + c1 + "','" + c2 + "','" + c3 + "','" + c4 + "','" + c5 + "','" + c6 + "','" + varMakerID +
                        "','" + varDate + "','" + year + "','" + month + "','" + day + "')");


                    }
                }
            }
            /*如果该公司信息里客户没有指定默认联系人就由系统带第一个人为默认 保证后面打印数据有默认联系人数据 start*/
            dt = bc.getdt("select * from companyinfo_det where coid='" + Text1.Value + "' and if_default_contact=1");
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                sqb = new StringBuilder();
                sqb.AppendFormat("update companyinfo_det set if_default_contact=1 where cokey=(select top 1 cokey from companyinfo_det where coid='" + Text1.Value + "')");
                sqb.AppendFormat(";update companyinfo_mst set cokey=(select top 1 cokey from companyinfo_det where coid='" + Text1.Value + "') where  coid='" + Text1.Value + "'");
                bc.getcom(sqb.ToString());
            }
            /*如果该公司信息里客户没有指定默认联系人就由系统带第一个人为默认 保证后面打印数据有默认联系人数据 end*/
            bind();

        }
        #endregion

        #region juage1()
        private bool juage1(int k)
        {
            string v1 = ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text;
            string v2 = ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text;
            string v3 = ((TextBox)GridView1.Rows[k].Cells[2].FindControl("TextBox3")).Text;
            string v4 = ((TextBox)GridView1.Rows[k].Cells[3].FindControl("TextBox4")).Text;
            string v5 = ((TextBox)GridView1.Rows[k].Cells[4].FindControl("TextBox5")).Text;
            string v6 = ((TextBox)GridView1.Rows[k].Cells[5].FindControl("TextBox6")).Text;

            bool ju = true;
            if (bc.checkphone(v2) == false)
            {
                ju = false;
                hint.Value = "电话号码只能输入数字！";

            }
            else if (bc.checkphone(v3) == false)
            {
                ju = false;
                hint.Value = "传真号码只能输入数字！";

            }
            else if (bc.checkphone(v4) == false)
            {
                ju = false;
                hint.Value = "邮编只能输入数字！";

            }
            //else if (bc.checkEmail(v5) == false)
            //{
            //ju = false;
            //hint.Value = "邮箱地址只能输入数字字母的组合！";

            //}
            else if (v6 == "")
            {

                ju = false;
                hint.Value = "地址不能为空！";

            }

            return ju;

        }
        #endregion

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                add();
            }
            else if (submit.ID == "Submit2")
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
                Response.Redirect("../BaseInfo/COMPANYINFO.aspx" + n2);
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
  
        private void add()
        {

            ClearText();
            Text1.Value = bc.numYM(10, 4, "0001", "SELECT * FROM COMPANYINFO_MST", "COID", "CO");
            ADD_OR_UPDATE = "ADD";
            bind();
        }


  

        protected void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            AddContactInfo();
            try
            {
               
            }
            catch (Exception EX)
            {
                Response.Write(EX.Message);
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            for (k = 0; k < 4; k++)
            {
                ((TextBox)GridView1.Rows[k].Cells[0].FindControl("TextBox1")).Text = "";
                ((TextBox)GridView1.Rows[k].Cells[1].FindControl("TextBox2")).Text = "";
                ((TextBox)GridView1.Rows[k].Cells[2].FindControl("TextBox3")).Text = "";
                ((TextBox)GridView1.Rows[k].Cells[3].FindControl("TextBox4")).Text = "";
                ((TextBox)GridView1.Rows[k].Cells[4].FindControl("TextBox5")).Text = "";
                ((TextBox)GridView1.Rows[k].Cells[5].FindControl("TextBox6")).Text = "";
            }
        }
    }
}
