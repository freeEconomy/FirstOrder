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

public partial class Page_AsRequestList : System.Web.UI.Page
{
    private StDataCommon stData = new StDataCommon();
    private WorkHistory whis = new WorkHistory();
    private StCommon st = null;
    private int totCount = 0;
    private string preVal = "";
    private string hgubun = "";

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

        st = new StCommon(preVal);
        st.GetSizeInfo(preVal);

        try
        {
            hgubun = StCommon.ReplaceSQ(Request["hgubun"]);
        }
        catch { }

        if (!IsPostBack)
        {
            if (hgubun == "back")
            {
                try
                {
                    ((TextBox)this.ucBnpmdateS.FindControl("txtDate")).Text = Session["bnpmDateS"].ToString();
                    ((TextBox)this.ucBnpmdateE.FindControl("txtDate")).Text = Session["bnpmDateE"].ToString();
                    ((TextBox)this.ucBljudateS.FindControl("txtDate")).Text = Session["bljuDateS"].ToString();
                    ((TextBox)this.ucBljudateE.FindControl("txtDate")).Text = Session["bljuDateE"].ToString();
                }
                catch { }
            }
            else
            {
                string nowFirstDay = DateTime.Now.ToShortDateString().Substring(0, 8) + "01";
                ((TextBox)this.ucBnpmdateS.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
                ((TextBox)this.ucBnpmdateE.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
            }

            BindList();
        }        
    }

    private void BindList()
    {
        string whereQry = "";

        string kurecode = MemberData.GetLoginSID("KureCode");
        
        string bnpmDateS = ((TextBox)this.ucBnpmdateS.FindControl("txtDate")).Text;
        string bnpmDateE = ((TextBox)this.ucBnpmdateE.FindControl("txtDate")).Text;
        string bljuDateS = ((TextBox)this.ucBljudateS.FindControl("txtDate")).Text;
        string bljuDateE = ((TextBox)this.ucBljudateE.FindControl("txtDate")).Text;

        Session["bnpmDateS"] = bnpmDateS;
        Session["bnpmDateE"] = bnpmDateE;
        Session["bljuDateS"] = bljuDateS;
        Session["bljuDateE"] = bljuDateE;

        whereQry = StCommon.MakeSearchQry("BnpmH_Date", bnpmDateS, bnpmDateE, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("BnpmH_Bjhd_Date", bljuDateS, bljuDateE, "S", whereQry);

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" SELECT (select top 1 BnpmD_StyleNox from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_StyleNox ");
        sb.AppendLine(" , (select sum(BnpmD_QtyTotal) from " + preVal + "BNPMD where BnpmD_Date=a.BnpmH_Date and BnpmD_Times=a.BnpmH_Times and BnpmD_MainBuyer=a.BnpmH_MainBuyer and BnpmD_Sample=a.BnpmH_Sample) as BnpmH_QtyTotal ");
        sb.AppendLine(" , * FROM " + preVal + "BNPMH a ");
        sb.AppendLine(" WHERE BnpmH_MainBuyer = '" + kurecode + "' " + whereQry + " ORDER BY BnpmH_Date desc, BnpmH_Times desc ");
        
        DataSet ds = stData.GetDataSet(sb.ToString());
        totCount = ds.Tables[0].Rows.Count;

        this.lblTotCount.Text = "총 : " + string.Format("{0:#,##0}", totCount) + " 건";

        this.lvList.DataSource = ds;
        this.lvList.DataBind();

        this.lvExcel.DataSource = ds;
        this.lvExcel.DataBind();

        if (totCount > 0)
        {
            this.btnExcel.Visible = true;
        }
        else
        {
            this.btnExcel.Visible = false;
        }
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            Literal ltlBonsaCheck = (Literal)e.Item.FindControl("ltlBonsaCheck");
            LinkButton lnkSubView = (LinkButton)e.Item.FindControl("lnkSubView");
            LinkButton lnkSubModify = (LinkButton)e.Item.FindControl("lnkSubModify");
            LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "0" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "")
            {
                ltlBonsaCheck.Text = "AS의뢰 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "1" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "")
            {
                ltlBonsaCheck.Text = "AS의뢰 완료";
                lnkSubModify.OnClientClick = "return confirm('[AS의뢰 중]으로 변경이 됩니다. 진행 하시겠습니까?');";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Y" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "0")
            {
                ltlBonsaCheck.Text = "본사확인 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Y" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "Y")
            {
                ltlBonsaCheck.Text = "본사확인 완료";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Z" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "U")
            {
                ltlBonsaCheck.Text = "확정작업 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Z" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "Z")
            {
                ltlBonsaCheck.Text = "확정작업 완료";
            }

            lnkSubView.OnClientClick = "location.href=\"/Page/AsRequest.aspx?mode=view&param_date=" + drItemRow["BnpmH_date"].ToString() + "&param_times=" + drItemRow["BnpmH_times"].ToString() + "&param_mainbuyer=" + drItemRow["BnpmH_mainbuyer"].ToString() + "&param_sample=" + drItemRow["BnpmH_sample"].ToString() + "\"; return false;";
            lnkSubModify.CommandArgument = drItemRow["BnpmH_Date"].ToString() + "|" + drItemRow["BnpmH_Times"].ToString() + "|" + drItemRow["BnpmH_MainBuyer"].ToString() + "|" + drItemRow["BnpmH_Sample"].ToString();
            lnkSubDelete.CommandArgument = drItemRow["BnpmH_Date"].ToString() + "|" + drItemRow["BnpmH_Times"].ToString() + "|" + drItemRow["BnpmH_MainBuyer"].ToString() + "|" + drItemRow["BnpmH_Sample"].ToString();

            // 대리점은 tblBNPMD테이블 BnpmH_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
            if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "0" || drItemRow["BnpmH_Bonsa_Check"].ToString() == "1")
            {
                if (drItemRow["BnpmH_CreateSawon"].ToString() == MemberData.GetLoginSID("LoginID")) // 로그인한 사용자가 요청한 내역만 수정/삭제 가능
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

        string bnpm_date = strArray[0];
        string bnpm_times = strArray[1];
        string bnpm_mainbuyer = strArray[2];
        string bnpm_sample = strArray[3];

        string kurename = "";
        string qry = "select * from gblKURE where Kure_Code='" + bnpm_mainbuyer + "' ";
        DataSet ds = stData.GetDataSet(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            kurename = ds.Tables[0].Rows[0]["Kure_Sangho"].ToString();
        }

        qry = " select BnpmH_Bonsa_Check, BnpmH_Bonsa_Check1 FROM " + preVal + "BNPMH where BnpmH_Date = '" + bnpm_date + "' and BnpmH_Times = '" + bnpm_times + "' and BnpmH_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmH_Sample='" + bnpm_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string bnpm_bonsa_check = "";
        string bnpm_bonsa_check1 = "";
        if (dsC.Tables[0].Rows.Count > 0)
        {
            bnpm_bonsa_check = dsC.Tables[0].Rows[0]["BnpmH_Bonsa_Check"].ToString();
            bnpm_bonsa_check1 = dsC.Tables[0].Rows[0]["BnpmH_Bonsa_Check1"].ToString();
        }

        string stateMsg = "";
        if (bnpm_bonsa_check == "Y" && bnpm_bonsa_check1 == "0")
        {
            stateMsg = "본사확인 중..";
        }
        else if (bnpm_bonsa_check == "Y" && bnpm_bonsa_check1 == "Y")
        {
            stateMsg = "본사확인 완료";
        }
        else if (bnpm_bonsa_check == "Z" && bnpm_bonsa_check1 == "U")
        {
            stateMsg = "확정작업 중..";
        }
        else if (bnpm_bonsa_check == "Z" && bnpm_bonsa_check1 == "Z")
        {
            stateMsg = "확정작업 완료";
        }

        StJavaScript js = new StJavaScript(this.Page, false, true);
        LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");
        
        switch (e.CommandName)
        {
            case "subModify":

                // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
                if (bnpm_bonsa_check == "0" || bnpm_bonsa_check == "1")
                {
                    qry = " update " + preVal + "BNPMH set BnpmH_Bonsa_Check = '0' where BnpmH_Date = '" + bnpm_date + "' and BnpmH_Times = '" + bnpm_times + "' and BnpmH_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmH_Sample='" + bnpm_sample + "' ";
                    stData.GetExecuteNonQry(qry);

                    whis.InsertWork("AS접수", "AS의뢰중으로 변경", qry);

                    js.WriteJavascript("location.href=\"/Page/AsRequest.aspx?mode=edit&param_date=" + bnpm_date + "&param_times=" + bnpm_times + "&param_mainbuyer=" + bnpm_mainbuyer + "&param_sample=" + bnpm_sample + "\";");
                }
                else
                {
                    js.WriteJavascript("alert('수정할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.');");
                }

                break;

            case "subDelete":

                // 대리점은 Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
                if (bnpm_bonsa_check == "0" || bnpm_bonsa_check == "1")
                {
                    qry = " DELETE FROM " + preVal + "BNPMH where BnpmH_Date = '" + bnpm_date + "' and BnpmH_Times = '" + bnpm_times + "' and BnpmH_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmH_Sample='" + bnpm_sample + "' ";
                    stData.GetExecuteNonQry(qry);

                    whis.InsertWork("AS접수", "헤더삭제", qry);

                    qry = " DELETE FROM " + preVal + "BNPMD where BnpmD_Date = '" + bnpm_date + "' and BnpmD_Times = '" + bnpm_times + "' and BnpmD_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmD_Sample='" + bnpm_sample + "' ";
                    stData.GetExecuteNonQry(qry);

                    whis.InsertWork("AS접수", "상세삭제", qry);

                    qry = " select BnpmX_imageFileName from " + preVal + "BNPMX where BnpmX_Date = '" + bnpm_date + "' and BnpmX_Times = '" + bnpm_times + "' and BnpmX_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmX_Sample = '" + bnpm_sample + "' order by BnpmX_LineSeqx ";
                    DataSet dsF = stData.GetDataSet(qry);
                    string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "AsFilePath");

                    for (int i = 0; i < dsF.Tables[0].Rows.Count; i++)
                    {
                        string filename = dsF.Tables[0].Rows[i]["BnpmX_imageFileName"].ToString().Trim();

                        if (filename != "")
                        {
                            // 물리파일 삭제
                            StFileFolder.DeleteFile(upDir, filename);
                        }
                    }

                    qry = " DELETE FROM " + preVal + "BNPMX where BnpmX_Date = '" + bnpm_date + "' and BnpmX_Times = '" + bnpm_times + "' and BnpmX_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmX_Sample='" + bnpm_sample + "' ";
                    stData.GetExecuteNonQry(qry);

                    whis.InsertWork("AS접수", "첨부파일삭제", qry);
                    
                    BindList();
                }
                else
                {   
                    js.WriteJavascript("alert('삭제할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.');");
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

    protected void lvExcel_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            Literal ltlBonsaCheck = (Literal)e.Item.FindControl("ltlBonsaCheck");
            
            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "0" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "")
            {
                ltlBonsaCheck.Text = "AS의뢰 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "1" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "")
            {
                ltlBonsaCheck.Text = "AS의뢰 완료";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Y" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "0")
            {
                ltlBonsaCheck.Text = "본사확인 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Y" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "Y")
            {
                ltlBonsaCheck.Text = "본사확인 완료";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Z" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "U")
            {
                ltlBonsaCheck.Text = "확정작업 중..";
            }
            else if (drItemRow["BnpmH_Bonsa_Check"].ToString() == "Z" && drItemRow["BnpmH_Bonsa_Check1"].ToString() == "Z")
            {
                ltlBonsaCheck.Text = "확정작업 완료";
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindList();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string appKind = "-AS접수현황조회";
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

        this.lvExcel.Visible = true;

        hw.Write("<table width='100%' border='1' cellpadding='0' cellspacing='0' >");
        this.lvExcel.EnableViewState = false;
        this.lvExcel.RenderControl(hw);
        hw.Write("</table>");

        this.lvExcel.Visible = false;

        Response.Write("<meta http-equiv=Content-Type content=''text/html; charset=utf-8''>");
        Response.Write("<link rel='stylesheet' href='http://" + Request["HTTP_HOST"] + "/css/style.css'>");
        Response.Write(sw.ToString());
        Response.End();
    }

    public string GetAmountFormat(Object obj)
    {
        string result = obj.ToString().Trim();
        
        if (result != "")
        {
            result = StCommon.NumberFormat(Convert.ToDouble(result));
        }

        return result;
    }
}