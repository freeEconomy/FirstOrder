using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

using FirstOrder.Util;
using System.Text.RegularExpressions;
using FirstOrder.Data;

public partial class Page_ReturnSelect : System.Web.UI.Page
{
    private string preVal = "";

    private string searchValue = "";

    private string paramDate = "";
    private string paramTime = "";
    private string paramKure = "";

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
            searchValue = Server.HtmlEncode(Request["searchValue"].Trim());

            paramDate = Server.HtmlEncode(Request["paramDate"].Trim());
            paramTime = Server.HtmlEncode(Request["paramTime"].Trim());
            paramKure = Server.HtmlEncode(Request["paramKure"].Trim());
        }
        catch { }

        if (!IsPostBack)
        {
            // 현재 발주상태 체크
            string nowBonsaCheck = StCommon.GetBonsaCheck(preVal, paramKure, paramDate, paramTime);
            if (nowBonsaCheck != "" && nowBonsaCheck != "0")
            {
                string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.ShowAlertMessage("현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.", "parent.$('#dialog').dialog('close');");
            }
            else
            {
                this.txtProduct.Text = searchValue;

                BindList("");
            }
        }
    }

    private void BindList(string mode)
    {
        string qry = "";
        string whereQry = "";

        string kurecode = MemberData.GetLoginSID("KureCode");
        string styleNox = StCommon.ReplaceSQ(this.txtProduct.Text);

        whereQry = StCommon.MakeSearchQry("Blju_StyleNox", styleNox, "%", whereQry);

        qry = " select * from " + preVal + "BLJU where Blju_Mainbuyer = '" + kurecode + "'  " + whereQry + " order by Blju_Date desc, Blju_Times desc ";
        DataSet ds = stData.GetDataSet(qry);
        totCount = ds.Tables[0].Rows.Count;

        this.lvList.DataSource = ds;
        this.lvList.DataBind();
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            
            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow dr = ((DataRowView)e.Item.DataItem).Row;
            
            // 함수 적용
            Literal ltlSelectScript = (Literal)e.Item.FindControl("ltlSelectScript");

            ltlSelectScript.Text = "Onclick=\"SetProduct('" + dr["Blju_StyleNox"].ToString() + "','" + dr["Blju_Date"].ToString() + "','" + dr["Blju_Times"].ToString() + "','" + dr["Blju_MainBuyer"].ToString() + "','" + dr["Blju_Sample"].ToString() + "','" + dr["Blju_Line"].ToString() + "')\"";
        }
    }

    public string GetAmountFormat(Object obj)
    {
        string result = obj.ToString().Trim();
        if (result == "0")
        {
            result = "";
        }

        if (result != "")
        {
            result = StCommon.NumberFormat(Convert.ToDouble(result));
        }

        return result;
    }

    protected void lvList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dp = ((ListView)sender).FindControl("dpList") as DataPager;
        dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindList("sch");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindList("sch");
    }
}

public class ProductInfo
{
    public string JegoQty01 { get; set; }
    public string JegoQty02 { get; set; }
    public string JegoQty03 { get; set; }
    public string JegoQty04 { get; set; }
    public string JegoQty05 { get; set; }
    public string JegoQty06 { get; set; }
    public string JegoQty07 { get; set; }
    public string JegoQty08 { get; set; }
    public string JegoQty09 { get; set; }
    public string JegoQty10 { get; set; }
    public string JegoQty11 { get; set; }
    public string JegoQty12 { get; set; }
    public string JegoQty13 { get; set; }
    public string JegoQty14 { get; set; }
    public string JegoQty15 { get; set; }
    public string JegoQty16 { get; set; }
    public string JegoQty17 { get; set; }
    public string JegoQtyTotal { get; set; }
    public string StyleNox { get; set; }
    public string MainName { get; set; }
    public string SubName { get; set; }
    public string SpecColor { get; set; }
    public string LowPrice { get; set; }
    public string JustPrice { get; set; }
    public string BigSizePrice { get; set; }
    public string SizeBoxQty { get; set; }
    public string BoxQty { get; set; }
    public string IsDuple { get; set; }
    public string Msg { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}