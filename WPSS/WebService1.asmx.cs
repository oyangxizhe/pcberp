using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using XizheC;
using System.Collections.Generic;

namespace WPSS
{
    
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
   
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string[] GET_PARENT_NODEID(string USID)
        {
            basec bc = new basec();
            DataTable dt = bc.getdt("select * from rightlist where PARENT_NODEID=0 and USID='" +USID  + "' ");
            List<string> list1 = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                list1.Add(dr["NODEID"].ToString());
            }
            string[] a = list1.ToArray();
            return a;
        }
    }
}
