using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

using FirstOrder.Util;
using System.Text.RegularExpressions;

public partial class Page_StyleSelect : System.Web.UI.Page
{
    private string preVal = "";

    private string searchValue = "";

    private string paramDate = "";
    private string paramTime = "";
    private string paramKure = "";

    private int totCount = 0;

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
            searchValue = Server.HtmlEncode(Request["searchValue"].Trim());

            paramDate = Server.HtmlEncode(Request["paramDate"].Trim());
            paramTime = Server.HtmlEncode(Request["paramTime"].Trim());
            paramKure = Server.HtmlEncode(Request["paramKure"].Trim());
        }
        catch { }

        if (!IsPostBack)
        {
            // 현재 발주상태 체크
            string nowBonsaCheck = StCommon.GetBonsaCheck(preVal, paramKure, paramDate, paramTime);
            if (nowBonsaCheck != "" && nowBonsaCheck != "0")
            {
                string bonsaCheckMsg = StCommon.MessageBonsaCheck(nowBonsaCheck);
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.ShowAlertMessage("현재 [" + bonsaCheckMsg + "] 상태입니다. 발주의뢰 현황조회에서 확인해주세요.", "parent.$('#dialog').dialog('close');");
            }
            else
            {
                this.txtProduct.Text = searchValue;

                BindList("");
            }
        }
    }

    private void BindList(string mode)
    {
        DataSet ds = null;
        string qry = "";
        string whereQry = "";

        string styleNox = StCommon.ReplaceSQ(this.txtProduct.Text);

        string areaGubun = StCommon.GetAreaGubun(preVal, paramKure);

        if (preVal == "tbl")
        {
            if (areaGubun == "2.대리점") // 대리점은 TB- 주문못함.
                whereQry = " and Jego_StyleNox not like 'TB-%' ";
        }

        whereQry = StCommon.MakeSearchQry("Jego_StyleNox", styleNox, "%", whereQry);

        qry = " select isnull((select Blju_Line from " + preVal + "BLJU where Blju_Date = '" + paramDate + "' and Blju_Times = '" + paramTime + "' and Blju_MainBuyer = '" + paramKure + "' and Blju_StyleNox = a.Jego_StyleNox),'') AS Blju_Line, * from View_" + preVal + "JEGO_Summary a where 1=1 " + whereQry;
        DataSet dsJego = stData.GetDataSet(qry);

        if (dsJego.Tables[0].Rows.Count == 1 && mode == "")
        {
            DataRow dr = dsJego.Tables[0].Rows[0];

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
            ds = stData.GetDataSet(qry);

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

            ProductInfoData pi = new ProductInfoData();

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
                        case "01": sizeName1 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "02": sizeName2 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "03": sizeName3 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "04": sizeName4 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "05": sizeName5 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "06": sizeName6 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "07": sizeName7 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "08": sizeName8 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "09": sizeName9 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "10": sizeName10 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "11": sizeName11 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "12": sizeName12 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "13": sizeName13 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "14": sizeName14 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "15": sizeName15 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "16": sizeName16 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "17": sizeName17 = dr2["Common_CodeName"].ToString().Trim(); break;
                    }
                }
                dr2.Close();
            }

            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("SetProduct('" + pi.StyleNox + "','" + Regex.Replace(pi.MainName, @"\n", " ") + "','" + Regex.Replace(pi.SubName, @"\n", " ") + "','" + Regex.Replace(pi.SpecColor, @"\n", " ") + "','" + pi.JegoQty01 + "','" + pi.JegoQty02 + "','" + pi.JegoQty03 + "','" + pi.JegoQty04 + "','" + pi.JegoQty05 + "','" + pi.JegoQty06 + "','" + pi.JegoQty07 + "','" + pi.JegoQty08 + "','" + pi.JegoQty09 + "','" + pi.JegoQty10 + "','" + pi.JegoQty11 + "','" + pi.JegoQty12 + "','" + pi.JegoQty13 + "','" + pi.JegoQty14 + "','" + pi.JegoQty15 + "','" + pi.JegoQty16 + "','" + pi.JegoQty17 + "','" + pi.JegoQtyTotal + "','" + pi.IsDuple + "','" + pi.Msg + "','" + pi.Width + "','" + pi.Height + "','" + sizeName1 + "','" + sizeName2 + "','" + sizeName3 + "','" + sizeName4 + "','" + sizeName5 + "','" + sizeName6 + "','" + sizeName7 + "','" + sizeName8 + "','" + sizeName9 + "','" + sizeName10 + "','" + sizeName11 + "','" + sizeName12 + "','" + sizeName13 + "','" + sizeName14 + "','" + sizeName15 + "','" + sizeName16 + "','" + sizeName17 + "','" + Kind + "');");
        }
        else
        {
            totCount = dsJego.Tables[0].Rows.Count;

            this.lvList.DataSource = dsJego;
            this.lvList.DataBind();
        }
    }

    protected void lvList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        DataRowView rowView = (DataRowView)item.DataItem;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Literal ltlViewNumber = (Literal)e.Item.FindControl("ltlNumber");

            Literal ltlMainName = (Literal)e.Item.FindControl("ltlMainName");
            Literal ltlSubName = (Literal)e.Item.FindControl("ltlSubName");
            Literal ltlSpecColor = (Literal)e.Item.FindControl("ltlSpecColor");
            Literal ltlJustPrice = (Literal)e.Item.FindControl("ltlJustPrice");
            Literal ltlLowPrice = (Literal)e.Item.FindControl("ltlLowPrice");
            Literal ltlBigSizePrice = (Literal)e.Item.FindControl("ltlBigSizePrice");
            Literal ltlSizeBoxQty = (Literal)e.Item.FindControl("ltlSizeBoxQty");
            Literal ltlBoxQty = (Literal)e.Item.FindControl("ltlBoxQty");

            ltlViewNumber.Text = (totCount - (item.DataItemIndex)).ToString();

            DataRow dr = ((DataRowView)e.Item.DataItem).Row;
            DataSet ds = null;
            string qry = "";

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
            ds = stData.GetDataSet(qry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Kind = (ds.Tables[0].Rows[0]["Dnga_Pants"].ToString().Trim() == "P") ? "2" : "1";

                MainName = ds.Tables[0].Rows[0]["Dnga_MainName"].ToString().Trim();
                SubName = ds.Tables[0].Rows[0]["Dnga_SubName"].ToString().Trim();
                SpecColor = ds.Tables[0].Rows[0]["Dnga_SpecColor"].ToString();
                JustPrice = ds.Tables[0].Rows[0]["Dnga_JustPrice"].ToString();
                LowPrice = ds.Tables[0].Rows[0]["Dnga_LowPrice"].ToString();
                BigSizePrice = ds.Tables[0].Rows[0]["Dnga_BigSizePrice"].ToString();
                SizeBoxQty = ds.Tables[0].Rows[0]["Dnga_SizeBoxQty"].ToString();
                BoxQty = ds.Tables[0].Rows[0]["Dnga_BoxQty"].ToString();
                chkBoxQty = StCommon.ToDouble(ds.Tables[0].Rows[0]["Dnga_BoxQty"].ToString(), 0);

                ltlMainName.Text = MainName;
                ltlSubName.Text = SubName;
                ltlSpecColor.Text = SpecColor;
                ltlJustPrice.Text = GetAmountFormat(JustPrice);
                ltlLowPrice.Text = GetAmountFormat(LowPrice);
                ltlBigSizePrice.Text = GetAmountFormat(BigSizePrice);
                ltlSizeBoxQty.Text = SizeBoxQty;
                ltlBoxQty.Text = BoxQty;
            }

            ProductInfoData pi = new ProductInfoData();

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
                        case "01": sizeName1 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "02": sizeName2 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "03": sizeName3 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "04": sizeName4 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "05": sizeName5 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "06": sizeName6 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "07": sizeName7 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "08": sizeName8 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "09": sizeName9 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "10": sizeName10 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "11": sizeName11 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "12": sizeName12 = dr2["Common_CodeName"].ToString().Trim() + "<br>(" + ((Kind == "2") ? dr2["Common_Remark2"].ToString().Trim() : dr2["Common_Remark1"].ToString().Trim()) + ")"; break;
                        case "13": sizeName13 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "14": sizeName14 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "15": sizeName15 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "16": sizeName16 = dr2["Common_CodeName"].ToString().Trim(); break;
                        case "17": sizeName17 = dr2["Common_CodeName"].ToString().Trim(); break;
                    }
                }
                dr2.Close();
            }

            // 함수 적용
            Literal ltlSelectScript = (Literal)e.Item.FindControl("ltlSelectScript");

            ltlSelectScript.Text = "Onclick=\"SetProduct('" + pi.StyleNox + "','" + Regex.Replace(pi.MainName, @"\n", " ") + "','" + Regex.Replace(pi.SubName, @"\n", " ") + "','" + Regex.Replace(pi.SpecColor, @"\n", " ") + "','" + pi.JegoQty01 + "','" + pi.JegoQty02 + "','" + pi.JegoQty03 + "','" + pi.JegoQty04 + "','" + pi.JegoQty05 + "','" + pi.JegoQty06 + "','" + pi.JegoQty07 + "','" + pi.JegoQty08 + "','" + pi.JegoQty09 + "','" + pi.JegoQty10 + "','" + pi.JegoQty11 + "','" + pi.JegoQty12 + "','" + pi.JegoQty13 + "','" + pi.JegoQty14 + "','" + pi.JegoQty15 + "','" + pi.JegoQty16 + "','" + pi.JegoQty17 + "','" + pi.JegoQtyTotal + "','" + pi.IsDuple + "','" + pi.Msg + "','" + pi.Width + "','" + pi.Height + "','" + sizeName1 + "','" + sizeName2 + "','" + sizeName3 + "','" + sizeName4 + "','" + sizeName5 + "','" + sizeName6 + "','" + sizeName7 + "','" + sizeName8 + "','" + sizeName9 + "','" + sizeName10 + "','" + sizeName11 + "','" + sizeName12 + "','" + sizeName13 + "','" + sizeName14 + "','" + sizeName15 + "','" + sizeName16 + "','" + sizeName17 + "','" + Kind + "')\"";
        }
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

    protected void lvList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dp = ((ListView)sender).FindControl("dpList") as DataPager;
        dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        BindList("sch");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindList("sch");
    }
}

public class ProductInfoData
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
}