using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;

using FirstOrder.Util;

public partial class Print_PDFPrint : System.Web.UI.Page
{
    private StCommon st = new StCommon();

    protected void Page_Load(object sender, EventArgs e)
    {
        string opt = "";
        string size = "";

        try
        {
            opt = st.GetRequestParameter("opt", "");
        }
        catch { }

        try
        {
            size = st.GetRequestParameter("size", "");
        }
        catch { }

        string url = HttpUtility.UrlDecode(Request["url"]);

        if (size == "")
        {
            size = "A4";
        }

        PdfPrint(url, opt, size);
    }

    private void PdfPrint(string _url, string _opt, string _size)
    {
        byte[] fileContent = GeneratePDFFile(_url, _size);

        if (fileContent != null)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", fileContent.Length.ToString());
            Response.BinaryWrite(fileContent);
            Response.End();
        }
    }

    private byte[] GeneratePDFFile(string url, string _size)
    {
        byte[] fileContent = null;

        //try
        //{
        var savePath = Server.MapPath("~/Print");
        var fileName = Path.Combine(savePath, "report.pdf");

        var wkhtml = ConfigurationManager.AppSettings["wkhtml"];
        var p = new Process();
        
        string switches = "";
        switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
        switches += "--page-size " + _size;
        
        Process process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        
        process.StartInfo.FileName = wkhtml;
        
        process.StartInfo.Arguments = switches + " " + url + " " + fileName;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        
        process.Start();
        
        process.WaitForExit();
        
        int returnCode = process.ExitCode;
        process.Close();
        
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        fileContent = new byte[(int)fs.Length];
        
        fs.Read(fileContent, 0, (int)fs.Length);
        
        fs.Close();

        return fileContent;
    }
}
