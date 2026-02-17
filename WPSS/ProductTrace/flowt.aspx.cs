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
    public partial class flowt : System.Web.UI.Page
    {
        DataTable dt ;//订单主表
        DataTable dtx1;
        DataTable dt1;
        basec bc = new basec();
        CORDER corder = new CORDER();
        StringBuilder sqb;
        public string ORKEY { set; get; }
        public string ORID { set; get; }
        public string SN { set; get; }
        public string stid { set; get; }
        public string WNAME { set; get; }
        public string SIZE { set; get; }
        public string COLOR { set; get; }
        public string MATERIAL { set; get; }
        public string OCOUNT { set; get; }
        public string AMOUNT { set; get; }
        public string PRICE { set; get; }
        public string REMARK { set; get; }
        public string YEAR { set; get; }
        public string MONTH { set; get; }
        public string DAY { set; get; }

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
        public string FLID { set; get; }
        DataTable dto = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Text1.Focus();
                Label2.Text = "添加产品工艺";
                Title = "Xizhe ERP";
                try
                {
                    if (Request.QueryString["id"].ToString() != null)
                    {
                        id.Value = Request.QueryString["id"].ToString();//表示修改用户修改信息
                    }
                    Label2.Text = "修改产品工艺";

                }
                catch (Exception)
                {

                }
       
                Bind();
               
            }
            if (Request.Cookies["cookiename"].Values["usid"].ToString()!= null)
            {

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
            dt.Columns.Add("csn", typeof(string));
            dt.Columns.Add("stid", typeof(string));
            dt.Columns.Add("step_name", typeof(string));
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
                    dr["csn"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text2")).Value;
                    string v1 = ((Label)GridView1.Rows[i].Cells[0].FindControl("L1")).Text;
                    dr["step_name"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                    dr["stid"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
                    dt.Rows.Add(dr);  
                      
                }
                DataRow dr1 = dt.NewRow();
                dr1["sn"] = GridView1.Rows.Count + 1;
                dr1["csn"] = GridView1.Rows.Count + 1;
                dt.Rows.Add(dr1);
            }
            else
            {
                for (i = 1; i <=10; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["sn"] = i;
                    dr["csn"] = i;
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
                    if (select!= "" && Convert.ToInt32(select) - 1 == i)//select为选中要删除的行
                    {
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["sn"] = j;
                        string v1 = ((Label)GridView1.Rows[i].Cells[0].FindControl("L1")).Text;
                        dr["step_name"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                        dr["csn"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text2")).Value;
                        dr["stid"] = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
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
         
           
           
            else if (submit.ID  == "Submit31")
            {

                Response.Redirect("/producttrace/flow.aspx");

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
            sqb.AppendFormat(@"
select a.*,b.*,c.* from flow_mst a 
left join flow_det b on a.flid=b.flid 
left join step_mst c on b.stid=c.stid
where a.flid='" + id.Value + "'");
          
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sqb.ToString(), sqlcon);
            sqlcon.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
          
            sqlcon.Close();
            sqlcom.Dispose();
            sqlcon.Dispose();
            if (id.Value != "")
            {
                
                if (dt.Rows.Count > 0)
                {
                    Text1.Value = dt.Rows[0]["flow_name"].ToString();
                  
                    Text5.Value = dt.Rows[0]["version"].ToString();
                    DropDownList1.Text = dt.Rows[0]["active"].ToString();
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
                    dr["csn"] = i;
                    dr["sn"] = i;
                    dt.Rows.Add(dr);
                }
             GridView1.DataSource = dt;
             GridView1.DataBind();
       


             dtx1 = new DataTable();
             dtx1.Columns.Add("sn", typeof(string));
             dtx1.Columns.Add("csn", typeof(string));
             dtx1.Columns.Add("file_name", typeof(string));
             for (int i = 1; i <=3; i++)
             {
                 DataRow dr1 = dtx1.NewRow();
                 dr1["sn"] = i.ToString();
                 dr1["csn"] = i.ToString();
                 dtx1.Rows.Add(dr1);
             }
         
             //初始化费用视窗
             Text5.Value = DateTime.Now.ToString("yyyyMMdd");
           }
        }
       
        #region juage()
        private bool juage()
        {
            bool b = false;
            string[] a = { "", "", "","","","" };
            Text1.Style["background-color"] = "#ffffff";
            Text1.Style["color"] = "#595d5a";
     
            Text5.Style["background-color"] = "#ffffff";
            Text5.Style["color"] = "#595d5a";
            if (Text1.Value == "")
            {
                a[0] = "工艺流程名称不能为空";
                Text1.Style["background-color"] = "#e04c64";
                Text1.Style["color"] = "#ffffff";
                Text1.Focus();
                b = true;
            }
         
            if (Text5.Value == "")
            {
                a[3] = "版本号不能为空";
                b = true;
            }
            if (juage_if_exist_onlyone() == false)
            {
                a[4] = "至少有一项工序才能保存";
                b = true;
            }
            if (juage_gridview1(""))//判断产品信息
            {
                b = true;
            }
            if (prompt.Value != "")
            {
                 prompt.Visible = true;
            }
            if (a[0]!= "")
            {
             prompt.Value = a[0];
            }
            else if (a[1]!= "")
            {
                prompt.Value = a[1];
            }
            else if (a[2]!= "")
            {
                prompt.Value = a[2];
            }
            else if (a[3]!= "")
            {
                prompt.Value = a[3];
            }
            else if (a[4]!= "")
            {
                prompt.Value = a[4];
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
                WNAME = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                stid = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;

                if (WNAME != "")
                {

                    if (stid == "")
                    {
                        b = true;
                        prompt.Value = "没有选择工序ID,单击右侧的选择按扭进行选择！";
                    }
                    if (WNAME == "")
                    {
                        b = true;
                        prompt.Value = "工序名称不能为空！";
                        a[0] = prompt.Value;
                        ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["background-color"] = "#e04c64";
                        ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Style["color"] = "#ffffff";

                    }
                }
                if (add == "add")
                {

                    return b;//新增产品项时不用显示判断内容，只是为了保持之前的背景样式
                }
                if (prompt.Value!= "")
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
                WNAME = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                if (WNAME!="")
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
            string v2 = bc.getOnlyString("SELECT flow_name FROM flow_mst WHERE  flid='" + id.Value + "'");
            string v4 = bc.getOnlyString("SELECT version FROM flow_mst WHERE  flid='" + id.Value + "'");
            string v5 = bc.getOnlyString("SELECT active FROM flow_mst WHERE  flid='" + id.Value + "'");
            sqb = new StringBuilder();
            if (id.Value != "")
            {
                FLID = id.Value;
            }
            else
            {
                FLID = new basec().numYM_NEW(10, 4, "0001", "flow_mst", "flid", "FL");
            }
            if (id.Value == "")
            {
                if (bc.exists("select * from flow_mst where flow_name='" + Text1.Value + "'  and version='"+Text5.Value +"'"))
                {
                    prompt.Visible = true;
                    prompt.Value = "该工艺流程名称+版本号的组合已经存在了！";
                    return;
                }
                else
                {
                    sqb.AppendFormat(";INSERT INTO flow_mst");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("flid");
                    sqb.AppendFormat(",flow_name");
               
                    sqb.AppendFormat(",version");
                    sqb.AppendFormat(",active");
                    sqb.AppendFormat(",MakerID");
                    sqb.AppendFormat(",MDate");
                    sqb.AppendFormat(",if_delete");
             
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'",FLID );
                    sqb.AppendFormat(",'{0}'", Text1.Value);
                
                    sqb.AppendFormat(",'{0}'", Text5.Value);
                    sqb.AppendFormat(",'{0}'", DropDownList1 .Text);
                    sqb.AppendFormat(",'{0}'", Request.Cookies["cookiename"].Values["usid"].ToString());
                    sqb.AppendFormat(",getdate()");
                    sqb.AppendFormat(",0");
                    sqb.AppendFormat(")");
                }
            }
            else if (v2 != Text1.Value  || v4!=Text5.Value  )
            {
                if (bc.exists("select * from flow_mst where flow_name='" + Text1.Value + "'  and version='" + Text5.Value + "'"))
                {
                    prompt.Visible = true;
                    prompt.Value = "该工艺流程名称+版本号的组合已经存在了！";
                    return;
                }
                else if (v5 == "Y" && DropDownList1.Text == "N")//用户要修改生效否
                {
                    prompt.Visible = true;
                    prompt.Value = "一个工艺流程名至少有一个生效版本";
                    return;
                }
                else
                {
                    sqb.AppendFormat("UPDATE flow_mst");
                    sqb.AppendFormat(" SET");
                    sqb.AppendFormat(" flow_name='{0}'", Text1.Value);
             
                    sqb.AppendFormat(" ,version='{0}'", Text5.Value);
                    sqb.AppendFormat(" ,active='{0}'", DropDownList1.Text);
                    sqb.AppendFormat(" where flid='" + id.Value + "'");
                }
            }
            else
            {
                if (v5 == "Y" && DropDownList1.Text == "N")//用户要修改生效否
                {
                    prompt.Visible = true;
                    prompt.Value = "一个工艺流程名至少有一个生效版本";
                    return;
                }
                else
                {
                    sqb.AppendFormat("UPDATE flow_mst");
                    sqb.AppendFormat(" SET");
                    sqb.AppendFormat(" flow_name='{0}'", Text1.Value);
                  
                    sqb.AppendFormat(" ,version='{0}'", Text5.Value);
                    sqb.AppendFormat(" ,active='{0}'", DropDownList1.Text);
                    sqb.AppendFormat(" where flid='" + id.Value + "'");
                }
            }
          
           //写入工序数据 start
            sqb.AppendFormat(";delete flow_det where flid='" + id.Value + "'");
            int j = 1;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                WNAME = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                string  csn = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text2")).Value;
                string WAREID = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text29")).Value;
                if (WNAME != "")//对没有录入表格名的不写入数据库
                {
                    sqb.AppendFormat(";INSERT INTO flow_det");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("flid");
                    sqb.AppendFormat(",stid");
                    sqb.AppendFormat(",sn");
                    sqb.AppendFormat(",csn");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'", FLID );
                    sqb.AppendFormat(",'{0}'", WAREID);
                    sqb.AppendFormat(",'{0}'", j);
                    sqb.AppendFormat(",'{0}'", csn);
                    sqb.AppendFormat(")");
                    j++;
                }
            }
            //写入工序数据 end
    
          
            //如果用户第一次做该工艺流程且没有点生效，为使后面的产品追溯表可用，由系统使其生效 start
            dt = bc.getdt("select * from flow_mst where flow_name='"+Text1.Value+"'");
            if (dt.Rows.Count > 0)
            {

               
            }
            else
            {
                sqb.AppendFormat(";update flow_mst set active='Y' WHERE  FLOW_NAME='" + Text1.Value + "' and version='" + Text5.Value + "'");

            }
            //如果用户第一次做该工艺流程且没有点生效，为使后面的产品追溯表可用，由系统使其生效 end
          
            if (DropDownList1.Text == "Y")//当选择当前做的信息是生效的，要更新该料号的其它流程为非生效，同一料号只有一个流程是生效的
            {
                sqb.AppendFormat(";update flow_mst set active='N' WHERE  FLID<>'"+id.Value +"'");
            }
            try
            {
                if (sqb.ToString().Length > 0)
                {
                    //Response.Write(sqb.ToString());
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
