using System;
using System.Data;
using System.IO;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Master_MainPop : System.Web.UI.MasterPage
{
    public string jsCssVer = DateTime.Now.ToString("yyyyMMddhhmmss");

    private StDataCommon stData = new StDataCommon();
    public string TitleName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (MemberData.IsLogin() == false)
        {
            StJavaScript js = new StJavaScript(this.Page, true);
            js.ShowAlertMessage("로그인 후 사용해 주세요.", "self.close();");
        }

        string sPageName = Path.GetFileName(Request.PhysicalPath);

        switch (sPageName)
        {
            case "BaeSong.aspx":

                TitleName = "배송지 선택";

                break;

            case "Chat.aspx":

                TitleName = "Q&A 대화방";

                break;                
        }

        if (!IsPostBack)
        {
            
        }
    }    
}
