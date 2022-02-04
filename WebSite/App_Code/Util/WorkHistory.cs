using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Data;

namespace FirstOrder.Util
{
	/// <summary>
	/// WorkHistory의 요약 설명입니다.
	/// </summary>
	public class WorkHistory
	{
        private StCommon st = new StCommon();//공통함수 선언
        private SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

        private string UserID;
        private string myUserName;
        private string myUserPID;
		private string myComputerName;
		private string sDate;
		private string sTime;

		public WorkHistory()
		{
            UserID = MemberData.GetLoginSID("LoginID");
            myUserName = MemberData.GetLoginSID("KureName");
            myComputerName = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			sDate = DateTime.Now.ToString("yyyy-MM-dd");
			sTime = DateTime.Now.ToString("HH:mm:ss");
		}

		public void InsertWork(string _gubun, string _gubun1, DbCommand _cmd)
		{
			StringBuilder sb = new StringBuilder();
			
			sb.AppendFormat("{0}:", _cmd.CommandText);
			for (int i = 0; i < _cmd.Parameters.Count; i++)
			{
				string paramValue = _cmd.Parameters[i].Value.ToString();
				
				if (!string.IsNullOrEmpty(paramValue))
				{
					sb.AppendFormat("{0}={1},", _cmd.Parameters[i].ParameterName, paramValue);
				}
			}

			Insert(myUserName, myComputerName, _gubun, _gubun1, sDate, sTime, sb.ToString().Substring(0,sb.ToString().Length-1));
		}

		public void InsertWork(string _gubun, string _gubun1, string _conent)
		{
			string conent = StCommon.ReplaceSQ(_conent);

            try
            {
                Insert(myUserName, myComputerName, _gubun, _gubun1, sDate, sTime, conent);
            }
            catch (Exception ex)
            {
                st.LogException(ex);
            }
		}

		private int Insert(string myUserName, string myComputerName, string gubun, string gubun1, string sDate, string sTime, string content)
		{
            string qry = string.Empty;

            qry = " Insert Into gblWORKHISTORY ";
            qry = qry + " ([ID],[NAME],[MYCOMPUTERNAME],[SDATE],[STIME],[CONTENT],[GUBUN],[GUBUN1]) ";
            qry = qry + " Values ";
            qry = qry + " (@ID,@NAME,@MYCOMPUTERNAME,@SDATE,@STIME,@CONTENT,@GUBUN,@GUBUN1) ";

            DbCommand cmd = db.GetSqlStringCommand(qry);

            if (UserID == "")
            {
                UserID = MemberData.GetLoginSID("LoginID");
            }
            if (myUserName == "")
            {
                myUserName = MemberData.GetLoginSID("KureName");
            }

            db.AddInParameter(cmd, "ID", DbType.String, UserID);
            db.AddInParameter(cmd, "NAME", DbType.String, myUserName);
            db.AddInParameter(cmd, "MYCOMPUTERNAME", DbType.String, myComputerName);
			db.AddInParameter(cmd, "SDATE", DbType.String, sDate);
			db.AddInParameter(cmd, "STIME", DbType.String, sTime);
			db.AddInParameter(cmd, "CONTENT", DbType.String, content);
			db.AddInParameter(cmd, "GUBUN", DbType.String, gubun);
			db.AddInParameter(cmd, "GUBUN1", DbType.String, gubun1);
			
			return db.ExecuteNonQuery(cmd);
		}
	}
}