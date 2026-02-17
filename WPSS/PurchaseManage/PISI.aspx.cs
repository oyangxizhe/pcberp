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
using System.Net;
using System.Text;
using XizheC;
using System.IO;
using System.Diagnostics;

namespace WPSS.PurchaseManage
{
    public partial class PISI: System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtx4 = new DataTable();
        basec bc = new basec();


        string sqlo = @"
select A.PUID AS 采购单号,
A.SN as 项次,E.WareID as ID,B.CO_WAREID AS EUJ料号,
B.WNAME AS 品名,B.CWAREID AS 客户料号,
RTRIM(CONVERT(DECIMAL(18,4),C.PurchaseUNITPRICE)) AS 采购单价,
RTRIM(CONVERT(DECIMAL(18,2),C.TAXRATE )) AS 税率,
RTRIM(CONVERT(DECIMAL(18,2),C.PCount)) as 采购数量 ,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.GECount))) as 累计采购入库数量 ,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.GECount*C.PurchaseUNITPRICE))) AS 累计采购未税金额,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.GECount*C.PurchaseUNITPRICE*C.TAXRATE/100) )) AS 累计采购税额,
RTRIM(CONVERT(DECIMAL(18,2),SUM(E.GECount*C.PurchaseUNITPRICE*(1+C.TAXRATE/100)) )) AS 累计采购含税金额,
C.SUID as 供应商代码,D.SNAME as 供应商 ,F.PDATE AS 采购单日期,F.DATE AS 采购制单日期,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.PURID )  AS 采购员
from PurchaseGode_DET A 
LEFT JOIN Purchase_DET C ON A.PUID=C.PUID AND A.SN=C.SN
LEFT JOIN SUPPLIERINFO_MST D ON C.SUID=D.SUID
LEFT JOIN GODE E ON A.PGKEY=E.GEKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN Purchase_MST F ON C.PUID=F.PUID
";
        string sqlt = @" GROUP BY A.PUID,A.SN,E.WAREID,B.CO_WAREID,B.WNAME,B.CWAREID,
