using System;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Util;
using System.Web;

namespace FirstOrder.Data
{
   public class MemberData
    {
        //암호 만들기
        //TEMP_PASSWORD = Trim(ucText_Password1.Text)          '사용자가 입력한 암호
        //TEMP_PASSDATE = Format(Now, "yyyy-mm-dd hh:Nn:ss")   '기준이 되는 날짜시간
        //TEMP_PASSWORD = PASSWORD_ENCODE(TEMP_PASSDATE, TEMP_PASSWORD)
        public static string Password_Encode(string passDateTime, string passwordData)
        {
            int ii, jj, kk;
            int[] divide_DateTime = new int[20];
            int[] divide_PassData = new int[20];
            string full_Data = "";
            string temp_Encode = "";

            char[] ss = passDateTime.ToCharArray();

            for (ii = 0; ii < ss.Length; ii++)
            {
                kk = (passDateTime.Length - ii) - 1;
                divide_DateTime[ii] = Asc(ss[kk]);  //한 자리씩 변환한 DateTime을 ASCII값으로 변환한다.

                //Console.WriteLine($"{ii} {ss[kk]} ---> {divide_DateTime[ii]}");
            }

            //full_Data = (passwordData + Chr(2) + passDateTime).Substring(0, passDateTime.Length);
            full_Data = Left(passwordData + Chr(2) + passDateTime, passDateTime.Length);

            char[] tt = full_Data.ToCharArray();

            for (ii = 0; ii < tt.Length; ii++)
            {
                divide_PassData[ii] = Asc(tt[ii]);  //한 자리씩 변환한 입력암를 ASCII값으로 변환한다.

                //Console.WriteLine($"{ii} {tt[ii]} ---> {divide_PassData[ii]}");
            }

            for (ii = 0; ii < passDateTime.Length; ii++)
            {
                jj = 0;
                jj = jj + divide_PassData[ii];
                jj = jj + divide_DateTime[ii];
                jj = jj + (ii + 1);
                temp_Encode = temp_Encode + Right(("000" + Convert.ToString(jj, 16).ToUpper()), 3);  //3자리씩 자른 16진수 값을 10진수로 변경한다.

                //Console.WriteLine($"{divide_PassData[ii]} + {divide_DateTime[ii]} + {ii+1} ===> {jj} -> {temp_Encode}");
            }
            return temp_Encode;
        }

        //암호 풀기
        //TEMP_PASSWORD = Trim(SAWN_TB!Sawn_PassWord & "")   '최초에 사용자가 입력한 암호를 암호화해서 DB에 저장한 값
        //TEMP_PASSDATE = Trim(SAWN_TB!Sawn_PassDate & "")   '최초에 사용자가 입력할 때를 기억한 날짜-시간을 DB에 저장한 값
        //COMMON_PASSWORD = PASSWORD_DECODE(TEMP_PASSDATE, TEMP_PASSWORD)
        public static string Password_Decode(string passDateTime, string passwordData)
        {
            int ii, jj, kk;
            int[] divide_DateTime = new int[20];
            int[] divide_PassData = new int[20];

            string temp_Decode = "";

            char[] ss = passDateTime.ToCharArray();

            for (ii = 0; ii < ss.Length; ii++)
            {
                kk = (passDateTime.Length - ii) - 1;
                divide_DateTime[ii] = Asc(ss[kk]);  //한 자리씩 변환한 ss에서 ASCII값으로 변환한다.

                //Console.WriteLine($"{ii} {ss[kk]} ---> {divide_DateTime[ii]}");
            }

            kk = -1;
            for (ii = 0; ii < passwordData.Length; ii += 3)
            {
                kk = kk + 1;
                //divide_PassData[kk] = Convert.ToInt32(passwordData.Substring(ii, 3), 16); //3자리씩 자른 16진수 값을 10진수로 변경한다.
                divide_PassData[kk] = Convert.ToInt32(Mid(passwordData, ii, 3), 16);      //3자리씩 자른 16진수 값을 10진수로 변경한다.

                //Console.WriteLine($"{ii} ---> {kk} {divide_PassData[kk]}");
            }

            for (ii = 0; ii < passDateTime.Length; ii++)
            {
                jj = 0;
                jj = jj + divide_PassData[ii];
                jj = jj - divide_DateTime[ii];
                jj = jj - (ii + 1);

                if (jj > 0)
                {
                    if (Chr(Convert.ToByte(jj)) == Chr(2)) break;
                    temp_Decode = temp_Decode + Chr(Convert.ToByte(jj));
                }
                //Console.WriteLine($"{divide_PassData[ii]} - {divide_DateTime[ii]} = {ii} ===> {jj} -> {temp_Decode}");
            }
            return temp_Decode;
        }

        public static byte Asc(char src) { return (System.Text.Encoding.ASCII.GetBytes(src + "")[0]); }
        public static char Chr(byte src) { return (System.Text.Encoding.ASCII.GetChars(new byte[] { src })[0]); }

        public static string Left(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth) TextLenth = Text.Length;
            ConvertText = Text.Substring(0, TextLenth);
            return ConvertText;
        }

