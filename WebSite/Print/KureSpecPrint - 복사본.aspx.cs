using System;
using System.Data;

using FirstOrder.Util;
using System.Text;
using FirstOrder.Data;

public partial class Print_KureSpecPrint : System.Web.UI.Page
{
    private StDataCommon stData = new StDataCommon();
    private StCommon st = new StCommon();
    private WorkHistory whis = new WorkHistory();
    private string preVal = "";

    private int totCount = 0;

    private string blju_date = "";
    private string blju_times = "";
    private string blju_mainbuyer = "";
    private string blju_sample = "";

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

        st.GetComInfo(preVal);
        st.GetSizeInfo(preVal);

        this.ltlComInfo1.Text = st.Cominfo1;
        this.ltlComInfo2.Text = st.Cominfo2;
        this.ltlComInfo3.Text = st.Cominfo3;
        this.ltlComInfo4.Text = st.Cominfo4;
        this.ltlComInfo5.Text = st.Cominfo5;
        this.ltlComInfo6.Text = st.Cominfo6;
        this.ltlComInfo7.Text = st.Cominfo7;
        this.ltlComInfo8.Text = st.Cominfo8;

        try
        {
            blju_mainbuyer = Server.HtmlEncode(Request["blju_mainbuyer"].Trim());
            blju_date = Server.HtmlEncode(Request["blju_date"].Trim());
            blju_times = Server.HtmlEncode(Request["blju_times"].Trim());
            blju_sample = Server.HtmlEncode(Request["blju_sample"].Trim());

            this.hidMainBuyer.Value = blju_mainbuyer;
            this.hidDate.Value = blju_date;
            this.hidTimes.Value = blju_times;
            this.hidSample.Value = blju_sample;

            string urlDetail = "/Print/KureSpecPrint.aspx?blju_date=" + blju_date + "&blju_times=" + blju_times + "&blju_mainbuyer=" + blju_mainbuyer + "&blju_sample=" + blju_sample;
            string url = @"http://" + Request["HTTP_HOST"] + urlDetail;
            
            //btnPrint2.OnClientClick = "return printPdf('" + url + "');";

            //btnPrint2.Visible = false;

            if (MemberData.GetLoginSID("LoginID") == "ZQ")
            {
                //btnPrint2.Visible = true;
            }
        }
        catch { }
        
        this.ltlSerial.Text = "<span class=\"font14 fontColor1\">No.</span> " + blju_date + " " + blju_times;        
        this.ltlPrintDay.Text = DateTime.Now.ToShortDateString() + " " + String.Format("{0:HH:mm}", DateTime.Now);

        string whereQry = " where Bjhd_MainBuyer = '" + blju_mainbuyer + "' ";
        whereQry = StCommon.MakeSearchQry("Bjhd_Date", blju_date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Times", blju_times, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Sample", blju_sample, "S", whereQry);

        StringBuilder sb = new StringBuilder();
        
        sb.AppendLine(" SELECT (select kure_sangho from gblKURE where kure_code = a.Bjhd_MainBuyer) as Bjhd_MainBuyerNm,*");
        sb.AppendLine(" ,(select Bjhd2_SongJangNox from tblBJHD2 where isnull(Bjhd2_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd2_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd2_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd2_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd2_SongJangNox ");
        sb.AppendLine(" ,(select sum(Blju_QtyTotal) from " + preVal + "BLJU where isnull(Blju_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Blju_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Blju_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Blju_Sample,'') = isnull(a.Bjhd_Sample,'')) as Blju_QtyTotal ");
        sb.AppendLine(" FROM " + preVal + "BJHD a " + whereQry);
        
        DataSet ds = stData.GetDataSet(sb.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.ltlNapmDate.Text = ds.Tables[0].Rows[0]["Bjhd_NapmDate"].ToString();
            this.ltlMainBuyerNm.Text = ds.Tables[0].Rows[0]["Bjhd_MainBuyerNm"].ToString();
            this.ltlHapAmount.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Bjhd_HapAmount"]);
            this.ltlQtyTotal.Text = GetAmountFormat(ds.Tables[0].Rows[0]["Blju_QtyTotal"]);
            string etc = ds.Tables[0].Rows[0]["Bjhd_Remark"].ToString();
            string songJangNox = ds.Tables[0].Rows[0]["Bjhd2_SongJangNox"].ToString().Trim();
            
            if (songJangNox != "")
            {
                etc = "운송장번호: " + songJangNox + "<br>" + etc;
            }

            this.ltlEtc.Text = etc;
        }

