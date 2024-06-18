using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace FirstOrder.Util
{
    /// <summary>
    /// Common의 요약 설명입니다.
    /// </summary>
    public class StCommon : System.Web.UI.Page
    {
        SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

        private string m_sizeName1;
        private string m_sizeName2;
        private string m_sizeName3;
        private string m_sizeName4;
        private string m_sizeName5;
        private string m_sizeName6;
        private string m_sizeName7;
        private string m_sizeName8;
        private string m_sizeName9;
        private string m_sizeName10;
        private string m_sizeName11;
        private string m_sizeName12;
        private string m_sizeName13;
        private string m_sizeName14;
        private string m_sizeName15;
        private string m_sizeName16;
        private string m_sizeName17;

        private string m_sizeNum1;
        private string m_sizeNum2;
        private string m_sizeNum3;
        private string m_sizeNum4;
        private string m_sizeNum5;
        private string m_sizeNum6;
        private string m_sizeNum7;
        private string m_sizeNum8;
        private string m_sizeNum9;
        private string m_sizeNum10;
        private string m_sizeNum11;
        private string m_sizeNum12;

        private string m_remark1;
        private string m_remark2;
        private string m_remark3;
        private string m_remark4;
        private string m_remark5;
        private string m_remark6;
        private string m_remark7;
        private string m_remark8;
        private string m_remark9;
        private string m_remark10;
        private string m_remark11;
        private string m_remark12;

        private string m_kind;
        
        private string m_cominfo1;
        private string m_cominfo2;
        private string m_cominfo3;
        private string m_cominfo4;
        private string m_cominfo5;
        private string m_cominfo6;
        private string m_cominfo7;
        private string m_cominfo8;

        public StCommon()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.s
            //
        }

        public StCommon(string preVal)
        {
            GetSizeInfo(preVal);
        }

        public string SizeName1
        {
            get { return m_sizeName1; }
        }
        public string SizeName2
        {
            get { return m_sizeName2; }
        }
        public string SizeName3
        {
            get { return m_sizeName3; }
        }
        public string SizeName4
        {
            get { return m_sizeName4; }
        }
        public string SizeName5
        {
            get { return m_sizeName5; }
        }
        public string SizeName6
        {
            get { return m_sizeName6; }
        }
        public string SizeName7
        {
            get { return m_sizeName7; }
        }
        public string SizeName8
        {
            get { return m_sizeName8; }
        }
        public string SizeName9
        {
            get { return m_sizeName9; }
        }
        public string SizeName10
        {
            get { return m_sizeName10; }
        }
        public string SizeName11
        {
            get { return m_sizeName11; }
        }
        public string SizeName12
        {
            get { return m_sizeName12; }
        }
        public string SizeName13
        {
            get { return m_sizeName13; }
        }
        public string SizeName14
        {
            get { return m_sizeName14; }
        }
        public string SizeName15
        {
            get { return m_sizeName15; }
        }
        public string SizeName16
        {
            get { return m_sizeName16; }
        }
        public string SizeName17
        {
            get { return m_sizeName17; }
        }

        public string SizeNum1
        {
            get { return m_sizeNum1; }
        }
        public string SizeNum2
        {
            get { return m_sizeNum2; }
        }
        public string SizeNum3
        {
            get { return m_sizeNum3; }
        }
        public string SizeNum4
        {
            get { return m_sizeNum4; }
        }
        public string SizeNum5
        {
            get { return m_sizeNum5; }
        }
        public string SizeNum6
        {
            get { return m_sizeNum6; }
        }
        public string SizeNum7
        {
            get { return m_sizeNum7; }
        }
        public string SizeNum8
        {
            get { return m_sizeNum8; }
        }
        public string SizeNum9
        {
            get { return m_sizeNum9; }
        }
        public string SizeNum10
        {
            get { return m_sizeNum10; }
        }
        public string SizeNum11
        {
            get { return m_sizeNum11; }
        }
        public string SizeNum12
        {
            get { return m_sizeNum12; }
        }
        
        public string Remark1
        {
            get { return m_remark1; }
        }
        public string Remark2
        {
            get { return m_remark2; }
        }
        public string Remark3
        {
            get { return m_remark3; }
        }
        public string Remark4
        {
            get { return m_remark4; }
        }
        public string Remark5
        {
            get { return m_remark5; }
        }
        public string Remark6
        {
            get { return m_remark6; }
        }
        public string Remark7
        {
            get { return m_remark7; }
        }
        public string Remark8
        {
            get { return m_remark8; }
        }
        public string Remark9
        {
            get { return m_remark9; }
        }
        public string Remark10
        {
            get { return m_remark10; }
        }
        public string Remark11
        {
            get { return m_remark11; }
        }
        public string Remark12
        {
            get { return m_remark12; }
        }
        public string Kind
        {   
            get { return m_kind; }
            set { m_kind = value; }
        }
        public string Cominfo1
        {
            get { return m_cominfo1; }
        }
        public string Cominfo2
        {
            get { return m_cominfo2; }
        }
        public string Cominfo3
        {
            get { return m_cominfo3; }
        }
        public string Cominfo4
        {
            get { return m_cominfo4; }
        }
        public string Cominfo5
        {
            get { return m_cominfo5; }
        }
        public string Cominfo6
        {
            get { return m_cominfo6; }
        }
        public string Cominfo7
        {
            get { return m_cominfo7; }
        }
        public string Cominfo8
        {
            get { return m_cominfo8; }
        }

        public void GetComInfo(string preVal)
        {
            string qry = " SELECT * FROM gblCOMMON WHERE Common_Key = 'T0313' ";

            DbCommand cmd = db.GetSqlStringCommand(qry);

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string field_name = (preVal == "tbl") ? "Common_Remark1" : "Common_Remark2";

                    string common_name = dr["Common_CodeName"].ToString().Trim();

                    switch (common_name)
                    {
                        case "사업자번호": m_cominfo1 = dr[field_name].ToString().Trim(); break;
                        case "상호": m_cominfo2 = dr[field_name].ToString().Trim(); break;
                        case "대표자": m_cominfo3 = dr[field_name].ToString().Trim(); break;
                        case "주소": m_cominfo4 = dr[field_name].ToString().Trim(); break;
                        case "업태": m_cominfo5 = dr[field_name].ToString().Trim(); break;
                        case "업종": m_cominfo6 = dr[field_name].ToString().Trim(); break;
                        case "전화번호": m_cominfo7 = dr[field_name].ToString().Trim(); break;
                        case "팩스번호": m_cominfo8 = dr[field_name].ToString().Trim(); break;
                    }
                }
                dr.Close();
            }
        }

        public void GetSizeInfo(string preVal)
        {
            string qry = " SELECT * FROM gblCOMMON WHERE Common_Key = 'T0511' and substring(Common_Code,1,3) = '" + preVal + "' ";

            DbCommand cmd = db.GetSqlStringCommand(qry);

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string common_num = dr["Common_Code"].ToString().Trim().Substring(3, 2);

                    switch (common_num)
                    {
                        case "01": m_sizeName1 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum1 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark1 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "02": m_sizeName2 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum2 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark2 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "03": m_sizeName3 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum3 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark3 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "04": m_sizeName4 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum4 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark4 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "05": m_sizeName5 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum5 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark5 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "06": m_sizeName6 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum6 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark6 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "07": m_sizeName7 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum7 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark7 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "08": m_sizeName8 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum8 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark8 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "09": m_sizeName9 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum9 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark9 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "10": m_sizeName10 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum10 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark10 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "11": m_sizeName11 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum11 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark11 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "12": m_sizeName12 = dr["Common_CodeName"].ToString().Trim(); m_sizeNum12 = (m_kind == "2") ? dr["Common_Remark2"].ToString().Trim() : dr["Common_Remark1"].ToString().Trim(); m_remark12 = dr["Common_Remark3"].ToString().Trim() + "<br>" + dr["Common_Remark2"].ToString().Trim(); break;
                        case "13": m_sizeName13 = dr["Common_CodeName"].ToString().Trim(); break;
                        case "14": m_sizeName14 = dr["Common_CodeName"].ToString().Trim(); break;
                        case "15": m_sizeName15 = dr["Common_CodeName"].ToString().Trim(); break;
                        case "16": m_sizeName16 = dr["Common_CodeName"].ToString().Trim(); break;
                        case "17": m_sizeName17 = dr["Common_CodeName"].ToString().Trim(); break;
                    }
                }
                dr.Close();
            }
        }

        public string GetSizeInfoNum(string kind, int common_num)
        {
            string result = "";

            switch (common_num)
            {
                case 1: result = (kind == "tbl") ? result = m_sizeName1 + "(" + m_sizeNum1 + ")" : result = m_sizeName1; break;
                case 2: result = (kind == "tbl") ? result = m_sizeName2 + "(" + m_sizeNum2 + ")" : result = m_sizeName2; break;
                case 3: result = (kind == "tbl") ? result = m_sizeName3 + "(" + m_sizeNum3 + ")" : result = m_sizeName3; break;
                case 4: result = (kind == "tbl") ? result = m_sizeName4 + "(" + m_sizeNum4 + ")" : result = m_sizeName4; break;
                case 5: result = (kind == "tbl") ? result = m_sizeName5 + "(" + m_sizeNum5 + ")" : result = m_sizeName5; break;
                case 6: result = (kind == "tbl") ? result = m_sizeName6 + "(" + m_sizeNum6 + ")" : result = m_sizeName6; break;
                case 7: result = (kind == "tbl") ? result = m_sizeName7 + "(" + m_sizeNum7 + ")" : result = m_sizeName7; break;
                case 8: result = (kind == "tbl") ? result = m_sizeName8 + "(" + m_sizeNum8 + ")" : result = m_sizeName8; break;
                case 9: result = (kind == "tbl") ? result = m_sizeName9 + "(" + m_sizeNum9 + ")" : result = m_sizeName9; break;
                case 10: result = (kind == "tbl") ? result = m_sizeName10 + "(" + m_sizeNum10 + ")" : result = m_sizeName10; break;
                case 11: result = (kind == "tbl") ? result = m_sizeName11 + "(" + m_sizeNum11 + ")" : result = m_sizeName11; break;
                case 12: result = (kind == "tbl") ? result = m_sizeName12 + "(" + m_sizeNum12 + ")" : result = m_sizeName12; break;
                case 13: result = m_sizeName13; break;
                case 14: result = m_sizeName14; break;
                case 15: result = m_sizeName15; break;
                case 16: result = m_sizeName16; break;
                case 17: result = m_sizeName17; break;
            }

            return result;
        }

        public static string EuckrToUtf8(string euckrStr)
        {
            byte[] euckrBytes = Encoding.GetEncoding(949).GetBytes(euckrStr);
            return Encoding.UTF8.GetString(euckrBytes);
        }

        // euc-kr
        public static String encodingEucKr(String s)
        {
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(51949);

            byte[] euckrBytes = euckr.GetBytes(s);

            string urlEncodingText = "";

            foreach (byte b in euckrBytes)
            {
                string addText = Convert.ToString(b, 16);

                urlEncodingText = urlEncodingText + "%" + addText;
            }

            return Convert.ToString(urlEncodingText);
        }

        // UTF-8
        public static String encodingUTF8(String s)
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

            byte[] utf8Bytes = utf8.GetBytes(s);

            string urlEncodingText = "";

            foreach (byte b in utf8Bytes)
            {
                string addText = Convert.ToString(b, 16);

                urlEncodingText = urlEncodingText + "%" + addText;
            }

            return Convert.ToString(urlEncodingText);
        }

        public static bool IsImageFileNameExt(string _checkExtStr)
        {
            bool returnValue = false;

            if (_checkExtStr != "")
            {
                string checkString = _checkExtStr.ToLower().Trim();

                if (checkString == "jpg" || checkString == "jpeg" || checkString == "gif" || checkString == "bmp" || checkString == "png")
                {
                    returnValue = true;
                }

            }

            return returnValue;
        }

        public static string RemoveHtmlTags(string inContent)
        {
            inContent = inContent.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&amp;nbsp;", " ").Replace("&nbsp;", " ").Replace("&amp;quot;", "\"").Replace("&quot;", "\"").Replace("&amp;#035;", "#").Replace("&#035;", "#").Replace("&amp;#039;", "'").Replace("&#039;", "'").Replace("&amp;#39;", "'").Replace("&#39;", "'");

            inContent = Regex.Replace(inContent, @"<(.|\n)*?>", string.Empty);
            
            return inContent;

            //Regex tagRegex = new Regex("<(.|\n)+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //return tagRegex.Replace(inContent, "");
        }

        public static string Chr2Br(string checkValue)
        {
            string returnValue = string.Empty;

            returnValue = checkValue.Replace("\n", "<br>");
            returnValue = returnValue.Replace("$l", "<br>");

            return returnValue;
        }

        public static string Chr2Br(object obj)
        {
            string returnValue = string.Empty;
            try
            {
                string checkValue = obj.ToString();

                returnValue = checkValue.Replace("\n", "<br>");
                returnValue = returnValue.Replace("$l", "<br>");
            }
            catch { }

            return returnValue;
        }

        public static string PTag2Chr(string checkValue)
        {
            string returnValue = string.Empty;

            returnValue = checkValue.Replace("<p>", "");
            returnValue = returnValue.Replace("</p>", "<br>");

            return returnValue;
        }

        public static string ReplaceCrLf(string checkValue)
        {
            string returnValue = string.Empty;

            returnValue = checkValue.Replace("\r", " ");
            returnValue = returnValue.Replace("\n", " ");

            return returnValue;
        }

        public static string ReplaceCrLfToJs(string checkValue)
        {
            string returnValue = string.Empty;

            returnValue = checkValue.Replace("\r", "");
            returnValue = returnValue.Replace("\n", "\\n");

            return returnValue;
        }

        public static string ReplaceSQ(string checkValue)
        {
            string returnValue = string.Empty;

            if (!string.IsNullOrEmpty(checkValue))
            {
                returnValue = checkValue.Trim();
                returnValue = returnValue.Replace("'", "''");
                returnValue = returnValue.Replace("--", "");
                returnValue = returnValue.Replace("\\", "");
                returnValue = returnValue.Replace(";", "");
                returnValue = returnValue.Replace("<", "&lt;");
                returnValue = returnValue.Replace("%3C", "&lt;");
                returnValue = returnValue.Replace(">", "&gt;");
                returnValue = returnValue.Replace("%3E", "&gt;");
            }
            return returnValue;
        }

        public static bool IsValidQueryDefStmt(string checkValue)
        {
            bool isValid = true;

            string[] denyList = { "delete", "update", "exec", "drop", "create", "alter", "declare", ";--", "set @", "%3C", "%3E" };

            if (!string.IsNullOrEmpty(checkValue))
            {
                for (int i = 0; i < denyList.Length; i++)
                {
                    if (checkValue.ToLower().IndexOf(denyList[i]) > 0)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        public static string CutTitle(object obj, int cutLength)
        {
            string returnValue = obj.ToString().Trim();

            if (!string.IsNullOrEmpty(returnValue))
            {
                if (returnValue.Length > cutLength)
                {
                    returnValue = returnValue.Substring(0, cutLength) + "...";
                }
            }

            return returnValue;
        }

        public static int ToInt(string str, int def)
        {
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return def;
            }
        }

        public static double ToDouble(string str, int def)
        {
            try
            {
                return double.Parse(str);
            }
            catch
            {
                return def;
            }
        }

        public static Int64 ToInt64(string str, int def)
        {
            try
            {
                return Int64.Parse(str);
            }
            catch
            {
                return def;
            }
        }

        public static int TrueToInt(bool chk, int defTrue, int defFalse)
        {
            if (chk)
            {
                return defTrue;
            }
            else
            {
                return defFalse;
            }
        }

        public static string TrueToString(bool chk, string defTrue, string defFalse)
        {
            if (chk)
            {
                return defTrue;
            }
            else
            {
                return defFalse;
            }
        }

        public static string TrueToString(object chk, string defTrue, string defFalse)
        {
            if (Convert.ToBoolean(chk))
            {
                return defTrue;
            }
            else
            {
                return defFalse;
            }
        }

        public static bool StringToTrue(string chk, string def)
        {
            bool returnValue = false;

            try
            {
                if (chk.Trim() == def.Trim())
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            catch
            {

            }

            return returnValue;
        }

        public static bool StringToFalse(string chk, string def)
        {
            bool returnValue = true;

            try
            {
                if (chk.Trim() == def.Trim())
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            catch
            {

            }

            return returnValue;
        }

        public static bool IntToTrue(int chk, int def)
        {
            bool returnValue = false;

            try
            {
                if (chk == def)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            catch
            {

            }

            return returnValue;
        }

        public static string StringToUseViewStr(string chk, string defTrue, string defFalse)
        {
            string returnValue = defFalse;

            try
            {
                if (chk == "1" || chk == "*" || chk.ToLower() == "true")
                {
                    returnValue = defTrue;
                }
                else
                {
                    returnValue = defFalse;
                }
            }
            catch
            {

            }

            return returnValue;
        }

        public static string NullEmptyToBlank(string chk)
        {
            if (string.IsNullOrEmpty(chk))
            {
                return "";
            }
            else
            {
                return chk;
            }
        }

        public static string BlankToNull(string chk)
        {
            if (chk == "")
            {
                return null;
            }
            else
            {
                return chk;
            }
        }

        public static string NumberFormat(object obj)
        {
            return string.Format("{0:#,##0}", obj);
        }

        public static string StringAddDelimeter(string checkStr1)
        {
            return checkStr1;
        }

        public static string StringAddDelimeter(string checkStr1, string checkStr2)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1))
            {
                returnValue += checkStr1;
            }

            if (!string.IsNullOrEmpty(checkStr2))
            {
                returnValue += " > " + checkStr2;
            }

            return returnValue;
        }

        public static string StringAddDelimeter(string checkStr1, string checkStr2, string checkStr3)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1))
            {
                returnValue += checkStr1;
            }

            if (!string.IsNullOrEmpty(checkStr2))
            {
                returnValue += " > " + checkStr2;
            }

            if (!string.IsNullOrEmpty(checkStr3))
            {
                returnValue += " > " + checkStr3;
            }

            return returnValue;
        }

        public static string StringAddDelimeter(string checkStr1, string checkStr2, string checkStr3, string checkStr4)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1))
            {
                returnValue += checkStr1;
            }

            if (!string.IsNullOrEmpty(checkStr2))
            {
                returnValue += " > " + checkStr2;
            }

            if (!string.IsNullOrEmpty(checkStr3))
            {
                returnValue += " > " + checkStr3;
            }

            if (!string.IsNullOrEmpty(checkStr4))
            {
                returnValue += " > " + checkStr4;
            }

            return returnValue;
        }

        public static string StringAddDelimeter(string checkStr1, string checkStr2, string checkStr3, string checkStr4, string checkStr5)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1))
            {
                returnValue += checkStr1;
            }

            if (!string.IsNullOrEmpty(checkStr2))
            {
                returnValue += " > " + checkStr2;
            }

            if (!string.IsNullOrEmpty(checkStr3))
            {
                returnValue += " > " + checkStr3;
            }

            if (!string.IsNullOrEmpty(checkStr4))
            {
                returnValue += " > " + checkStr4;
            }

            if (!string.IsNullOrEmpty(checkStr5))
            {
                returnValue += " > " + checkStr5;
            }

            return returnValue;
        }

        public static string StringAddDelimeter(object checkStr1)
        {
            return checkStr1.ToString().Trim();
        }

        public static string StringAddDelimeter(object checkStr1, object checkStr2)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1.ToString().Trim()))
            {
                returnValue += checkStr1.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr2.ToString().Trim()))
            {
                returnValue += " > " + checkStr2.ToString().Trim();
            }

            return returnValue;
        }

        public static string StringAddDelimeter(object checkStr1, object checkStr2, object checkStr3)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1.ToString().Trim()))
            {
                returnValue += checkStr1.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr2.ToString().Trim()))
            {
                returnValue += " > " + checkStr2.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr3.ToString().Trim()))
            {
                returnValue += " > " + checkStr3.ToString().Trim();
            }

            return returnValue;
        }

        public static string StringAddDelimeter(object checkStr1, object checkStr2, object checkStr3, object checkStr4)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1.ToString().Trim()))
            {
                returnValue += checkStr1.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr2.ToString().Trim()))
            {
                returnValue += " > " + checkStr2.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr3.ToString().Trim()))
            {
                returnValue += " > " + checkStr3.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr4.ToString().Trim()))
            {
                returnValue += " > " + checkStr4.ToString().Trim();
            }

            return returnValue;
        }

        public static string StringAddDelimeter(object checkStr1, object checkStr2, object checkStr3, object checkStr4, object checkStr5)
        {
            string returnValue = "";

            if (!string.IsNullOrEmpty(checkStr1.ToString().Trim()))
            {
                returnValue += checkStr1.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr2.ToString().Trim()))
            {
                returnValue += " > " + checkStr2.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr3.ToString().Trim()))
            {
                returnValue += " > " + checkStr3.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr4.ToString().Trim()))
            {
                returnValue += " > " + checkStr4.ToString().Trim();
            }

            if (!string.IsNullOrEmpty(checkStr5.ToString().Trim()))
            {
                returnValue += " > " + checkStr5.ToString().Trim();
            }

            return returnValue;
        }

        public static string ZeroToEmpty(int checkValue, int compareValue, string returnValue)
        {
            if (checkValue == compareValue)
            {
                return returnValue;
            }
            else
            {
                return checkValue.ToString();
            }
        }

        public static bool IsMobileAgent(string userAgent)
        {
            string[] mobileAgent = {
                                       "iPhone", "iPod", "Windows CE", "Symbian", "BlackBerry",
                                       "Android", "Windows Phone", "webOS", "Opera Mini", "Opera Mobi",
                                       "POLARIS", "IEMobile", "lgtelecom", "nokia", "SonyEricsson"
                                   };

            bool isMobile = false;

            for (int i = 0; i < mobileAgent.Length; i++)
            {
                if (userAgent.ToLower().IndexOf(mobileAgent[i].ToLower()) > 0)
                {
                    isMobile = true;
                    break;
                }
            }
            return isMobile;
        }

        public static bool IsAppleAgent(string userAgent)
        {
            string[] appleAgent = { "iPhone", "iPod", "iPad" };

            bool isApple = false;

            for (int i = 0; i < appleAgent.Length; i++)
            {
                if (userAgent.ToLower().IndexOf(appleAgent[i].ToLower()) > 0)
                {
                    isApple = true;
                    break;
                }
            }
            return isApple;
        }

        public static bool IsAndroidAgent(string userAgent)
        {
            string[] androidAgent = { "Android" };

            bool isAndroid = false;

            for (int i = 0; i < androidAgent.Length; i++)
            {
                if (userAgent.ToLower().IndexOf(androidAgent[i].ToLower()) > 0)
                {
                    isAndroid = true;
                    break;
                }
            }
            return isAndroid;
        }

        public static string MakePercent(int _child, int _mother, int _digit)
        {
            string returnValue = string.Empty;

            try
            {
                double pctValue = Math.Round(Convert.ToDouble(_child) / Convert.ToDouble(_mother) * 100, _digit);

                if (double.IsNaN(pctValue))
                    pctValue = 0.0;

                if (double.IsPositiveInfinity(pctValue))
                {
                    pctValue = 100;
                }
                else if (double.IsNegativeInfinity(pctValue))
                {
                    pctValue = -100;
                }

                returnValue = pctValue.ToString() + "%";
            }
            catch { }

            return returnValue;
        }

        public static string ReturnJSonEmptyData()
        {
            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            return jsSer.Serialize("");
        }

        public static string MakeSearchQry(string fieldName, string fieldValue, string chkOpt, string whereQry)
        {
            string returnValue = string.Empty;

            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(fieldName))
            {
                switch (chkOpt)
                {
                    case "S":
                        returnValue = "(" + fieldName + "='" + fieldValue + "')";
                        break;

                    case "N":
                        returnValue = "(" + fieldName + "=" + fieldValue + ")";
                        break;

                    case "%":
                        returnValue = "(" + fieldName + " LIKE '%" + fieldValue + "%')";
                        break;

                    case "I":
                        returnValue = "(" + fieldName + " IS " + fieldValue + ")";
                        break;

                    case "IN":
                        returnValue = "(" + fieldName + " IN (" + fieldValue + "))";
                        break;
                }

                returnValue = whereQry + " AND " + returnValue + " ";
            }
            else
            {
                returnValue = whereQry;
            }


            return returnValue;
        }

        public static string MakeSearchQry(string fieldName, string fieldValue, string fieldValue1, string chkOpt, string whereQry)
        {
            string returnValue = string.Empty;

            if (!string.IsNullOrWhiteSpace(fieldValue) && string.IsNullOrWhiteSpace(fieldValue1))
            {
                fieldValue1 = fieldValue;
            }
            else if (string.IsNullOrWhiteSpace(fieldValue) && !string.IsNullOrWhiteSpace(fieldValue1))
            {
                fieldValue = fieldValue1;
            }

            if (!string.IsNullOrEmpty(fieldValue) && !string.IsNullOrEmpty(fieldName))
            {
                switch (chkOpt)
                {
                    case "S":
                        returnValue = "(" + fieldName + " BETWEEN '" + fieldValue + "' AND '" + fieldValue1 + "')";
                        break;

                    case "N":
                        returnValue = "(" + fieldName + " BETWEEN " + fieldValue + " AND " + fieldValue1 + ")";
                        break;
                }

                returnValue = whereQry + " AND " + returnValue + " ";
            }
            else
            {
                returnValue = whereQry;
            }


            return returnValue;
        }

        public static string ConvertMobileDate(string _checkDate)
        {
            string returnValue = _checkDate;

            if (!string.IsNullOrWhiteSpace(_checkDate))
            {
                try
                {
                    returnValue = Convert.ToDateTime(_checkDate).ToShortDateString();
                }
                catch { }
            }

            return returnValue;
        }

        /*Log*/
        #region ■ LogException(Exception logexception) : 로그 작성:Exception
        public void LogException(Exception logexception)
        {
            DateTime dtNow = DateTime.Now;
            string rootPath = ConfigurationManager.AppSettings["RootDir"];
            string logFilePath = @"\" + ConfigurationManager.AppSettings["LogFilePath"] + @"\";
            string fileName = dtNow.Year.ToString() + string.Format("{0:0#}", dtNow.Month) + string.Format("{0:0#}", dtNow.Day) + ".log";
            string fileSum = rootPath + logFilePath + fileName;

            string msg = "Log Entry : \r\n\r\n";
            msg += "LoginID :: " + Session["LoginID"] + " \r\n";
            msg += "KureCode :: " + Session["KureCode"] + " \r\n";
            msg += "KureName :: " + Session["KureName"] + " \r\n\r\n";
            msg += DateTime.Now.ToLongDateString() + " ";
            msg += DateTime.Now.ToLongTimeString() + " \r\n\r\n";
            msg += logexception.ToString();
            msg += "\r\n---------------------------------------------------------\r\n\r\n";

            using (FileStream fs = new FileStream(fileSum, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);    // 포인터를 화일 끝으로 이동시킨다.

                    sw.Write(msg);
                    sw.Flush();                     // 버퍼를 비운다.
                    sw.Close();
                }

                fs.Close();
            }
        }
        #endregion

        #region ■ GetRequestParameter(string parametername, string defaultvalue) : Request Parameter String
        public string GetRequestParameter(string parametername, string defaultvalue)
        {
            string strValue = defaultvalue;
            try
            {
                strValue = (HttpContext.Current.Request[parametername] != null) ? HttpContext.Current.Request[parametername].Trim() : defaultvalue;
                if (XSSCheck(strValue.ToString()))
                {
                    string strScript = String.Format("<script type='text/javascript'>alert(\"허용되지 않은 문자열을 포함하고 있습니다.\");history.back();</script>");
                    Context.Response.Write(strScript);
                    Context.Response.End();
                }
                strValue = Server.HtmlEncode(strValue);
            }
            catch
            {
                strValue = defaultvalue;
            }
            return strValue;
        }
        #endregion

        #region ■ GetRequestParameter(string parametername, int defaultvalue) : Request Parameter Int
        public int GetRequestParameter(string parametername, int defaultvalue)
        {
            int nValue = defaultvalue;
            try
            {
                if (HttpContext.Current.Request[parametername] != null)
                {
                    nValue = Convert.ToInt32(HttpContext.Current.Request[parametername]);
                    if (XSSCheck(nValue.ToString()))
                    {
                        string strScript = String.Format("<script type='text/javascript'>alert(\"허용되지 않은 문자열을 포함하고 있습니다.\");history.back();</script>");
                        Context.Response.Write(strScript);
                        Context.Response.End();
                    }
                }
                else
                {
                    nValue = defaultvalue;
                }
            }
            catch
            {
                nValue = defaultvalue;
            }

            return nValue;
        }
        #endregion

        #region ■ XSSCheck(string ckStr) : XSS 입력 방지
        public bool XSSCheck(string ckStr)
        {
            string strCheck = "ck" + ckStr.ToLower();
            if (strCheck.IndexOf("|") > 0 ||
                strCheck.IndexOf("&") > 0 ||
                strCheck.IndexOf(";") > 0 ||
                strCheck.IndexOf("$") > 0 ||
                strCheck.IndexOf("%") > 0 ||
                strCheck.IndexOf("@") > 0 ||
                strCheck.IndexOf("'") > 0 ||
                strCheck.IndexOf("\"") > 0 ||
                strCheck.IndexOf("\\") > 0 ||
                strCheck.IndexOf("<") > 0 ||
                strCheck.IndexOf(">") > 0 ||
                strCheck.IndexOf("(") > 0 ||
                strCheck.IndexOf(")") > 0 ||
                strCheck.IndexOf("+") > 0 ||
                strCheck.IndexOf(",") > 0 ||
                strCheck.IndexOf("=") > 0 ||
                strCheck.IndexOf("--") > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public static string GetCommonCookie(string _name)
        {
            string returnValue = string.Empty;

            try
            {
                returnValue = HttpContext.Current.Request.Cookies[_name].Value.ToString().Trim();
            }
            catch { }

            return returnValue;
        }

        public static string GetBonsaCheck(string preVal, string kure, string date, string time)
        {
            StDataCommon stData = new StDataCommon();

            string result = "";

            string whereQry = " where Bjhd_MainBuyer = '" + kure + "' ";
            whereQry = StCommon.MakeSearchQry("Bjhd_Date", date, "S", whereQry);
            whereQry = StCommon.MakeSearchQry("Bjhd_Times", time, "S", whereQry);

            string qry = " select Bjhd_Bonsa_Check from " + preVal + "BJHD " + whereQry;
            DataSet ds = stData.GetDataSet(qry);

            try
            {
                result = ds.Tables[0].Rows[0][0].ToString();
            }
            catch { }

            return result;
        }

        public static string MessageBonsaCheck(string bonsacheck)
        {
            string stateMsg = "";
            if (bonsacheck == "0")
            {
                stateMsg = "발주의뢰 중";
            }
            else if (bonsacheck == "1")
            {
                stateMsg = "본사주문 완료";
            }
            else if (bonsacheck == "2")
            {
                stateMsg = "본사주문 완료";
            }
            else if (bonsacheck == "Y")
            {
                stateMsg = "배송준비 중";
            }
            else if (bonsacheck == "Z")
            {
                stateMsg = "배송완료";
            }

            return stateMsg;
        }

        public static string GetAmountFormat(Object obj)
        {
            string result = obj.ToString().Trim();
            if (result == "0")
            {
                result = "";
            }

            if (result != "")
            {
                result = StCommon.NumberFormat(Convert.ToDouble(result));
            }

            return result;
        }

        public static string[] JegoSet(string preVal, string date, string times, string kure, string line, string blju_style, string mode)
        {
            string[] JegoArray = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }; // 첫값은 비우고, 사이즈17개+Total 까지 19개
            StDataCommon stData = new StDataCommon();

            string qry = " select * from View_" + preVal + "JEGO_Summary where Jego_StyleNox = '" + blju_style + "' ";
            DataSet dsJ = stData.GetDataSet(qry);

            if (dsJ.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsJ.Tables[0].Rows[0];

                JegoArray[0] = "";
                JegoArray[1] = GetAmountFormat(dr["Jego_Qty01"]);
                JegoArray[2] = GetAmountFormat(dr["Jego_Qty02"]);
                JegoArray[3] = GetAmountFormat(dr["Jego_Qty03"]);
                JegoArray[4] = GetAmountFormat(dr["Jego_Qty04"]);
                JegoArray[5] = GetAmountFormat(dr["Jego_Qty05"]);
                JegoArray[6] = GetAmountFormat(dr["Jego_Qty06"]);
                JegoArray[7] = GetAmountFormat(dr["Jego_Qty07"]);
                JegoArray[8] = GetAmountFormat(dr["Jego_Qty08"]);
                JegoArray[9] = GetAmountFormat(dr["Jego_Qty09"]);
                JegoArray[10] = GetAmountFormat(dr["Jego_Qty10"]);
                JegoArray[11] = GetAmountFormat(dr["Jego_Qty11"]);
                JegoArray[12] = GetAmountFormat(dr["Jego_Qty12"]);
                JegoArray[13] = GetAmountFormat(dr["Jego_Qty13"]);
                JegoArray[14] = GetAmountFormat(dr["Jego_Qty14"]);
                JegoArray[15] = GetAmountFormat(dr["Jego_Qty15"]);
                JegoArray[16] = GetAmountFormat(dr["Jego_Qty16"]);
                JegoArray[17] = GetAmountFormat(dr["Jego_Qty17"]);
                JegoArray[18] = GetAmountFormat(dr["Jego_QtyTotal"]);
            }
            else
            {
                JegoArray[0] = "";
                JegoArray[1] = "";
                JegoArray[2] = "";
                JegoArray[3] = "";
                JegoArray[4] = "";
                JegoArray[5] = "";
                JegoArray[6] = "";
                JegoArray[7] = "";
                JegoArray[8] = "";
                JegoArray[9] = "";
                JegoArray[10] = "";
                JegoArray[11] = "";
                JegoArray[12] = "";
                JegoArray[13] = "";
                JegoArray[14] = "";
                JegoArray[15] = "";
                JegoArray[16] = "";
                JegoArray[17] = "";
                JegoArray[18] = "";
            }

            if (mode == "mod") // 수정일 경우에는 재고 <= (재고 + 현재 데이터 주문량)
            {
                double jego1 = StCommon.ToDouble(JegoArray[1], 0);
                double jego2 = StCommon.ToDouble(JegoArray[2], 0);
                double jego3 = StCommon.ToDouble(JegoArray[3], 0);
                double jego4 = StCommon.ToDouble(JegoArray[4], 0);
                double jego5 = StCommon.ToDouble(JegoArray[5], 0);
                double jego6 = StCommon.ToDouble(JegoArray[6], 0);
                double jego7 = StCommon.ToDouble(JegoArray[7], 0);
                double jego8 = StCommon.ToDouble(JegoArray[8], 0);
                double jego9 = StCommon.ToDouble(JegoArray[9], 0);
                double jego10 = StCommon.ToDouble(JegoArray[10], 0);
                double jego11 = StCommon.ToDouble(JegoArray[11], 0);
                double jego12 = StCommon.ToDouble(JegoArray[12], 0);
                double jego13 = StCommon.ToDouble(JegoArray[13], 0);
                double jego14 = StCommon.ToDouble(JegoArray[14], 0);
                double jego15 = StCommon.ToDouble(JegoArray[15], 0);
                double jego16 = StCommon.ToDouble(JegoArray[16], 0);
                double jego17 = StCommon.ToDouble(JegoArray[17], 0);
                double jegoTotal = StCommon.ToDouble(JegoArray[18], 0);

                qry = " select ";
                qry += " isnull(sum(blju_qty01),0) as blju_qty01,isnull(sum(blju_qty02),0) as blju_qty02,isnull(sum(blju_qty03),0) as blju_qty03,isnull(sum(blju_qty04),0) as blju_qty04 ";
                qry += " ,isnull(sum(blju_qty05),0) as blju_qty05,isnull(sum(blju_qty06),0) as blju_qty06,isnull(sum(blju_qty07),0) as blju_qty07,isnull(sum(blju_qty08),0) as blju_qty08 ";
                qry += " ,isnull(sum(blju_qty09),0) as blju_qty09,isnull(sum(blju_qty10),0) as blju_qty10,isnull(sum(blju_qty11),0) as blju_qty11,isnull(sum(blju_qty12),0) as blju_qty12 ";
                qry += " ,isnull(sum(blju_qty13),0) as blju_qty13,isnull(sum(blju_qty14),0) as blju_qty14,isnull(sum(blju_qty15),0) as blju_qty15,isnull(sum(blju_qty16),0) as blju_qty16,isnull(sum(blju_qty17),0) as blju_qty17 ";
                qry += " FROM " + preVal + "BLJU a where blju_stylenox = '" + blju_style + "' and blju_date = '" + date + "' and blju_times = '" + times + "' and blju_mainbuyer = '" + kure + "' and blju_line = '" + line + "' ";
                DataSet ds = stData.GetDataSet(qry);
                DataRow dr = ds.Tables[0].Rows[0];

                jego1 = jego1 + StCommon.ToDouble(dr["blju_qty01"].ToString(), 0);
                jego2 = jego2 + StCommon.ToDouble(dr["blju_qty02"].ToString(), 0);
                jego3 = jego3 + StCommon.ToDouble(dr["blju_qty03"].ToString(), 0);
                jego4 = jego4 + StCommon.ToDouble(dr["blju_qty04"].ToString(), 0);
                jego5 = jego5 + StCommon.ToDouble(dr["blju_qty05"].ToString(), 0);
                jego6 = jego6 + StCommon.ToDouble(dr["blju_qty06"].ToString(), 0);
                jego7 = jego7 + StCommon.ToDouble(dr["blju_qty07"].ToString(), 0);
                jego8 = jego8 + StCommon.ToDouble(dr["blju_qty08"].ToString(), 0);
                jego9 = jego9 + StCommon.ToDouble(dr["blju_qty09"].ToString(), 0);
                jego10 = jego10 + StCommon.ToDouble(dr["blju_qty10"].ToString(), 0);
                jego11 = jego11 + StCommon.ToDouble(dr["blju_qty11"].ToString(), 0);
                jego12 = jego12 + StCommon.ToDouble(dr["blju_qty12"].ToString(), 0);
                jego13 = jego13 + StCommon.ToDouble(dr["blju_qty13"].ToString(), 0);
                jego14 = jego14 + StCommon.ToDouble(dr["blju_qty14"].ToString(), 0);
                jego15 = jego15 + StCommon.ToDouble(dr["blju_qty15"].ToString(), 0);
                jego16 = jego16 + StCommon.ToDouble(dr["blju_qty16"].ToString(), 0);
                jego17 = jego17 + StCommon.ToDouble(dr["blju_qty17"].ToString(), 0);

                JegoArray[1] = GetAmountFormat(jego1);
                JegoArray[2] = GetAmountFormat(jego2);
                JegoArray[3] = GetAmountFormat(jego3);
                JegoArray[4] = GetAmountFormat(jego4);
                JegoArray[5] = GetAmountFormat(jego5);
                JegoArray[6] = GetAmountFormat(jego6);
                JegoArray[7] = GetAmountFormat(jego7);
                JegoArray[8] = GetAmountFormat(jego8);
                JegoArray[9] = GetAmountFormat(jego9);
                JegoArray[10] = GetAmountFormat(jego10);
                JegoArray[11] = GetAmountFormat(jego11);
                JegoArray[12] = GetAmountFormat(jego12);
                JegoArray[13] = GetAmountFormat(jego13);
                JegoArray[14] = GetAmountFormat(jego14);
                JegoArray[15] = GetAmountFormat(jego15);
                JegoArray[16] = GetAmountFormat(jego16);
                JegoArray[17] = GetAmountFormat(jego17);
                JegoArray[18] = GetAmountFormat(jego1 + jego2 + jego3 + jego4 + jego5 + jego6 + jego7 + jego8 + jego9 + jego10 + jego11 + jego12 + jego13 + jego14 + jego15 + jego16 + jego17);
            }

            return JegoArray;
        }

        /*
        등록시에는 기존에 해당 product가 1개 이상 있으면 중복 (라인 무시)
        수정시에는 기존에 해당 product가 라인이 다른 1개 이상 있으면 중복
        */
        public static string DupleStyleCheck(string date, string time, string kure, string product, string line, string mode)
        {
            StDataCommon stData = new StDataCommon();

            string preVal = "";

            try
            {
                preVal = HttpContext.Current.Session["PreVal"].ToString();
            }
            catch
            {
                preVal = "tbl";
            }

            string result = "";

            if (mode == "mod")
            {
                string qry = " select * from " + preVal + "BLJU where Blju_Date = '" + date + "' and Blju_Times = '" + time + "' and Blju_MainBuyer = '" + kure + "' and Blju_StyleNox = '" + product + "' and Blju_Line <> '" + line + "' ";

                DataSet dsC = stData.GetDataSet(qry);

                if (dsC.Tables[0].Rows.Count > 0)
                {
                    result = dsC.Tables[0].Rows[0]["Blju_Line"].ToString();
                }
            }
            else
            {
                string qry = " select * from " + preVal + "BLJU where Blju_Date = '" + date + "' and Blju_Times = '" + time + "' and Blju_MainBuyer = '" + kure + "' and Blju_StyleNox = '" + product + "' ";
                DataSet dsC = stData.GetDataSet(qry);

                if (dsC.Tables[0].Rows.Count > 0)
                {
                    result = dsC.Tables[0].Rows[0]["Blju_Line"].ToString();
                }
            }

            return result;
        }

        public static string[] JegoSet(string blju_style, string mode, string date, string time, string kure, string line)
        {
            StDataCommon stData = new StDataCommon();

            string preVal = "";

            try
            {
                preVal = HttpContext.Current.Session["PreVal"].ToString();
            }
            catch
            {
                preVal = "tbl";
            }

            string[] JegoArray = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }; // 첫값은 비우고, 사이즈17개+Total 까지 19개

            string qry = " select * from View_" + preVal + "JEGO_Summary where Jego_StyleNox = '" + blju_style + "' ";
            DataSet dsJ = stData.GetDataSet(qry);

            if (dsJ.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsJ.Tables[0].Rows[0];
                JegoArray[1] = GetAmountFormat(dr["Jego_Qty01"]);
                JegoArray[2] = GetAmountFormat(dr["Jego_Qty02"]);
                JegoArray[3] = GetAmountFormat(dr["Jego_Qty03"]);
                JegoArray[4] = GetAmountFormat(dr["Jego_Qty04"]);
                JegoArray[5] = GetAmountFormat(dr["Jego_Qty05"]);
                JegoArray[6] = GetAmountFormat(dr["Jego_Qty06"]);
                JegoArray[7] = GetAmountFormat(dr["Jego_Qty07"]);
                JegoArray[8] = GetAmountFormat(dr["Jego_Qty08"]);
                JegoArray[9] = GetAmountFormat(dr["Jego_Qty09"]);
                JegoArray[10] = GetAmountFormat(dr["Jego_Qty10"]);
                JegoArray[11] = GetAmountFormat(dr["Jego_Qty11"]);
                JegoArray[12] = GetAmountFormat(dr["Jego_Qty12"]);
                JegoArray[13] = GetAmountFormat(dr["Jego_Qty13"]);
                JegoArray[14] = GetAmountFormat(dr["Jego_Qty14"]);
                JegoArray[15] = GetAmountFormat(dr["Jego_Qty15"]);
                JegoArray[16] = GetAmountFormat(dr["Jego_Qty16"]);
                JegoArray[17] = GetAmountFormat(dr["Jego_Qty17"]);
                JegoArray[0] = GetAmountFormat(dr["Jego_QtyTotal"]);
            }
            else
            {
                JegoArray[1] = "";
                JegoArray[2] = "";
                JegoArray[3] = "";
                JegoArray[4] = "";
                JegoArray[5] = "";
                JegoArray[6] = "";
                JegoArray[7] = "";
                JegoArray[8] = "";
                JegoArray[9] = "";
                JegoArray[10] = "";
                JegoArray[11] = "";
                JegoArray[12] = "";
                JegoArray[13] = "";
                JegoArray[14] = "";
                JegoArray[15] = "";
                JegoArray[16] = "";
                JegoArray[17] = "";
                JegoArray[0] = "";
            }

            if (mode == "mod") // 수정일 경우에는 재고 <= (재고 + 현재 데이터 주문량)
            {   
                double jego1 = StCommon.ToDouble(JegoArray[1], 0);
                double jego2 = StCommon.ToDouble(JegoArray[2], 0);
                double jego3 = StCommon.ToDouble(JegoArray[3], 0);
                double jego4 = StCommon.ToDouble(JegoArray[4], 0);
                double jego5 = StCommon.ToDouble(JegoArray[5], 0);
                double jego6 = StCommon.ToDouble(JegoArray[6], 0);
                double jego7 = StCommon.ToDouble(JegoArray[7], 0);
                double jego8 = StCommon.ToDouble(JegoArray[8], 0);
                double jego9 = StCommon.ToDouble(JegoArray[9], 0);
                double jego10 = StCommon.ToDouble(JegoArray[10], 0);
                double jego11 = StCommon.ToDouble(JegoArray[11], 0);
                double jego12 = StCommon.ToDouble(JegoArray[12], 0);
                double jego13 = StCommon.ToDouble(JegoArray[13], 0);
                double jego14 = StCommon.ToDouble(JegoArray[14], 0);
                double jego15 = StCommon.ToDouble(JegoArray[15], 0);
                double jego16 = StCommon.ToDouble(JegoArray[16], 0);
                double jego17 = StCommon.ToDouble(JegoArray[17], 0);
                double jegoTotal = StCommon.ToDouble(JegoArray[0], 0);

                qry = " select ";
                qry += " isnull(sum(blju_qty01),0) as blju_qty01,isnull(sum(blju_qty02),0) as blju_qty02,isnull(sum(blju_qty03),0) as blju_qty03,isnull(sum(blju_qty04),0) as blju_qty04 ";
                qry += " ,isnull(sum(blju_qty05),0) as blju_qty05,isnull(sum(blju_qty06),0) as blju_qty06,isnull(sum(blju_qty07),0) as blju_qty07,isnull(sum(blju_qty08),0) as blju_qty08 ";
                qry += " ,isnull(sum(blju_qty09),0) as blju_qty09,isnull(sum(blju_qty10),0) as blju_qty10,isnull(sum(blju_qty11),0) as blju_qty11,isnull(sum(blju_qty12),0) as blju_qty12 ";
                qry += " ,isnull(sum(blju_qty13),0) as blju_qty13,isnull(sum(blju_qty14),0) as blju_qty14,isnull(sum(blju_qty15),0) as blju_qty15,isnull(sum(blju_qty16),0) as blju_qty16,isnull(sum(blju_qty17),0) as blju_qty17 ";
                qry += " FROM " + preVal + "BLJU a where blju_stylenox = '" + blju_style + "' and blju_date = '" + date + "' and blju_times = '" + time + "' and blju_mainbuyer = '" + kure + "' and blju_line = '" + line + "' ";
                DataSet ds = stData.GetDataSet(qry);
                DataRow dr = ds.Tables[0].Rows[0];

                jego1 = jego1 + StCommon.ToDouble(dr["blju_qty01"].ToString(), 0);
                jego2 = jego2 + StCommon.ToDouble(dr["blju_qty02"].ToString(), 0);
                jego3 = jego3 + StCommon.ToDouble(dr["blju_qty03"].ToString(), 0);
                jego4 = jego4 + StCommon.ToDouble(dr["blju_qty04"].ToString(), 0);
                jego5 = jego5 + StCommon.ToDouble(dr["blju_qty05"].ToString(), 0);
                jego6 = jego6 + StCommon.ToDouble(dr["blju_qty06"].ToString(), 0);
                jego7 = jego7 + StCommon.ToDouble(dr["blju_qty07"].ToString(), 0);
                jego8 = jego8 + StCommon.ToDouble(dr["blju_qty08"].ToString(), 0);
                jego9 = jego9 + StCommon.ToDouble(dr["blju_qty09"].ToString(), 0);
                jego10 = jego10 + StCommon.ToDouble(dr["blju_qty10"].ToString(), 0);
                jego11 = jego11 + StCommon.ToDouble(dr["blju_qty11"].ToString(), 0);
                jego12 = jego12 + StCommon.ToDouble(dr["blju_qty12"].ToString(), 0);
                jego13 = jego13 + StCommon.ToDouble(dr["blju_qty13"].ToString(), 0);
                jego14 = jego14 + StCommon.ToDouble(dr["blju_qty14"].ToString(), 0);
                jego15 = jego15 + StCommon.ToDouble(dr["blju_qty15"].ToString(), 0);
                jego16 = jego16 + StCommon.ToDouble(dr["blju_qty16"].ToString(), 0);
                jego17 = jego17 + StCommon.ToDouble(dr["blju_qty17"].ToString(), 0);

                JegoArray[1] = GetAmountFormat(jego1);
                JegoArray[2] = GetAmountFormat(jego2);
                JegoArray[3] = GetAmountFormat(jego3);
                JegoArray[4] = GetAmountFormat(jego4);
                JegoArray[5] = GetAmountFormat(jego5);
                JegoArray[6] = GetAmountFormat(jego6);
                JegoArray[7] = GetAmountFormat(jego7);
                JegoArray[8] = GetAmountFormat(jego8);
                JegoArray[9] = GetAmountFormat(jego9);
                JegoArray[10] = GetAmountFormat(jego10);
                JegoArray[11] = GetAmountFormat(jego11);
                JegoArray[12] = GetAmountFormat(jego12);
                JegoArray[13] = GetAmountFormat(jego13);
                JegoArray[14] = GetAmountFormat(jego14);
                JegoArray[15] = GetAmountFormat(jego15);
                JegoArray[16] = GetAmountFormat(jego16);
                JegoArray[17] = GetAmountFormat(jego17);
                JegoArray[0] = GetAmountFormat(jego1 + jego2 + jego3 + jego4 + jego5 + jego6 + jego7 + jego8 + jego9 + jego10 + jego11 + jego12 + jego13 + jego14 + jego15 + jego16 + jego17);
            }

            return JegoArray;
        }

        public static string GetAreaGubun(string preVal, string KureCode)
        {
            StDataCommon stData = new StDataCommon();
            string resultValue = "";
            string qry = " SELECT * FROM gblKURE WHERE kure_code = '" + KureCode + "' ";
            
            using (IDataReader dr = stData.GetDataReader(qry))
            {
                if (dr.Read())
                {
                    resultValue = dr["Kure_AreaGubun"].ToString().Trim();
                }
                dr.Close();
            }

            return resultValue;
        }

        /*
        스타일별 최소 수량 가져오기
        */
        public static double GetMinimumQty(string styleNox)
        {
            StDataCommon stData = new StDataCommon();

            string preVal = "";

            try
            {
                preVal = HttpContext.Current.Session["PreVal"].ToString();
            }
            catch
            {
                preVal = "tbl";
            }

            double result = 0;

            string qry = " select Dnga_MinimumQty from " + preVal + "DNGA with(nolock) where Dnga_StyleNox = '" + styleNox + "' ";

            DataSet dsC = stData.GetDataSet(qry);

            if (dsC.Tables[0].Rows.Count > 0)
            {
                result = StCommon.ToDouble(dsC.Tables[0].Rows[0][0].ToString(), 0);
            }

            return result;
        }

    }
}
