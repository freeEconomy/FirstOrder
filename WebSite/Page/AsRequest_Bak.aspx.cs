using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Data;
using FirstOrder.Util;
using System.Data.Common;

public partial class Page_AsRequest_Bak : System.Web.UI.Page
{
    private SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);
    private WorkHistory whis = new WorkHistory();
    private string preVal = "";
    private StCommon st = null;
    private SqlDatabase tbuc_db = StDBConn.GetOpenDB(OpenDBType.tbucDB);
    private StDataCommon stData = new StDataCommon();
    private StDataCommon stDataTbuc = null;
    private int totCount = 0;
    private string allNoTax = "";
    
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
            this.hidNowDate.Value = DateTime.Now.ToShortDateString();
            this.hidNowTime.Value = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);

            this.valPreVal.Text = preVal;

            if (preVal == "abl")
            {
                this.divBrand.Visible = false;
            }

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
            if (preVal == "tbl")
            { }
            else
            {
                this.ltlSize11.Text = st.SizeName11;
                this.ltlSize12.Text = st.SizeName12;
                this.ltlSize13.Text = st.SizeName13;
                this.ltlSize14.Text = st.SizeName14;
                this.ltlSize15.Text = st.SizeName15;
                this.ltlSize16.Text = st.SizeName16;
                this.ltlSize17.Text = st.SizeName17;
            }

            this.txtProduct.Text = "";
        }        
    }

    private void BindMainList()
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        string styleNox = "";

        try
        {
            styleNox = this.txtProduct.Text;
        }
        catch { }
        
        string qry = " SELECT * from gblKURE where kure_code = '" + styleNox + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            allNoTax = dsK.Tables[0].Rows[0]["Kure_All_NoTax"].ToString().Trim();
        }

        StringBuilder sb = new StringBuilder();

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
                
                if (allNoTax == "Y")
                {
                    vat2 = 0;
                }

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
            Literal ltlVatAmount = (Literal)e.Item.FindControl("ltlVatAmount");
            Literal ltlHapAmount = (Literal)e.Item.FindControl("ltlHapAmount");

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;
            double net = StCommon.ToDouble(drItemRow["Blju_JustAmount"].ToString(), 0);
            double vat = net * (0.1);
            if (allNoTax == "Y")
            {
                vat = 0;
            }
            ltlVatAmount.Text = GetAmountFormat(vat);
            ltlHapAmount.Text = GetAmountFormat(net + vat);
        }
    }

    protected void lvMain_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        this.dpMain.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindMainList();
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

    protected void btnWrite_Click(object sender, EventArgs e)
    {
        char[] delimeter = { ',' };
        string[] arrIDX = Request.Form["rowNum"].Split(delimeter);

        string kurecode = MemberData.GetLoginSID("KureCode");

        string date = this.hidDate.Value;
        string times = this.hidTimes.Value;
        string sample = this.hidSample.Value;
        string style = this.hidStyle.Value;

        string tbucmarktop = "";
        if (this.cboTBuk.Checked == true)
        {
            tbucmarktop = "T";
        }
        else if (preVal == "tbl")
        {
            tbucmarktop = "M";
        }
        else if (preVal == "abl")
        {
            tbucmarktop = "A";
        }

        // 헤더 저장
        AsRequestData asr = new AsRequestData();

        asr.Date = this.hidNowDate.Value;
        asr.Times = this.hidNowTime.Value;
        asr.Mainbuyer = kurecode;
        asr.Sample = "0";
        asr.tbucmarktop = tbucmarktop;
        asr.bjhd_date = date;
        asr.bjhd_times = times;
        asr.bjhd_mainbuyer = kurecode;
        asr.bjhd_sample = sample;
        asr.bonsa_check = "0";
        asr.remark = "";
        asr.Sdate = DateTime.Now;
        asr.Ssawon = MemberData.GetLoginSID("LoginID");
        asr.Mdate = DateTime.Now;
        asr.Msawon = MemberData.GetLoginSID("LoginID");

        asr.InsertHead();
        /*
        StringBuilder sb = new StringBuilder();
        sb.Append(" INSERT INTO " + preVal + "BNPMH (BnpmH_Date,BnpmH_Times,BnpmH_MainBuyer,BnpmH_Sample,BnpmH_TbucMarkTop,BnpmH_Bjhd_Date,BnpmH_Bjhd_Times,BnpmH_Bjhd_MainBuyer,BnpmH_Bjhd_Sample ");
        sb.Append(" ,BnpmH_Bonsa_Check,BnpmH_Bonsa_Check1,BnpmH_ChongPan_Check,BnpmH_ChongPan_Check1,BnpmH_Remark,BnpmH_CreateDate,BnpmH_CreateSawon,BnpmH_ModifyDate,BnpmH_ModifySaWon) ");
        sb.Append(" VALUES(@BnpmH_Date,@BnpmH_Times,@BnpmH_MainBuyer,@BnpmH_Sample,@BnpmH_TbucMarkTop,@BnpmH_Bjhd_Date,@BnpmH_Bjhd_Times,@BnpmH_Bjhd_MainBuyer,@BnpmH_Bjhd_Sample ");
        sb.Append(" ,@BnpmH_Bonsa_Check,@BnpmH_Bonsa_Check1,@BnpmH_ChongPan_Check,@BnpmH_ChongPan_Check1,@BnpmH_Remark ");
        sb.Append(" ,@BnpmH_CreateDate,@BnpmH_CreateSawon,@BnpmH_ModifyDate,@BnpmH_ModifySaWon) ");

        DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

        db.AddInParameter(cmd, "@BnpmH_Date", DbType.String, nowdate);
        db.AddInParameter(cmd, "@BnpmH_Times", DbType.String, nowtime);
        db.AddInParameter(cmd, "@BnpmH_MainBuyer", DbType.String, kurecode);
        db.AddInParameter(cmd, "@BnpmH_Sample", DbType.String, "0");
        db.AddInParameter(cmd, "@BnpmH_TbucMarkTop", DbType.String, "");
        db.AddInParameter(cmd, "@BnpmH_Bjhd_Date", DbType.String, date);
        db.AddInParameter(cmd, "@BnpmH_Bjhd_Times", DbType.String, times);
        db.AddInParameter(cmd, "@BnpmH_Bjhd_MainBuyer", DbType.String, kurecode);
        db.AddInParameter(cmd, "@BnpmH_Bjhd_Sample", DbType.String, sample);
        db.AddInParameter(cmd, "@BnpmH_Bonsa_Check", DbType.String, "0");
        db.AddInParameter(cmd, "@BnpmH_Bonsa_Check1", DbType.String, "");
        db.AddInParameter(cmd, "@BnpmH_ChongPan_Check", DbType.String, "");
        db.AddInParameter(cmd, "@BnpmH_ChongPan_Check1", DbType.String, "");
        db.AddInParameter(cmd, "@BnpmH_Remark", DbType.String, "");
        db.AddInParameter(cmd, "@BnpmH_CreateDate", DbType.DateTime, DateTime.Now);
        db.AddInParameter(cmd, "@BnpmH_CreateSawon", DbType.String, MemberData.GetLoginSID("LoginID"));
        db.AddInParameter(cmd, "@BnpmH_ModifyDate", DbType.DateTime, DateTime.Now);
        db.AddInParameter(cmd, "@BnpmH_ModifySaWon", DbType.String, MemberData.GetLoginSID("LoginID"));

        db.ExecuteNonQuery(cmd);

        whis.InsertWork("AS접수", "헤더입력", cmd);
        */

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
            
            asr.Date = this.hidNowDate.Value;
            asr.Times = this.hidNowTime.Value;
            asr.Mainbuyer = kurecode;
            asr.Sample = "0";
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

            /*
            sb = new StringBuilder();
            
            sb.Append(" INSERT INTO " + preVal + "BNPMD (BnpmD_Date,BnpmD_Times,BnpmD_MainBuyer,BnpmD_Sample,BnpmD_StyleNox,BnpmD_StyleNoxLine,BnpmD_BnpmCode ");
            sb.Append(" ,BnpmD_Qty01,BnpmD_Qty02,BnpmD_Qty03,BnpmD_Qty04,BnpmD_Qty05,BnpmD_Qty06,BnpmD_Qty07,BnpmD_Qty08,BnpmD_Qty09,BnpmD_Qty10,BnpmD_Qty11,BnpmD_Qty12,BnpmD_Qty13,BnpmD_Qty14,BnpmD_Qty15,BnpmD_Qty16,BnpmD_Qty17,BnpmD_QtyTotal ");
            sb.Append(" ,BnpmD_JustPrice,BnpmD_JustAmount,BnpmD_UsedRemark,BnpmD_ReasonRemark) ");
            sb.Append(" VALUES(@BnpmD_Date,@BnpmD_Times,@BnpmD_MainBuyer,@BnpmD_Sample,@BnpmD_StyleNox,@BnpmD_StyleNoxLine,@BnpmD_BnpmCode ");
            sb.Append(" ,@BnpmD_Qty01,@BnpmD_Qty02,@BnpmD_Qty03,@BnpmD_Qty04,@BnpmD_Qty05,@BnpmD_Qty06,@BnpmD_Qty07,@BnpmD_Qty08,@BnpmD_Qty09,@BnpmD_Qty10,@BnpmD_Qty11,@BnpmD_Qty12,@BnpmD_Qty13,@BnpmD_Qty14,@BnpmD_Qty15,@BnpmD_Qty16,@BnpmD_Qty17,@BnpmD_QtyTotal ");
            sb.Append(" ,@BnpmD_JustPrice,@BnpmD_JustAmount,@BnpmD_UsedRemark,@BnpmD_ReasonRemark) ");

            cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmD_Date", DbType.String, nowdate);
            db.AddInParameter(cmd, "@BnpmD_Times", DbType.String, nowtime);
            db.AddInParameter(cmd, "@BnpmD_MainBuyer", DbType.String, kurecode);
            db.AddInParameter(cmd, "@BnpmD_Sample", DbType.String, "0");
            db.AddInParameter(cmd, "@BnpmD_StyleNox", DbType.String, style);
            db.AddInParameter(cmd, "@BnpmD_StyleNoxLine", DbType.Int32, 0);
            db.AddInParameter(cmd, "@BnpmD_BnpmCode", DbType.String, BnpmD_BnpmCode);
            db.AddInParameter(cmd, "@BnpmD_Qty01", DbType.Int32, Qty01);
            db.AddInParameter(cmd, "@BnpmD_Qty02", DbType.Int32, Qty02);
            db.AddInParameter(cmd, "@BnpmD_Qty03", DbType.Int32, Qty03);
            db.AddInParameter(cmd, "@BnpmD_Qty04", DbType.Int32, Qty04);
            db.AddInParameter(cmd, "@BnpmD_Qty05", DbType.Int32, Qty05);
            db.AddInParameter(cmd, "@BnpmD_Qty06", DbType.Int32, Qty06);
            db.AddInParameter(cmd, "@BnpmD_Qty07", DbType.Int32, Qty07);
            db.AddInParameter(cmd, "@BnpmD_Qty08", DbType.Int32, Qty08);
            db.AddInParameter(cmd, "@BnpmD_Qty09", DbType.Int32, Qty09);
            db.AddInParameter(cmd, "@BnpmD_Qty10", DbType.Int32, Qty10);
            db.AddInParameter(cmd, "@BnpmD_Qty11", DbType.Int32, Qty11);
            db.AddInParameter(cmd, "@BnpmD_Qty12", DbType.Int32, Qty12);
            db.AddInParameter(cmd, "@BnpmD_Qty13", DbType.Int32, Qty13);
            db.AddInParameter(cmd, "@BnpmD_Qty14", DbType.Int32, Qty14);
            db.AddInParameter(cmd, "@BnpmD_Qty15", DbType.Int32, Qty15);
            db.AddInParameter(cmd, "@BnpmD_Qty16", DbType.Int32, Qty16);
            db.AddInParameter(cmd, "@BnpmD_Qty17", DbType.Int32, Qty17);
            db.AddInParameter(cmd, "@BnpmD_QtyTotal", DbType.Int32, QtyTotal);
            db.AddInParameter(cmd, "@BnpmD_JustPrice", DbType.Double, 0);
            db.AddInParameter(cmd, "@BnpmD_JustAmount", DbType.Double, 0);
            db.AddInParameter(cmd, "@BnpmD_UsedRemark", DbType.String, BnpmD_UsedRemark);
            db.AddInParameter(cmd, "@BnpmD_ReasonRemark", DbType.String, BnpmD_ReasonRemark);

            whis.InsertWork("AS접수", "상세입력", cmd);

            db.ExecuteNonQuery(cmd);
            */
        }

        saveFiles(this.hidNowDate.Value, this.hidNowTime.Value, kurecode, "0");

        StJavaScript js = new StJavaScript(this.Page, false, true);
        js.WriteJavascript("alert(\"접수되었습니다.\");");
    }
    
    private void saveFiles(string date, string times, string mainbuyer, string sample)
    {
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
    }
}