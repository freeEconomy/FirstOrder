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

public partial class Page_JegoList : System.Web.UI.Page
{
    private string preVal = "";
    private StCommon st = null;
    private int totCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.isSizeNum.Text = MemberData.GetLoginSID("tblSizeNum");

        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        st = new StCommon(preVal);

        if (!IsPostBack)
        {
            this.valPreVal.Text = preVal;

            this.ucTrader.BindData(" where kure_Code in(select Jego_MainBuyer from " + preVal + "JEGO) ");
            this.ucProduct.BindData(" where Dnga_StyleNox in(select Jego_StyleNox from " + preVal + "JEGO) ");
        }
    }

    private void BindMainList()
    {
        string whereQry = "";

        string kurecode = MemberData.GetLoginSID("KureCode");
        string mainBuyer = ((DropDownList)this.ucTrader.FindControl("ddlTrader")).SelectedValue;
        string styleNox = ((DropDownList)this.ucProduct.FindControl("ddlProduct")).SelectedValue;

        whereQry = StCommon.MakeSearchQry("Jego_MainBuyer", mainBuyer, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Jego_StyleNox", styleNox, "S", whereQry);

        StDataCommon stData = new StDataCommon();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select Jego_MainBuyer ");
        sb.AppendLine(" ,(select kure_sangho from gblKURE where kure_code = a.Jego_MainBuyer) as KureSangho ");
        sb.AppendLine(" ,(select Kure_DaePyo from gblKURE where kure_code = a.Jego_MainBuyer) as KureDaePyo ");
        sb.AppendLine(" ,(select '(' + Kure_ZipCode + ')' + Kure_Address1 + ' ' + Kure_Address2 + ' ' + Kure_Address3 from gblKURE where kure_code = a.Jego_MainBuyer) as KureAddr ");
        sb.AppendLine(" ,(select Kure_Tel from gblKURE where kure_code = a.Jego_MainBuyer) as KureTel ");
        sb.AppendLine(" ,(select Kure_MainDamdang_CTel from gblKURE where kure_code = a.Jego_MainBuyer) as KurePhone ");
        sb.AppendLine(" ,Jego_StyleNox ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty01),0) as Jego_Qty01 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty02),0) as Jego_Qty02 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty03),0) as Jego_Qty03 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty04),0) as Jego_Qty04 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty05),0) as Jego_Qty05 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty06),0) as Jego_Qty06 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty07),0) as Jego_Qty07 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty08),0) as Jego_Qty08 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty09),0) as Jego_Qty09 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty10),0) as Jego_Qty10 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty11),0) as Jego_Qty11 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty12),0) as Jego_Qty12 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty13),0) as Jego_Qty13 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty14),0) as Jego_Qty14 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty15),0) as Jego_Qty15 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty16),0) as Jego_Qty16 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty17),0) as Jego_Qty17 ");
        sb.AppendLine(" ,isnull(sum(Jego_QtyTotal),0) as Jego_QtyTotal, Max(convert(nvarchar(10),Jego_CreateDate,120)) as JEGO_CreateDate ");
        sb.AppendLine(" from " + preVal + "JEGO a ");
        sb.AppendLine(" inner join gblKURE b ");
        sb.AppendLine(" on a.Jego_MainBuyer = b.kure_code ");
        sb.AppendLine(" inner join " + preVal + "DNGA c ");
        sb.AppendLine(" on a.Jego_StyleNox = c.Dnga_StyleNox ");
        sb.AppendLine(" where ((isnull(b.Kure_JegoView,'') <> 'N' and isnull(c.Dnga_JegoView,'') <> 'N') or (a.Jego_MainBuyer = '" + kurecode + "')) " + whereQry);
        sb.AppendLine(" group by Jego_MainBuyer,Jego_StyleNox order by Jego_MainBuyer,Jego_StyleNox ");

        DataSet ds = stData.GetDataSet(sb.ToString());
        totCount = ds.Tables[0].Rows.Count;

        this.lblTotCount.Text = "총 : " + string.Format("{0:#,##0}", totCount) + " 건";

        this.lvMain.DataSource = ds;
        this.lvMain.DataBind();

        #region // 합계 바인딩
        double sum1 = 0;
        double sum2 = 0;
        double sum3 = 0;
        double sum4 = 0;
        double sum5 = 0;
        double sum6 = 0;
        double sum7 = 0;
        double sum8 = 0;
        double sum9 = 0;
        double sum10 = 0;
        double sum11 = 0;
        double sum12 = 0;
        double sum13 = 0;
        double sum14 = 0;
        double sum15 = 0;
        double sum16 = 0;
        double sum17 = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dr = ds.Tables[0].Rows[i];

            sum1 += StCommon.ToDouble(dr["Jego_Qty01"].ToString(), 0);
            sum2 += StCommon.ToDouble(dr["Jego_Qty02"].ToString(), 0);
            sum3 += StCommon.ToDouble(dr["Jego_Qty03"].ToString(), 0);
            sum4 += StCommon.ToDouble(dr["Jego_Qty04"].ToString(), 0);
            sum5 += StCommon.ToDouble(dr["Jego_Qty05"].ToString(), 0);
            sum6 += StCommon.ToDouble(dr["Jego_Qty06"].ToString(), 0);
            sum7 += StCommon.ToDouble(dr["Jego_Qty07"].ToString(), 0);
            sum8 += StCommon.ToDouble(dr["Jego_Qty08"].ToString(), 0);
            sum9 += StCommon.ToDouble(dr["Jego_Qty09"].ToString(), 0);
            sum10 += StCommon.ToDouble(dr["Jego_Qty10"].ToString(), 0);
            sum11 += StCommon.ToDouble(dr["Jego_Qty11"].ToString(), 0);
            sum12 += StCommon.ToDouble(dr["Jego_Qty12"].ToString(), 0);
            sum13 += StCommon.ToDouble(dr["Jego_Qty13"].ToString(), 0);
            sum14 += StCommon.ToDouble(dr["Jego_Qty14"].ToString(), 0);
            sum15 += StCommon.ToDouble(dr["Jego_Qty15"].ToString(), 0);
            sum16 += StCommon.ToDouble(dr["Jego_Qty16"].ToString(), 0);
            sum17 += StCommon.ToDouble(dr["Jego_Qty17"].ToString(), 0);
        }

        ((Literal)this.lvMain.FindControl("ltlSum1")).Text = GetAmountFormat(sum1);
        ((Literal)this.lvMain.FindControl("ltlSum2")).Text = GetAmountFormat(sum2);
        ((Literal)this.lvMain.FindControl("ltlSum3")).Text = GetAmountFormat(sum3);
        ((Literal)this.lvMain.FindControl("ltlSum4")).Text = GetAmountFormat(sum4);
        ((Literal)this.lvMain.FindControl("ltlSum5")).Text = GetAmountFormat(sum5);
        ((Literal)this.lvMain.FindControl("ltlSum6")).Text = GetAmountFormat(sum6);
        ((Literal)this.lvMain.FindControl("ltlSum7")).Text = GetAmountFormat(sum7);
        ((Literal)this.lvMain.FindControl("ltlSum8")).Text = GetAmountFormat(sum8);
        ((Literal)this.lvMain.FindControl("ltlSum9")).Text = GetAmountFormat(sum9);
        ((Literal)this.lvMain.FindControl("ltlSum10")).Text = GetAmountFormat(sum10);
        ((Literal)this.lvMain.FindControl("ltlSum11")).Text = GetAmountFormat(sum11);
        ((Literal)this.lvMain.FindControl("ltlSum12")).Text = GetAmountFormat(sum12);
        ((Literal)this.lvMain.FindControl("ltlSum13")).Text = GetAmountFormat(sum13);
        ((Literal)this.lvMain.FindControl("ltlSum14")).Text = GetAmountFormat(sum14);
        ((Literal)this.lvMain.FindControl("ltlSum15")).Text = GetAmountFormat(sum15);
        ((Literal)this.lvMain.FindControl("ltlSum16")).Text = GetAmountFormat(sum16);
        ((Literal)this.lvMain.FindControl("ltlSum17")).Text = GetAmountFormat(sum17);
        ((Literal)this.lvMain.FindControl("ltlSumTotal")).Text = GetAmountFormat(sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9 + sum10 + sum11 + sum12 + sum13 + sum14 + sum15 + sum14 + sum15 + sum16 + sum17);
        #endregion
    }

    protected void lvMain_LayoutCreated(object sender, EventArgs e)
    {
        try
        {
            ((Literal)this.lvMain.FindControl("ltlSize1")).Text = st.SizeName1;
            ((Literal)this.lvMain.FindControl("ltlSize2")).Text = st.SizeName2;
            ((Literal)this.lvMain.FindControl("ltlSize3")).Text = st.SizeName3;
            ((Literal)this.lvMain.FindControl("ltlSize4")).Text = st.SizeName4;
            ((Literal)this.lvMain.FindControl("ltlSize5")).Text = st.SizeName5;
            ((Literal)this.lvMain.FindControl("ltlSize6")).Text = st.SizeName6;
            ((Literal)this.lvMain.FindControl("ltlSize7")).Text = st.SizeName7;
            ((Literal)this.lvMain.FindControl("ltlSize8")).Text = st.SizeName8;
            ((Literal)this.lvMain.FindControl("ltlSize9")).Text = st.SizeName9;
            ((Literal)this.lvMain.FindControl("ltlSize10")).Text = st.SizeName10;
            ((Literal)this.lvMain.FindControl("ltlSize11")).Text = st.SizeName11;
            ((Literal)this.lvMain.FindControl("ltlSize12")).Text = st.SizeName12;
            ((Literal)this.lvMain.FindControl("ltlSize13")).Text = st.SizeName13;
            ((Literal)this.lvMain.FindControl("ltlSize14")).Text = st.SizeName14;
            ((Literal)this.lvMain.FindControl("ltlSize15")).Text = st.SizeName15;
            ((Literal)this.lvMain.FindControl("ltlSize16")).Text = st.SizeName16;
            ((Literal)this.lvMain.FindControl("ltlSize17")).Text = st.SizeName17;
        }
        catch { }
    }

    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            LinkButton lnkMainBuyer = (LinkButton)e.Item.FindControl("lnkMainBuyer");
            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();
            lnkMainBuyer.Text = drItemRow["Jego_MainBuyer"].ToString().Trim() + "(" + drItemRow["KureSangho"].ToString().Trim() + ")";

            int rowsize = 0;
            int itemindex = item.DataItemIndex;
            rowsize = itemindex;

            lnkMainBuyer.OnClientClick = "return ViewMainBuyer('" + rowView["KureDaePyo"].ToString() + "', '" + rowView["KureAddr"].ToString() + "', '" + rowView["KureTel"].ToString() + "', '" + rowView["KurePhone"].ToString() + "', '" + rowsize + "');";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMainList();
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
}