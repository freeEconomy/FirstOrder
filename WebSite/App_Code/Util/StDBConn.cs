using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace FirstOrder.Util
{
	public enum OpenDBType
	{
        mainDB,
        tbucDB
    }

	/// <summary>
	/// DBConn의 요약 설명입니다.
	/// </summary>
	public class StDBConn
	{
		public StDBConn()
		{
		}

		public static SqlDatabase GetOpenDB(OpenDBType _dbType)
		{
            return new SqlDatabase(System.Configuration.ConfigurationManager.AppSettings[_dbType.ToString()]);
        }

        public static string GetConntionString(OpenDBType _dbType)
        {
            return System.Configuration.ConfigurationManager.AppSettings[_dbType.ToString()];
        }

		public static string GetExcelProvider(string _fullPath, string _extFileName)
		{
			string strProvider = string.Empty;
            
			if (_extFileName.ToLower() == "xls")
			{
				strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _fullPath + "; Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1\"";
			}
			else if (_extFileName.ToLower() == "xlsx")
			{
				strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _fullPath + "; Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1\"";
			}

			return strProvider;
		}

		public static string GetExcelProvider(string _fullPath, string _extFileName, string _isHdr)
		{
            string strProvider = string.Empty;
            
			if (_extFileName.ToLower() == "xls")
			{
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _fullPath + "; Extended Properties=\"Excel 8.0;HDR=" + _isHdr + ";IMEX=1\"";
			}
			else if (_extFileName.ToLower() == "xlsx")
			{
				strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + _fullPath + "; Extended Properties=\"Excel 8.0;HDR=" + _isHdr + ";IMEX=1\"";
			}

			return strProvider;
		}
	}
}