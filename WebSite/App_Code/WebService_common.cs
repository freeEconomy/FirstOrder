using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

using FirstOrder.Data;
using FirstOrder.Util;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

/// <summary>
/// WebService의 요약 설명입니다.
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
[System.Web.Script.Services.ScriptService]
public class WebService_common : System.Web.Services.WebService
{
    public class NoticeDataStat
    {
        public string totalcount { get; set; }
        public string totalpage { get; set; }
        public List<NoticeData> NoticeInfo { get; set; }
    }

    public class NoticeData
    {
        public string idx { get; set; }
        public string memoday { get; set; }
        public string memo { get; set; }
        public string isnotice { get; set; }
        public string filename { get; set; }
    }

    public class OrderDataStat
    {
        public string totalcount { get; set; }
        public string totalpage { get; set; }
        public List<OrderData> OrderInfo { get; set; }
    }

    public class OrderData
    {
        public string date { get; set; }
        public string times { get; set; }
        public string mainbuyer { get; set; }
        public string sample { get; set; }
        public string styleNox { get; set; }
        public string bonsaCheck { get; set; }
        public string daeRiReadOk { get; set; }
    }

    public class SelectOption
    {
        public string optValue { get; set; }
        public string optText { get; set; }
        public string optSelect { get; set; }
    }

    public class JegoInfo
    {
        public string trader { get; set; }
        public string styleNo { get; set; }
        public string size01 { get; set; }
        public string size02 { get; set; }
        public string size03 { get; set; }
        public string size04 { get; set; }
        public string size05 { get; set; }
        public string size06 { get; set; }
        public string size07 { get; set; }
        public string size08 { get; set; }
        public string size09 { get; set; }
        public string size10 { get; set; }
        public string size11 { get; set; }
        public string size12 { get; set; }
        public string size13 { get; set; }
        public string size14 { get; set; }
        public string size15 { get; set; }
        public string size16 { get; set; }
        public string size17 { get; set; }
        public string jego01 { get; set; }
        public string jego02 { get; set; }
        public string jego03 { get; set; }
        public string jego04 { get; set; }
        public string jego05 { get; set; }
        public string jego06 { get; set; }
        public string jego07 { get; set; }
        public string jego08 { get; set; }
        public string jego09 { get; set; }
        public string jego10 { get; set; }
        public string jego11 { get; set; }
        public string jego12 { get; set; }
        public string jego13 { get; set; }
        public string jego14 { get; set; }
        public string jego15 { get; set; }
        public string jego16 { get; set; }
        public string jego17 { get; set; }
        public string jegototal { get; set; }
    }

    public class ProductStat
    {
        public string totalcount { get; set; }
        public string totalpage { get; set; }
        public List<ProductInfo> StyleInfo { get; set; }
    }

    public class ProductInfo
    {
        public string JegoQty01 { get; set; }
        public string JegoQty02 { get; set; }
        public string JegoQty03 { get; set; }
        public string JegoQty04 { get; set; }
        public string JegoQty05 { get; set; }
        public string JegoQty06 { get; set; }
        public string JegoQty07 { get; set; }
        public string JegoQty08 { get; set; }
        public string JegoQty09 { get; set; }
        public string JegoQty10 { get; set; }
        public string JegoQty11 { get; set; }
        public string JegoQty12 { get; set; }
        public string JegoQty13 { get; set; }
        public string JegoQty14 { get; set; }
        public string JegoQty15 { get; set; }
        public string JegoQty16 { get; set; }
        public string JegoQty17 { get; set; }
        public string JegoQtyTotal { get; set; }
        public string StyleNox { get; set; }
        public string MainName { get; set; }
        public string SubName { get; set; }
        public string SpecColor { get; set; }
        public string LowPrice { get; set; }
        public string JustPrice { get; set; }
        public string BigSizePrice { get; set; }
        public string SizeBoxQty { get; set; }
        public string BoxQty { get; set; }
        public string IsDuple { get; set; }
        public string Msg { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string sizeName1 { get; set; }
        public string sizeName2 { get; set; }
        public string sizeName3 { get; set; }
        public string sizeName4 { get; set; }
        public string sizeName5 { get; set; }
        public string sizeName6 { get; set; }
        public string sizeName7 { get; set; }
        public string sizeName8 { get; set; }
        public string sizeName9 { get; set; }
        public string sizeName10 { get; set; }
        public string sizeName11 { get; set; }
        public string sizeName12 { get; set; }
        public string sizeName13 { get; set; }
        public string sizeName14 { get; set; }
        public string sizeName15 { get; set; }
        public string sizeName16 { get; set; }
        public string sizeName17 { get; set; }
    }

    public class OrderApplayData
    {
        public string date { get; set; }
        public string time { get; set; }
        public string kure { get; set; }
        public string sample { get; set; }
        public string line { get; set; }
        public string product { get; set; }
        public string jego01 { get; set; }
        public string jego02 { get; set; }
        public string jego03 { get; set; }
        public string jego04 { get; set; }
        public string jego05 { get; set; }
        public string jego06 { get; set; }
        public string jego07 { get; set; }
        public string jego08 { get; set; }
        public string jego09 { get; set; }
        public string jego10 { get; set; }
        public string jego11 { get; set; }
        public string jego12 { get; set; }
        public string jego13 { get; set; }
        public string jego14 { get; set; }
        public string jego15 { get; set; }
        public string jego16 { get; set; }
        public string jego17 { get; set; }
        public string ordertotal { get; set; }
        public string netAmount { get; set; }
        public string vatAmount { get; set; }
        public string hapAmount { get; set; }
        public string firstDate { get; set; }
        public string lastDate { get; set; }
    }

    public class OrderProcessData
    {
        public string dngaLowPrice { get; set; }
        public string dngaJustPrice { get; set; }
        public string dngaBigSizePrice { get; set; }
        public string dngaSizeBoxQty { get; set; }
        public string dngaBoxQty { get; set; }
        public string justAmount { get; set; }
        public string justPrice { get; set; }
        public string dnga01 { get; set; }
        public string dnga02 { get; set; }
        public string dnga03 { get; set; }
        public string dnga04 { get; set; }
        public string dnga05 { get; set; }
        public string dnga06 { get; set; }
        public string dnga07 { get; set; }
        public string dnga08 { get; set; }
        public string dnga09 { get; set; }
        public string dnga10 { get; set; }
        public string dnga11 { get; set; }
        public string dnga12 { get; set; }
        public string dnga13 { get; set; }
        public string dnga14 { get; set; }
        public string dnga15 { get; set; }
        public string dnga16 { get; set; }
        public string dnga17 { get; set; }
    }

    public class OrderResult
    {
        public string totalcount { get; set; }
        public string msg { get; set; }
    }

    public class MessageInfo
    {
        public string mesg_bonsa_daeri { get; set; }
        public string mesg_message { get; set; }
        public string createDate { get; set; }
        public string isBonsaRead { get; set; }
    }

    public class AsData
    {
        public string bnpmCode { get; set; }
        public string bnpmQty01 { get; set; }
        public string bnpmQty02 { get; set; }
        public string bnpmQty03 { get; set; }
        public string bnpmQty04 { get; set; }
        public string bnpmQty05 { get; set; }
        public string bnpmQty06 { get; set; }
        public string bnpmQty07 { get; set; }
        public string bnpmQty08 { get; set; }
        public string bnpmQty09 { get; set; }
        public string bnpmQty10 { get; set; }
        public string bnpmQty11 { get; set; }
        public string bnpmQty12 { get; set; }
        public string bnpmQty13 { get; set; }
        public string bnpmQty14 { get; set; }
        public string bnpmQty15 { get; set; }
        public string bnpmQty16 { get; set; }
        public string bnpmQty17 { get; set; }
        public string bnpmQtyTotal { get; set; }
        public string bnpmUsedRemark { get; set; }
        public string bnpmReasonRemark { get; set; }
        public string bnpmImageFile { get; set; }
        public string bnpmFileLine { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    
    public class AsBljuData
    {
        public string BljuQty01 { get; set; }
        public string BljuQty02 { get; set; }
        public string BljuQty03 { get; set; }
        public string BljuQty04 { get; set; }
        public string BljuQty05 { get; set; }
        public string BljuQty06 { get; set; }
        public string BljuQty07 { get; set; }
        public string BljuQty08 { get; set; }
        public string BljuQty09 { get; set; }
        public string BljuQty10 { get; set; }
        public string BljuQty11 { get; set; }
        public string BljuQty12 { get; set; }
        public string BljuQty13 { get; set; }
        public string BljuQty14 { get; set; }
        public string BljuQty15 { get; set; }
        public string BljuQty16 { get; set; }
        public string BljuQty17 { get; set; }
        public string BljuQtyTotal { get; set; }
        public string BljuDate { get; set; }
        public string BljuTimes { get; set; }
        public string BljuMainBuyer { get; set; }
        public string BljuSample { get; set; }
        public string BljuStyleNox { get; set; }
        public string JustAmount { get; set; }
        public string VatAmount { get; set; }
        public string HapAmount { get; set; }
        public string Etc { get; set; }
        public string sizeName1 { get; set; }
        public string sizeName2 { get; set; }
        public string sizeName3 { get; set; }
        public string sizeName4 { get; set; }
        public string sizeName5 { get; set; }
        public string sizeName6 { get; set; }
        public string sizeName7 { get; set; }
        public string sizeName8 { get; set; }
        public string sizeName9 { get; set; }
        public string sizeName10 { get; set; }
        public string sizeName11 { get; set; }
        public string sizeName12 { get; set; }
        public string sizeName13 { get; set; }
        public string sizeName14 { get; set; }
        public string sizeName15 { get; set; }
        public string sizeName16 { get; set; }
        public string sizeName17 { get; set; }
    }

    public class AsBljuStat
    {
        public string totalcount { get; set; }
        public string totalpage { get; set; }
        public List<AsBljuData> BljuData { get; set; }
    }
    
    public class AsResult
    {
        public string totalcount { get; set; }
        public string msg { get; set; }
        public string date { get; set; }
        public string times { get; set; }
        public string mainbuyer { get; set; }
        public string sample { get; set; }
    }

    public WebService_common()
    {
        //디자인된 구성 요소를 사용하는 경우 다음 줄의 주석 처리를 제거합니다. 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public int UpdateKureMyungPrint(string param_mainbuyer, string param_date, string param_times, string param_sample, string paramValue)
    {
        StDataCommon stData = new StDataCommon();
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        string blju_mainbuyer = StCommon.ReplaceSQ(param_mainbuyer);
        string blju_date = StCommon.ReplaceSQ(param_date);
        string blju_times = StCommon.ReplaceSQ(param_times);
        string blju_sample = StCommon.ReplaceSQ(param_sample);

        string whereQry = " where Bjhd_MainBuyer = '" + blju_mainbuyer + "' ";
        whereQry = StCommon.MakeSearchQry("Bjhd_Date", blju_date, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Times", blju_times, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("Bjhd_Sample", blju_sample, "S", whereQry);

        string qry = " UPDATE " + preVal + "BJHD SET Bjhd_KureMyung_Print = '" + paramValue + "' " + whereQry;
        stData.GetExecuteNonQry(qry);

        WorkHistory whis = new WorkHistory();
        whis.InsertWork("거래명세서", "인쇄", qry);

        return 1;
    }

    [WebMethod(EnableSession = true)]
    public string GetNoticeList(string paramDateS, string paramDateE, string paramContent, string paramPage, string paramPageSize)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        List<NoticeData> lstSelect = new List<NoticeData>();

        string whereQryTot = "";
        string whereQry = "";
        
        string orderQry = " ORDER BY [isNotice] desc, memoday DESC, u DESC ";

        int page = StCommon.ToInt(paramPage, 1);
        int pageSize = StCommon.ToInt(paramPageSize, 20);

        int totalCount = 0;
        int totalPage = 0;

        string iodateS = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateS));
        string iodateE = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateE));
        string memo = StCommon.ReplaceSQ(paramContent);

