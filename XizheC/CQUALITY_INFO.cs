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
    public class CQUALITY_INFO
    {
        basec bc = new basec();
        private   string _GETID;
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
         string s = @"

SELECT A.QUID
      ,A.WAREID
      ,A.SUID
      ,
      CASE WHEN A.INITIAL_DATE  IS NULL OR A.INITIAL_DATE='' THEN NULL 
      ELSE SUBSTRING(A.INITIAL_DATE,6,5)
      END 
      AS INITIAL_DATE,
        CASE WHEN A.RECENTLY_DATE  IS NULL OR A.RECENTLY_DATE ='' THEN NULL 
      ELSE SUBSTRING(A.RECENTLY_DATE,6,5)
      END 
      AS RECENTLY_DATE
      ,A.HAPPEN_DATE
      ,A.HAPPEN_PLACE
      ,A.DEFECT_NAME
      ,A.DEFECT_DETAIL
      ,A.TOTAL_COUNT
      ,A.DEFECT_COUNT
,RTRIM(CONVERT(DECIMAL(18,4),(A.DEFECT_COUNT/A.TOTAL_COUNT) *100 )) +'%' AS  DEFECT_PROPOTION
      ,A.DEFECT_PERIOD
      ,A.DEFECT_PROCESS_MODE
      ,A.DEFECT_LOSS
      ,A.SOLUTION
      ,A.MakerID
      ,(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS MAKER
      ,A.Date
      ,A.Year
      ,A.Month
      ,A.Day
      ,B.CWareID 
      ,B.WName 
      ,B.CO_WAREID 
      ,B.WName
      ,C.CName 
      ,D.SName 
FROM QUALITY_INFO A
LEFT JOIN WareInfo B ON A.WAREID=B.WareID 
LEFT JOIN CustomerInfo_MST C ON B.CUID=C.CUID 
LEFT JOIN SUPPLIERINFO_MST D ON A.SUID=D.SUID
";
        string s1 = @"INSERT INTO QUALITY_INFO(
QUID,
WAREID,
SUID,
INITIAL_DATE,
RECENTLY_DATE,
HAPPEN_DATE,
HAPPEN_PLACE,
DEFECT_NAME,
DEFECT_DETAIL,
TOTAL_COUNT,
DEFECT_COUNT,
DEFECT_PERIOD,
DEFECT_PROCESS_MODE,
DEFECT_LOSS,
SOLUTION,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES 
(
@QUID,
@WAREID,
@SUID,
@INITIAL_DATE,
@RECENTLY_DATE,
@HAPPEN_DATE,
@HAPPEN_PLACE,
@DEFECT_NAME,
@DEFECT_DETAIL,
@TOTAL_COUNT,
@DEFECT_COUNT,
@DEFECT_PERIOD,
@DEFECT_PROCESS_MODE,
@DEFECT_LOSS,
@SOLUTION,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY

)

";
        string s2 = @"
UPDATE QUALITY_INFO SET 
WAREID=@WAREID,
SUID=@SUID,
RECENTLY_DATE=@RECENTLY_DATE,
HAPPEN_DATE=@HAPPEN_DATE,
HAPPEN_PLACE=@HAPPEN_PLACE,
DEFECT_NAME=@DEFECT_NAME,
DEFECT_DETAIL=@DEFECT_DETAIL,
TOTAL_COUNT=@TOTAL_COUNT,
DEFECT_COUNT=@DEFECT_COUNT,
DEFECT_PERIOD=@DEFECT_PERIOD,
DEFECT_PROCESS_MODE=@DEFECT_PROCESS_MODE,
DEFECT_LOSS=@DEFECT_LOSS,
SOLUTION=@SOLUTION,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";

        public CQUALITY_INFO()
        {
            sql = s;
            sqlo = s1;
            sqlt = s2;
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
