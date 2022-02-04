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

public partial class Page_KureSpecList : System.Web.UI.Page
{
    private StDataCommon stData = new StDataCommon();
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
                    ((TextBox)this.ucNapmDateS.FindControl("txtDate")).Text = Session["napmDateS"].ToString();
                    ((TextBox)this.ucNapmDateE.FindControl("txtDate")).Text = Session["napmDateE"].ToString();
                }
                catch { }
            }
            else
            {
                string nowFirstDay = DateTime.Now.ToShortDateString().Substring(0, 8) + "01";
                //((TextBox)this.ucNapmDateS.FindControl("txtDate")).Text = Convert.ToDateTime(nowFirstDay).ToShortDateString();
                ((TextBox)this.ucNapmDateS.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
                ((TextBox)this.ucNapmDateE.FindControl("txtDate")).Text = DateTime.Now.ToShortDateString();
            }

            BindMainList();
        }        
    }

    private void BindMainList()
    {
        string whereQry = "";

        string kurecode = MemberData.GetLoginSID("KureCode");
        
        string napmDateS = ((TextBox)this.ucNapmDateS.FindControl("txtDate")).Text;
        string napmDateE = ((TextBox)this.ucNapmDateE.FindControl("txtDate")).Text;

        Session["napmDateS"] = napmDateS;
        Session["napmDateE"] = napmDateE;

        whereQry = StCommon.MakeSearchQry("Bjhd_NapmDate", napmDateS, napmDateE, "S", whereQry);
        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" SELECT (select count(*) " + preVal + "MESG where Mesg_Date = Bjhd_Date AND Mesg_Times = Bjhd_Times AND Mesg_MainBuyer = Bjhd_MainBuyer AND Mesg_Sample = Bjhd_Sample) msgCnt ");
        sb.AppendLine(" , ISNULL(SQL_BonSaReadOk,0) as BonSaReadOk,ISNULL(SQL_DaeRiReadOk,0) as DaeRiReadOk, Bjhd_NapmDate ");
        sb.AppendLine(" , LTRIM(STUFF((select ',' + Blju_StyleNox from " + preVal + "BLJU where isnull(Blju_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Blju_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Blju_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') FOR XML PATH('')), 1, 1, '')) as Blju_StyleNox ");
        sb.AppendLine(" , Bjhd_NetAmount, Bjhd_VatAmount, Bjhd_HapAmount, Bjhd_Remark, Bjhd_Date, Bjhd_Times, Bjhd_MainBuyer, Bjhd_Sample, Bjhd_BaeSong, Bjhd_KureMyung_Print ");
        sb.AppendLine(" , ISNULL((select Bjhd0_Name from " + preVal + "BJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'')),'') as Bjhd0_Name ");
        sb.AppendLine(" , ISNULL((select Bjhd0_Name_Send from " + preVal + "BJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'')),'') as Bjhd0_Name_Send ");
        sb.AppendLine(" FROM " + preVal + "BJHD a LEFT OUTER JOIN (SELECT Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample,SUM(case when Mesg_BonSa_Daeri = '1' then Mesg_BonSaReadOk else 0 end) AS SQL_BonSaReadOk,SUM(case when Mesg_BonSa_Daeri = '0' then Mesg_DaeRiReadOk else 0 end) AS SQL_DaeRiReadOk FROM " + preVal + "MESG GROUP BY Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample) t1 ");
        sb.AppendLine(" ON t1.Mesg_Date = Bjhd_Date AND t1.Mesg_Times = Bjhd_Times AND t1.Mesg_MainBuyer = Bjhd_MainBuyer AND t1.Mesg_Sample = Bjhd_Sample ");
        sb.AppendLine(" WHERE Bjhd_MainBuyer = '" + kurecode + "' and (Bjhd_Bonsa_Check = 'Z' AND Bjhd_Bonsa_Check1 IN('X','Z')) " + whereQry + " ORDER BY Bjhd_NapmDate asc ");
        
        DataSet ds = stData.GetDataSet(sb.ToString());
        totCount = ds.Tables[0].Rows.Count;

        this.lblTotCount.Text = "총 : " + string.Format("{0:#,##0}", totCount) + " 건";

        this.lvMain.DataSource = ds;
        this.lvMain.DataBind();

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

        #region // 합계 바인딩
        double sumNet = 0;
        double sumVat = 0;
        double sumHap = 0;
        try
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                sumNet += StCommon.ToDouble(dr["Bjhd_NetAmount"].ToString(), 0);
                sumVat += StCommon.ToDouble(dr["Bjhd_VatAmount"].ToString(), 0);
                sumHap += StCommon.ToDouble(dr["Bjhd_HapAmount"].ToString(), 0);
            }

            ((Literal)this.lvMain.FindControl("ltlSumNet")).Text = GetAmountFormat(sumNet);
            ((Literal)this.lvMain.FindControl("ltlSumVat")).Text = GetAmountFormat(sumVat);
            ((Literal)this.lvMain.FindControl("ltlSumHap")).Text = GetAmountFormat(sumHap);

            ((Literal)this.lvExcel.FindControl("ltlSumNet")).Text = GetAmountFormat(sumNet);
            ((Literal)this.lvExcel.FindControl("ltlSumVat")).Text = GetAmountFormat(sumVat);
            ((Literal)this.lvExcel.FindControl("ltlSumHap")).Text = GetAmountFormat(sumHap);
        }
        catch { }
        #endregion
    }
    
    protected void lvMain_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");
            Literal ltlStyleNox = (Literal)e.Item.FindControl("ltlStyleNox");
            Literal ltlSample = (Literal)e.Item.FindControl("ltlSample");
            Literal ltlBaesongSend = (Literal)e.Item.FindControl("ltlBaesongSend");
            LinkButton lnkMessenger = (LinkButton)e.Item.FindControl("lnkMessenger");
            LinkButton lnkSubView = (LinkButton)e.Item.FindControl("lnkSubView");

            ltlViewNumber.Text = (item.DataItemIndex + 1).ToString();

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

            if (drItemRow["Bjhd_Sample"].ToString() == "0")
            {
                ltlSample.Text = "정상";
            }
            else if (drItemRow["Bjhd_Sample"].ToString() == "2")
            {
                ltlSample.Text = "반품";
            }
            else if (drItemRow["Bjhd_Sample"].ToString() == "3")
            {
                ltlSample.Text = "교환";
            }
            else if (drItemRow["Bjhd_Sample"].ToString() == "9")
            {
                ltlSample.Text = "재고조정";
            }

            if (drItemRow["Bjhd_BaeSong"].ToString() == "0")
            {
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name_Send"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "1")
            {
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "2")
            {
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }
            else if (drItemRow["Bjhd_BaeSong"].ToString() == "3")
            {
                ltlBaesongSend.Text = drItemRow["Bjhd0_Name"].ToString().Trim();
            }

            try
            {
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
                for (int i = 1; i <= 11; i++)
                {
                    //((Literal)e.Item.FindControl("ltlOpenChat" + i)).Text = openChat;
                }
                ((Literal)e.Item.FindControl("ltlOpenChat11")).Text = openChat;

                lnkSubView.OnClientClick = "location.href=\"/Page/Order_" + preVal + ".aspx?mode=view2&param_date=" + drItemRow["bjhd_date"].ToString() + "&param_times=" + drItemRow["bjhd_times"].ToString() + "&param_mainbuyer=" + drItemRow["bjhd_mainbuyer"].ToString() + "&param_sample=" + drItemRow["bjhd_sample"].ToString() + "\"; return false;";
            }
            catch { }

            if (this.cbSizeDetail.Checked)
            {
                ListView lvDetail = (ListView)e.Item.FindControl("lvDetail");

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" select * from " + preVal + "BLJU where Blju_Date = '" + drItemRow["Bjhd_Date"].ToString() + "' and Blju_Times = '" + drItemRow["Bjhd_Times"].ToString() + "' and Blju_MainBuyer = '" + drItemRow["Bjhd_MainBuyer"].ToString() + "' and Blju_Sample = '" + drItemRow["Bjhd_Sample"].ToString() + "' order by Blju_Line ");

                DataSet ds = stData.GetDataSet(sb.ToString());

                DataSet dsSize = new DataSet();
                DataTable _table = new DataTable("sizeTable");

                dsSize.Tables.Add(_table);

                dsSize.Tables[0].Columns.Add("StyleNox", typeof(string));
                dsSize.Tables[0].Columns.Add("StyleSize", typeof(string));
                dsSize.Tables[0].Columns.Add("SizeQty", typeof(string));
                dsSize.Tables[0].Columns.Add("SumNet", typeof(string));

                double sumCnt = 0;

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    for (int i = 1; i <= 17; i++)
                    {
                        string num = (i < 10) ? "0" + i.ToString() : i.ToString();

                        int qty = StCommon.ToInt(ds.Tables[0].Rows[k]["Blju_Qty" + num].ToString(), 0);

                        if (qty > 0)
                        {
                            DataRow dr = dsSize.Tables[0].NewRow();

                            dr[0] = ds.Tables[0].Rows[k]["Blju_StyleNox"].ToString();
                            dr[1] = st.GetSizeInfoNum(preVal, i);
                            dr[2] = ds.Tables[0].Rows[k]["Blju_Qty" + num].ToString();
                            dr[3] = ds.Tables[0].Rows[k]["Blju_Dnga" + num].ToString();

                            dsSize.Tables[0].Rows.Add(dr);

                            sumCnt += StCommon.ToDouble(ds.Tables[0].Rows[k]["Blju_Qty" + num].ToString(), 0);
                        }
                    }

                    if (ds.Tables[0].Rows[k]["Blju_Line"].ToString() == "999")
                    {
                        DataRow dr = dsSize.Tables[0].NewRow();

                        dr[0] = ds.Tables[0].Rows[k]["Blju_StyleNox"].ToString();
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = ds.Tables[0].Rows[k]["Blju_JustAmount"].ToString();

                        dsSize.Tables[0].Rows.Add(dr);

                        sumCnt += StCommon.ToDouble(ds.Tables[0].Rows[k]["Blju_JustAmount"].ToString(), 0);
                    }
                }

                lvDetail.DataSource = dsSize;
                lvDetail.DataBind();

                try
                {
                    ((Literal)lvDetail.FindControl("ltlSumCnt")).Text = GetAmountFormat(sumCnt);
                    ((Literal)lvDetail.FindControl("ltlSumNet")).Text = GetAmountFormat(drItemRow["Bjhd_NetAmount"].ToString());
                }
                catch { }
            }
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMainList();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string appKind = "-거래명세서현황조회";
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
}