using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

using FirstOrder.Util;

/// <summary>
/// WebService의 요약 설명입니다.
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
[System.Web.Script.Services.ScriptService]
public class WebService_tbl : System.Web.Services.WebService
{
    public WebService_tbl()
    {

        //디자인된 구성 요소를 사용하는 경우 다음 줄의 주석 처리를 제거합니다. 
        //InitializeComponent(); 
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
    }

    [WebMethod(EnableSession = true)]
    public string GetProduct(string paramDate, string paramTime, string paramKure, string searchValue)
    {
        StDataCommon stData = new StDataCommon();

        string qry = "";
        DataSet ds = null;

        List<ProductInfo> lstSelect = new List<ProductInfo>();

        string strJson = "";

        qry = " select * from View_tblJEGO_Summary where Jego_StyleNox like '%" + searchValue + "%' ";

        using (IDataReader dr = stData.GetDataReader(qry))
        {
            if (dr.Read())
            {
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

                qry = " select * from tblBLJU where Blju_Date = '" + paramDate + "' and Blju_Times = '" + paramTime + "' and Blju_MainBuyer = '" + paramKure + "' and Blju_StyleNox = '" + dr["Jego_StyleNox"].ToString() + "' ";
                DataSet dsC = stData.GetDataSet(qry);

                if (dsC.Tables[0].Rows.Count > 0)
                {
                    IsDuple = dsC.Tables[0].Rows[0]["Blju_Line"].ToString();
                }

                qry = " select Dnga_MainName,Dnga_SubName,Dnga_SpecColor,isnull(Dnga_LowPrice,0) as Dnga_LowPrice,isnull(Dnga_JustPrice,0) as Dnga_JustPrice,isnull(Dnga_BigSizePrice,0) as Dnga_BigSizePrice,Dnga_SizeBoxQty,Dnga_BoxQty from tblDNGA where Dnga_StyleNox = '" + dr["Jego_StyleNox"].ToString() + "' ";
                ds = stData.GetDataSet(qry);

                if (ds.Tables[0].Rows.Count > 0)
                {
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
                pi.JegoQtyTotal = GetAmountFormat(dr["Jego_QtyTotal"]);
                pi.MainName = MainName;
                pi.SubName = SubName;
                pi.SpecColor = SpecColor;
                pi.LowPrice = GetAmountFormat(LowPrice);
                pi.JustPrice = GetAmountFormat(JustPrice);
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
                    // ((박수수량 * j) - (박수수량 * l) / (박수수량 * j)) * 100
                    double justPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_JustPrice"]);
                    double lowPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_LowPrice"]);

                    //double dcPercent = Math.Round((((chkBoxQty * justPrice) - (chkBoxQty * lowPrice)) / (chkBoxQty * justPrice)) * 100, 1);

                    // (j - l / l) * 100
                    double dcPercent = Math.Round(((justPrice - lowPrice) / lowPrice) * 100, 1);

                    pi.Msg = "BOX수량: " + chkBoxQty + " PCS (" + dcPercent + " % 할인됩니다.)";
                }

                qry = "SELECT * FROM tblJEPG WHERE Jepg_StyleNox='" + dr["Jego_StyleNox"].ToString() + "'";
                ds = stData.GetDataSet(qry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Jepg_Image"].ToString()))
                        {
                            byte[] empPic = (byte[])ds.Tables[0].Rows[0]["Jepg_Image"];

                            MemoryStream ms = new MemoryStream(empPic);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                            pi.Width = image.Width;
                            pi.Height = image.Height;
                        }
                    }
                    catch { }
                }

                lstSelect.Add(pi);

                while (dr.Read())
                {
                    MainName = "";
                    SubName = "";
                    SpecColor = "";
                    LowPrice = "";
                    JustPrice = "";
                    BigSizePrice = "";
                    SizeBoxQty = "";
                    BoxQty = "";

                    IsDuple = "";
                    chkBoxQty = 0;

                    qry = " select * from tblBLJU where Blju_Date = '" + paramDate + "' and Blju_Times = '" + paramTime + "' and Blju_MainBuyer = '" + paramKure + "' and Blju_StyleNox = '" + dr["Jego_StyleNox"].ToString() + "' ";
                    dsC = stData.GetDataSet(qry);

                    if (dsC.Tables[0].Rows.Count > 0)
                    {
                        IsDuple = dsC.Tables[0].Rows[0]["Blju_Line"].ToString();
                    }

                    qry = " select isnull(Dnga_JustPrice,0) as Dnga_JustPrice,isnull(Dnga_LowPrice,0) as Dnga_LowPrice,isnull(Dnga_BoxQty,0) as Dnga_BoxQty,Dnga_StyleNox,Dnga_MainName,Dnga_SubName,Dnga_SpecColor from tblDNGA where Dnga_StyleNox = '" + dr["Jego_StyleNox"].ToString() + "' ";
                    ds = stData.GetDataSet(qry);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
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

                    pi = new ProductInfo();

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
                    pi.JegoQtyTotal = GetAmountFormat(dr["Jego_QtyTotal"]);
                    pi.MainName = MainName;
                    pi.SubName = SubName;
                    pi.SpecColor = SpecColor;
                    pi.LowPrice = GetAmountFormat(LowPrice);
                    pi.JustPrice = GetAmountFormat(JustPrice);
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
                        // ((박수수량 * j) - (박수수량 * l) / (박수수량 * j)) * 100
                        double justPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_JustPrice"]);
                        double lowPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["Dnga_LowPrice"]);

                        //double dcPercent = Math.Round((((chkBoxQty * justPrice) - (chkBoxQty * lowPrice)) / (chkBoxQty * justPrice)) * 100, 1);

                        // (j - l / l) * 100
                        double dcPercent = Math.Round(((justPrice - lowPrice) / lowPrice) * 100, 1);

                        pi.Msg = "BOX수량: " + chkBoxQty + " PCS (" + dcPercent + " % 할인됩니다.)";
                    }

                    qry = "SELECT * FROM tblJEPG WHERE Jepg_StyleNox='" + dr["Jego_StyleNox"].ToString() + "'";
                    ds = stData.GetDataSet(qry);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Jepg_Image"].ToString()))
                            {
                                byte[] empPic = (byte[])ds.Tables[0].Rows[0]["Jepg_Image"];

                                MemoryStream ms = new MemoryStream(empPic);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                                pi.Width = image.Width;
                                pi.Height = image.Height;
                            }
                        }
                        catch { }
                    }

                    lstSelect.Add(pi);
                }
            }
            else
            {
                ProductInfo pi = new ProductInfo();

                pi.JegoQty01 = "";
                pi.JegoQty02 = "";
                pi.JegoQty03 = "";
                pi.JegoQty04 = "";
                pi.JegoQty05 = "";
                pi.JegoQty06 = "";
                pi.JegoQty07 = "";
                pi.JegoQty08 = "";
                pi.JegoQty09 = "";
                pi.JegoQty10 = "";
                pi.JegoQty11 = "";
                pi.JegoQty12 = "";
                pi.JegoQtyTotal = "";
                pi.MainName = "검색하신 목록이 없습니다.";
                pi.SubName = "";
                pi.SpecColor = "";
                pi.LowPrice = "";
                pi.JustPrice = "";
                pi.BigSizePrice = "";
                pi.SizeBoxQty = "";
                pi.BoxQty = "";

                pi.StyleNox = "";

                pi.IsDuple = "";

                pi.Msg = "";

                pi.Width = 0;
                pi.Height = 0;
            }

            JavaScriptSerializer jsSer = new JavaScriptSerializer();
            strJson = jsSer.Serialize(lstSelect);

            dr.Close();
        }

        return strJson;
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
