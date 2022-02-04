using System;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Login_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MemberData.SetLogOut();

        Response.Redirect("~/Default.aspx", true);
    }
}