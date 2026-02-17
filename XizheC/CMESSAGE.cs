using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml;
namespace XizheC
{
    public class CMESSAGE
    {
        basec bc = new basec();

        #region nature
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _TEL;
        public string TEL
        {
            set { _TEL = value; }
            get { return _TEL; }

        }
        private string _SHORT_MESSAGE_CONTENT;
        public string SHORT_MESSAGE_CONTENT
        {
            set { _SHORT_MESSAGE_CONTENT = value; }
            get { return _SHORT_MESSAGE_CONTENT; }

        }
        private string _LIMITED_DATE;
        public string LIMITED_DATE
        {
            set { _LIMITED_DATE = value; }
            get { return _LIMITED_DATE; }

        }
        private string _RMKEY;
        public string RMKEY
        {
            set { _RMKEY = value; }
            get { return _RMKEY; }

        }
        private string _LIMITED_STATUS;
        public string LIMITED_STATUS
        {
            set { _LIMITED_STATUS = value; }
            get { return _LIMITED_STATUS; }

        }
        private string _CONTACT;
        public string CONTACT
        {
            set { _CONTACT = value; }
            get { return _CONTACT; }

        }
        private string _PHONE;
        public string PHONE
        {
            set { _PHONE = value; }
            get { return _PHONE; }

        }
        private string _FAX;
        public string FAX
        {
            set { _FAX = value; }
            get { return _FAX; }

        }
        private string _QQ;
        public string QQ
        {
            set { _QQ = value; }
            get { return _QQ; }

        }
        private string _ALWW;
        public string ALWW
        {
            set { _ALWW = value; }
            get { return _ALWW; }

        }
        private string _EMAIL;
        public string EMAIL
        {
            set { _EMAIL = value; }
            get { return _EMAIL; }

        }
        private string _DEPART;
        public string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }

        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }

        }
        private string _PAYMENT_CLAUSE;
        public string PAYMENT_CLAUSE
        {
            set { _PAYMENT_CLAUSE = value; }
            get { return _PAYMENT_CLAUSE; }

        }
        private string _CUSTOMER_ID;
        public string CUSTOMER_ID
        {
            set { _CUSTOMER_ID = value; }
            get { return _CUSTOMER_ID; }

        }
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }

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
        private string _POSTCODE;
        public string POSTCODE
        {
            set { _POSTCODE = value; }
            get { return _POSTCODE; }

        }
        private string _ADDRESS;
        public string ADDRESS
        {
            set { _ADDRESS = value; }
            get { return _ADDRESS; }

        }
        private string _sqlsi;
        public string sqlsi
        {
            set { _sqlsi = value; }
            get { return _sqlsi; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
        private string _CUKEY;
        public string CUKEY
        {
            set { _CUKEY = value; }
            get { return _CUKEY; }

        }
        private  bool _IFExecutionSUCCESS;
        public  bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _PAYMENT;
        public string PAYMENT
        {
            set { _PAYMENT = value; }
            get { return _PAYMENT; }

        }

        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _THE_DEFAULT;
        public string THE_DEFAULT
        {
            set { _THE_DEFAULT = value; }
            get { return _THE_DEFAULT; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _PROVINCE;
        public string PROVINCE
        {
            set { _PROVINCE = value; }
            get { return _PROVINCE; }

        }
        private string _CARTYPE;
        public string CARTYPE
        {
            set { _CARTYPE = value; }
            get { return _CARTYPE; }

        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }

        }
        #endregion
        DataTable dt = new DataTable();
        #region sql
        string setsql = @"
SELECT 
B.CUID AS 客户编号,
B.CUSTOMER_ID AS 车牌号码,
B.CNAME AS 车主姓名,
B.CARTYPE AS 车辆类型,
B.LIMITED_DATE AS 年审日期,
CASE WHEN B.SHORT_MESSAGE IS NOT NULL OR B.SHORT_MESSAGE!='' THEN B.SHORT_MESSAGE
ELSE 
'未年审或已过年审期'
END AS 状态,
B.PHONE AS 电话,
B.PROVINCE AS 省份,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID) AS 制单人,
B.DATE AS 制单日期
FROM CUSTOMERINFO_MST B

";


        string setsqlo = @"
INSERT INTO CUSTOMERINFO_DET
(
CUKEY,
CUID,
SN,
CONTACT,
THE_DEFAULT,
PHONE,
TEL,
QQ,
FAX,
POSTCODE,
EMAIL,
ADDRESS,
DEPART,
REMARK,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@CUKEY,
@CUID,
@SN,
@CONTACT,
@THE_DEFAULT,
@PHONE,
@TEL,
@QQ,
@FAX,
@POSTCODE,
@EMAIL,
@ADDRESS,
@DEPART,
@REMARK,
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY

)


";

        string setsqlt = @"

INSERT INTO CUSTOMERINFO_MST
(
CUID,
CNAME,
CARTYPE,
LIMITED_DATE,
PHONE,
DATE,
MAKERID,
YEAR,
MONTH,
DAY,
CUSTOMER_ID,
PROVINCE
)
VALUES
(
@CUID,
@CNAME,
@CARTYPE,
@LIMITED_DATE,
@PHONE,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY,
@CUSTOMER_ID,
@PROVINCE
)
";
        string setsqlth = @"
UPDATE CUSTOMERINFO_MST SET 
CNAME=@CNAME,
CARTYPE=@CARTYPE,
LIMITED_DATE=@LIMITED_DATE,
PHONE=@PHONE,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY,
CUSTOMER_ID=@CUSTOMER_ID,
PROVINCE=@PROVINCE
";

        string setsqlf = @"

";
        string setsqlfi = @"


";
        #endregion
        public string IDENTIFYING { set; get; }
        public CMESSAGE()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_PICKING_MST", "WPID", "WP");

            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
     
        }
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            dt = new DataTable();
            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("默认联系人",typeof (bool ));
            dt.Columns.Add("联系人", typeof(string));
            dt.Columns.Add("联系电话", typeof(string));
            dt.Columns.Add("手机号码", typeof(string));
            dt.Columns.Add("QQ号", typeof(string));
            dt.Columns.Add("传真号码", typeof(string));
            dt.Columns.Add("邮政编码", typeof(string));
            dt.Columns.Add("EMAIL", typeof(string));
            dt.Columns.Add("公司地址", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            dt.Columns.Add("备注", typeof(string));
            return dt;
        }
        #endregion
        public string GETID()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.numYM(10, 4, "0001", "select * from CUSTOMERINFO_MST", "CUID", "CU");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
              
            }
            return GETID;
        }
     
  
        public  void SEND_MESSAGE(string PHONE,string IDENTIFYING)
        {

            StringBuilder sqb = new StringBuilder();
            sqb = new StringBuilder();
            sqb.AppendFormat("【希哲软件】您好，您的验证码是 {0} ", IDENTIFYING);
            SHORT_MESSAGE_CONTENT = sqb.ToString();
        
            string c = UrlEncode(SHORT_MESSAGE_CONTENT, Encoding.UTF8);
            string address = "http://smsapi.c133.cn/OpenPlatform/OpenApi?";
            string action = "sendOnce";
            string ac = "1001@501347500001";
            string authkey = "C2BAC531EDD76C4AE1943F4D91984AFA";
            string cgid = "52";
            string csid = "50134750@13511634094";
            string v1 = address + "action={0}&" + "ac={1}&" + "authkey={2}&" + "cgid={3}&" + "csid={4}&" + "c={5}&" + "m={6}";
            string v2 = string.Format(v1, action, ac, authkey, cgid, csid, c, PHONE);
            string s=sendQuery(v2, SHORT_MESSAGE_CONTENT);
            ErrowInfo = v2;
            
        }
    
        /* UrlEncode
      /* 对指定字符串进行URL标准化转码
      /************************************************************************/
        private static string UrlEncode(string text, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byData = encoding.GetBytes(text);
            for (int i = 0; i < byData.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byData[i], 16));
            }
            return sb.ToString();
        }
        /* sendQuery
          /* 向指定的接口地址POST数据并返回响应数据
          /************************************************************************/
        private static string sendQuery(string url, string param)
        {
            // 准备要POST的数据
            byte[] byData = Encoding.UTF8.GetBytes(param);

            // 设置发送的参数
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = "POST";
            req.Timeout = 5000;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byData.Length;

            // 提交数据
            Stream rs = req.GetRequestStream();
            rs.Write(byData, 0, byData.Length);
            rs.Close();

            // 取响应结果
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

            try
            {
                return sr.ReadToEnd();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            sr.Close();
            return null;
        }

    }
}
