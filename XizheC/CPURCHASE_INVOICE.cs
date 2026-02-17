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
    public class   CPURCHASE_INVOICE
    {
        basec bc = new basec();


        public CPURCHASE_INVOICE()
        {

        }
        CPURCHASE_GODE cpurchase_gode = new CPURCHASE_GODE();
        public DataTable DT_EMPTY()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("选择", typeof(bool));
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("应付单号", typeof(string));
            dtt.Columns.Add("采购单号", typeof(string));
            dtt.Columns.Add("入库单号", typeof(string));
            dtt.Columns.Add("目录项次", typeof(Int32));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("采购单位", typeof(string));
            dtt.Columns.Add("采购单价", typeof(decimal));
            dtt.Columns.Add("采购数量", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            dtt.Columns.Add("供应商代码", typeof(string));
            dtt.Columns.Add("供应商名称", typeof(string));
            dtt.Columns.Add("采购日期", typeof(string));
            dtt.Columns.Add("发票号码", typeof(string));
            dtt.Columns.Add("发票未税金额", typeof(decimal));
            dtt.Columns.Add("发票税额", typeof(decimal));
            dtt.Columns.Add("发票含税金额", typeof(decimal));
            dtt.Columns.Add("应付索引", typeof(string));
            dtt.Columns.Add("入库(退货)单号", typeof(string));
            dtt.Columns.Add("入库(退货)数量", typeof(string));
            dtt.Columns.Add("累计入库数量", typeof(decimal));
            dtt.Columns.Add("应付人工号", typeof(string));
            dtt.Columns.Add("应付人", typeof(string));
            dtt.Columns.Add("应付日期", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("合计未税金额", typeof(decimal));
            dtt.Columns.Add("合计税额", typeof(decimal));
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dtt.Columns.Add("预收款单号", typeof(string));
            dtt.Columns.Add("预收款金额", typeof(string));
            dtt.Columns.Add("扣款项目", typeof(string));
            dtt.Columns.Add("扣款金额", typeof(string));
            dtt.Columns.Add("实际应付金额", typeof(decimal));
            dtt.Columns.Add("累计付款金额", typeof(string));
            dtt.Columns.Add("未付款金额", typeof(decimal));
            dtt.Columns.Add("付款单号", typeof(string));
            dtt.Columns.Add("付款金额", typeof(decimal));
            dtt.Columns.Add("付款人工号", typeof(string));
            dtt.Columns.Add("付款人", typeof(string));
            dtt.Columns.Add("付款日期", typeof(string));
            dtt.Columns.Add("付款制单人", typeof(string));
            dtt.Columns.Add("付款制单日期", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
            return dtt;
        }


        #region dtx
        public DataTable dtx()
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(cpurchase_gode.sql + " WHERE A.STATUS='RECONCILE' " + " ORDER BY A.PGKEY ASC");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                   
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = false;
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["入库(退货)单号"] = dr1["入库单号"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["采购单位"] = dr1["采购单位"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["入库(退货)数量"] = dr1["入库数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }

            return dtt;
        }
        #endregion

    }
}
