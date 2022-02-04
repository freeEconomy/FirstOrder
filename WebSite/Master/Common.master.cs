using System;
using System.IO;

using FirstOrder.Util;

public partial class Master_Common : System.Web.UI.MasterPage
{
    public string jsCssVer = DateTime.Now.ToString("yyyyMMddhhmmss");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StCommon.IsMobileAgent(Request.UserAgent))
        {
            Response.Redirect("~/Mobile/", true);
        }
    }
}
