<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
    
using FirstOrder.Data;
using FirstOrder.Util;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState //세션처리용 추가
{
    private string date = "";
    private string times = "";
    private string mainbuyer = "";
    private string sample = "";
    private int line = 0;
    private string delChk = "";
    private string serverPath = "";
    private string ioTable = "";

    public void ProcessRequest(HttpContext context)
    {
        string rtn = string.Empty;
        
        if (context.Request.QueryString["date"] != null)
            date = context.Request.QueryString["date"].ToString();
        else
            throw new ArgumentException("No parameter specified");

        if (context.Request.QueryString["times"] != null)
            times = context.Request.QueryString["times"].ToString();
        else
            throw new ArgumentException("No parameter specified");

        if (context.Request.QueryString["mainbuyer"] != null)
            mainbuyer = context.Request.QueryString["mainbuyer"].ToString();
        else
            throw new ArgumentException("No parameter specified");

        if (context.Request.QueryString["sample"] != null)
            sample = context.Request.QueryString["sample"].ToString();
        else
            throw new ArgumentException("No parameter specified");

        if (context.Request.QueryString["line"] != null)
            line = Convert.ToInt32(context.Request.QueryString["line"].ToString());

        if (context.Request.QueryString["delChk"] != null)
            delChk = context.Request.QueryString["delChk"].ToString();

        try
        {
            if (context.Request.QueryString["ioTable"] != null)
                ioTable = context.Request.QueryString["ioTable"].ToString();
            else
                throw new ArgumentException("No parameter specified");
        }
        catch { }

        serverPath = StFileFolder.GetPhygicalUploadDir(null, "AsFilePath");

        if (delChk == "1")
        {
            DeleteFile(date, times, mainbuyer, sample, line, ioTable);
        }
        
        DirectoryInfo di = new DirectoryInfo(serverPath);
        if (!di.Exists)
        {
            di.Create();
        }

        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                string fname = file.FileName;

                int index = 1;
                while (File.Exists(serverPath + fname))
                {
                    fname = string.Format("{0}[{1}]{2}",
                        Regex.Replace(Path.GetFileNameWithoutExtension(fname), @"\[(\d+)\]", string.Empty),
                        index.ToString(), Path.GetExtension(fname));
                    index++;
                }

                file.SaveAs(serverPath + fname);

                rtn = fname;

                // DB 저장 필요
                AsRequestData asr = new AsRequestData();

                asr.Date = date;
                asr.Times = times;
                asr.Mainbuyer = mainbuyer;
                asr.Sample = sample;
                asr.Imagefilename = fname;
                asr.InsertFile();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(rtn);
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void DeleteFile(string date, string times, string mainbuyer, string sample, int line, string table)
    {
        WorkHistory whis = new WorkHistory();
            
        string ioTable = "";
        ioTable = table;

        StDataCommon stData = new StDataCommon();
        string qry = " select BnpmX_imageFileName from " + ioTable + " where BnpmX_Date = '" + date + "' and BnpmX_Times = '" + times + "' and BnpmX_MainBuyer = '" + mainbuyer + "' and BnpmX_Sample = '" + sample + "' and BnpmX_LineSeqx = '" + line + "' ";
        DataSet ds = stData.GetDataSet(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string filename = ds.Tables[0].Rows[0]["BnpmX_imageFileName"].ToString().Trim();

            if (filename != "")
            {
                // 물리파일 삭제
                StFileFolder.DeleteFile(serverPath, filename);
                
                qry = " delete from " + ioTable + " where BnpmX_Date = '" + date + "' and BnpmX_Times = '" + times + "' and BnpmX_MainBuyer = '" + mainbuyer + "' and BnpmX_Sample = '" + sample + "' and BnpmX_LineSeqx = '" + line + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("AS", "첨부파일삭제", qry);
            }
        }
    }
}