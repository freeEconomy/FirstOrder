using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FirstOrder.Data;
using FirstOrder.Util;

public partial class Page_JegoUpload_abl : System.Web.UI.Page
{
    private string preVal = "";

    private StCommon st = new StCommon("abl");
    private int totCount = 0;

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
            this.btnSave.Visible = false;
            this.btnOther.Visible = false;
        }
    }

    protected void lvMain_LayoutCreated(object sender, EventArgs e)
    {
        ((Literal)this.lvMain.FindControl("ltlSize1")).Text = st.SizeName1;
        ((Literal)this.lvMain.FindControl("ltlSize2")).Text = st.SizeName2;
        ((Literal)this.lvMain.FindControl("ltlSize3")).Text = st.SizeName3;
        ((Literal)this.lvMain.FindControl("ltlSize4")).Text = st.SizeName4;
        ((Literal)this.lvMain.FindControl("ltlSize5")).Text = st.SizeName5;
        ((Literal)this.lvMain.FindControl("ltlSize6")).Text = st.SizeName6;
        ((Literal)this.lvMain.FindControl("ltlSize7")).Text = st.SizeName7;
        ((Literal)this.lvMain.FindControl("ltlSize8")).Text = st.SizeName8;
        ((Literal)this.lvMain.FindControl("ltlSize9")).Text = st.SizeName9;
        ((Literal)this.lvMain.FindControl("ltlSize10")).Text = st.SizeName10;
        ((Literal)this.lvMain.FindControl("ltlSize11")).Text = st.SizeName11;
        ((Literal)this.lvMain.FindControl("ltlSize12")).Text = st.SizeName12;
        ((Literal)this.lvMain.FindControl("ltlSize13")).Text = st.SizeName13;
        ((Literal)this.lvMain.FindControl("ltlSize14")).Text = st.SizeName14;
        ((Literal)this.lvMain.FindControl("ltlSize15")).Text = st.SizeName15;
        ((Literal)this.lvMain.FindControl("ltlSize16")).Text = st.SizeName16;
        ((Literal)this.lvMain.FindControl("ltlSize17")).Text = st.SizeName17;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string upDir = StFileFolder.GetPhygicalUploadDir(this.Page, "UploadFilePath");
        string fileName = "Jego_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string extFileName = StFileFolder.GetFileExtName(this.fileExcel.PostedFile.FileName);

        string fullPath = upDir + fileName + "." + extFileName;
        this.fileExcel.SaveAs(fullPath);

        BindExcelData(fullPath, extFileName);
    }

    private void BindExcelData(string fullPathFile, string extFileName)
    {
        string strCon = StDBConn.GetExcelProvider(fullPathFile, extFileName);

        OleDbConnection oleDBCon = null;
        OleDbCommand oleDBCmd = null;
        OleDbDataReader oleDBDr = null;

        StDataCommon stData = new StDataCommon();
        DataSet ds = null;

        try
        {
            oleDBCon = new OleDbConnection(strCon);

            oleDBCon.Open();

            string[] sheetNames = null;
            DataTable dtSheets = new DataTable();

            dtSheets = oleDBCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dtSheets == null)
            {
                sheetNames = null;
            }
            sheetNames = new String[dtSheets.Rows.Count];

            string firstSheet = "";
            for (int i = 0; i < dtSheets.Rows.Count; i++)
            {
                sheetNames[i] = dtSheets.Rows[i]["TABLE_NAME"].ToString().Trim('\'').Replace("$", "");
                if (sheetNames[i] == "가로양식(안전화용)" || sheetNames[i] == "세로양식(안전화용)")
                {
                    firstSheet = sheetNames[i];
                }
            }

            this.hidGubun.Value = firstSheet;

            string qry = string.Format("SELECT * FROM [{0}$]", firstSheet);

            oleDBCmd = new OleDbCommand(qry, oleDBCon);
            oleDBDr = oleDBCmd.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable dt = new DataTable();
            dt.Load(oleDBDr);

            this.lvMain.Visible = false;
            this.lvMain2.Visible = false;

            this.btnSave.Visible = true;

            if (firstSheet == "가로양식(안전화용)")
            {
                this.lvMain.Visible = true;

                qry = " DELETE from " + preVal + "JEGOEXCEL_H where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
                stData.GetExecuteNonQry(qry);

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if (dr[0].ToString() != "")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" INSERT INTO " + preVal + "JEGOEXCEL_H (Jego_Num, Jego_MainBuyer,Jego_StyleNox,Jego_Qty01,Jego_Qty02,Jego_Qty03,Jego_Qty04,Jego_Qty05,Jego_Qty06,Jego_Qty07,Jego_Qty08,Jego_Qty09,Jego_Qty10,Jego_Qty11,Jego_Qty12,Jego_Qty13,Jego_Qty14,Jego_Qty15,Jego_Qty16,Jego_Qty17,Jego_QtyTotal) ");
                        sb.Append(" VALUES('" + (i + 2) + "','" + MemberData.GetLoginSID("KureCode") + "','" + dr[0] + "','" + GetQtyNumber(dr[1]) + "','" + GetQtyNumber(dr[2]) + "','" + GetQtyNumber(dr[3]) + "','" + GetQtyNumber(dr[4]) + "','" + GetQtyNumber(dr[5]) + "','" + GetQtyNumber(dr[6]) + "','" + GetQtyNumber(dr[7]) + "','" + GetQtyNumber(dr[8]) + "','" + GetQtyNumber(dr[9]) + "','" + GetQtyNumber(dr[10]) + "','" + GetQtyNumber(dr[11]) + "','" + GetQtyNumber(dr[12]) + "','" + GetQtyNumber(dr[13]) + "','" + GetQtyNumber(dr[14]) + "','" + GetQtyNumber(dr[15]) + "','" + GetQtyNumber(dr[16]) + "','" + GetQtyNumber(dr[17]) + "','" + GetQtyNumber(dr[18]) + "') ");
                        stData.GetExecuteNonQry(sb.ToString());
                    }
                    else // 중간에 빈 값이 있으면 거기까지만 업로드후 중지
                    {
                        break;
                    }
                }

                qry = " select (select count(*) from " + preVal + "DNGA where Dnga_StyleNox = a.Jego_StyleNox) as ExistStyle,* from " + preVal + "JEGOEXCEL_H a where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' order by Jego_Num ";
                ds = stData.GetDataSet(qry);

                qry = " select max(Jego_Num) as Jego_Num, Jego_StyleNox from " + preVal + "JEGOEXCEL_H a where Jego_MainBuyer = '" + MemberData.GetLoginSID("KureCode") + "' group by Jego_StyleNox having count(*) > 1 ";
                DataSet dsDuple = stData.GetDataSet(qry);

                int failNum = 0;
                string failGubun = "";
                string failMsg = "";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (StCommon.ToInt(dr["ExistStyle"].ToString(), 0) == 0)
                    {
                        failNum = StCommon.ToInt(dr["Jego_Num"].ToString(), 0);
                        failGubun = "nostyle";
                        failMsg = "엑셀 " + failNum + "라인에 '" + dr["Jego_StyleNox"].ToString() + "'스타일은 본사에 없는 스타일입니다.";
                        break;
                    }

                    if (dsDuple.Tables[0].Rows.Count > 0)
                    {
                        failNum = StCommon.ToInt(dsDuple.Tables[0].Rows[0]["Jego_Num"].ToString(), 0);
                        failGubun = "duple";
                        failMsg = "엑셀 " + failNum + "라인에 중복된 스타일이 있습니다.";
                        break;
                    }

                    double chkSum = StCommon.ToDouble(dr["Jego_Qty01"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty02"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty03"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty04"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty05"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty06"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty07"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty08"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty09"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty10"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty11"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty12"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty13"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty14"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty15"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty16"].ToString(), 0) + StCommon.ToDouble(dr["Jego_Qty17"].ToString(), 0);
                    double sum = StCommon.ToDouble(dr["Jego_QtyTotal"].ToString(), 0);

                    if (chkSum != sum)
                    {
                        failNum = StCommon.ToInt(dr["Jego_Num"].ToString(), 0);
                        failGubun = "sumfail";
                        failMsg = "엑셀 " + failNum + "라인에 합계수량이 맞지 않습니다.";
                        break;
                    }
                }

                totCount = ds.Tables[0].Rows.Count;

                //DataTable dt1 = ds.Tables[0];
                //DataTable dt2 = dt1.DefaultView.ToTable(true, "Jego_StyleNox");
                //if (dt1.Rows.Count > dt2.Rows.Count)

                if (failGubun != "")
                {
                    StJavaScript js = new StJavaScript(this.Page, false, true);
                    js.ShowAlertMessage(failMsg);

                    this.btnSave.Visible = false;

                    this.fileExcel.Visible = false;
                    this.btnExcel.Visible = false;
                    this.lvMain.Visible = false;
                    this.btnOther.Visible = true;
                }
                else
                {
                    this.lvMain.DataSource = ds;
                    this.lvMain.DataBind();

                    this.btnSave.Visible = true;

                    this.fileExcel.Visible = false;
                    this.btnExcel.Visible = false;
                    this.btnOther.Visible = true;
                }
            }
            else if (firstSheet == "세로양식(안전화용)")
            {
                this.lvMain2.Visible = true;

                qry = " DELETE from " + preVal + "JEGOEXCEL_V where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
                stData.GetExecuteNonQry(qry);

                StringBuilder sb = new StringBuilder();

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if (dr[0].ToString() != "")
                    {
                        sb.Append(" INSERT INTO " + preVal + "JEGOEXCEL_V (Jego_Num, Jego_MainBuyer,Jego_StyleNox,Jego_Size,Jego_QtyTotal) ");
                        sb.Append(" VALUES('" + (i + 2) + "','" + MemberData.GetLoginSID("KureCode") + "','" + dr[0] + "','" + dr[1] + "','" + GetQtyNumber(dr[2]) + "') ");
                    }
                    else // 중간에 빈 값이 있으면 거기까지만 업로드후 중지
                    {
                        break;
                    }
                }

                if (sb.ToString() != "")
                {
                    stData.GetExecuteNonQry(sb.ToString());
                }

                qry = " select (select count(*) from " + preVal + "DNGA where Dnga_StyleNox = a.Jego_StyleNox) as ExistStyle,(select count(*) from gblCOMMON WHERE Common_Key = 'T0511' and substring(Common_Code,1,3) = '" + preVal + "' and Common_CodeName = a.Jego_Size) as ExistSize,* from " + preVal + "JEGOEXCEL_V a where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' order by Jego_Num ";
                ds = stData.GetDataSet(qry);

                qry = " select max(Jego_Num) as Jego_Num, Jego_StyleNox, Jego_Size from " + preVal + "JEGOEXCEL_V a where Jego_MainBuyer = '" + MemberData.GetLoginSID("KureCode") + "' group by Jego_StyleNox,Jego_Size having count(*) > 1 ";
                DataSet dsDuple = stData.GetDataSet(qry);

                int failNum = 0;
                string failGubun = "";
                string failMsg = "";

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    if (StCommon.ToInt(dr["ExistStyle"].ToString(), 0) == 0)
                    {
                        failNum = StCommon.ToInt(dr["Jego_Num"].ToString(), 0);
                        failGubun = "nostyle";
                        failMsg = "엑셀 " + failNum + "라인에 '" + dr["Jego_StyleNox"].ToString() + "'스타일은 본사에 없는 스타일입니다.";
                        break;
                    }

                    if (StCommon.ToInt(dr["ExistSize"].ToString(), 0) == 0)
                    {
                        failNum = StCommon.ToInt(dr["Jego_Num"].ToString(), 0);
                        failGubun = "nostyle";
                        failMsg = "엑셀 " + failNum + "라인에 '" + dr["Jego_Size"].ToString() + "'사이즈는 본사에 없는 사이즈입니다.";
                        break;
                    }

                    if (dsDuple.Tables[0].Rows.Count > 0)
                    {
                        failNum = StCommon.ToInt(dsDuple.Tables[0].Rows[0]["Jego_Num"].ToString(), 0);
                        failGubun = "duple";
                        failMsg = "엑셀 " + failNum + "라인에 중복된 스타일/사이즈 가 있습니다.";
                        break;
                    }
                }

                totCount = ds.Tables[0].Rows.Count;

                //DataTable dt1 = ds.Tables[0];
                //DataTable dt2 = dt1.DefaultView.ToTable(true, "Jego_StyleNox", "Jego_Size");
                //if (dt1.Rows.Count > dt2.Rows.Count)

                if (failGubun != "")
                {
                    StJavaScript js = new StJavaScript(this.Page, false, true);
                    js.ShowAlertMessage(failMsg);

                    this.btnSave.Visible = false;

                    this.fileExcel.Visible = false;
                    this.btnExcel.Visible = false;
                    this.lvMain2.Visible = false;
                    this.btnOther.Visible = true;
                }
                else
                {
                    this.lvMain2.DataSource = ds;
                    this.lvMain2.DataBind();

                    this.btnSave.Visible = true;

                    this.fileExcel.Visible = false;
                    this.btnExcel.Visible = false;
                    this.btnOther.Visible = true;
                }
            }
            else
            {
                StJavaScript js = new StJavaScript(this.Page, false, true);
                js.WriteJavascript("showMessageToolTip('" + btnOther.ClientID + "', '시트명을 `가로양식(안전화용)`이나 `세로양식(안전화용)`으로 해주세요. 샘플 양식을 다운받아서 사용해주세요.');");
                
                this.btnSave.Visible = false;

                this.fileExcel.Visible = false;
                this.btnExcel.Visible = false;
                this.lvMain2.Visible = false;
                this.btnOther.Visible = true;
            }
        }
        catch (Exception ex)
        {
            StJavaScript js = new StJavaScript(this.Page, false, true);
            js.WriteJavascript("showMessageToolTip('" + btnExcel.ClientID + "', '양식에 오류가 있습니다. 샘플 양식을 다운받아서 형식에 맞춰서 사용해주세요.');");
            
            this.btnSave.Visible = false;
            
            this.lvMain.Visible = false;
            this.lvMain2.Visible = false;
            
            st.LogException(ex);
        }
        finally
        {
            try
            {
                oleDBDr.Close();
                oleDBDr.Dispose();
                oleDBCon.Close();
            }
            catch { }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        WorkHistory whis = new WorkHistory();
        StDataCommon stData = new StDataCommon();
        string qry = "";

        if (this.hidGubun.Value == "가로양식(안전화용)")
        {
            qry = " DELETE from " + preVal + "JEGO where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
            stData.GetExecuteNonQry(qry);

            whis.InsertWork("재고", "삭제", qry);

            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO " + preVal + "JEGO (Jego_MainBuyer,Jego_StyleNox,Jego_Qty01,Jego_Qty02,Jego_Qty03,Jego_Qty04,Jego_Qty05,Jego_Qty06,Jego_Qty07,Jego_Qty08,Jego_Qty09,Jego_Qty10,Jego_StyleNox,Jego_Qty11,Jego_Qty12,Jego_Qty13,Jego_Qty14,Jego_Qty15,Jego_Qty16,Jego_Qty17,Jego_QtyTotal,Jego_CreateDate,Jego_CreateSawon) ");
            sb.Append(" SELECT Jego_MainBuyer,Jego_StyleNox,Jego_Qty01,Jego_Qty02,Jego_Qty03,Jego_Qty04,Jego_Qty05,Jego_Qty06,Jego_Qty07,Jego_Qty08,Jego_Qty09,Jego_Qty10,Jego_Qty11,Jego_Qty12,Jego_Qty13,Jego_Qty14,Jego_Qty15,Jego_Qty16,Jego_Qty17,Jego_QtyTotal,GetDate(),'" + MemberData.GetLoginSID("LoginID") + "' from " + preVal + "JEGOEXCEL_H where Jego_MainBuyer = '" + MemberData.GetLoginSID("KureCode") + "' ");
            stData.GetExecuteNonQry(sb.ToString());

            whis.InsertWork("재고", "등록", sb.ToString());
        }
        else if (this.hidGubun.Value == "세로양식(안전화용)")
        {
            qry = " DELETE from " + preVal + "JEGO where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
            stData.GetExecuteNonQry(qry);

            whis.InsertWork("재고", "삭제", qry);

            qry = " select * from " + preVal + "JEGOEXCEL_V a where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' order by Jego_Num ";
            DataSet ds = stData.GetDataSet(qry);

            StringBuilder sbInsert = new StringBuilder();
            sbInsert.AppendLine(" INSERT INTO " + preVal + "JEGO (Jego_MainBuyer,Jego_StyleNox,Jego_CreateDate,Jego_CreateSawon) ");
            sbInsert.AppendLine(" SELECT Jego_MainBuyer,Jego_StyleNox,GetDate(),'" + MemberData.GetLoginSID("LoginID") + "' from " + preVal + "JEGOEXCEL_V where Jego_MainBuyer = '" + MemberData.GetLoginSID("KureCode") + "' group by Jego_MainBuyer,Jego_StyleNox; ");
            stData.GetExecuteNonQry(sbInsert.ToString());

            whis.InsertWork("재고", "등록", sbInsert.ToString());

            StringBuilder sbUpdate = new StringBuilder();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                string Column = "";
                string Value = dr["Jego_QtyTotal"].ToString();
                string Jego1 = "";
                string Jego2 = "";
                string Jego3 = "";
                string Jego4 = "";
                string Jego5 = "";
                string Jego6 = "";
                string Jego7 = "";
                string Jego8 = "";
                string Jego9 = "";
                string Jego10 = "";
                string Jego11 = "";
                string Jego12 = "";
                string Jego13 = "";
                string Jego14 = "";
                string Jego15 = "";
                string Jego16 = "";
                string Jego17 = "";
                string JegoTotal = dr["Jego_QtyTotal"].ToString();
                switch (dr["Jego_Size"].ToString().Trim())
                {
                    case "230":
                        Column = "Jego_Qty01";
                        Jego1 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "235":
                        Column = "Jego_Qty02";
                        Jego2 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "240":
                        Column = "Jego_Qty03";
                        Jego3 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "245":
                        Column = "Jego_Qty04";
                        Jego4 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "250":
                        Column = "Jego_Qty05";
                        Jego5 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "255":
                        Column = "Jego_Qty06";
                        Jego6 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "260":
                        Column = "Jego_Qty07";
                        Jego7 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "265":
                        Column = "Jego_Qty08";
                        Jego8 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "270":
                        Column = "Jego_Qty09";
                        Jego9 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "275":
                        Column = "Jego_Qty10";
                        Jego10 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "280":
                        Column = "Jego_Qty11";
                        Jego11 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "285":
                        Column = "Jego_Qty12";
                        Jego12 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "290":
                        Column = "Jego_Qty13";
                        Jego13 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "295":
                        Column = "Jego_Qty14";
                        Jego14 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "300":
                        Column = "Jego_Qty15";
                        Jego15 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "305":
                        Column = "Jego_Qty16";
                        Jego16 = dr["Jego_QtyTotal"].ToString();
                        break;
                    case "310":
                        Column = "Jego_Qty17";
                        Jego17 = dr["Jego_QtyTotal"].ToString();
                        break;
                }

                if (Column != "")
                {
                    sbUpdate.AppendLine(" UPDATE " + preVal + "JEGO SET " + Column + " = '" + Value + "' where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' and Jego_StyleNox='" + dr["Jego_StyleNox"].ToString() + "'; ");
                }
            }

            stData.GetExecuteNonQry(sbUpdate.ToString());
            whis.InsertWork("재고", "등록", sbUpdate.ToString());

            // 총계 업데이트
            qry = " UPDATE " + preVal + "JEGO SET Jego_QtyTotal = (isnull(jego_qty01,0)+isnull(jego_qty02,0)+isnull(jego_qty03,0)+isnull(jego_qty04,0)+isnull(jego_qty05,0)+isnull(jego_qty06,0)+isnull(jego_qty07,0)+isnull(jego_qty08,0)+isnull(jego_qty09,0)+isnull(jego_qty10,0)+isnull(jego_qty11,0)+isnull(jego_qty12,0)+isnull(jego_qty13,0)+isnull(jego_qty14,0)+isnull(jego_qty15,0)+isnull(jego_qty16,0)+isnull(jego_qty17,0)) where Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
            stData.GetExecuteNonQry(qry);

            whis.InsertWork("재고", "총계", qry);

            // 날짜 빈거 없데이트
            qry = " UPDATE " + preVal + "JEGO SET Jego_CreateDate = GetDate(), Jego_CreateSawon = '" + MemberData.GetLoginSID("LoginID") + "' where Jego_CreateDate is null and Jego_MainBuyer='" + MemberData.GetLoginSID("KureCode") + "' ";
            stData.GetExecuteNonQry(qry);
        }

        StJavaScript st = new StJavaScript(this.Page);
        st.WriteJavascript("alert('저장되었습니다.'); location.href='/Page/JegoList.aspx';");
    }

    protected void btnOther_Click(object sender, EventArgs e)
    {
        this.fileExcel.Visible = true;
        this.btnExcel.Visible = true;

        this.btnSave.Visible = false;
        this.lvMain.Visible = false;
        this.lvMain2.Visible = false;

        this.btnOther.Visible = false;
    }

    private int GetQtyNumber(object obj)
    {
        return StCommon.ToInt(obj.ToString().Replace(",", "").Replace(" ", ""), 0);
    }
}