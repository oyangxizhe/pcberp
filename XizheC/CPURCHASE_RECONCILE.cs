using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace XizheC
{
    public class CPURCHASE_RECONCILE
    {
        basec bc = new basec();
        #region nature
    
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
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
      
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
     
        #endregion
        #region sql
        #region setsqlo
        string setsqlo = @"
INSERT INTO SU_RECONCILE_DET
(
SNKEY,
SNID,
PRKEY,
STATUS,
YEAR,
MONTH,
DAY
)
VALUES
(
@SNKEY,
@SNID,
@PRKEY,
@STATUS,
@YEAR,
@MONTH,
@DAY

)
";
        #endregion
        #region setsqlt
        string setsqlt = @"
INSERT INTO SU_RECONCILE_MST
(
SNID,
SUID,
NOTAX_AMOUNT,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@SNID,
@SUID,
@NOTAX_AMOUNT,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY

)
";
        #endregion
        #region setsqlth
        string setsqlth = @"
UPDATE SU_RECONCILE_MST SET 
SUID=@SUID,
NOTAX_AMOUNT=@NOTAX_AMOUNT,
DATE=@DATE,
MAKERID=@MAKERID
";

        #region setsqlf
        string setsqlf = @" 
SELECT 
B.SNKEY AS 供应商对账索引,
A.SNID AS 供应商对账单号,
D.PUID AS 采购单号,
G.SN AS 项次,
H.WareID AS ID,
H.CO_WAREID AS 料号,
H.WName AS 品名,
H.CWAREID AS 客户料号,
H.SPEC AS 规格,
G.SKU AS 采购单位,
D.PCOUNT AS 采购数量,
E.PDATE AS 采购日期,
D.PURCHASEUNITPRICE AS 采购单价,
D.TaxRate AS 税率,
B.PRKEY AS 索引,
C.PGID AS 入库退货单号,
G.GECount AS 入库退货数量,
RTRIM(CONVERT(DECIMAL(18,2),G.GECount * D.PURCHASEUNITPRICE)) AS 未税金额,
RTRIM(CONVERT(DECIMAL(18,2),(G.GECount * D.PURCHASEUNITPRICE) * D.TaxRate/100  )) AS 税额,
RTRIM(CONVERT(DECIMAL(18,2),(G.GECount * D.PURCHASEUNITPRICE)* (1+D.TaxRate/100) )) AS 含税金额,
F.SUID AS 供应商代码,
F.SNAME AS 供应商名称,
(SELECT ENAME FROM EmployeeInfo WHERE EMID=A.MakerID ) AS 制单人,
A.Date AS 制单日期,
I.CONTACT AS 联系人,
I.PHONE AS 联系电话,
I.ADDRESS AS 送货地址,
I.EMAIL AS EMAIL
 ,(SELECT TOP 1 COName  FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) AS  公司名称
 ,(SELECT TOP 1 Address   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司地址
 ,(SELECT TOP 1 Phone   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司电话
 ,(SELECT TOP 1 Contact   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司联系人
 ,(SELECT TOP 1 Email   FROM CompanyInfo_MST A LEFT JOIN CompanyInfo_DET B ON A.COKEY=B.COKEY WHERE A.IF_DEFAULT =1) as 公司邮箱
FROM SU_RECONCILE_MST A 
LEFT JOIN SU_RECONCILE_DET B ON A.SNID=B.SNID 
LEFT JOIN PURCHASEGODE_DET C ON B.PRKEY =C.PGKEY 
LEFT JOIN PURCHASE_DET D ON D.PUID=C.PUID AND C.SN=D.SN 
LEFT JOIN PURCHASE_MST E ON D.PUID=E.PUID 
LEFT JOIN SUPPLIERINFO_MST F ON E.SUID =F.SUID 
LEFT JOIN GODE G ON C.PGKEY =G.GEKEY 
LEFT JOIN WareInfo H ON G.WareID =H.WareID
LEFT JOIN SUPPLIERINFO_DET I ON F.SUKEY=I.SUKEY
";
        #endregion
        #region setsqlfi
        string setsqlfi = @" 
SELECT 
B.SNKEY AS 供应商对账索引,
A.SNID AS 供应商对账单号,
D.PUID AS 采购单号,
G.SN AS 项次,
H.WareID AS ID,
H.CO_WAREID AS 料号,
H.WName AS 品名,
H.CWAREID AS 客户料号,
H.SPEC AS 规格,
G.SKU AS 采购单位,
D.PCOUNT AS 采购数量,
E.PDATE AS 采购日期,
D.PURCHASEUNITPRICE AS 采购单价,
D.TaxRate AS 税率,
B.PRKEY AS 索引,
C.REID AS 入库退货单号,
G.MRCount AS 入库退货数量,
RTRIM(CONVERT(DECIMAL(18,2),C.NOTAX_AMOUNT  )) AS 未税金额,
RTRIM(CONVERT(DECIMAL(18,2),C.TAX_AMOUNT  )) AS 税额,
RTRIM(CONVERT(DECIMAL(18,2),C.AMOUNT )) AS 含税金额,
F.SUID AS 供应商代码,
F.SNAME AS 供应商名称,
(SELECT ENAME FROM EmployeeInfo WHERE EMID=A.MakerID ) AS 制单人,
A.Date AS 制单日期,
I.CONTACT AS 联系人,
I.PHONE AS 联系电话,
I.ADDRESS AS 送货地址,
I.EMAIL AS EMAIL
FROM SU_RECONCILE_MST A 
LEFT JOIN SU_RECONCILE_DET B ON A.SNID=B.SNID 
LEFT JOIN RETURN_DET C ON B.PRKEY =C.REKEY 
LEFT JOIN PURCHASE_DET D ON D.PUID=C.PUID AND C.SN=D.SN 
LEFT JOIN PURCHASE_MST E ON D.PUID=E.PUID 
LEFT JOIN SUPPLIERINFO_MST F ON E.SUID =F.SUID 
LEFT JOIN MateRe  G ON C.REKEY =G.MRKEY 
LEFT JOIN WareInfo H ON G.WareID =H.WareID
LEFT JOIN SUPPLIERINFO_DET I ON F.SUKEY=I.SUKEY
";
        #endregion
        #endregion
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        CPURCHASE cPURCHASE = new CPURCHASE();
        StringBuilder sqb = new StringBuilder();
        public string MESSAGE { get; set; }
        public string PUID { get; set; }
        public CPURCHASE_RECONCILE()
        {
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM SU_RECONCILE_MST", "SNID", "SN");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        public DataTable DT_EMPTY()
        {

            DataTable dtt = new DataTable();
            dtt.Columns.Add("选择", typeof(bool));
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("供应商对账索引", typeof(string));
            dtt.Columns.Add("供应商对账单号", typeof(string));
            dtt.Columns.Add("采购单号", typeof(string));
            dtt.Columns.Add("目录项次", typeof(Int32));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("采购单位", typeof(string));
            dtt.Columns.Add("采购单价", typeof(decimal));
            dtt.Columns.Add("采购数量", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            dtt.Columns.Add("发票号码", typeof(string));
            dtt.Columns.Add("发票未税金额", typeof(decimal));
            dtt.Columns.Add("发票税额", typeof(decimal));
            dtt.Columns.Add("发票含税金额", typeof(decimal));
            dtt.Columns.Add("供应商代码", typeof(string));
            dtt.Columns.Add("供应商名称", typeof(string));
            dtt.Columns.Add("采购日期", typeof(string));
            dtt.Columns.Add("入库退货单号", typeof(string));
            dtt.Columns.Add("入库退货数量", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("合计未税金额", typeof(decimal));
            dtt.Columns.Add("合计税额", typeof(decimal));
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dtt.Columns.Add("备注", typeof(string));
            dtt.Columns.Add("联系人", typeof(string));
            dtt.Columns.Add("联系电话", typeof(string));
            dtt.Columns.Add("EMAIL", typeof(string));

            dtt.Columns.Add("公司名称", typeof(string));
            dtt.Columns.Add("公司地址", typeof(string));
            dtt.Columns.Add("公司联系人", typeof(string));
            dtt.Columns.Add("公司电话", typeof(string));
            dtt.Columns.Add("公司邮箱", typeof(string));
            return dtt;
        }
   
     
        #region RETURN_PG_AND_RETURN_DT
        public DataTable RETURN_PG_AND_RETURN_DT(string SNAME,string STARTDATE,string ENDDATE,string STATUS,string WNAME,string CWAREID,bool IF_USE_DATE,bool IF_VAGUE_SEARCH)
        {

            DataTable dtt = DT_EMPTY();
            sqb = new StringBuilder();
            MESSAGE = "";
            sqb.AppendFormat(new CPURCHASE_GODE ().sql );
            if (IF_VAGUE_SEARCH)
            {
                sqb.AppendFormat(" WHERE D.SNAME LIKE  '%" + SNAME + "%'");
            }
            else
            {
                sqb.AppendFormat(" WHERE D.SNAME= '" + SNAME + "'");
            }
          
            if (IF_USE_DATE)
            {
                sqb.AppendFormat(" AND E.Date>= '" + STARTDATE + "'  AND E.DATE<='" + ENDDATE + "'");
            }
            if (STATUS == "未对账")
            {
                sqb.AppendFormat(" AND A.STATUS='N'");
            }
            else if (STATUS == "已对账")
            {
                sqb.AppendFormat(" AND A.STATUS IN ('RECONCILE','INVOICE')");
            }
            sqb.AppendFormat(" AND B.WNAME LIKE '%{0}%'",WNAME );
            sqb.AppendFormat(" AND B.CWAREID LIKE '%{0}%'", CWAREID );
            sqb.AppendFormat("  ORDER BY  D.SUID ASC");
            MESSAGE = MESSAGE + sqb.ToString();
            DataTable dt4 = bc.getdt(sqb.ToString ());
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = true;
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["客户料号"] = dr1["客户料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["入库退货单号"] = dr1["入库单号"].ToString();
                    dr["入库退货数量"] = dr1["入库数量"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["采购单位"] = dr1["采购单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["索引"] = dr1["索引"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            sqb = new StringBuilder();
            sqb.AppendFormat(new CRETURN ().sql );
            if (IF_VAGUE_SEARCH)
            {
                sqb.AppendFormat(" WHERE D.SNAME LIKE  '%" + SNAME + "%'");
            }
            else
            {
                sqb.AppendFormat(" WHERE D.SNAME= '" + SNAME + "'");
            }
            if (IF_USE_DATE)
            {
                 sqb.AppendFormat(" AND E.Date>= '" + STARTDATE + "'  AND E.DATE<='"+ENDDATE +"'");
            }
            if (STATUS == "未对账")
            {
                sqb.AppendFormat(" AND A.STATUS='N'");
            }
            else if (STATUS == "已对账")
            {
                sqb.AppendFormat(" AND A.STATUS IN ('RECONCILE','INVOICE')");
            }
            sqb.AppendFormat(" AND B.WNAME LIKE '%{0}%'", WNAME);
            sqb.AppendFormat(" AND B.CWAREID LIKE '%{0}%'", CWAREID);
            sqb.AppendFormat("  ORDER BY  D.SUID ASC");
            dt4 = basec.getdts(sqb.ToString ());
            MESSAGE = MESSAGE + "*********************"+sqb.ToString();
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = true;
                    decimal d1 = decimal.Parse(dr1["退货未税金额"].ToString());
                    decimal d2 = -d1;
                    decimal d3 = decimal.Parse(dr1["退货税额"].ToString());
                    decimal d4 = -d3;
                    decimal d5 = -decimal.Parse(dr1["退货含税金额"].ToString());
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["客户料号"] = dr1["客户料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["入库退货单号"] = dr1["退货单号"].ToString();
                    dr["入库退货数量"] = dr1["退货数量"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["采购单位"] = dr1["单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = d2;
                    dr["税额"] = d4;
                    dr["含税金额"] = d5;
                    dr["索引"] = dr1["索引"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                 
                }

            }
            if (dtt.Rows.Count > 0)
            {
                DataRow dr2 = dtt.NewRow();
                dr2["选择"] = true;
                dr2["未税金额"] = dtt.Compute("SUM(未税金额)", "");
                dr2["税额"] = dtt.Compute("SUM(税额)", "");
                dr2["含税金额"] = dtt.Compute("SUM(含税金额)", "");
                dtt.Rows.Add(dr2);
            }

            
            return dtt;
        }
        #endregion
        #region RETURN_PG_AND_RETURN_DT /*发票使用数据*/
        public DataTable RETURN_PG_AND_RETURN_DT()
        {

            DataTable dtt = DT_EMPTY();
            sqb = new StringBuilder();
            MESSAGE = "";
            sqb.AppendFormat(new CPURCHASE_GODE().sql);
            sqb.AppendFormat(" WHERE  A.STATUS NOT IN ('INVOICE')");//状态为没有开过发票的采购入库单
            sqb.AppendFormat("  ORDER BY  D.SUID,A.PGID,A.SN ASC");
            MESSAGE = MESSAGE + sqb.ToString();
            DataTable dt4 = bc.getdt(sqb.ToString());
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = true;
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["客户料号"] = dr1["客户料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["入库退货单号"] = dr1["入库单号"].ToString();
                    dr["入库退货数量"] = dr1["入库数量"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["采购单位"] = dr1["采购单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();

                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            sqb = new StringBuilder();
            sqb.AppendFormat(new CRETURN().sql);
            sqb.AppendFormat(" WHERE  A.STATUS NOT IN ('INVOICE')");//状态为没有开过发票的采购入库单
            sqb.AppendFormat("  ORDER BY  D.SUID,A.REID,A.SN ASC");
            dt4 = basec.getdts(sqb.ToString());
            MESSAGE = MESSAGE + "*********************" + sqb.ToString();
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    decimal d1 = decimal.Parse(dr1["退货未税金额"].ToString());
                    decimal d2 = -d1;
                    decimal d3 = decimal.Parse(dr1["退货税额"].ToString());
                    decimal d4 = -d3;
                    decimal d5 = -decimal.Parse(dr1["退货含税金额"].ToString());
                    dr["选择"] = true;
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["客户料号"] = dr1["客户料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["入库退货单号"] = dr1["退货单号"].ToString();
                    dr["入库退货数量"] = dr1["退货数量"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["采购单位"] = dr1["单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = d2;
                    dr["税额"] = d4;
                    dr["含税金额"] = d5;

                    dtt.Rows.Add(dr);
                    i = i + 1;

                }

            }
            dtt = bc.GET_DT_TO_DV_TO_DT(dtt, " 供应商名称 asc", "");//排序可能引起数据加载过慢
            if (dtt.Rows.Count > 0)
            {
                for (i = 0; i < dtt.Rows.Count; i++)
                {
                    dtt.Rows[i]["目录项次"] = (i + 1).ToString();//再次对目前项次排序，可能引起数据加载过慢
                }
            }
            if (dtt.Rows.Count > 0)
            {
                DataRow dr2 = dtt.NewRow();
                dr2["选择"] = true;
                dr2["未税金额"] = dtt.Compute("SUM(未税金额)", "");
                dr2["税额"] = dtt.Compute("SUM(税额)", "");
                dr2["含税金额"] = dtt.Compute("SUM(含税金额)", "");
                dtt.Rows.Add(dr2);
            }


            return dtt;
        }
        #endregion
        #region RETURN_PG_AND_RETURN_DT2/*查询有对账单的数据 VIEW*/
        public DataTable RETURN_PG_AND_RETURN_DT2(string SNID, string SNAME, string STARTDATE, string ENDDATE, string STATUS, string WNAME, string CWAREID, bool IF_USE_DATE, bool IF_VAGUE_SEARCH)
        {

            DataTable dtt = DT_EMPTY();
            MESSAGE = "";
            sqb.AppendFormat(sqlf);
            if (IF_VAGUE_SEARCH)
            {
                sqb.AppendFormat(" WHERE F.SNAME LIKE  '%" + SNAME + "%'");
            }
            else
            {
                sqb.AppendFormat(" WHERE F.SNAME= '" + SNAME + "'");
            }
            sqb.AppendFormat(" AND A.SNID LIKE '%{0}%'", SNID);
            sqb.AppendFormat(" AND D.PUID LIKE '%{0}%'", PUID);
            if (IF_USE_DATE)
            {
                sqb.AppendFormat(" AND convert(varchar(10),A.Date,112)>= '" + STARTDATE + "'  AND convert(varchar(10),A.Date,112)<='" + ENDDATE + "'");
            }
            if (STATUS == "未对账")
            {
                sqb.AppendFormat(" AND C.STATUS='N'");
            }
            else if (STATUS == "已对账")
            {
                sqb.AppendFormat(" AND C.STATUS IN ('RECONCILE','INVOICE')");
            }
            sqb.AppendFormat(" AND H.WNAME LIKE '%{0}%'", WNAME);
            sqb.AppendFormat(" AND H.CWAREID LIKE '%{0}%'", CWAREID);
            sqb.AppendFormat(" AND SUBSTRING (B.PRKEY,1,2)='PG' ORDER BY D.SUID ASC");
            MESSAGE = sqb.ToString();
            DataTable dt4 = basec.getdts(sqb.ToString());
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = true;
                    dr["索引"] = dr1["索引"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["入库退货单号"] = dr1["入库退货单号"].ToString();
                    dr["入库退货数量"] = dr1["入库退货数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["采购单位"] = dr1["采购单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购日期"] = dr1["采购日期"].ToString();
                    dr["供应商对账索引"] = dr1["供应商对账索引"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["联系人"] = dr1["联系人"].ToString();
                    dr["联系电话"] = dr1["联系电话"].ToString();
                    dr["EMAIL"] = dr1["EMAIL"].ToString();
                    dr["供应商对账单号"] = dr1["供应商对账单号"].ToString();

                    dr["公司名称"] = dr1["公司名称"].ToString();
                    dr["公司地址"] = dr1["公司地址"].ToString();
                    dr["公司联系人"] = dr1["公司联系人"].ToString();
                    dr["公司电话"] = dr1["公司电话"].ToString();
                    dr["公司邮箱"] = dr1["公司邮箱"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            sqb = new StringBuilder();
            sqb.AppendFormat(sqlfi);
            if (IF_VAGUE_SEARCH)
            {
                sqb.AppendFormat(" WHERE F.SNAME LIKE  '%" + SNAME + "%'");
            }
            else
            {
                sqb.AppendFormat(" WHERE F.SNAME= '" + SNAME + "'");
            }
            if (IF_USE_DATE)
            {
                sqb.AppendFormat(" AND convert(varchar(10),A.Date,112)>= '" + STARTDATE + "'  AND convert(varchar(10),A.Date,112)<='" + ENDDATE + "'");
            }
            if (STATUS == "未对账")
            {
                sqb.AppendFormat(" AND C.STATUS='N'");
            }
            else if (STATUS == "已对账")
            {
                sqb.AppendFormat(" AND C.STATUS IN ('RECONCILE','INVOICE')");
            }
            sqb.AppendFormat(" AND A.SNID LIKE '%{0}%'", SNID);
            sqb.AppendFormat(" AND D.PUID LIKE '%{0}%'", PUID);
            sqb.AppendFormat(" AND H.WNAME LIKE '%{0}%'", WNAME);
            sqb.AppendFormat(" AND H.CWAREID LIKE '%{0}%'", CWAREID);
            sqb.AppendFormat(" AND SUBSTRING (B.PRKEY,1,2)='RE' ORDER BY D.SUID ASC");
            dt4 = basec.getdts(sqb.ToString());
            MESSAGE = MESSAGE + "/*********************/" + sqb.ToString();
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    decimal d1 = decimal.Parse(dr1["未税金额"].ToString());
                    decimal d2 = -d1;
                    decimal d3 = decimal.Parse(dr1["税额"].ToString());
                    decimal d4 = -d3;
                    decimal d5 = decimal.Parse(dr1["含税金额"].ToString());
                    decimal d6 = -d5;
                    dr["选择"] = true;
                    dr["索引"] = dr1["索引"].ToString();
                    dr["采购单号"] = dr1["采购单号"].ToString();
                    dr["入库退货单号"] = dr1["入库退货单号"].ToString();
                    dr["入库退货数量"] = dr1["入库退货数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["采购单位"] = dr1["采购单位"].ToString();
                    dr["采购单价"] = dr1["采购单价"].ToString();
                    dr["采购数量"] = dr1["采购数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = d2;
                    dr["税额"] = d4;
                    dr["含税金额"] = d6;
                    dr["供应商代码"] = dr1["供应商代码"].ToString();
                    dr["供应商名称"] = dr1["供应商名称"].ToString();
                    dr["采购日期"] = dr1["采购日期"].ToString();
                    dr["供应商对账索引"] = dr1["供应商对账索引"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["联系人"] = dr1["联系人"].ToString();
                    dr["联系电话"] = dr1["联系电话"].ToString();
                    dr["EMAIL"] = dr1["EMAIL"].ToString();
                    dr["供应商对账单号"] = dr1["供应商对账单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            DataRow dr2 = dtt.NewRow();
            dr2["选择"] = true;
            dr2["未税金额"] = dtt.Compute("SUM(未税金额)", "");
            dr2["税额"] = dtt.Compute("SUM(税额)", "");
            dr2["含税金额"] = dtt.Compute("SUM(含税金额)", "");
            dtt.Rows.Add(dr2);
            dtt = bc.GET_DT_TO_DV_TO_DT(dtt, "供应商对账单号 DESC", "");/*按对账单号排序可能会使用加载过慢*/
            return dtt;
        }
        #endregion
    }
}
