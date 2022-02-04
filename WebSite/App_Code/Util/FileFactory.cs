using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// FileFactory의 요약 설명입니다.
/// </summary>
namespace FirstOrder.Util
{
	public class FileFactory
	{
		public FileFactory()
		{

		}

		public static string SaveFile(FileUpload fu, string upDir)
		{
			DirectoryInfo di = new DirectoryInfo(upDir);

			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = fu.FileName;

			string fFullName = upDir + fileName;

			FileInfo fInfo = new FileInfo(fFullName);

			string newFileName = "";

			if (fInfo.Exists)
			{
				int fIndex = 0;
				string fExtension = fInfo.Extension;
				string fRealName = fileName.Replace(fExtension, "");

				do
				{
					fIndex++;
					newFileName = fRealName + "_" + fIndex.ToString() + fExtension;
					fInfo = new FileInfo(upDir + newFileName);

				} while (fInfo.Exists);

				fFullName = upDir + newFileName;
				fu.PostedFile.SaveAs(fFullName);

				return newFileName;
			}
			else
			{
				fu.PostedFile.SaveAs(fFullName);

				return fileName;
			}
		}

        public static bool CheckImageFile(string fileName)
        {
            bool returnValue = false;

            char[] delimiter = ".".ToCharArray();
            string[] strArray = fileName.ToString().Trim().Split(delimiter);
            string fileNameExt = strArray[strArray.Length - 1].ToLower();

            string[] arrFileName = { "jpg", "gif", "jpeg", "png", "bmp", "ico" };

            for (int i = 0; i < arrFileName.Length; i++)
            {
                if (fileNameExt == arrFileName[i])
                {
                    returnValue = true;
                    break;
                }
            }
            return returnValue;
        }

        public static string[] GetFileNameAndExt(string checkValue)
        {
            char[] delimiter = ".".ToCharArray();
            string[] strArray = checkValue.ToString().Trim().Split(delimiter);

            string realFileName = "";
            for (int i = 0; i < strArray.Length - 1; i++)
            {
                realFileName += strArray[i];
            }
            string extValue = strArray[strArray.Length - 1];

            string[] returnValue = { realFileName, extValue };

            return returnValue;
        }
    }
}