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

public partial class Mobile_Common_BaeSong : System.Web.UI.Page
{
    private string preVal = "";

    private string baeSongOptID = "";
    private string baeSongID = "";
    private string baeSongNameID = "";

    private string blju_date = "";
    private string blju_times = "";
    private string blju_mainbuyer = "";
    private string blju_sample = "";

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
            baeSongOptID = Server.HtmlEncode(Request["baeSongOptID"].Trim());
            baeSongID = Server.HtmlEncode(Request["baeSongID"].Trim());
            baeSongNameID = Server.HtmlEncode(Request["baeSongNameID"].Trim());
        }
        catch { }

        try
        {
            blju_date = Server.HtmlEncode(Request["blju_date"].Trim());
            blju_times = Server.HtmlEncode(Request["blju_times"].Trim());
            blju_mainbuyer = Server.HtmlEncode(Request["blju_mainbuyer"].Trim());
            blju_sample = Server.HtmlEncode(Request["blju_sample"].Trim());
        }
        catch { }

        this.btnConfirm.OnClientClick = "return CheckBaeSong('" + baeSongOptID + "', '" + baeSongID + "', '" + baeSongNameID + "');";

        if (!IsPostBack)
        {
            // 현재 발주상태 체크
            string nowBonsaCheck = StCommon.GetBonsaCheck(preVal, blju_mainbuyer, blju_date, blju_times);
            if (nowBonsaCheck != "" && nowBonsaCheck != "0")
            {
                string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.ShowAlertMessage("현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.", "self.close();");
            }

            string whereQry = " where Bjhd_MainBuyer = '" + blju_mainbuyer + "' ";
            whereQry = StCommon.MakeSearchQry("Bjhd_Date", blju_date, "S", whereQry);
            whereQry = StCommon.MakeSearchQry("Bjhd_Times", blju_times, "S", whereQry);
            whereQry = StCommon.MakeSearchQry("Bjhd_Sample", blju_sample, "S", whereQry);

            string qry = " select Bjhd_BaeSong, Bjhd_BaeSongName from " + preVal + "BJHD " + whereQry;
            DataSet ds = stData.GetDataSet(qry);

            string recentBaesong = "";
            string recentBaesongName = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                recentBaesong = ds.Tables[0].Rows[0]["Bjhd_BaeSong"].ToString();
                recentBaesongName = ds.Tables[0].Rows[0]["Bjhd_BaeSongName"].ToString();
            }

            if (recentBaesongName != "")
            {
                this.rbBaeSong1.Checked = (recentBaesong == "0") ? true : false;
                this.rbBaeSong2.Checked = (recentBaesong == "1") ? true : false;
                this.rbBaeSong3.Checked = (recentBaesong == "2") ? true : false;
                this.rbBaeSong4.Checked = (recentBaesong == "3") ? true : false;

                if (this.rbBaeSong1.Checked)
                {
                    EnableSet("0");
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select * from " + preVal + "BJHD0 where ");
                    sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') = '' AND ISNULL(Bjhd0_ZipCode_Send, '') = '' ");

                    ds = stData.GetDataSet(sb.ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtBaeSongName1.Text = ds.Tables[0].Rows[0]["Bjhd0_Name"].ToString();
                        this.txtBaeSongName1Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Name_Send"].ToString();
                        this.txtBaeSongTel1Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Tel1_Send"].ToString();

                        this.chkSunbulPay.Checked = StCommon.StringToTrue(ds.Tables[0].Rows[0]["Bjhd0_SunbulPay"].ToString(), "1");
                    }
                }
                else if (this.rbBaeSong2.Checked || this.rbBaeSong3.Checked)
                {
                    if (this.rbBaeSong2.Checked)
                        EnableSet("1");
                    if (this.rbBaeSong3.Checked)
                        EnableSet("2");

                    this.rblBaeSongOption.SelectedValue = "새로";
                    this.rblBaeSongSendOption.SelectedValue = "새로";

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select * from " + preVal + "BJHD0 where ");
                    sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') > '' AND ISNULL(Bjhd0_ZipCode_Send, '') > '' ");

                    ds = stData.GetDataSet(sb.ToString());
                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtBaeSongName2.Text = ds.Tables[0].Rows[0]["Bjhd0_Name"].ToString();
                        this.txtZipcode.Text = ds.Tables[0].Rows[0]["Bjhd0_ZipCode"].ToString();
                        this.txtAddress1.Text = ds.Tables[0].Rows[0]["Bjhd0_Address1"].ToString();
                        this.txtAddress2.Text = ds.Tables[0].Rows[0]["Bjhd0_Address2"].ToString();
                        this.txtTel1.Text = ds.Tables[0].Rows[0]["Bjhd0_Tel1"].ToString();
                        this.txtTel2.Text = ds.Tables[0].Rows[0]["Bjhd0_Tel2"].ToString();
                        this.txtRemark.Text = ds.Tables[0].Rows[0]["Bjhd0_Remark"].ToString();
                        this.txtBaeSongName2Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Name_Send"].ToString();
                        this.txtZipcodeSend.Text = ds.Tables[0].Rows[0]["Bjhd0_ZipCode_Send"].ToString();
                        this.txtAddress1Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Address1_Send"].ToString();
                        this.txtAddress2Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Address2_Send"].ToString();
                        this.txtTel1Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Tel1_Send"].ToString();
                        this.txtTel2Send.Text = ds.Tables[0].Rows[0]["Bjhd0_Tel2_Send"].ToString();

                        this.chkSunbulPay.Checked = StCommon.StringToTrue(ds.Tables[0].Rows[0]["Bjhd0_SunbulPay"].ToString(), "1");
                    }
                }
                else if (this.rbBaeSong4.Checked)
                {
                    EnableSet("3");

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select * from " + preVal + "BJHD0 where ");
                    sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') = '' AND ISNULL(Bjhd0_ZipCode_Send, '') = '' ");

                    ds = stData.GetDataSet(sb.ToString());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtBaeSongName4.Text = ds.Tables[0].Rows[0]["Bjhd0_Name"].ToString();

                        this.chkSunbulPay.Checked = StCommon.StringToTrue(ds.Tables[0].Rows[0]["Bjhd0_SunbulPay"].ToString(), "1");
                    }
                }
            }
            else
            {
                EnableSet("0");

                string kurename = MemberData.GetLoginSID("KureName");
                string kurecode = MemberData.GetLoginSID("KureCode");

                this.txtBaeSongName1.Text = "경동화물";
                this.txtBaeSongName1Send.Text = kurename;
            }

            this.ddlAddrList.Visible = false;

            this.chkSunbulPay.Text = "이 송장은 \"선불배송\"으로 처리합니다.";
        }
    }

    protected void rbBaeSong1_CheckedChanged(object sender, EventArgs e)
    {
        EnableSet("0");

        this.rblBaeSongOption.SelectedValue = "새로";
        this.rblBaeSongSendOption.SelectedValue = "새로";

        this.txtBaeSongName2.Text = "";
        this.txtZipcode.Text = "";
        this.txtAddress1.Text = "";
        this.txtAddress2.Text = "";
        this.txtTel1.Text = "";
        this.txtTel2.Text = "";
        this.txtRemark.Text = "";
        this.txtBaeSongName2Send.Text = "";
        this.txtZipcodeSend.Text = "";
        this.txtAddress1Send.Text = "";
        this.txtAddress2Send.Text = "";
        this.txtTel1Send.Text = "";
        this.txtTel2Send.Text = "";

        this.txtBaeSongName4.Text = "";
    }

    protected void rbBaeSong2_CheckedChanged(object sender, EventArgs e)
    {
        EnableSet("1");

        this.txtBaeSongName1.Text = "";
        this.txtBaeSongName1Send.Text = "";
        this.txtBaeSongTel1Send.Text = "";

        this.rblBaeSongOption.SelectedValue = "새로";
        this.rblBaeSongSendOption.SelectedValue = "새로";

        this.txtBaeSongName2.Text = "";
        this.txtZipcode.Text = "";
        this.txtAddress1.Text = "";
        this.txtAddress2.Text = "";
        this.txtTel1.Text = "";
        this.txtTel2.Text = "";
        this.txtRemark.Text = "";
        this.txtBaeSongName2Send.Text = "";
        this.txtZipcodeSend.Text = "";
        this.txtAddress1Send.Text = "";
        this.txtAddress2Send.Text = "";
        this.txtTel1Send.Text = "";
        this.txtTel2Send.Text = "";

        this.txtBaeSongName4.Text = "";
    }

    protected void rbBaeSong3_CheckedChanged(object sender, EventArgs e)
    {
        EnableSet("2");

        this.txtBaeSongName1.Text = "";
        this.txtBaeSongName1Send.Text = "";
        this.txtBaeSongTel1Send.Text = "";

        this.rblBaeSongOption.SelectedValue = "새로";
        this.rblBaeSongSendOption.SelectedValue = "새로";

        this.txtBaeSongName2.Text = "";
        this.txtZipcode.Text = "";
        this.txtAddress1.Text = "";
        this.txtAddress2.Text = "";
        this.txtTel1.Text = "";
        this.txtTel2.Text = "";
        this.txtRemark.Text = "";
        this.txtBaeSongName2Send.Text = "";
        this.txtZipcodeSend.Text = "";
        this.txtAddress1Send.Text = "";
        this.txtAddress2Send.Text = "";
        this.txtTel1Send.Text = "";
        this.txtTel2Send.Text = "";

        this.txtBaeSongName4.Text = "";
    }

    protected void rbBaeSong4_CheckedChanged(object sender, EventArgs e)
    {
        EnableSet("3");

        this.txtBaeSongName1.Text = "";
        this.txtBaeSongName1Send.Text = "";
        this.txtBaeSongTel1Send.Text = "";

        this.rblBaeSongOption.SelectedValue = "새로";
        this.rblBaeSongSendOption.SelectedValue = "새로";

        this.txtBaeSongName2.Text = "";
        this.txtZipcode.Text = "";
        this.txtAddress1.Text = "";
        this.txtAddress2.Text = "";
        this.txtTel1.Text = "";
        this.txtTel2.Text = "";
        this.txtRemark.Text = "";
        this.txtBaeSongName2Send.Text = "";
        this.txtZipcodeSend.Text = "";
        this.txtAddress1Send.Text = "";
        this.txtAddress2Send.Text = "";
        this.txtTel1Send.Text = "";
        this.txtTel2Send.Text = "";
    }

    private void EnableSet(string baesong)
    {
        if (baesong == "0")
        {
            this.txtBaeSongName1.Enabled = true;
            this.txtBaeSongName1Send.Enabled = true;
            this.txtBaeSongTel1Send.Enabled = true;
            this.pnBaeSong.Enabled = false;
            this.txtBaeSongName4.Enabled = false;
        }
        else if (baesong == "1")
        {
            this.txtBaeSongName1.Enabled = false;
            this.txtBaeSongName1Send.Enabled = false;
            this.txtBaeSongTel1Send.Enabled = false;
            this.pnBaeSong.Enabled = true;
            this.txtBaeSongName4.Enabled = false;
        }
        else if (baesong == "2")
        {
            this.txtBaeSongName1.Enabled = false;
            this.txtBaeSongName1Send.Enabled = false;
            this.txtBaeSongTel1Send.Enabled = false;
            this.pnBaeSong.Enabled = true;
            this.txtBaeSongName4.Enabled = false;
        }
        else if (baesong == "3")
        {
            this.txtBaeSongName1.Enabled = false;
            this.txtBaeSongName1Send.Enabled = false;
            this.txtBaeSongTel1Send.Enabled = false;
            this.pnBaeSong.Enabled = false;
            this.txtBaeSongName4.Enabled = true;
        }
    }

    protected void rblBaeSongOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        string kurename = MemberData.GetLoginSID("KureName");
        string kurecode = MemberData.GetLoginSID("KureCode");

        DataSet ds = null;

        this.btnSearchAddr.Visible = false;

        switch (this.rblBaeSongOption.SelectedValue)
        {
            case "기본":

                this.rblBaeSongSendOption.SelectedValue = "본사";
                ChangeBaeSongSendOption(this.rblBaeSongSendOption.SelectedValue);

                this.txtBaeSongName2.Text = kurename;

                string qry = " SELECT * from gblKURE where kure_code = '" + kurecode + "' ";
                DataSet dsK = stData.GetDataSet(qry);

                if (dsK.Tables[0].Rows.Count > 0)
                {
                    this.txtZipcode.Text = dsK.Tables[0].Rows[0]["Kure_ZipCode"].ToString();
                    this.txtAddress1.Text = dsK.Tables[0].Rows[0]["Kure_Address1"].ToString();
                    this.txtAddress2.Text = dsK.Tables[0].Rows[0]["Kure_Address2"].ToString();
                    this.txtTel1.Text = dsK.Tables[0].Rows[0]["Kure_Tel"].ToString();
                    this.txtTel2.Text = dsK.Tables[0].Rows[0]["Kure_Fax"].ToString();
                }

                this.ddlAddrList.Visible = false;

                break;

            case "새로":

                this.rblBaeSongSendOption.SelectedValue = "새로";
                ChangeBaeSongSendOption(this.rblBaeSongSendOption.SelectedValue);

                this.btnSearchAddr.Visible = true;

                this.txtBaeSongName2.Text = "";
                this.txtZipcode.Text = "";
                this.txtAddress1.Text = "";
                this.txtAddress2.Text = "";
                this.txtTel1.Text = "";
                this.txtTel2.Text = "";
                this.txtRemark.Text = "";
                this.txtBaeSongName2Send.Text = "";
                this.txtZipcodeSend.Text = "";
                this.txtAddress1Send.Text = "";
                this.txtAddress2Send.Text = "";
                this.txtTel1Send.Text = "";
                this.txtTel2Send.Text = "";

                this.ddlAddrList.Visible = false;

                break;

            case "최근":

                qry = " select top 5 * from " + preVal + "BJHD0 where bjhd0_mainbuyer='" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') > '' AND ISNULL(Bjhd0_ZipCode_Send, '') > '' order by Bjhd0_Date desc, Bjhd0_Times desc ";
                ds = stData.GetDataSet(qry);

                this.ddlAddrList.Items.Clear();
                this.ddlAddrList.Items.Add(new ListItem("====== 선택하세요 ======", ""));
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++) {
                    string name = ds.Tables[0].Rows[k]["Bjhd0_Name"].ToString();
                    string zipcode = ds.Tables[0].Rows[k]["Bjhd0_ZipCode"].ToString();
                    string address1 = ds.Tables[0].Rows[k]["Bjhd0_Address1"].ToString();
                    string address2 = ds.Tables[0].Rows[k]["Bjhd0_Address2"].ToString();
                    string tel1 = ds.Tables[0].Rows[k]["Bjhd0_Tel1"].ToString();
                    string tel2 = ds.Tables[0].Rows[k]["Bjhd0_Tel2"].ToString();
                    string remark = ds.Tables[0].Rows[k]["Bjhd0_Remark"].ToString();
                    string nameSend = ds.Tables[0].Rows[k]["Bjhd0_Name_Send"].ToString();
                    string zipcodeSend = ds.Tables[0].Rows[k]["Bjhd0_ZipCode_Send"].ToString();
                    string address1Send = ds.Tables[0].Rows[k]["Bjhd0_Address1_Send"].ToString();
                    string address2Send = ds.Tables[0].Rows[k]["Bjhd0_Address2_Send"].ToString();
                    string tel1Send = ds.Tables[0].Rows[k]["Bjhd0_Tel1_Send"].ToString();
                    string tel2Send = ds.Tables[0].Rows[k]["Bjhd0_Tel2_Send"].ToString();

                    string text = name + " " + zipcode + " " + address1 + " " + address2 + " " + tel1 + " " + tel2 + " " + remark + " " + nameSend + " " + zipcodeSend + " " + address1Send + " " + address2Send + " " + tel1Send + " " + tel2Send;
                    string val = name + "||" + zipcode + "||" + address1 + "||" + address2 + "||" + tel1 + "||" + tel2 + "||" + remark + "||" + nameSend + "||" + zipcodeSend + "||" + address1Send + "||" + address2Send + "||" + tel1Send + "||" + tel2Send;
                    
                    this.ddlAddrList.Items.Add(new ListItem(text, val));
                }

                this.ddlAddrList.Visible = true;

                break;

            case "배송지":

                qry = " select top 50 * from " + preVal + "BJHD1 where bjhd1_mainbuyer='" + blju_mainbuyer + "' order by Bjhd1_CreateDateTimes desc ";
                
                ds = stData.GetDataSet(qry);

                this.ddlAddrList.Items.Clear();
                this.ddlAddrList.Items.Add(new ListItem("====== 선택하세요 ======", ""));
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string name = ds.Tables[0].Rows[k]["Bjhd1_Name"].ToString();
                    string zipcode = ds.Tables[0].Rows[k]["Bjhd1_ZipCode"].ToString();
                    string address1 = ds.Tables[0].Rows[k]["Bjhd1_Address1"].ToString();
                    string address2 = ds.Tables[0].Rows[k]["Bjhd1_Address2"].ToString();
                    string tel1 = ds.Tables[0].Rows[k]["Bjhd1_Tel1"].ToString();
                    string tel2 = ds.Tables[0].Rows[k]["Bjhd1_Tel2"].ToString();
                    string remark = ds.Tables[0].Rows[k]["Bjhd1_Remark"].ToString();
                    string nameSend = ds.Tables[0].Rows[k]["Bjhd1_Name_Send"].ToString();
                    string zipcodeSend = ds.Tables[0].Rows[k]["Bjhd1_ZipCode_Send"].ToString();
                    string address1Send = ds.Tables[0].Rows[k]["Bjhd1_Address1_Send"].ToString();
                    string address2Send = ds.Tables[0].Rows[k]["Bjhd1_Address2_Send"].ToString();
                    string tel1Send = ds.Tables[0].Rows[k]["Bjhd1_Tel1_Send"].ToString();
                    string tel2Send = ds.Tables[0].Rows[k]["Bjhd1_Tel2_Send"].ToString();

                    string text = name + " " + zipcode + " " + address1 + " " + address2 + " " + tel1 + " " + tel2 + " " + remark + " " + nameSend + " " + zipcodeSend + " " + address1Send + " " + address2Send + " " + tel1Send + " " + tel2Send;
                    string val = name + "||" + zipcode + "||" + address1 + "||" + address2 + "||" + tel1 + "||" + tel2 + "||" + remark + "||" + nameSend + "||" + zipcodeSend + "||" + address1Send + "||" + address2Send + "||" + tel1Send + "||" + tel2Send;

                    this.ddlAddrList.Items.Add(new ListItem(text, val));
                }
                
                this.ddlAddrList.Visible = true;

                break;
        }
    }

    protected void ddlAddrList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selAddr = this.ddlAddrList.SelectedValue;

        if (selAddr.Trim() != "")
        {
            string[] delimiter = { "||" };
            string[] strArray = selAddr.Split(delimiter, StringSplitOptions.None);

            try
            {
                this.txtBaeSongName2.Text = strArray[0];
                this.txtZipcode.Text = strArray[1];
                this.txtAddress1.Text = strArray[2];
                this.txtAddress2.Text = strArray[3];
                this.txtTel1.Text = strArray[4];
                this.txtTel2.Text = strArray[5];
                this.txtRemark.Text = strArray[6];
                this.txtBaeSongName2Send.Text = strArray[7];
                this.txtZipcodeSend.Text = strArray[8];
                this.txtAddress1Send.Text = strArray[9];
                this.txtAddress2Send.Text = strArray[10];
                this.txtTel1Send.Text = strArray[11];
                this.txtTel2Send.Text = strArray[12];
            }
            catch { }
        }
    }

    private void ChangeBaeSongSendOption(string baeSongSendOption)
    {
        string kurename = MemberData.GetLoginSID("KureName");
        string kurecode = MemberData.GetLoginSID("KureCode");

        string qry = "";
        DataSet dsK = null;

        switch (baeSongSendOption)
        {
            case "발주처":

                this.txtBaeSongName2Send.Text = kurename;

                qry = " SELECT * from gblKURE where kure_code = '" + kurecode + "' ";
                dsK = stData.GetDataSet(qry);

                if (dsK.Tables[0].Rows.Count > 0)
                {
                    this.txtZipcodeSend.Text = dsK.Tables[0].Rows[0]["Kure_ZipCode"].ToString();
                    this.txtAddress1Send.Text = dsK.Tables[0].Rows[0]["Kure_Address1"].ToString();
                    this.txtAddress2Send.Text = dsK.Tables[0].Rows[0]["Kure_Address2"].ToString();
                    this.txtTel1Send.Text = dsK.Tables[0].Rows[0]["Kure_Tel"].ToString();
                    this.txtTel2Send.Text = dsK.Tables[0].Rows[0]["Kure_Fax"].ToString();
                }

                break;

            case "본사":

                this.txtBaeSongName2Send.Text = kurename;

                qry = " SELECT * from gblKURE where kure_code = 'ZX-999' ";
                dsK = stData.GetDataSet(qry);

                if (dsK.Tables[0].Rows.Count > 0)
                {
                    this.txtZipcodeSend.Text = dsK.Tables[0].Rows[0]["Kure_ZipCode"].ToString();
                    this.txtAddress1Send.Text = dsK.Tables[0].Rows[0]["Kure_Address1"].ToString();
                    this.txtAddress2Send.Text = dsK.Tables[0].Rows[0]["Kure_Address2"].ToString();
                    this.txtTel1Send.Text = dsK.Tables[0].Rows[0]["Kure_Tel"].ToString();
                    this.txtTel2Send.Text = dsK.Tables[0].Rows[0]["Kure_Fax"].ToString();
                }

                break;

            case "새로":

                this.btnSearchAddrSend.Visible = true;

                this.txtBaeSongName2Send.Text = "";
                this.txtZipcodeSend.Text = "";
                this.txtAddress1Send.Text = "";
                this.txtAddress2Send.Text = "";
                this.txtTel1Send.Text = "";
                this.txtTel2Send.Text = "";

                break;
        }
    }

    protected void rblBaeSongSendOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnSearchAddrSend.Visible = false;
        
        ChangeBaeSongSendOption(this.rblBaeSongSendOption.SelectedValue);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        WorkHistory whis = new WorkHistory();

        string qry = "";

        if (this.rbBaeSong1.Checked || this.rbBaeSong4.Checked)
        {
            string name = this.txtBaeSongName1.Text.ToString();
            string zipcode = "";
            string address1 = "";
            string address2 = "";
            string tel1 = "";
            string tel2 = "";
            string remark = "";
            string nameSend = this.txtBaeSongName1Send.Text.ToString();
            string zipcodeSend = "";
            string address1Send = "";
            string address2Send = "";
            string tel1Send = this.txtBaeSongTel1Send.Text.ToString();
            string tel2Send = "";

            string sunbulPay = StCommon.TrueToString(this.chkSunbulPay.Checked, "1", "");
            
            if (this.rbBaeSong4.Checked)
            {
                name = this.txtBaeSongName4.Text.ToString();
                nameSend = "";
                tel1Send = "";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample, '')='" + blju_sample + "' ");

            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지 삭제(옵션변경)", sb.ToString());

            sb = new StringBuilder();
            sb.Append(" INSERT INTO " + preVal + "BJHD0 (Bjhd0_Date,Bjhd0_Times,Bjhd0_MainBuyer,Bjhd0_Sample,Bjhd0_Name,Bjhd0_ZipCode,Bjhd0_Address1,Bjhd0_Address2,Bjhd0_Tel1,Bjhd0_Tel2,Bjhd0_Remark,Bjhd0_Name_Send,Bjhd0_ZipCode_Send,Bjhd0_Address1_Send,Bjhd0_Address2_Send,Bjhd0_Tel1_Send,Bjhd0_Tel2_Send,Bjhd0_SunbulPay) ");
            sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + name + "','" + zipcode + "','" + address1 + "','" + address2 + "','" + tel1 + "','" + tel2 + "','" + remark + "','" + nameSend + "','" + zipcodeSend + "','" + address1Send + "','" + address2Send + "','" + tel1Send + "','" + tel2Send + "','" + sunbulPay + "') ");
            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지", sb.ToString());

            /*
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample, '')='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') = '' AND ISNULL(Bjhd0_ZipCode_Send, '') = '' ");

            DataSet ds = stData.GetDataSet(sb.ToString());

            if (ds.Tables[0].Rows.Count == 0)
            {
                sb = new StringBuilder();
                sb.Append(" INSERT INTO " + preVal + "BJHD0 (Bjhd0_Date,Bjhd0_Times,Bjhd0_MainBuyer,Bjhd0_Sample,Bjhd0_Name,Bjhd0_ZipCode,Bjhd0_Address1,Bjhd0_Address2,Bjhd0_Tel1,Bjhd0_Tel2,Bjhd0_Remark,Bjhd0_Name_Send,Bjhd0_ZipCode_Send,Bjhd0_Address1_Send,Bjhd0_Address2_Send,Bjhd0_Tel1_Send,Bjhd0_Tel2_Send,Bjhd0_SunbulPay) ");
                sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + name + "','" + zipcode + "','" + address1 + "','" + address2 + "','" + tel1 + "','" + tel2 + "','" + remark + "','" + nameSend + "','" + zipcodeSend + "','" + address1Send + "','" + address2Send + "','" + tel1Send + "','" + tel2Send + "','" + sunbulPay + "') ");
                stData.GetExecuteNonQry(sb.ToString());

                whis.InsertWork("배송지", "최근배송지", sb.ToString());
            }
            else
            {       
                sb = new StringBuilder();
                sb.Append(" UPDATE " + preVal + "BJHD0 ");
                sb.Append(" SET Bjhd0_Name = '" + name + "',Bjhd0_ZipCode = '" + zipcode + "',Bjhd0_Address1 = '" + address1 + "',Bjhd0_Address2 = '" + address2 + "',Bjhd0_Tel1 = '" + tel1 + "',Bjhd0_Tel2 = '" + tel2 + "',Bjhd0_Remark = '" + remark + "',Bjhd0_Name_Send = '" + nameSend + "',Bjhd0_ZipCode_Send = '" + zipcodeSend + "',Bjhd0_Address1_Send = '" + address1Send + "',Bjhd0_Address2_Send = '" + address2Send + "',Bjhd0_Tel1_Send = '" + tel1Send + "',Bjhd0_Tel2_Send = '" + tel2Send + "',Bjhd0_SunbulPay = '" + sunbulPay + "' ");
                sb.Append(" WHERE bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') = '' AND ISNULL(Bjhd0_ZipCode_Send, '') = '' ");
                stData.GetExecuteNonQry(sb.ToString());

                whis.InsertWork("배송지", "최근배송지", sb.ToString());
            }

            sb = new StringBuilder();
            sb.Append(" delete from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' ");

            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지 삭제(옵션변경)", sb.ToString());
            */
        }
        else if (this.rbBaeSong2.Checked || this.rbBaeSong3.Checked)
        {
            string name = this.txtBaeSongName2.Text.ToString();
            string zipcode = this.txtZipcode.Text.ToString();
            string address1 = this.txtAddress1.Text.ToString();
            string address2 = this.txtAddress2.Text.ToString();
            string tel1 = this.txtTel1.Text.ToString();
            string tel2 = this.txtTel2.Text.ToString();
            string remark = this.txtRemark.Text.ToString();
            string nameSend = this.txtBaeSongName2Send.Text.ToString();
            string zipcodeSend = this.txtZipcodeSend.Text.ToString();
            string address1Send = this.txtAddress1Send.Text.ToString();
            string address2Send = this.txtAddress2Send.Text.ToString();
            string tel1Send = this.txtTel1Send.Text.ToString();
            string tel2Send = this.txtTel2Send.Text.ToString();

            string sunbulPay = StCommon.TrueToString(this.chkSunbulPay.Checked, "1", "");

            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' ");

            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지 삭제(옵션변경)", sb.ToString());

            sb = new StringBuilder();
            sb.Append(" INSERT INTO " + preVal + "BJHD0 (Bjhd0_Date,Bjhd0_Times,Bjhd0_MainBuyer,Bjhd0_Sample,Bjhd0_Name,Bjhd0_ZipCode,Bjhd0_Address1,Bjhd0_Address2,Bjhd0_Tel1,Bjhd0_Tel2,Bjhd0_Remark,Bjhd0_Name_Send,Bjhd0_ZipCode_Send,Bjhd0_Address1_Send,Bjhd0_Address2_Send,Bjhd0_Tel1_Send,Bjhd0_Tel2_Send,Bjhd0_SunbulPay) ");
            sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + name + "','" + zipcode + "','" + address1 + "','" + address2 + "','" + tel1 + "','" + tel2 + "','" + remark + "','" + nameSend + "','" + zipcodeSend + "','" + address1Send + "','" + address2Send + "','" + tel1Send + "','" + tel2Send + "','" + sunbulPay + "') ");
            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지", sb.ToString());

            /*
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') > '' AND ISNULL(Bjhd0_ZipCode_Send, '') > '' ");

            DataSet ds = stData.GetDataSet(sb.ToString());

            if (ds.Tables[0].Rows.Count == 0)
            {
                sb = new StringBuilder();
                sb.Append(" INSERT INTO " + preVal + "BJHD0 (Bjhd0_Date,Bjhd0_Times,Bjhd0_MainBuyer,Bjhd0_Sample,Bjhd0_Name,Bjhd0_ZipCode,Bjhd0_Address1,Bjhd0_Address2,Bjhd0_Tel1,Bjhd0_Tel2,Bjhd0_Remark,Bjhd0_Name_Send,Bjhd0_ZipCode_Send,Bjhd0_Address1_Send,Bjhd0_Address2_Send,Bjhd0_Tel1_Send,Bjhd0_Tel2_Send,Bjhd0_SunbulPay) ");
                sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + name + "','" + zipcode + "','" + address1 + "','" + address2 + "','" + tel1 + "','" + tel2 + "','" + remark + "','" + nameSend + "','" + zipcodeSend + "','" + address1Send + "','" + address2Send + "','" + tel1Send + "','" + tel2Send + "','" + sunbulPay + "') ");
                stData.GetExecuteNonQry(sb.ToString());

                whis.InsertWork("배송지", "최근배송지", sb.ToString());
            }
            else
            {
                sb = new StringBuilder();
                sb.Append(" UPDATE " + preVal + "BJHD0 ");
                sb.Append(" SET Bjhd0_Name = '" + name + "',Bjhd0_ZipCode = '" + zipcode + "',Bjhd0_Address1 = '" + address1 + "',Bjhd0_Address2 = '" + address2 + "',Bjhd0_Tel1 = '" + tel1 + "',Bjhd0_Tel2 = '" + tel2 + "',Bjhd0_Remark = '" + remark + "',Bjhd0_Name_Send = '" + nameSend + "',Bjhd0_ZipCode_Send = '" + zipcodeSend + "',Bjhd0_Address1_Send = '" + address1Send + "',Bjhd0_Address2_Send = '" + address2Send + "',Bjhd0_Tel1_Send = '" + tel1Send + "',Bjhd0_Tel2_Send = '" + tel2Send + "',Bjhd0_SunbulPay = '" + sunbulPay + "' ");
                sb.Append(" WHERE bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') > '' AND ISNULL(Bjhd0_ZipCode_Send, '') > '' ");
                stData.GetExecuteNonQry(sb.ToString());

                whis.InsertWork("배송지", "최근배송지", sb.ToString());
            }

            sb = new StringBuilder();
            sb.Append(" delete from " + preVal + "BJHD0 where ");
            sb.Append(" bjhd0_date='" + blju_date + "' and bjhd0_times='" + blju_times + "' and bjhd0_mainbuyer='" + blju_mainbuyer + "' and ISNULL(bjhd0_sample,'')='" + blju_sample + "' AND ISNULL(Bjhd0_ZipCode, '') = '' AND ISNULL(Bjhd0_ZipCode_Send, '') = '' ");

            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("배송지", "최근배송지 삭제(옵션변경)", sb.ToString());
            */

            if (this.cbSaveAddr.Checked)
            {
                sb = new StringBuilder();
                sb.Append(" select * from " + preVal + "BJHD1 where ");
                sb.Append(" Bjhd1_MainBuyer='" + blju_mainbuyer + "' and Bjhd1_Name='" + name + "' and Bjhd1_ZipCode='" + zipcode + "' and Bjhd1_Address1='" + address1 + "' and Bjhd1_Address2='" + address2 + "' ");

                DataSet ds = stData.GetDataSet(sb.ToString());

                if (ds.Tables[0].Rows.Count == 0)
                {
                    string createDate = DateTime.Now.ToShortDateString() + " " + String.Format("{0:HH:mm:ss:fff}", DateTime.Now);

                    sb = new StringBuilder();
                    sb.Append(" INSERT INTO " + preVal + "BJHD1 (Bjhd1_CreateDateTimes,Bjhd1_MainBuyer,Bjhd1_Name,Bjhd1_ZipCode,Bjhd1_Address1,Bjhd1_Address2,Bjhd1_Tel1,Bjhd1_Tel2,Bjhd1_Remark,Bjhd1_Name_Send,Bjhd1_ZipCode_Send,Bjhd1_Address1_Send,Bjhd1_Address2_Send,Bjhd1_Tel1_Send,Bjhd1_Tel2_Send) ");
                    sb.Append(" VALUES('" + createDate + "','" + blju_mainbuyer + "','" + name + "','" + zipcode + "','" + address1 + "','" + address2 + "','" + tel1 + "','" + tel2 + "','" + remark + "','" + nameSend + "','" + zipcodeSend + "','" + address1Send + "','" + address2Send + "','" + tel1Send + "','" + tel2Send + "') ");
                    stData.GetExecuteNonQry(sb.ToString());

                    whis.InsertWork("배송지", "배송지목록", sb.ToString());

                    qry = " select * from " + preVal + "BJHD1 where bjhd1_mainbuyer='" + blju_mainbuyer + "' order by Bjhd1_CreateDateTimes desc ";
                    ds = stData.GetDataSet(qry);

                    if (ds.Tables[0].Rows.Count > 50)
                    {
                        qry = " select top 1 * from " + preVal + "BJHD1 where bjhd1_mainbuyer='" + blju_mainbuyer + "' order by Bjhd1_CreateDateTimes ";
                        DataSet dsD = stData.GetDataSet(qry);

                        if (dsD.Tables[0].Rows.Count > 0)
                        {
                            qry = " DELETE FROM " + preVal + "BJHD1 where bjhd1_mainbuyer='" + dsD.Tables[0].Rows[0]["Bjhd1_MainBuyer"].ToString() + "' and Bjhd1_CreateDateTimes = '" + dsD.Tables[0].Rows[0]["Bjhd1_CreateDateTimes"].ToString() + "' ";
                            stData.GetExecuteNonQry(qry);

                            whis.InsertWork("배송지", "배송지목록 삭제", qry);
                        }
                    }
                }
            }
        }

        string baesong = "";
        string baesongname = "";
        if (this.rbBaeSong1.Checked)
        {
            baesong = "0";
            baesongname = this.txtBaeSongName1.Text.ToString();
        }
        else if (this.rbBaeSong2.Checked)
        {
            baesong = "1";
            baesongname = this.txtBaeSongName2.Text.ToString();
        }
        else if (this.rbBaeSong3.Checked)
        {
            baesong = "2";
            baesongname = this.txtBaeSongName2.Text.ToString();
        }
        else if (this.rbBaeSong4.Checked)
        {
            baesong = "3";
            baesongname = this.txtBaeSongName4.Text.ToString();
        }

        string whereQry = " where Bjhd_MainBuyer = '" + blju_mainbuyer + "' ";
        whereQry = StCommon.MakeSearchQry("Bjhd_Date", blju_date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Times", blju_times, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Sample", blju_sample, "S", whereQry);

        qry = " update " + preVal + "BJHD set Bjhd_BaeSong = '" + baesong + "',Bjhd_BaeSongName = '" + baesongname + "',Bjhd_ModifyDate=getDate(),Bjhd_ModifySaWon = '" + MemberData.GetLoginSID("LoginID") + "' " + whereQry;
        stData.GetExecuteNonQry(qry);

        whis.InsertWork("배송지", "헤더입력", qry);

        StJavaScript js = new StJavaScript(this.Page, false, true);
        js.WriteJavascript("self.close();");
    }
}
