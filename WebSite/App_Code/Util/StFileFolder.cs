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
using System.Drawing;

/// <summary>
/// FileFactory의 요약 설명입니다.
/// </summary>
namespace FirstOrder.Util
{

	public class StFileFolder
	{
		public StFileFolder()
		{
			//
			// TODO: 생성자 논리를 여기에 추가합니다.
			//
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

		public static bool CheckUploadFile(string fileName)
		{
			bool returnValue = true;

			char[] delimiter = ".".ToCharArray();
			string[] strArray = fileName.ToString().Trim().Split(delimiter);
			string fileNameExt = strArray[strArray.Length-1].ToLower();

			string[] arrFileName = { "asp", "aspx", "php", "php3", "inc", "asa", "idc", "html", "shtml", "htm", "xml", "xaml", "js", "jsp", "vbs", "cer", "hta" };

			for (int i = 0; i < arrFileName.Length; i++)
			{
				if (fileNameExt == arrFileName[i])
				{
					returnValue = false;
					break;
				}
			}

			return returnValue;
		}

		public static string SaveFile(FileUpload fu, string upDir)
		{
			DirectoryInfo di = new DirectoryInfo(upDir);

			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = fu.FileName;

            // 아래 특수문자가 있으면 다운 및 웹상에서 못찾음. shm 20170215
            fileName = fileName.Replace("+", "").Replace("#", "").Replace("%", "").Replace("&", "");
            string fFullName = upDir + fileName;

            FileInfo fInfo = new FileInfo(fFullName);

            if (CheckUploadFile(fileName) && !fileName.Replace(fInfo.Extension, "").Equals(""))
            {
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
            else
            {
                return "";
            }
		}

		public static string SaveFile(FileUpload fu, string upDir, string savedFileName)
		{
			DirectoryInfo di = new DirectoryInfo(upDir);

			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = fu.FileName;

			if (CheckUploadFile(fileName))
			{
				string fFullName = upDir + savedFileName;

				FileInfo fInfo = new FileInfo(fFullName);

				fu.PostedFile.SaveAs(fFullName);

				return fFullName;
			}
			else
			{
				return "";
			}
		}		


		public static string SaveFile(AjaxControlToolkit.AsyncFileUpload fu, string upDir)
		{
			DirectoryInfo di = new DirectoryInfo(upDir);

			if (!di.Exists)
			{
				di.Create();
			}

			string fileName = fu.FileName;

			if (CheckUploadFile(fileName))
			{
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
					fu.SaveAs(fFullName);

					return newFileName;
				}
				else
				{
					fu.SaveAs(fFullName);

					return fileName;
				}
			}
			else
			{
				return "";
			}
		}
        	
		public static string GetUniqueFileName(string upDir, string fileName)
		{
			string returnValue = "";
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

				returnValue = newFileName;
			}
			else
			{
				returnValue = fileName;
			}

			return returnValue;
		}

		public static void DownLoadFile(string sPhysicalPath, string fileName)
		{
			#region 대용량일 경우?
			/*
			System.IO.Stream iStream = null;

			// Buffer to read 10K bytes in chunk:
			byte[] buffer = new Byte[10000];

			// Length of the file:
			int length;

			// Total bytes to read:
			long dataToRead;

			// Identify the file to download including its path.
			string filepath = sPhysicalPath;

			// Identify the file name.﻿
			//string  filename  = System.IO.Path.GetFileName(filepath);

			try
			{
				// Open the file.
				iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, 
				System.IO.FileAccess.Read,System.IO.FileShare.Read);


				// Total bytes to read:
				dataToRead = iStream.Length;

				HttpContext.Current.Response.ContentType = "application/octet-stream";
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
	
				// Read the bytes.
				while (dataToRead > 0)
				{
					// Verify that the client is connected.
					if (HttpContext.Current.Response.IsClientConnected) 
					{
						// Read the data in buffer.
						length = iStream.Read(buffer, 0, 10000);

						// Write the data to the current output stream.
						HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

						// Flush the data to the HTML output.
						HttpContext.Current.Response.Flush();

						buffer= new Byte[10000];
						dataToRead = dataToRead - length;
					}
					else
					{
						//prevent infinite loop if user disconnects
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex) 
			{
				// Trap the error, if any.
				HttpContext.Current.Response.Write("Error : " + ex.Message);
			}
			finally
			{
				if (iStream != null) 
				{
					//Close the file.
					iStream.Close();
				}
				HttpContext.Current.Response.Close();
			}
			*/
			#endregion	
			
			try
			{
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ContentType = "application/unknown";
				HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
				HttpContext.Current.Response.AddHeader("Expires", "0");
				HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, new System.Text.UTF8Encoding()).Replace("+", " "));
				HttpContext.Current.Response.TransmitFile(sPhysicalPath);
				//HttpContext.Current.Response.WriteFile(sPhysicalPath);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			catch
			{
			}
		}

