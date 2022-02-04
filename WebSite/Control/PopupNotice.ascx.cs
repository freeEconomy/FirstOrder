using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Util;

public partial class Control_PopupNotice : System.Web.UI.UserControl
{
    private StDataCommon stData = new StDataCommon();
    private string preVal = "";

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

        if (!IsPostBack)
		{
            btnTodayClose.OnClientClick = "return fnPopupTodayClose('" + preVal + "PopupNotice');";

            BindList();

            /*
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ltlNotice.Text = Server.HtmlDecode(ds.Tables[0].Rows[0]["memo"].ToString());

                string fileName = ds.Tables[0].Rows[0]["fileName"].ToString().Trim();
                if (string.IsNullOrEmpty(fileName))
                {
                    this.lnkFile.Visible = false;
                }
                else
                {
                    this.lnkFile.Text = "[첨부파일]: " + fileName;;
                    this.lnkFile.OnClientClick = "return FileDownLoad('" + fileName + "');";
                    this.lnkFile.Visible = true;
                }
            }
            */

            string sql = " select top 1 * from " + preVal + "NOTICE where convert(nvarchar(10),getDate(),120) between noticeday and noticeday2 ORDER BY NoticeDay DESC, u DESC ";

            DataSet dsC = stData.GetDataSet(sql);

            if (dsC.Tables[0].Rows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(StCommon.GetCommonCookie("" + preVal + "PopupNotice")))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "MESSAGE", "<script>PopupNotice();</script>");
                }
            }
        }
	}

    private void BindList()
    {
        string qry = " select * from " + preVal + "NOTICE where convert(nvarchar(10),getDate(),120) between noticeday and noticeday2 ORDER BY memoday DESC, u DESC ";

        DataSet ds = stData.GetDataSet(qry);

        this.lvList.DataSource = ds;
        this.lvList.DataBind();

        if (ds.Tables[0].Rows.Count > 1)
        {
            ((DataPager)this.lvList.FindControl("dpList")).Visible = true;
        }
        else
        {
            try
            {
                ((DataPager)this.lvList.FindControl("dpList")).Visible = false;
            }
            catch { }
        }
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlNotice = (Literal)e.Item.FindControl("ltlNotice");
            LinkButton lnkFile = (LinkButton)e.Item.FindControl("lnkFile");
            
            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            ltlNotice.Text = Server.HtmlDecode(drItemRow["memo"].ToString());

            string fileName = drItemRow["fileName"].ToString().Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                lnkFile.Visible = false;
            }
            else
            {
                lnkFile.Text = "[첨부파일]: " + fileName; ;
                lnkFile.OnClientClick = "return FileDownLoad('" + fileName + "');";
                lnkFile.Visible = true;
            }
        }
    }

    protected void lvList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dp = ((ListView)sender).FindControl("dpList") as DataPager;
        dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindList();
    }
}