        DataTable dt = new DataTable("BLJUTABLE");
        dt.Columns.Add(new DataColumn("num", typeof(int)));
        dt.Columns.Add(new DataColumn("style", typeof(string)));
        dt.Columns.Add(new DataColumn("size", typeof(string)));
        dt.Columns.Add(new DataColumn("unit", typeof(string)));
        dt.Columns.Add(new DataColumn("qty", typeof(string)));
        dt.Columns.Add(new DataColumn("dnga", typeof(string)));
        dt.Columns.Add(new DataColumn("dngasum", typeof(string)));

        string qry = " select * from " + preVal + "BLJU where Blju_Date = '" + blju_date + "' and Blju_Times = '" + blju_times + "' and Blju_MainBuyer = '" + blju_mainbuyer + "' and Blju_Sample = '" + blju_sample + "' order by Blju_Line ";
        ds = stData.GetDataSet(qry);

        int listNo = 1;
        double netAmount = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            double line = StCommon.ToDouble(ds.Tables[0].Rows[i]["Blju_Line"].ToString(), 0);
            string style = ds.Tables[0].Rows[i]["Blju_StyleNox"].ToString();
            double justAmount = StCommon.ToDouble(ds.Tables[0].Rows[i]["Blju_JustAmount"].ToString(), 0);

            if (line == 999) // 선불운임
            {
                dt.Rows.Add(listNo, "선불운임", "", "", "", "", GetAmountFormat(justAmount));
                listNo = listNo + 1;
                netAmount = netAmount + justAmount;
            }
            else
            {
                for (int k = 1; k <= 17; k++)
                {
                    string number = (k.ToString().Length == 1) ? "0" + k.ToString() : k.ToString();

                    double qty = StCommon.ToDouble(ds.Tables[0].Rows[i]["Blju_Qty" + number].ToString(), 0);
                    double dnga = StCommon.ToDouble(ds.Tables[0].Rows[i]["Blju_Dnga" + number].ToString(), 0);
                    
                    if (qty > 0)
                    {
                        dt.Rows.Add(listNo, style, GetSizeName(k), "PCS", GetAmountFormat(qty), GetAmountFormat(dnga), GetAmountFormat(qty * dnga));
                        listNo = listNo + 1;
                        netAmount = netAmount + (qty * dnga);
                    }
                }
            }
        }

        int emptyNum = 0;
        if (listNo < 30)
        {
            emptyNum = 30 - listNo;

            DataTable dtE = new DataTable("EMPTYTABLE");
            dtE.Columns.Add(new DataColumn("num", typeof(int)));

            for (int i = 1; i <= emptyNum; i++)
            {
                dtE.Rows.Add(i);
            }

            this.lvEmpty.DataSource = dtE;
            this.lvEmpty.DataBind();
        }

        this.ltlNetAmount.Text = GetAmountFormat(netAmount);

        totCount = dt.Rows.Count;

        this.lvList.DataSource = dt;
        this.lvList.DataBind();
    }

    private string GetSizeName(int num)
    {
        string result = "";

        switch (num)
        {
            case 1: result = st.SizeName1 + ((st.SizeNum1 != "") ? "(" + st.SizeNum1 + ")" : ""); break;
            case 2: result = st.SizeName2 + ((st.SizeNum2 != "") ? "(" + st.SizeNum2 + ")" : ""); break;
            case 3: result = st.SizeName3 + ((st.SizeNum3 != "") ? "(" + st.SizeNum3 + ")" : ""); break;
            case 4: result = st.SizeName4 + ((st.SizeNum4 != "") ? "(" + st.SizeNum4 + ")" : ""); break;
            case 5: result = st.SizeName5 + ((st.SizeNum5 != "") ? "(" + st.SizeNum5 + ")" : ""); break;
            case 6: result = st.SizeName6 + ((st.SizeNum6 != "") ? "(" + st.SizeNum6 + ")" : ""); break;
            case 7: result = st.SizeName7 + ((st.SizeNum7 != "") ? "(" + st.SizeNum7 + ")" : ""); break;
            case 8: result = st.SizeName8 + ((st.SizeNum8 != "") ? "(" + st.SizeNum8 + ")" : ""); break;
            case 9: result = st.SizeName9 + ((st.SizeNum9 != "") ? "(" + st.SizeNum9 + ")" : ""); break;
            case 10: result = st.SizeName10 + ((st.SizeNum10 != "") ? "(" + st.SizeNum10 + ")" : ""); break;
            case 11: result = st.SizeName11 + ((st.SizeNum11 != "") ? "(" + st.SizeNum11 + ")" : ""); break;
            case 12: result = st.SizeName12 + ((st.SizeNum12 != "") ? "(" + st.SizeNum12 + ")" : ""); break;
            case 13: result = st.SizeName13; break;
            case 14: result = st.SizeName14; break;
            case 15: result = st.SizeName15; break;
            case 16: result = st.SizeName16; break;
            case 17: result = st.SizeName17; break;

            default: result = ""; break;
        }

        return result;
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