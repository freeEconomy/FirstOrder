using System;
using System.Data;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Login_Login : System.Web.UI.Page
{
    private StDataCommon stData = new StDataCommon();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // ZX-999 / qweqwe13!
            // ZQ / zq000...
            // DA / donga961!!!
            // YM / ``ym35
            // KR / corea0891!!
            // KN / Ads0251*
            // GN / gana3327!!
            // CBJ / eoqkrrkdns
            // DS / chlrkdeotjd1!
            // BDA -- 총판 / 8520zx8520@
            // ACE -- 대리점 / sangseon0#

            // ZX-7 / saeoul2723!

            // 암호 찾기

            string[] passArr = MemberData.GetPassDate("ZX-7");

            string chkPassDate = passArr[0];
            string chkPassWord = passArr[1];

            string decodePassword = MemberData.Password_Decode(chkPassDate, chkPassWord);

            //Response.Write(decodePassword);
            //Response.End();

            string idsave = "";
            try
            {
                idsave = Server.UrlDecode(Request.Cookies["USERINFO"]["IDSAVE"].ToString().Trim());
            }
            catch { }

            if (idsave == "OK")
            {
                this.cboIDSave.Checked = true;
                this.txtID.Text = Server.UrlDecode(Request.Cookies["USERINFO"]["ID"].ToString().Trim());
            }
        }
    }

    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        string idAll = StCommon.ReplaceSQ(this.txtID.Text);
        string id = StCommon.ReplaceSQ(this.txtID.Text);

        char[] delimiter = ":".ToCharArray();
        string[] strArray = id.Trim().Split(delimiter);
        if (strArray.Length >= 2)
        {
            id = strArray[0];
        }

        string pass = StCommon.ReplaceSQ(this.txtPass.Text);

        if (id.ToUpper() == "ZQ")
        {
            //id = "ds";
            //pass = "chlrkdeotjd1!";

            //Session["DevID"] = "ZQ";
        }
        else if (id.ToUpper() == "ZQAS")
        {
            //id = "ds";
            //pass = "chlrkdeotjd1!";

            //Session["DevID"] = "ZQAS";
        }

        string[] passArr = MemberData.GetPassDate(id);

        string chkPassDate = passArr[0];
        string chkPassWord = passArr[1];
        string kureSangho = passArr[2];
        string kureStatus = passArr[3];

        string decodePassword = MemberData.Password_Decode(chkPassDate, chkPassWord);

        string savePreVal = "";
        try
        {
            savePreVal = Server.UrlDecode(Request.Cookies["USERINFO"]["PREVAL"].ToString().Trim());
        }
        catch { }

        StJavaScript js = new StJavaScript(this.Page, false, true);

        if (MemberData.ExistLoginID(id))
        {
            if (decodePassword == "")
            {
                if (this.cboIDSave.Checked)
                {
                    Response.Cookies["USERINFO"]["IDSAVE"] = "OK";
                    Response.Cookies["USERINFO"]["ID"] = idAll;
                    Response.Cookies["USERINFO"].Expires = DateTime.Now.AddDays(365);
                }
                else
                {
                    Response.Cookies["USERINFO"]["IDSAVE"] = "";
                    Response.Cookies["USERINFO"]["ID"] = "";
                    Response.Cookies["USERINFO"].Expires = DateTime.Now.AddDays(-1);
                }

                // 신규 사용자
                js.WriteJavascript("ShowCreatePassword('" + kureSangho + "');");
            }
            else
            {
                if (pass == decodePassword)
                {
                    if (this.cboIDSave.Checked)
                    {
                        Response.Cookies["USERINFO"]["IDSAVE"] = "OK";
                        Response.Cookies["USERINFO"]["ID"] = idAll;
                        Response.Cookies["USERINFO"].Expires = DateTime.Now.AddDays(365);
                    }
                    else
                    {
                        Response.Cookies["USERINFO"]["IDSAVE"] = "";
                        Response.Cookies["USERINFO"]["ID"] = "";
                        Response.Cookies["USERINFO"].Expires = DateTime.Now.AddDays(-1);
                    }

                    int lastDateCheck = 180;
                    string qry = " select * from gblCommon where common_key = 'T0701' AND Common_Code = 'PASS_CHANGE' ";
                    DataSet ds = stData.GetDataSet(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (StCommon.ToInt(ds.Tables[0].Rows[0]["Common_Remark3"].ToString(), 0) > 0)
                        {
                            lastDateCheck = StCommon.ToInt(ds.Tables[0].Rows[0]["Common_Remark3"].ToString(), 0);
                        }
                    }

                    if (Convert.ToDateTime(chkPassDate).AddDays(lastDateCheck).CompareTo(DateTime.Now) < 0)
                    {
                        // 암호 생성후 ? 일이 지난 사용자
                        js.WriteJavascript("ShowChangePassword('" + kureSangho + "', '" + lastDateCheck + "');");
                    }
                    else
                    {
                        if (kureStatus == "9")
                        {
                            js.WriteJavascript("alert(\"로그인을 할 수 없습니다... '본사'에 문의바랍니다...\");");
                        }
                        else
                        {
                            MemberData.LoginSession(idAll, id, kureSangho);

                            string preval = "tbl";

                            if (savePreVal != "")
                                preval = savePreVal;

                            string stopMsg = OrderData_common.GetStopCheck(preval);
                            if (stopMsg != "")
                            {
                                Response.Redirect("/Page/Notice.aspx");
                            }
                            else
                            {
                                Response.Redirect("/Page/Order_" +preval+".aspx?mode=add");
                            }
                        }
                    }
                }
                else
                {
                    Session["LoginID"] = "";
                    Session["KureCode"] = "";
                    Session["KureName"] = "";

                    js.WriteJavascript("showMessageToolTip('" + this.txtPass.ClientID + "', '비밀번호가 일치하지 않습니다.');");
                }
            }
        }
        else
        {
            js.WriteJavascript("showMessageToolTip('" + this.txtID.ClientID + "', '아이디가 존재하지 않습니다.');");
        }
    }

    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        string kurecode = StCommon.ReplaceSQ(this.txtID.Text);
        
        string[] passArr = MemberData.GetPassDate(kurecode);

        string chkPassDate = passArr[0];
        string chkPassWord = passArr[1];
        string kureSangho = passArr[2];

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
            if (oldpass == pass)
            {
                js.WriteJavascript("$('#passChangeModal').modal('show'); alert('이전 암호와 새 암호가 같습니다.\\n새로운 암호로 입력해주세요.');");
            }
            else
            {
                if (pass == passconfirm)
                {
                    MemberData.UpdatePassWord(kurecode, MemberData.Password_Encode(String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now), pass));

                    js.ShowAlertMessage("변경하였습니다. 다시 로그인해주세요.", "location.href='/Login/Login.aspx';");
                }
                else
                {
                    js.WriteJavascript("$('#passChangeModal').modal('show'); alert('새 암호가 서로 일치하지 않습니다.');");
                }
            }
        }
        else
        {
            js.WriteJavascript("$('#passChangeModal').modal('show'); alert('이전 암호가 일치하지 않습니다.');");
        }
    }

    protected void btnCreatePwd_Click(object sender, EventArgs e)
    {
        string kurecode = StCommon.ReplaceSQ(this.txtID.Text);

        string[] passArr = MemberData.GetPassDate(kurecode);

        string kureSangho = passArr[2];

        string pass = StCommon.ReplaceSQ(this.txtNewPassword.Text.Trim());
        string passconfirm = StCommon.ReplaceSQ(this.txtNewPasswordConfirm.Text.Trim());

        this.txtNewPassword.Attributes.Add("value", pass);
        this.txtNewPasswordConfirm.Attributes.Add("value", passconfirm);

        StJavaScript js = new StJavaScript(this.Page, true);

        if (pass == passconfirm)
        {
            MemberData.CreatePassWord(kurecode, MemberData.Password_Encode(String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now), pass));

            js.ShowAlertMessage("생성하였습니다. 다시 로그인해주세요.", "location.href='/Login/Login.aspx';");
        }
        else
        {
            js.WriteJavascript("$('#passCreateModal').modal('show'); alert('새 암호가 서로 일치하지 않습니다.');");
        }
    }
}