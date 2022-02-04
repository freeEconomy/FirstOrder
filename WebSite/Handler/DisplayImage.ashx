<%@ WebHandler Language="C#" Class="DisplayImage" %>

using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Util;

public class DisplayImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    byte[] empPic = null;
    long seq = 0;
    string kureCode;
    string churchid = "";

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["code"] != null)
            kureCode = context.Request.QueryString["code"];
        else
            throw new ArgumentException("No parameter specified");

        string preVal = "";
        try
        {
            preVal = context.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        // Convert Byte[] to Bitmap
        Bitmap newBmp = ConvertToBitmap(ShowEmpImage(kureCode, preVal));
        if (newBmp != null)
        {
            newBmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            newBmp.Dispose();
        }
    }

    // Convert byte array to Bitmap (byte[] to Bitmap)
    protected Bitmap ConvertToBitmap(byte[] bmp)
    {
        Bitmap bit = null;

        if (bmp != null)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            bit = (Bitmap)tc.ConvertFrom(bmp);
        }
        else
        {
            bit = new Bitmap(10, 10);
            Graphics gra = Graphics.FromImage(bit);
            gra.Clear(Color.White);
        }
        return bit;
    }

    public byte[] ShowEmpImage(string kureCode, string preVal)
    {
        SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

        byte[] empPic = null;

        string qry = "SELECT * FROM " + preVal + "JEPG WHERE Jepg_StyleNox=@Jepg_StyleNox";

        DbCommand cmd = db.GetSqlStringCommand(qry);

        db.AddInParameter(cmd, "Jepg_StyleNox", DbType.String, kureCode);

        DataSet ds = db.ExecuteDataSet(cmd);

        // 기본이미지를 byte[] 로 변환 empPic = ??
        FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath("/images/empty.jpg"));

        if (fi.Exists)
        {
            byte[] imgByte = null;

            string fileName = fi.Name;
            fileName = Path.GetFileName(fileName);
            string[] arrFileName = FileFactory.GetFileNameAndExt(fileName);

            //try
            //{
            System.Drawing.Image orgImage = System.Drawing.Image.FromFile(fi.FullName);

            MemoryStream ms = new MemoryStream();
            orgImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            imgByte = new byte[ms.Length];
            ms.Read(imgByte, 0, (int)ms.Length);
            ms.Close();

            //}
            //catch { }

            empPic = imgByte;
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Jepg_Image"].ToString()))
                {
                    empPic = (byte[])ds.Tables[0].Rows[0]["Jepg_Image"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return empPic;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
