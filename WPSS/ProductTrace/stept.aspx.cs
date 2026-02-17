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
using System.IO;


namespace WPSS.ProductTrace
{
    public partial class stept : System.Web.UI.Page
    {
        DataTable dt ;//订单主表
        DataTable dtx1;
        basec bc = new basec();
        CORDER corder = new CORDER();
        StringBuilder sqb;
        public string utid { set; get; }
        public string step_name { set; get; }
        public string file_name { set; get; }

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
        public string STID { set; get; }
        DataTable dto = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Text1.Focus();
                Label2.Text = "添加工序";
                Title = "Xizhe ERP";
                try
                {
                    if (Request.QueryString["id"].ToString() != null)
                    {
                        id.Value = Request.QueryString["id"].ToString();//表示修改用户修改信息
                    }
                    Label2.Text = "修改工序";

                }
                catch (Exception)
                {

                }
                Bind();
               
            }
            if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
            {
            
                try
                {
                
                 
                }
                catch (Exception)
                {

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
        private DataTable table_inital()
        {
            dt = new DataTable();
            dt.Columns.Add("sn", typeof(string));
            dt.Columns.Add("utid", typeof(string));
            dt.Columns.Add("utable_name", typeof(string));
            return dt;
        }
        #region dtx
        protected DataTable dtx()
        {
            int i;
            dt = table_inital();
            if (GridView1.Rows.Count > 0)
            {
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sn"] = (i + 1).ToString();
                
                    string v1 = ((Label)GridView1.Rows[i].Cells[0].FindControl("L1")).Text;
                    dr["utable_name"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                    dr["utid"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
                    dt.Rows.Add(dr);

                }
                DataRow dr1 = dt.NewRow();
                dr1["sn"] = GridView1.Rows.Count + 1;
             
                dt.Rows.Add(dr1);
            }
            else
            {
                for (i = 1; i <= 10; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sn"] = i;
                  
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion
        #region dtx
        protected DataTable dtx(string select)
        {
            int i;
            DataTable dt = table_inital();

            if (GridView1.Rows.Count > 0)
            {
                int j = 1;
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (select != "" && Convert.ToInt32(select) - 1 == i)//select为选中要删除的行
                    {
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["sn"] = j;
                        string v1 = ((Label)GridView1.Rows[i].Cells[0].FindControl("L1")).Text;
                        dr["utable_name"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                     
                        dr["utid"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
                        dt.Rows.Add(dr);
                        j++;
                    }
                }

            }

            return dt;
        }
        #endregion
     

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;

            if (submit.Value == "提交")
            {
                if (juage())
                {
                    
                }
                else
                { 
                    save();

                }
               
                //save();
                try
                {
                 
                }
                catch (Exception ex)
                {
                    prompt.Value = ex.Message;
                }
            }
           
            else if (submit.Value == "添加")
            {
                
                GridView1.DataSource = dtx();
                GridView1.DataBind();
            }
            else if (submit.Value == "添加附加费用")
            {
               
            }
           
           
            else if (submit.ID  == "Submit31")
            {

                Response.Redirect("/producttrace/step.aspx");

            }
     

            else if (submit.Value == "上传")
            {

                
            }
            else if (submit.Value == "上一页")
            {
              
                Response.Write("<script language=javascript>history.go(-2);</script>");
               
            }
            else if (submit.Value =="返回PI管理")
            {
              
            }
            else if (submit.Value == "提示修改附加成本成功")
            {
                Bind();
                prompt1.Value = "修改附加成本成功";
                prompt1.Visible = true;
            }
            else if (submit.Value == "提示修改发票成功")
            {
                Bind();
                prompt1.Value = "修改发票成功";
                prompt1.Visible = true;
            }
          
             
        }
     
        protected void Bind()
        {
            prompt1.Visible = false;//prompt1用于在本作业中删除发票或删除采购单或修改采购单时，若单据已锁定提示已锁定
            prompt1.Value = "";
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
               prompt.Value= bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            sqb = new StringBuilder();
            if (id.Value != "")
            {
                dt = bc.getdt(@"
select *,c.utable_name as step_name from step_mst a 
left join step_det b on a.stid=b.stid 
left join utable_mst c on b.utid=c.utid   where a.stid='" + id.Value + "'");
                if (dt.Rows.Count > 0)
                {
                    Text1.Value = dt.Rows[0]["step_name"].ToString();
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

               
            }
            else
            {
             //初始化产品视窗
                dt = table_inital();
                for (int i = 1; i <= 10; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sn"] = i;
                    dt.Rows.Add(dr);
                }
             GridView1.DataSource = dt;
             GridView1.DataBind();
             //初始化费用视窗

             dtx1 = new DataTable();
             dtx1.Columns.Add("sn", typeof(string));
             dtx1.Columns.Add("csn", typeof(string));
             dtx1.Columns.Add("file_name", typeof(string));
             for (int i = 1; i <= 3; i++)
             {
                 DataRow dr1 = dtx1.NewRow();
                 dr1["sn"] = i.ToString();
                 dr1["csn"] = i.ToString();
                 dtx1.Rows.Add(dr1);
             }
            
           }
        }
       
        #region juage()
        private bool juage()
        {
            bool b = false;
            string[] a = { "", "", "","","","" };
            Text1.Style["background-color"] = "#ffffff";
            Text1.Style["color"] = "#595d5a";
            if (Text1.Value == "")
            {
                a[0] = "工序名称不能为空";
                Text1.Style["background-color"] = "#e04c64";
                Text1.Style["color"] = "#ffffff";
                Text1.Focus();
                b = true;
            }
    
            if (juage_if_exist_onlyone() == false)
            {
                a[2] = "至少有一项表格才能保存";
                b = true;
            }
            if (juage_gridview1(""))//判断产品信息
            {
                b = true;
            }
            if (prompt.Value!= "")
            {
                 prompt.Visible = true;
            }
            if (a[0]!= "")
            {
             prompt.Value = a[0];
            }
            else if (a[1] != "")
            {
                prompt.Value = a[1];
            }
            else if (a[2]!= "")
            {
                prompt.Value = a[2];
            }
            return b;
        }
        #endregion
        #region juage_gridview1()
        private bool juage_gridview1(string add)
        {
            bool b = false;
            string[] a = { "", "", "", "", "", "", "", "", "", "" };
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //颜色初始化使用原来的颜色 start
                ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["background-color"] = "#FFFFFF";
                ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["color"] = "#595d5a";
                //颜色初始化使用原来的颜色 end
                step_name = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                utid = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;

                if (step_name != "")
                {

                    if (utid == "")
                    {
                        b = true;
                        prompt.Value = "没有选择表格ID,单击右侧的选择按扭进行选择！";
                    }
                    if (step_name == "")
                    {
                        b = true;
                        prompt.Value = "表格名称不能为空！";
                        a[0] = prompt.Value;
                        ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["background-color"] = "#e04c64";
                        ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["color"] = "#ffffff";

                    }
                }
                if (add == "add")
                {

                    return b;//新增产品项时不用显示判断内容，只是为了保持之前的背景样式
                }
                if (prompt.Value != "")
                {
                    prompt.Visible = true;
                }
                if (a[0]!= "")
                {
                    prompt.Value = a[0];
                }
            }
            return b;
        }
        #endregion
        #region juage_if_exist_onlyone()
        private bool juage_if_exist_onlyone()
        {
            bool b = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                step_name = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                if (step_name !="")
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        #endregion
        protected void save()
        {
            prompt.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            string v2 = bc.getOnlyString("SELECT step_name FROM step_mst WHERE  stid='" + id.Value + "'");
            sqb = new StringBuilder();
            if (id.Value != "")
            {
                STID = id.Value;
            }
            else
            {
                STID = new basec().numYM_NEW(10, 4, "0001", "step_mst", "stid", "ST");
            }
            if (id.Value == "")
            {

                if (bc.exists("select * from step_mst where step_name='" + Text1.Value + "'"))
                {
                    prompt.Visible = true;
                    prompt.Value = "该工序名称已经存在了！";
                    return;
                }
                else
                {
                    sqb.AppendFormat(";INSERT INTO step_mst");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("stid");
                    sqb.AppendFormat(",step_name");
                    sqb.AppendFormat(",MakerID");
                    sqb.AppendFormat(",MDate");
                    sqb.AppendFormat(",if_delete");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'",STID );
                    sqb.AppendFormat(",'{0}'", Text1.Value);
                    sqb.AppendFormat(",'{0}'", Request.Cookies["cookiename"].Values["usid"].ToString());
                    sqb.AppendFormat(",getdate()");
                    sqb.AppendFormat(",0");
                    sqb.AppendFormat(")");
                }
            }
            else if (v2 != Text1.Value)
            {
                if (bc.exists("select * from step_mst where step_name='" + Text1.Value + "'"))
                {
                    prompt.Visible = true;
                    prompt.Value = "该工序名称已经存在了！";
                    return;
                }
                else
                {
                    sqb.AppendFormat("UPDATE step_mst");
                    sqb.AppendFormat(" SET");
                    sqb.AppendFormat(" step_name='{0}'", Text1.Value);
                    sqb.AppendFormat(" where stid='" + id.Value + "'");
                }
            }
            else
            {
                sqb.AppendFormat("UPDATE step_mst");
                sqb.AppendFormat(" SET");
                sqb.AppendFormat(" step_name='{0}'", Text1.Value);
                sqb.AppendFormat(" where stid='" + id.Value + "'");
            }
          
           
            basec.getcoms(";delete step_det where stid='" + id.Value + "'");
            int j = 1;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                step_name = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                string WAREID = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
                if (step_name != "")//对没有录入表格名的不写入数据库
                {
                    sqb.AppendFormat(";INSERT INTO step_det");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("stid");
                    sqb.AppendFormat(",utid");
                    sqb.AppendFormat(",sn");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'", STID );
                    sqb.AppendFormat(",'{0}'", WAREID);
                    sqb.AppendFormat(",'{0}'", j);
                    sqb.AppendFormat(")");
                    j++;
                }
            }
       
            try
            {
                if (sqb.ToString().Length > 0)
                {
                    basec.getcoms(sqb.ToString());
                }
                IFExecution_SUCCESS = true;
            }
            catch (Exception ex)
            {
                prompt.Value = ex.Message;
                IFExecution_SUCCESS = false;
            }
       
            if (IFExecution_SUCCESS == true && id.Value == "")//添加
            {
                ClearText();
                Bind();
            }
            else if (IFExecution_SUCCESS == true)//修改
            {
                Bind();
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView gridview = (GridView)sender;
            if (gridview.ID == "GridView1")
            {
                string v1 = ((Label)gridview.Rows[gridview.SelectedIndex].Cells[0].FindControl("L1")).Text;
                gridview.DataSource = dtx(v1);
                gridview.DataBind();
            }
      
        
        }

        protected void ClearText()
        {
            Text1.Value = "";
        }

     

        protected void s1_ServerClick()
        {

        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eeeeee',this.style.fontWeight='';");
                //当鼠标离开的时候 将背景颜色还原的以前的颜色 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }
            e.Row.Attributes.Add("style", "height:26px");//这里设置GridView的行高*/
        }

     

     

    }
}
