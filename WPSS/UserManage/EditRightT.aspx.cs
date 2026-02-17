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


namespace WPSS.UserManage
{
    public partial class EditRightT : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        basec bc = new basec();
        WPSS.Validate va = new Validate();
        public static string[] str1 = new string[] { "" };
        public static string[] strE = new string[] { "" };
        System.Drawing.Color c1 = System.Drawing.ColorTranslator.FromHtml("#c0c0c0");
        System.Drawing .Color c2 = System.Drawing.ColorTranslator.FromHtml("#990033");
        private string _USID;
        public string USID
        {
            set { _USID = value; }
            get { return _USID; }

        }
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
        CEDIT_RIGHT cedit_right = new CEDIT_RIGHT();

        int i;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Text1.Value = IDO;
                IDO = "";
                Bind();
            }
          
            //if (va.returnb() == true)
            //Response.Redirect("\\Default.aspx"); 
        }
        protected void Bind()
        {
            Label2.Text = "(背景色为浅灰色的复选框无需点选为不可用)";
            Label2.ForeColor  = c2;
            hint.Value = "";
        
            /*Bind("US13110001");
            USID = "US13110001";
            Text1.Value = "admin";*/
            dt = basec.getdts(cedit_right.sql   + " WHERE  A.UNAME='" + Text1.Value + "'");
            if (dt.Rows.Count > 0)
            {
                Text1.Value = dt.Rows[0]["用户名"].ToString();
                Label1.Text = dt.Rows[0]["姓名"].ToString();
                Bind(dt.Rows[0]["用户编号"].ToString());
            }
            else
            {
                Bind("");
            }

            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Value = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Value = "";
            }
            //Response.Write(cedit_right.sql + " WHERE  A.UNAME='" + Text1.Value + "'");
       
    
        }
    
        protected void ClearText()
        {
            Text1.Value = "";
            Label1.Text = "";
        }
        protected void Assignment(DataTable dt)
        {
           
        
         
        }

        protected void submit1_Click(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlInputSubmit submit = (System.Web.UI.HtmlControls.HtmlInputSubmit)sender;
            if (submit.ID == "Submit1")
            {
                n(false);
                Clear();
                CheckBox6.Checked = false;
                CheckBox7.Checked = false;
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
                RadioButton3.Checked = true;
            }
            else if (submit.ID == "Submit2")
            {
                save();
                try
                {


                }
                catch (Exception)
                {

                }
            }
            else if (submit.ID == "Submit3")
            {
                string n1 = Request.Url.AbsoluteUri;
                string n2 = n1.Substring(n1.Length - 16, 16);
                Response.Redirect("../UserManage/EditRight.aspx" + n2);
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
        protected void Clear()
        {
           
            Text1.Value = "";
            Label1.Text = "";
        

        }
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
         


        }
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
            USID = bc.getOnlyString("SELECT USID FROM USERINFO WHERE UNAME='"+ Text1 .Value +"'");
            #region Save
            if (!juage1())
            {

            }
            else
            {
               
                bc.getcom("DELETE RIGHTLIST WHERE USID='" + USID + "'");
                bc.getcom("DELETE SCOPE_OF_AUTHORIZATION WHERE USID='" + USID + "'");
                if (juage_if_noall_select())
                {

                }
                else
                {
                    save1();
                    if (RadioButton1.Checked == true)
                    {
                        bc.getcom("INSERT INTO SCOPE_OF_AUTHORIZATION(USID,SCOPE) VALUES ('" + USID + "','Y')");
                    }
                    else if (RadioButton2.Checked == true)
                    {
                        bc.getcom("INSERT INTO SCOPE_OF_AUTHORIZATION(USID,SCOPE) VALUES ('" + USID + "','GROUP')");
                    }
                    else
                    {
                        bc.getcom("INSERT INTO SCOPE_OF_AUTHORIZATION(USID,SCOPE) VALUES ('" + USID + "','N')");
                    }

                    IFExecution_SUCCESS = true;
                    if (IFExecution_SUCCESS == true)
                    {
                        Bind();
                    }

                }
            }
       
            #endregion

        }
        #endregion
        #region juage1()
        private bool juage1()
        {

            bool ju = true;
            if (Text1 .Value =="")
            {
                ju = false;
                hint.Value = "用户名不能为空！";

            }
            else if (!bc.exists("SELECT * FROM USERINFO WHERE UNAME='" + Text1.Value + "'"))
            {
                ju = false;
                hint.Value = "用户名在系统中不存在！";

            }

            return ju;

        }
        #endregion
        #region juage_if_noall_select
        private bool juage_if_noall_select()
        {
            bool b = true;
             for (int i = 0; i <GridView1.Rows.Count; i++)
             {
                 string vx = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                 if (cedit_right.JUAGE_IF_SAME(vx))
                 {

                     if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2")).Checked)
                     {
                         b = false;
                         break;

                     }
                     else if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3")).Checked)
                     {
                         b = false;
                         break;

                     }
                     else if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4")).Checked)
                     {
                         b = false;
                         break;

                     }
                     else if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5")).Checked)
                     {
                         b = false;
                         break;

                     }
                 
                 }
                 else
                 {
                     if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                     {
                         b = false;
                         break;

                     }
                    

                 }

             }
            return b;
        }
        #endregion
    
        #region save
        private void save1()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace ("/","-");
            string n1 = Request.Url.AbsoluteUri;
            string n2 = n1.Substring(n1.Length - 10, 10);
            string varMakerID = bc.getOnlyString("SELECT EMID FROM USERINFO WHERE USID='" + n2 + "'");
            USID = bc.getOnlyString("SELECT USID FROM USERINFO WHERE UNAME='" + Text1.Value + "'");
            string v1, v2, v3, v4, v5;
            for (int i = 0; i <GridView1 .Rows.Count ; i++)
            {
                string vx = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                if (cedit_right.JUAGE_IF_SAME(vx))
                {
                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2")).Checked==false )
                    {

                        v2 = "N";
                    }
                    else
                    {
                        v2 = "Y";

                    }
                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3")).Checked==false )
                    {

                        v3 = "N";
                    }
                    else
                    {
                        v3 = "Y";

                    }
                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4")).Checked==false )
                    {

                        v4 = "N";
                    }
                    else
                    {
                        v4 = "Y";

                    }
                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5")).Checked==false )
                    {

                        v5 = "N";
                    }
                    else
                    {
                        v5 = "Y";

                    }
                  
                    if (v2 == "N" && v3 == "N" && v4 == "N" && v5 == "N")
                    {

                    }
                    else
                    {
                        //MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString() +" "+dataGridView1 .Columns [j].Name .ToString ()+ dataGridView1.Rows[i].Cells[j].Value.ToString()+ " "+v1);
                       
                        cedit_right.USID = USID;
                        cedit_right.NODEID = bc.getOnlyString("SELECT NODEID FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.PARENT_NODEID = bc.getOnlyString("SELECT PARENT_NODEID FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.NODE_NAME = vx;
                        cedit_right.IMAGE_URL = bc.getOnlyString("SELECT IMAGE_URL FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.URL = bc.getOnlyString("SELECT URL FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.OPERATE = "N";
                        cedit_right.SEARCH = v2; ;
                        cedit_right.ADD_NEW = v3;
                        cedit_right.EDIT = v4;
                        cedit_right.DEL = v5;
                        cedit_right.MANAGE = "N";
                        cedit_right.FINANCIAL = "N";
                        cedit_right.GENERAL_MANAGE = "N";
                        cedit_right.EMID = varMakerID;
                        cedit_right.SQlcommandE();
                    }
                }
          
                else
                {

                    if (((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == false)
                    {

                        v1 = "N";
                    }
                    else
                    {
                        v1 = "Y";

                    }
                    //MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString() + v1);
                    if (v1 == "Y")
                    {


                        cedit_right.USID = USID;
                        cedit_right.NODEID = bc.getOnlyString("SELECT NODEID FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.PARENT_NODEID = bc.getOnlyString("SELECT PARENT_NODEID FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.NODE_NAME = vx;
                        cedit_right.IMAGE_URL = bc.getOnlyString("SELECT IMAGE_URL FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.URL = bc.getOnlyString("SELECT URL FROM RIGHTNAME WHERE NODE_NAME='" + vx + "'");
                        cedit_right.OPERATE = v1;
                        cedit_right.SEARCH = "N";
                        cedit_right.ADD_NEW = "N";
                        cedit_right.EDIT = "N";
                        cedit_right.DEL = "N";
                        cedit_right.MANAGE = "N";
                        cedit_right.FINANCIAL = "N";
                        cedit_right.GENERAL_MANAGE = "N";
                        cedit_right.EMID = varMakerID;
                        cedit_right.SQlcommandE();
                    }
                }
            }

        }
        #endregion
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
 
        }

        #region bind
        private void Bind(string USID)
        {
            DataTable dty = bc.getdt("SELECT * FROM RIGHTLIST WHERE USID='" + USID + "'");
            dt = bc.getdt("SELECT * FROM RIGHTNAME");
            dt = GetTableInfo(dt);

            if (dty.Rows.Count > 0)
            {

                foreach (DataRow dr1 in dty.Rows)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr1["NODE_NAME"].ToString() == dr["作业名称"].ToString())
                        {
                            if (cedit_right .JUAGE_IF_SAME (dr1["NODE_NAME"].ToString()))
                            {
                                if (dr1["SEARCH"].ToString() == "Y")
                                {
                                    dr["查询"] = true;
                                }
                                if (dr1["ADD_NEW"].ToString() == "Y")
                                {
                                    dr["新增"] = true;
                                }
                                if (dr1["EDIT"].ToString() == "Y")
                                {
                                    dr["修改"] = true;
                                }
                                if (dr1["DEL"].ToString() == "Y")
                                {
                                    dr["删除"] = true;
                                }
                            }
                            else
                            {
                                if (dr1["OPERATE"].ToString() == "Y")
                                {
                                    dr["复选框"] = true;
                                }
                           
                            }
                            break;
                        }

                    }
                }

            }
            if (bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + USID + "'") == "Y")
            {
                RadioButton1.Checked = true;
            }
            else if (bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + USID + "'") == "GROUP")
            {
                RadioButton2.Checked = true;
            }
            else
            {
                RadioButton3.Checked = true;
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string v1 = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                if (cedit_right .JUAGE_IF_SAME (v1))
                {
                    GridView1.Rows[i].Cells[0].BackColor = c1;
                }
                else
                {
                    GridView1.Rows[i].Cells[2].BackColor = c1;
                    GridView1.Rows[i].Cells[3].BackColor = c1;
                    GridView1.Rows[i].Cells[4].BackColor = c1;
                    GridView1.Rows[i].Cells[5].BackColor = c1;
                }
            }
            string a = bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE USID='" + USID + "'");
            try
            {
           

            }
            catch (Exception)
            {
                
            }
          
        }
        #endregion
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("复选框", typeof(bool));
            dtt.Columns.Add("作业名称", typeof(string));
            dtt.Columns.Add("查询", typeof(bool));
            dtt.Columns.Add("新增", typeof(bool));
            dtt.Columns.Add("修改", typeof(bool));
            dtt.Columns.Add("删除", typeof(bool));
            return dtt;
        }
        #endregion
        #region GetTableInfo1
        public DataTable GetTableInfo(DataTable dtx)
        {
            dt = GetTableInfo();
            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["复选框"] = false;
                    dr["作业名称"] = dr1["NODE_NAME"].ToString();
                    dr["查询"] = false;
                    dr["新增"] = false;
                    dr["修改"] = false;
                    dr["删除"] = false;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion
        #region GetTableInfow
        public DataTable GetTableInfow()
        {
            DataTable dtt = GetTableInfo();
            DataRow dr = dtt.NewRow();
            dr["复选框"] = false;
            dr["作业名称"] = "";
            dr["查询"] = false;
            dr["新增"] = false;
            dr["修改"] = false;
            dr["删除"] = false;
            dtt.Rows.Add(dr);
            return dtt;
        }
        #endregion
       
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

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    
        protected void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
           
            if (CheckBox6.Checked)
            {
               n(true);
            }
            else
            {
               n(false );

            } 
            
        }
        protected void n(bool b)
        {
            int i;
            for (i = 0; i < GridView1.Rows.Count; i++)
            {
                string v1 = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                if (cedit_right.JUAGE_IF_SAME(v1))
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2")).Checked = b;
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3")).Checked = b;
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4")).Checked = b;
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5")).Checked = b;
                }
                else
                {
                    ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = b;
                }

            }
        }

        protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            
           
          
                for (i = 0; i < GridView1.Rows.Count; i++)
                {
                    string v1 = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("TextBox1")).Text;
                    if (cedit_right.JUAGE_IF_SAME(v1))
                    {
                        CheckBox cbx = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2"));
                        if (cbx.Checked)
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox2")).Checked = true;

                        }
                        CheckBox cbxo = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3"));
                        if (cbxo.Checked)
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox3")).Checked = true;

                        }
                        CheckBox cbxt = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4"));
                        if (cbxt.Checked)
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox4")).Checked = true;

                        }
                        CheckBox cbxth = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5"));
                        if (cbxth.Checked)
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox5")).Checked = true;

                        }
                    }
                    else
                    {
                        CheckBox cbx = ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1"));
                        if (cbx.Checked)
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;

                        }
                    }

                }
       
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton1.Checked = true;
            RadioButton2.Checked = false;
            RadioButton3.Checked = false;
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = true;
            RadioButton3.Checked = false;
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            RadioButton3.Checked = true;
        }

        protected void PageButton_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
            Bind();
        }

        protected void btngo_Click(object sender, EventArgs e)
        {
     
        }
    }
}
