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
    public class   PrintSellTableBill
    {
        basec bc = new basec();
        public string sqlo { get; set; }
      
        string setsqlo = @"
SELECT 
A.SEKEY AS 索引,
F.CUKEY AS CUKEY,A.SEID AS 销货单号,A.ORID as 订单号, A.SN as 项次,E.WareID as ID,B.CO_WAREID AS 料号,
B.WNAME AS 品名,B.PLANK_THICKNESS 板厚,B.SPEC as 铜厚,B.CWAREID AS 客户料号,
B.UNIT as 单位,C.OCOUNT AS 订单数量,C.SELLUNITPRICE AS 销售单价,C.TAXRATE AS 税率,
CAST(ROUND(E.MRCount,2) AS DECIMAL(18,2)) as 销货数量 ,CAST(ROUND(E.FREECount,2) AS DECIMAL(18,2)) as FREE数量 ,
isnull(CAST(ROUND(E.MRCOUNT*C.SELLUNITPRICE+C.URGENT,4) AS DECIMAL(18,2)),0) AS 未税金额,
isnull(CAST(ROUND((E.MRCOUNT*C.SELLUNITPRICE+C.URGENT)*C.TAXRATE/100,2) AS DECIMAL(18,2)),0) AS 税额,
isnull(CAST(ROUND((E.MRCOUNT*C.SELLUNITPRICE+C.URGENT)*(1+C.TAXRATE/100),2) AS DECIMAL(18,2)),0) AS 含税金额,
C.CUID as 客户代码,
D.CName as 客户名称 ,
G.CONTACT AS 联系人,
G.PHONE AS 联系电话,
G.ADDRESS AS 送货地址,
G.EMAIL AS EMAIL,
F.SELLDATE AS 销货日期,
F.SELLERID AS 销货员工号,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.SELLERID )  AS 销货员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=E.MAKERID )  AS 制单人,
F.STATUS 状态,
H.CustomerORID AS 客户订单号,
E.DATE AS 制单日期,A.REMARK AS 备注,E.BatchID AS 批号,C.URGENT AS 工程费,
F.COURIER_NUMBER AS 快递单号,
F.company_title AS 公司抬头
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN CUSTOMERINFO_DET G ON F.CUKEY=G.CUKEY
LEFT JOIN Order_MST H ON C.ORID =H.ORID 
";

        string sqlt = @"
