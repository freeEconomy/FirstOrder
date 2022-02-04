using System;
using System.Data;
using System.IO;
using System.Web;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Common_ZoomImage : System.Web.UI.Page
{
    private StCommon st = new StCommon();
    private string preVal = "";
    private string gubun = "";
    private string code = "";

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

        try
        {
            gubun = Request["gubun"];
        }
        catch { }

        try
        {
            code = Request["code"];
        }
        catch { }

        int width = st.GetRequestParameter("width", 0);
        int height = st.GetRequestParameter("height", 0);

        if (!IsPostBack)
		{
            if (gubun == "style")
            {
                if (width == 0)
                {
                    StDataCommon stData = new StDataCommon();
                    string qry = " SELECT Jepg_Image FROM " + preVal + "JEPG WHERE Jepg_StyleNox = '" + code + "' ";
                    DataSet ds = stData.GetDataSet(qry);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        try
                        {
                            if (!string.IsNullOrEmpty(dr["Jepg_Image"].ToString()))
                            {
                                byte[] empPic = (byte[])dr["Jepg_Image"];

                                MemoryStream ms = new MemoryStream(empPic);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                                width = image.Width;
                                height = image.Height;
                            }
                        }
                        catch { }
                    }
                }

                imgFile.ImageUrl = "~/Handler/DisplayImage.ashx?code=" + code;
            }
            else
            {
                string file = code;

                imgFile.ImageUrl = "~/Upload/AS/" + file;
            }

            imgFile.Width = width;
            imgFile.Height = height;

            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("SetReSize('" + width + "','" + height + "');");
        }
    }
}
