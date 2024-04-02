using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using FirstOrder.Util;

public partial class Control_PopupCheck : System.Web.UI.UserControl
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
            BindList();
        }
	}

    private void BindList()
    {
        string qry = " select * from " + preVal + "NOTICE where convert(nvarchar(10),getDate(),120) between noticeday and noticeday2 ORDER BY memoday ASC, u ASC ";
        
        DataSet ds = stData.GetDataSet(qry);

        this.lvList.DataSource = ds;
        this.lvList.DataBind();

        string strScript = "";
        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
        {
            strScript += "checkCookie('layerPop" + ds.Tables[0].Rows[k]["U"].ToString() + "');";
        }

        if (strScript != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", strScript, true);
        }
    }

    public string GetPopupContentHeight(Object obj)
    {
        return obj.ToString() == "0" ? "0" : (Convert.ToInt32(obj) - 170).ToString();
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlNotice = (Literal)e.Item.FindControl("ltlNotice");
            HtmlAnchor aFile = (HtmlAnchor)e.Item.FindControl("aFile");
            Literal ltlFilePre = (Literal)e.Item.FindControl("ltlFilePre");
            Literal ltlFile = (Literal)e.Item.FindControl("ltlFile");

            DataRow drItemRow = ((DataRowView)e.Item.DataItem).Row;

            ltlNotice.Text = Server.HtmlDecode(drItemRow["memo"].ToString());

            string fileName = drItemRow["fileName"].ToString().Trim();
            if (!string.IsNullOrEmpty(fileName))
            {
                ltlFilePre.Text = "[첨부파일]: ";
                ltlFile.Text = fileName;
                aFile.Attributes.Add("onclick", "FileDownLoad('" + fileName + "');");
                aFile.Attributes.Add("style", "cursor:pointer;");
            }
        }
    }
}
