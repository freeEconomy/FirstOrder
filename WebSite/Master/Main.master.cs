using System;
using System.Data;
using System.IO;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Master_Main : System.Web.UI.MasterPage
{
    public string jsCssVer = DateTime.Now.ToString("yyyyMMddhhmmss");

    private StDataCommon stData = new StDataCommon();
    private string preVal = "";
    private string oppVal = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StCommon.IsMobileAgent(Request.UserAgent))
        {
            Response.Redirect("~/Mobile/", true);
        }

        if (MemberData.IsLogin() == false)
        {
            Response.Redirect("~/Login/Login.aspx", true);
        }
        
        if (!IsPostBack)
        {
            string sPageName = Path.GetFileName(Request.PhysicalPath);

            PopupCheck.Visible = sPageName.ToLower().Trim() == "order_tbl.aspx" || sPageName.ToLower().Trim() == "order_abl.aspx";

            if (sPageName.ToLower().Trim() == "order_tbl.aspx" && Request.QueryString.ToString() == "mode=add")
            {
                Session["PreVal"] = "tbl";
                Response.Cookies["USERINFO"]["PREVAL"] = "tbl";
            }

            if (sPageName.ToLower().Trim() == "order_abl.aspx" && Request.QueryString.ToString() == "mode=add")
            {
                Session["PreVal"] = "abl";
                Response.Cookies["USERINFO"]["PREVAL"] = "abl";
            }

            try
            {
                preVal = Session["PreVal"].ToString();
            }
            catch
            {
                preVal = "tbl";
            }

            oppVal = (preVal == "tbl") ? "abl" : "tbl";

            this.ltlActive1.Text = "class=\"nav-item\"";
            this.ltlActive2.Text = "class=\"nav-item\"";
            this.ltlActive3.Text = "class=\"nav-item\"";
            this.ltlActive4.Text = "class=\"nav-item\"";
            this.ltlActive5.Text = "class=\"nav-item\"";
            this.ltlActive6.Text = "class=\"nav-item\"";
            this.ltlActive7.Text = "class=\"nav-item\"";
            this.ltlActive8.Text = "class=\"nav-item\" style=\"display:none\"";
            this.ltlActive9.Text = "class=\"nav-item\" style=\"display:none\"";
            if (MemberData.GetLoginSID("DevID") == "ZQ" || MemberData.GetLoginSID("DevID") == "ZQAS")
            {
                this.ltlActive8.Text = "class=\"nav-item\"";
                this.ltlActive9.Text = "class=\"nav-item\"";
            }

            switch (sPageName)
            {
                case "Notice.aspx":

                    this.ltlActive1.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "공지사항";

                    break;

                case "Order_tbl.aspx":
                case "Order_abl.aspx":

                    if (Request.QueryString.ToString() == "mode=add")
                    {
                        this.ltlActive2.Text = "class=\"nav-item active\"";
                        this.ltlTitle.Text = "발주의뢰";
                    }
                    else
                    {
                        this.ltlActive3.Text = "class=\"nav-item active\"";
                        this.ltlTitle.Text = "발주의뢰 현황조회";
                    }

                    break;

                case "KureSpecList.aspx":

                    this.ltlActive4.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "거래명세서 현황조회";

                    break;

                case "MarkJegoList.aspx":

                    this.ltlActive5.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "본사 재고 조회";

                    break;

                case "JegoUpload_tbl.aspx":
                case "JegoUpload_abl.aspx":

                    this.ltlActive6.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "대리점 재고 업로드";

                    break;

                case "JegoList.aspx":

                    this.ltlActive7.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "대리점 재고 조회";

                    break;

                case "AsRequest.aspx":

                    this.ltlActive8.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "As접수";

                    break;

                case "AsRequestList.aspx":

                    this.ltlActive9.Text = "class=\"nav-item active\"";
                    this.ltlTitle.Text = "As접수 현황조회";

                    break;
            }

            this.hylLink1.NavigateUrl = "/Page/Notice.aspx";

            string stopMsg = OrderData_common.GetStopCheck(preVal);
            if (stopMsg != "")
            {
                this.hylLink2.NavigateUrl = "";
                this.hylLink2.Attributes.Add("onclick", "alert(\"" + stopMsg + "\");");
            }
            else
            {
                this.hylLink2.NavigateUrl = "/Page/Order_" + preVal + ".aspx?mode=add";
            }

            this.hylLink3.NavigateUrl = "/Page/Order_" + preVal + ".aspx";
            this.hylLink4.NavigateUrl = "/Page/KureSpecList.aspx";
            this.hylLink5.NavigateUrl = "/Page/MarkJegoList.aspx";
            this.hylLink6.NavigateUrl = "/Page/JegoUpload_" + preVal + ".aspx";
            this.hylLink7.NavigateUrl = "/Page/JegoList.aspx";
            this.hylLink8.NavigateUrl = "/Page/AsRequest.aspx";
            this.hylLink9.NavigateUrl = "/Page/AsRequestList.aspx";

            this.ltlMenuStyle.Text = (preVal == "tbl") ? "class=\"navbar-nav bg-gradient-orange sidebar sidebar-dark accordion tblbackground\"" : "class=\"navbar-nav bg-gradient-primary sidebar sidebar-dark accordion ablbackground\"";
            this.ltlTopBgStyle.Text = (preVal == "tbl") ? "class=\"navbar navbar-expand navbar-light bg-orange topbar static-top shadow\"" : "class=\"navbar navbar-expand navbar-light bg-blue topbar static-top shadow\"";

            this.ltlGubun.Text = (preVal == "tbl") ? "의류" : "안전화";

            this.lbtBljuLink1.Visible = (preVal == "tbl") ? false : true;
            this.lbtBljuLink2.Visible = (preVal == "tbl") ? true : false;

            string stopMsg1 = OrderData_common.GetStopCheck("tbl");
            string stopMsg2 = OrderData_common.GetStopCheck("abl");

            if (stopMsg1 != "")
            {
                this.lbtBljuLink1.OnClientClick = "location.href='/Page/Notice.aspx?preChk=tbl'; return false;";
            }
            else
            {
                this.lbtBljuLink1.OnClientClick = "location.href='/Page/Order_tbl.aspx?mode=add'; return false;";
            }

            if (stopMsg2 != "")
            {
                this.lbtBljuLink2.OnClientClick = "location.href='/Page/Notice.aspx?preChk=abl'; return false;";
            }
            else
            {
                this.lbtBljuLink2.OnClientClick = "location.href='/Page/Order_abl.aspx?mode=add'; return false;";
            }

            string kurecode = MemberData.GetLoginSID("KureCode");

            string qry = " select mesg_date,mesg_times,mesg_mainbuyer,mesg_sample from " + preVal + "MESG where mesg_mainbuyer = '" + kurecode + "' and Mesg_DaeRiReadOk <> '0' and Mesg_BonSa_DaeRi = '0' group by mesg_date,mesg_times,mesg_mainbuyer,mesg_sample order by mesg_date desc,mesg_times desc ";
            DataSet ds = stData.GetDataSet(qry);

            this.ltlMsgCnt.Text = ds.Tables[0].Rows.Count.ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                string param_date = ds.Tables[0].Rows[0]["mesg_date"].ToString();
                string param_times = ds.Tables[0].Rows[0]["mesg_times"].ToString();
                string param_mainbuyer = ds.Tables[0].Rows[0]["mesg_mainbuyer"].ToString();
                string param_sample = ds.Tables[0].Rows[0]["mesg_sample"].ToString();
                this.hylMsgLink.Attributes.Add("onclick", "location.href=\"/Page/Order_" + preVal + ".aspx?mode=view&param_date=" + param_date + "&param_times=" + param_times + "&param_mainbuyer=" + param_mainbuyer + "&param_sample=" + param_sample + "\"; ");
                this.hylMsgLink.Attributes.Add("style", "cursor:pointer");
            }

            BindKureInfo();
        }
    }
    
    private void BindKureInfo()
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        string qry = " select * from gblKURE where kure_code = '" + kurecode + "' ";
        DataSet ds = stData.GetDataSet(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.ltlKureSangho.Text = ds.Tables[0].Rows[0]["Kure_Sangho"].ToString();
            this.ltlKureSangho2.Text = ds.Tables[0].Rows[0]["Kure_Sangho"].ToString();
            this.ltlKureDaePyo.Text = ds.Tables[0].Rows[0]["Kure_DaePyo"].ToString();
            this.ltlKureAddr.Text = "(" + ds.Tables[0].Rows[0]["Kure_ZipCode"].ToString() + ")" + ds.Tables[0].Rows[0]["Kure_Address1"].ToString() + " " + ds.Tables[0].Rows[0]["Kure_Address2"].ToString() + " " + ds.Tables[0].Rows[0]["Kure_Address3"].ToString();
            this.ltlKureTel.Text = ds.Tables[0].Rows[0]["Kure_Tel"].ToString();
            this.ltlKurePhone.Text = ds.Tables[0].Rows[0]["Kure_MainDamdang_CTel"].ToString();

            this.txtZipcode.Text = ds.Tables[0].Rows[0]["Kure_ZipCode"].ToString();
            this.txtAddress1.Text = ds.Tables[0].Rows[0]["Kure_Address1"].ToString();
            this.txtAddress2.Text = ds.Tables[0].Rows[0]["Kure_Address2"].ToString();
            this.txtKureTel.Text = ds.Tables[0].Rows[0]["Kure_Tel"].ToString();
            this.txtKurePhone.Text = ds.Tables[0].Rows[0]["Kure_MainDamdang_CTel"].ToString();
        }
    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        string[] passArr = MemberData.GetPassDate(kurecode);

        string chkPassDate = passArr[0];
        string chkPassWord = passArr[1];

        string decodePassword = MemberData.Password_Decode(chkPassDate, chkPassWord);

        string oldpass = StCommon.ReplaceSQ(this.txtOldPassword.Text.Trim());
        string pass = StCommon.ReplaceSQ(this.txtPassword.Text.Trim());
        string passconfirm = StCommon.ReplaceSQ(this.txtPasswordConfirm.Text.Trim());

        this.txtOldPassword.Attributes.Add("value", oldpass);
        this.txtPassword.Attributes.Add("value", pass);
        this.txtPasswordConfirm.Attributes.Add("value", passconfirm);

        StJavaScript js = new StJavaScript(this.Page, false, true);

        if (decodePassword == oldpass)
        {
            if (pass == passconfirm)
            {
                MemberData.UpdatePassWord(kurecode, MemberData.Password_Encode(String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now), pass));

                js.ShowAlertMessage("변경하였습니다.");
            }
            else
            {
                js.WriteJavascript("$('#passChangeModal').modal('show'); alert('새 암호가 서로 일치하지 않습니다.');");
            }
        }
        else
        {
            js.WriteJavascript("$('#passChangeModal').modal('show'); alert('이전 암호가 일치하지 않습니다.');");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        string zipcode = this.txtZipcode.Text.ToString();
        string address1 = this.txtAddress1.Text.ToString();
        string address2 = this.txtAddress2.Text.ToString();
        string tel = this.txtKureTel.Text.ToString();
        string phone = this.txtKurePhone.Text.ToString();

        string qry = " update gblKURE set Kure_ZipCode = '"+ zipcode + "',Kure_Address1 = '" + address1 + "',Kure_Address2 = '" + address2 + "',Kure_Tel = '" + tel + "',Kure_MainDamdang_CTel = '" + phone + "' where kure_code = '" + kurecode + "' ";
        stData.GetExecuteNonQry(qry);
        
        WorkHistory whis = new WorkHistory();
        whis.InsertWork("대리점", "정보수정", qry);

        BindKureInfo();
    }
}