SELECT A.SEID AS 销货单号, 
A.SN as 项次,
B.CO_WAREID AS 料号,
B.WNAME AS 品名,
B.PLANK_THICKNESS,
B.SPEC,
B.CWAREID AS 客户料号,
CAST(ROUND(SUM(E.MRCount),2) AS DECIMAL(18,2)) as 销货数量 ,
CAST(ROUND(SUM(E.FREECount),2) AS DECIMAL(18,2)) as FREE数量 ,
C.SELLUNITPRICE AS 销售单价,
D.CName as 客户名称 ,
G.CONTACT AS 联系人,
G.PHONE AS 联系电话,
G.ADDRESS AS 送货地址,
F.SELLDATE AS 销货日期,
H.CustomerORID AS 客户订单号,
A.REMARK AS 备注,
E.BatchID AS 批号
 ,(SELECT TOP 1 COName  FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) AS  公司名称
 ,(SELECT TOP 1 Address   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司地址
 ,(SELECT TOP 1 Phone   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司电话
 ,(SELECT TOP 1 Contact   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司联系人
 ,(SELECT TOP 1 Email   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司邮箱
 ,(SELECT TOP 1 fax   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司传真
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN CUSTOMERINFO_MST D ON C.CUID=D.CUID
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN CUSTOMERINFO_DET G ON F.CUKEY=G.CUKEY
LEFT JOIN Order_MST H ON C.ORID =H.ORID 

";
        string sqlth = @"
 GROUP BY A.SEID,
A.SN,
B.CO_WAREID,
B.WNAME,
B.PLANK_THICKNESS,
B.SPEC,
B.CWAREID,
C.SELLUNITPRICE,
D.CNAME,
G.CONTACT,
G.PHONE,
G.ADDRESS,
F.SELLDATE,
H.CustomerORID,
A.REMARK,
E.BATCHID 
ORDER BY A.SEID,A.SN ASC
";
        public string sqlf = @"
DECLARE @T1 AS TABLE
(序号 INT,
品号 VARCHAR(20),
STORAGEID VARCHAR(20),
批号 VARCHAR(20),
库存数量 DECIMAL(10, 2),
品名 VARCHAR(50),
料号 VARCHAR(50),
客户料号 VARCHAR(50),
仓库 VARCHAR(50),
CUID VARCHAR(20),
客户名称 VARCHAR(50));

        DECLARE @T2 AS TABLE
        (序号 INT,
        品号 VARCHAR(20),
STORAGEID VARCHAR(20),
批号 VARCHAR(20),
库存数量 DECIMAL(10,2),
品名 VARCHAR(50),
料号 VARCHAR(50),
客户料号 VARCHAR(50),
仓库 VARCHAR(50),
CUID VARCHAR(20),
客户名称 VARCHAR(50) );
--取得库存数量不为的库存表
WITH X1 AS(
SELECT
A.WAREID,
A.STORAGEID,
A.BATCHID AS BATCHID,
SUM(ISNULL(A.GECOUNT,0))+SUM(ISNULL(A.FREECOUNT,0)) AS GECOUNT FROM GODE A
GROUP BY A.WAREID, A.STORAGEID, A.BATCHID
),
X2 AS(
SELECT
A.WAREID,
A.STORAGEID,
A.BATCHID AS BATCHID,
SUM(ISNULL(A.MRCOUNT,0))+SUM(ISNULL(A.FREECOUNT,0))
AS MRCOUNT FROM MATERE A
GROUP BY A.WAREID, A.STORAGEID, A.BATCHID
),
X3 AS(
SELECT
X1.WAREID 品号,
X1.STORAGEID ,
X1.BATCHID 批号,
(X1.GECOUNT-ISNULL(X2.MRCOUNT,0)) AS 库存数量
FROM X1 LEFT JOIN X2 ON X1.WAREID=X2.WAREID AND X1.STORAGEID=X2.STORAGEID AND X1.BATCHID=X2.BATCHID
WHERE(X1.GECOUNT-ISNULL(X2.MRCOUNT,0))<>0
),
X7 AS(
SELECT X3.*, X4.WNAME 品名, X4.CO_WAREID 料号, X4.CWAREID 客户料号, X5.STORAGENAME 仓库, X4.CUID, X6.CNAME 客户名称
FROM X3 LEFT JOIN WAREINFO X4 ON X3.品号= X4.WAREID
LEFT JOIN STORAGEINFO X5 ON X3.STORAGEID= X5.STORAGEID
LEFT JOIN CUSTOMERINFO_MST X6 ON X4.CUID= X6.CUID)

-- 只要库存数量最大的仓库批号
INSERT INTO @T1 SELECT ROW_NUMBER() OVER(ORDER BY 品号, 库存数量 ASC)  AS 序号,* FROM X7 ORDER BY 品号,库存数量 ASC
--SELECT* FROM @T1 
--SELECT* FROM @T1 A WHERE A.序号IN(SELECT MAX(序号) FROM @T1 WHERE 品号= A.品号) ORDER BY  品号,库存数量ASC
INSERT INTO @T2 SELECT* FROM @T1 A WHERE A.序号 IN (SELECT MAX(序号) FROM @T1 WHERE 品号= A.品号) ORDER BY  品号,库存数量 ASC;
        WITH DS1 AS(SELECT
        A.ORID AS 订单号,
        D.SN AS 项次,
        E.WAREID AS 品号,
        E.CO_WAREID AS 料号,
        E.WNAME AS 品名,
        E.SPEC AS 规格,
        E.UNIT AS 单位,
        E.CWAREID AS 客户料号,
        E.PLANK_THICKNESS 板厚,
        D.SELLUNITPRICE AS 销售单价,
        D.TAXRATE 税率,
        D.OCOUNT 订单数量,
        D.URGENT 工程费,
        CAST(ROUND(D.OCOUNT* D.SELLUNITPRICE+D.URGENT,4) AS DECIMAL(18,2)) AS 未税金额,
         CAST(ROUND((D.OCOUNT * D.SELLUNITPRICE + D.URGENT) * D.TAXRATE / 100, 2) AS DECIMAL(18, 2)) AS 税额,
                   CAST(ROUND((D.OCOUNT * D.SELLUNITPRICE + D.URGENT) * (1 + D.TAXRATE / 100), 2) AS DECIMAL(18, 2)) AS 含税金额,
                               A.CUID AS CUID,
B.CNAME AS CNAME,
B.CUKEY,
A.CUSTOMERORID AS CUSTOMERORID,
A.ORDERSTATUS_MST,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID )  AS MAKER,
CASE WHEN A.ORDERSTATUS_MST='CLOSE' THEN '已出货'
WHEN A.ORDERSTATUS_MST= 'PROGRESS' THEN '部分出货'
WHEN A.ORDERSTATUS_MST= 'DELAY' THEN 'DELAY'
WHEN A.ORDERSTATUS_MST= 'INVOICE' THEN '已开票'
ELSE 'OPEN'
END AS 订单状态,
F.INVOICE_HAVETAX_AMOUNT,
F.INVOICE_HAVETAX_AMOUNT AS 应收金额,
ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE ORID= A.ORID)),0) AS 已收金额,
 ISNULL(F.INVOICE_HAVETAX_AMOUNT, 0)-ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE ORID= A.ORID)),0) AS 未收金额,
   CASE WHEN ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE ORID= A.ORID)),0)=0 THEN '未收款'
