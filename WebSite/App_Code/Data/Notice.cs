using System;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Util;

namespace FirstOrder.Data
{
	/// <summary>
	/// WeekPlan의 요약 설명입니다.
	/// </summary>
	public class Notice
    {
		SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);
        WorkHistory whis = new WorkHistory();

		private int m_idx;
		private string m_noticeday;
        private string m_noticeday2;
        private string m_memo;
		private string m_fileName;
		private bool m_isNotice;

        private string preVal;

        public Notice(string checkPreVal)
        {
            preVal = checkPreVal;
        }
		public Notice(string checkPreVal, int checkIDX)
        {
            preVal = checkPreVal;
            m_idx = checkIDX;
        }

		public int IDX
		{
			set { m_idx = value; }
		}
		public string NoticeDay
		{
			get { return m_noticeday; }
			set { m_noticeday = value; }
        }
        public string NoticeDay2
        {
            get { return m_noticeday2; }
            set { m_noticeday2 = value; }
        }
        public string MemoData
		{
			get { return m_memo; }
			set { m_memo = value; }
		}
		public string FileName
		{
			get { return m_fileName; }
			set { m_fileName = value; }
		}
		public bool IsNotice
        {
			get { return m_isNotice; }
			set { m_isNotice = value; }
		}

		public void InsertData()
		{
            string qry = "INSERT INTO " + preVal + "NOTICE (memoday,noticeday,noticeday2,[memo],[fileName],[isNotice]) VALUES (@memoday,@noticeday,@noticeday2,@memo,@fileName,@isNotice)";

			DbCommand cmd = db.GetSqlStringCommand(qry);

			db.AddInParameter(cmd, "@memoday", DbType.String, DateTime.Now.ToShortDateString());
            db.AddInParameter(cmd, "@noticeday", DbType.String, m_noticeday);
            db.AddInParameter(cmd, "@noticeday2", DbType.String, m_noticeday2);
            db.AddInParameter(cmd, "@memo", DbType.String, m_memo);
			db.AddInParameter(cmd, "@fileName", DbType.String, m_fileName);
			db.AddInParameter(cmd, "@isNotice", DbType.Boolean, m_isNotice);

			db.ExecuteNonQuery(cmd);

			whis.InsertWork("공지사항", "등록", cmd);
		}

		public void UpdateData()
		{
            string qry = "UPDATE " + preVal + "NOTICE SET noticeday=@noticeday,noticeday2=@noticeday2,[memo]=@memo,[fileName]=@fileName,[isNotice]=@isNotice WHERE u=@u";

			DbCommand cmd = db.GetSqlStringCommand(qry);

            db.AddInParameter(cmd, "@noticeday", DbType.String, m_noticeday);
            db.AddInParameter(cmd, "@noticeday2", DbType.String, m_noticeday2);
            db.AddInParameter(cmd, "@memo", DbType.String, m_memo);
			db.AddInParameter(cmd, "@fileName", DbType.String, m_fileName);
			db.AddInParameter(cmd, "@isNotice", DbType.Boolean, m_isNotice);
			db.AddInParameter(cmd, "@u", DbType.Int32, m_idx);

			db.ExecuteNonQuery(cmd);

			whis.InsertWork("공지사항", "수정", cmd);
		}

		public void DeleteData()
		{
			string qry = "DELETE FROM " + preVal + "NOTICE WHERE u=@u";

			DbCommand cmd = db.GetSqlStringCommand(qry);

			db.AddInParameter(cmd, "@u", DbType.Int32, m_idx);

			db.ExecuteNonQuery(cmd);

			whis.InsertWork("공지사항", "삭제", cmd);
		}

		public void GetNoticeInfoOne()
		{
			string qry = "SELECT * FROM " + preVal + "NOTICE WHERE u=@u";
			
			DbCommand cmd = db.GetSqlStringCommand(qry);
			db.AddInParameter(cmd, "@u", DbType.Int32, m_idx);

			using (IDataReader dr = db.ExecuteReader(cmd))
			{
				if (dr.Read())
				{
                    m_noticeday = dr["noticeday"].ToString().Trim();
                    m_noticeday2 = dr["noticeday2"].ToString().Trim();
                    m_memo = dr["memo"].ToString().Trim();
					m_fileName = dr["fileName"].ToString().Trim();
                    m_isNotice = Convert.ToBoolean(dr["isNotice"]);
				}
				dr.Close();
			}
		}
	}
}