using System;
using System.Web.UI.WebControls;
using FirstOrder.Data;
using FirstOrder.Util;

public partial class Control_Product : System.Web.UI.UserControl
{
    public delegate void OnButtonClick();
    public event OnButtonClick btnHandler;
    private string preVal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindData()
    {
        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        string kureCode = MemberData.GetLoginSID("KureCode");
        string areaGubun = StCommon.GetAreaGubun(preVal, kureCode);

        string whereQry = "";

        if (areaGubun.Substring(0, 1).Equals("2")) // 대리점이면 Dnga_JegoView = 'N' 조건 추가
            whereQry = " where isnull(Dnga_JegoView,'') <> 'N' ";

        string qry = " select * from " + preVal + "DNGA " + whereQry + " order by Dnga_StyleNox ";

        StDataCommon stData = new StDataCommon();

        this.ddlProduct.Items.Clear();

        ListItem lstIt = new ListItem("", "");
        this.ddlProduct.Items.Insert(0, lstIt);

        this.ddlProduct.DataSource = stData.GetDataSet(qry);
        this.ddlProduct.DataTextField = "Dnga_StyleNox";
        this.ddlProduct.DataValueField = "Dnga_StyleNox";
        this.ddlProduct.DataBind();
    }

    public void BindData(string whereQry)
    {
        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        string kureCode = MemberData.GetLoginSID("KureCode");
        string areaGubun = StCommon.GetAreaGubun(preVal, kureCode);

        if (areaGubun.Substring(0, 1).Equals("2")) // 대리점이면 Dnga_JegoView = 'N' 조건 추가
            whereQry += " and isnull(Dnga_JegoView,'') <> 'N' ";

        string qry = " select * from " + preVal + "DNGA " + whereQry + "order by Dnga_StyleNox ";

        StDataCommon stData = new StDataCommon();

        this.ddlProduct.Items.Clear();

        ListItem lstIt = new ListItem("==전체==", "");
        this.ddlProduct.Items.Insert(0, lstIt);

        this.ddlProduct.DataSource = stData.GetDataSet(qry);
        this.ddlProduct.DataTextField = "Dnga_StyleNox";
        this.ddlProduct.DataValueField = "Dnga_StyleNox";
        this.ddlProduct.DataBind();
    }
}
