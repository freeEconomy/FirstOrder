using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Page_Order_tbl : System.Web.UI.Page
{
    private SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);
    private StDataCommon stData = new StDataCommon();
    private StCommon st = new StCommon("tbl");
    private WorkHistory whis = new WorkHistory();
    private string mode = "";
    private string hgubun = "";
    private int totCount = 0;

    private string param_date = "";
    private string param_times = "";
    private string param_mainbuyer = "";
    private string param_sample = "";

    private string stopMsg = "";

    private string preVal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.isSizeNum.Text = MemberData.GetLoginSID("tblSizeNum");
        this.hidBljuMin.Value = StCommon.ToDouble(MemberData.GetLoginSID("tblBljuMin"), 0).ToString();

        preVal = "tbl";

        try
        {
            mode = StCommon.ReplaceSQ(Request["mode"]);
        }
        catch { }

        try
        {
            hgubun = StCommon.ReplaceSQ(Request["hgubun"]);
        }
        catch { }

        try
        {
            param_date = StCommon.ReplaceSQ(Request["param_date"]);
            param_times = StCommon.ReplaceSQ(Request["param_times"]);
            param_mainbuyer = StCommon.ReplaceSQ(Request["param_mainbuyer"]);
            param_sample = StCommon.ReplaceSQ(Request["param_sample"]);
        }
        catch { }

        string stopMsg = OrderData_common.GetStopCheck(preVal);

        if (!IsPostBack)
        {
            this.hidKind.Value = "1";

            this.styleDiv.Visible = false;

            // 버튼 비활성화
            EnableButton(false);

            this.lnkKureSpecPrintV.Visible = false;

            if (mode == "add")
            {
                if (stopMsg != "")
                {
                    StJavaScript js = new StJavaScript(this.Page, false, true);
                    js.WriteJavascript("alert(\"" + stopMsg + "\"); history.back(-1);");
                }

                this.mvMain.ActiveViewIndex = 1;
            }
            else if (mode == "view")
            {
                this.mvMain.ActiveViewIndex = 2;
                this.lnkList2.Visible = false;
                this.lnkList3.Visible = true;
                this.lnkList4.Visible = false;
                this.lnkKureSpecPrintV.Visible = false;

                BindView(param_date, param_times, param_mainbuyer, param_sample);
                BindViewList();
            }
            else if (mode == "view2")
            {
                this.mvMain.ActiveViewIndex = 2;
                this.lnkList2.Visible = false;
                this.lnkList3.Visible = false;
                this.lnkList4.Visible = true;
                this.lnkKureSpecPrintV.Visible = true;

                BindView(param_date, param_times, param_mainbuyer, param_sample);
                BindViewList();
            }
            else
            {
                this.mvMain.ActiveViewIndex = 0;
                this.lnkList2.Visible = true;
                this.lnkList3.Visible = false;
                this.lnkList4.Visible = false;

                if (hgubun == "back")
                {
                    try
                    {
                        ((TextBox)this.ucBljuday.FindControl("txtDate")).Text = Session["bljudayS"].ToString();
                        ((TextBox)this.ucBljuday1.FindControl("txtDate")).Text = Session["bljudayE"].ToString();
                    }
                    catch { }
                }
                else
                {
                    if (param_date != "")
                    {
                        ((TextBox)this.ucBljuday.FindControl("txtDate")).Text = param_date;
                    }
                    else
                    {
                        ((TextBox)this.ucBljuday.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
                    }

                    ((TextBox)this.ucBljuday1.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
                }
                
                BindList();
            }

            this.btnModify.Visible = false;

            this.ucTrader.BindData();
            //this.ucProduct.BindData();

            string kurename = MemberData.GetLoginSID("KureName");
            string kurecode = MemberData.GetLoginSID("KureCode");

            string bljudate = DateTime.Now.ToShortDateString();
            string postbljudate = DateTime.Now.AddDays(-1).ToShortDateString();

            if (mode == "add")
            {
                if (param_date == "")
                {
                    // 어제 날짜로 발주의뢰중인 발주내역 체크
                    string qry = " select * from tblBJHD where Bjhd_Bonsa_Check in('0') and Bjhd_Date = '" + postbljudate + "' and Bjhd_MainBuyer = '" + kurecode + "' and Bjhd_CreateSawon = '" + MemberData.GetLoginSID("LoginID") + "' order by Bjhd_Times desc ";
                    DataSet ds = stData.GetDataSet(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myconfirm", "OpenConfirmPage();", true);
                    }
                }
                else
                {
                    bljudate = param_date;
                }
            }

            BindCheck(bljudate, "", kurecode);
            BindData(kurename, kurecode);
            BindDataList();

            string code = "";
            this.imgProduct.ImageUrl = "~/Handler/DisplayImage.ashx?code=" + code;
            
            string blju_date = StCommon.ReplaceSQ(this.txtDate.Text);
            string blju_times = StCommon.ReplaceSQ(this.txtTime.Text);
            string blju_mainbuyer = this.hidKureCode.Value;
            string blju_sample = "0";

            this.lnkChating.OnClientClick = "return OpenChatingCheck('" + this.hidTotal.ClientID + "','" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "', '');";
            this.btnBaeSong.OnClientClick = "return OpenBaeSong('" + this.hidTotal.ClientID + "','" + this.txtBaeSongOpt.ClientID + "','" + this.hidBaeSong.ClientID + "','" + this.txtBaeSongName.ClientID + "','" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "');";
        }

        //this.ucProduct.btnHandler += new Control_Product.OnButtonClick(fncProduct);
    }

    #region 발주의뢰 등록화면
    private void BindCheck(string bljudate, string bljutime, string kurecode)
    {
        // 당일 거래처의 발주내역이 있으면 가져올것(단, 본사처리가 안된 상태인 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0'인 것만)
        string whereQry = "";
        whereQry = StCommon.MakeSearchQry("Bjhd_Times", bljutime, "S", whereQry);

        string qry = " select * from tblBJHD where Bjhd_Bonsa_Check in('0') and Bjhd_Date = '" + bljudate + "' and Bjhd_MainBuyer = '" + kurecode + "' " + whereQry + " and Bjhd_CreateSawon = '" + MemberData.GetLoginSID("LoginID") + "' order by Bjhd_Times desc ";
        
        DataSet ds = stData.GetDataSet(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Bjhd_Bonsa_Check"].ToString() == "0")
            {
                this.lnkComplete.Visible = true;
                
                qry = " select * from tblBLJU where Blju_Date = '" + bljudate + "' and Blju_Times = '" + bljutime + "' and Blju_MainBuyer = '" + kurecode + "' ";
                DataSet dsC = stData.GetDataSet(qry);

                if (dsC.Tables[0].Rows.Count > 0)
                {
                    this.lnkComplete.OnClientClick = "return CheckComplete();";
                }
            }
            else
            {
                this.lnkComplete.Visible = false;
            }

            this.txtDate.Text = ds.Tables[0].Rows[0]["Bjhd_Date"].ToString();
            this.txtTime.Text = ds.Tables[0].Rows[0]["Bjhd_Times"].ToString();

            string baesong = ds.Tables[0].Rows[0]["Bjhd_BaeSong"].ToString();
            string baesongName = ds.Tables[0].Rows[0]["Bjhd_BaeSongName"].ToString();

            if (baesongName == "")
            {
                baesong = "";
            }

            if (baesong == "0")
            {
                this.txtBaeSongOpt.Text = "화물 발송";
            }
            else if (baesong == "1")
            {
                this.txtBaeSongOpt.Text = "로젠 택배";
            }
            else if (baesong == "2")
            {
                this.txtBaeSongOpt.Text = "화물 택배";
            }
            else if (baesong == "3")
            {
                this.txtBaeSongOpt.Text = "기타 발송";
            }
            this.hidBaeSong.Value = baesong;
            this.txtBaeSongName.Text = ds.Tables[0].Rows[0]["Bjhd_BaeSongName"].ToString();

            this.txtEtc.Text = ds.Tables[0].Rows[0]["Bjhd_Remark"].ToString();
            this.txtNetAmount.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_NetAmount"]);
            this.txtVatAmount.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_VatAmount"]);
            this.txtHapAmount.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_HapAmount"]);
            this.txtFirstDate.Text = ds.Tables[0].Rows[0]["Bjhd_CreateDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_CreateSawon"].ToString();
            this.txtLastDate.Text = ds.Tables[0].Rows[0]["Bjhd_ModifyDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_ModifySaWon"].ToString();
        }
        else
        {
            this.txtDate.Text = DateTime.Now.ToShortDateString();
            this.txtTime.Text = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);
        }
    }

    private void BindData(string kurename, string kurecode)
    {
        this.txtKure.Text = kurename;
        this.hidKureCode.Value = kurecode;

        st.Kind = this.hidKind.Value;
        st.GetSizeInfo("tbl");

        this.ltlSize1.Text = st.SizeName1 + "<br>(" + st.SizeNum1 + ")";
        this.ltlSize2.Text = st.SizeName2 + "<br>(" + st.SizeNum2 + ")";
        this.ltlSize3.Text = st.SizeName3 + "<br>(" + st.SizeNum3 + ")";
        this.ltlSize4.Text = st.SizeName4 + "<br>(" + st.SizeNum4 + ")";
        this.ltlSize5.Text = st.SizeName5 + "<br>(" + st.SizeNum5 + ")";
        this.ltlSize6.Text = st.SizeName6 + "<br>(" + st.SizeNum6 + ")";
        this.ltlSize7.Text = st.SizeName7 + "<br>(" + st.SizeNum7 + ")";
        this.ltlSize8.Text = st.SizeName8 + "<br>(" + st.SizeNum8 + ")";
        this.ltlSize9.Text = st.SizeName9 + "<br>(" + st.SizeNum9 + ")";
        this.ltlSize10.Text = st.SizeName10 + "<br>(" + st.SizeNum10 + ")";
        this.ltlSize11.Text = st.SizeName11 + "<br>(" + st.SizeNum11 + ")";
        this.ltlSize12.Text = st.SizeName12 + "<br>(" + st.SizeNum12 + ")";
        this.ltlSizeTotal.Text = "TOTAL";

        this.txtOrder1.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder2.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder3.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder4.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder5.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder6.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder7.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder8.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder9.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder10.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder11.Attributes.Add("onkeydown", "return fnNumberOnly(event);");
        this.txtOrder12.Attributes.Add("onkeydown", "return fnNumberOnly(event);");

        this.txtOrder1.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder2.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder3.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder4.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder5.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder6.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder7.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder8.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder9.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder10.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder11.Attributes.Add("onkeypress", "return CheckProduct(event);");
        this.txtOrder12.Attributes.Add("onkeypress", "return CheckProduct(event);");
    }

    private void BindDataList()
    {
        string date = txtDate.Text;
        string time = txtTime.Text;

        string whereQry = " where Blju_MainBuyer = '" + MemberData.GetLoginSID("KureCode") + "' ";
        whereQry = StCommon.MakeSearchQry("Blju_Date", date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Blju_Times", time, "S", whereQry);

        string qry = " SELECT isnull((select isnull(Dnga_SubName,'') + ', ' + isnull(Dnga_SpecColor,'') from tblDNGA where Dnga_StyleNox=a.Blju_StyleNox),'') as Blju_StyleName,* FROM tblBLJU a " + whereQry + " ORDER BY Blju_Line desc ";
        DataSet ds = stData.GetDataSet(qry);

        this.hidTotal.Value = ds.Tables[0].Rows.Count.ToString();

        if (StCommon.ToInt(this.hidTotal.Value, 0) == 0)
        {
            this.btnBaeSong.Enabled = false;
            this.lnkChating.Visible = false;
            this.lnkComplete.Visible = false;
        }
        else
        {
            this.btnBaeSong.Enabled = true;
            this.lnkChating.Visible = true;
            this.lnkComplete.Visible = true;
        }

        this.lvMain.DataSource = ds;
        this.lvMain.DataBind();

        #region // 합계 바인딩
        qry = " SELECT isnull(sum(blju_qty01),0) as q1,isnull(sum(blju_qty02),0) as q2,isnull(sum(blju_qty03),0) as q3,isnull(sum(blju_qty04),0) as q4,isnull(sum(blju_qty05),0) as q5,isnull(sum(blju_qty06),0) as q6 ";
        qry += " ,isnull(sum(blju_qty07),0) as q7,isnull(sum(blju_qty08),0) as q8,isnull(sum(blju_qty09),0) as q9,isnull(sum(blju_qty10),0) as q10,isnull(sum(blju_qty11),0) as q11,isnull(sum(blju_qty12),0) as q12 ";
        qry += " FROM tblBLJU a " + whereQry + " ";
        DataSet dsSum = stData.GetDataSet(qry);

        DataRow dr = dsSum.Tables[0].Rows[0];

        double sum1 = StCommon.ToDouble(dr["q1"].ToString(), 0);
        double sum2 = StCommon.ToDouble(dr["q2"].ToString(), 0);
        double sum3 = StCommon.ToDouble(dr["q3"].ToString(), 0);
        double sum4 = StCommon.ToDouble(dr["q4"].ToString(), 0);
        double sum5 = StCommon.ToDouble(dr["q5"].ToString(), 0);
        double sum6 = StCommon.ToDouble(dr["q6"].ToString(), 0);
        double sum7 = StCommon.ToDouble(dr["q7"].ToString(), 0);
        double sum8 = StCommon.ToDouble(dr["q8"].ToString(), 0);
        double sum9 = StCommon.ToDouble(dr["q9"].ToString(), 0);
        double sum10 = StCommon.ToDouble(dr["q10"].ToString(), 0);
        double sum11 = StCommon.ToDouble(dr["q11"].ToString(), 0);
        double sum12 = StCommon.ToDouble(dr["q12"].ToString(), 0);
        try
        {
            int k = 1;
            while (k <= 12)
            {
                ((Literal)this.lvMain.FindControl("ltlSum" + k)).Text = GetAmountFormat(dr["q" + k].ToString());
            
                k++;
            }
        }
        catch { }

        try
        {
            ((Literal)this.lvMain.FindControl("ltlSumTotal")).Text = GetAmountFormat(sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9 + sum10 + sum11 + sum12);
            this.hidBljuTotal.Value = StCommon.ToDouble(((Literal)this.lvMain.FindControl("ltlSumTotal")).Text.ToString(), 0).ToString();
        }
        catch { }

        double JustAmountTotal = 0;
        for (int i = 0; i < lvMain.Items.Count; i++)
        {
            Literal ltlJustAmount = (Literal)lvMain.Items[i].FindControl("ltlJustAmount");

            JustAmountTotal += StCommon.ToDouble(ltlJustAmount.Text.Replace(",", ""), 0);
        }

        try
        {
            ((Literal)this.lvMain.FindControl("ltlSumMoney")).Text = GetAmountFormat(JustAmountTotal);
        }
        catch { }
        #endregion
    }

    protected void lvMain_LayoutCreated(object sender, EventArgs e)
    {
        st.Kind = "1";// this.hidKind.Value;
        st.GetSizeInfo("tbl");

        ((Literal)this.lvMain.FindControl("ltlSize1")).Text = st.SizeName1 + " <br>(" + st.SizeNum1 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize2")).Text = st.SizeName2 + " <br>(" + st.SizeNum2 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize3")).Text = st.SizeName3 + " <br>(" + st.SizeNum3 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize4")).Text = st.SizeName4 + " <br>(" + st.SizeNum4 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize5")).Text = st.SizeName5 + " <br>(" + st.SizeNum5 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize6")).Text = st.SizeName6 + " <br>(" + st.SizeNum6 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize7")).Text = st.SizeName7 + " <br>(" + st.SizeNum7 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize8")).Text = st.SizeName8 + " <br>(" + st.SizeNum8 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize9")).Text = st.SizeName9 + " <br>(" + st.SizeNum9 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize10")).Text = st.SizeName10 + " <br>(" + st.SizeNum10 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize11")).Text = st.SizeName11 + " <br>(" + st.SizeNum11 + ")";
        ((Literal)this.lvMain.FindControl("ltlSize12")).Text = st.SizeName12 + " <br>(" + st.SizeNum12 + ")";
    }

    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            LinkButton lnkSubModify = (LinkButton)e.Item.FindControl("lnkSubModify");
            LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            lnkSubModify.CommandArgument = drItemRow["blju_date"].ToString() + "|" + drItemRow["blju_times"].ToString() + "|" + drItemRow["blju_mainbuyer"].ToString() + "|" + drItemRow["blju_sample"].ToString() + "|" + drItemRow["blju_line"].ToString();
            lnkSubDelete.CommandArgument = drItemRow["blju_date"].ToString() + "|" + drItemRow["blju_times"].ToString() + "|" + drItemRow["blju_mainbuyer"].ToString() + "|" + drItemRow["blju_sample"].ToString() + "|" + drItemRow["blju_line"].ToString();

            // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
            string qry = " SELECT * FROM tblBJHD where Bjhd_Bonsa_Check in('0','1') and bjhd_date = '" + drItemRow["blju_date"].ToString() + "' and bjhd_times = '" + drItemRow["blju_times"].ToString() + "' and bjhd_mainbuyer = '" + drItemRow["blju_mainbuyer"].ToString() + "' and bjhd_sample = '" + drItemRow["blju_sample"].ToString() + "' ";
            DataSet dsC = stData.GetDataSet(qry);

            if (dsC.Tables[0].Rows.Count == 0)
            {
                lnkSubModify.Visible = false;
                lnkSubDelete.Visible = false;
            }
        }
    }

    protected void lvMain_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string strList = e.CommandArgument.ToString();

        char[] delimiter = "|".ToCharArray();
        string[] strArray = strList.Trim().Split(delimiter);

        string blju_date = strArray[0];
        string blju_times = strArray[1];
        string blju_mainbuyer = strArray[2];
        string blju_sample = strArray[3];
        string blju_line = strArray[4];

        // 현재 발주상태 체크
        string nowBonsaCheck = StCommon.GetBonsaCheck("tbl", blju_mainbuyer, blju_date, blju_times);
        if (nowBonsaCheck != "" && nowBonsaCheck != "0")
        {
            string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);
            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("showMessageToolTip('" + this.lnkComplete.ClientID + "', '현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.');");
        }
        else
        {
            switch (e.CommandName)
            {
                case "subModify":

                    this.btnWrite.Visible = false;
                    this.btnModify.Visible = true;

                    BindDataModify(blju_date, blju_times, blju_mainbuyer, blju_sample, blju_line);

                    break;

                case "subDelete":

                    string qry = " DELETE FROM tblBLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' and blju_line = '" + blju_line + "' ";
                    stData.GetExecuteNonQry(qry);

                    whis.InsertWork("발주", "삭제", qry);

                    qry = " select * FROM tblBLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' ";
                    DataSet ds = stData.GetDataSet(qry);

                    if (ds.Tables[0].Rows.Count == 0) // 발주내역이 하나도 없으면 헤더도 삭제
                    {
                        qry = " DELETE FROM tblBJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주헤더", "삭제", qry);

                        qry = " DELETE FROM tblBLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주", "삭제", qry);

                        qry = " DELETE FROM tblBJHD0 where bjhd0_date = '" + blju_date + "' and bjhd0_times = '" + blju_times + "' and bjhd0_mainbuyer = '" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주배송지", "삭제", qry);

                        qry = " DELETE FROM tblMESG where Mesg_Date = '" + blju_date + "' and Mesg_Times = '" + blju_times + "' and Mesg_MainBuyer = '" + blju_mainbuyer + "' and Mesg_Sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주대화방", "삭제", qry);

                        if (mode == "add")
                        {
                            Response.Redirect("Order_tbl.aspx?mode=add");
                        }
                        else
                        {
                            Response.Redirect("Order_tbl.aspx");
                        }
                    }
                    else
                    {
                        SetHeadAmount(blju_date, blju_times, blju_mainbuyer, blju_sample);
                        WriteDefault();
                        BindDataList();
                    }

                    break;

                default:
                    break;
            }
        }
    }

    private void BindDataModify(string blju_date, string blju_times, string blju_mainbuyer, string blju_sample, string blju_line)
    {
        string qry = "";

        qry = " SELECT (select kure_sangho from gblKURE where kure_code = a.Bjhd_MainBuyer) as Bjhd_MainBuyerNm,* FROM tblBJHD a where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample = '" + blju_sample + "' ";
        DataSet dsH = stData.GetDataSet(qry);

        if (dsH.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsH.Tables[0].Rows[0];

            this.txtDate.Text = dr["Bjhd_Date"].ToString();
            this.txtTime.Text = dr["Bjhd_Times"].ToString();
            this.txtKure.Text = dr["Bjhd_MainBuyerNm"].ToString();
            this.hidKureCode.Value = dr["Bjhd_MainBuyer"].ToString();

            string baesong = dr["Bjhd_BaeSong"].ToString();
            string baesongName = dr["Bjhd_BaeSongName"].ToString();

            if (baesongName == "")
            {
                baesong = "";
            }

            if (baesong == "0")
            {
                this.txtBaeSongOpt.Text = "화물 발송";
            }
            else if (baesong == "1")
            {
                this.txtBaeSongOpt.Text = "로젠 택배";
            }
            else if (baesong == "2")
            {
                this.txtBaeSongOpt.Text = "화물 택배";
            }
            else if (baesong == "3")
            {
                this.txtBaeSongOpt.Text = "기타 발송";
            }
            this.hidBaeSong.Value = baesong;
            this.txtBaeSongName.Text = dr["Bjhd_BaeSongName"].ToString();

            this.txtEtc.Text = dr["Bjhd_Remark"].ToString();
            this.txtNetAmount.Text = GetAmountFormat(dr["Bjhd_NetAmount"]);
            this.txtVatAmount.Text = GetAmountFormat(dr["Bjhd_VatAmount"]);
            this.txtHapAmount.Text = GetAmountFormat(dr["Bjhd_HapAmount"]);
            this.txtFirstDate.Text = dr["Bjhd_CreateDate"].ToString() + " " + dr["Bjhd_CreateSawon"].ToString();
            this.txtLastDate.Text = dr["Bjhd_ModifyDate"].ToString() + " " + dr["Bjhd_ModifySaWon"].ToString();
        }

        qry = " SELECT isnull((select isnull(Dnga_MainName,'') + ', ' + isnull(Dnga_SubName,'') + ', ' + isnull(Dnga_SpecColor,'') from tblDNGA where Dnga_StyleNox=a.Blju_StyleNox),'') as Blju_StyleName,* FROM tblBLJU a where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' and blju_line = '" + blju_line + "' ";
        DataSet dsD = stData.GetDataSet(qry);

        string blju_style = "";
        if (dsD.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsD.Tables[0].Rows[0];

            blju_style = dr["Blju_StyleNox"].ToString();

            this.txtProduct.Text = dr["Blju_StyleNox"].ToString();
            this.txtProductDetail.Text = dr["Blju_StyleName"].ToString();

            string IsDuple = "";
            double BoxQty = 0;

            qry = " select * from tblBLJU where Blju_Date = '" + dr["blju_date"].ToString() + "' and Blju_Times = '" + dr["blju_times"].ToString() + "' and Blju_MainBuyer = '" + dr["blju_mainbuyer"].ToString() + "' and Blju_StyleNox = '" + dr["Blju_StyleNox"].ToString() + "' ";
            DataSet dsC = stData.GetDataSet(qry);

            if (dsC.Tables[0].Rows.Count > 0)
            {
                IsDuple = dsC.Tables[0].Rows[0]["Blju_Line"].ToString();
            }

            qry = " select isnull(Dnga_BoxQty,0) as Dnga_BoxQty,isnull(Dnga_JustPrice,0) as Dnga_JustPrice,isnull(Dnga_LowPrice,0) as Dnga_LowPrice from tblDNGA where Dnga_StyleNox = '" + dr["Blju_StyleNox"].ToString() + "' ";
            DataSet ds = stData.GetDataSet(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                BoxQty = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_BoxQty"]);
            }

            if (BoxQty == 0)
            {
                this.txtMsg.Text = "BOX적용 수량이 없습니다.";
            }
            else
            {
                // ((박수수량 * j) - (박수수량 * l) / (박수수량 * j)) * 100
                double justPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_JustPrice"]);
                double lowPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_LowPrice"]);

                //double dcPercent = Math.Round((((BoxQty * justPrice) - (BoxQty * lowPrice)) / (BoxQty * justPrice)) * 100, 1);
                
                // (j - l / l) * 100
                double dcPercent = Math.Round(((justPrice - lowPrice) / lowPrice) * 100, 1);

                this.txtMsg.Text = "BOX수량: " + BoxQty + " PCS (" + dcPercent + " % 할인됩니다.)";
            }
            
            this.txtOrder1.Text = GetAmountFormat(dr["Blju_Qty01"]);
            this.txtOrder2.Text = GetAmountFormat(dr["Blju_Qty02"]);
            this.txtOrder3.Text = GetAmountFormat(dr["Blju_Qty03"]);
            this.txtOrder4.Text = GetAmountFormat(dr["Blju_Qty04"]);
            this.txtOrder5.Text = GetAmountFormat(dr["Blju_Qty05"]);
            this.txtOrder6.Text = GetAmountFormat(dr["Blju_Qty06"]);
            this.txtOrder7.Text = GetAmountFormat(dr["Blju_Qty07"]);
            this.txtOrder8.Text = GetAmountFormat(dr["Blju_Qty08"]);
            this.txtOrder9.Text = GetAmountFormat(dr["Blju_Qty09"]);
            this.txtOrder10.Text = GetAmountFormat(dr["Blju_Qty10"]);
            this.txtOrder11.Text = GetAmountFormat(dr["Blju_Qty11"]);
            this.txtOrder12.Text = GetAmountFormat(dr["Blju_Qty12"]);
            this.txtOrderTotal.Text = GetAmountFormat(dr["Blju_QtyTotal"]);

            this.hidLine.Value = dr["Blju_Line"].ToString();

            this.imgProduct.ImageUrl = "~/Handler/DisplayImage.ashx?code=" + blju_style;

            // 이미지 확대 오픈
            qry = "SELECT * FROM tblJEPG WHERE Jepg_StyleNox='" + blju_style + "'";
            ds = stData.GetDataSet(qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Jepg_Image"].ToString()))
                    {
                        byte[] empPic = (byte[])ds.Tables[0].Rows[0]["Jepg_Image"];

                        MemoryStream ms = new MemoryStream(empPic);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                        imgProduct.Attributes.Add("style", "cursor:pointer");
                        imgProduct.Attributes.Add("onclick", "zoomViewImage('style','" + blju_style + "','" + image.Width + "','" + image.Height + "')");
                    }
                }
                catch { }
            }
            //

            string date = StCommon.ReplaceSQ(this.txtDate.Text);
            string time = StCommon.ReplaceSQ(this.txtTime.Text);
            string kure = this.hidKureCode.Value;
            string line = this.hidLine.Value;

            JegoSet(blju_style, "mod", date, time, kure, line);

            // 버튼 활성화
            EnableButton(true);

            JegoEnable();
        }
    }

    private void JegoSet(string blju_style, string mode, string date, string time, string kure, string line)
    {
        string[] JegoSet = StCommon.JegoSet(blju_style, mode, date, time, kure, line);

        this.txtJego1.Text = JegoSet[1];
        this.txtJego2.Text = JegoSet[2];
        this.txtJego3.Text = JegoSet[3];
        this.txtJego4.Text = JegoSet[4];
        this.txtJego5.Text = JegoSet[5];
        this.txtJego6.Text = JegoSet[6];
        this.txtJego7.Text = JegoSet[7];
        this.txtJego8.Text = JegoSet[8];
        this.txtJego9.Text = JegoSet[9];
        this.txtJego10.Text = JegoSet[10];
        this.txtJego11.Text = JegoSet[11];
        this.txtJego12.Text = JegoSet[12];
        this.txtJegoTotal.Text = JegoSet[0];
    }

    private string JegoOverCheck(string mode)
    {
        string result = "";

        string date = StCommon.ReplaceSQ(this.txtDate.Text);
        string time = StCommon.ReplaceSQ(this.txtTime.Text);
        string kure = this.hidKureCode.Value;
        string line = this.hidLine.Value;

        string product = StCommon.ReplaceSQ(this.txtProduct.Text);

        string qry = " select * from View_tblJEGO_Summary where Jego_StyleNox = '" + product + "' ";
        DataSet dsJ = stData.GetDataSet(qry);

        double jego1 = 0;
        double jego2 = 0;
        double jego3 = 0;
        double jego4 = 0;
        double jego5 = 0;
        double jego6 = 0;
        double jego7 = 0;
        double jego8 = 0;
        double jego9 = 0;
        double jego10 = 0;
        double jego11 = 0;
        double jego12 = 0;
        double jegoTotal = 0;

        if (dsJ.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsJ.Tables[0].Rows[0];

            jego1 = StCommon.ToDouble(dr["Jego_Qty01"].ToString(), 0);
            jego2 = StCommon.ToDouble(dr["Jego_Qty02"].ToString(), 0);
            jego3 = StCommon.ToDouble(dr["Jego_Qty03"].ToString(), 0);
            jego4 = StCommon.ToDouble(dr["Jego_Qty04"].ToString(), 0);
            jego5 = StCommon.ToDouble(dr["Jego_Qty05"].ToString(), 0);
            jego6 = StCommon.ToDouble(dr["Jego_Qty06"].ToString(), 0);
            jego7 = StCommon.ToDouble(dr["Jego_Qty07"].ToString(), 0);
            jego8 = StCommon.ToDouble(dr["Jego_Qty08"].ToString(), 0);
            jego9 = StCommon.ToDouble(dr["Jego_Qty09"].ToString(), 0);
            jego10 = StCommon.ToDouble(dr["Jego_Qty10"].ToString(), 0);
            jego11 = StCommon.ToDouble(dr["Jego_Qty11"].ToString(), 0);
            jego12 = StCommon.ToDouble(dr["Jego_Qty12"].ToString(), 0);
            jegoTotal = StCommon.ToDouble(dr["Jego_QtyTotal"].ToString(), 0);
        }

        // 재고 (총량 - 주문 누적량)
        if (mode == "mod") // 수정일 경우에는 재고 <= (재고 + 현재 데이터 주문량)
        {
            qry = " select ";
            qry += " isnull(sum(blju_qty01),0) as blju_qty01,isnull(sum(blju_qty02),0) as blju_qty02,isnull(sum(blju_qty03),0) as blju_qty03,isnull(sum(blju_qty04),0) as blju_qty04 ";
            qry += " ,isnull(sum(blju_qty05),0) as blju_qty05,isnull(sum(blju_qty06),0) as blju_qty06,isnull(sum(blju_qty07),0) as blju_qty07,isnull(sum(blju_qty08),0) as blju_qty08 ";
            qry += " ,isnull(sum(blju_qty09),0) as blju_qty09,isnull(sum(blju_qty10),0) as blju_qty10,isnull(sum(blju_qty11),0) as blju_qty11,isnull(sum(blju_qty12),0) as blju_qty12 ";
            qry += " FROM tblBLJU a where blju_stylenox = '" + product + "' and blju_date = '" + date + "' and blju_times = '" + time + "' and blju_mainbuyer = '" + kure + "' and blju_line = '" + line + "' ";
            DataSet ds = stData.GetDataSet(qry);
            DataRow dr = ds.Tables[0].Rows[0];

            jego1 = jego1 + StCommon.ToDouble(dr["blju_qty01"].ToString(), 0);
            jego2 = jego2 + StCommon.ToDouble(dr["blju_qty02"].ToString(), 0);
            jego3 = jego3 + StCommon.ToDouble(dr["blju_qty03"].ToString(), 0);
            jego4 = jego4 + StCommon.ToDouble(dr["blju_qty04"].ToString(), 0);
            jego5 = jego5 + StCommon.ToDouble(dr["blju_qty05"].ToString(), 0);
            jego6 = jego6 + StCommon.ToDouble(dr["blju_qty06"].ToString(), 0);
            jego7 = jego7 + StCommon.ToDouble(dr["blju_qty07"].ToString(), 0);
            jego8 = jego8 + StCommon.ToDouble(dr["blju_qty08"].ToString(), 0);
            jego9 = jego9 + StCommon.ToDouble(dr["blju_qty09"].ToString(), 0);
            jego10 = jego10 + StCommon.ToDouble(dr["blju_qty10"].ToString(), 0);
            jego11 = jego11 + StCommon.ToDouble(dr["blju_qty11"].ToString(), 0);
            jego12 = jego12 + StCommon.ToDouble(dr["blju_qty12"].ToString(), 0);
        }

        double order1 = StCommon.ToDouble(this.txtOrder1.Text, 0);
        double order2 = StCommon.ToDouble(this.txtOrder2.Text, 0);
        double order3 = StCommon.ToDouble(this.txtOrder3.Text, 0);
        double order4 = StCommon.ToDouble(this.txtOrder4.Text, 0);
        double order5 = StCommon.ToDouble(this.txtOrder5.Text, 0);
        double order6 = StCommon.ToDouble(this.txtOrder6.Text, 0);
        double order7 = StCommon.ToDouble(this.txtOrder7.Text, 0);
        double order8 = StCommon.ToDouble(this.txtOrder8.Text, 0);
        double order9 = StCommon.ToDouble(this.txtOrder9.Text, 0);
        double order10 = StCommon.ToDouble(this.txtOrder10.Text, 0);
        double order11 = StCommon.ToDouble(this.txtOrder11.Text, 0);
        double order12 = StCommon.ToDouble(this.txtOrder12.Text, 0);
        double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12;

        // 재고 < 주문량 일 경우, 주문량 초과임.
        if ((jego1 < order1) || (jego2 < order2) || (jego3 < order3) || (jego4 < order4) || (jego5 < order5) || (jego6 < order6) || (jego7 < order7) || (jego8 < order8) || (jego9 < order9) || (jego10 < order10) || (jego11 < order11) || (jego12 < order12))
        {
            result = "over";
        }

        if (orderTotal == 0)
        {
            result = "zero";
        }

        return result;
    }

    private void SetJustPrice(string blju_buyer, string blju_style)
    {
        // tblDNGA에서 단가를 가져와 합계수량 *단가를 적용
        /*
         gblKURE 테이블에서 
         Kure_PriceGubun 은
        1:하한가(LowPrice)
        2:정상가(JustPrice)
        3:상한가(HighPrice)

        tblDNGA 테이블에서 가격을 Dnga_LowPrice를 쓰는지, Dnga_LowPrice를 쓰는지, Dnga_HighPrice를 쓰는지 정하면 됨
        gblKURE에서 대리점들이 쓰는 코드는 BR로 시작되는 코드만 쓸 것임
        
        Kure_PriceGubun 가 Null인 것은 BR로 시작되는 코드일 경우임. 그럼 2:정상가(JustPrice) 값으로 적용하면 됨.
         */

        string qry = "";
        double price = 0;

        /*
        qry = " SELECT isnull(Kure_PriceGubun,'') as PriceGubun from gblKURE where kure_code = '" + blju_buyer + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            string PriceGubun = dsK.Tables[0].Rows[0]["PriceGubun"].ToString();
            PriceGubun = (PriceGubun == "") ? "2" : PriceGubun;

            qry = " SELECT * from tblDNGA where dnga_stylenox = '" + blju_style + "' ";
            DataSet dsD = stData.GetDataSet(qry);

            if (PriceGubun == "1")
            {
                price = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
            }
            else if (PriceGubun == "2")
            {
                price = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
            }
            else if (PriceGubun == "3")
            {
                price = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_HighPrice"].ToString(), 0);
            }
        }
        */

        string allBoxDnga = "";
        string allZeroDnga = "";
        
        qry = " SELECT * from gblKURE where kure_code = '" + blju_buyer + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            allBoxDnga = dsK.Tables[0].Rows[0]["Kure_All_BoxDnga"].ToString().Trim();
            allZeroDnga = dsK.Tables[0].Rows[0]["Kure_All_ZeroDnga"].ToString().Trim();
        }

        /*
        if (MemberData.GetLoginSID("LoginID") != "ZQ")
        {
            allBoxDnga = "";
            allZeroDnga = "";
        }
        */

        double order1 = StCommon.ToDouble(this.txtOrder1.Text, 0);
        double order2 = StCommon.ToDouble(this.txtOrder2.Text, 0);
        double order3 = StCommon.ToDouble(this.txtOrder3.Text, 0);
        double order4 = StCommon.ToDouble(this.txtOrder4.Text, 0);
        double order5 = StCommon.ToDouble(this.txtOrder5.Text, 0);
        double order6 = StCommon.ToDouble(this.txtOrder6.Text, 0);

        double order7 = StCommon.ToDouble(this.txtOrder7.Text, 0);
        double order8 = StCommon.ToDouble(this.txtOrder8.Text, 0);
        double order9 = StCommon.ToDouble(this.txtOrder9.Text, 0);
        double order10 = StCommon.ToDouble(this.txtOrder10.Text, 0);
        double order11 = StCommon.ToDouble(this.txtOrder11.Text, 0);
        double order12 = StCommon.ToDouble(this.txtOrder12.Text, 0);
        double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12;

        double price1 = 0;
        double price2 = 0;
        double price3 = 0;
        double price4 = 0;
        double price5 = 0;
        double price6 = 0;
        double price7 = 0;
        double price8 = 0;
        double price9 = 0;
        double price10 = 0;
        double price11 = 0;
        double price12 = 0;
        
        qry = " SELECT * from tblDNGA where dnga_stylenox = '" + blju_style + "' ";
        DataSet dsD = stData.GetDataSet(qry);

        if (dsD.Tables[0].Rows.Count > 0)
        {
            double boxQty = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_BoxQty"].ToString(), 0);
            double bigSizePrice = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString(), 0);
            double sizeBoxQty = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString(), 0);
            
            this.hidDngaLowPrice.Value = dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString();
            this.hidDngaJustPrice.Value = dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString();
            this.hidDngaBigSizePrice.Value = dsD.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString();
            this.hidDngaSizeBoxQty.Value = dsD.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString();
            this.hidDngaBoxQty.Value = dsD.Tables[0].Rows[0]["Dnga_BoxQty"].ToString();
            this.hidDngaIpgoPrice.Value = dsD.Tables[0].Rows[0]["Dnga_ipgoPrice"].ToString();

            if (boxQty == 0)
            {
                price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
            }
            else
            {
                if (boxQty <= orderTotal) // 박스가 적용
                {
                    price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                }
                else
                {
                    price1 = StCommon.ToDouble((order1 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price2 = StCommon.ToDouble((order2 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price3 = StCommon.ToDouble((order3 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price4 = StCommon.ToDouble((order4 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price5 = StCommon.ToDouble((order5 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price6 = StCommon.ToDouble((order6 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price7 = StCommon.ToDouble((order7 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price8 = StCommon.ToDouble((order8 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price9 = StCommon.ToDouble((order9 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price10 = StCommon.ToDouble((order10 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price11 = StCommon.ToDouble((order11 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price12 = StCommon.ToDouble((order12 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                }
            }

            // Big사이즈 추가금 (2XL부터 했었는데 5XL부터 적용되게 변경 20211118
            //price7 += bigSizePrice;
            //price8 += bigSizePrice;
            //price9 += bigSizePrice;
            price10 += bigSizePrice;
            price11 += bigSizePrice;
            price12 += bigSizePrice;

            if (allBoxDnga == "Y")
            {
                price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
            }

            if (allZeroDnga == "Y")
            {
                price1 = 0;
                price2 = 0;
                price3 = 0;
                price4 = 0;
                price5 = 0;
                price6 = 0;
                price7 = 0;
                price8 = 0;
                price9 = 0;
                price10 = 0;
                price11 = 0;
                price12 = 0;
            }
        }

        this.txtOrderTotal.Text = StCommon.NumberFormat(orderTotal);

        //double justAmount = price * orderTotal;
        double justAmount = (price1 * order1) + (price2 * order2) + (price3 * order3) + (price4 * order4) + (price5 * order5) + (price6 * order6) + (price7 * order7) + (price8 * order8) + (price9 * order9) + (price10 * order10) + (price11 * order11) + (price12 * order12);

        this.hidJustAmount.Value = (justAmount).ToString();
        this.hidJustPrice.Value = (justAmount / orderTotal).ToString();
        this.hidDnga1.Value = (price1).ToString();
        this.hidDnga2.Value = (price2).ToString();
        this.hidDnga3.Value = (price3).ToString();
        this.hidDnga4.Value = (price4).ToString();
        this.hidDnga5.Value = (price5).ToString();
        this.hidDnga6.Value = (price6).ToString();
        this.hidDnga7.Value = (price7).ToString();
        this.hidDnga8.Value = (price8).ToString();
        this.hidDnga9.Value = (price9).ToString();
        this.hidDnga10.Value = (price10).ToString();
        this.hidDnga11.Value = (price11).ToString();
        this.hidDnga12.Value = (price12).ToString();
    }

    private void SetHeadAmount(string blju_date, string blju_times, string blju_mainbuyer, string blju_sample)
    {
        string allNoTax = "";

        string qry = " SELECT * from gblKURE where kure_code = '" + blju_mainbuyer + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            allNoTax = dsK.Tables[0].Rows[0]["Kure_All_NoTax"].ToString().Trim();
        }

        /*
        if (MemberData.GetLoginSID("LoginID") != "ZQ")
        {
            allNoTax = "";
        }
        */

        qry = " select isnull(sum(Blju_JustAmount),0) FROM tblBLJU a where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' ";
        double bljuTotal = Convert.ToDouble(stData.GetDataOne(qry));

        double net = bljuTotal;
        double vat = net * (0.1);

        if (allNoTax == "Y")
        {
            vat = 0;
        }

        double hap = net + vat;

        qry = " UPDATE tblBJHD SET Bjhd_NetAmount = '"+ net.ToString() + "',Bjhd_VatAmount = '" + vat.ToString() + "',Bjhd_HapAmount = '" + hap.ToString() + "',Bjhd_ModifyDate=getDate(),Bjhd_ModifySaWon = '" + MemberData.GetLoginSID("LoginID") + "' where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample = '" + blju_sample + "' ";
        stData.GetExecuteNonQry(qry);

        whis.InsertWork("발주헤더", "금액업데이트", qry);

        this.txtNetAmount.Text = StCommon.NumberFormat(net);
        this.txtVatAmount.Text = StCommon.NumberFormat(vat);
        this.txtHapAmount.Text = StCommon.NumberFormat(hap);
    }

    private void DateUpdate(string blju_date, string blju_times, string blju_mainbuyer, string blju_sample)
    {
        string qry = " select * FROM tblBJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample = '" + blju_sample + "' ";
        DataSet ds = stData.GetDataSet(qry);
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.txtFirstDate.Text = ds.Tables[0].Rows[0]["Bjhd_CreateDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_CreateSawon"].ToString();
            this.txtLastDate.Text = ds.Tables[0].Rows[0]["Bjhd_ModifyDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_ModifySaWon"].ToString();
        }
    }
    
    private void WriteDefault()
    {
        // 버튼 비활성화
        EnableButton(false);

        this.hidLine.Value = "";
        
        this.txtProduct.Text = "";
        this.txtProductDetail.Text = "";
        this.txtMsg.Text = "";
        string code = "";
        this.imgProduct.ImageUrl = "~/Handler/DisplayImage.ashx?code=" + code;

        this.txtJego1.Text = "";
        this.txtJego2.Text = "";
        this.txtJego3.Text = "";
        this.txtJego4.Text = "";
        this.txtJego5.Text = "";
        this.txtJego6.Text = "";
        this.txtJego7.Text = "";
        this.txtJego8.Text = "";
        this.txtJego9.Text = "";
        this.txtJego10.Text = "";
        this.txtJego11.Text = "";
        this.txtJego12.Text = "";
        this.txtJegoTotal.Text = "";
        this.txtOrder1.Text = "";
        this.txtOrder2.Text = "";
        this.txtOrder3.Text = "";
        this.txtOrder4.Text = "";
        this.txtOrder5.Text = "";
        this.txtOrder6.Text = "";
        this.txtOrder7.Text = "";
        this.txtOrder8.Text = "";
        this.txtOrder9.Text = "";
        this.txtOrder10.Text = "";
        this.txtOrder11.Text = "";
        this.txtOrder12.Text = "";
        this.txtOrderTotal.Text = "";

        this.btnWrite.Visible = true;
        this.btnModify.Visible = false;

        this.btnSelectProduct.Visible = true;
        this.ltlProductTitle.Visible = false;
        this.txtProduct.Enabled = true;

        string date = StCommon.ReplaceSQ(this.txtDate.Text);
        string time = StCommon.ReplaceSQ(this.txtTime.Text);
        string kure = this.hidKureCode.Value;
        
        string qry = " select * from tblBJHD where Bjhd_Bonsa_Check in('0') and Bjhd_Date = '" + date + "' and Bjhd_Times = '" + time + "' and Bjhd_MainBuyer = '" + kure + "' order by Bjhd_Times desc ";

        DataSet ds = stData.GetDataSet(qry);

        this.lnkComplete.OnClientClick = "return true;";

        if (ds.Tables[0].Rows.Count > 0)
        {
            qry = " select * from tblBLJU where Blju_Date = '" + date + "' and Blju_Times = '" + time + "' and Blju_MainBuyer = '" + kure + "' ";
            DataSet dsC = stData.GetDataSet(qry);

            if (dsC.Tables[0].Rows.Count > 0)
            {
                this.lnkComplete.OnClientClick = "return CheckComplete();";
            }
        }

        st.Kind = this.hidKind.Value;
        st.GetSizeInfo("tbl");

        this.ltlSize1.Text = st.SizeName1 + "<br>(" + st.SizeNum1 + ")";
        this.ltlSize2.Text = st.SizeName2 + "<br>(" + st.SizeNum2 + ")";
        this.ltlSize3.Text = st.SizeName3 + "<br>(" + st.SizeNum3 + ")";
        this.ltlSize4.Text = st.SizeName4 + "<br>(" + st.SizeNum4 + ")";
        this.ltlSize5.Text = st.SizeName5 + "<br>(" + st.SizeNum5 + ")";
        this.ltlSize6.Text = st.SizeName6 + "<br>(" + st.SizeNum6 + ")";
        this.ltlSize7.Text = st.SizeName7 + "<br>(" + st.SizeNum7 + ")";
        this.ltlSize8.Text = st.SizeName8 + "<br>(" + st.SizeNum8 + ")";
        this.ltlSize9.Text = st.SizeName9 + "<br>(" + st.SizeNum9 + ")";
        this.ltlSize10.Text = st.SizeName10 + "<br>(" + st.SizeNum10 + ")";
        this.ltlSize11.Text = st.SizeName11 + "<br>(" + st.SizeNum11 + ")";
        this.ltlSize12.Text = st.SizeName12 + "<br>(" + st.SizeNum12 + ")";
        this.ltlSizeTotal.Text = "TOTAL";
    }

    protected void btnWrite_Click(object sender, EventArgs e)
    {
        string date = StCommon.ReplaceSQ(this.txtDate.Text);
        string time = StCommon.ReplaceSQ(this.txtTime.Text);
        //string kure = ((DropDownList)this.ucTrader.FindControl("ddlTrader")).SelectedValue;
        string kure = this.hidKureCode.Value;

        string baesong = StCommon.ReplaceSQ(this.hidBaeSong.Value);
        string baesongname = StCommon.ReplaceSQ(this.txtBaeSongName.Text);

        string etc = StCommon.ReplaceSQ(this.txtEtc.Text);

        string product = StCommon.ReplaceSQ(this.txtProduct.Text);
        /*
         
          사이즈별 재고량과 주문량을 다시 한 번 비교하여 모든 사이즈가 재고량 범위에 있으면 리스트에 추가하고,
            tblDNGA에서 단가를 가져와 합계수량 * 단가를 적용하고
            tblBJHD, tblBLJU에 각각 INSERT한다
         
         */

        // 동일 스타일 중복 체크
        string existLine = StCommon.DupleStyleCheck(date, time, kure, product, "", "");
        if (existLine != "")
        {
            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("showMessageToolTip('" + this.btnWrite.ClientID + "', '" + existLine + "번 라인에 해당스타일이 이미 등록되어 있습니다!');");

            JegoSet(product, "", date, time, kure, "");

            EnableButton(true);

            JegoEnable();

            SetProduct(product);

            SetJustPrice(kure, product);

            BindDataList();
        }
        else
        {
            string jegoOverChk = JegoOverCheck("");
            SetJustPrice(kure, product);

            if (jegoOverChk == "")
            {
                double order1 = StCommon.ToDouble(this.txtOrder1.Text, 0);
                double order2 = StCommon.ToDouble(this.txtOrder2.Text, 0);
                double order3 = StCommon.ToDouble(this.txtOrder3.Text, 0);
                double order4 = StCommon.ToDouble(this.txtOrder4.Text, 0);
                double order5 = StCommon.ToDouble(this.txtOrder5.Text, 0);
                double order6 = StCommon.ToDouble(this.txtOrder6.Text, 0);
                double order7 = StCommon.ToDouble(this.txtOrder7.Text, 0);
                double order8 = StCommon.ToDouble(this.txtOrder8.Text, 0);
                double order9 = StCommon.ToDouble(this.txtOrder9.Text, 0);
                double order10 = StCommon.ToDouble(this.txtOrder10.Text, 0);
                double order11 = StCommon.ToDouble(this.txtOrder11.Text, 0);
                double order12 = StCommon.ToDouble(this.txtOrder12.Text, 0);
                double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12;

                OrderData_tbl order = new OrderData_tbl();

                order.Date = date;
                order.Time = time;
                order.Kure = kure;
                order.Baesong = baesong;
                order.BaesongName = baesongname;
                order.Etc = etc;
                order.Sdate = DateTime.Now;
                order.Ssawon = MemberData.GetLoginSID("LoginID");
                order.Mdate = DateTime.Now;
                order.Msawon = MemberData.GetLoginSID("LoginID");
                order.JustPrice = this.hidJustPrice.Value;
                order.JustAmount = this.hidJustAmount.Value;
                order.Product = product;
                order.Order1 = order1;
                order.Order2 = order2;
                order.Order3 = order3;
                order.Order4 = order4;
                order.Order5 = order5;
                order.Order6 = order6;
                order.Order7 = order7;
                order.Order8 = order8;
                order.Order9 = order9;
                order.Order10 = order10;
                order.Order11 = order11;
                order.Order12 = order12;
                order.OrderTotal = orderTotal;
                order.Dnga1 = this.hidDnga1.Value;
                order.Dnga2 = this.hidDnga2.Value;
                order.Dnga3 = this.hidDnga3.Value;
                order.Dnga4 = this.hidDnga4.Value;
                order.Dnga5 = this.hidDnga5.Value;
                order.Dnga6 = this.hidDnga6.Value;
                order.Dnga7 = this.hidDnga7.Value;
                order.Dnga8 = this.hidDnga8.Value;
                order.Dnga9 = this.hidDnga9.Value;
                order.Dnga10 = this.hidDnga10.Value;
                order.Dnga11 = this.hidDnga11.Value;
                order.Dnga12 = this.hidDnga12.Value;
                order.DngaLowPrice = this.hidDngaLowPrice.Value;
                order.DngaJustPrice = this.hidDngaJustPrice.Value;
                order.DngaBigSizePrice = this.hidDngaBigSizePrice.Value;
                order.DngaSizeBoxQty = StCommon.ToDouble(this.hidDngaSizeBoxQty.Value, 0);
                order.DngaBoxQty = StCommon.ToDouble(this.hidDngaBoxQty.Value, 0);
                order.DngaIpgoPrice = this.hidDngaIpgoPrice.Value;

                order.InsertHead();
                order.InsertData();

                // 헤더 금액 업데이트
                SetHeadAmount(date, time, kure, "0");

                // 최초등록일자, 최종수정일자 업데이트
                DateUpdate(date, time, kure, "0");

                WriteDefault();
                BindDataList();
            }
            else if (jegoOverChk == "over")
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                //js.WriteJavascript("showMessageDiv('validateMsg', '주문량이 재고 수량보다 많습니다.');");
                js.WriteJavascript("showMessageToolTip('" + this.btnWrite.ClientID + "', '주문량이 재고 수량보다 많습니다.');");

                JegoSet(product, "", date, time, kure, "");

                EnableButton(true);

                JegoEnable();

                SetProduct(product);

                SetJustPrice(kure, product);
            }
            else if (jegoOverChk == "zero")
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                //js.WriteJavascript("showMessageDiv('validateMsg', '주문량을 입력해주세요.');");
                js.WriteJavascript("showMessageToolTip('" + this.txtOrderTotal.ClientID + "', '주문량을 입력해주세요.');");
                
                JegoSet(product, "", date, time, kure, "");

                EnableButton(true);

                JegoEnable();

                SetProduct(product);
            }
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string date = StCommon.ReplaceSQ(this.txtDate.Text);
        string time = StCommon.ReplaceSQ(this.txtTime.Text);
        //string kure = ((DropDownList)this.ucTrader.FindControl("ddlTrader")).SelectedValue;
        string kure = this.hidKureCode.Value;

        string baesong = StCommon.ReplaceSQ(this.hidBaeSong.Value);
        string baesongname = StCommon.ReplaceSQ(this.txtBaeSongName.Text);

        string line = this.hidLine.Value;

        string etc = StCommon.ReplaceSQ(this.txtEtc.Text);

        string product = StCommon.ReplaceSQ(this.txtProduct.Text);
        /*
         
          사이즈별 재고량과 주문량을 다시 한 번 비교하여 모든 사이즈가 재고량 범위에 있으면 리스트에 추가하고,
            tblDNGA에서 단가를 가져와 합계수량 * 단가를 적용하고
            tblBJHD, tblBLJU에 각각 UPDATE한다
         
         */

        // 동일 스타일 중복 체크
        string existLine = StCommon.DupleStyleCheck(date, time, kure, product, line, "mod");
        if (existLine != "")
        {
            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("showMessageToolTip('" + this.btnModify.ClientID + "', '" + existLine + "번 라인에 해당스타일이 이미 등록되어 있습니다!');");

            JegoSet(product, "mod", date, time, kure, line);

            EnableButton(true);

            JegoEnable();

            SetProduct(product);

            SetJustPrice(kure, product);

            BindDataList();
        }
        else
        {
            string jegoOverChk = JegoOverCheck("mod");
            SetJustPrice(kure, product);
            if (jegoOverChk == "")
            {
                double order1 = StCommon.ToDouble(this.txtOrder1.Text, 0);
                double order2 = StCommon.ToDouble(this.txtOrder2.Text, 0);
                double order3 = StCommon.ToDouble(this.txtOrder3.Text, 0);
                double order4 = StCommon.ToDouble(this.txtOrder4.Text, 0);
                double order5 = StCommon.ToDouble(this.txtOrder5.Text, 0);
                double order6 = StCommon.ToDouble(this.txtOrder6.Text, 0);
                double order7 = StCommon.ToDouble(this.txtOrder7.Text, 0);
                double order8 = StCommon.ToDouble(this.txtOrder8.Text, 0);
                double order9 = StCommon.ToDouble(this.txtOrder9.Text, 0);
                double order10 = StCommon.ToDouble(this.txtOrder10.Text, 0);
                double order11 = StCommon.ToDouble(this.txtOrder11.Text, 0);
                double order12 = StCommon.ToDouble(this.txtOrder12.Text, 0);
                double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12;

                OrderData_tbl order = new OrderData_tbl();

                order.Date = date;
                order.Time = time;
                order.Kure = kure;
                order.Baesong = baesong;
                order.BaesongName = baesongname;
                order.Line = line;
                order.Etc = etc;
                order.Mdate = DateTime.Now;
                order.Msawon = MemberData.GetLoginSID("LoginID");
                order.JustPrice = this.hidJustPrice.Value;
                order.JustAmount = this.hidJustAmount.Value;
                order.Product = product;
                order.Order1 = order1;
                order.Order2 = order2;
                order.Order3 = order3;
                order.Order4 = order4;
                order.Order5 = order5;
                order.Order6 = order6;
                order.Order7 = order7;
                order.Order8 = order8;
                order.Order9 = order9;
                order.Order10 = order10;
                order.Order11 = order11;
                order.Order12 = order12;
                order.OrderTotal = orderTotal;
                order.Dnga1 = this.hidDnga1.Value;
                order.Dnga2 = this.hidDnga2.Value;
                order.Dnga3 = this.hidDnga3.Value;
                order.Dnga4 = this.hidDnga4.Value;
                order.Dnga5 = this.hidDnga5.Value;
                order.Dnga6 = this.hidDnga6.Value;
                order.Dnga7 = this.hidDnga7.Value;
                order.Dnga8 = this.hidDnga8.Value;
                order.Dnga9 = this.hidDnga9.Value;
                order.Dnga10 = this.hidDnga10.Value;
                order.Dnga11 = this.hidDnga11.Value;
                order.Dnga12 = this.hidDnga12.Value;
                order.DngaLowPrice = this.hidDngaLowPrice.Value;
                order.DngaJustPrice = this.hidDngaJustPrice.Value;
                order.DngaBigSizePrice = this.hidDngaBigSizePrice.Value;
                order.DngaSizeBoxQty = StCommon.ToDouble(this.hidDngaSizeBoxQty.Value, 0);
                order.DngaBoxQty = StCommon.ToDouble(this.hidDngaBoxQty.Value, 0);
                order.DngaIpgoPrice = this.hidDngaIpgoPrice.Value;

                order.UpdateHead();
                order.UpdateData();

                // 헤더 금액 업데이트
                SetHeadAmount(date, time, kure, "0");

                // 최초등록일자, 최종수정일자 업데이트
                DateUpdate(date, time, kure, "0");

                WriteDefault();
                BindDataList();
            }
            else if (jegoOverChk == "over")
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                //js.WriteJavascript("showMessageDiv('validateMsg', '주문량이 재고 수량보다 많습니다.');");
                js.WriteJavascript("showMessageToolTip('" + this.btnModify.ClientID + "', '주문량이 재고 수량보다 많습니다.');");

                JegoSet(product, "mod", date, time, kure, line);

                EnableButton(true);

                JegoEnable();

                SetProduct(product);

                SetJustPrice(kure, product);
            }
            else if (jegoOverChk == "zero")
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.WriteJavascript("showMessageToolTip('" + this.txtOrderTotal.ClientID + "', '주문량을 입력해주세요.');");
                
                JegoSet(product, "mod", date, time, kure, line);

                EnableButton(true);

                JegoEnable();

                SetProduct(product);
            }
        }
    }

    protected void lnkComplete_Click(object sender, EventArgs e)
    {
        string date = txtDate.Text;
        string time = txtTime.Text;
        string kure = this.hidKureCode.Value;

        string baesong = StCommon.ReplaceSQ(this.hidBaeSong.Value);
        string baesongname = StCommon.ReplaceSQ(this.txtBaeSongName.Text);

        string etc = StCommon.ReplaceSQ(this.txtEtc.Text);

        string qry = " select * from tblBLJU where Blju_Date = '" + date + "' and Blju_Times = '" + time + "' and Blju_MainBuyer = '" + kure + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        if (dsC.Tables[0].Rows.Count == 0)
        {
            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("showMessageToolTip('" + this.lnkComplete.ClientID + "', '주문 내역이 없습니다. 상품 선택후 주문량을 입력해주세요.');");
        }
        else
        {
            string nowBonsaCheck = StCommon.GetBonsaCheck("tbl", kure, date, time);
            if (nowBonsaCheck != "" && nowBonsaCheck != "0")
            {
                string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);

                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.WriteJavascript("showMessageToolTip('" + this.lnkComplete.ClientID + "', '현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.');");
            }
            else
            {
                string whereQry = " where Bjhd_MainBuyer = '" + kure + "' ";
                whereQry = StCommon.MakeSearchQry("Bjhd_Date", date, "S", whereQry);
                whereQry = StCommon.MakeSearchQry("Bjhd_Times", time, "S", whereQry);

                // 주문완료 후 종료  tblBJHD테이블 Bjhd_Bonsa_Check 필드에 '1': 주문완료
                qry = " update tblBJHD set Bjhd_Bonsa_Check = '1',Bjhd_BaeSong = '" + baesong + "',Bjhd_BaeSongName = '" + baesongname + "',Bjhd_Remark = '" + etc + "',Bjhd_ModifyDate=getDate(),Bjhd_ModifySaWon = '" + MemberData.GetLoginSID("LoginID") + "' " + whereQry;
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주", "주문완료", qry);

                //BindDataList();
                Response.Redirect("/Page/Order_tbl.aspx");
            }
        }
    }

    protected void fncProduct()
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileFactory.CheckImageFile(this.fu.FileName))
        {
            //byte로 변환
            byte[] imgByte = null;

            System.Drawing.Image orgImage = System.Drawing.Image.FromStream(fu.FileContent);

            MemoryStream ms = new MemoryStream();
            orgImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            imgByte = new byte[ms.Length];
            ms.Read(imgByte, 0, (int)ms.Length);
            ms.Close();

            string qry = " insert into tblJEPG(Jepg_StyleNox,Jepg_Image) values (@Jepg_StyleNox,@Jepg_Image); ";
            DbCommand cmd = db.GetSqlStringCommand(qry);

            db.AddInParameter(cmd, "Jepg_StyleNox", SqlDbType.VarChar, "BR-101");
            db.AddInParameter(cmd, "Jepg_Image", SqlDbType.Image, imgByte);

            db.ExecuteNonQuery(cmd);
        }
    }

    // [새로고침]: 해당 스타일 본사재고 리셋, 주문량 초기화
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string product = StCommon.ReplaceSQ(this.txtProduct.Text);

        string date = StCommon.ReplaceSQ(this.txtDate.Text);
        string time = StCommon.ReplaceSQ(this.txtTime.Text);
        string kure = this.hidKureCode.Value;
        string line = this.hidLine.Value;

        if (this.hidLine.Value == "")
        {
            JegoSet(product, "", date, time, kure, "");
        }
        else
        {
            JegoSet(product, "mod", date, time, kure, line);
        }

        this.txtOrder1.Text = "";
        this.txtOrder2.Text = "";
        this.txtOrder3.Text = "";
        this.txtOrder4.Text = "";
        this.txtOrder5.Text = "";
        this.txtOrder6.Text = "";
        this.txtOrder7.Text = "";
        this.txtOrder8.Text = "";
        this.txtOrder9.Text = "";
        this.txtOrder10.Text = "";
        this.txtOrder11.Text = "";
        this.txtOrder12.Text = "";
        this.txtOrderTotal.Text = "";

        EnableButton(true);

        JegoEnable();

        SetProduct(product);
    }

    private void JegoEnable()
    {
        double jego1 = StCommon.ToDouble(this.txtJego1.Text, 0);
        double jego2 = StCommon.ToDouble(this.txtJego2.Text, 0);
        double jego3 = StCommon.ToDouble(this.txtJego3.Text, 0);
        double jego4 = StCommon.ToDouble(this.txtJego4.Text, 0);
        double jego5 = StCommon.ToDouble(this.txtJego5.Text, 0);
        double jego6 = StCommon.ToDouble(this.txtJego6.Text, 0);
        double jego7 = StCommon.ToDouble(this.txtJego7.Text, 0);
        double jego8 = StCommon.ToDouble(this.txtJego8.Text, 0);
        double jego9 = StCommon.ToDouble(this.txtJego9.Text, 0);
        double jego10 = StCommon.ToDouble(this.txtJego10.Text, 0);
        double jego11 = StCommon.ToDouble(this.txtJego11.Text, 0);
        double jego12 = StCommon.ToDouble(this.txtJego12.Text, 0);

        if (jego1 == 0)
            this.txtOrder1.Enabled = false;
        if (jego2 == 0)
            this.txtOrder2.Enabled = false;
        if (jego3 == 0)
            this.txtOrder3.Enabled = false;
        if (jego4 == 0)
            this.txtOrder4.Enabled = false;
        if (jego5 == 0)
            this.txtOrder5.Enabled = false;
        if (jego6 == 0)
            this.txtOrder6.Enabled = false;
        if (jego7 == 0)
            this.txtOrder7.Enabled = false;
        if (jego8 == 0)
            this.txtOrder8.Enabled = false;
        if (jego9 == 0)
            this.txtOrder9.Enabled = false;
        if (jego10 == 0)
            this.txtOrder10.Enabled = false;
        if (jego11 == 0)
            this.txtOrder11.Enabled = false;
        if (jego12 == 0)
            this.txtOrder12.Enabled = false;
    }

    private void SetProduct(string product)
    {
        string qry = " select Dnga_MainName, Dnga_SubName, Dnga_SpecColor from tblDNGA where Dnga_StyleNox='" + product + "' ";
        DataSet dsD = stData.GetDataSet(qry);

        if (dsD.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsD.Tables[0].Rows[0];

            this.txtProductDetail.Text = dr["Dnga_MainName"].ToString() + ", " + dr["Dnga_SubName"].ToString() + ", " + dr["Dnga_SpecColor"].ToString();
            this.imgProduct.ImageUrl = "~/Handler/DisplayImage.ashx?code=" + product;
        }
    }

    // [신규]: 발주스타일, 본사재고, 주문량 초기화
    protected void btnNewWrite_Click(object sender, EventArgs e)
    {
        WriteDefault();
        /*
         * string bljudate = DateTime.Now.ToShortDateString();
        string bljudatetime = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);
        string kurename = MemberData.GetLoginSID("KureName");
        string kurecode = MemberData.GetLoginSID("KureCode");

        WriteDefault();

        BindCheck(bljudate, bljudatetime, kurecode);
        BindData(kurename, kurecode);
        BindDataList();
        */
    }
    
    private void EnableButton(bool isEnable)
    {
        this.btnRefresh.Enabled = isEnable;
        this.btnNewWrite.Enabled = isEnable;
        this.btnWrite.Enabled = isEnable;
        this.btnModify.Enabled = isEnable;
        
        this.txtOrder1.Enabled = isEnable;
        this.txtOrder2.Enabled = isEnable;
        this.txtOrder3.Enabled = isEnable;
        this.txtOrder4.Enabled = isEnable;
        this.txtOrder5.Enabled = isEnable;
        this.txtOrder6.Enabled = isEnable;
        this.txtOrder7.Enabled = isEnable;
        this.txtOrder8.Enabled = isEnable;
        this.txtOrder9.Enabled = isEnable;
        this.txtOrder10.Enabled = isEnable;
        this.txtOrder11.Enabled = isEnable;
        this.txtOrder12.Enabled = isEnable;

        this.btnSelectProduct.Visible = !isEnable;
        this.ltlProductTitle.Visible = isEnable;
        this.txtProduct.Enabled = !isEnable;
    }
    #endregion

    #region 발주의뢰 현황조회
    private void BindList()
    {
        string whereQry = "";

        string bDay1 = ((TextBox)this.ucBljuday.FindControl("txtDate")).Text;
        string bDay2 = ((TextBox)this.ucBljuday1.FindControl("txtDate")).Text;

        Session["bljudayS"] = bDay1;
        Session["bljudayE"] = bDay2;

        string kurecode = MemberData.GetLoginSID("KureCode");
        string product = StCommon.ReplaceSQ(this.txtProductList.Text);

        whereQry = StCommon.MakeSearchQry("bjhd_date", bDay1, bDay2, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("bjhd_mainbuyer", kurecode, "S", whereQry);

        string qry = " SELECT (select kure_sangho from gblKURE where kure_code = Bjhd_MainBuyer) as Bjhd_MainBuyerNm,(select count(*) tblMESG where Mesg_Date = Bjhd_Date AND Mesg_Times = Bjhd_Times AND Mesg_MainBuyer = Bjhd_MainBuyer AND Mesg_Sample = Bjhd_Sample) msgCnt,*,ISNULL(SQL_BonSaReadOk,0) as BonSaReadOk,ISNULL(SQL_DaeRiReadOk,0) as DaeRiReadOk ";
        qry += " , LTRIM(STUFF((select ',' + Blju_StyleNox from tblBLJU where isnull(Blju_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Blju_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Blju_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Blju_Sample,'') = isnull(a.Bjhd_Sample,'') FOR XML PATH('')), 1, 1, '')) as Blju_StyleNox ";
        qry += " , (select Bjhd0_Name from tblBJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd0_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd0_Name ";
        qry += " , (select Bjhd0_Name_Send from tblBJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd0_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd0_Name_Send ";
        qry += " FROM tblBJHD a LEFT OUTER JOIN (SELECT Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample,SUM(case when Mesg_BonSa_Daeri = '1' then Mesg_BonSaReadOk else 0 end) AS SQL_BonSaReadOk,SUM(case when Mesg_BonSa_Daeri = '0' then Mesg_DaeRiReadOk else 0 end) AS SQL_DaeRiReadOk FROM tblMESG GROUP BY Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample) t1 ";
        qry += " ON t1.Mesg_Date = Bjhd_Date AND t1.Mesg_Times = Bjhd_Times AND t1.Mesg_MainBuyer = Bjhd_MainBuyer AND t1.Mesg_Sample = Bjhd_Sample ";
        qry += " where 1=1 " + whereQry + " ORDER BY Bjhd_Bonsa_Check, bjhd_date desc, bjhd_times desc, bjhd_mainbuyer ";
        
        DataSet tmpData = stData.GetDataSet(qry);

        totCount = tmpData.Tables[0].Rows.Count;

        this.lvList.DataSource = tmpData;
        this.lvList.DataBind();
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            Literal ltlStyleNox = (Literal)e.Item.FindControl("ltlStyleNox");
            Literal ltlBaesong = (Literal)e.Item.FindControl("ltlBaesong");
            Literal ltlBaesongSend = (Literal)e.Item.FindControl("ltlBaesongSend");
            Literal ltlBonsaCheck = (Literal)e.Item.FindControl("ltlBonsaCheck");
            LinkButton lnkMessenger = (LinkButton)e.Item.FindControl("lnkMessenger");
            LinkButton lnkSubView = (LinkButton)e.Item.FindControl("lnkSubView");
            LinkButton lnkSubModify = (LinkButton)e.Item.FindControl("lnkSubModify");
            LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            char[] delimiter = ",".ToCharArray();
            string[] strArray = drItemRow["Blju_StyleNox"].ToString().Trim().Split(delimiter);

            if (strArray.Length > 0)
            {
                ltlStyleNox.Text = strArray[0];
                if (strArray.Length > 1)
                {
                    ltlStyleNox.Text = ltlStyleNox.Text + " 외 " + (strArray.Length - 1).ToString() + "건";
                }
            }

            if (drItemRow["Bjhd_BaeSong"].ToString() == "0")
            {
                ltlBaesong.Text = "화물 발송";
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name_Send"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "1")
            {
                ltlBaesong.Text = "로젠 택배";
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "2")
            {
                ltlBaesong.Text = "화물 택배";
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "3")
            {
                ltlBaesong.Text = "기타 발송";
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }

            if (drItemRow["Bjhd_BaeSongName"].ToString() == "")
            {
                ltlBaesong.Text = "";
            }

            if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "0")
            {
                ltlBonsaCheck.Text = "발주의뢰 중";
            }
            else if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "1")
            {
                ltlBonsaCheck.Text = "발주의뢰 완료";
                lnkSubModify.OnClientClick = "return confirm('[발주의뢰 중]으로 변경이 됩니다. 진행 하시겠습니까?');";
            }
            else if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "2")
            {
                ltlBonsaCheck.Text = "본사주문 완료";
            }
            else if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "Y")
            {
                ltlBonsaCheck.Text = "배송준비 중";
            }
            else if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "Z")
            {
                ltlBonsaCheck.Text = "배송완료";
            }

            if (stopMsg != "")
            {
                lnkSubModify.OnClientClick = "alert(\"" + stopMsg + "\"); return false;";
                lnkSubDelete.OnClientClick = "alert(\"" + stopMsg + "\"); return false;";
            }

            if (Convert.ToInt32(drItemRow["msgCnt"]) > 0)
            {
                // 대리점이 보낸걸 본사가 안 읽은 수 / 본사가 보낸걸 대리점이 안 읽은 수
                lnkMessenger.Text = drItemRow["BonSaReadOk"].ToString() + " / " + drItemRow["DaeRiReadOk"].ToString();
            }
            else
            {
                lnkMessenger.Text = "&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;";
            }
            
            if (Convert.ToInt32(drItemRow["DaeRiReadOk"]) > 0)
            {
                ((Literal)e.Item.FindControl("ltlNumberStyle")).Text = " style=\"background-color: #ffa0a0;\" ";
            }

            lnkMessenger.OnClientClick = "return OpenChating('" + drItemRow["bjhd_date"].ToString() + "','" + drItemRow["bjhd_times"].ToString() + "','" + drItemRow["bjhd_mainbuyer"].ToString() + "','" + drItemRow["bjhd_sample"].ToString() + "', 'ok');";

            string openChat = " onclick=\"OpenChating('" + drItemRow["bjhd_date"].ToString() + "','" + drItemRow["bjhd_times"].ToString() + "','" + drItemRow["bjhd_mainbuyer"].ToString() + "','" + drItemRow["bjhd_sample"].ToString() + "', 'ok')\" ";
            for (int i = 1; i <= 14; i++)
            {
                //((Literal)e.Item.FindControl("ltlOpenChat" + i)).Text = openChat;
            }
            ((Literal)e.Item.FindControl("ltlOpenChat14")).Text = openChat;

            lnkSubView.CommandArgument = drItemRow["bjhd_date"].ToString() + "|" + drItemRow["bjhd_times"].ToString() + "|" + drItemRow["bjhd_mainbuyer"].ToString() + "|" + drItemRow["bjhd_sample"].ToString();
            lnkSubModify.CommandArgument = drItemRow["bjhd_date"].ToString() + "|" + drItemRow["bjhd_times"].ToString() + "|" + drItemRow["bjhd_mainbuyer"].ToString() + "|" + drItemRow["bjhd_sample"].ToString();
            lnkSubDelete.CommandArgument = drItemRow["bjhd_date"].ToString() + "|" + drItemRow["bjhd_times"].ToString() + "|" + drItemRow["bjhd_mainbuyer"].ToString() + "|" + drItemRow["bjhd_sample"].ToString();

            // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
            if (drItemRow["Bjhd_Bonsa_Check"].ToString() == "0" || drItemRow["Bjhd_Bonsa_Check"].ToString() == "1")
            {
                if (drItemRow["Bjhd_CreateSawon"].ToString() == MemberData.GetLoginSID("LoginID")) // 로그인한 사용자가 발주한 내역만 수정/삭제 가능
                {
                }
                else
                {
                    lnkSubModify.Visible = false;
                    lnkSubDelete.Visible = false;
                }
            }
            else
            {   
                lnkSubModify.Visible = false;
                lnkSubDelete.Visible = false;
            }
        }
    }

    protected void lvList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string strList = e.CommandArgument.ToString();

        char[] delimiter = "|".ToCharArray();
        string[] strArray = strList.Trim().Split(delimiter);

        string blju_date = strArray[0];
        string blju_times = strArray[1];
        string blju_mainbuyer = strArray[2];
        string blju_sample = strArray[3];

        string kurename = "";
        string qry = "select * from gblKURE where Kure_Code='" + blju_mainbuyer + "' ";
        DataSet ds = stData.GetDataSet(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            kurename = ds.Tables[0].Rows[0]["Kure_Sangho"].ToString();
        }

        qry = " select Bjhd_Bonsa_Check FROM tblBJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string bjhd_bonsa_check = "";
        if (dsC.Tables[0].Rows.Count > 0)
        {
            bjhd_bonsa_check = dsC.Tables[0].Rows[0][0].ToString();
        }

        string stateMsg = "";
        if (bjhd_bonsa_check == "2")
        {
            stateMsg = "본사주문 완료";
        }
        else if (bjhd_bonsa_check == "Y")
        {
            stateMsg = "배송준비 중";
        }
        else if (bjhd_bonsa_check == "Z")
        {
            stateMsg = "배송완료";
        }

        LinkButton lnkSubView = (LinkButton)e.Item.FindControl("lnkSubView");
        LinkButton lnkSubModify = (LinkButton)e.Item.FindControl("lnkSubModify");
        LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");

        switch (e.CommandName)
        {
            case "subView":

                BindView(blju_date, blju_times, blju_mainbuyer, blju_sample);
                BindViewList();

                this.mvMain.ActiveViewIndex = 2;

                break;

            case "subModify":

                string stopMsg2 = OrderData_common.GetStopCheck(preVal);
                if (stopMsg2 != "")
                {
                    StJavaScript js = new StJavaScript(this.Page, false, true);
                    js.WriteJavascript("alert(\"" + stopMsg2 + "\");");
                }
                else
                {
                    // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
                    if (bjhd_bonsa_check == "0" || bjhd_bonsa_check == "1")
                    {
                        qry = " update tblBJHD set Bjhd_Bonsa_Check = '0' where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주", "발주의뢰중으로 변경", qry);

                        BindCheck(blju_date, blju_times, blju_mainbuyer);
                        BindData(kurename, blju_mainbuyer);
                        BindDataList();

                        WriteDefault();

                        this.btnBaeSong.OnClientClick = "return OpenBaeSong('" + this.hidTotal.ClientID + "','" + this.txtBaeSongOpt.ClientID + "','" + this.hidBaeSong.ClientID + "','" + this.txtBaeSongName.ClientID + "','" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "');";

                        this.lnkChating.OnClientClick = "return OpenChatingCheck('" + this.hidTotal.ClientID + "','" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "', '');";

                        this.mvMain.ActiveViewIndex = 1;
                    }
                    else
                    {
                        StJavaScript js = new StJavaScript(this.Page, false, true);
                        js.WriteJavascript("showMessageToolTip('" + lnkSubModify.ClientID + "', '수정할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.');");
                    }
                }
                
                break;

            case "subDelete":

                string stopMsg3 = OrderData_common.GetStopCheck(preVal);
                if (stopMsg3 != "")
                {
                    StJavaScript js = new StJavaScript(this.Page, false, true);
                    js.WriteJavascript("alert(\"" + stopMsg3 + "\");");
                }
                else
                {
                    // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
                    if (bjhd_bonsa_check == "0" || bjhd_bonsa_check == "1")
                    {
                        qry = " DELETE FROM tblBJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주헤더", "삭제", qry);

                        qry = " DELETE FROM tblBLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주", "삭제", qry);

                        qry = " DELETE FROM tblBJHD0 where bjhd0_date = '" + blju_date + "' and bjhd0_times = '" + blju_times + "' and bjhd0_mainbuyer = '" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주배송지", "삭제", qry);

                        qry = " DELETE FROM tblMESG where Mesg_Date = '" + blju_date + "' and Mesg_Times = '" + blju_times + "' and Mesg_MainBuyer = '" + blju_mainbuyer + "' and Mesg_Sample='" + blju_sample + "' ";
                        stData.GetExecuteNonQry(qry);

                        whis.InsertWork("발주대화방", "삭제", qry);

                        BindList();
                    }
                    else
                    {
                        StJavaScript js = new StJavaScript(this.Page, false, true);
                        js.WriteJavascript("showMessageToolTip('" + lnkSubDelete.ClientID + "', '삭제할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.');");
                    }
                }
                
                break;

            default:
                break;
        }
    }

    protected void lvList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dp = ((ListView)sender).FindControl("dpList") as DataPager;
        dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindList();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ((TextBox)this.ucBljuday.FindControl("txtDate")).Text = DateTime.Now.AddMonths(-1).ToShortDateString();
        ((TextBox)this.ucBljuday1.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();

        this.txtProductList.Text = "";
        this.txtProductDetailList.Text = "";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string qry = " select Dnga_MainName, Dnga_SubName, Dnga_SpecColor from tblDNGA where Dnga_StyleNox='" + this.txtProductList.Text + "' ";
        DataSet dsD = stData.GetDataSet(qry);

        if (dsD.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsD.Tables[0].Rows[0];

            this.txtProductDetailList.Text = dr["Dnga_MainName"].ToString() + ", " + dr["Dnga_SubName"].ToString() + ", " + dr["Dnga_SpecColor"].ToString();
        }

        BindList();
    }
    
    protected void lnkList_Click(object sender, EventArgs e)
    {
        this.mvMain.ActiveViewIndex = 0;
        BindList();
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
    #endregion

    #region 발주의뢰 세부내역
    protected void lvView_LayoutCreated(object sender, EventArgs e)
    {
        st.Kind = "1";// this.hidKind.Value;
        st.GetSizeInfo("tbl");

        ((Literal)this.lvView.FindControl("ltlSize1")).Text = st.SizeName1 + " <br>(" + st.SizeNum1 + ")";
        ((Literal)this.lvView.FindControl("ltlSize2")).Text = st.SizeName2 + " <br>(" + st.SizeNum2 + ")";
        ((Literal)this.lvView.FindControl("ltlSize3")).Text = st.SizeName3 + " <br>(" + st.SizeNum3 + ")";
        ((Literal)this.lvView.FindControl("ltlSize4")).Text = st.SizeName4 + " <br>(" + st.SizeNum4 + ")";
        ((Literal)this.lvView.FindControl("ltlSize5")).Text = st.SizeName5 + " <br>(" + st.SizeNum5 + ")";
        ((Literal)this.lvView.FindControl("ltlSize6")).Text = st.SizeName6 + " <br>(" + st.SizeNum6 + ")";
        ((Literal)this.lvView.FindControl("ltlSize7")).Text = st.SizeName7 + " <br>(" + st.SizeNum7 + ")";
        ((Literal)this.lvView.FindControl("ltlSize8")).Text = st.SizeName8 + " <br>(" + st.SizeNum8 + ")";
        ((Literal)this.lvView.FindControl("ltlSize9")).Text = st.SizeName9 + " <br>(" + st.SizeNum9 + ")";
        ((Literal)this.lvView.FindControl("ltlSize10")).Text = st.SizeName10 + " <br>(" + st.SizeNum10 + ")";
        ((Literal)this.lvView.FindControl("ltlSize11")).Text = st.SizeName11 + " <br>(" + st.SizeNum11 + ")";
        ((Literal)this.lvView.FindControl("ltlSize12")).Text = st.SizeName12 + " <br>(" + st.SizeNum12 + ")";        
    }

    private void BindView(string bljudate, string bljutime, string kurecode, string blju_sample)
    {
        // 당일 거래처의 발주내역이 있으면 가져올것(단, 본사처리가 안된 상태인 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만)
        string whereQry = "";
        whereQry = StCommon.MakeSearchQry("Bjhd_Times", bljutime, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("bjhd_sample", blju_sample, "S", whereQry);

        string qry = " select (select kure_sangho from gblKURE where kure_code = a.Bjhd_MainBuyer) as Bjhd_MainBuyerNm,(select Bjhd2_SongJangNox from tblBJHD2 where isnull(Bjhd2_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd2_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd2_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd2_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd2_SongJangNox,* from tblBJHD a where Bjhd_Date = '" + bljudate + "' and Bjhd_MainBuyer = '" + kurecode + "' " + whereQry + " order by Bjhd_Times desc ";
        
        DataSet ds = stData.GetDataSet(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.txtDateV.Text = ds.Tables[0].Rows[0]["Bjhd_Date"].ToString();
            this.txtTimeV.Text = ds.Tables[0].Rows[0]["Bjhd_Times"].ToString();
            this.txtKureV.Text = ds.Tables[0].Rows[0]["Bjhd_MainBuyerNm"].ToString();
            this.hidKureCodeV.Value = ds.Tables[0].Rows[0]["Bjhd_MainBuyer"].ToString();
            string bjhd_sample = ds.Tables[0].Rows[0]["bjhd_sample"].ToString();

            string baesong = ds.Tables[0].Rows[0]["Bjhd_BaeSong"].ToString();
            string baesongName = ds.Tables[0].Rows[0]["Bjhd_BaeSongName"].ToString();

            if (baesongName == "")
            {
                baesong = "";
            }

            if (baesong == "0")
            {
                this.txtBaeSongOptV.Text = "화물 발송";
            }
            else if (baesong == "1")
            {
                this.txtBaeSongOptV.Text = "로젠 택배";
            }
            else if (baesong == "2")
            {
                this.txtBaeSongOptV.Text = "화물 택배";
            }
            else if (baesong == "3")
            {
                this.txtBaeSongOptV.Text = "기타 발송";
            }
            this.txtBaeSongNameV.Text = ds.Tables[0].Rows[0]["Bjhd_BaeSongName"].ToString();

            this.txtSongJangNoxV.Text = ds.Tables[0].Rows[0]["Bjhd2_SongJangNox"].ToString();

            this.txtEtcV.Text = ds.Tables[0].Rows[0]["Bjhd_Remark"].ToString();
            this.txtNetAmountV.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_NetAmount"]);
            this.txtVatAmountV.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_VatAmount"]);
            this.txtHapAmountV.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_HapAmount"]);
            this.txtFirstDateV.Text = ds.Tables[0].Rows[0]["Bjhd_CreateDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_CreateSawon"].ToString();
            this.txtLastDateV.Text = ds.Tables[0].Rows[0]["Bjhd_ModifyDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_ModifySaWon"].ToString();

            this.lnkChatingV.OnClientClick = "return OpenChatingCheck('" + this.hidTotalV.ClientID + "','" + this.txtDateV.Text.ToString() + "','" + this.txtTimeV.Text.ToString() + "','" + this.hidKureCodeV.Value + "','" + bjhd_sample + "', '');";

            this.lnkKureSpecPrintV.OnClientClick = "return OpenKureSpecPrint('" + this.txtDateV.Text.ToString() + "','" + this.txtTimeV.Text.ToString() + "','" + this.hidKureCodeV.Value + "','" + bjhd_sample + "', '');";
        }
        else
        {
            this.txtDateV.Text = DateTime.Now.ToShortDateString();
            this.txtTimeV.Text = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);
            this.txtKureV.Text = MemberData.GetLoginSID("KureName");
            this.hidKureCodeV.Value = MemberData.GetLoginSID("KureCode");

            this.lnkChatingV.OnClientClick = "return OpenChatingCheck('" + this.hidTotalV.ClientID + "','" + this.txtDateV.Text.ToString() + "','" + this.txtTimeV.Text.ToString() + "','" + this.hidKureCodeV.Value + "','0');";

            this.lnkKureSpecPrintV.OnClientClick = "return OpenKureSpecPrint('" + this.txtDateV.Text.ToString() + "','" + this.txtTimeV.Text.ToString() + "','" + this.hidKureCodeV.Value + "','0');";
        }
    }
    
    private void BindViewList()
    {
        string date = txtDateV.Text;
        string time = txtTimeV.Text;
        string kure = hidKureCodeV.Value;

        string whereQry = " where Blju_MainBuyer = '" + kure + "' ";
        whereQry = StCommon.MakeSearchQry("Blju_Date", date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Blju_Times", time, "S", whereQry);

        string qry = " SELECT isnull((select isnull(Dnga_SubName,'') + ', ' + isnull(Dnga_SpecColor,'') from tblDNGA where Dnga_StyleNox=a.Blju_StyleNox),'') as Blju_StyleName,* FROM tblBLJU a " + whereQry + " ORDER BY Blju_Line ";

        DataSet ds = stData.GetDataSet(qry);

        this.hidTotalV.Value = ds.Tables[0].Rows.Count.ToString();

        this.lvView.DataSource = ds;
        this.lvView.DataBind();

        #region // 합계 바인딩
        qry = " SELECT isnull(sum(blju_qty01),0) as q1,isnull(sum(blju_qty02),0) as q2,isnull(sum(blju_qty03),0) as q3,isnull(sum(blju_qty04),0) as q4,isnull(sum(blju_qty05),0) as q5,isnull(sum(blju_qty06),0) as q6 ";
        qry += " ,isnull(sum(blju_qty07),0) as q7,isnull(sum(blju_qty08),0) as q8,isnull(sum(blju_qty09),0) as q9,isnull(sum(blju_qty10),0) as q10,isnull(sum(blju_qty11),0) as q11,isnull(sum(blju_qty12),0) as q12 ";
        qry += " FROM tblBLJU a " + whereQry + " ";
        DataSet dsSum = stData.GetDataSet(qry);

        DataRow dr = dsSum.Tables[0].Rows[0];

        double sum1 = StCommon.ToDouble(dr["q1"].ToString(), 0);
        double sum2 = StCommon.ToDouble(dr["q2"].ToString(), 0);
        double sum3 = StCommon.ToDouble(dr["q3"].ToString(), 0);
        double sum4 = StCommon.ToDouble(dr["q4"].ToString(), 0);
        double sum5 = StCommon.ToDouble(dr["q5"].ToString(), 0);
        double sum6 = StCommon.ToDouble(dr["q6"].ToString(), 0);
        double sum7 = StCommon.ToDouble(dr["q7"].ToString(), 0);
        double sum8 = StCommon.ToDouble(dr["q8"].ToString(), 0);
        double sum9 = StCommon.ToDouble(dr["q9"].ToString(), 0);
        double sum10 = StCommon.ToDouble(dr["q10"].ToString(), 0);
        double sum11 = StCommon.ToDouble(dr["q11"].ToString(), 0);
        double sum12 = StCommon.ToDouble(dr["q12"].ToString(), 0);
        try
        {
            int k = 1;
            while (k <= 12)
            {
                ((Literal)this.lvView.FindControl("ltlSum" + k)).Text = GetAmountFormat(dr["q" + k].ToString());

                k++;
            }
        }
        catch { }

        try
        {
            ((Literal)this.lvView.FindControl("ltlSumTotal")).Text = GetAmountFormat(sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + sum7 + sum8 + sum9 + sum10 + sum11 + sum12);
        }
        catch { }

        double JustAmountTotal = 0;
        for (int i = 0; i < lvView.Items.Count; i++)
        {
            Literal ltlJustAmount = (Literal)lvView.Items[i].FindControl("ltlJustAmount");

            JustAmountTotal += StCommon.ToDouble(ltlJustAmount.Text.Replace(",", ""), 0);
        }

        try
        {
            ((Literal)this.lvView.FindControl("ltlSumMoney")).Text = GetAmountFormat(JustAmountTotal);
        }
        catch { }
        #endregion
    }
    #endregion

    public string GetRound(object obj)
    {
        return GetAmountFormat(Math.Round(Convert.ToDouble(obj.ToString()) * 100, 2).ToString());
    }
}