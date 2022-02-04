using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Data;
using FirstOrder.Util;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public partial class Page_AsProduct : System.Web.UI.Page
{
    private string preVal = "";
    private StCommon st = null;
    private int totCount = 0;
    private SqlDatabase tbuc_db = StDBConn.GetOpenDB(OpenDBType.tbucDB);
    StDataCommon stData = new StDataCommon();
    StDataCommon stDataTbuc = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        stDataTbuc = new StDataCommon(tbuc_db);

        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }
        
        if (!IsPostBack)
        {
            this.valPreVal.Text = preVal;
        }        
    }

    private void BindMainList()
    {
        string whereQry = "";

        string styleNox = "";

        try
        {
            styleNox = this.txtProduct.Text;
        }
        catch { }

        whereQry = StCommon.MakeSearchQry("Jego_StyleNox", styleNox, "S", whereQry);

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select Jego_StyleNox ");
        sb.AppendLine(" ,Jego_Qty01 ");
        sb.AppendLine(" ,Jego_Qty02 ");
        sb.AppendLine(" ,Jego_Qty03 ");
        sb.AppendLine(" ,Jego_Qty04 ");
        sb.AppendLine(" ,Jego_Qty05 ");
        sb.AppendLine(" ,Jego_Qty06 ");
        sb.AppendLine(" ,Jego_Qty07 ");
        sb.AppendLine(" ,Jego_Qty08 ");
        sb.AppendLine(" ,Jego_Qty09 ");
        sb.AppendLine(" ,Jego_Qty10 ");
        sb.AppendLine(" ,Jego_Qty11 ");
        sb.AppendLine(" ,Jego_Qty12 ");
        sb.AppendLine(" ,Jego_Qty13 ");
        sb.AppendLine(" ,Jego_Qty14 ");
        sb.AppendLine(" ,Jego_Qty15 ");
        sb.AppendLine(" ,Jego_Qty16 ");
        sb.AppendLine(" ,Jego_Qty17 ");
        sb.AppendLine(" ,Jego_QtyTotal ");
        sb.AppendLine(" from View_" + preVal + "JEGO_Summary where 1=1 " + whereQry);
        sb.AppendLine(" order by Jego_StyleNox ");

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
        try
        {
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
            ((Literal)this.lvMain.FindControl("ltlSumTotal")).Text = GetAmountFormat(sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9 + sum10 + sum11 + sum12 + sum13 + sum14 + sum15 + sum16 + sum17);
        }
        catch { }
        #endregion
    }

    protected void lvMain_LayoutCreated(object sender, EventArgs e)
    {
        st = new StCommon(preVal);

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
        if (preVal == "tbl")
        { }
        else
        {
            ((Literal)this.lvMain.FindControl("ltlSize11")).Text = st.SizeName11;
            ((Literal)this.lvMain.FindControl("ltlSize12")).Text = st.SizeName12;
            ((Literal)this.lvMain.FindControl("ltlSize13")).Text = st.SizeName13;
            ((Literal)this.lvMain.FindControl("ltlSize14")).Text = st.SizeName14;
            ((Literal)this.lvMain.FindControl("ltlSize15")).Text = st.SizeName15;
            ((Literal)this.lvMain.FindControl("ltlSize16")).Text = st.SizeName16;
            ((Literal)this.lvMain.FindControl("ltlSize17")).Text = st.SizeName17;
        }
    }

    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");

            //ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();
            //ltlViewNumber.Text = ((item.DataItemIndex) + 1).ToString();
        }
    }

    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        string kurecode = MemberData.GetLoginSID("KureCode");
        string qry = " select * from " + preVal + "BLJU where Blju_Date = '" + this.hidDate.Value + "' and Blju_Times = '" + this.hidTimes.Value + "' and Blju_MainBuyer = '" + kurecode + "' and Blju_Sample = '" + this.hidSample.Value + "' and Blju_Line = '" + this.hidLine.Value + "' ";
        DataSet ds = stData.GetDataSet(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            this.txtDate.Text = dr["Blju_Date"].ToString();
            this.txtTime.Text = dr["Blju_Times"].ToString();

            double net = Convert.ToDouble(dr["Blju_JustAmount"].ToString());
            double vat = net * (0.1);
            double hap = net + vat;

            this.txtNetAmount.Text = StCommon.NumberFormat(net);
            this.txtVatAmount.Text = StCommon.NumberFormat(vat);
            this.txtHapAmount.Text = StCommon.NumberFormat(hap);

            this.txtEtc.Text = dr["Blju_Remark"].ToString();

            BindMainList();
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string appKind = "-본사재고조회";
        string fileName = Server.UrlDecode(DateTime.Now.ToString("yyyy_MM_dd").ToString() + appKind);
        fileName = HttpUtility.UrlEncode(fileName, new UTF8Encoding()).Replace("+", "%20");

        Response.Clear();
        Response.ClearHeaders();
        Response.ClearContent();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-disposition", "attachment;filename=" + fileName + ".xls");
        Response.Charset = "utf-8";
        Response.ContentEncoding = Encoding.GetEncoding("utf-8");

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        
        hw.Write("<table width='100%' border='1' cellpadding='0' cellspacing='0' >");
        this.lvMain.EnableViewState = false;
        this.lvMain.RenderControl(hw);
        hw.Write("</table>");
        
        Response.Write("<meta http-equiv=Content-Type content=''text/html; charset=utf-8''>");
        Response.Write("<link rel='stylesheet' href='http://" + Request["HTTP_HOST"] + "/css/style.css'>");
        Response.Write(sw.ToString());
        Response.End();
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