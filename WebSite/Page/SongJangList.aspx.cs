using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Page_SongJangList : System.Web.UI.Page
{
    private string preVal = "";

    private string blju_date = "";
    private string blju_times = "";
    private string blju_mainbuyer = "";
    private string blju_sample = "";

    private int totCount = 0;

    private StDataCommon stData = new StDataCommon();

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
            blju_date = Server.HtmlEncode(Request["blju_date"].Trim());
            blju_times = Server.HtmlEncode(Request["blju_times"].Trim());
            blju_mainbuyer = Server.HtmlEncode(Request["blju_mainbuyer"].Trim());
            blju_sample = Server.HtmlEncode(Request["blju_sample"].Trim());
        }
        catch { }

        if (!IsPostBack)
        {
            BindList();
        }
    }

    private void BindList()
    {
        string whereQry = " where Bjhd3_MainBuyer = '" + blju_mainbuyer + "' ";
        whereQry = StCommon.MakeSearchQry("Bjhd3_Date", blju_date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd3_Times", blju_times, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd3_Sample", blju_sample, "S", whereQry);

        string qry = " select Bjhd3_Seqx, Bjhd3_SongJangNox from " + preVal + "BJHD3 " + whereQry + " order by Bjhd3_Seqx ";
        DataSet ds = stData.GetDataSet(qry);

        totCount = ds.Tables[0].Rows.Count;

        this.lvList.DataSource = ds;
        this.lvList.DataBind();
    }
}
