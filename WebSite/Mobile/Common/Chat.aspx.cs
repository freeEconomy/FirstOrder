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

public partial class Mobile_Common_Chat : System.Web.UI.Page
{
    private string preVal = "";

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
            blju_date = Server.HtmlEncode(Request["blju_date"].Trim());
            blju_times = Server.HtmlEncode(Request["blju_times"].Trim());
            blju_mainbuyer = Server.HtmlEncode(Request["blju_mainbuyer"].Trim());
            blju_sample = Server.HtmlEncode(Request["blju_sample"].Trim());
        }
        catch { }

        this.hidDate.Value = blju_date;
        this.hidTimes.Value = blju_times;
        this.hidMainbuyer.Value = blju_mainbuyer;
        this.hidSample.Value = blju_sample;

        if (!IsPostBack)
        {
            MsgRead();
            BindData();
        }
    }

    private void MsgRead()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" update " + preVal + "MESG set Mesg_DaeRiReadOk = '0' ");
        sb.Append(" where Mesg_Date='" + blju_date + "' and Mesg_Times='" + blju_times + "' and Mesg_MainBuyer='" + blju_mainbuyer + "' and Mesg_Sample='" + blju_sample + "' and Mesg_BonSa_DaeRi = '0' ");

        stData.GetExecuteNonQry(sb.ToString());

        WorkHistory whis = new WorkHistory();
        whis.InsertWork("대화방", "읽음", sb.ToString());
    }

    private void BindData()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from " + preVal + "MESG ");
        sb.Append(" where Mesg_Date='" + blju_date + "' and Mesg_Times='" + blju_times + "' and Mesg_MainBuyer='" + blju_mainbuyer + "' and Mesg_Sample='" + blju_sample + "' ");
        sb.Append(" order by Mesg_CreateDateTimes ");

        DataSet ds = stData.GetDataSet(sb.ToString());

        this.lvMain.DataSource = ds;
        this.lvMain.DataBind();
    }

    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            string mesg_bonsa_daeri = rowView["Mesg_BonSa_DaeRi"].ToString();
            string mesg_message = rowView["Mesg_Message"].ToString();
            string mesg_createdatetimes = rowView["Mesg_CreateDateTimes"].ToString();
            DateTime createDate = Convert.ToDateTime(mesg_createdatetimes.Substring(0, 19));
            string isBonsaRead = rowView["Mesg_BonSaReadOk"].ToString();
            string isDaeriRead = rowView["Mesg_DaeRiReadOk"].ToString();

            StringBuilder sb = new StringBuilder();

            if (mesg_bonsa_daeri == "0") // 본사 메시지
            {
                sb.AppendLine("<div class=\"incoming_msg\">");
                sb.AppendLine("    <div class=\"received_msg\">");
                sb.AppendLine("        <p>");
                sb.AppendLine("            " + StCommon.Chr2Br(mesg_message) + "");
                sb.AppendLine("        </p>");
                sb.AppendLine("        <span class=\"time_date\">");
                sb.AppendLine("             " + createDate.ToLongDateString() + " " + createDate.ToLongTimeString() + "");
                sb.AppendLine("        </span>");
                sb.AppendLine("    </div>");
                sb.AppendLine("</div>");
            }
            else // 대리점 메시지
            {
                sb.AppendLine("<div class=\"outgoing_msg\">");
                sb.AppendLine("    <div class=\"sent_msg\">");
                sb.AppendLine("        <p>");
                sb.AppendLine("            " + StCommon.Chr2Br(mesg_message) + "");
                sb.AppendLine("        </p>");
                sb.AppendLine("        <span class=\"time_date\">");
                sb.AppendLine("             " + createDate.ToLongDateString() + " " + createDate.ToLongTimeString() + "");
                if (isBonsaRead == "1") // 본사 확인여부
                {
                    sb.AppendLine("             <span style='color:red;'>&nbsp;1</span>");
                }
                sb.AppendLine("        </span>");
                sb.AppendLine("    </div>");
                sb.AppendLine("</div>");
            }

            Literal rsltItem = (Literal)e.Item.FindControl("ltlItem");
            rsltItem.Text = sb.ToString();
        }
    }

    protected void sendMessage(object sender, EventArgs e)
    {
        string msg = StCommon.ReplaceSQ(this.txtMsg.Text);

        msg += "\n$l";

        string createDate = DateTime.Now.ToShortDateString() + " " + String.Format("{0:HH:mm:ss:fff}", DateTime.Now);

        StringBuilder sb = new StringBuilder();
        sb.Append(" INSERT INTO " + preVal + "MESG (Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample,Mesg_CreateDateTimes,Mesg_BonSa_DaeRi,Mesg_Message,Mesg_BonSaReadOk,Mesg_DaeRiReadOk,Mesg_CreateSaWon) ");
        sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + createDate + "','1','" + msg + "','1','0','" + MemberData.GetLoginSID("LoginID") + "') ");
        stData.GetExecuteNonQry(sb.ToString());

        WorkHistory whis = new WorkHistory();
        whis.InsertWork("대화방", "입력", sb.ToString());

        BindData();

        this.txtMsg.Text = "";
        this.txtMsg.Focus();
    }

    protected void sendMessage2(object sender, EventArgs e)
    {
        string msg = StCommon.ReplaceSQ(this.txtMsg.Text);

        msg += "\n$l";

        string createDate = DateTime.Now.ToShortDateString() + " " + String.Format("{0:HH:mm:ss:fff}", DateTime.Now);

        StringBuilder sb = new StringBuilder();
        sb.Append(" INSERT INTO " + preVal + "MESG (Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample,Mesg_CreateDateTimes,Mesg_BonSa_DaeRi,Mesg_Message,Mesg_BonSaReadOk,Mesg_DaeRiReadOk,Mesg_CreateSaWon) ");
        sb.Append(" VALUES('" + blju_date + "','" + blju_times + "','" + blju_mainbuyer + "','" + blju_sample + "','" + createDate + "','0','" + msg + "','0','1','" + MemberData.GetLoginSID("LoginID") + "') ");
        stData.GetExecuteNonQry(sb.ToString());

        WorkHistory whis = new WorkHistory();
        whis.InsertWork("대화방", "입력", sb.ToString());

        BindData();

        this.txtMsg.Text = "";
        this.txtMsg.Focus();
    }
}