        whereQry = StCommon.MakeSearchQry("memoday", iodateS, iodateE, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("[memo]", memo, "%", whereQry);

        whereQryTot = whereQry;

        StDataCommon stData = new StDataCommon();

        string qryT = "SELECT Count(*) as cnt FROM " + preVal + "NOTICE WHERE (u>0) " + whereQryTot;
        using (IDataReader drT = stData.GetDataReader(qryT))
        {
            if (drT.Read())
            {
                totalCount = Convert.ToInt32(drT["cnt"]);
            }
            drT.Close();
        }
        totalPage = Convert.ToInt32((totalCount - 1) / pageSize) + 1;

        string pagingQry = string.Format(" AND (u Not In (SELECT Top {0} u FROM " + preVal + "NOTICE WHERE (u>0) {1} {2}))", (page - 1) * pageSize, whereQryTot, orderQry);

        using (IDataReader dr = stData.GetDataReader("SELECT Top " + pageSize + " * FROM " + preVal + "NOTICE WHERE (u>0) " + whereQryTot + pagingQry + orderQry))
        {

            while (dr.Read())
            {
                NoticeData nom = new NoticeData();

                nom.idx = dr["u"].ToString().Trim();
                nom.memoday = dr["memoday"].ToString().Trim();
                nom.memo = StCommon.CutTitle(StCommon.RemoveHtmlTags(dr["memo"].ToString().Trim()), 150);
                nom.filename = dr["filename"].ToString().Trim();
                nom.isnotice = dr["isnotice"].ToString().Trim();

                lstSelect.Add(nom);
            }
            dr.Close();
        }

        NoticeDataStat pDataStat = new NoticeDataStat();
        pDataStat.totalcount = totalCount.ToString();
        pDataStat.totalpage = totalPage.ToString();
        pDataStat.NoticeInfo = lstSelect;

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(pDataStat);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetNoticeDetailData(string paramIdx)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        List<NoticeData> lstSelect = new List<NoticeData>();

        string idx = StCommon.ReplaceSQ(paramIdx);

        StDataCommon stData = new StDataCommon();

        using (IDataReader dr = stData.GetDataReader("SELECT * FROM " + preVal + "NOTICE WHERE (u=" + idx + ")"))
        {
            while (dr.Read())
            {
                NoticeData nom = new NoticeData();

                nom.idx = dr["u"].ToString().Trim();
                nom.memoday = dr["memoday"].ToString().Trim();
                nom.memo = Server.HtmlDecode(dr["memo"].ToString().Trim());
                nom.filename = dr["filename"].ToString().Trim();
                nom.isnotice = dr["isnotice"].ToString().Trim();

                lstSelect.Add(nom);
            }
            dr.Close();
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetTrader(string dValue, string whereQry)
    {
        List<SelectOption> lstSelect = new List<SelectOption>();

        SelectOption spBlank = new SelectOption();
        spBlank.optValue = "";
        spBlank.optText = "";
        spBlank.optSelect = "";
        lstSelect.Add(spBlank);

        StDataCommon stData = new StDataCommon();

        string qry = " select * from gblKURE " + whereQry + " order by Kure_Sangho ";

        IDataReader dr = stData.GetDataReader(qry);

        while (dr.Read())
        {
            string _selected = "";

            string _dValue = dr["Kure_Code"].ToString().Trim();
            string _dText = dr["Kure_Sangho"].ToString().Trim();
            if (_dValue == dValue)
                _selected = "selected";

            SelectOption sp = new SelectOption();
            sp.optValue = _dValue;
            sp.optText = _dText;
            sp.optSelect = _selected;

            lstSelect.Add(sp);
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetStyleNox(string dValue, string whereQry)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        List<SelectOption> lstSelect = new List<SelectOption>();

        SelectOption spBlank = new SelectOption();
        spBlank.optValue = "";
        spBlank.optText = "";
        spBlank.optSelect = "";
        lstSelect.Add(spBlank);

        StDataCommon stData = new StDataCommon();

        string qry = " select * from " + preVal + "DNGA " + whereQry + " order by Dnga_StyleNox ";
        
        IDataReader dr = stData.GetDataReader(qry);

        while (dr.Read())
        {
            string _selected = "";
            
            string _dValue = dr["Dnga_StyleNox"].ToString().Trim();
            string _dText = _dValue;
            if (_dValue == dValue)
                _selected = "selected";

            SelectOption sp = new SelectOption();
            sp.optValue = _dValue;
            sp.optText = _dText;
            sp.optSelect = _selected;

            lstSelect.Add(sp);
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetMarkJeogoList(string paramStyleNox)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();
        string whereQry = "";
        string styleNox = StCommon.ReplaceSQ(paramStyleNox);

        char[] delimiter = ",".ToCharArray();
        string[] strArray = styleNox.ToString().Trim().Split(delimiter);

        if (strArray.Length > 1)
        {
            string nameAll = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] != "")
                {
                    nameAll += ",'" + strArray[i].Trim() + "'";
                }
            }

            if (nameAll != "")
            {
                nameAll = ("[" + nameAll).Replace("[,", "");
                whereQry = StCommon.MakeSearchQry("Jego_StyleNox", nameAll, "IN", whereQry);
            }
        }
        else
        {
            whereQry = StCommon.MakeSearchQry("Jego_StyleNox", styleNox, "%", whereQry);
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select Jego_StyleNox ");
        sb.AppendLine(" ,Jego_Qty01 ");
        sb.AppendLine(" ,Jego_Qty02 ");
        sb.AppendLine(" ,Jego_Qty03 ");
        sb.AppendLine(" ,Jego_Qty04 ");
        sb.AppendLine(" ,Jego_Qty05 ");
        sb.AppendLine(" ,Jego_Qty06 ");
        sb.AppendLine(" ,Jego_Qty07 ");
        sb.AppendLine(" ,Jego_Qty08 ");
        sb.AppendLine(" ,Jego_Qty09 ");
        sb.AppendLine(" ,Jego_Qty10 ");
        sb.AppendLine(" ,Jego_Qty11 ");
        sb.AppendLine(" ,Jego_Qty12 ");
        sb.AppendLine(" ,Jego_Qty13 ");
        sb.AppendLine(" ,Jego_Qty14 ");
        sb.AppendLine(" ,Jego_Qty15 ");
        sb.AppendLine(" ,Jego_Qty16 ");
        sb.AppendLine(" ,Jego_Qty17 ");
        sb.AppendLine(" ,Jego_QtyTotal ");
        sb.AppendLine(" from View_" + preVal + "JEGO_Summary where 1=1 " + whereQry);
        sb.AppendLine(" order by Jego_StyleNox ");

        List<JegoInfo> lstSelect = new List<JegoInfo>();
        
        string qry = sb.ToString();

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                JegoInfo ji = new JegoInfo();
                ji.size01 = st.SizeName1;
                ji.size02 = st.SizeName2;
                ji.size03 = st.SizeName3;
                ji.size04 = st.SizeName4;
                ji.size05 = st.SizeName5;
                ji.size06 = st.SizeName6;
                ji.size07 = st.SizeName7;
                ji.size08 = st.SizeName8;
                ji.size09 = st.SizeName9;
                ji.size10 = st.SizeName10;
                ji.size11 = st.SizeName11;
                ji.size12 = st.SizeName12;
                ji.size13 = st.SizeName13;
                ji.size14 = st.SizeName14;
                ji.size15 = st.SizeName15;
                ji.size16 = st.SizeName16;
                ji.size17 = st.SizeName17;
                ji.jego01 = GetAmountFormat(dr["Jego_Qty01"].ToString().Trim());
                ji.jego02 = GetAmountFormat(dr["Jego_Qty02"].ToString().Trim());
                ji.jego03 = GetAmountFormat(dr["Jego_Qty03"].ToString().Trim());
                ji.jego04 = GetAmountFormat(dr["Jego_Qty04"].ToString().Trim());
                ji.jego05 = GetAmountFormat(dr["Jego_Qty05"].ToString().Trim());
                ji.jego06 = GetAmountFormat(dr["Jego_Qty06"].ToString().Trim());
                ji.jego07 = GetAmountFormat(dr["Jego_Qty07"].ToString().Trim());
                ji.jego08 = GetAmountFormat(dr["Jego_Qty08"].ToString().Trim());
                ji.jego09 = GetAmountFormat(dr["Jego_Qty09"].ToString().Trim());
                ji.jego10 = GetAmountFormat(dr["Jego_Qty10"].ToString().Trim());
                ji.jego11 = GetAmountFormat(dr["Jego_Qty11"].ToString().Trim());
                ji.jego12 = GetAmountFormat(dr["Jego_Qty12"].ToString().Trim());
                ji.jego13 = GetAmountFormat(dr["Jego_Qty13"].ToString().Trim());
                ji.jego14 = GetAmountFormat(dr["Jego_Qty14"].ToString().Trim());
                ji.jego15 = GetAmountFormat(dr["Jego_Qty15"].ToString().Trim());
                ji.jego16 = GetAmountFormat(dr["Jego_Qty16"].ToString().Trim());
                ji.jego17 = GetAmountFormat(dr["Jego_Qty17"].ToString().Trim());
                ji.jegototal = GetAmountFormat(dr["Jego_QtyTotal"].ToString().Trim());
                ji.styleNo = dr["Jego_StyleNox"].ToString().Trim();

                lstSelect.Add(ji);
            }
            dr.Close();
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetJeogoList(string paramTrader, string paramStyleNox)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        string kurecode = MemberData.GetLoginSID("KureCode");

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();
        string whereQry = "";
        string styleNox = StCommon.ReplaceSQ(paramStyleNox);

        char[] delimiter = ",".ToCharArray();
        string[] strArray = styleNox.ToString().Trim().Split(delimiter);

        if (strArray.Length > 1)
        {
            string nameAll = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] != "")
                {
                    nameAll += ",'" + strArray[i].Trim() + "'";
                }
            }

