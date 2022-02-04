using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Page_AsRequest : System.Web.UI.Page
{
    private SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);
    private string preVal = "";
    private StCommon st = null;
    private SqlDatabase tbuc_db = StDBConn.GetOpenDB(OpenDBType.tbucDB);
    private StDataCommon stData = new StDataCommon();
    private StDataCommon stDataTbuc = null;
    private int totCount = 0;

    private string mode = "";
    private string param_date = "";
    private string param_times = "";
    private string param_mainbuyer = "";
    private string param_sample = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.isSizeNum.Text = MemberData.GetLoginSID("tblSizeNum");

        stDataTbuc = new StDataCommon(tbuc_db);

        try
        {
            preVal = Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        this.hidIoTable.Value = preVal + "BNPMX";

        try
        {
            mode = StCommon.ReplaceSQ(Request["mode"]);
            param_date = StCommon.ReplaceSQ(Request["param_date"]);
            param_times = StCommon.ReplaceSQ(Request["param_times"]);
            param_mainbuyer = StCommon.ReplaceSQ(Request["param_mainbuyer"]);
            param_sample = StCommon.ReplaceSQ(Request["param_sample"]);
        }
        catch { }

        this.hidMode.Value = mode;
        
        this.ltlView.Text = "style=\"display:none\"";
        this.ltlViewFoot.Text = "style=\"display:none\"";

        this.btnEdit.Visible = false;
        this.btnList.Visible = false;
        this.divFileEdit.Visible = true;
        this.divFileView.Visible = false;

        this.ltlNumber.Visible = false;

        if (!IsPostBack)
        {
            if (mode != "")
            {
                this.divReg.Visible = false;
                this.ltlView.Text = "";
                this.ltlViewFoot.Text = "";
                this.btnList.Visible = true;

                if (mode == "view")
                {
                    this.btnSave.Visible = false;
                    this.btnComplete.Visible = false;
                    this.btnEdit.Visible = true;
                    this.divFileEdit.Visible = true;
                    this.divFileView.Visible = false;

                    this.ltlNumber.Visible = true;
                    this.Upload.Visible = false;
                    this.fileMargin.Visible = false;

                    this.txtEtc.Enabled = false;
                }
            }

            string kurecode = MemberData.GetLoginSID("KureCode");

            this.hidAsMainbuyer.Value = kurecode;
            this.hidAsDate.Value = DateTime.Now.ToShortDateString();
            this.hidAsTimes.Value = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);
            this.hidAsSample.Value = "0";

            this.valPreVal.Text = preVal;

            string whereQry = "";

            if (preVal == "tbl")
            {
                whereQry = " and substring(Common_Code,1,1) = 'M' ";
            }
            else if (preVal == "abl")
            {
                whereQry = " and substring(Common_Code,1,1) = 'T' ";

                this.divBrand.Visible = false;
            }

            string qry = " select (case when Common_Remark1='대분류' then '' else Common_Code end) as code, (case when Common_Remark1='대분류' then '--' + Common_CodeName + '--' else Common_Remark1 end) as name from gblCOMMON where Common_Key = 'T0319' and Common_Code not in('TA0') " + whereQry +" order by Common_Code ";
            DataSet ds = stData.GetDataSet(qry);
            StringBuilder sb = new StringBuilder();
            sb.Append(" <option value=\"\">==사유 선택==</option>");
            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
            {
                DataRow dr = ds.Tables[0].Rows[k];
                sb.Append(" <option value=\"" + dr["code"] + "\">" + dr["name"] + "</option>");
            }
            this.valBnpmSelect.Text = sb.ToString();
            
            st = new StCommon(preVal);
            
            this.ltlSize1.Text = st.SizeName1;
            this.ltlSize2.Text = st.SizeName2;
            this.ltlSize3.Text = st.SizeName3;
            this.ltlSize4.Text = st.SizeName4;
            this.ltlSize5.Text = st.SizeName5;
            this.ltlSize6.Text = st.SizeName6;
            this.ltlSize7.Text = st.SizeName7;
            this.ltlSize8.Text = st.SizeName8;
            this.ltlSize9.Text = st.SizeName9;
            this.ltlSize10.Text = st.SizeName10;
            this.ltlSize11.Text = st.SizeName11;
            this.ltlSize12.Text = st.SizeName12;
            if (preVal == "tbl")
            { }
            else
            {
                this.ltlSize13.Text = st.SizeName13;
                this.ltlSize14.Text = st.SizeName14;
                this.ltlSize15.Text = st.SizeName15;
                this.ltlSize16.Text = st.SizeName16;
                this.ltlSize17.Text = st.SizeName17;
            }

            this.txtProduct.Text = "";
            
            if (mode != "")
            {
                BindModify(mode, param_date, param_times, param_mainbuyer, param_sample);
            }
        }        
    }

    private void BindMainList()
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        string styleNox = "";

        try
        {
            styleNox = StCommon.ReplaceSQ(this.txtProduct.Text);
        }
        catch { }
        
        StringBuilder sb = new StringBuilder();

        if (!this.cboTBuc.Checked)
        {
            this.bljuHistory.Visible = true;
            this.pn01.Visible = true;
            this.dpMain.Visible = true;

            this.ltlView.Text = "style=\"display:none\"";
            this.ltlViewFoot.Text = "style=\"display:none\"";

            sb.AppendLine(" select * ");
            sb.AppendLine(" ,(select Bjhd_Remark from " + preVal + "BJHD where isnull(BJHD_Date,'') = isnull(a.blju_Date,'') and isnull(BJHD_Times,'') = isnull(a.blju_Times,'') and isnull(BJHD_MainBuyer,'') = isnull(a.blju_MainBuyer,'') and isnull(BJHD_Sample,'') = isnull(a.blju_Sample,'')) as Etc ");
            sb.AppendLine(" from " + preVal + "BLJU a where blju_mainbuyer = '" + kurecode + "' and blju_styleNox = '" + styleNox + "' ");
            sb.AppendLine(" and (select count(*) from " + preVal + "BJHD where isnull(BJHD_Date,'') = isnull(a.blju_Date,'') and isnull(BJHD_Times,'') = isnull(a.blju_Times,'') and isnull(BJHD_MainBuyer,'') = isnull(a.blju_MainBuyer,'') and isnull(BJHD_Sample,'') = isnull(a.blju_Sample,'') and (Bjhd_Bonsa_Check = 'Z' AND Bjhd_Bonsa_Check1 IN('X','Z'))) > 0 ");
            sb.AppendLine(" order by blju_date desc, blju_times desc ");

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
            double net = 0;
            double vat = 0;
            double hap = 0;
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    sum1 += StCommon.ToDouble(dr["blju_Qty01"].ToString(), 0);
                    sum2 += StCommon.ToDouble(dr["blju_Qty02"].ToString(), 0);
                    sum3 += StCommon.ToDouble(dr["blju_Qty03"].ToString(), 0);
                    sum4 += StCommon.ToDouble(dr["blju_Qty04"].ToString(), 0);
                    sum5 += StCommon.ToDouble(dr["blju_Qty05"].ToString(), 0);
                    sum6 += StCommon.ToDouble(dr["blju_Qty06"].ToString(), 0);
                    sum7 += StCommon.ToDouble(dr["blju_Qty07"].ToString(), 0);
                    sum8 += StCommon.ToDouble(dr["blju_Qty08"].ToString(), 0);
                    sum9 += StCommon.ToDouble(dr["blju_Qty09"].ToString(), 0);
                    sum10 += StCommon.ToDouble(dr["blju_Qty10"].ToString(), 0);
                    sum11 += StCommon.ToDouble(dr["blju_Qty11"].ToString(), 0);
                    sum12 += StCommon.ToDouble(dr["blju_Qty12"].ToString(), 0);
                    sum13 += StCommon.ToDouble(dr["blju_Qty13"].ToString(), 0);
                    sum14 += StCommon.ToDouble(dr["blju_Qty14"].ToString(), 0);
                    sum15 += StCommon.ToDouble(dr["blju_Qty15"].ToString(), 0);
                    sum16 += StCommon.ToDouble(dr["blju_Qty16"].ToString(), 0);
                    sum17 += StCommon.ToDouble(dr["blju_Qty17"].ToString(), 0);

                    double net2 = StCommon.ToDouble(dr["Blju_JustAmount"].ToString(), 0);
                    double vat2 = StCommon.ToDouble(dr["Blju_JustAmount"].ToString(), 0) * (0.1);

                    net += net2;
                    vat += vat2;
                    hap += net2 + vat2;
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
                ((Literal)this.lvMain.FindControl("ltlSumNet")).Text = GetAmountFormat(net);
                ((Literal)this.lvMain.FindControl("ltlSumVat")).Text = GetAmountFormat(vat);
                ((Literal)this.lvMain.FindControl("ltlSumHap")).Text = GetAmountFormat(hap);
            }
            catch { }
            #endregion
        }
        else
        {
            this.bljuHistory.Visible = false;
            this.pn01.Visible = false;
            this.dpMain.Visible = false;

            sb.AppendLine(" select * ");
            sb.AppendLine(" from tblSTYLE where Dnga_StyleNox = '" + styleNox + "' ");
            
            DataSet ds = stDataTbuc.GetDataSet(sb.ToString());
            totCount = ds.Tables[0].Rows.Count;

            if (totCount == 0)
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.WriteJavascript("EmptyMessage();");
            }
            else
            {
                this.ltlView.Text = "";
                this.ltlViewFoot.Text = "";

                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.WriteJavascript("SetTBuc();");
            }
        }
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
        ((Literal)this.lvMain.FindControl("ltlSize11")).Text = st.SizeName11;
        ((Literal)this.lvMain.FindControl("ltlSize12")).Text = st.SizeName12;
        if (preVal == "tbl")
        { }
        else
        {
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
            Literal ltlVatAmount = (Literal)e.Item.FindControl("ltlVatAmount");
            Literal ltlHapAmount = (Literal)e.Item.FindControl("ltlHapAmount");

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;
            double net = StCommon.ToDouble(drItemRow["Blju_JustAmount"].ToString(), 0);
            double vat = net * (0.1);
            ltlVatAmount.Text = GetAmountFormat(vat);
            ltlHapAmount.Text = GetAmountFormat(net + vat);
        }
    }

    protected void lvMain_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        this.dpMain.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindMainList();
    }

    private void BindModify(string mode, string date, string times, string mainbuyer, string sample)
    {
        this.hidAsMainbuyer.Value = mainbuyer;
        this.hidAsDate.Value = date;
        this.hidAsTimes.Value = times;
        this.hidAsSample.Value = sample;
        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" SELECT (select top 1 BnpmD_StyleNox from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_StyleNox ");
        sb.AppendLine(" , (select sum(BnpmD_Qty01) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty01 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty02) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty02 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty03) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty03 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty04) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty04 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty05) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty05 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty06) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty06 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty07) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty07 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty08) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty08 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty09) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty09 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty10) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty10 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty11) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty11 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty12) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty12 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty13) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty13 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty14) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty14 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty15) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty15 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty16) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty16 ");
        sb.AppendLine(" , (select sum(BnpmD_Qty17) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_Qty17 ");
        sb.AppendLine(" , (select sum(BnpmD_QtyTotal) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_QtyTotal ");
        sb.AppendLine(" , * FROM " + preVal + "BNPMH a ");
        sb.AppendLine(" WHERE BnpmH_Date = '" + date + "' and BnpmH_Times = '" + times + "' and BnpmH_MainBuyer = '" + mainbuyer + "' and BnpmH_Sample = '" + sample + "' ");

        DataSet dsH = stData.GetDataSet(sb.ToString());

        if (dsH.Tables[0].Rows.Count > 0)
        {
            DataRow drH = dsH.Tables[0].Rows[0];

            if (drH["BnpmH_TbucMarkTop"].ToString() == "T")
            {
                this.bljuHistory.Visible = false;
                this.cboTBuc.Checked = true;
            }
            else
            {
                this.bljuHistory.Visible = true;
                this.cboTBuc.Checked = false;
            }

            sb = new StringBuilder();
            sb.AppendLine(" select * ");
            sb.AppendLine(" from " + preVal + "BLJU a where blju_Date = '" + drH["BnpmH_Bjhd_Date"] + "' and blju_Times = '" + drH["BnpmH_Bjhd_Times"] + "' and blju_MainBuyer = '" + drH["BnpmH_Bjhd_MainBuyer"] + "' and blju_Sample = '" + drH["BnpmH_Bjhd_Sample"] + "' and blju_styleNox = '" + drH["BnpmH_StyleNox"] + "' ");
            sb.AppendLine(" order by blju_date desc, blju_times desc ");
            DataSet dsD = stData.GetDataSet(sb.ToString());

            if (dsD.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsD.Tables[0].Rows[0];

                this.Blju_Qty01.Value = dr["Blju_Qty01"].ToString();
                this.Blju_Qty02.Value = dr["Blju_Qty02"].ToString();
                this.Blju_Qty03.Value = dr["Blju_Qty03"].ToString();
                this.Blju_Qty04.Value = dr["Blju_Qty04"].ToString();
                this.Blju_Qty05.Value = dr["Blju_Qty05"].ToString();
                this.Blju_Qty06.Value = dr["Blju_Qty06"].ToString();
                this.Blju_Qty07.Value = dr["Blju_Qty07"].ToString();
                this.Blju_Qty08.Value = dr["Blju_Qty08"].ToString();
                this.Blju_Qty09.Value = dr["Blju_Qty09"].ToString();
                this.Blju_Qty10.Value = dr["Blju_Qty10"].ToString();
                this.Blju_Qty11.Value = dr["Blju_Qty11"].ToString();
                this.Blju_Qty12.Value = dr["Blju_Qty12"].ToString();
                this.Blju_Qty13.Value = dr["Blju_Qty13"].ToString();
                this.Blju_Qty14.Value = dr["Blju_Qty14"].ToString();
                this.Blju_Qty15.Value = dr["Blju_Qty15"].ToString();
                this.Blju_Qty16.Value = dr["Blju_Qty16"].ToString();
                this.Blju_Qty17.Value = dr["Blju_Qty17"].ToString();
                this.Blju_QtyTotal.Value = dr["Blju_QtyTotal"].ToString();
            }

            this.Size_Total01.Value = drH["BnpmH_Qty01"].ToString();
            this.Size_Total02.Value = drH["BnpmH_Qty02"].ToString();
            this.Size_Total03.Value = drH["BnpmH_Qty03"].ToString();
            this.Size_Total04.Value = drH["BnpmH_Qty04"].ToString();
            this.Size_Total05.Value = drH["BnpmH_Qty05"].ToString();
            this.Size_Total06.Value = drH["BnpmH_Qty06"].ToString();
            this.Size_Total07.Value = drH["BnpmH_Qty07"].ToString();
            this.Size_Total08.Value = drH["BnpmH_Qty08"].ToString();
            this.Size_Total09.Value = drH["BnpmH_Qty09"].ToString();
            this.Size_Total10.Value = drH["BnpmH_Qty10"].ToString();
            this.Size_Total11.Value = drH["BnpmH_Qty11"].ToString();
            this.Size_Total12.Value = drH["BnpmH_Qty12"].ToString();
            this.Size_Total13.Value = drH["BnpmH_Qty13"].ToString();
            this.Size_Total14.Value = drH["BnpmH_Qty14"].ToString();
            this.Size_Total15.Value = drH["BnpmH_Qty15"].ToString();
            this.Size_Total16.Value = drH["BnpmH_Qty16"].ToString();
            this.Size_Total17.Value = drH["BnpmH_Qty17"].ToString();
            this.Size_TotalSum.Value = drH["BnpmH_QtyTotal"].ToString();

            this.txtStyle.Value = drH["BnpmH_StyleNox"].ToString();

            this.txtEtc.Text = drH["BnpmH_Remark"].ToString();

            if (drH["BnpmH_Bonsa_Check"].ToString() == "0" || drH["BnpmH_Bonsa_Check"].ToString() == "1")
            {
                if (drH["BnpmH_CreateSawon"].ToString() == MemberData.GetLoginSID("LoginID")) // 로그인한 사용자가 요청한 내역만 수정/삭제 가능
                {
                }
                else
                {
                    this.btnEdit.Visible = false;
                }
            }
            else
            {
                this.btnEdit.Visible = false;
            }

            // 파일 리스트 Call
            /*
            try
            {
                lstFile.Items.Clear();
            }
            catch { }
            */
            SetFileNames(date, times, mainbuyer, sample);

            BindFiles(date, times, mainbuyer, sample);

            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("BindAsDetailList('" + mode + "','" + date + "','" + times + "','" + mainbuyer + "','" + sample + "');");
        }

        sb = new StringBuilder();

        sb.AppendLine(" select BnpmR_Line, BnpmR_LineSeqx, (case when BnpmR_Line = 1 then BnpmR_ResonName else '' end) as BnpmR_ResonName ");
        sb.AppendLine(" , (case when BnpmR_LineSeqx = 1 then BnpmR_SizeName else '' end) as BnpmR_SizeName, BnpmR_AsQty ");
        sb.AppendLine(" , BnpmR_Result01, BnpmR_Result02, BnpmR_Result03, BnpmR_Result04, BnpmR_Result05, BnpmR_ResultRemark ");
        sb.AppendLine(" , * FROM " + preVal + "BnpmR ");
        sb.AppendLine(" WHERE BnpmR_Date = '" + date + "' and BnpmR_Times = '" + times + "' and BnpmR_MainBuyer = '" + mainbuyer + "' and BnpmR_Sample = '" + sample + "' ");

        DataSet dsR = stData.GetDataSet(sb.ToString());

        this.lvResult.DataSource = dsR;
        this.lvResult.DataBind();

        #region // 합계 바인딩
        double sum1 = 0;
        double sum2 = 0;
        double sum3 = 0;
        double sum4 = 0;
        double sum5 = 0;
        try
        {
            for (int i = 0; i < dsR.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dsR.Tables[0].Rows[i];

                sum1 += StCommon.ToDouble(dr["BnpmR_Result01"].ToString(), 0);
                sum2 += StCommon.ToDouble(dr["BnpmR_Result02"].ToString(), 0);
                sum3 += StCommon.ToDouble(dr["BnpmR_Result03"].ToString(), 0);
                sum4 += StCommon.ToDouble(dr["BnpmR_Result04"].ToString(), 0);
                sum5 += StCommon.ToDouble(dr["BnpmR_Result05"].ToString(), 0);
            }

            ((Literal)this.lvResult.FindControl("ltlResult01")).Text = GetAmountFormat(sum1);
            ((Literal)this.lvResult.FindControl("ltlResult02")).Text = GetAmountFormat(sum2);
            ((Literal)this.lvResult.FindControl("ltlResult03")).Text = GetAmountFormat(sum3);
            ((Literal)this.lvResult.FindControl("ltlResult04")).Text = GetAmountFormat(sum4);
            ((Literal)this.lvResult.FindControl("ltlResult05")).Text = GetAmountFormat(sum5);
            ((Literal)this.lvResult.FindControl("ltlResultTotal")).Text = GetAmountFormat(sum1 + sum2 + sum3 + sum4 + sum5);
        }
        catch { }
        #endregion
    }

    protected void lvResult_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlResult01 = (Literal)e.Item.FindControl("ltlResult01");
            Literal ltlResult02 = (Literal)e.Item.FindControl("ltlResult02");
            Literal ltlResult03 = (Literal)e.Item.FindControl("ltlResult03");
            Literal ltlResult04 = (Literal)e.Item.FindControl("ltlResult04");
            Literal ltlResult05 = (Literal)e.Item.FindControl("ltlResult05");
            
            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;
            ltlResult01.Text = (drItemRow["BnpmR_Result01"].ToString() == "1") ? "<i class=\"fas fa-check\"></i>" : "";
            ltlResult02.Text = (drItemRow["BnpmR_Result02"].ToString() == "1") ? "<i class=\"fas fa-check\"></i>" : "";
            ltlResult03.Text = (drItemRow["BnpmR_Result03"].ToString() == "1") ? "<i class=\"fas fa-check\"></i>" : "";
            ltlResult04.Text = (drItemRow["BnpmR_Result04"].ToString() == "1") ? "<i class=\"fas fa-check\"></i>" : "";
            ltlResult05.Text = (drItemRow["BnpmR_Result05"].ToString() == "1") ? "<i class=\"fas fa-check\"></i>" : "";
        }
    }

    private void SetFileNames(string date, string times, string mainbuyer, string sample)
    {
        /*
        try
        {
            string qry = string.Empty;
            qry = " SELECT * FROM " + preVal + "BNPMX where BnpmX_Date='" + date + "' and BnpmX_Times='" + times + "' and BnpmX_MainBuyer='" + mainbuyer + "' and BnpmX_Sample='" + sample + "' ";
            DataSet ds = stData.GetDataSet(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstFile.Items.Add(ds.Tables[0].Rows[i]["bnpmX_imageFileName"].ToString().Trim());
                }
            }
        }
        catch { }
        */
    }

    private void BindFiles(string date, string times, string mainbuyer, string sample)
    {
        string qry = " SELECT bnpmX_imageFileName FROM " + preVal + "BNPMX where BnpmX_Date='" + date + "' and BnpmX_Times='" + times + "' and BnpmX_MainBuyer='" + mainbuyer + "' and BnpmX_Sample='" + sample + "' ";
        DataSet ds = stData.GetDataSet(qry);
        
        this.rptDownload.DataSource = ds;
        this.rptDownload.DataBind();
    }

    protected void rptDownload_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DownloadFile":
                string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "AsFilePath");
                StFileFolder.DownLoadFile(upDir + e.CommandArgument.ToString().Trim(), e.CommandArgument.ToString().Trim());
                break;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMainList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        char[] delimeter = { ',' };
        string[] arrIDX = Request.Form["rowNum"].Split(delimeter);
        
        string bljuDate = this.hidBljuDate.Value;
        string bljuTimes = this.hidBljuTimes.Value;
        string bljuSample = this.hidBljuSample.Value;
        string bljuStyle = this.hidBljuStyle.Value;

        string style = bljuStyle;

        string tbucmarktop = "";
        if (this.cboTBuc.Checked == true)
        {
            tbucmarktop = "T";
            style = StCommon.ReplaceSQ(this.txtProduct.Text);
        }
        else if (preVal == "tbl")
        {
            tbucmarktop = "M";
        }
        else if (preVal == "abl")
        {
            tbucmarktop = "A";
        }
        
        if (mode != "")
        {
            style = this.txtStyle.Value;
        }

        style = style.ToUpper();

        // 헤더 저장
        AsRequestData asr = new AsRequestData();

        asr.Date = this.hidAsDate.Value;
        asr.Times = this.hidAsTimes.Value;
        asr.Mainbuyer = this.hidAsMainbuyer.Value;
        asr.Sample = this.hidAsSample.Value;
        asr.tbucmarktop = tbucmarktop;
        asr.bjhd_date = bljuDate;
        asr.bjhd_times = bljuTimes;
        asr.bjhd_mainbuyer = this.hidAsMainbuyer.Value;
        asr.bjhd_sample = bljuSample;
        asr.bonsa_check = this.hidBonsaCheck.Value;
        asr.remark = StCommon.ReplaceSQ(this.txtEtc.Text);
        asr.Sdate = DateTime.Now;
        asr.Ssawon = MemberData.GetLoginSID("LoginID");
        asr.Mdate = DateTime.Now;
        asr.Msawon = MemberData.GetLoginSID("LoginID");

        if (mode == "")
        {
            asr.InsertHead();
        }
        else
        {
            asr.UpdateHead();
        }

        if (mode != "")
        {
            asr.DeleteData();
        }

        HttpFileCollection hfc = Request.Files;
        string test2 = hfc.ToString();
        string test = StCommon.ReplaceSQ(Request.Form["hidFiles"]);

        // 상세 저장
        for (int i = 0; i < arrIDX.Length; i++)
        {
            string BnpmD_BnpmCode = StCommon.ReplaceSQ(Request.Form["BnpmD_BnpmCode_" + arrIDX[i]]);
            string BnpmD_Qty01 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty01_" + arrIDX[i]]);
            string BnpmD_Qty02 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty02_" + arrIDX[i]]);
            string BnpmD_Qty03 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty03_" + arrIDX[i]]);
            string BnpmD_Qty04 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty04_" + arrIDX[i]]);
            string BnpmD_Qty05 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty05_" + arrIDX[i]]);
            string BnpmD_Qty06 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty06_" + arrIDX[i]]);
            string BnpmD_Qty07 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty07_" + arrIDX[i]]);
            string BnpmD_Qty08 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty08_" + arrIDX[i]]);
            string BnpmD_Qty09 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty09_" + arrIDX[i]]);
            string BnpmD_Qty10 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty10_" + arrIDX[i]]);
            string BnpmD_Qty11 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty11_" + arrIDX[i]]);
            string BnpmD_Qty12 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty12_" + arrIDX[i]]);
            string BnpmD_Qty13 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty13_" + arrIDX[i]]);
            string BnpmD_Qty14 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty14_" + arrIDX[i]]);
            string BnpmD_Qty15 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty15_" + arrIDX[i]]);
            string BnpmD_Qty16 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty16_" + arrIDX[i]]);
            string BnpmD_Qty17 = StCommon.ReplaceSQ(Request.Form["BnpmD_Qty17_" + arrIDX[i]]);
            string BnpmD_QtyTotal = StCommon.ReplaceSQ(Request.Form["BnpmD_QtyTotal_" + arrIDX[i]]);
            string BnpmD_UsedRemark = StCommon.ReplaceSQ(Request.Form["BnpmD_UsedRemark_" + arrIDX[i]]);
            string BnpmD_ReasonRemark = StCommon.ReplaceSQ(Request.Form["BnpmD_ReasonRemark_" + arrIDX[i]]);

            double Qty01 = StCommon.ToDouble(BnpmD_Qty01, 0);
            double Qty02 = StCommon.ToDouble(BnpmD_Qty02, 0);
            double Qty03 = StCommon.ToDouble(BnpmD_Qty03, 0);
            double Qty04 = StCommon.ToDouble(BnpmD_Qty04, 0);
            double Qty05 = StCommon.ToDouble(BnpmD_Qty05, 0);
            double Qty06 = StCommon.ToDouble(BnpmD_Qty06, 0);
            double Qty07 = StCommon.ToDouble(BnpmD_Qty07, 0);
            double Qty08 = StCommon.ToDouble(BnpmD_Qty08, 0);
            double Qty09 = StCommon.ToDouble(BnpmD_Qty09, 0);
            double Qty10 = StCommon.ToDouble(BnpmD_Qty10, 0);
            double Qty11 = StCommon.ToDouble(BnpmD_Qty11, 0);
            double Qty12 = StCommon.ToDouble(BnpmD_Qty12, 0);
            double Qty13 = StCommon.ToDouble(BnpmD_Qty13, 0);
            double Qty14 = StCommon.ToDouble(BnpmD_Qty14, 0);
            double Qty15 = StCommon.ToDouble(BnpmD_Qty15, 0);
            double Qty16 = StCommon.ToDouble(BnpmD_Qty16, 0);
            double Qty17 = StCommon.ToDouble(BnpmD_Qty17, 0);
            double QtyTotal = Qty01 + Qty02 + Qty03 + Qty04 + Qty05 + Qty06 + Qty07 + Qty08 + Qty09 + Qty10 + Qty11 + Qty12 + Qty13 + Qty14 + Qty15 + Qty16 + Qty17;

            asr = new AsRequestData();
            
            asr.Date = this.hidAsDate.Value;
            asr.Times = this.hidAsTimes.Value;
            asr.Mainbuyer = this.hidAsMainbuyer.Value;
            asr.Sample = this.hidAsSample.Value;
            asr.Stylenox = style;
            asr.Bnpmcode = BnpmD_BnpmCode;
            asr.Qty01 = Qty01;
            asr.Qty02 = Qty02;
            asr.Qty03 = Qty03;
            asr.Qty04 = Qty04;
            asr.Qty05 = Qty05;
            asr.Qty06 = Qty06;
            asr.Qty07 = Qty07;
            asr.Qty08 = Qty08;
            asr.Qty09 = Qty09;
            asr.Qty10 = Qty10;
            asr.Qty11 = Qty11;
            asr.Qty12 = Qty12;
            asr.Qty13 = Qty13;
            asr.Qty14 = Qty14;
            asr.Qty15 = Qty15;
            asr.Qty16 = Qty16;
            asr.Qty17 = Qty17;
            asr.QtyTotal = QtyTotal;
            asr.JustPrice = "0";
            asr.JustAmount = "0";
            asr.Usedremark = BnpmD_UsedRemark;
            asr.Reasonremark = BnpmD_ReasonRemark;

            asr.InsertData();
        }

        //saveFiles(this.hidAsDate.Value, this.hidAsTimes.Value, this.hidAsMainbuyer.Value, this.hidAsSample.Value);

        StJavaScript js = new StJavaScript(this.Page, false, true);

        if (this.hidBonsaCheck.Value == "1")
        {
            js.WriteJavascript("alert(\"전송되었습니다.\"); location.href='/Page/AsRequestList.aspx?hgubun=back';");
        }
        else
        {
            if (mode == "")
            {
                js.WriteJavascript("alert(\"접수되었습니다.\"); location.href='/Page/AsRequestList.aspx?hgubun=back';");
            }
            else
            {
                js.WriteJavascript("alert(\"수정되었습니다.\"); location.href='/Page/AsRequestList.aspx?hgubun=back';");
            }
        }
    }
    
    private void saveFiles(string date, string times, string mainbuyer, string sample)
    {
        /*
        string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "AsFilePath");
        
        HttpFileCollection hfc = Request.Files;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength > 0)
            {
                string extension = Path.GetExtension(hpf.FileName);
                string filename = Path.GetFileName(hpf.FileName.Replace(extension, string.Empty) + "_" + DateTime.Now.ToString("yyyymmddhhmmss") + extension);
                hpf.SaveAs(upDir + "\\" + filename);

                // DB에 파일명 저장
                AsRequestData asr = new AsRequestData();

                asr.Date = date;
                asr.Times = times;
                asr.Mainbuyer = mainbuyer;
                asr.Sample = sample;
                asr.Imagefilename = filename;
                asr.InsertFile();
            }
        }
        */
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

    protected void btnFileDownload_Click(object sender, EventArgs e)
    {
        /*
        try
        {
            string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "AsFilePath");

            if (lstFile.Items.Count > 0)
            {
                if (lstFile.SelectedIndex < 0)
                {
                    StJavaScript js = new StJavaScript(this.Page);
                    js.ShowAlertMessage("항목을 선택해주세요.");
                }
                else
                {
                    string fFullName = upDir + lstFile.SelectedValue.ToString().Trim();

                    FileInfo fInfo = new FileInfo(fFullName);

                    if (fInfo.Exists)
                    {
                        StFileFolder.DownLoadFile(upDir + lstFile.SelectedValue.ToString().Trim(), lstFile.SelectedValue.ToString().Trim());
                    }
                    else
                    {
                        StJavaScript js = new StJavaScript(this.Page);
                        js.ShowAlertMessage("파일이 존재하지 않습니다.");
                    }
                }
            }
        }
        catch { }
        */
    }
    
    protected void btnFileRemove_Click(object sender, EventArgs e)
    {
        /*
        string mainbuyer = this.hidAsMainbuyer.Value;
        string date = this.hidAsDate.Value;
        string times = this.hidAsTimes.Value;
        string sample = this.hidAsSample.Value;

        try
        {
            string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "AsFilePath");
            
            if (lstFile.Items.Count > 0)
            {
                if (lstFile.SelectedIndex < 0)
                {
                    StJavaScript js = new StJavaScript(this.Page);
                    js.ShowAlertMessage("항목을 선택해주세요.");
                }
                else
                {
                    AsRequestData asr = new AsRequestData();

                    asr.Date = date;
                    asr.Times = times;
                    asr.Mainbuyer = mainbuyer;
                    asr.Sample = sample;
                    asr.Imagefilename = lstFile.SelectedValue;
                    asr.DeleteFile();

                    StFileFolder.DeleteFile(upDir, lstFile.SelectedValue);

                    // 파일 리스트 Call
                    SetFileNames(date, times, mainbuyer, sample);
                }
            }
        }
        catch { }
        */
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string mainbuyer = this.hidAsMainbuyer.Value;
        string date = this.hidAsDate.Value;
        string times = this.hidAsTimes.Value;
        string sample = this.hidAsSample.Value;
        Response.Redirect("AsRequest.aspx?mode=edit&param_date=" + date + "&param_times=" + times + "&param_mainbuyer=" + mainbuyer + "&param_sample=" + sample + "");
    }
    
    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("AsRequestList.aspx?hgubun=back");
    }
}