WHEN ISNULL((SELECT SUM(RECEIVABLES_ORDER_AMOUNT) FROM RECEIVABLES_ORDER WHERE RCID IN (SELECT RCID FROM RECEIVABLES_MST WHERE ORID= A.ORID)),0)=F.INVOICE_HAVETAX_AMOUNT THEN '已收款'
ELSE '部分收款'
END AS 收款状态,
D.ORDERSTATUS_DET AS ORDERSTATUS_DET,
D.DELIVERYDATE AS DELIVERYDATE,
D.OCOUNT AS OCOUNT,
E.WNAME AS WNAME,
E.CWAREID AS CWAREID,
A.DATE AS DATE,
C.CONTACT,
C.PHONE,
C.ADDRESS,
A.ORDERDATE,
(SELECT
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS OCOUNT
FROM SELLTABLE_DET A1
LEFT JOIN MATERE
B ON A1.SEKEY=B.MRKEY
WHERE  A1.ORID =D.ORID AND A1.SN=D.SN ) AS 累计销货数量,
(SELECT SUM(B.GECOUNT) AS GECOUNT FROM SELLRETURN_DET A1
LEFT JOIN GODE B ON A1.SRKEY = B.GEKEY  WHERE  A1.ORID = D.ORID AND A1.SN = D.SN ) AS 累计销退数量,
      (SELECT
      CAST(ROUND(SUM(B.MRCOUNT), 2) AS DECIMAL(18, 2)) AS MRCOUNT
      FROM SELLTABLE_DET A1
      LEFT JOIN  MATERE B
      ON A1.SEKEY = B.MRKEY
      WHERE  A1.ORID = A.ORID AND A1.SN = D.SN  AND A1.SEID = '{1}' ) AS 本销货单累计销货数量,
            (SELECT 仓库 FROM @T2 WHERE 品号 = D.WAREID) 仓库,