C.PurchaseUNITPRICE,C.TAXRATE,C.PCount,C.SUID,D.SNAME,F.PDATE,F.PURID,F.DATE  ORDER BY F.PDATE ASC";



        protected string M_str_sql1;
        WPSS.Validate va = new Validate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (va.returnb() == true)
                Response.Redirect("\\Default.aspx");
          
            select();

        }
        #region bind()
        private void bind()
        {


            try
            {
                hint.Value = "";
                x.Value = "";
                x1.Value = "";
                GridView1.AllowPaging = true;
                GridView1.PageSize = 10;
                select();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region select()
        protected void select()
        {

            string v6 = "", v7 = "";
            string v1 = Text1.Value;/*sname*/
            string v2 = StartDate.Value;
            string v3 = EndDate.Value;
            string v8 = DropDownList1.Text;/*status*/
            string v9 = Text2.Value;/*wname*/
            string v10 = Text3.Value;/*cwareid*/

            if (!bc.juagedate(v2, v3))
            {
                hint.Value = bc.ErrowInfo;
                clear();
                return;
            }
            if (v2 != "" && v3 != "")
            {
                DateTime v4 = Convert.ToDateTime(v2);
                DateTime v5 = Convert.ToDateTime(v3);
                v6 = v4.ToString("yyyy-MM-dd") + " 00:00:00";
                v7 = v5.ToString("yyyy-MM-dd") + " 23:59:59";

            }
            if (CheckBox1.Checked)
            {
                showdata(@" where    D.SNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
                    "%'  AND F.DATE BETWEEN  '" + v6 + "'AND '" + v7 + "' AND B.WNAME like '%" + v9 +
                    "%'");
            }

            else
            {
                showdata(@" where    D.SNAME like '%" + v1 + "%' AND B.CWAREID like '%" + v10 +
              "%'  AND B.WNAME like '%" + v9 + "%'");
            }


        }
        #endregion
        protected void showdata(string sql)
        {

            if (DropDownList1.Text == "已对帐")
            {
                dt = ask(sqlo + sql + " AND F.PurchaseStatus_MST='RECONCILE'" + sqlt);
            }
            else if (DropDownList1.Text == "未对帐")
            {


                dt = ask(sqlo + sql + " AND F.PurchaseStatus_MST NOT IN ('RECONCILE') " + sqlt);
            }
            else if ( DropDownList1.Text == "全部")
            {
                dt = ask(sqlo + sql + sqlt);
            }
            else if (DropDownList1.Text == "" || DropDownList1.Text == "最近30天")
            {
                dt = ask(sqlo + sql + " AND DATEDIFF(DAY,F.PDATE,GETDATE())>0 AND DATEDIFF(DAY,F.PDATE,GETDATE())<=30" + sqlt);
            }
            if (dt.Rows.Count > 1)
            {

                x1.Value = Convert.ToString(1);
                x.Value = Convert.ToString(1);
         
           

                dt1 = ask_o(dt);
                

                Chart1.DataSource = dt1;
                if (dt1.Rows.Count >=1 && dt1.Rows.Count <= 16)
                {
                    Chart1.Width = 990;
                 
                }
                else
                {
                  
                    Chart1.Width = 2900;

                }
                Chart1.Titles.Clear();
                Chart1.Titles.Add("采购指数");
                Chart1.Titles[0].Alignment = System.Drawing.ContentAlignment.TopCenter;
                Chart1.Titles[0].Font = new System.Drawing.Font("黑体", 11);
                
                Chart1.Series.Add("未税金额");
                Chart1.Series["未税金额"].IsValueShownAsLabel = true;
                Chart1.Series["未税金额"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                Chart1.Series["未税金额"].XValueMember = "采购单日期";
                Chart1.Series["未税金额"].YValueMembers = "未税金额";

                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;//启用三维图表区
                //Chart1.ChartAreas[0].Area3DStyle.Rotation = 30;//设置图表区绕Y轴倾斜旋转的角度
                //Chart1.ChartAreas[0].Area3DStyle.Inclination = 50;//设置图表区绕X轴倾斜旋转的角度
                this.Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;//不显示网格线X
                this.Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;//不显示网格线Y
                Chart1.ChartAreas[0].AxisX.Title = "采购日期";
                
                Chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;


             
              

            }
            else
            {

                hint.Value = "没有找到记录!";
                Text7.Value = "";
                Text8.Value = "";
                Text9.Value = "";

            }
          

        }
   
        #region ask
        private DataTable ask(string sql)
        {
            int i, j;
            DataTable dtt = new DataTable();
            dtt.Columns.Add("采购单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("EUJ料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("采购单价", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("采购数量", typeof(decimal));
            dtt.Columns.Add("累计采购入库数量", typeof(decimal));
            dtt.Columns.Add("累计采购未税金额", typeof(decimal));
            dtt.Columns.Add("累计采购税额", typeof(decimal));
            dtt.Columns.Add("累计采购含税金额", typeof(decimal));

            dtt.Columns.Add("累计退货未税金额", typeof(decimal));
            dtt.Columns.Add("累计退货税额", typeof(decimal));
            dtt.Columns.Add("累计退货含税金额", typeof(decimal));

            dtt.Columns.Add("未税金额", typeof(decimal), "累计采购未税金额-累计退货未税金额");
            dtt.Columns.Add("税额", typeof(decimal), "累计采购税额-累计退货税额");
            dtt.Columns.Add("含税金额", typeof(decimal), "累计采购含税金额-累计退货含税金额");

            dtt.Columns.Add("采购单日期", typeof(string));
            dtt.Columns.Add("采购制单日期", typeof(string));
            dtt.Columns.Add("供应商代码", typeof(string));
            dtt.Columns.Add("供应商", typeof(string));


            DataTable dtx1 = bc.getdt(sql);
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["采购单号"] = dtx1.Rows[i]["采购单号"].ToString();
                    dr["项次"] = dtx1.Rows[i]["项次"].ToString();
                    dr["ID"] = dtx1.Rows[i]["ID"].ToString();
                    DataTable dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["ID"].ToString() + "'");
                    dr["EUJ料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["采购数量"] = dtx1.Rows[i]["采购数量"].ToString();
                    dr["采购单价"] = dtx1.Rows[i]["采购单价"].ToString();
                    dr["税率"] = dtx1.Rows[i]["税率"].ToString();
                    dr["累计采购入库数量"] = dtx1.Rows[i]["累计采购入库数量"].ToString();
                    dr["累计采购未税金额"] = dtx1.Rows[i]["累计采购未税金额"].ToString();
                    dr["累计采购税额"] = dtx1.Rows[i]["累计采购税额"].ToString();
                    dr["累计采购含税金额"] = dtx1.Rows[i]["累计采购含税金额"].ToString();

                    dr["累计退货未税金额"] = 0;
                    dr["累计退货税额"] = 0;
                    dr["累计退货含税金额"] = 0;

                    dr["采购单日期"] = dtx1.Rows[i]["采购单日期"].ToString();
                    dr["采购制单日期"] = dtx1.Rows[i]["采购制单日期"].ToString();
                    dr["供应商代码"] = dtx1.Rows[i]["供应商代码"].ToString();
                    dr["供应商"] = dtx1.Rows[i]["供应商"].ToString();

                    dtt.Rows.Add(dr);

                }

            }

            DataTable dtx41 = bc.getdt(@"SELECT A.PUID AS 采购单号,A.SN AS 项次,SUM(A.NOTAX_AMOUNT) AS 累计退货未税金额,
SUM(A.TAX_AMOUNT) AS 累计退货税额,SUM(A.AMOUNT) AS 累计退货含税金额 FROM RETURN_DET A GROUP BY A.PUID,A.SN");
            if (dtx41.Rows.Count > 0)
            {
                for (i = 0; i < dtx41.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["采购单号"].ToString() == dtx41.Rows[i]["采购单号"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx41.Rows[i]["项次"].ToString())
                        {
                            dtt.Rows[j]["累计退货未税金额"] = dtx41.Rows[i]["累计退货未税金额"].ToString();
                            dtt.Rows[j]["累计退货税额"] = dtx41.Rows[i]["累计退货税额"].ToString();
                            dtt.Rows[j]["累计退货含税金额"] = dtx41.Rows[i]["累计退货含税金额"].ToString();
                            break;
                        }

                    }
                }

            }
            DataRow dr1 = dtt.NewRow();
            dr1["采购单号"] = "合计";
            dr1["采购数量"] = dtt.Compute("SUM(采购数量)", "");
            dr1["累计采购入库数量"] = dtt.Compute("SUM(累计采购入库数量)", "");
            dr1["累计采购未税金额"] = dtt.Compute("SUM(累计采购未税金额)", "");
            dr1["累计采购税额"] = dtt.Compute("SUM(累计采购税额)", "");
            dr1["累计采购含税金额"] = dtt.Compute("SUM(累计采购含税金额)", "");

            dr1["累计退货未税金额"] = dtt.Compute("SUM(累计退货未税金额)", "");
            dr1["累计退货税额"] = dtt.Compute("SUM(累计退货税额)", "");
            dr1["累计退货含税金额"] = dtt.Compute("SUM(累计退货含税金额)", "");

            dr1["未税金额"] = dtt.Compute("SUM(累计采购未税金额)-SUM(累计退货未税金额)", "");
            dr1["税额"] = dtt.Compute("SUM(累计采购税额)-SUM(累计退货税额)", "");
            dr1["含税金额"] = dtt.Compute("SUM(累计采购含税金额)-SUM(累计退货含税金额)", "");

            if (!string.IsNullOrEmpty(dr1["未税金额"].ToString()))
            {
                Text7.Value = string.Format("{0:F2}", Convert.ToDouble(dr1["未税金额"].ToString()));
                Text8.Value = string.Format("{0:F2}", Convert.ToDouble(dr1["税额"].ToString()));
                Text9.Value = string.Format("{0:F2}", Convert.ToDouble(dr1["含税金额"].ToString()));
            }
            dtt.Rows.Add(dr1);
            return dtt;
        }
        #endregion

        #region ask_o
        private DataTable ask_o(DataTable dt)
        {
           
            DataTable dtt = new DataTable();
            dtt.Columns.Add("采购单日期", typeof(string));
            dtt.Columns.Add("未税金额", typeof(decimal));
       
            if (dt.Rows.Count > 0)
            {
                
                foreach (DataRow dr1 in dt.Rows)
                {
                    DataTable dtt1 = bc.GET_DT_TO_DV_TO_DT(dtt, "", "采购单日期='" + dr1["采购单日期"] + "'");
                    if (dtt1.Rows.Count > 0)
                    {
                      
                    }
                    else
                    {
                        
                        DataRow dr = dtt.NewRow();
                        dr["采购单日期"] = dr1["采购单日期"].ToString();
                        if (dr1["采购单日期"].ToString() == "")
                        {
                            //dr["未税金额"] = dr1["未税金额"].ToString();
                            //dtt.Rows.Add(dr);
                        }
                        else
                        {
                            dr["未税金额"] = dt.Compute("SUM(未税金额)", "采购单日期='" + dr1["采购单日期"] + "'");
                            dtt.Rows.Add(dr);
                        }
                    }
                
                }
                
            }
            return dtt;
        }
        #endregion
        private void clear()
        {

       
            Text7.Value = "";
            Text8.Value = "";
            Text9.Value = "";

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void PageButton_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = Convert.ToInt32(((LinkButton)sender).CommandName) - 1;
            bind();
        }

   

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            bind();
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            bind();
        }





    }
}