            if (nameAll != "")
            {
                nameAll = ("[" + nameAll).Replace("[,", "");
                whereQry = StCommon.MakeSearchQry("Jego_StyleNox", nameAll, "IN", whereQry);
            }
        }
        else
        {
            whereQry = StCommon.MakeSearchQry("Jego_StyleNox", styleNox, "%", whereQry);
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select Jego_MainBuyer,(select kure_sangho from gblKURE where kure_code = a.Jego_MainBuyer) as Jego_MainBuyerName,Jego_StyleNox ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty01),0) as Jego_Qty01 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty02),0) as Jego_Qty02 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty03),0) as Jego_Qty03 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty04),0) as Jego_Qty04 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty05),0) as Jego_Qty05 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty06),0) as Jego_Qty06 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty07),0) as Jego_Qty07 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty08),0) as Jego_Qty08 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty09),0) as Jego_Qty09 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty10),0) as Jego_Qty10 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty11),0) as Jego_Qty11 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty12),0) as Jego_Qty12 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty13),0) as Jego_Qty13 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty14),0) as Jego_Qty14 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty15),0) as Jego_Qty15 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty16),0) as Jego_Qty16 ");
        sb.AppendLine(" ,isnull(sum(Jego_Qty17),0) as Jego_Qty17 ");
        sb.AppendLine(" ,isnull(sum(Jego_QtyTotal),0) as Jego_QtyTotal, Max(convert(nvarchar(10),Jego_CreateDate,120)) as JEGO_CreateDate ");
        sb.AppendLine(" from " + preVal + "JEGO a ");
        sb.AppendLine(" inner join gblKURE b ");
        sb.AppendLine(" on a.Jego_MainBuyer = b.kure_code ");
        sb.AppendLine(" inner join " + preVal + "DNGA c ");
        sb.AppendLine(" on a.Jego_StyleNox = c.Dnga_StyleNox ");
        sb.AppendLine(" where ((isnull(b.Kure_JegoView,'') <> 'N' and isnull(c.Dnga_JegoView,'') <> 'N') or (a.Jego_MainBuyer = '" + kurecode + "')) " + whereQry);
        sb.AppendLine(" group by Jego_MainBuyer,Jego_StyleNox order by Jego_MainBuyer,Jego_StyleNox ");

        List<JegoInfo> lstSelect = new List<JegoInfo>();

        string qry = sb.ToString();

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                JegoInfo ji = new JegoInfo();
                ji.trader = dr["Jego_MainBuyerName"].ToString().Trim();
                ji.styleNo = dr["Jego_StyleNox"].ToString().Trim();
                ji.size01 = st.SizeName1;
                ji.size02 = st.SizeName2;
                ji.size03 = st.SizeName3;
                ji.size04 = st.SizeName4;
                ji.size05 = st.SizeName5;
                ji.size06 = st.SizeName6;
                ji.size07 = st.SizeName7;
                ji.size08 = st.SizeName8;
                ji.size09 = st.SizeName9;
                ji.size10 = st.SizeName10;
                ji.size11 = st.SizeName11;
                ji.size12 = st.SizeName12;
                ji.size13 = st.SizeName13;
                ji.size14 = st.SizeName14;
                ji.size15 = st.SizeName15;
                ji.size16 = st.SizeName16;
                ji.size17 = st.SizeName17;
                ji.jego01 = GetAmountFormat(dr["Jego_Qty01"].ToString().Trim());
                ji.jego02 = GetAmountFormat(dr["Jego_Qty02"].ToString().Trim());
                ji.jego03 = GetAmountFormat(dr["Jego_Qty03"].ToString().Trim());
                ji.jego04 = GetAmountFormat(dr["Jego_Qty04"].ToString().Trim());
                ji.jego05 = GetAmountFormat(dr["Jego_Qty05"].ToString().Trim());
                ji.jego06 = GetAmountFormat(dr["Jego_Qty06"].ToString().Trim());
                ji.jego07 = GetAmountFormat(dr["Jego_Qty07"].ToString().Trim());
                ji.jego08 = GetAmountFormat(dr["Jego_Qty08"].ToString().Trim());
                ji.jego09 = GetAmountFormat(dr["Jego_Qty09"].ToString().Trim());
                ji.jego10 = GetAmountFormat(dr["Jego_Qty10"].ToString().Trim());
                ji.jego11 = GetAmountFormat(dr["Jego_Qty11"].ToString().Trim());
                ji.jego12 = GetAmountFormat(dr["Jego_Qty12"].ToString().Trim());
                ji.jego13 = GetAmountFormat(dr["Jego_Qty13"].ToString().Trim());
                ji.jego14 = GetAmountFormat(dr["Jego_Qty14"].ToString().Trim());
                ji.jego15 = GetAmountFormat(dr["Jego_Qty15"].ToString().Trim());
                ji.jego16 = GetAmountFormat(dr["Jego_Qty16"].ToString().Trim());
                ji.jego17 = GetAmountFormat(dr["Jego_Qty17"].ToString().Trim());
                ji.jegototal = GetAmountFormat(dr["Jego_QtyTotal"].ToString().Trim());

                lstSelect.Add(ji);
            }
            dr.Close();
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetStyleList(string paramDate, string paramTimes, string paramKure, string paramSample, string paramStyleNox, string paramPage, string paramPageSize)
    {
        bool usePaging = true;

        string pagingQry = "";
        string orderQry = " ORDER BY Jego_StyleNox ";

        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        int page = StCommon.ToInt(paramPage, 1);
        int pageSize = StCommon.ToInt(paramPageSize, 20);

        if (page == 0 && pageSize == 0)
        {
            usePaging = false;
        }

        int totalCount = 0;
        int totalPage = 0;

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();

        List<ProductInfo> lstSelect = new List<ProductInfo>();

        string whereQry = "";
        string styleNox = StCommon.ReplaceSQ(paramStyleNox);

        string areaGubun = StCommon.GetAreaGubun(preVal, paramKure);

        if (preVal == "tbl")
        {
            if (areaGubun == "2.대리점") // 대리점은 TB- 주문못함.
                whereQry = " and Jego_StyleNox not like 'TB-%' ";
        }

        whereQry += " and (Jego_StyleNox like '%" + styleNox + "%' or Jego_StyleNox in (select Dnga_StyleNox from " + preVal + "DNGA where Dnga_SubName like '%" + styleNox + "%')) ";

        if (usePaging == true)
        {
            string qryT = "SELECT Count(*) as cnt FROM View_" + preVal + "JEGO_Summary WHERE (1=1) " + whereQry;
            using (IDataReader drT = stData.GetDataReader(qryT))
            {
                if (drT.Read())
                {
                    totalCount = Convert.ToInt32(drT["cnt"]);
                }
                drT.Close();
            }
            totalPage = Convert.ToInt32((totalCount - 1) / pageSize) + 1;
        }

        pagingQry = string.Format(" AND (Jego_StyleNox Not In (SELECT Top {0} Jego_StyleNox FROM View_" + preVal + "JEGO_Summary WHERE (1=1) {1} {2}))", (page - 1) * pageSize, whereQry, orderQry);

        string qry = " select Top " + pageSize + " isnull((select Blju_Line from " + preVal + "BLJU where Blju_Date = '" + paramDate + "' and Blju_Times = '" + paramTimes + "' and Blju_MainBuyer = '" + paramKure + "' and Blju_Sample = '" + paramSample + "' and Blju_StyleNox = a.Jego_StyleNox),'') AS Blju_Line, * from View_" + preVal + "JEGO_Summary a where 1=1 " + whereQry + pagingQry + orderQry;

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                string Kind = "1";

                string MainName = "";
                string SubName = "";
                string SpecColor = "";
                string LowPrice = "";
                string JustPrice = "";
                string BigSizePrice = "";
                string SizeBoxQty = "";
                string BoxQty = "";

                string IsDuple = "";
                double chkBoxQty = 0;

                if (StCommon.ToInt(dr["Blju_Line"].ToString().Trim(), 0) > 0)
                {
                    IsDuple = dr["Blju_Line"].ToString();
                }

                qry = " select Dnga_Pants,Dnga_MainName,Dnga_SubName,Dnga_SpecColor,isnull(Dnga_LowPrice,0) as Dnga_LowPrice,isnull(Dnga_JustPrice,0) as Dnga_JustPrice,isnull(Dnga_BigSizePrice,0) as Dnga_BigSizePrice,Dnga_SizeBoxQty,Dnga_BoxQty from " + preVal + "DNGA where Dnga_StyleNox = '" + dr["Jego_StyleNox"].ToString() + "' ";
                DataSet ds = stData.GetDataSet(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Kind = (ds.Tables[0].Rows[0]["Dnga_Pants"].ToString().Trim() == "P") ? "2" : "1";

                    MainName = ds.Tables[0].Rows[0]["Dnga_MainName"].ToString().Trim();
                    SubName = ds.Tables[0].Rows[0]["Dnga_SubName"].ToString().Trim();
                    SpecColor = ds.Tables[0].Rows[0]["Dnga_SpecColor"].ToString();
                    LowPrice = ds.Tables[0].Rows[0]["Dnga_LowPrice"].ToString();
                    JustPrice = ds.Tables[0].Rows[0]["Dnga_JustPrice"].ToString();
                    BigSizePrice = ds.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString();
                    SizeBoxQty = ds.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString();
                    BoxQty = ds.Tables[0].Rows[0]["Dnga_BoxQty"].ToString();
                    chkBoxQty = StCommon.ToDouble(ds.Tables[0].Rows[0]["Dnga_BoxQty"].ToString(), 0);
                }

                ProductInfo pi = new ProductInfo();

                pi.JegoQty01 = GetAmountFormat(dr["Jego_Qty01"]);
                pi.JegoQty02 = GetAmountFormat(dr["Jego_Qty02"]);
                pi.JegoQty03 = GetAmountFormat(dr["Jego_Qty03"]);
                pi.JegoQty04 = GetAmountFormat(dr["Jego_Qty04"]);
                pi.JegoQty05 = GetAmountFormat(dr["Jego_Qty05"]);
                pi.JegoQty06 = GetAmountFormat(dr["Jego_Qty06"]);
                pi.JegoQty07 = GetAmountFormat(dr["Jego_Qty07"]);
                pi.JegoQty08 = GetAmountFormat(dr["Jego_Qty08"]);
                pi.JegoQty09 = GetAmountFormat(dr["Jego_Qty09"]);
                pi.JegoQty10 = GetAmountFormat(dr["Jego_Qty10"]);
                pi.JegoQty11 = GetAmountFormat(dr["Jego_Qty11"]);
                pi.JegoQty12 = GetAmountFormat(dr["Jego_Qty12"]);
                pi.JegoQty13 = GetAmountFormat(dr["Jego_Qty13"]);
                pi.JegoQty14 = GetAmountFormat(dr["Jego_Qty14"]);
                pi.JegoQty15 = GetAmountFormat(dr["Jego_Qty15"]);
                pi.JegoQty16 = GetAmountFormat(dr["Jego_Qty16"]);
                pi.JegoQty17 = GetAmountFormat(dr["Jego_Qty17"]);
                pi.JegoQtyTotal = GetAmountFormat(dr["Jego_QtyTotal"]);
                pi.MainName = Regex.Replace(MainName, @"\n", " ");
                pi.SubName = Regex.Replace(SubName, @"\n", " ");
                pi.SpecColor = Regex.Replace(SpecColor, @"\n", " ");
                pi.JustPrice = GetAmountFormat(JustPrice);
                pi.LowPrice = GetAmountFormat(LowPrice);
                pi.BigSizePrice = GetAmountFormat(BigSizePrice);
                pi.SizeBoxQty = SizeBoxQty;
                pi.BoxQty = BoxQty;

                pi.StyleNox = dr["Jego_StyleNox"].ToString();

                pi.IsDuple = IsDuple;

                if (chkBoxQty == 0)
                {
                    pi.Msg = "BOX적용 수량이 없습니다.";
                }
                else
                {
                    double justPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_JustPrice"]);
                    double lowPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_LowPrice"]);

                    // (낱장단가 - BOX단가 / BOX단가) * 100
                    double dcPercent = Math.Round(((justPrice - lowPrice) / lowPrice) * 100, 1);

                    pi.Msg = "BOX수량: " + chkBoxQty + " PCS (" + dcPercent + " % 할인됩니다.)";
                }

                pi.Width = 0; // 리스트에서 이미지 크기 계산하면 너무 오래걸려서 줌창에서 처리하도록 함.
                pi.Height = 0;

                string sizeName1 = "";
                string sizeName2 = "";
                string sizeName3 = "";
                string sizeName4 = "";
                string sizeName5 = "";
                string sizeName6 = "";
                string sizeName7 = "";
                string sizeName8 = "";
                string sizeName9 = "";
                string sizeName10 = "";
                string sizeName11 = "";
                string sizeName12 = "";
                string sizeName13 = "";
                string sizeName14 = "";
                string sizeName15 = "";
                string sizeName16 = "";
                string sizeName17 = "";

                qry = " SELECT * FROM gblCOMMON WHERE Common_Key = 'T0511' and substring(Common_Code,1,3) = '" + preVal + "' ";
                using (IDataReader dr2 = stData.GetDataReader(qry))
                {
                    while (dr2.Read())
                    {
                        string common_num = dr2["Common_Code"].ToString().Trim().Substring(3, 2);

                        switch (common_num)
                        {
                            case "01": sizeName1 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "02": sizeName2 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "03": sizeName3 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "04": sizeName4 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "05": sizeName5 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "06": sizeName6 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "07": sizeName7 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "08": sizeName8 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "09": sizeName9 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "10": sizeName10 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "11": sizeName11 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "12": sizeName12 = dr2["Common_CodeName"].ToString().Trim() + "(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                            case "13": sizeName13 = dr2["Common_CodeName"].ToString().Trim(); break;
                            case "14": sizeName14 = dr2["Common_CodeName"].ToString().Trim(); break;
                            case "15": sizeName15 = dr2["Common_CodeName"].ToString().Trim(); break;
                            case "16": sizeName16 = dr2["Common_CodeName"].ToString().Trim(); break;
                            case "17": sizeName17 = dr2["Common_CodeName"].ToString().Trim(); break;
                        }
                    }
                    dr2.Close();
                }

                pi.sizeName1 = sizeName1;
                pi.sizeName2 = sizeName2;
                pi.sizeName3 = sizeName3;
                pi.sizeName4 = sizeName4;
                pi.sizeName5 = sizeName5;
                pi.sizeName6 = sizeName6;
                pi.sizeName7 = sizeName7;
                pi.sizeName8 = sizeName8;
                pi.sizeName9 = sizeName9;
                pi.sizeName10 = sizeName10;
                pi.sizeName11 = sizeName11;
                pi.sizeName12 = sizeName12;
                pi.sizeName13 = sizeName13;
                pi.sizeName14 = sizeName14;
                pi.sizeName15 = sizeName15;
                pi.sizeName16 = sizeName16;
                pi.sizeName17 = sizeName17;

                lstSelect.Add(pi);
            }
            dr.Close();
        }
        
        ProductStat pDataStat = new ProductStat();
        pDataStat.totalcount = totalCount.ToString();
        pDataStat.totalpage = totalPage.ToString();
        pDataStat.StyleInfo = lstSelect;

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(pDataStat);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string MessageUpdate(string paramDate, string paramTimes, string paramKure, string paramSample)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();

        List<MessageInfo> lstSelect = new List<MessageInfo>();


        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from " + preVal + "MESG ");
        sb.Append(" where Mesg_Date='" + paramDate + "' and Mesg_Times='" + paramTimes + "' and Mesg_MainBuyer='" + paramKure + "' and Mesg_Sample='" + paramSample + "' and Mesg_DaeRiReadOk='1' ");
        sb.Append(" order by Mesg_CreateDateTimes ");

        string qry = sb.ToString();
        
        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                MessageInfo mi = new MessageInfo();

                mi.mesg_bonsa_daeri = dr["Mesg_BonSa_DaeRi"].ToString();
                mi.mesg_message = StCommon.Chr2Br(dr["Mesg_Message"].ToString());
                string mesg_createdatetimes = dr["Mesg_CreateDateTimes"].ToString();
                DateTime createDate = Convert.ToDateTime(mesg_createdatetimes.Substring(0, 19));
                mi.createDate = createDate.ToLongDateString() + " " + createDate.ToLongTimeString();
                mi.isBonsaRead = GetAmountFormat(dr["Mesg_BonSaReadOk"]);

                lstSelect.Add(mi);
            }
            dr.Close();
        }

        // 메시지 읽음 (리스트 보여준 이후에는 읽음 처리)
        sb = new StringBuilder();
        sb.Append(" update " + preVal + "MESG set Mesg_DaeRiReadOk = '0' ");
        sb.Append(" where Mesg_Date='" + paramDate + "' and Mesg_Times='" + paramTimes + "' and Mesg_MainBuyer='" + paramKure + "' and Mesg_Sample='" + paramSample + "' and Mesg_BonSa_DaeRi = '0' ");

        stData.GetExecuteNonQry(sb.ToString());

        WorkHistory whis = new WorkHistory();
        whis.InsertWork("대화방", "읽음", sb.ToString());

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetOrderList(string paramDateS, string paramDateE, string paramPage, string paramPageSize)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        List<OrderData> lstSelect = new List<OrderData>();

        string whereQryTot = "";
        string whereQry = "";

        string orderQry = " ORDER BY Bjhd_Bonsa_Check, bjhd_date desc, bjhd_times desc, bjhd_mainbuyer ";

        int page = StCommon.ToInt(paramPage, 1);
        int pageSize = StCommon.ToInt(paramPageSize, 20);

        int totalCount = 0;
        int totalPage = 0;

        string iodateS = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateS));
        string iodateE = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateE));

        //string kurecode = "YM";// MemberData.GetLoginSID("KureCode");
        string kurecode = MemberData.GetLoginSID("KureCode");

        whereQry = StCommon.MakeSearchQry("bjhd_date", iodateS, iodateE, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("bjhd_mainbuyer", kurecode, "S", whereQry);

        whereQryTot = whereQry;

        StDataCommon stData = new StDataCommon();

        string qrySelect = " (select kure_sangho from gblKURE where kure_code = Bjhd_MainBuyer) as Bjhd_MainBuyerNm,(select count(*) " + preVal + "MESG where Mesg_Date = Bjhd_Date AND Mesg_Times = Bjhd_Times AND Mesg_MainBuyer = Bjhd_MainBuyer AND Mesg_Sample = Bjhd_Sample) msgCnt,*,ISNULL(SQL_BonSaReadOk,0) as BonSaReadOk,ISNULL(SQL_DaeRiReadOk,0) as DaeRiReadOk ";
        qrySelect += " , LTRIM(STUFF((select ',' + Blju_StyleNox from " + preVal + "BLJU where isnull(Blju_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Blju_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Blju_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Blju_Sample,'') = isnull(a.Bjhd_Sample,'') FOR XML PATH('')), 1, 1, '')) as Blju_StyleNox ";
        qrySelect += " , (select Bjhd0_Name from " + preVal + "BJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd0_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd0_Name ";
        qrySelect += " , (select Bjhd0_Name_Send from " + preVal + "BJHD0 where isnull(Bjhd0_Date,'') = isnull(a.Bjhd_Date,'') and isnull(Bjhd0_Times,'') = isnull(a.Bjhd_Times,'') and isnull(Bjhd0_MainBuyer,'') = isnull(a.Bjhd_MainBuyer,'') and isnull(Bjhd0_Sample,'') = isnull(a.Bjhd_Sample,'')) as Bjhd0_Name_Send ";

        string qryTable = " " + preVal + "BJHD a LEFT OUTER JOIN (SELECT Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample,SUM(case when Mesg_BonSa_Daeri = '1' then Mesg_BonSaReadOk else 0 end) AS SQL_BonSaReadOk,SUM(case when Mesg_BonSa_Daeri = '0' then Mesg_DaeRiReadOk else 0 end) AS SQL_DaeRiReadOk FROM " + preVal + "MESG GROUP BY Mesg_Date,Mesg_Times,Mesg_MainBuyer,Mesg_Sample) t1 ";
        qryTable += " ON t1.Mesg_Date = Bjhd_Date AND t1.Mesg_Times = Bjhd_Times AND t1.Mesg_MainBuyer = Bjhd_MainBuyer AND t1.Mesg_Sample = Bjhd_Sample ";
        
        string qryT = "SELECT Count(*) as cnt FROM " + qryTable + " WHERE (1=1) " + whereQryTot;
        using (IDataReader drT = stData.GetDataReader(qryT))
        {
            if (drT.Read())
            {
                totalCount = Convert.ToInt32(drT["cnt"]);
            }
            drT.Close();
        }
        totalPage = Convert.ToInt32((totalCount - 1) / pageSize) + 1;

        string pagingQry = string.Format(" AND (u Not In (SELECT Top {0} u FROM " + qryTable + " WHERE (1=1) {1} {2}))", (page - 1) * pageSize, whereQryTot, orderQry);

        using (IDataReader dr = stData.GetDataReader("SELECT Top " + pageSize + " " + qrySelect + " FROM " + qryTable + " WHERE (1=1) " + whereQryTot + pagingQry + orderQry))
        {
            while (dr.Read())
            {
                OrderData ord = new OrderData();

                ord.date = dr["bjhd_date"].ToString().Trim();
                ord.times = dr["bjhd_times"].ToString().Trim();
                ord.mainbuyer = dr["bjhd_mainbuyer"].ToString().Trim();
                ord.sample = dr["bjhd_sample"].ToString().Trim();

                char[] delimiter = ",".ToCharArray();
                string[] strArray = dr["Blju_StyleNox"].ToString().Trim().Split(delimiter);

                if (strArray.Length > 0)
                {
                    ord.styleNox = strArray[0];
                    if (strArray.Length > 1)
                    {
                        ord.styleNox = ord.styleNox + " 외 " + (strArray.Length - 1).ToString() + "건";
                    }
                }

                if (dr["Bjhd_Bonsa_Check"].ToString() == "0")
                {
                    ord.bonsaCheck = "발주의뢰 중";
                }
                else if (dr["Bjhd_Bonsa_Check"].ToString() == "1")
                {
                    ord.bonsaCheck = "발주의뢰 완료";
                }
                else if (dr["Bjhd_Bonsa_Check"].ToString() == "2")
                {
                    ord.bonsaCheck = "본사주문 완료";
                }
                else if (dr["Bjhd_Bonsa_Check"].ToString() == "Y")
                {
                    ord.bonsaCheck = "배송준비 중";
                }
                else if (dr["Bjhd_Bonsa_Check"].ToString() == "Z")
                {
                    ord.bonsaCheck = "배송완료";
                }

                ord.daeRiReadOk = StCommon.ToInt(dr["DaeRiReadOk"].ToString(), 0).ToString();

                lstSelect.Add(ord);
            }
            dr.Close();
        }

        OrderDataStat pDataStat = new OrderDataStat();
        pDataStat.totalcount = totalCount.ToString();
        pDataStat.totalpage = totalPage.ToString();
        pDataStat.OrderInfo = lstSelect;

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(pDataStat);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string OrderApply(string paramMode, string paramDate, string paramTimes, string paramKure, string paramSample, string paramLine, string paramProduct, string paramEtc, string paramOrder1, string paramOrder2, string paramOrder3, string paramOrder4, string paramOrder5, string paramOrder6, string paramOrder7, string paramOrder8, string paramOrder9, string paramOrder10, string paramOrder11, string paramOrder12, string paramOrder13, string paramOrder14, string paramOrder15, string paramOrder16, string paramOrder17)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        string mode = StCommon.ReplaceSQ(paramMode);
        string date = StCommon.ReplaceSQ(paramDate);
        string time = StCommon.ReplaceSQ(paramTimes);
        string kure = StCommon.ReplaceSQ(paramKure);
        string sample = StCommon.ReplaceSQ(paramSample);
        string product = StCommon.ReplaceSQ(paramProduct);
        string etc = StCommon.ReplaceSQ(paramEtc);
        string line = StCommon.ReplaceSQ(paramLine);

        string[] OrderArray = { "", paramOrder1, paramOrder2, paramOrder3, paramOrder4, paramOrder5, paramOrder6, paramOrder7, paramOrder8, paramOrder9, paramOrder10, paramOrder11, paramOrder12, paramOrder13, paramOrder14, paramOrder15, paramOrder16, paramOrder17, "" }; // 첫값은 비우고, 사이즈17개+Total 까지 19개
        
        OrderApplayData oad = new OrderApplayData();
        OrderProcessData opd = new OrderProcessData();

        oad.date = date;
        oad.time = time;
        oad.kure = kure;
        oad.line = line;
        oad.product = product;

        string[] JegoSet = StCommon.JegoSet(product, mode, date, time, kure, line);

        string failMsg = "";

        int result = 0;

        // 동일 스타일 중복 체크
        string existLine = StCommon.DupleStyleCheck(date, time, kure, product, line, mode);
        if (existLine != "")
        {
            failMsg = "" + existLine + "번 라인에 해당스타일이 이미 등록되어 있습니다!";
            result = 0;
            
            oad.jego01 = JegoSet[1];
            oad.jego02 = JegoSet[2];
            oad.jego03 = JegoSet[31];
            oad.jego04 = JegoSet[4];
            oad.jego05 = JegoSet[5];
            oad.jego06 = JegoSet[6];
            oad.jego07 = JegoSet[7];
            oad.jego08 = JegoSet[8];
            oad.jego09 = JegoSet[9];
            oad.jego10 = JegoSet[10];
            oad.jego11 = JegoSet[11];
            oad.jego12 = JegoSet[12];
            oad.jego13 = JegoSet[13];
            oad.jego14 = JegoSet[14];
            oad.jego15 = JegoSet[15];
            oad.jego16 = JegoSet[16];
            oad.jego17 = JegoSet[17];
            
            SetJustPrice(kure, product, OrderArray, opd, oad);
        }
        else
        {
            double order1 = StCommon.ToDouble(OrderArray[1], 0);
            double order2 = StCommon.ToDouble(OrderArray[2], 0);
            double order3 = StCommon.ToDouble(OrderArray[3], 0);
            double order4 = StCommon.ToDouble(OrderArray[4], 0);
            double order5 = StCommon.ToDouble(OrderArray[5], 0);
            double order6 = StCommon.ToDouble(OrderArray[6], 0);
            double order7 = StCommon.ToDouble(OrderArray[7], 0);
            double order8 = StCommon.ToDouble(OrderArray[8], 0);
            double order9 = StCommon.ToDouble(OrderArray[9], 0);
            double order10 = StCommon.ToDouble(OrderArray[10], 0);
            double order11 = StCommon.ToDouble(OrderArray[11], 0);
            double order12 = StCommon.ToDouble(OrderArray[12], 0);
            double order13 = StCommon.ToDouble(OrderArray[13], 0);
            double order14 = StCommon.ToDouble(OrderArray[14], 0);
            double order15 = StCommon.ToDouble(OrderArray[15], 0);
            double order16 = StCommon.ToDouble(OrderArray[16], 0);
            double order17 = StCommon.ToDouble(OrderArray[17], 0);
            double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12 + order13 + order14 + order15 + order16 + order17;
            double miniQty = StCommon.GetMinimumQty(product);

            string jegoOverChk = "";

            if (orderTotal < miniQty)
            {
                jegoOverChk = "minimum";
            }
            else
            {
                jegoOverChk = JegoOverCheck(mode, oad, OrderArray);
            }

            SetJustPrice(kure, product, OrderArray, opd, oad);

            if (jegoOverChk == "")
            {
                OrderData_common order = new OrderData_common();
                
                order.Date = date;
                order.Time = time;
                order.Kure = kure;
                order.Baesong = "";
                order.BaesongName = "";
                if (mode == "mod")
                {
                    order.Line = line;
                }
                order.Etc = etc;
                if (mode != "mod")
                {
                    order.Sdate = DateTime.Now;
                    order.Ssawon = MemberData.GetLoginSID("LoginID");
                }
                order.Mdate = DateTime.Now;
                order.Msawon = MemberData.GetLoginSID("LoginID");
                order.JustPrice = opd.justPrice;
                order.JustAmount = opd.justAmount;
                order.Product = product;
                order.Order1 = order1;
                order.Order2 = order2;
                order.Order3 = order3;
                order.Order4 = order4;
                order.Order5 = order5;
                order.Order6 = order6;
                order.Order7 = order7;
                order.Order8 = order8;
                order.Order9 = order9;
                order.Order10 = order10;
                order.Order11 = order11;
                order.Order12 = order12;
                order.Order13 = order13;
                order.Order14 = order14;
                order.Order15 = order15;
                order.Order16 = order16;
                order.Order17 = order17;
                order.OrderTotal = orderTotal;
                order.Dnga1 = opd.dnga01;
                order.Dnga2 = opd.dnga02;
                order.Dnga3 = opd.dnga03;
                order.Dnga4 = opd.dnga04;
                order.Dnga5 = opd.dnga05;
                order.Dnga6 = opd.dnga06;
                order.Dnga7 = opd.dnga07;
                order.Dnga8 = opd.dnga08;
                order.Dnga9 = opd.dnga09;
                order.Dnga10 = opd.dnga10;
                order.Dnga11 = opd.dnga11;
                order.Dnga12 = opd.dnga12;
                order.Dnga13 = opd.dnga13;
                order.Dnga14 = opd.dnga14;
                order.Dnga15 = opd.dnga15;
                order.Dnga16 = opd.dnga16;
                order.Dnga17 = opd.dnga17;
                order.DngaLowPrice = opd.dngaLowPrice;
                order.DngaJustPrice = opd.dngaJustPrice;
                order.DngaBigSizePrice = opd.dngaBigSizePrice;
                order.DngaSizeBoxQty = StCommon.ToDouble(opd.dngaSizeBoxQty, 0);
                order.DngaBoxQty = StCommon.ToDouble(opd.dngaBoxQty, 0);

                if (mode == "mod")
                {
                    order.UpdateHead();
                    order.UpdateData();
                }
                else
                {
                    order.InsertHead();
                    order.InsertData();
                }

                // 헤더 금액 업데이트
                SetHeadAmount(date, time, kure, "0", oad);

                // 최초등록일자, 최종수정일자 업데이트
                DateUpdate(date, time, kure, "0", oad);

                result = 2;
            }
            else if (jegoOverChk == "over")
            {
                failMsg = "주문량이 재고 수량보다 많습니다.";
                result = 0;
                
                oad.jego01 = JegoSet[1];
                oad.jego02 = JegoSet[2];
                oad.jego03 = JegoSet[31];
                oad.jego04 = JegoSet[4];
                oad.jego05 = JegoSet[5];
                oad.jego06 = JegoSet[6];
                oad.jego07 = JegoSet[7];
                oad.jego08 = JegoSet[8];
                oad.jego09 = JegoSet[9];
                oad.jego10 = JegoSet[10];
                oad.jego11 = JegoSet[11];
                oad.jego12 = JegoSet[12];
                oad.jego13 = JegoSet[13];
                oad.jego14 = JegoSet[14];
                oad.jego15 = JegoSet[15];
                oad.jego16 = JegoSet[16];
                oad.jego17 = JegoSet[17];

                SetJustPrice(kure, product, OrderArray, opd, oad);
            }
            else if (jegoOverChk == "zero")
            {
                failMsg = "주문량을 입력해주세요.";
                result = 0;
                
                oad.jego01 = JegoSet[1];
                oad.jego02 = JegoSet[2];
                oad.jego03 = JegoSet[31];
                oad.jego04 = JegoSet[4];
                oad.jego05 = JegoSet[5];
                oad.jego06 = JegoSet[6];
                oad.jego07 = JegoSet[7];
                oad.jego08 = JegoSet[8];
                oad.jego09 = JegoSet[9];
                oad.jego10 = JegoSet[10];
                oad.jego11 = JegoSet[11];
                oad.jego12 = JegoSet[12];
                oad.jego13 = JegoSet[13];
                oad.jego14 = JegoSet[14];
                oad.jego15 = JegoSet[15];
                oad.jego16 = JegoSet[16];
                oad.jego17 = JegoSet[17];
            }
            else if (jegoOverChk == "minimum")
            {
                failMsg = "해당 스타일은 `최소 주문량(" + miniQty + ")`이상을 주문해야 합니다. !!!";
                result = 0;

                oad.jego01 = JegoSet[1];
                oad.jego02 = JegoSet[2];
                oad.jego03 = JegoSet[31];
                oad.jego04 = JegoSet[4];
                oad.jego05 = JegoSet[5];
                oad.jego06 = JegoSet[6];
                oad.jego07 = JegoSet[7];
                oad.jego08 = JegoSet[8];
                oad.jego09 = JegoSet[9];
                oad.jego10 = JegoSet[10];
                oad.jego11 = JegoSet[11];
                oad.jego12 = JegoSet[12];
                oad.jego13 = JegoSet[13];
                oad.jego14 = JegoSet[14];
                oad.jego15 = JegoSet[15];
                oad.jego16 = JegoSet[16];
                oad.jego17 = JegoSet[17];
            }
        }

        or.totalcount = result.ToString();
        or.msg = failMsg;
        
        strJson = jsSer.Serialize(or);

        return strJson;
    }

    private void SetJustPrice(string blju_buyer, string blju_style, string[] OrderArray, OrderProcessData opd, OrderApplayData oad)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();

        // ablDNGA에서 단가를 가져와 합계수량 *단가를 적용
        /*
         gblKURE 테이블에서 
         Kure_PriceGubun 은
        1:하한가(LowPrice)
        2:정상가(JustPrice)
        3:상한가(HighPrice)

        ablDNGA 테이블에서 가격을 Dnga_LowPrice를 쓰는지, Dnga_LowPrice를 쓰는지, Dnga_HighPrice를 쓰는지 정하면 됨
        gblKURE에서 대리점들이 쓰는 코드는 BR로 시작되는 코드만 쓸 것임
        
        Kure_PriceGubun 가 Null인 것은 BR로 시작되는 코드일 경우임. 그럼 2:정상가(JustPrice) 값으로 적용하면 됨.
         */

        string qry = "";
        double price = 0;
        
        string allBoxDnga = "";
        string allZeroDnga = "";

        qry = " SELECT * from gblKURE where kure_code = '" + blju_buyer + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            allBoxDnga = dsK.Tables[0].Rows[0]["Kure_All_BoxDnga"].ToString().Trim();
            allZeroDnga = dsK.Tables[0].Rows[0]["Kure_All_ZeroDnga"].ToString().Trim();
        }
        
        double order1 = StCommon.ToDouble(OrderArray[1], 0);
        double order2 = StCommon.ToDouble(OrderArray[2], 0);
        double order3 = StCommon.ToDouble(OrderArray[3], 0);
        double order4 = StCommon.ToDouble(OrderArray[4], 0);
        double order5 = StCommon.ToDouble(OrderArray[5], 0);
        double order6 = StCommon.ToDouble(OrderArray[6], 0);

        double order7 = StCommon.ToDouble(OrderArray[7], 0);
        double order8 = StCommon.ToDouble(OrderArray[8], 0);
        double order9 = StCommon.ToDouble(OrderArray[9], 0);
        double order10 = StCommon.ToDouble(OrderArray[10], 0);
        double order11 = StCommon.ToDouble(OrderArray[11], 0);
        double order12 = StCommon.ToDouble(OrderArray[12], 0);
        double order13 = StCommon.ToDouble(OrderArray[13], 0);
        double order14 = StCommon.ToDouble(OrderArray[14], 0);
        double order15 = StCommon.ToDouble(OrderArray[15], 0);
        double order16 = StCommon.ToDouble(OrderArray[16], 0);
        double order17 = StCommon.ToDouble(OrderArray[17], 0);
        double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12 + order13 + order14 + order15 + order16 + order17;

        double price1 = 0;
        double price2 = 0;
        double price3 = 0;
        double price4 = 0;
        double price5 = 0;
        double price6 = 0;
        double price7 = 0;
        double price8 = 0;
        double price9 = 0;
        double price10 = 0;
        double price11 = 0;
        double price12 = 0;
        double price13 = 0;
        double price14 = 0;
        double price15 = 0;
        double price16 = 0;
        double price17 = 0;

        qry = " SELECT * from " + preVal + "DNGA where dnga_stylenox = '" + blju_style + "' ";
        DataSet dsD = stData.GetDataSet(qry);

        if (dsD.Tables[0].Rows.Count > 0)
        {
            double boxQty = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_BoxQty"].ToString(), 0);
            double bigSizePrice = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString(), 0);
            double sizeBoxQty = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString(), 0);

            opd.dngaLowPrice = dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString();
            opd.dngaJustPrice = dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString();
            opd.dngaBigSizePrice = dsD.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString();
            opd.dngaSizeBoxQty = dsD.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString();
            opd.dngaBoxQty = dsD.Tables[0].Rows[0]["Dnga_BoxQty"].ToString();

            if (boxQty == 0)
            {
                price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price13 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price14 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price15 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price16 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                price17 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
            }
            else
            {
                if (boxQty <= orderTotal) // 박스가 적용
                {
                    price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price13 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price14 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price15 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price16 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                    price17 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                }
                else
                {
                    price1 = StCommon.ToDouble((order1 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price2 = StCommon.ToDouble((order2 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price3 = StCommon.ToDouble((order3 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price4 = StCommon.ToDouble((order4 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price5 = StCommon.ToDouble((order5 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price6 = StCommon.ToDouble((order6 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price7 = StCommon.ToDouble((order7 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price8 = StCommon.ToDouble((order8 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price9 = StCommon.ToDouble((order9 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price10 = StCommon.ToDouble((order10 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price11 = StCommon.ToDouble((order11 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price12 = StCommon.ToDouble((order12 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price13 = StCommon.ToDouble((order13 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price14 = StCommon.ToDouble((order14 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price15 = StCommon.ToDouble((order15 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price16 = StCommon.ToDouble((order16 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                    price17 = StCommon.ToDouble((order17 >= sizeBoxQty && sizeBoxQty > 0) ? dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString() : dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0);
                }
            }

            // Big사이즈 추가금 (2XL부터 했었는데 의류는 5XL부터 적용되게 변경 20211118
            if (preVal == "abl")
            {
                price7 += bigSizePrice;
                price8 += bigSizePrice;
                price9 += bigSizePrice;
            }
            price10 += bigSizePrice;
            price11 += bigSizePrice;
            price12 += bigSizePrice;
            price13 += bigSizePrice;
            price14 += bigSizePrice;
            price15 += bigSizePrice;
            price16 += bigSizePrice;
            price17 += bigSizePrice;

            if (allBoxDnga == "Y")
            {
                price1 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price2 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price3 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price4 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price5 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price6 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price7 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price8 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price9 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price10 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price11 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price12 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price13 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price14 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price15 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price16 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
                price17 = StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_LowPrice"].ToString(), 0);
            }

            #region ## 의류 부분만 별도 적용(2022-10-03부터)
            if (preVal == "tbl")
            {
                OrderData_tbl order = new OrderData_tbl();
                price1 = order.GetTblPrice(price1, 1, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price2 = order.GetTblPrice(price2, 2, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price3 = order.GetTblPrice(price3, 3, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price4 = order.GetTblPrice(price4, 4, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price5 = order.GetTblPrice(price5, 5, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price6 = order.GetTblPrice(price6, 6, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price7 = order.GetTblPrice(price7, 7, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price8 = order.GetTblPrice(price8, 8, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price9 = order.GetTblPrice(price9, 9, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price10 = order.GetTblPrice(price10, 10, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price11 = order.GetTblPrice(price11, 11, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
                price12 = order.GetTblPrice(price12, 12, StCommon.ToDouble(dsD.Tables[0].Rows[0]["Dnga_JustPrice"].ToString(), 0), orderTotal, allBoxDnga);
            }
            #endregion

            if (allZeroDnga == "Y")
            {
                price1 = 0;
                price2 = 0;
                price3 = 0;
                price4 = 0;
                price5 = 0;
                price6 = 0;
                price7 = 0;
                price8 = 0;
                price9 = 0;
                price10 = 0;
                price11 = 0;
                price12 = 0;
                price13 = 0;
                price14 = 0;
                price15 = 0;
                price16 = 0;
                price17 = 0;
            }
        }

        oad.ordertotal = StCommon.NumberFormat(orderTotal);
        
        double justAmount = (price1 * order1) + (price2 * order2) + (price3 * order3) + (price4 * order4) + (price5 * order5) + (price6 * order6) + (price7 * order7) + (price8 * order8) + (price9 * order9) + (price10 * order10) + (price11 * order11) + (price12 * order12) + (price13 * order13) + (price14 * order14) + (price15 * order15) + (price16 * order16) + (price17 * order17);

        opd.justAmount = (justAmount).ToString();
        opd.justPrice = (justAmount / orderTotal).ToString();
        opd.dnga01 = (price1).ToString();
        opd.dnga02 = (price2).ToString();
        opd.dnga03 = (price3).ToString();
        opd.dnga04 = (price4).ToString();
        opd.dnga05 = (price5).ToString();
        opd.dnga06 = (price6).ToString();
        opd.dnga07 = (price7).ToString();
        opd.dnga08 = (price8).ToString();
        opd.dnga09 = (price9).ToString();
        opd.dnga10 = (price10).ToString();
        opd.dnga11 = (price11).ToString();
        opd.dnga12 = (price12).ToString();
        opd.dnga13 = (price13).ToString();
        opd.dnga14 = (price14).ToString();
        opd.dnga15 = (price15).ToString();
        opd.dnga16 = (price16).ToString();
        opd.dnga17 = (price17).ToString();
    }

    private void SetHeadAmount(string blju_date, string blju_times, string blju_mainbuyer, string blju_sample, OrderApplayData oad)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string allNoTax = "";

        string qry = " SELECT * from gblKURE where kure_code = '" + blju_mainbuyer + "' ";
        DataSet dsK = stData.GetDataSet(qry);

        if (dsK.Tables[0].Rows.Count > 0)
        {
            allNoTax = dsK.Tables[0].Rows[0]["Kure_All_NoTax"].ToString().Trim();
        }
        
        qry = " select isnull(sum(Blju_JustAmount),0) FROM " + preVal + "BLJU a where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' ";
        double bljuTotal = Convert.ToDouble(stData.GetDataOne(qry));

        double net = bljuTotal;
        double vat = net * (0.1);

        if (allNoTax == "Y")
        {
            vat = 0;
        }

        double hap = net + vat;

        qry = " UPDATE " + preVal + "BJHD SET Bjhd_NetAmount = '" + net.ToString() + "',Bjhd_VatAmount = '" + vat.ToString() + "',Bjhd_HapAmount = '" + hap.ToString() + "',Bjhd_ModifyDate=getDate(),Bjhd_ModifySaWon = '" + MemberData.GetLoginSID("LoginID") + "' where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample = '" + blju_sample + "' ";
        stData.GetExecuteNonQry(qry);

        whis.InsertWork("발주헤더", "금액업데이트", qry);

        oad.netAmount = StCommon.NumberFormat(net);
        oad.vatAmount = StCommon.NumberFormat(vat);
        oad.hapAmount = StCommon.NumberFormat(hap);
    }

    private void DateUpdate(string blju_date, string blju_times, string blju_mainbuyer, string blju_sample, OrderApplayData oad)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();

        string qry = " select * FROM " + preVal + "BJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample = '" + blju_sample + "' ";
        DataSet ds = stData.GetDataSet(qry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            oad.firstDate = ds.Tables[0].Rows[0]["Bjhd_CreateDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_CreateSawon"].ToString();
            oad.lastDate = ds.Tables[0].Rows[0]["Bjhd_ModifyDate"].ToString() + " " + ds.Tables[0].Rows[0]["Bjhd_ModifySaWon"].ToString();
        }
    }

    private string JegoOverCheck(string mode, OrderApplayData oad, string[] OrderArray)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();

        string result = "";

        string date = oad.date;
        string time = oad.time;
        string kure = oad.kure;
        string line = oad.line;

        string product = oad.product;

        string qry = " select * from View_" + preVal + "JEGO_Summary where Jego_StyleNox = '" + product + "' ";
        DataSet dsJ = stData.GetDataSet(qry);

        double jego1 = 0;
        double jego2 = 0;
        double jego3 = 0;
        double jego4 = 0;
        double jego5 = 0;
        double jego6 = 0;
        double jego7 = 0;
        double jego8 = 0;
        double jego9 = 0;
        double jego10 = 0;
        double jego11 = 0;
        double jego12 = 0;
        double jego13 = 0;
        double jego14 = 0;
        double jego15 = 0;
        double jego16 = 0;
        double jego17 = 0;
        double jegoTotal = 0;

        if (dsJ.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsJ.Tables[0].Rows[0];

            jego1 = StCommon.ToDouble(dr["Jego_Qty01"].ToString(), 0);
            jego2 = StCommon.ToDouble(dr["Jego_Qty02"].ToString(), 0);
            jego3 = StCommon.ToDouble(dr["Jego_Qty03"].ToString(), 0);
            jego4 = StCommon.ToDouble(dr["Jego_Qty04"].ToString(), 0);
            jego5 = StCommon.ToDouble(dr["Jego_Qty05"].ToString(), 0);
            jego6 = StCommon.ToDouble(dr["Jego_Qty06"].ToString(), 0);
            jego7 = StCommon.ToDouble(dr["Jego_Qty07"].ToString(), 0);
            jego8 = StCommon.ToDouble(dr["Jego_Qty08"].ToString(), 0);
            jego9 = StCommon.ToDouble(dr["Jego_Qty09"].ToString(), 0);
            jego10 = StCommon.ToDouble(dr["Jego_Qty10"].ToString(), 0);
            jego11 = StCommon.ToDouble(dr["Jego_Qty11"].ToString(), 0);
            jego12 = StCommon.ToDouble(dr["Jego_Qty12"].ToString(), 0);
            jego13 = StCommon.ToDouble(dr["Jego_Qty13"].ToString(), 0);
            jego14 = StCommon.ToDouble(dr["Jego_Qty14"].ToString(), 0);
            jego15 = StCommon.ToDouble(dr["Jego_Qty15"].ToString(), 0);
            jego16 = StCommon.ToDouble(dr["Jego_Qty16"].ToString(), 0);
            jego17 = StCommon.ToDouble(dr["Jego_Qty17"].ToString(), 0);
            jegoTotal = StCommon.ToDouble(dr["Jego_QtyTotal"].ToString(), 0);
        }

        // 재고 (총량 - 주문 누적량)
        if (mode == "mod") // 수정일 경우에는 재고 <= (재고 + 현재 데이터 주문량)
        {
            qry = " select ";
            qry += " isnull(sum(blju_qty01),0) as blju_qty01,isnull(sum(blju_qty02),0) as blju_qty02,isnull(sum(blju_qty03),0) as blju_qty03,isnull(sum(blju_qty04),0) as blju_qty04 ";
            qry += " ,isnull(sum(blju_qty05),0) as blju_qty05,isnull(sum(blju_qty06),0) as blju_qty06,isnull(sum(blju_qty07),0) as blju_qty07,isnull(sum(blju_qty08),0) as blju_qty08 ";
            qry += " ,isnull(sum(blju_qty09),0) as blju_qty09,isnull(sum(blju_qty10),0) as blju_qty10,isnull(sum(blju_qty11),0) as blju_qty11,isnull(sum(blju_qty12),0) as blju_qty12 ";
            qry += " ,isnull(sum(blju_qty13),0) as blju_qty13,isnull(sum(blju_qty14),0) as blju_qty14,isnull(sum(blju_qty15),0) as blju_qty15,isnull(sum(blju_qty16),0) as blju_qty16,isnull(sum(blju_qty17),0) as blju_qty17 ";
            qry += " FROM " + preVal + "BLJU a where blju_stylenox = '" + product + "' and blju_date = '" + date + "' and blju_times = '" + time + "' and blju_mainbuyer = '" + kure + "' and blju_line = '" + line + "' ";
            DataSet ds = stData.GetDataSet(qry);
            DataRow dr = ds.Tables[0].Rows[0];

            jego1 = jego1 + StCommon.ToDouble(dr["blju_qty01"].ToString(), 0);
            jego2 = jego2 + StCommon.ToDouble(dr["blju_qty02"].ToString(), 0);
            jego3 = jego3 + StCommon.ToDouble(dr["blju_qty03"].ToString(), 0);
            jego4 = jego4 + StCommon.ToDouble(dr["blju_qty04"].ToString(), 0);
            jego5 = jego5 + StCommon.ToDouble(dr["blju_qty05"].ToString(), 0);
            jego6 = jego6 + StCommon.ToDouble(dr["blju_qty06"].ToString(), 0);
            jego7 = jego7 + StCommon.ToDouble(dr["blju_qty07"].ToString(), 0);
            jego8 = jego8 + StCommon.ToDouble(dr["blju_qty08"].ToString(), 0);
            jego9 = jego9 + StCommon.ToDouble(dr["blju_qty09"].ToString(), 0);
            jego10 = jego10 + StCommon.ToDouble(dr["blju_qty10"].ToString(), 0);
            jego11 = jego11 + StCommon.ToDouble(dr["blju_qty11"].ToString(), 0);
            jego12 = jego12 + StCommon.ToDouble(dr["blju_qty12"].ToString(), 0);
            jego13 = jego13 + StCommon.ToDouble(dr["blju_qty13"].ToString(), 0);
            jego14 = jego14 + StCommon.ToDouble(dr["blju_qty14"].ToString(), 0);
            jego15 = jego15 + StCommon.ToDouble(dr["blju_qty15"].ToString(), 0);
            jego16 = jego16 + StCommon.ToDouble(dr["blju_qty16"].ToString(), 0);
            jego17 = jego17 + StCommon.ToDouble(dr["blju_qty17"].ToString(), 0);
        }

        double order1 = StCommon.ToDouble(OrderArray[1], 0);
        double order2 = StCommon.ToDouble(OrderArray[2], 0);
        double order3 = StCommon.ToDouble(OrderArray[3], 0);
        double order4 = StCommon.ToDouble(OrderArray[4], 0);
        double order5 = StCommon.ToDouble(OrderArray[5], 0);
        double order6 = StCommon.ToDouble(OrderArray[6], 0);
        double order7 = StCommon.ToDouble(OrderArray[7], 0);
        double order8 = StCommon.ToDouble(OrderArray[8], 0);
        double order9 = StCommon.ToDouble(OrderArray[9], 0);
        double order10 = StCommon.ToDouble(OrderArray[10], 0);
        double order11 = StCommon.ToDouble(OrderArray[11], 0);
        double order12 = StCommon.ToDouble(OrderArray[12], 0);
        double order13 = StCommon.ToDouble(OrderArray[13], 0);
        double order14 = StCommon.ToDouble(OrderArray[14], 0);
        double order15 = StCommon.ToDouble(OrderArray[15], 0);
        double order16 = StCommon.ToDouble(OrderArray[16], 0);
        double order17 = StCommon.ToDouble(OrderArray[17], 0);
        double orderTotal = order1 + order2 + order3 + order4 + order5 + order6 + order7 + order8 + order9 + order10 + order11 + order12 + order13 + order14 + order15 + order16 + order17;

        // 재고 < 주문량 일 경우, 주문량 초과임.
        if ((jego1 < order1) || (jego2 < order2) || (jego3 < order3) || (jego4 < order4) || (jego5 < order5) || (jego6 < order6) || (jego7 < order7) || (jego8 < order8) || (jego9 < order9) || (jego10 < order10) || (jego11 < order11) || (jego12 < order12) || (jego13 < order13) || (jego14 < order14) || (jego15 < order15) || (jego16 < order16) || (jego17 < order17))
        {
            result = "over";
        }

        if (orderTotal == 0)
        {
            result = "zero";
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string OrderDelete(string paramOption, string paramDate, string paramTimes, string paramKure, string paramSample, string paramLine)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }
        
        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string failMsg = "";

        int result = 0;

        string option = StCommon.ReplaceSQ(paramOption);

        string blju_date = StCommon.ReplaceSQ(paramDate);
        string blju_times = StCommon.ReplaceSQ(paramTimes);
        string blju_mainbuyer = StCommon.ReplaceSQ(paramKure);
        string blju_sample = StCommon.ReplaceSQ(paramSample);
        string blju_line = StCommon.ReplaceSQ(paramLine);

        string qry = " select Bjhd_Bonsa_Check FROM " + preVal + "BJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string bjhd_bonsa_check = "";
        if (dsC.Tables[0].Rows.Count > 0)
        {
            bjhd_bonsa_check = dsC.Tables[0].Rows[0][0].ToString();
        }

        string stateMsg = "";
        if (bjhd_bonsa_check == "2")
        {
            stateMsg = "본사주문 완료";
        }
        else if (bjhd_bonsa_check == "Y")
        {
            stateMsg = "배송준비 중";
        }
        else if (bjhd_bonsa_check == "Z")
        {
            stateMsg = "배송완료";
        }

        // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
        if (bjhd_bonsa_check == "0" || bjhd_bonsa_check == "1")
        {
            if (option == "all")
            {
                qry = " DELETE FROM " + preVal + "BLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주", "삭제", qry);
            }
            else
            {
                qry = " DELETE FROM " + preVal + "BLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' and blju_line = '" + blju_line + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주", "삭제", qry);
            }

            qry = " select * FROM " + preVal + "BLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample = '" + blju_sample + "' ";
            DataSet ds = stData.GetDataSet(qry);

            if (ds.Tables[0].Rows.Count == 0) // 발주내역이 하나도 없으면 헤더도 삭제
            {
                qry = " DELETE FROM " + preVal + "BJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주헤더", "삭제", qry);

                qry = " DELETE FROM " + preVal + "BLJU where blju_date = '" + blju_date + "' and blju_times = '" + blju_times + "' and blju_mainbuyer = '" + blju_mainbuyer + "' and blju_sample='" + blju_sample + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주", "삭제", qry);

                qry = " DELETE FROM " + preVal + "BJHD0 where bjhd0_date = '" + blju_date + "' and bjhd0_times = '" + blju_times + "' and bjhd0_mainbuyer = '" + blju_mainbuyer + "' and bjhd0_sample='" + blju_sample + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주배송지", "삭제", qry);

                qry = " DELETE FROM " + preVal + "MESG where Mesg_Date = '" + blju_date + "' and Mesg_Times = '" + blju_times + "' and Mesg_MainBuyer = '" + blju_mainbuyer + "' and Mesg_Sample='" + blju_sample + "' ";
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주대화방", "삭제", qry);
            }

            result = 2;
        }
        else
        {
            failMsg = "삭제할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.";
            result = 0;
        }

        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";
        
        or.totalcount = result.ToString();
        or.msg = failMsg;

        strJson = jsSer.Serialize(or);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string OrderComplete(string paramDate, string paramTimes, string paramKure, string paramSample, string paramBaesong, string paramBaeSongName, string paramEtc)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string failMsg = "";

        int result = 0;

        string date = StCommon.ReplaceSQ(paramDate);
        string time = StCommon.ReplaceSQ(paramTimes);
        string kure = StCommon.ReplaceSQ(paramKure);
        string sample = StCommon.ReplaceSQ(paramSample);
        
        string baesong = StCommon.ReplaceSQ(paramBaesong);
        string baesongname = StCommon.ReplaceSQ(paramBaeSongName);

        string etc = StCommon.ReplaceSQ(paramEtc);

        string qry = " select * from " + preVal + "BLJU where Blju_Date = '" + date + "' and Blju_Times = '" + time + "' and Blju_MainBuyer = '" + kure + "' and Blju_Sample = '" + sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        if (dsC.Tables[0].Rows.Count == 0)
        {
            failMsg = "주문 내역이 없습니다. 상품 선택후 주문량을 입력해주세요.";
            result = 0;
        }
        else
        {
            string nowBonsaCheck = StCommon.GetBonsaCheck(preVal, kure, date, time);
            if (nowBonsaCheck != "" && nowBonsaCheck != "0")
            {
                string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);

                failMsg = "현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.";
                result = 0;
            }
            else
            {
                string whereQry = " where Bjhd_MainBuyer = '" + kure + "' ";
                whereQry = StCommon.MakeSearchQry("Bjhd_Date", date, "S", whereQry);
                whereQry = StCommon.MakeSearchQry("Bjhd_Times", time, "S", whereQry);

                // 주문완료 후 종료  tblBJHD테이블 Bjhd_Bonsa_Check 필드에 '1': 주문완료
                qry = " update " + preVal + "BJHD set Bjhd_Bonsa_Check = '1',Bjhd_BaeSong = '" + baesong + "',Bjhd_BaeSongName = '" + baesongname + "',Bjhd_Remark = '" + etc + "',Bjhd_ModifyDate=getDate(),Bjhd_ModifySaWon = '" + MemberData.GetLoginSID("LoginID") + "' " + whereQry;
                stData.GetExecuteNonQry(qry);

                whis.InsertWork("발주", "주문완료", qry);

                result = 2;
            }
        }

        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        or.totalcount = result.ToString();
        or.msg = failMsg;

        strJson = jsSer.Serialize(or);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string OrderStateEdit(string paramDate, string paramTimes, string paramKure, string paramSample)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string failMsg = "";

        int result = 0;

        string blju_date = StCommon.ReplaceSQ(paramDate);
        string blju_times = StCommon.ReplaceSQ(paramTimes);
        string blju_mainbuyer = StCommon.ReplaceSQ(paramKure);
        string blju_sample = StCommon.ReplaceSQ(paramSample);
        
        string qry = " select Bjhd_Bonsa_Check FROM " + preVal + "BJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string bjhd_bonsa_check = "";
        if (dsC.Tables[0].Rows.Count > 0)
        {
            bjhd_bonsa_check = dsC.Tables[0].Rows[0][0].ToString();
        }

        string stateMsg = "";
        if (bjhd_bonsa_check == "2")
        {
            stateMsg = "본사주문 완료";
        }
        else if (bjhd_bonsa_check == "Y")
        {
            stateMsg = "배송준비 중";
        }
        else if (bjhd_bonsa_check == "Z")
        {
            stateMsg = "배송완료";
        }

        // 대리점은 tblBJHD테이블 Bjhd_Bonsa_Check 필드가 '0', '1'인 것만 처리할 수 있다. (나머지는 수정, 삭제를 할 수 없고 조회만 가능함)
        if (bjhd_bonsa_check == "0" || bjhd_bonsa_check == "1")
        {
            qry = " update " + preVal + "BJHD set Bjhd_Bonsa_Check = '0' where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
            stData.GetExecuteNonQry(qry);

            whis.InsertWork("발주", "발주의뢰중으로 변경", qry);
            
            result = 2;
        }
        else
        {
            failMsg = "수정할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.";
            result = 0;
        }

        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        or.totalcount = result.ToString();
        or.msg = failMsg;

        strJson = jsSer.Serialize(or);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetAsList(string paramDateS, string paramDateE, string paramPage, string paramPageSize)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        List<OrderData> lstSelect = new List<OrderData>();

        string whereQryTot = "";
        string whereQry = "";

        string orderQry = " ORDER BY BnpmH_Bonsa_Check, BnpmH_Date desc, BnpmH_Times desc, BnpmH_Mainbuyer ";

        int page = StCommon.ToInt(paramPage, 1);
        int pageSize = StCommon.ToInt(paramPageSize, 20);

        int totalCount = 0;
        int totalPage = 0;

        string iodateS = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateS));
        string iodateE = StCommon.ConvertMobileDate(StCommon.ReplaceSQ(paramDateE));

        string kurecode = MemberData.GetLoginSID("KureCode");

        whereQry = StCommon.MakeSearchQry("BnpmH_Date", iodateS, iodateE, "S", whereQry);
        whereQry = StCommon.MakeSearchQry("BnpmH_Mainbuyer", kurecode, "S", whereQry);

        whereQryTot = whereQry;

        StDataCommon stData = new StDataCommon();

        string qrySelect = " (select kure_sangho from gblKURE where kure_code = BnpmH_MainBuyer) as BnpmH_MainBuyerNm,* ";
        qrySelect += " , LTRIM(STUFF((select ',' + BnpmD_StyleNox from " + preVal + "BNPMD where isnull(BnpmD_Date,'') = isnull(a.BnpmH_Date,'') and isnull(BnpmD_Times,'') = isnull(a.BnpmH_Times,'') and isnull(BnpmD_MainBuyer,'') = isnull(a.BnpmH_MainBuyer,'') and isnull(BnpmD_Sample,'') = isnull(a.BnpmH_Sample,'') FOR XML PATH('')), 1, 1, '')) as BnpmH_StyleNox ";
        
        string qryTable = " " + preVal + "BNPMH a ";
        
        string qryT = "SELECT Count(*) as cnt FROM " + qryTable + " WHERE (1=1) " + whereQryTot;
        using (IDataReader drT = stData.GetDataReader(qryT))
        {
            if (drT.Read())
            {
                totalCount = Convert.ToInt32(drT["cnt"]);
            }
            drT.Close();
        }
        totalPage = Convert.ToInt32((totalCount - 1) / pageSize) + 1;

        string pagingQry = string.Format(" AND (u Not In (SELECT Top {0} u FROM " + qryTable + " WHERE (1=1) {1} {2}))", (page - 1) * pageSize, whereQryTot, orderQry);

        using (IDataReader dr = stData.GetDataReader("SELECT Top " + pageSize + " " + qrySelect + " FROM " + qryTable + " WHERE (1=1) " + whereQryTot + pagingQry + orderQry))
        {
            while (dr.Read())
            {
                OrderData ord = new OrderData();

                ord.date = dr["BnpmH_Date"].ToString().Trim();
                ord.times = dr["BnpmH_Times"].ToString().Trim();
                ord.mainbuyer = dr["BnpmH_Mainbuyer"].ToString().Trim();
                ord.sample = dr["BnpmH_Sample"].ToString().Trim();

                char[] delimiter = ",".ToCharArray();
                string[] strArray = dr["BnpmH_StyleNox"].ToString().Trim().Split(delimiter);

                if (strArray.Length > 0)
                {
                    ord.styleNox = strArray[0];
                    if (strArray.Length > 1)
                    {
                        //ord.styleNox = ord.styleNox + " 외 " + (strArray.Length - 1).ToString() + "건";
                    }
                }
                
                if (dr["BnpmH_Bonsa_Check"].ToString() == "0" && dr["BnpmH_Bonsa_Check1"].ToString() == "")
                {
                    ord.bonsaCheck = "AS의뢰 중..";
                }
                else if (dr["BnpmH_Bonsa_Check"].ToString() == "1" && dr["BnpmH_Bonsa_Check1"].ToString() == "")
                {
                    ord.bonsaCheck = "AS의뢰 완료";
                }
                else if (dr["BnpmH_Bonsa_Check"].ToString() == "Y" && dr["BnpmH_Bonsa_Check1"].ToString() == "0")
                {
                    ord.bonsaCheck = "본사확인 중..";
                }
                else if (dr["BnpmH_Bonsa_Check"].ToString() == "Y" && dr["BnpmH_Bonsa_Check1"].ToString() == "Y")
                {
                    ord.bonsaCheck = "본사확인 완료";
                }
                else if (dr["BnpmH_Bonsa_Check"].ToString() == "Z" && dr["BnpmH_Bonsa_Check1"].ToString() == "U")
                {
                    ord.bonsaCheck = "확정작업 중..";
                }
                else if (dr["BnpmH_Bonsa_Check"].ToString() == "Z" && dr["BnpmH_Bonsa_Check1"].ToString() == "Z")
                {
                    ord.bonsaCheck = "확정작업 완료";
                }

                lstSelect.Add(ord);
            }
            dr.Close();
        }

        OrderDataStat pDataStat = new OrderDataStat();
        pDataStat.totalcount = totalCount.ToString();
        pDataStat.totalpage = totalPage.ToString();
        pDataStat.OrderInfo = lstSelect;

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(pDataStat);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string GetAsOrderList(string paramStyleNox, string paramPage, string paramPageSize)
    {
        string kurecode = MemberData.GetLoginSID("KureCode");

        bool usePaging = true;

        string pagingQry = "";
        string orderQry = " order by blju_date desc, blju_times desc ";

        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        int page = StCommon.ToInt(paramPage, 1);
        int pageSize = StCommon.ToInt(paramPageSize, 20);

        if (page == 0 && pageSize == 0)
        {
            usePaging = false;
        }

        int totalCount = 0;
        int totalPage = 0;

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();

        List<AsBljuData> lstSelect = new List<AsBljuData>();

        string styleNox = StCommon.ReplaceSQ(paramStyleNox);

        string whereQry = " where blju_mainbuyer = '" + kurecode + "' and blju_styleNox like '%" + styleNox + "%' ";
        whereQry += " and (select count(*) from " + preVal + "BJHD where isnull(BJHD_Date,'') = isnull(a.blju_Date,'') and isnull(BJHD_Times,'') = isnull(a.blju_Times,'') and isnull(BJHD_MainBuyer,'') = isnull(a.blju_MainBuyer,'') and isnull(BJHD_Sample,'') = isnull(a.blju_Sample,'') and (Bjhd_Bonsa_Check = 'Z' AND Bjhd_Bonsa_Check1 IN('X','Z'))) > 0 ";

        if (usePaging == true)
        {
            string qryT = "SELECT Count(*) as cnt FROM " + preVal + "BLJU a " + whereQry;
            using (IDataReader drT = stData.GetDataReader(qryT))
            {
                if (drT.Read())
                {
                    totalCount = Convert.ToInt32(drT["cnt"]);
                }
                drT.Close();
            }
            totalPage = Convert.ToInt32((totalCount - 1) / pageSize) + 1;
        }

        int skip = (page - 1) * pageSize;
        int take = pageSize;
        
        pagingQry = string.Format(" AND (rowNum between {0} and {1}) ", skip + 1, take);
        
        string qry = " select * ";
        qry += " ,(select Bjhd_Remark from " + preVal + "BJHD where isnull(BJHD_Date,'') = isnull(b.blju_Date,'') and isnull(BJHD_Times,'') = isnull(b.blju_Times,'') and isnull(BJHD_MainBuyer,'') = isnull(b.blju_MainBuyer,'') and isnull(BJHD_Sample,'') = isnull(b.blju_Sample,'')) as Etc ";
        qry += " from ( select ROW_NUMBER() OVER (" + orderQry + ") as rowNum,* from " + preVal + "BLJU a " + whereQry + " ) b where 1=1 " + pagingQry + orderQry;

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                AsBljuData pi = new AsBljuData();

                pi.BljuQty01 = GetAmountFormat(dr["blju_Qty01"]);
                pi.BljuQty02 = GetAmountFormat(dr["blju_Qty02"]);
                pi.BljuQty03 = GetAmountFormat(dr["blju_Qty03"]);
                pi.BljuQty04 = GetAmountFormat(dr["blju_Qty04"]);
                pi.BljuQty05 = GetAmountFormat(dr["blju_Qty05"]);
                pi.BljuQty06 = GetAmountFormat(dr["blju_Qty06"]);
                pi.BljuQty07 = GetAmountFormat(dr["blju_Qty07"]);
                pi.BljuQty08 = GetAmountFormat(dr["blju_Qty08"]);
                pi.BljuQty09 = GetAmountFormat(dr["blju_Qty09"]);
                pi.BljuQty10 = GetAmountFormat(dr["blju_Qty10"]);
                pi.BljuQty11 = GetAmountFormat(dr["blju_Qty11"]);
                pi.BljuQty12 = GetAmountFormat(dr["blju_Qty12"]);
                pi.BljuQty13 = GetAmountFormat(dr["blju_Qty13"]);
                pi.BljuQty14 = GetAmountFormat(dr["blju_Qty14"]);
                pi.BljuQty15 = GetAmountFormat(dr["blju_Qty15"]);
                pi.BljuQty16 = GetAmountFormat(dr["blju_Qty16"]);
                pi.BljuQty17 = GetAmountFormat(dr["blju_Qty17"]);
                pi.BljuQtyTotal = GetAmountFormat(dr["blju_QtyTotal"]);

                pi.BljuStyleNox = dr["Blju_styleNox"].ToString();
                pi.BljuDate = dr["Blju_Date"].ToString();
                pi.BljuTimes = dr["Blju_Times"].ToString();
                pi.BljuMainBuyer = dr["Blju_MainBuyer"].ToString();
                pi.BljuSample = dr["Blju_Sample"].ToString();

                double net = StCommon.ToDouble(dr["Blju_JustAmount"].ToString(), 0);
                double vat = StCommon.ToDouble(dr["Blju_JustAmount"].ToString(), 0) * (0.1);
                double hap = net + vat;

                pi.JustAmount = GetAmountFormat(net);
                pi.VatAmount = GetAmountFormat(vat);
                pi.HapAmount = GetAmountFormat(hap);
                pi.Etc = dr["Etc"].ToString();
                
                st = new StCommon(preVal);

                string sizeName1 = "";
                string sizeName2 = "";
                string sizeName3 = "";
                string sizeName4 = "";
                string sizeName5 = "";
                string sizeName6 = "";
                string sizeName7 = "";
                string sizeName8 = "";
                string sizeName9 = "";
                string sizeName10 = "";
                string sizeName11 = "";
                string sizeName12 = "";
                string sizeName13 = "";
                string sizeName14 = "";
                string sizeName15 = "";
                string sizeName16 = "";
                string sizeName17 = "";

                sizeName1 = String.Concat(st.SizeName1, "(", st.SizeNum1, ")");
                sizeName2 = String.Concat(st.SizeName2, "(", st.SizeNum2, ")");
                sizeName3 = String.Concat(st.SizeName3, "(", st.SizeNum3, ")");
                sizeName4 = String.Concat(st.SizeName4, "(", st.SizeNum4, ")");
                sizeName5 = String.Concat(st.SizeName5, "(", st.SizeNum5, ")");
                sizeName6 = String.Concat(st.SizeName6, "(", st.SizeNum6, ")");
                sizeName7 = String.Concat(st.SizeName7, "(", st.SizeNum7, ")");
                sizeName8 = String.Concat(st.SizeName8, "(", st.SizeNum8, ")");
                sizeName9 = String.Concat(st.SizeName9, "(", st.SizeNum9, ")");
                sizeName10 = String.Concat(st.SizeName10, "(", st.SizeNum10, ")");
                sizeName11 = String.Concat(st.SizeName11, "(", st.SizeNum11, ")");
                sizeName12 = String.Concat(st.SizeName12, "(", st.SizeNum12, ")");
                if (preVal == "tbl")
                { }
                else
                {
                    sizeName13 = st.SizeName13;
                    sizeName14 = st.SizeName14;
                    sizeName15 = st.SizeName15;
                    sizeName16 = st.SizeName16;
                    sizeName17 = st.SizeName17;
                }

                pi.sizeName1 = sizeName1;
                pi.sizeName2 = sizeName2;
                pi.sizeName3 = sizeName3;
                pi.sizeName4 = sizeName4;
                pi.sizeName5 = sizeName5;
                pi.sizeName6 = sizeName6;
                pi.sizeName7 = sizeName7;
                pi.sizeName8 = sizeName8;
                pi.sizeName9 = sizeName9;
                pi.sizeName10 = sizeName10;
                pi.sizeName11 = sizeName11;
                pi.sizeName12 = sizeName12;
                pi.sizeName13 = sizeName13;
                pi.sizeName14 = sizeName14;
                pi.sizeName15 = sizeName15;
                pi.sizeName16 = sizeName16;
                pi.sizeName17 = sizeName17;

                lstSelect.Add(pi);
            }
            dr.Close();
        }

        AsBljuStat pDataStat = new AsBljuStat();
        pDataStat.totalcount = totalCount.ToString();
        pDataStat.totalpage = totalPage.ToString();
        pDataStat.BljuData = lstSelect;

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(pDataStat);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetAsDetailList(string paramDate, string paramTimes, string paramMainbuyer, string paramSample)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }
        
        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();
        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select BnpmD_BnpmCode ");
        sb.AppendLine(" ,BnpmD_Qty01 ");
        sb.AppendLine(" ,BnpmD_Qty02 ");
        sb.AppendLine(" ,BnpmD_Qty03 ");
        sb.AppendLine(" ,BnpmD_Qty04 ");
        sb.AppendLine(" ,BnpmD_Qty05 ");
        sb.AppendLine(" ,BnpmD_Qty06 ");
        sb.AppendLine(" ,BnpmD_Qty07 ");
        sb.AppendLine(" ,BnpmD_Qty08 ");
        sb.AppendLine(" ,BnpmD_Qty09 ");
        sb.AppendLine(" ,BnpmD_Qty10 ");
        sb.AppendLine(" ,BnpmD_Qty11 ");
        sb.AppendLine(" ,BnpmD_Qty12 ");
        sb.AppendLine(" ,BnpmD_Qty13 ");
        sb.AppendLine(" ,BnpmD_Qty14 ");
        sb.AppendLine(" ,BnpmD_Qty15 ");
        sb.AppendLine(" ,BnpmD_Qty16 ");
        sb.AppendLine(" ,BnpmD_Qty17 ");
        sb.AppendLine(" ,BnpmD_QtyTotal ");
        sb.AppendLine(" ,BnpmD_UsedRemark ");
        sb.AppendLine(" ,BnpmD_ReasonRemark ");
        sb.AppendLine(" FROM " + preVal + "BNPMD a ");
        sb.AppendLine(" WHERE BnpmD_Date = '" + paramDate + "' and BnpmD_Times = '" + paramTimes + "' and BnpmD_MainBuyer = '" + paramMainbuyer + "' and BnpmD_Sample = '" + paramSample + "' ");
        sb.AppendLine(" Order by BnpmD_StyleNoxLine ");

        List<AsData> lstSelect = new List<AsData>();

        string qry = sb.ToString();

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                AsData ad = new AsData();
                ad.bnpmCode = dr["bnpmd_bnpmCode"].ToString().Trim();
                ad.bnpmQty01 = GetAmountFormat(dr["bnpmd_qty01"].ToString().Trim());
                ad.bnpmQty02 = GetAmountFormat(dr["bnpmd_qty02"].ToString().Trim());
                ad.bnpmQty03 = GetAmountFormat(dr["bnpmd_qty03"].ToString().Trim());
                ad.bnpmQty04 = GetAmountFormat(dr["bnpmd_qty04"].ToString().Trim());
                ad.bnpmQty05 = GetAmountFormat(dr["bnpmd_qty05"].ToString().Trim());
                ad.bnpmQty06 = GetAmountFormat(dr["bnpmd_qty06"].ToString().Trim());
                ad.bnpmQty07 = GetAmountFormat(dr["bnpmd_qty07"].ToString().Trim());
                ad.bnpmQty08 = GetAmountFormat(dr["bnpmd_qty08"].ToString().Trim());
                ad.bnpmQty09 = GetAmountFormat(dr["bnpmd_qty09"].ToString().Trim());
                ad.bnpmQty10 = GetAmountFormat(dr["bnpmd_qty10"].ToString().Trim());
                ad.bnpmQty11 = GetAmountFormat(dr["bnpmd_qty11"].ToString().Trim());
                ad.bnpmQty12 = GetAmountFormat(dr["bnpmd_qty12"].ToString().Trim());
                ad.bnpmQty13 = GetAmountFormat(dr["bnpmd_qty13"].ToString().Trim());
                ad.bnpmQty14 = GetAmountFormat(dr["bnpmd_qty14"].ToString().Trim());
                ad.bnpmQty15 = GetAmountFormat(dr["bnpmd_qty15"].ToString().Trim());
                ad.bnpmQty16 = GetAmountFormat(dr["bnpmd_qty16"].ToString().Trim());
                ad.bnpmQty17 = GetAmountFormat(dr["bnpmd_qty17"].ToString().Trim());
                ad.bnpmQtyTotal = GetAmountFormat(dr["bnpmd_qtyTotal"].ToString().Trim());
                ad.bnpmUsedRemark = dr["bnpmd_UsedRemark"].ToString().Trim();
                ad.bnpmReasonRemark = dr["bnpmd_ReasonRemark"].ToString().Trim();

                lstSelect.Add(ad);
            }
            dr.Close();
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }

    [WebMethod(EnableSession = true)]
    public string GetAsFileList(string paramDate, string paramTimes, string paramMainbuyer, string paramSample)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StCommon st = new StCommon(preVal);
        StDataCommon stData = new StDataCommon();

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" select * ");
        sb.AppendLine(" FROM " + preVal + "BNPMX a ");
        sb.AppendLine(" WHERE BnpmX_Date = '" + paramDate + "' and BnpmX_Times = '" + paramTimes + "' and BnpmX_MainBuyer = '" + paramMainbuyer + "' and BnpmX_Sample = '" + paramSample + "' ");
        sb.AppendLine(" Order by BnpmX_LineSeqx ");

        List<AsData> lstSelect = new List<AsData>();

        string qry = sb.ToString();

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            while (dr.Read())
            {
                AsData ad = new AsData();
                ad.bnpmImageFile = dr["BnpmX_imageFileName"].ToString().Trim();
                ad.bnpmFileLine = dr["BnpmX_LineSeqx"].ToString().Trim();

                string serverPath = StFileFolder.GetPhygicalUploadDir(null, "AsFilePath");

                try
                {
                    FileStream fsIn = new FileStream(serverPath + ad.bnpmImageFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(fsIn);
                    fsIn.Close();

                    ad.width = image.Width;
                    ad.height = image.Height;
                }
                catch { }

                lstSelect.Add(ad);
            }
            dr.Close();
        }

        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = jsSer.Serialize(lstSelect);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string AsDelete(string paramDate, string paramTimes, string paramKure, string paramSample)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string bnpm_date = StCommon.ReplaceSQ(paramDate);
        string bnpm_times = StCommon.ReplaceSQ(paramTimes);
        string bnpm_mainbuyer = StCommon.ReplaceSQ(paramKure);
        string bnpm_sample = StCommon.ReplaceSQ(paramSample);

        string qry = " select BnpmH_Bonsa_Check, BnpmH_Bonsa_Check1 FROM " + preVal + "BNPMH where BnpmH_Date = '" + bnpm_date + "' and BnpmH_Times = '" + bnpm_times + "' and BnpmH_MainBuyer = '" + bnpm_mainbuyer + "' and BnpmH_Sample='" + bnpm_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string bnpm_bonsa_check = "";
        string bnpm_bonsa_check1 = "";
        if (dsC.Tables[0].Rows.Count > 0)
        {
            bnpm_bonsa_check = dsC.Tables[0].Rows[0]["BnpmH_Bonsa_Check"].ToString();
            bnpm_bonsa_check1 = dsC.Tables[0].Rows[0]["BnpmH_Bonsa_Check1"].ToString();
        }

        string failMsg = "";

        int result = 0;
        
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
            string upDir = StFileFolder.GetPhygicalUploadDir(null, "AsFilePath");

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

            result = 2;
        }
        else
        {
            failMsg = "삭제할 수 없습니다. 현재 [" + stateMsg + "]인 상태입니다.";
            result = 0;
        }

        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        or.totalcount = result.ToString();
        or.msg = failMsg;

        strJson = jsSer.Serialize(or);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string AsRequest(string paramDate, string paramTimes, string paramMainbuyer, string paramSample, string paramTbuc)
    {
        string preVal = "";

        try
        {
            preVal = HttpContext.Current.Session["PreVal"].ToString();
        }
        catch
        {
            preVal = "tbl";
        }

        StDataCommon stData = new StDataCommon();
        WorkHistory whis = new WorkHistory();

        string failMsg = "";

        int result = 0;

        string blju_date = StCommon.ReplaceSQ(paramDate);
        string blju_times = StCommon.ReplaceSQ(paramTimes);
        string blju_mainbuyer = StCommon.ReplaceSQ(paramMainbuyer);
        string blju_sample = StCommon.ReplaceSQ(paramSample);
        string tbuc = StCommon.ReplaceSQ(paramTbuc);
        
        string date = DateTime.Now.ToShortDateString();
        string times = String.Format("{0:HH:mm:ss:fff}", DateTime.Now);
        string mainbuyer = MemberData.GetLoginSID("KureCode");
        string sample = "0";

        string qry = " select * FROM " + preVal + "BJHD where bjhd_date = '" + blju_date + "' and bjhd_times = '" + blju_times + "' and bjhd_mainbuyer = '" + blju_mainbuyer + "' and bjhd_sample='" + blju_sample + "' ";
        DataSet dsC = stData.GetDataSet(qry);

        string tbucmarktop = "";
        if (tbuc == "1")
        {
            tbucmarktop = "T";
        }
        else if (preVal == "tbl")
        {
            tbucmarktop = "M";
        }
        else if (preVal == "abl")
        {
            tbucmarktop = "A";
        }

        if (dsC.Tables[0].Rows.Count == 0 && tbucmarktop != "T")
        {
            failMsg = "요청하신 스타일을 다시 확인해주세요.";
            result = 0;
        }
        else
        {
            // 헤더 저장
            AsRequestData asr = new AsRequestData();

            asr.Date = date;
            asr.Times = times;
            asr.Mainbuyer = mainbuyer;
            asr.Sample = sample;
            asr.tbucmarktop = tbucmarktop;
            asr.bjhd_date = blju_date;
            asr.bjhd_times = blju_times;
            asr.bjhd_mainbuyer = blju_mainbuyer;
            asr.bjhd_sample = blju_sample;
            asr.bonsa_check = "0";
            asr.remark = "";
            asr.Sdate = DateTime.Now;
            asr.Ssawon = MemberData.GetLoginSID("LoginID");
            asr.Mdate = DateTime.Now;
            asr.Msawon = MemberData.GetLoginSID("LoginID");

            asr.InsertHead();

            result = 2;
        }

        AsResult ar = new AsResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        ar.totalcount = result.ToString();
        ar.msg = failMsg;

        ar.date = date;
        ar.times = times;
        ar.mainbuyer = mainbuyer;
        ar.sample = sample;

        strJson = jsSer.Serialize(ar);

        return strJson;
    }
    
    [WebMethod(EnableSession = true)]
    public string TbucCheck(string paramProduct)
    {
        SqlDatabase tbuc_db = StDBConn.GetOpenDB(OpenDBType.tbucDB);
        StDataCommon stDataTbuc = new StDataCommon(tbuc_db);
        WorkHistory whis = new WorkHistory();

        string product = StCommon.ReplaceSQ(paramProduct);
        
        string qry = " select * FROM tblSTYLE where Dnga_StyleNox = '" + product + "' ";
        DataSet dsC = stDataTbuc.GetDataSet(qry);

        int result = 0;
        string msg = "";

        if (dsC.Tables[0].Rows.Count > 0)
        {
            msg = dsC.Tables[0].Rows[0]["Dnga_StyleNox"].ToString();
            result = 1;
        }
        
        OrderResult or = new OrderResult();
        JavaScriptSerializer jsSer = new JavaScriptSerializer();
        string strJson = "";

        or.totalcount = result.ToString();
        or.msg = msg;

        strJson = jsSer.Serialize(or);

        return strJson;
    }

    private string GetAmountFormat(Object obj)
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
