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
using System.Collections.Generic;

namespace XizheC
{
    public  class COFFER_SEARCH
    {
        basec bc = new basec();
        #region nature
        private string _GEID;
        public string GEID
        {
            set { _GEID = value; }
            get { return _GEID; }

        }
        private string _USID;
        public string USID
        {
            set { _USID = value; }
            get { return _USID; }

        }
        private string _UNAME;
        public string UNAME
        {
            set { _UNAME = value; }
            get { return _UNAME; }

        }
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _ENAME;
        public string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        private string _DEPART;
        public string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }

        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

        }
        private string _sqlo;
        public string sqlo
        {
            set { _sqlo = value; }
            get { return _sqlo; }

        }
        private string _sqlt;
        public string sqlt
        {
            set { _sqlt = value; }
            get { return _sqlt; }

        }
        private string _sqlth;
        public string sqlth
        {
            set { _sqlth = value; }
            get { return _sqlth; }

        }
        private string _sqlf;
        public string sqlf
        {
            set { _sqlf = value; }
            get { return _sqlf; }

        }
        private string _sqlfi;
        public string sqlfi
        {
            set { _sqlfi = value; }
            get { return _sqlfi; }

        }
        #endregion
        #region setsql
        string setsql = @"
SELECT
A.OFID AS 报价单号,
A.WareID as ID,
B.CO_WAREID AS 料号,
B.WNAME AS 品名,
B.CWAREID AS 客户料号,
B.SPEC AS 铜厚,
B.PLANK_THICKNESS AS 板厚,
B.PANEL AS 板材,
B.[SET_LEN] AS SET长,
B.[SET_WIDTH] as set宽,
B.[SET_COMPOSING] as set排版数,
D.CName as 客户 ,
CONVERT(VARCHAR(10),A.DATE,112) AS 日期,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID )  AS 制单人,
A.QUANTITY_UNITPRICE AS 量产单价,
A.QUANTITY_COUNT AS 量产数量,
A.SAMPLE_UNITPRICE AS Sample单价,
A.SAMPLE_COUNT AS Sample数量,
A.SMALLQUANTITY_UNITPRICE AS 小量单价,
CASE WHEN A.PROJECT_COST IS NULL OR A.PROJECT_COST='' THEN C.PROJECT_COST
ELSE A.PROJECT_COST
END  AS 工程费,
CASE WHEN A.REMARK IS NULL OR A.REMARK='' THEN B.REMARK
ELSE A.REMARK
END AS 备注,
A.MUCH_PRICE AS 批量单价,
A.UNIT_AREA AS 单位面积价格,
E.SellUnitPrice AS 销售单价
FROM OFFER A 
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN SPEC C ON C.SPEC=B.SPEC
LEFT JOIN CUSTOMERINFO_MST D ON B.CUID=D.CUID
LEFT JOIN SellUnitPrice E ON A.WAREID =E.WareID 
";
        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO OFFER(
OFID,
WAREID,
QUANTITY_UNITPRICE,
QUANTITY_COUNT,
SAMPLE_UNITPRICE,
SAMPLE_COUNT,
SMALLQUANTITY_UNITPRICE,
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
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
"
;
        #endregion
        #region setsqlt
        string setsqlt = @"
UPDATE OFFER SET 
WAREID=@WAREID,
QUANTITY_UNITPRICE=@QUANTITY_UNITPRICE,
QUANTITY_COUNT=@QUANTITY_COUNT,
SAMPLE_UNITPRICE=@SAMPLE_UNITPRICE,
SAMPLE_COUNT=@SAMPLE_COUNT,
SMALLQUANTITY_UNITPRICE=@SMALLQUANTITY_UNITPRICE,
PROJECT_COST=@PROJECT_COST,
REMARK=@REMARK,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY,
MUCH_PRICE=@MUCH_PRICE
";
        public COFFER_SEARCH()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
        }
        #endregion
    
        
      



    
    }
}