(SELECT 批号 FROM @T2 WHERE 品号=D.WAREID) 批号,
(SELECT 库存数量 FROM @T2 WHERE 品号=D.WAREID) 库存数量
FROM   ORDER_MST A
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN CUSTOMERINFO_DET C ON B.CUKEY= C.CUKEY
LEFT JOIN ORDER_DET D ON A.ORID = D.ORID
LEFT JOIN WAREINFO E ON D.WAREID = E.WAREID
LEFT JOIN RECEIVABLES_MST F ON A.ORID= F.ORID
),
DS2 AS(
SELECT*, OCOUNT-ISNULL(DS1.累计销货数量,0)+ISNULL(DS1.累计销退数量,0) AS 未销货数量, OCOUNT-ISNULL(DS1.累计销货数量,0)+ISNULL(DS1.累计销退数量,0) AS 销货数量 FROM DS1)
SELECT* FROM DS2 WHERE 订单号 IN ({0});
";
        public PrintSellTableBill()
        {
            sqlo = setsqlo;
        }
        #region ask
        public  DataTable ask(string seid)
        {
            DataTable dtt= bc.getdt(sqlo+" WHERE A.SEID='"+seid +"' ORDER BY A.SEKEY ASC");
            return dtt;
        }
        #endregion
        #region table  /*crystalprint 1/2*/
        public DataTable table()
        {
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("客户名称", typeof(string));
            dt4.Columns.Add("销货单号", typeof(string));
            dt4.Columns.Add("送货地址", typeof(string));
            dt4.Columns.Add("联系人", typeof(string));
            dt4.Columns.Add("联系电话", typeof(string));
            dt4.Columns.Add("销货日期", typeof(string));
            dt4.Columns.Add("客户订单号", typeof(string));
            dt4.Columns.Add("料号", typeof(string));
            dt4.Columns.Add("板厚", typeof(string));
            dt4.Columns.Add("铜厚", typeof(string));
            dt4.Columns.Add("客户料号", typeof(string));
            dt4.Columns.Add("品名", typeof(string));
            dt4.Columns.Add("项次", typeof(string));
            dt4.Columns.Add("销货数量", typeof(string));
            dt4.Columns.Add("FREE数量", typeof(string));
            dt4.Columns.Add("批号", typeof(string));
            dt4.Columns.Add("备注", typeof(string));
            dt4.Columns.Add("合计销货数量", typeof(string));
            dt4.Columns.Add("合计FREE数量", typeof(string));
            dt4.Columns.Add("销售单价", typeof(string));

            dt4.Columns.Add("公司名称", typeof(string));
            dt4.Columns.Add("公司地址", typeof(string));
            dt4.Columns.Add("公司联系人", typeof(string));
            dt4.Columns.Add("公司电话", typeof(string));
            dt4.Columns.Add("公司邮箱", typeof(string));
            dt4.Columns.Add("公司传真", typeof(string));
            return dt4;
        }
        #endregion
        #region askTOTALt
        public DataTable askt(string seid)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlt + " WHERE A.SEID='" + seid + "' "+sqlth);
            DataRow dr2 = dtt.NewRow();
            dtt.Columns.Add("合计销货数量", typeof(decimal));
            dtt.Columns.Add("合计FREE数量", typeof(decimal));
            dr2["合计销货数量"] = dtt.Compute("SUM(销货数量)", "");
            dr2["合计FREE数量"] = dtt.Compute("SUM(FREE数量)", "");
            dtt.Rows.Add(dr2);
            return dtt;
        }
        #endregion

        #region asko   /*此方法在使用*/
        public DataTable asko(string seid)
        {
            DataTable dtt = this.table();
            DataTable dt = bc.getdt(sqlt + " WHERE A.SEID='" + seid + "' " + sqlth);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr1 = dtt.NewRow();
                    dr1["客户名称"] = dr["客户名称"].ToString();
                    dr1["销货单号"] = dr["销货单号"].ToString();
                    dr1["送货地址"] = dr["送货地址"].ToString();
                    dr1["联系人"] = dr["联系人"].ToString();
                    dr1["联系电话"] = dr["联系电话"].ToString();
                    dr1["销货日期"] = dr["销货日期"].ToString();
                    dr1["客户订单号"] = dr["客户订单号"].ToString();
                    dr1["料号"] = dr["料号"].ToString();
                    dr1["板厚"] = dr["PLANK_THICKNESS"].ToString();
                    dr1["铜厚"] = dr["SPEC"].ToString();
                    dr1["客户料号"] = dr["客户料号"].ToString();
                    dr1["品名"] = dr["品名"].ToString();
                    dr1["项次"] = dr["项次"].ToString();
                    dr1["销货数量"] = dr["销货数量"].ToString();
                    dr1["销售单价"] = dr["销售单价"].ToString();
                    dr1["FREE数量"] = dr["FREE数量"].ToString();
                    dr1["批号"] = dr["批号"].ToString();
                    dr1["备注"] = dr["备注"].ToString();
                    dr1["合计销货数量"] = dt.Compute ("SUM(销货数量)","").ToString();
                    dr1["合计FREE数量"] = dt.Compute ("SUM(FREE数量)","").ToString();

                    dr1["公司名称"] = dr["公司名称"].ToString();
                    dr1["公司地址"] = dr["公司地址"].ToString();
                    dr1["公司联系人"] = dr["公司联系人"].ToString();
                    dr1["公司电话"] = dr["公司电话"].ToString();
                    dr1["公司邮箱"] = dr["公司邮箱"].ToString();
                    dr1["公司传真"] = dr["公司传真"].ToString();
                    dtt.Rows.Add(dr1);
                }
            }
            return dtt;
        }
        #endregion

    }
}
