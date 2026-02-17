using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace XizheC
{
    public class   PrintPurchaseBill
    {
        private string _PRINT_ID;
        public string PRINT_ID
        {
            set { _PRINT_ID = value; }
            get { return _PRINT_ID; }

        }
        basec bc = new basec();
        public string sqlo1 { set; get; }
        string sqlo = @"
SELECT A.PUKEY AS 索引,A.PUID as 采购单号
,A.SN  AS  项次
,A.WareID as 品号,B.CO_WAREID AS 料号,
B.WNAME AS 品名,B.CWAREID AS 客户料号,B.SPEC as 铜厚,
B.UNIT as 单位,A.PCount as 采购数量 ,A.PurchaseUnitPrice as 采购单价 ,A.TaxRate as 税率,
 CAST(ROUND((A.PurchaseUNITPRICE*A.PCOUNT),2) AS DECIMAL(18,2)) AS 未税金额,
 CAST(ROUND((A.TAXRATE/100*A.PurchaseUNITPRICE*PCOUNT),2) AS DECIMAL(18,2)) AS 税额,
 CAST(ROUND((A.PurchaseUNITPRICE*(1+(A.TAXRATE)/100)*PCOUNT),2) AS DECIMAL(18,2)) AS 含税金额, 
A.SUID as 供应商代码,C.SName as 供应商名称,F.CONTACT AS 联系人,F.PHONE AS 电话,H.CONTACT AS 收货人,
H.Phone AS 收货人电话,H.CONAME AS 收货公司,
 H.ADDRESS AS 收货地址,D.PDATE AS 采购日期,D.PURID AS 采购员工号,E.ENAME AS 采购员姓名, F.ADDRESS AS 地址,
 J.CONAME AS 公司名称, I .Contact AS 公司联系人,C.PAYMENT AS 付款方式,C.PAYMENT_CLAUSE AS 付款条件,
 I.Phone AS 公司电话,D.RDID AS RDID,D.COKEY AS COKEY,
 A.NEEDDATE AS 需求日期,
A.URGENT AS 工程费,
A.REMARK AS 备注 
 ,(SELECT TOP 1 COName  FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) AS  公司名称
 ,(SELECT TOP 1 Address   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司地址
 ,(SELECT TOP 1 Phone   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司电话
 ,(SELECT TOP 1 Contact   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司联系人
 ,(SELECT TOP 1 Email   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司邮箱
from Purchase_DET A 
 LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
 LEFT JOIN SUPPLIERINFO_MST C ON A.SUID=C.SUID
 LEFT JOIN PURCHASE_MST D ON A.PUID=D.PUID
 LEFT JOIN EMPLOYEEINFO E ON D.PURID=E.EMID
 LEFT JOIN SUPPLIERINFO_DET F ON F.SUKEY=C.SUKEY
 LEFT JOIN RECEIVINGANDDELIVERY H ON D.RDID=H.RDID
 LEFT JOIN CompanyInfo_DET I ON D.COKEY=I.COKEY 
 LEFT JOIN CompanyInfo_MST J ON I.COID =J.COID 
";
       
       /*string sqlt = @"
INSERT INTO TEMP_PRINT_FOR_PURCHASE
(
PRINT_ID,
采购日期,
采购单号,
公司名称,
公司联系人,
公司电话,
供应商名称,
联系人,
电话,
地址,
收货公司,
收货地址,
收货人,
收货人电话,
客户料号,
品名,
项次,
未税金额,
含税金额,
付款方式,
付款条件,
采购数量,
采购单价,
需求日期,
备注,
合计含税金额,
STATUS,
DATE,
YEAR,
MONTH,
DAY


)
VALUES 
(
@PRINT_ID,
@采购日期,
@采购单号,
@公司名称,
@公司联系人,
@公司电话,
@供应商名称,
@联系人,
@电话,
@地址,
@收货公司,
@收货地址,
@收货人,
@收货人电话,
@客户料号,
@品名,
@项次,
@未税金额,
@含税金额,
@付款方式,
@付款条件,
@采购数量,
@采购单价,
@需求日期,
@备注,
@合计含税金额,
@STATUS,
@DATE,
@YEAR,
@MONTH,
@DAY




)

";*/
        public PrintPurchaseBill()
        {
            sqlo1 = sqlo;
        }
        #region purchasedt  /*crystalprint 1/2*/
        public  DataTable purchasedt()
        {
            DataTable dt4 = new DataTable();
           
            dt4.Columns.Add("采购日期", typeof(string));
            dt4.Columns.Add("采购单号", typeof(string));
            dt4.Columns.Add("公司名称", typeof(string));
            dt4.Columns.Add("公司联系人", typeof(string));
            dt4.Columns.Add("公司电话", typeof(string));
            dt4.Columns.Add("供应商名称", typeof(string));
            dt4.Columns.Add("联系人", typeof(string));
            dt4.Columns.Add("电话", typeof(string));
            dt4.Columns.Add("地址", typeof(string));
            dt4.Columns.Add("收货公司", typeof(string));
            dt4.Columns.Add("收货地址", typeof(string));
            dt4.Columns.Add("收货人", typeof(string));
            dt4.Columns.Add("收货人电话", typeof(string));
            dt4.Columns.Add("客户料号", typeof(string));
            dt4.Columns.Add("品名", typeof(string));
            dt4.Columns.Add("项次", typeof(string));
            dt4.Columns.Add("未税金额", typeof(string));
            dt4.Columns.Add("含税金额", typeof(string));
            dt4.Columns.Add("付款方式", typeof(string));
            dt4.Columns.Add("付款条件", typeof(string));
            dt4.Columns.Add("采购数量", typeof(string));
            dt4.Columns.Add("采购单价", typeof(string));
            dt4.Columns.Add("需求日期", typeof(string));
            dt4.Columns.Add("备注", typeof(string));
            dt4.Columns.Add("合计含税金额", typeof(string));
            return dt4;
        }
        #endregion
       
        #region ask
        public  DataTable ask(string puid)
        {
            string sql1 = sqlo;
            DataTable dtt= bc.getdt(sql1+" WHERE A.PUID='"+puid +"' ORDER BY A.PUKEY ASC");
            return dtt;
        }
        #endregion
        #region asko   /*打印采购单使用中*/
        public DataTable asko(string puid)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss").Replace("/", "-");
            DataTable dtt = this.purchasedt();
            DataTable dt = bc.getdt(sqlo + " WHERE A.PUID='" + puid + "' ORDER BY A.PUKEY ASC");
            if (dt.Rows.Count > 0)
            {
               
                
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dtt.NewRow();
dr1["采购日期"]=dr["采购日期"].ToString();
dr1["采购单号"]=dr["采购单号"].ToString();
dr1["公司名称"] = dr["公司名称"].ToString();
dr1["公司联系人"]=dr["公司联系人"].ToString();
dr1["公司电话"]=dr["公司电话"].ToString();
dr1["供应商名称"]=dr["供应商名称"].ToString();
dr1["联系人"]=dr["联系人"].ToString();
dr1["电话"]=dr["电话"].ToString();
dr1["地址"]=dr["地址"].ToString();
dr1["收货公司"] = dr["收货公司"].ToString();
dr1["收货地址"]=dr["收货地址"].ToString();
dr1["收货人"]=dr["收货人"].ToString();
dr1["收货人电话"]=dr["收货人电话"].ToString();
dr1["客户料号"]=dr["客户料号"].ToString();
dr1["品名"]=dr["品名"].ToString();
dr1["项次"]=dr["项次"].ToString();
dr1["未税金额"]=dr["未税金额"].ToString();
dr1["含税金额"]=dr["含税金额"].ToString();
dr1["付款方式"]=dr["付款方式"].ToString();
dr1["付款条件"]=dr["付款条件"].ToString();
dr1["采购数量"]=dr["采购数量"].ToString();
dr1["采购单价"]=dr["采购单价"].ToString();
dr1["需求日期"]=dr["需求日期"].ToString();
dr1["备注"]=dr["备注"].ToString();
dr1["合计含税金额"] = dt.Compute("SUM(含税金额)", "").ToString();

/*SqlCommand sqlcom = new SqlCommand();
                    sqlcom .CommandText =sqlt ;

                    sqlcom.Parameters.Add("@PRINT_ID", SqlDbType.VarChar, 20).Value = PRINT_ID;
                    sqlcom.Parameters.Add("@采购日期", SqlDbType.VarChar, 20).Value = dr["采购日期"].ToString();
                    sqlcom.Parameters.Add("@采购单号", SqlDbType.VarChar, 20).Value = dr["采购单号"].ToString();
                    sqlcom.Parameters.Add("@公司名称", SqlDbType.VarChar, 50).Value = dr["公司名称"].ToString();
                    sqlcom.Parameters.Add("@公司联系人", SqlDbType.VarChar, 20).Value = dr["公司联系人"].ToString();
                    sqlcom.Parameters.Add("@公司电话", SqlDbType.VarChar, 20).Value = dr["公司电话"].ToString();
                    sqlcom.Parameters.Add("@供应商名称", SqlDbType.VarChar, 50).Value = dr["供应商名称"].ToString();
                    sqlcom.Parameters.Add("@联系人", SqlDbType.VarChar, 20).Value = dr["联系人"].ToString();
                    sqlcom.Parameters.Add("@电话", SqlDbType.VarChar, 20).Value = dr["电话"].ToString();
                    sqlcom.Parameters.Add("@地址", SqlDbType.VarChar, 100).Value = dr["地址"].ToString();
                    sqlcom.Parameters.Add("@收货公司", SqlDbType.VarChar, 50).Value = dr["收货公司"].ToString();
                    sqlcom.Parameters.Add("@收货地址", SqlDbType.VarChar, 100).Value = dr["收货地址"].ToString();
                    sqlcom.Parameters.Add("@收货人", SqlDbType.VarChar, 20).Value = dr["收货人"].ToString();
                    sqlcom.Parameters.Add("@收货人电话", SqlDbType.VarChar, 20).Value = dr["收货人电话"].ToString();
                    sqlcom.Parameters.Add("@客户料号", SqlDbType.VarChar, 20).Value = dr["客户料号"].ToString();
                    sqlcom.Parameters.Add("@品名", SqlDbType.VarChar, 50).Value = dr["品名"].ToString();
                    sqlcom.Parameters.Add("@项次", SqlDbType.VarChar, 20).Value = dr["项次"].ToString();
                    sqlcom.Parameters.Add("@未税金额", SqlDbType.VarChar, 20).Value = dr["未税金额"].ToString();
                    sqlcom.Parameters.Add("@含税金额", SqlDbType.VarChar, 20).Value = dr["含税金额"].ToString();
                    sqlcom.Parameters.Add("@付款方式", SqlDbType.VarChar, 20).Value = dr["付款方式"].ToString();
                    sqlcom.Parameters.Add("@付款条件", SqlDbType.VarChar, 20).Value = dr["付款条件"].ToString();
                    sqlcom.Parameters.Add("@采购数量", SqlDbType.VarChar, 20).Value = dr["采购数量"].ToString();
                    sqlcom.Parameters.Add("@采购单价", SqlDbType.VarChar, 20).Value = dr["采购单价"].ToString();
                    sqlcom.Parameters.Add("@需求日期", SqlDbType.VarChar, 20).Value = dr["需求日期"].ToString();
                    sqlcom.Parameters.Add("@备注", SqlDbType.VarChar, 1000).Value = dr["备注"].ToString();
                    sqlcom.Parameters.Add("@合计含税金额", SqlDbType.VarChar, 20).Value = dt.Compute("SUM(含税金额)", "").ToString();
                    sqlcom.Parameters.Add("@STATUS", SqlDbType.VarChar, 20).Value = "N";
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 1000).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    SqlConnection sqlcn = bc.getcon();
                    sqlcom.Connection = sqlcn;
                    sqlcn.Open ();
                    sqlcom .ExecuteNonQuery ();
                    sqlcn.Close();*/
                    dtt.Rows.Add(dr1);
                }
              
            }
            return dtt;
        }
        #endregion
        
        #region askTOTAL
        public DataTable asktotal(string puid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sql1 + " WHERE A.PUID='" + puid + "' ORDER BY A.PUKEY ASC");
            DataRow dr2 = dtt.NewRow();
            dtt.Columns .Add ("合计含税金额",typeof(decimal));
            dr2["未税金额"] = dtt.Compute("SUM(未税金额)", "");
            dr2["税额"] = dtt.Compute("SUM(税额)", "");
            dr2["含税金额"] = dtt.Compute("SUM(含税金额)", "");
            dr2["合计含税金额"] = dtt.Compute("SUM(含税金额)", "");
            dtt.Rows.Add(dr2);
            return dtt;
        }
        #endregion
        #region askTOTALt
        public DataTable askt(string puid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sql1 + " WHERE A.PUID='" + puid + "' ORDER BY A.PUKEY ASC");
            DataRow dr2 = dtt.NewRow();
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dr2["合计含税金额"] = dtt.Compute("SUM(含税金额)", "");
            dtt.Rows.Add(dr2);
            return dtt;
        }
        #endregion

    }
}