        public static string Right(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth) TextLenth = Text.Length;
            ConvertText = Text.Substring(Text.Length - TextLenth, TextLenth);
            return ConvertText;
        }

        public static string Mid(string Text, int Startint, int Endint)
        {
            string ConvertText;
            if (Startint < Text.Length || Endint < Text.Length)
            {
                ConvertText = Text.Substring(Startint, Endint);
                return ConvertText;
            }
            else
                return Text;
        }

        public static bool ExistLoginID(string Kure_Code)
        {
            bool result = false;

            SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

            StringBuilder sb = new StringBuilder();

            sb.Append("select * from gblKURE where ");
            sb.Append("Kure_Code=@Kure_Code");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Kure_Code", DbType.String, Kure_Code);

            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        public static string[] GetPassDate(string Kure_Code)
        {
            string[] result = { "", "", "", "" };

            SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

            StringBuilder sb = new StringBuilder();

            sb.Append("select * from gblKURE where ");
            sb.Append("Kure_Code=@Kure_Code");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Kure_Code", DbType.String, Kure_Code);

            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0]["Kure_PassDate"].ToString();
                result[1] = ds.Tables[0].Rows[0]["Kure_Password"].ToString();
                result[2] = ds.Tables[0].Rows[0]["Kure_Sangho"].ToString();
                result[3] = ds.Tables[0].Rows[0]["Kure_Status"].ToString();
            }

            return result;
        }

        public static void UpdatePassWord(string Kure_Code, string Kure_Password)
        {   
            SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

            StringBuilder sb = new StringBuilder();
            
            string Kure_PassDate = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
            
            sb.Append(" Update gblKURE ");
            sb.Append(" Set Kure_PassDate=@Kure_PassDate, Kure_Password=@Kure_Password, Kure_ModifyDate=@Kure_ModifyDate ");
            sb.Append(" Where Kure_Code=@Kure_Code ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Kure_PassDate", DbType.String, Kure_PassDate);
            db.AddInParameter(cmd, "@Kure_Password", DbType.String, Kure_Password);
            db.AddInParameter(cmd, "@Kure_ModifyDate", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@Kure_Code", DbType.String, Kure_Code);

            db.ExecuteNonQuery(cmd);

            WorkHistory whis = new WorkHistory();
            whis.InsertWork("거래처", "암호수정", cmd);
        }

        public static void CreatePassWord(string Kure_Code, string Kure_Password)
        {
            SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

            StringBuilder sb = new StringBuilder();

            string Kure_PassDate = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);

            sb.Append(" Update gblKURE ");
            sb.Append(" Set Kure_PassDate=@Kure_PassDate, Kure_Password=@Kure_Password, Kure_ModifyDate=@Kure_ModifyDate ");
            sb.Append(" Where Kure_Code=@Kure_Code ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Kure_PassDate", DbType.String, Kure_PassDate);
            db.AddInParameter(cmd, "@Kure_Password", DbType.String, Kure_Password);
            db.AddInParameter(cmd, "@Kure_ModifyDate", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@Kure_Code", DbType.String, Kure_Code);

            db.ExecuteNonQuery(cmd);

            WorkHistory whis = new WorkHistory();
            whis.InsertWork("거래처", "암호생성", cmd);
        }

        public static void LoginSession(string LoginID, string KureCode, string KureName)
        {
            HttpContext.Current.Session["LoginID"] = LoginID.ToUpper();
            HttpContext.Current.Session["KureCode"] = KureCode.ToUpper();
            HttpContext.Current.Session["KureName"] = KureName;

            WorkHistory whis = new WorkHistory();
            whis.InsertWork("로그인", "성공", "");

            SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

            StringBuilder sb = new StringBuilder();

            sb.Append("select substring(Kure_AreaGubun,1,1) as AreaGubun from gblKure where Kure_Code = '" + KureCode.ToUpper() + "' ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            DataSet ds = db.ExecuteDataSet(cmd);

            string areaGubun = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                areaGubun = ds.Tables[0].Rows[0]["AreaGubun"].ToString();
            }
            areaGubun = (areaGubun == "1") ? "1" : "2";

            sb = new StringBuilder();

            sb.Append("select * from gblCommon where Common_Key = 'T0311' ");

            cmd = db.GetSqlStringCommand(sb.ToString());
            
            ds = db.ExecuteDataSet(cmd);

            HttpContext.Current.Session["tblBljuMin"] = "20";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Common_Code"].ToString() == "tbl")
                {
                    HttpContext.Current.Session["tblBljuMin"] = ds.Tables[0].Rows[i]["Common_Remark" + areaGubun].ToString();
                    HttpContext.Current.Session["tblSizeNum"] = ds.Tables[0].Rows[i]["Common_Remark3"].ToString();
                    //HttpContext.Current.Session["tblSizeNum"] = "12";
                }
                else if (ds.Tables[0].Rows[i]["Common_Code"].ToString() == "abl")
                {
                    HttpContext.Current.Session["ablBljuMin"] = ds.Tables[0].Rows[i]["Common_Remark" + areaGubun].ToString();
                    HttpContext.Current.Session["ablSizeNum"] = ds.Tables[0].Rows[i]["Common_Remark3"].ToString();
                }
            }
        }

        public static void SetLogOut()
        {
            HttpContext.Current.Session["KureCode"] = "";
            HttpContext.Current.Session["KureName"] = "";
        }

        public static bool IsLogin()
        {
            string checkStr = string.Empty;

            try
            {
                checkStr = HttpContext.Current.Session["KureCode"].ToString().Trim();
            }
            catch
            {
            }

            if (!string.IsNullOrWhiteSpace(checkStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetLoginSID(string sessionName)
        {
            string returnValue = string.Empty;

            try
            {
                returnValue = HttpContext.Current.Session[sessionName].ToString().Trim();
            }
            catch
            {

            }

            return returnValue;
        }
    }
}
