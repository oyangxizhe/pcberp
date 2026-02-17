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
    public partial class tracet : System.Web.UI.Page
    {
        DataTable dt ;//追溯主表数据
        DataTable dt1;//追溯明细表数据
        DataTable dt2;//追溯文件表单数据
        basec bc = new basec();
        CORDER corder = new CORDER();
        StringBuilder sqb;
        public string stid { set; get; }
        public string WNAME { set; get; }
        public string tableName { set; get; }
        public string parameterName { set; get; }
        public string parameter { set; get; }
        public string unit { set; get; }
        public string stepName { set; get; }
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
        public string TRID { set; get; }
        DataTable dto = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
       
      
            try
            {

                if (Request.Cookies["cookiename"].Values["usid"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        Text1.Focus();
                        Label2.Text = "添加产品追溯表";
                        Title = "Xizhe ERP";
                        try
                        {
                            if (Request.QueryString["id"].ToString() != null)
                            {
                                id.Value = Request.QueryString["id"].ToString();//表示修改用户修改信息
                            }
                            Label2.Text = "修改产品追溯表";


                        }
                        catch (Exception)
                        {

                        }
                        Text1.Value = "自动生成";
                        p1.Visible = false;//默认不加载产品工艺数据显示窗口
                        p2.Visible = false;//默认不加载文件上传窗口
                        p3.Visible = false;//默认不加载文件显示窗口
                        Submit1.Visible = false;//默认不显示提交按扭
                        Submit6.Visible = false;//默认不显示暂存按扭
                    
                        DataTable dtx1;
                        dtx1 = bc.getdt("select * from set_showname");
                        if (dtx1.Rows.Count > 0)
                        {
                            Label3.Text = dtx1.Rows[0]["co_wareid"].ToString();
                            Label4.Text = dtx1.Rows[0]["wname"].ToString();
                            Label5.Text = dtx1.Rows[0]["cwareid"].ToString();
                        }
                    }
                    Bind();
                }
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
            dt.Columns.Add("stid", typeof(string));
            dt.Columns.Add("wname", typeof(string));
            return dt;
        }
  
        protected void submit1_Click(object sender, EventArgs e)
        {
            
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.Value == "暂存")
            {
                
                if (juage())
                {
                   
                }
                else
                {
                   
                    prompt.Value = "";
                    save(submit);

                }
               
                try
                {
                 
                }
                catch (Exception ex)
                {
                    prompt.Value = ex.Message;
                }
            }
            else if (submit.Value == "提交")
            {
                if (juage())
                {

                }
                else
                {
                    prompt.Value = "";
                    save(submit);
                

                }

                try
                {

                }
                catch (Exception ex)
                {
                    prompt.Value = ex.Message;
                }
            }
            else if (submit.Value == "带出产品工艺")
            {
                sqb = new StringBuilder();
                sqb.AppendFormat(@"
select e.wareid AS 产品ID , e.co_wareid as 料号,e.WName AS 品名,
 a.flow_name AS 产品工艺名称,c.step_name as 工序名称,f.utable_name AS 表格名称,
 h.parameter_name AS 参数名称
,'' as parameter
 ,h.unit AS 单位
 ,a.*,b.*,c.*,e.*,f.*,h.*,g.* 
from flow_mst a 
left join flow_det b on a.flid=b.flid 
left join step_mst c on b.stid=c.stid
left join step_det d on c.stid =d.stid 
left join wareinfo e on a.flid=e.flid
left join utable_mst f on f.utid =d.utid 
left join utable_det g on f.utid =g.utid 
left join parameter h on g.paid =h.paid 
where  A.if_delete =0 AND C.if_delete =0 AND F.if_delete =0 AND H.if_delete =0 
and e.wareid='{0}' and a.active='Y' order by b.csn asc", Text4.Value);
                sqb.AppendFormat(@"
;select e.wareid AS 产品ID , e.co_wareid as 料号,e.WName AS 品名,
 a.flow_name AS 产品工艺名称,c.step_name as 工序名称,f.utable_name AS 表格名称
,'' as path
,'' as oldfilename
 ,a.*,b.*,c.*,e.*,f.*
from flow_mst a 
left join flow_det b on a.flid=b.flid 
left join step_mst c on b.stid=c.stid
left join step_det d on c.stid =d.stid 
left join wareinfo e on a.flid=e.flid
left join utable_mst f on f.utid =d.utid 
where  A.if_delete =0 AND C.if_delete =0 AND F.if_delete =0
and e.wareid='{0}' and a.active='Y' order by b.csn asc", Text4.Value);
                SqlConnection sqlcon = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sqb.ToString(), sqlcon);
                sqlcon.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
                sqlcon.Close();
                sqlcom.Dispose();
                sqlcon.Dispose();

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView2.DataSource = dt1;
                    GridView2.DataBind();
                    p1.Visible = true;//当料号存在产品工艺时才显示Gridview1
                    Submit1.Visible = true;//当料号存在产品工艺时才显示提交按扭
                    Submit6.Visible = true;//当料号存在产品工艺时才显示暂存按扭
                }
                else
                {
                    prompt.Visible = true;
                    prompt.Value = "该料号ID不存在生效的产品工艺";

                }
                if (dt1.Rows.Count > 0)
                {
                    p2.Visible = true;//当上传文件的数量大于0时才显示Gridview2
                  
                }
               
            }
            else if (submit.Value == "添加产品追溯表")
            {
                Response.Redirect("/producttrace/tracet.aspx");
            }
            else if (submit.ID == "Submit31")
            {

                Response.Redirect("/producttrace/trace.aspx");

            }
            else if (submit.Value == "上一页")
            {
              
                Response.Write("<script language=javascript>history.go(-2);</script>");
               
            }
          
        }
     
        protected void Bind()
        {
            prompt.Value = "";
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                prompt.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            if (prompt.Value !="" )
            {
                prompt.Visible = true;
            }
            if (id.Value != "")
            {
                sqb = new StringBuilder();
                sqb.AppendFormat(@"
select a.wareid AS 产品ID , a.co_wareid as 料号,a.WName AS 品名,
a.flow_name AS 产品工艺名称,* from trace_mst a 
where  A.if_delete =0 and a.trid='{0}'", id.Value);
                sqb.AppendFormat(@"
;select 
step_name as 工序名称,
utable_name AS 表格名称,
parameter_name AS 参数名称
,unit as 单位
,* from trace_det where trid='{0}' ", id.Value);
                sqb.AppendFormat(@"
;
select distinct(csn),step_name,utable_name,(b.OldFileName)
,(select top 1 path from warefile where WareID =a.trid and OldFileName =a.utable_name order by FLKEY desc) as path   from trace_det a 
left join WareFile b on a.trid=b.WareID and a.utable_name=b.OldFileName  where trid ='{0}' order by csn  asc /*一个表名上传多次取最后一次上传的文件*/
", id.Value);
                SqlConnection sqlcon = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sqb.ToString(), sqlcon);
                sqlcon.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
                dt2 = ds.Tables[2];
                sqlcon.Close();
                sqlcom.Dispose();
                sqlcon.Dispose();
                if (dt.Rows.Count > 0)
                {
                    Text1.Value = dt.Rows[0]["trid"].ToString();
                    //Text2.Value = dt.Rows[0]["trid"].ToString();
                    Text3.Value = dt.Rows[0]["orid"].ToString();
                    Text4.Value = dt.Rows[0]["wareid"].ToString();
                    Text5.Value = dt.Rows[0]["co_wareid"].ToString();
                    Text6.Value = dt.Rows[0]["wname"].ToString();
                    Text7.Value = dt.Rows[0]["cwareid"].ToString();

                    if (dt.Rows[0]["if_submit"].ToString() == "1")
                    {
                        Submit1.Visible = false;//提交后不允许修改
                        Submit6.Visible = false;//提交后不允许修改
                    }
                    else
                    {

                        Submit1.Visible = true;
                        Submit6.Visible = true;
                    }
                }
                if (dt1.Rows.Count > 0)
                {
                    p1.Visible = true;
                   
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                }
                if (dt2.Rows.Count > 0)
                {
                    p2.Visible = true;
                    GridView2.DataSource = dt2;
                    GridView2.DataBind();
                }
            }
            else
            {
            
            }
         
        }
       
        #region juage()
        private bool juage()
        {
            bool b = false;
            string[] a = { "", "", "","","","" };
            Text3.Style["background-color"] = "#ffffff";
            Text3.Style["color"] = "#595d5a";
            Text4.Style["background-color"] = "#ffffff";
            Text4.Style["color"] = "#595d5a";
            if (Text3.Value == "")
            {
                a[0] = "订单号不能为空";
                prompt.Value = "订单号不能为空";
                Text3.Style["background-color"] = "#e04c64";
                Text3.Style["color"] = "#ffffff";
                Text3.Focus();
                b = true;
            }
            if (Text4.Value == "")
            {

                a[1] = "料号ID不能为空";
                prompt.Value = "料号ID不能为空";
                Text4.Style["background-color"] = "#e04c64";
                Text4.Style["color"] = "#ffffff";
                Text4.Focus();
                b = true;
            }
            /*if (juage_if_exist_onlyone() == false)
            {
                a[3] = "至少有一项工序才能保存";
                b = true;
            }
            if (juage_gridview1(""))//判断产品信息
            {
                b = true;
            }*/
 
            if (id.Value=="" && returnFileDt().Rows.Count != juage_onloadfile_count())
            {
               
                prompt.Value = "有几个表名就要上传几个文件才能保存" + "gridview_count=" + GridView2.Rows.Count.ToString() + "," + juage_onloadfile_count().ToString();
                a[2] = "有几个表名就要上传几个文件才能保存";
                b = true;
            }
            if (prompt.Value != "")
            {
                 prompt.Visible = true;
            }
            if (a[0] != "")
            {
             prompt.Value = a[0];
            }
            else if (a[1] != "")
            {
                prompt.Value = a[1];
            }
            else if (a[2] != "")
            {
                prompt.Value = a[2];
            }
            return b;
        }
        #endregion
        private int juage_onloadfile_count()//要求有几个上传表名就要上传几个文件才能保存
        {
            int filecount=0;
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            int i;
            for (i = 0; i < files.Count; i++)
            {
                string FileName = "";
                System.Web.HttpPostedFile myFile = files[i];
                FileName = System.IO.Path.GetFileName(myFile.FileName);
                if (FileName.Length > 0)//有文件才执行上传操作再保存到数据库 
                {
                    filecount++;
                }
            }
            return filecount;
        }
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
                if (prompt.Value != "")
                {
                    prompt.Visible = true;
                }
                if (a[0] != "")
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
                if (WNAME !="")
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        #endregion
        protected void save(HtmlInputSubmit submit)
        {
            prompt.Value = "";
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
            sqb = new StringBuilder();
      
            if (id.Value != "")
            {
                TRID = id.Value;
            }
            else
            {
                TRID = new basec().numYM_NEW(10, 4, "0001", "trace_mst", "TRID", "TR");
            }
            if (id.Value == "")
            {
             
                    sqb.AppendFormat(";INSERT INTO trace_mst");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("TRID");
                    sqb.AppendFormat(",orid");
                    sqb.AppendFormat(",flow_name");
                    sqb.AppendFormat(",wareid");
                    sqb.AppendFormat(",co_wareid");
                    sqb.AppendFormat(",wname");
                    sqb.AppendFormat(",cwareid");
                    sqb.AppendFormat(",MakerID");
                    sqb.AppendFormat(",MDate");
                    sqb.AppendFormat(",if_delete");
                    sqb.AppendFormat(",if_submit");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'",TRID );
                    sqb.AppendFormat(",'{0}'", Text3.Value);
                    sqb.AppendFormat(",'{0}'", "");
                    sqb.AppendFormat(",'{0}'", Text4.Value);
                    sqb.AppendFormat(",'{0}'", Text5.Value);
                    sqb.AppendFormat(",'{0}'", Text6.Value);
                    sqb.AppendFormat(",'{0}'", Text7.Value);
                    sqb.AppendFormat(",'{0}'", Request.Cookies["cookiename"].Values["usid"].ToString());
                    sqb.AppendFormat(",getdate()");
                    sqb.AppendFormat(",0");
                    if (submit.Value == "暂存")
                    {
                        sqb.AppendFormat(",0");
                    }
                    if (submit.Value == "提交")//提前后更新提交状态为已提交，提交后不能再修改
                    {
                        sqb.AppendFormat(",1");
                    }
                   
                    sqb.AppendFormat(")");
                
            }
      
            else
            {
                sqb.AppendFormat("UPDATE trace_mst");
                sqb.AppendFormat(" SET");
                sqb.AppendFormat(" wareid='{0}'", Text4.Value);
                if (submit.Value == "暂存")
                {
                    sqb.AppendFormat(",if_submit=0");
                }
                if (submit.Value == "提交")//提前后更新提交状态为已提交，提交后不能再修改
                {
                    sqb.AppendFormat(",if_submit=1");
                }
                sqb.AppendFormat(" where TRID='" + id.Value + "'");
            }
          
           //写入追溯表明细数据 start
            basec.getcoms(";delete trace_det where TRID='" + id.Value + "'");
            int j = 1;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                stepName = ((Label)GridView1.Rows[i].Cells[0].FindControl("l2")).Text;
                tableName  = ((Label)GridView1.Rows[i].Cells[0].FindControl("l3")).Text;
                parameterName = ((Label)GridView1.Rows[i].Cells[0].FindControl("l4")).Text;
                parameter = ((HtmlInputText)GridView1.Rows[i].Cells[0].FindControl("Text1")).Value;
                unit = ((Label)GridView1.Rows[i].Cells[0].FindControl("l5")).Text;
                string  csn = ((Label)GridView1.Rows[i].Cells[0].FindControl("l6")).Text;
                    sqb.AppendFormat(";INSERT INTO trace_det");
                    sqb.AppendFormat("(");
                    sqb.AppendFormat("TRID");
                    sqb.AppendFormat(",csn");
                    sqb.AppendFormat(",step_name");
                    sqb.AppendFormat(",utable_name");
                    sqb.AppendFormat(",parameter_name");
                    sqb.AppendFormat(",parameter");
                    sqb.AppendFormat(",unit");
                    sqb.AppendFormat(")");
                    sqb.AppendFormat(" VALUES (");
                    sqb.AppendFormat("'{0}'", TRID );
                    sqb.AppendFormat(",'{0}'", csn);
                    sqb.AppendFormat(",'{0}'", stepName);
                    sqb.AppendFormat(",'{0}'", tableName );
                    sqb.AppendFormat(",'{0}'", parameterName);
                    sqb.AppendFormat(",'{0}'", parameter);
                    sqb.AppendFormat(",'{0}'", unit);
                    sqb.AppendFormat(")");
                    j++;
                
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
                prompt.Visible = true;
                prompt.Value = ex.Message;
                IFExecution_SUCCESS = false;
            }
            /*执行文件上传 start*/
            if (IFExecution_SUCCESS)
            {

            
                try
                {
                    CFileInfo cf = new CFileInfo();
                    cf.OnloadFileForTrace(TRID, returnFileDt());
                    prompt.Value = cf.ErrowInfo;
                    IFExecution_SUCCESS = true;
                }
                catch (Exception ex)
                {
                    prompt.Visible = true;
                    IFExecution_SUCCESS = false;
                    prompt.Value = ex.Message;

                }
            }
        
            /*执行文件上传 end*/
            if (IFExecution_SUCCESS == true && id.Value == "")//添加
            {
                //ClearText();
                Text1.Value = TRID;
                id.Value = TRID;
                Bind();
            }
            else if (IFExecution_SUCCESS == true)//修改
            {
                Text1.Value = TRID;
                id.Value = TRID;
                Bind();
            }
        }
        private DataTable returnFileDt()
        {

            
            dt = bc.getdt(@"
select e.wareid AS 产品ID 
, e.co_wareid as 料号
,e.WName AS 品名
, a.flow_name AS 产品工艺名称
,c.step_name as 工序名称
,f.utable_name AS 表格名称
from flow_mst a 
left join flow_det b on a.flid=b.flid 
left join step_mst c on b.stid=c.stid
left join step_det d on c.stid =d.stid 
left join wareinfo e on a.flid=e.flid
left join utable_mst f on f.utid =d.utid 
where  e.wareid='" + Text4.Value + "' and a.active='Y' order by b.csn asc");

            return dt;

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        
        }

        protected void ClearText()
        {
         
            Text1.Value = "自动生成";
            p1.Visible = false;//默认不加载产品工艺数据显示窗口
            p2.Visible = false;//默认不加载文件上传窗口
            p3.Visible = false;//默认不加载文件显示窗口
            Submit1.Visible = false;//默认不显示提交按扭
            Text2.Value = "";
            Text3.Value = "";
            Text4.Value = "";
            Text5.Value = "";
            Text6.Value = "";
            Text7.Value = "";
            cuid.Value = "";//清空上一次查询的客户ID
         
          

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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

     

     

    }
}
