using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Page_Notice : System.Web.UI.Page
{
    private string preVal = "";
    private string preChk = "";

    private int totCount = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            preChk = StCommon.ReplaceSQ(Request["preChk"]);
        }
        catch { }

        if (preChk != "")
        {
            Session["PreVal"] = preChk;
            Response.Cookies["USERINFO"]["PREVAL"] = preChk;
        }

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
            this.btnAdd.Visible = false;
            if (MemberData.GetLoginSID("LoginID") == "ZX-999")
            {
                btnAdd.Visible = true;
            }

            BindMainList();
        }        
    }

    private void BindMainList()
    {
        string whereQry = "";

        string memoDayS = ((TextBox)this.ucMemoDaySSch.FindControl("txtDate")).Text;
        string memoDayE = ((TextBox)this.ucMemoDayESch.FindControl("txtDate")).Text;
        string memo = StCommon.ReplaceSQ(this.txtMemoSch.Text);
        
        whereQry = StCommon.MakeSearchQry("memoday", memoDayS, memoDayE, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("[memo]", memo, "%", whereQry);

        StDataCommon stData = new StDataCommon();

        DataSet tmpData = stData.GetDataSet("SELECT * FROM " + preVal + "NOTICE WHERE (u>0) " + whereQry + " ORDER BY [isNotice] desc, memoday DESC, u DESC ");
        totCount = tmpData.Tables[0].Rows.Count;

        this.lblTotCount.Text = "총 : " + string.Format("{0:#,##0}", totCount) + " 건";

        this.lvMain.DataSource = tmpData;
        this.lvMain.DataBind();
    }

    protected void lvMain_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dp = ((ListView)sender).FindControl("dpMain") as DataPager;
        dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindMainList();
    }

    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();
            
            Literal ltlContent = (Literal)e.Item.FindControl("ltlContent");
            
            if (Convert.ToBoolean(rowView["isNotice"].ToString()))
            {
                ltlViewNumber.Text = "<img src=\"/images/notice.png\">";
            }

            ltlContent.Text = Server.HtmlDecode(rowView["memo"].ToString());

            ImageButton imgBtn = (ImageButton)e.Item.FindControl("imgFile");
            string fileName = rowView["fileName"].ToString().Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                imgBtn.Visible = false;
            }
            else
            {
                imgBtn.ImageUrl = StFileFolder.GetFileIconByFileName(fileName);
                imgBtn.AlternateText = fileName;
                imgBtn.OnClientClick = "FileDownLoad('" + fileName + "');";
                imgBtn.Visible = true;
            }

            LinkButton lnkSubModify = (LinkButton)e.Item.FindControl("lnkSubModify");
            LinkButton lnkSubDelete = (LinkButton)e.Item.FindControl("lnkSubDelete");
            
            if (MemberData.GetLoginSID("LoginID") == "ZX-999")
            {
                lnkSubModify.Visible = true;
                lnkSubDelete.Visible = true;
            }
            else
            {
                lnkSubModify.Visible = false;
                lnkSubDelete.Visible = false;
            }
        }
    }

    protected void lvMain_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int idx = StCommon.ToInt(e.CommandArgument.ToString(), 0);
        this.hidIdx.Value = idx.ToString();

        switch (e.CommandName)
        {
            case "subModify":
                BindModify(idx);

                break;

            case "subDelete":
                Notice no = new Notice(preVal, idx);
                no.DeleteData();

                BindMainList();

                break;

            default:
                break;
        }
    }

    private void SetWriteDefaut()
    {
        ((TextBox)this.ucNoticeDay.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
        ((TextBox)this.ucNoticeDay2.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
        this.ckContent.Text = "";
    }

    private void BindModify(int idx)
    {
        this.mvMain.ActiveViewIndex = 1;
        this.btnWrite.Visible = false;
        this.btnModify.Visible = true;
        this.btnDelete.Visible = true;

        //초기화

        Notice no = new Notice(preVal, idx);
        no.GetNoticeInfoOne();

        ((TextBox)this.ucNoticeDay.FindControl("txtDate")).Text = no.NoticeDay;
        ((TextBox)this.ucNoticeDay2.FindControl("txtDate")).Text = no.NoticeDay2;
        //this.txtMemo.Text = no.MemoData;
        this.ckContent.Text = Server.HtmlDecode(no.MemoData);
        this.cbIsNotice.Checked = no.IsNotice;
        this.hidOrgFileName.Value = no.FileName;
        
        if (string.IsNullOrEmpty(no.FileName))
        {
            this.lblFile.Visible = false;
        }
        else
        {
            this.lblFile.Visible = true;
            this.lblFile.Text = string.Format("기존파일 : {0}", no.FileName);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMainList();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.mvMain.ActiveViewIndex = 1;
        this.btnWrite.Visible = true;
        this.btnModify.Visible = false;
        this.btnDelete.Visible = false;
        SetWriteDefaut();
    }

    protected void btnWrite_Click(object sender, EventArgs e)
    {
        string fileName = StCommon.ReplaceSQ(this.hidUpFileName.Value);

        Notice no = new Notice(preVal);

        no.NoticeDay = StCommon.ReplaceSQ(((TextBox)this.ucNoticeDay.FindControl("txtDate")).Text);
        no.NoticeDay2 = StCommon.ReplaceSQ(((TextBox)this.ucNoticeDay2.FindControl("txtDate")).Text);
        no.MemoData = Server.HtmlEncode(this.ckContent.Text.Trim());
        no.FileName = fileName;
        no.IsNotice = this.cbIsNotice.Checked;
        
        no.InsertData();

        this.mvMain.ActiveViewIndex = 0;
        BindMainList();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {        
        string fileName = StCommon.ReplaceSQ(this.hidUpFileName.Value);
        string orgFileName = StCommon.ReplaceSQ(this.hidOrgFileName.Value);
        string upDateFileName = string.Empty;

        if (!string.IsNullOrEmpty(fileName))
        {
            upDateFileName = fileName;
        }
        else
        {
            upDateFileName = orgFileName;
        }

        Notice no = new Notice(preVal, StCommon.ToInt(this.hidIdx.Value, 0));

        no.NoticeDay = StCommon.ReplaceSQ(((TextBox)this.ucNoticeDay.FindControl("txtDate")).Text);
        no.NoticeDay2 = StCommon.ReplaceSQ(((TextBox)this.ucNoticeDay2.FindControl("txtDate")).Text);
        no.MemoData = Server.HtmlEncode(this.ckContent.Text.Trim());        
        no.FileName = upDateFileName;
        no.IsNotice = this.cbIsNotice.Checked;

        no.UpdateData();

        BindMainList();

        this.mvMain.ActiveViewIndex = 0;
        this.hidIdx.Value = "";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Notice no = new Notice(preVal, StCommon.ToInt(this.hidIdx.Value, 0));
        no.DeleteData();

        BindMainList();

        this.mvMain.ActiveViewIndex = 0;
        this.hidIdx.Value = "";
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        this.mvMain.ActiveViewIndex = 0;
        this.hidIdx.Value = "";
    }
}