		public static void DeleteFile(string sPhysicalPath, string fileName)
		{
			string fFullName = sPhysicalPath + fileName;

			FileInfo fInfo = new FileInfo(fFullName);

			if (fInfo.Exists)
			{
				try
				{
					fInfo.Delete();
				}
				catch
				{
				}
			}
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

		public static string GetFileExtName(string checkValue)
		{
			string extValue = string.Empty;

			char[] delimiter = ".".ToCharArray();
			string[] strArray = checkValue.ToString().Trim().Split(delimiter);

			string realFileName = "";
			for (int i = 0; i < strArray.Length - 1; i++)
			{
				realFileName += strArray[i];
			}
			
			extValue = strArray[strArray.Length - 1];

			return extValue;
		}

		public static string GetFileIconByFileName(string checkValue)
		{
			string returnValue = "";
			
			if (!string.IsNullOrEmpty(checkValue))
			{
				string[] arrFileName = GetFileNameAndExt(checkValue);

				switch (arrFileName[1].ToUpper())
				{
					case "ACE":
						returnValue = "~/images/fileicon/ACE.gif";
						break;

					case "BMP":
						returnValue = "~/images/fileicon/BMP.gif";
						break;

					case "CHM":
						returnValue = "~/images/fileicon/CHM.gif";
						break;

					case "COM":
						returnValue = "~/images/fileicon/COM.gif";
						break;

					case "DOC":
						returnValue = "~/images/fileicon/DOC.gif";
						break;

					case "EXE":
						returnValue = "~/images/fileicon/EXE.gif";
						break;

					case "GIF":
						returnValue = "~/images/fileicon/GIF.gif";
						break;

					case "HLP":
						returnValue = "~/images/fileicon/HLP.gif";
						break;

					case "HTM":
						returnValue = "~/images/fileicon/HTM.gif";
						break;

					case "HWP":
						returnValue = "~/images/fileicon/HWP.gif";
						break;

					case "JPG":
						returnValue = "~/images/fileicon/JPG.gif";
                        break;

                    case "PNG":
                        returnValue = "~/images/fileicon/PNG.gif";
                        break;

					case "MP3":
						returnValue = "~/images/fileicon/MP3.gif";
						break;

					case "MPG":
						returnValue = "~/images/fileicon/MPG.gif";
						break;

					case "PDF":
						returnValue = "~/images/fileicon/PDF.gif";
						break;

					case "RA":
						returnValue = "~/images/fileicon/RA.gif";
						break;

					case "RAR":
						returnValue = "~/images/fileicon/RAR.gif";
						break;

					case "TXT":
						returnValue = "~/images/fileicon/TXT.gif";
						break;

					case "WAV":
						returnValue = "~/images/fileicon/WAV.gif";
						break;

					case "XLS":
						returnValue = "~/images/fileicon/XLS.gif";
						break;

					case "ZIP":
						returnValue = "~/images/fileicon/ZIP.gif";
						break;
					
					default:
						returnValue = "~/images/fileicon/ETC.gif";
						break;
				}
			}

			return returnValue;
		}

		public static byte[] FileUploadToByte(FileUpload fu)
		{
			if (fu.HasFile)
			{
				System.Drawing.Image tImage = System.Drawing.Image.FromStream(fu.FileContent);

				MemoryStream ms = new MemoryStream();
				tImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
				ms.Position = 0;
				byte[] imgByte = new byte[ms.Length];
				ms.Read(imgByte, 0, (int)ms.Length);
				ms.Close();

				return imgByte;
			}
			else
			{
				return null;
			}
		}

		private static string CheckConfigDirOpt(string checkValue)
		{
			string returnValue = string.Empty;

			switch (checkValue.Substring(0,1))
			{
				case "/":
					returnValue = "0";

					break;

				case @"\":
					returnValue = "1";
					break;

				default:
					returnValue = "2";
					break;
			}

			return returnValue;
		}

		private static string MakeAddDirByCheckConfigDirOpt(string addFolder, string checkConfigDirOpt)
		{
			string returnValue = string.Empty;

			if (!string.IsNullOrEmpty(addFolder))
			{
				switch (checkConfigDirOpt)
				{
					case "0":
						returnValue = addFolder + "/";
						break;

					case "1":
						returnValue = addFolder + @"\";
						break;

					case "2":
						returnValue = addFolder + @"\";
						break;
				}
			}

			return returnValue;
		}

		private static string MakeAddDirByCheckConfigDirOpt(string[] addFolder, string checkConfigDirOpt)
		{
			string returnValue = string.Empty;

			for (int i = 0; i < addFolder.Length; i++)
			{
				if (!string.IsNullOrEmpty(addFolder[i]))
				{
					switch (checkConfigDirOpt)
					{
						case "0":
							returnValue += addFolder[i] + "/";
							break;

						case "1":
							returnValue += addFolder[i] + @"\";
							break;

						case "2":
							returnValue += addFolder[i] + @"\";
							break;
					}
				}
			}

			return returnValue;
		}

		public static long GetFileSize(string fullFilePath)
		{
			long returnValue = 0;

			FileInfo fi = new FileInfo(fullFilePath);

			if (fi.Exists)
			{
				returnValue = fi.Length;
			}

			return returnValue;
		}

		public static int[] GetImageSize(string fullFilePath)
		{
			int[] returnValue = { 0, 0, 1 };
			try
			{
				Bitmap myBitmap = new Bitmap(fullFilePath);

				if (myBitmap.Width > 0 && myBitmap.Height > 0)
				{
					returnValue[0] = myBitmap.Width;
					returnValue[1] = myBitmap.Height;
					returnValue[2] = 0;
				}

				myBitmap.Dispose();
			}
			catch
			{
			}

			return returnValue;
		}

        public static string GetPhygicalUploadDir(Page sourcePage, string addFolderName)
        {
            string returnValue = string.Empty;
            string _defaultFolder = ConfigurationManager.AppSettings["RootDir"];
            string _appSetFolder = ConfigurationManager.AppSettings[addFolderName];

            returnValue = _defaultFolder + @"\" + _appSetFolder + @"\";
            
            return @returnValue;
        }
    }
}