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
    public class   CSALE_INVOICE
    {
        basec bc = new basec();


        public CSALE_INVOICE()
        {

        }
        PrintSellTableBill se = new PrintSellTableBill();
        public DataTable DT_EMPTY()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("选择", typeof(bool));
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("应收单号", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("销货单号", typeof(string));
            dtt.Columns.Add("目录项次", typeof(Int32));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("销售单位", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户名称", typeof(string));
            dtt.Columns.Add("订单日期", typeof(string));
            dtt.Columns.Add("发票号码", typeof(string));
            dtt.Columns.Add("发票未税金额", typeof(decimal));
            dtt.Columns.Add("发票税额", typeof(decimal));
            dtt.Columns.Add("发票含税金额", typeof(decimal));
            dtt.Columns.Add("应收索引", typeof(string));
            dtt.Columns.Add("销货销退单号", typeof(string));
            dtt.Columns.Add("销货销退数量", typeof(string));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("应收人工号", typeof(string));
            dtt.Columns.Add("应收人", typeof(string));
            dtt.Columns.Add("应收日期", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("合计未税金额", typeof(decimal));
            dtt.Columns.Add("合计税额", typeof(decimal));
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dtt.Columns.Add("预收款单号", typeof(string));
            dtt.Columns.Add("预收款金额", typeof(string));
            dtt.Columns.Add("扣款项目", typeof(string));
            dtt.Columns.Add("扣款金额", typeof(string));
            dtt.Columns.Add("实际应收金额", typeof(decimal));
            dtt.Columns.Add("累计收款金额", typeof(string));
            dtt.Columns.Add("未收款金额", typeof(decimal));
            dtt.Columns.Add("收款单号", typeof(string));
            dtt.Columns.Add("收款金额", typeof(decimal));
            dtt.Columns.Add("收款人工号", typeof(string));
            dtt.Columns.Add("收款人", typeof(string));
            dtt.Columns.Add("收款日期", typeof(string));
            dtt.Columns.Add("收款制单人", typeof(string));
            dtt.Columns.Add("收款制单日期", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
            dtt.Columns.Add("工程费", typeof(decimal));
            return dtt;
        }


        #region dtx
        public DataTable dtx()
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(se.sqlo + " WHERE A.STATUS='RECONCILE' " + " ORDER BY A.SEKEY ASC");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                   
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = false;
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["销货销退单号"] = dr1["销货单号"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    //dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["销货销退数量"] = dr1["销货数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["工程费"] = dr1["工程费"].ToString();
                    //dr["订单日期"] = dr1["订单日期"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }

            return dtt;
        }
        #endregion

    }
}
