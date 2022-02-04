using System;
using System.Web.UI.WebControls;

using FirstOrder.Util;

public partial class Control_Trader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

	public void BindData()
	{
		this.ddlTrader.Items.Clear();

		ListItem lstIt = new ListItem("", "");
		this.ddlTrader.Items.Insert(0, lstIt);

		string qry = " select * from gblKURE order by Kure_Sangho ";
		
		StDataCommon stData = new StDataCommon();

		this.ddlTrader.DataSource = stData.GetDataSet(qry);
		this.ddlTrader.DataTextField = "Kure_Sangho";
		this.ddlTrader.DataValueField = "Kure_Code";
		this.ddlTrader.DataBind();
    }

    public void BindData(string whereQry)
    {
        this.ddlTrader.Items.Clear();

        ListItem lstIt = new ListItem("====전체====", "");
        this.ddlTrader.Items.Insert(0, lstIt);

        string qry = " select * from gblKURE " + whereQry + " order by Kure_Sangho ";

        StDataCommon stData = new StDataCommon();

        this.ddlTrader.DataSource = stData.GetDataSet(qry);
        this.ddlTrader.DataTextField = "Kure_Sangho";
        this.ddlTrader.DataValueField = "Kure_Code";
        this.ddlTrader.DataBind();
    }
}
