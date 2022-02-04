using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Util;

public partial class Common_FileUpload : System.Web.UI.Page
{
	string tDir = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			tDir = Request["tDir"];
		}
		catch { }
    }
	protected void btnUpload_Click(object sender, EventArgs e)
	{
		string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, tDir);

		string fileName = "";
		int fileSize = 0;
		if (this.fuFile.HasFile)
		{
			fileName = StFileFolder.SaveFile(this.fuFile, upDir);
			fileSize = this.fuFile.PostedFile.ContentLength / 1024;

			ClientScript.RegisterStartupScript(typeof(Page), "Upload Completed", "window.parent.iFrame_OnUploadComplete('" + fileName + "','" + fileSize + "');", true);
		}
	}
}
