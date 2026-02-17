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

namespace XizheC
{
    public class CLIMITE_SEARCH
    {
        basec bc = new basec();
        private  string _GETID;
        public  string GETID
        {
            set { _GETID =value ; }
            get { return _GETID; }

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
         string setsql= @"
SELECT 
DISTINCT(A.USID) AS USID,
B.UNAME AS UNAME,
C.ENAME AS ENAME,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
A.DATE AS DATE 
FROM   
LIMITE_SEARCH  A 
LEFT JOIN USERINFO B ON A.USID=B.USID
LEFT JOIN EMPLOYEEINFO C ON C.EMID=B.EMID
";
        string setsqlo = @"INSERT INTO LIMITE_SEARCH(
USID,
CUID_OR_SUID,
MAKERID,
DATE
)
VALUES 
(
@USID,
@CUID_OR_SUID,
@MAKERID,
@DATE
)

";
        string setsqlt = @"
UPDATE LIMITE_SEARCH SET 
USID=@USID,
CUID_OR_SUID=@CUID_OR_SUID,
MAKERID=@MAKERID,
DATE=@DATE
";
        string setsqlth = @"
SELECT 
A.USID AS USID,
A.CUID_OR_SUID AS CUID_OR_SUID,
CASE WHEN D.CName IS NOT NULL THEN D.CName 
ELSE E.SName 
END 
AS CNAME_OR_SNAME,
B.UNAME AS UNAME,
C.ENAME AS ENAME,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER,
A.DATE AS DATE 
FROM   
LIMITE_SEARCH  A 
LEFT JOIN USERINFO B ON A.USID=B.USID
LEFT JOIN EMPLOYEEINFO C ON C.EMID=B.EMID
LEFT JOIN CustomerInfo_MST D ON A.CUID_OR_SUID=D.CUID 
LEFT JOIN SupplierInfo_MST E ON A.CUID_OR_SUID=E.SUID 
";

        public CLIMITE_SEARCH()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth= setsqlth;
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            GETID = bc.numYMD(11, 3, "001", "SELECT * FROM QUALITY_INFO", "QUID", "QU");
        }
        public static DataTable SqlDTM(string TableName, string ColumnName)
        {

            return basec.getdts("SELECT " + ColumnName + " FROM " + TableName);
        }
    }
}
