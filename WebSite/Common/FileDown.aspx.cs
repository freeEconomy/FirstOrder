using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Util;

public partial class Common_FileDown : System.Web.UI.Page
{
	string tDir = string.Empty;
	string tFileName = string.Empty;
	
	protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			tDir = Request["tDir"];
		}
		catch { }
        
		try
		{
			tFileName = Request["tFileName"];
		}
		catch { }

		FileDownLoad();
    }

	private void FileDownLoad()
	{
		string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, tDir);
        
        StFileFolder.DownLoadFile(upDir + tFileName, tFileName);
	}
}
