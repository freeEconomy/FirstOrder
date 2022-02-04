using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace FirstOrder.Util
{
    /// <summary>
    /// Common의 요약 설명입니다.
    /// </summary>
    public class StDataCommon
    {
        private SqlDatabase m_db;
        private string qry = string.Empty;

        public StDataCommon()
        {
            m_db = StDBConn.GetOpenDB(OpenDBType.mainDB);
        }

        public StDataCommon(SqlDatabase _db)
        {
            m_db = _db;
        }
        
        public IDataReader GetDataReader(string _qry)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            return m_db.ExecuteReader(cmd);
        }

        public IDataReader GetDataReader(DbCommand _cmd)
        {
            return m_db.ExecuteReader(_cmd);
        }

        public DataSet GetDataSet(string _qry)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);
            cmd.CommandTimeout = 1000;
            return m_db.ExecuteDataSet(cmd);
        }

        public DataSet GetDataSet(string _qry, int _timeOut)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            cmd.CommandTimeout = _timeOut;

            return m_db.ExecuteDataSet(cmd);
        }

        public DataSet GetDataSet(DbCommand _cmd)
        {
            return m_db.ExecuteDataSet(_cmd);
        }

        public int GetExecuteNonQry(string _qry)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            return m_db.ExecuteNonQuery(cmd);
        }

        public int GetExecuteNonQry(string _qry, int _timeOut)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            cmd.CommandTimeout = _timeOut;

            return m_db.ExecuteNonQuery(cmd);
        }

        public int GetCount(string _qry)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            return Convert.ToInt32(m_db.ExecuteScalar(cmd));
        }

        public object GetDataOne(string _qry)
        {
            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            return m_db.ExecuteScalar(cmd);
        }        
    }

    public class MakePaging
    {
        private SqlDatabase m_db;
        private int m_totalCount = 0;
        private int m_totalPage = 0;
        private int m_pageSize = 10;
        private int m_page = 0;
        private bool m_isPaging = true;
        private string m_whereQryTotPaging = string.Empty;
        private string m_tableName = string.Empty;
        private string m_qryEnc = string.Empty;
        private string m_orderQry = string.Empty;

        public int TotalCount
        {
            get { return m_totalCount; }
            set { m_totalCount = value; }
        }

        public int TotalPage
        {
            get { return m_totalPage; }
            set { m_totalPage = value; }
        }

        public int PageSize
        {
            get { return PageSize; }
            set { PageSize = value; }
        }

        public int NowPage
        {
            get { return m_page; }
            set { m_page = value; }
        }

        public bool IsPaging
        {
            get { return m_isPaging; }
            set { m_isPaging = value; }
        }

        public string WhereQryTotPaging
        {
            get { return m_whereQryTotPaging; }
            set { m_whereQryTotPaging = value; }
        }

        public string TableName
        {
            get { return m_tableName; }
            set { m_tableName = value; }
        }

        public string QryEnc
        {
            get { return m_qryEnc; }
            set { m_qryEnc = value; }
        }

        public string OrderQry
        {
            get { return m_orderQry; }
            set { m_orderQry = value; }
        }

        public MakePaging(string _whereQryTotPaging, string _tableName, string _qryEnc, string _orderQry, int _page, int _pageSize, bool _isPaging)
        {
            m_db = StDBConn.GetOpenDB(OpenDBType.mainDB);
            m_whereQryTotPaging = _whereQryTotPaging;
            m_tableName = _tableName;
            m_qryEnc = _qryEnc;
            m_orderQry = _orderQry;
            m_page = _page;
            m_isPaging = _isPaging;
            m_pageSize = _pageSize;
        }

        private int GetTotalCounts(string _qry)
        {
            int returnValue = 0;

            DbCommand cmd = m_db.GetSqlStringCommand(_qry);

            returnValue = Convert.ToInt32(m_db.ExecuteScalar(cmd));

            return returnValue;
        }

        private int GetTotalPage(int _totalCount, int _pageSize)
        {
            int returnValue = 0;

            returnValue = Convert.ToInt32((_totalCount - 1) / _pageSize) + 1;

            return returnValue;
        }

        private string MakePagingIndexQry(int _page, int _pageSize)
        {
            string returnValue = string.Empty;

            returnValue = string.Format("(pgIdx between ({0}-1) * {1} + 1 and ({0} * {1}))", _page, _pageSize);

            return returnValue;
        }

        public string MakeSetTotalPagindQry()
        {
            StringBuilder sbQryPaging = new StringBuilder();

            string returnValue = string.Empty;
            string whereQry = string.Empty;

            string qryT = "SELECT Count(*) as cnt FROM " + m_tableName + " WHERE (1=1) " + m_whereQryTotPaging;

            m_totalCount = GetTotalCounts(qryT);
            m_totalPage = GetTotalPage(m_totalCount, m_pageSize);
            string pagingQry = MakePagingIndexQry(m_page, m_pageSize);

            sbQryPaging.AppendLine("SELECT * FROM ");
            sbQryPaging.AppendLine("(");
            sbQryPaging.AppendFormat("SELECT ROW_NUMBER() Over ({0}) as pgIdx,", m_orderQry);
            sbQryPaging.AppendLine();
            sbQryPaging.AppendLine("*");
            if (!string.IsNullOrWhiteSpace(m_qryEnc))
            {
                sbQryPaging.AppendLine("," + m_qryEnc);
            }
            sbQryPaging.AppendFormat("FROM {0}", m_tableName);
            sbQryPaging.AppendLine();
            sbQryPaging.AppendLine("WHERE");
            sbQryPaging.AppendLine("(1=1)");
            sbQryPaging.AppendLine(m_whereQryTotPaging);
            sbQryPaging.AppendLine(") as a");

            if (m_isPaging == false)
            {

            }
            else
            {
                sbQryPaging.AppendFormat("WHERE ({0}) {1}", pagingQry, m_orderQry);
            }

            returnValue = sbQryPaging.ToString();

            return returnValue;
        }

        public string MakeSetTotalPagindQry(string chk)
        {
            StringBuilder sbQryPaging = new StringBuilder();

            string returnValue = string.Empty;
            string whereQry = string.Empty;

            string qryT = "SELECT Count(*) as cnt FROM " + m_tableName + " WHERE (1=1) " + m_whereQryTotPaging;

            m_totalCount = GetTotalCounts(qryT);
            m_totalPage = GetTotalPage(m_totalCount, m_pageSize);
            string pagingQry = MakePagingIndexQry(m_page, m_pageSize);

            sbQryPaging.AppendLine("SELECT * FROM ");
            sbQryPaging.AppendLine("(");
            sbQryPaging.AppendFormat("SELECT ROW_NUMBER() Over ({0}) as pgIdx,", m_orderQry);
            sbQryPaging.AppendLine();

            if (chk == "*")
            {
                sbQryPaging.AppendLine("*");
            }
            else
            {
                sbQryPaging.AppendLine("1 as temp");
            }

            if (!string.IsNullOrWhiteSpace(m_qryEnc))
            {
                sbQryPaging.AppendLine("," + m_qryEnc);
            }
            sbQryPaging.AppendFormat("FROM {0}", m_tableName);
            sbQryPaging.AppendLine();
            sbQryPaging.AppendLine("WHERE");
            sbQryPaging.AppendLine("(1=1)");
            sbQryPaging.AppendLine(m_whereQryTotPaging);
            sbQryPaging.AppendLine(") as a");

            if (m_isPaging == false)
            {

            }
            else
            {
                sbQryPaging.AppendFormat("WHERE ({0}) {1}", pagingQry, m_orderQry);
            }

            returnValue = sbQryPaging.ToString();

            return returnValue;
        }
    }
}