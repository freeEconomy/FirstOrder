using System;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Default : System.Web.UI.Page
{
    private string preVal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StJavaScript js = new StJavaScript(this.Page, true);

        if (MemberData.IsLogin())
        {
            js.WriteJavascript("location.href='/Page/Order_" + preVal + ".aspx';");
        }
        else
        {
            js.WriteJavascript("location.href='/Login/Login.aspx';");
        }
    }
}