using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Util;

namespace FirstOrder.Data
{
    public class AsRequestData
    {
        SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

        WorkHistory whis = new WorkHistory();

        private string m_date;
        private string m_times;
        private string m_mainbuyer;
        private string m_sample;

        private string m_tbucmarktop;
        private string m_bjhd_date;
        private string m_bjhd_times;
        private string m_bjhd_mainbuyer;
        private string m_bjhd_sample;
        private string m_bonsa_check;
        private string m_remark;

        private string m_stylenox;
        private string m_stylenoxline;
        private string m_bnpmcode;
        private double m_qty01;
        private double m_qty02;
        private double m_qty03;
        private double m_qty04;
        private double m_qty05;
        private double m_qty06;
        private double m_qty07;
        private double m_qty08;
        private double m_qty09;
        private double m_qty10;
        private double m_qty11;
        private double m_qty12;
        private double m_qty13;
        private double m_qty14;
        private double m_qty15;
        private double m_qty16;
        private double m_qty17;
        private double m_qtytotal;
        private string m_justprice;
        private string m_justamount;
        private string m_usedremark;
        private string m_reasonremark;
        private string m_imagefilename;
        private DateTime m_sdate;
        private string m_ssawon;
        private DateTime m_mdate;
        private string m_msawon;

        public string Date { get { return m_date; } set { m_date = value; } }
        public string Times { get { return m_times; } set { m_times = value; } }
        public string Mainbuyer { get { return m_mainbuyer; } set { m_mainbuyer = value; } }
        public string Sample { get { return m_sample; } set { m_sample = value; } }
        
        public string tbucmarktop { get { return m_tbucmarktop; } set { m_tbucmarktop = value; } }
        public string bjhd_date { get { return m_bjhd_date; } set { m_bjhd_date = value; } }
        public string bjhd_times { get { return m_bjhd_times; } set { m_bjhd_times = value; } }
        public string bjhd_mainbuyer { get { return m_bjhd_mainbuyer; } set { m_bjhd_mainbuyer = value; } }
        public string bjhd_sample { get { return m_bjhd_sample; } set { m_bjhd_sample = value; } }
        public string bonsa_check { get { return m_bonsa_check; } set { m_bonsa_check = value; } }
        public string remark { get { return m_remark; } set { m_remark = value; } }

        public string Stylenox { get { return m_stylenox; } set { m_stylenox = value; } }
        public string Stylenoxline { get { return m_stylenoxline; } set { m_stylenoxline = value; } }
        public string Bnpmcode { get { return m_bnpmcode; } set { m_bnpmcode = value; } }
        public double Qty01 { get { return m_qty01; } set { m_qty01 = value; } }
        public double Qty02 { get { return m_qty02; } set { m_qty02 = value; } }
        public double Qty03 { get { return m_qty03; } set { m_qty03 = value; } }
        public double Qty04 { get { return m_qty04; } set { m_qty04 = value; } }
        public double Qty05 { get { return m_qty05; } set { m_qty05 = value; } }
        public double Qty06 { get { return m_qty06; } set { m_qty06 = value; } }
        public double Qty07 { get { return m_qty07; } set { m_qty07 = value; } }
        public double Qty08 { get { return m_qty08; } set { m_qty08 = value; } }
        public double Qty09 { get { return m_qty09; } set { m_qty09 = value; } }
        public double Qty10 { get { return m_qty10; } set { m_qty10 = value; } }
        public double Qty11 { get { return m_qty11; } set { m_qty11 = value; } }
        public double Qty12 { get { return m_qty12; } set { m_qty12 = value; } }
        public double Qty13 { get { return m_qty13; } set { m_qty13 = value; } }
        public double Qty14 { get { return m_qty14; } set { m_qty14 = value; } }
        public double Qty15 { get { return m_qty15; } set { m_qty15 = value; } }
        public double Qty16 { get { return m_qty16; } set { m_qty16 = value; } }
        public double Qty17 { get { return m_qty17; } set { m_qty17 = value; } }
        public double QtyTotal { get { return m_qtytotal; } set { m_qtytotal = value; } }
        public string JustPrice { get { return m_justprice; } set { m_justprice = value; } }
        public string JustAmount { get { return m_justamount; } set { m_justamount = value; } }
        public string Usedremark { get { return m_usedremark; } set { m_usedremark = value; } }
        public string Reasonremark { get { return m_reasonremark; } set { m_reasonremark = value; } }
        public string Imagefilename { get { return m_imagefilename; } set { m_imagefilename = value; } }
        
        public DateTime Sdate { get { return m_sdate; } set { m_sdate = value; } }
        public string Ssawon { get { return m_ssawon; } set { m_ssawon = value; } }
        public DateTime Mdate { get { return m_mdate; } set { m_mdate = value; } }
        public string Msawon { get { return m_msawon; } set { m_msawon = value; } }

        public void InsertHead()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" select * from " + preVal + "BNPMH where ");
            sb.Append(" BnpmH_Date='" + m_date + "' and BnpmH_Times='" + m_times + "' and BnpmH_MainBuyer='" + m_mainbuyer + "' and BnpmH_Sample='" + m_sample + "'");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count == 0)
            {
                sb = new StringBuilder();
                sb.Append(" INSERT INTO " + preVal + "BNPMH (BnpmH_Date,BnpmH_Times,BnpmH_MainBuyer,BnpmH_Sample,BnpmH_TbucMarkTop,BnpmH_Bjhd_Date,BnpmH_Bjhd_Times,BnpmH_Bjhd_MainBuyer,BnpmH_Bjhd_Sample ");
                sb.Append(" ,BnpmH_Bonsa_Check,BnpmH_Remark,BnpmH_CreateDate,BnpmH_CreateSawon,BnpmH_ModifyDate,BnpmH_ModifySaWon) ");
                sb.Append(" VALUES(@BnpmH_Date,@BnpmH_Times,@BnpmH_MainBuyer,@BnpmH_Sample,@BnpmH_TbucMarkTop,@BnpmH_Bjhd_Date,@BnpmH_Bjhd_Times,@BnpmH_Bjhd_MainBuyer,@BnpmH_Bjhd_Sample ");
                sb.Append(" ,@BnpmH_Bonsa_Check,@BnpmH_Remark ");
                sb.Append(" ,@BnpmH_CreateDate,@BnpmH_CreateSawon,@BnpmH_ModifyDate,@BnpmH_ModifySaWon) ");

                cmd = db.GetSqlStringCommand(sb.ToString());
                
                db.AddInParameter(cmd, "@BnpmH_Date", DbType.String, m_date);
                db.AddInParameter(cmd, "@BnpmH_Times", DbType.String, m_times);
                db.AddInParameter(cmd, "@BnpmH_MainBuyer", DbType.String, m_mainbuyer);
                db.AddInParameter(cmd, "@BnpmH_Sample", DbType.String, m_sample);
                db.AddInParameter(cmd, "@BnpmH_TbucMarkTop", DbType.String, m_tbucmarktop);
                db.AddInParameter(cmd, "@BnpmH_Bjhd_Date", DbType.String, m_bjhd_date);
                db.AddInParameter(cmd, "@BnpmH_Bjhd_Times", DbType.String, m_bjhd_times);
                db.AddInParameter(cmd, "@BnpmH_Bjhd_MainBuyer", DbType.String, m_bjhd_mainbuyer);
                db.AddInParameter(cmd, "@BnpmH_Bjhd_Sample", DbType.String, m_bjhd_sample);
                db.AddInParameter(cmd, "@BnpmH_Bonsa_Check", DbType.String, m_bonsa_check);
                db.AddInParameter(cmd, "@BnpmH_Remark", DbType.String, m_remark);
                db.AddInParameter(cmd, "@BnpmH_CreateDate", DbType.DateTime, m_sdate);
                db.AddInParameter(cmd, "@BnpmH_CreateSawon", DbType.String, m_ssawon);
                db.AddInParameter(cmd, "@BnpmH_ModifyDate", DbType.DateTime, m_mdate);
                db.AddInParameter(cmd, "@BnpmH_ModifySaWon", DbType.String, m_msawon);

                db.ExecuteNonQuery(cmd);

                whis.InsertWork("AS접수", "헤더입력", cmd);
            }
            else
            {
                UpdateHead();
            }
        }

        public void UpdateHead()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE " + preVal + "BNPMH SET ");
            sb.Append(" BnpmH_TbucMarkTop=@BnpmH_TbucMarkTop, ");
            sb.Append(" BnpmH_Bjhd_Date=@BnpmH_Bjhd_Date, ");
            sb.Append(" BnpmH_Bjhd_Times=@BnpmH_Bjhd_Times, ");
            sb.Append(" BnpmH_Bjhd_MainBuyer=@BnpmH_Bjhd_MainBuyer, ");
            sb.Append(" BnpmH_Bjhd_Sample=@BnpmH_Bjhd_Sample, ");
            sb.Append(" BnpmH_Bonsa_Check=@BnpmH_Bonsa_Check, ");
            sb.Append(" BnpmH_Remark=@BnpmH_Remark, ");
            sb.Append(" BnpmH_ModifyDate=@BnpmH_ModifyDate, ");
            sb.Append(" BnpmH_ModifySaWon=@BnpmH_ModifySaWon ");
            sb.Append(" Where ");
            sb.Append(" BnpmH_Date=@BnpmH_Date and ");
            sb.Append(" BnpmH_Times=@BnpmH_Times and ");
            sb.Append(" BnpmH_MainBuyer=@BnpmH_MainBuyer and ");
            sb.Append(" BnpmH_Sample=@BnpmH_Sample ");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            
            db.AddInParameter(cmd, "@BnpmH_TbucMarkTop", DbType.String, m_tbucmarktop);
            db.AddInParameter(cmd, "@BnpmH_Bjhd_Date", DbType.String, m_bjhd_date);
            db.AddInParameter(cmd, "@BnpmH_Bjhd_Times", DbType.String, m_bjhd_times);
            db.AddInParameter(cmd, "@BnpmH_Bjhd_MainBuyer", DbType.String, m_bjhd_mainbuyer);
            db.AddInParameter(cmd, "@BnpmH_Bjhd_Sample", DbType.String, m_bjhd_sample);
            db.AddInParameter(cmd, "@BnpmH_Bonsa_Check", DbType.String, m_bonsa_check);
            db.AddInParameter(cmd, "@BnpmH_Remark", DbType.String, m_remark);
            db.AddInParameter(cmd, "@BnpmH_ModifyDate", DbType.DateTime, m_mdate);
            db.AddInParameter(cmd, "@BnpmH_ModifySaWon", DbType.String, m_msawon);

            db.AddInParameter(cmd, "@BnpmH_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmH_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmH_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmH_Sample", DbType.String, m_sample);

            db.ExecuteNonQuery(cmd);

            whis.InsertWork("AS접수", "헤더수정", cmd);
        }

        public void InsertData()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" INSERT INTO " + preVal + "BNPMD (BnpmD_Date,BnpmD_Times,BnpmD_MainBuyer,BnpmD_Sample,BnpmD_StyleNox,BnpmD_StyleNoxLine,BnpmD_BnpmCode ");
            sb.Append(" ,BnpmD_Qty01,BnpmD_Qty02,BnpmD_Qty03,BnpmD_Qty04,BnpmD_Qty05,BnpmD_Qty06,BnpmD_Qty07,BnpmD_Qty08,BnpmD_Qty09,BnpmD_Qty10,BnpmD_Qty11,BnpmD_Qty12,BnpmD_Qty13,BnpmD_Qty14,BnpmD_Qty15,BnpmD_Qty16,BnpmD_Qty17,BnpmD_QtyTotal ");
            sb.Append(" ,BnpmD_JustPrice,BnpmD_JustAmount,BnpmD_UsedRemark,BnpmD_ReasonRemark) ");
            sb.Append(" VALUES(@BnpmD_Date,@BnpmD_Times,@BnpmD_MainBuyer,@BnpmD_Sample,@BnpmD_StyleNox,@BnpmD_StyleNoxLine,@BnpmD_BnpmCode ");
            sb.Append(" ,@BnpmD_Qty01,@BnpmD_Qty02,@BnpmD_Qty03,@BnpmD_Qty04,@BnpmD_Qty05,@BnpmD_Qty06,@BnpmD_Qty07,@BnpmD_Qty08,@BnpmD_Qty09,@BnpmD_Qty10,@BnpmD_Qty11,@BnpmD_Qty12,@BnpmD_Qty13,@BnpmD_Qty14,@BnpmD_Qty15,@BnpmD_Qty16,@BnpmD_Qty17,@BnpmD_QtyTotal ");
            sb.Append(" ,@BnpmD_JustPrice,@BnpmD_JustAmount,@BnpmD_UsedRemark,@BnpmD_ReasonRemark) ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            
            db.AddInParameter(cmd, "@BnpmD_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmD_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmD_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmD_Sample", DbType.String, m_sample);
            db.AddInParameter(cmd, "@BnpmD_StyleNox", DbType.String, m_stylenox);
            db.AddInParameter(cmd, "@BnpmD_StyleNoxLine", DbType.Int32, MaxLine());
            db.AddInParameter(cmd, "@BnpmD_BnpmCode", DbType.String, m_bnpmcode);
            db.AddInParameter(cmd, "@BnpmD_Qty01", DbType.Int32, m_qty01);
            db.AddInParameter(cmd, "@BnpmD_Qty02", DbType.Int32, m_qty02);
            db.AddInParameter(cmd, "@BnpmD_Qty03", DbType.Int32, m_qty03);
            db.AddInParameter(cmd, "@BnpmD_Qty04", DbType.Int32, m_qty04);
            db.AddInParameter(cmd, "@BnpmD_Qty05", DbType.Int32, m_qty05);
            db.AddInParameter(cmd, "@BnpmD_Qty06", DbType.Int32, m_qty06);
            db.AddInParameter(cmd, "@BnpmD_Qty07", DbType.Int32, m_qty07);
            db.AddInParameter(cmd, "@BnpmD_Qty08", DbType.Int32, m_qty08);
            db.AddInParameter(cmd, "@BnpmD_Qty09", DbType.Int32, m_qty09);
            db.AddInParameter(cmd, "@BnpmD_Qty10", DbType.Int32, m_qty10);
            db.AddInParameter(cmd, "@BnpmD_Qty11", DbType.Int32, m_qty11);
            db.AddInParameter(cmd, "@BnpmD_Qty12", DbType.Int32, m_qty12);
            db.AddInParameter(cmd, "@BnpmD_Qty13", DbType.Int32, m_qty13);
            db.AddInParameter(cmd, "@BnpmD_Qty14", DbType.Int32, m_qty14);
            db.AddInParameter(cmd, "@BnpmD_Qty15", DbType.Int32, m_qty15);
            db.AddInParameter(cmd, "@BnpmD_Qty16", DbType.Int32, m_qty16);
            db.AddInParameter(cmd, "@BnpmD_Qty17", DbType.Int32, m_qty17);
            db.AddInParameter(cmd, "@BnpmD_QtyTotal", DbType.Int32, m_qtytotal);
            db.AddInParameter(cmd, "@BnpmD_JustPrice", DbType.Double, m_justprice);
            db.AddInParameter(cmd, "@BnpmD_JustAmount", DbType.Double, m_justamount);
            db.AddInParameter(cmd, "@BnpmD_UsedRemark", DbType.String, m_usedremark);
            db.AddInParameter(cmd, "@BnpmD_ReasonRemark", DbType.String, m_reasonremark);

            whis.InsertWork("AS접수", "상세입력", cmd);
            db.ExecuteNonQuery(cmd);

        }

        public void UpdateData()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE " + preVal + "BNPMD SET ");
            sb.Append(" BnpmD_StyleNoxLine=@BnpmD_StyleNoxLine, ");
            sb.Append(" BnpmD_BnpmCode=@BnpmD_BnpmCode, ");
            sb.Append(" BnpmD_Qty01=@BnpmD_Qty01, ");
            sb.Append(" BnpmD_Qty02=@BnpmD_Qty02, ");
            sb.Append(" BnpmD_Qty03=@BnpmD_Qty03, ");
            sb.Append(" BnpmD_Qty04=@BnpmD_Qty04, ");
            sb.Append(" BnpmD_Qty05=@BnpmD_Qty05, ");
            sb.Append(" BnpmD_Qty06=@BnpmD_Qty06, ");
            sb.Append(" BnpmD_Qty07=@BnpmD_Qty07, ");
            sb.Append(" BnpmD_Qty08=@BnpmD_Qty08, ");
            sb.Append(" BnpmD_Qty09=@BnpmD_Qty09, ");
            sb.Append(" BnpmD_Qty10=@BnpmD_Qty10, ");
            sb.Append(" BnpmD_Qty11=@BnpmD_Qty11, ");
            sb.Append(" BnpmD_Qty12=@BnpmD_Qty12, ");
            sb.Append(" BnpmD_Qty13=@BnpmD_Qty13, ");
            sb.Append(" BnpmD_Qty14=@BnpmD_Qty14, ");
            sb.Append(" BnpmD_Qty15=@BnpmD_Qty15, ");
            sb.Append(" BnpmD_Qty16=@BnpmD_Qty16, ");
            sb.Append(" BnpmD_Qty17=@BnpmD_Qty17, ");
            sb.Append(" BnpmD_QtyTotal=@BnpmD_QtyTotal, ");
            sb.Append(" BnpmD_JustPrice=@BnpmD_JustPrice, ");
            sb.Append(" BnpmD_JustAmount=@BnpmD_JustAmount, ");
            sb.Append(" BnpmD_UsedRemark=@BnpmD_UsedRemark, ");
            sb.Append(" BnpmD_ReasonRemark=@BnpmD_ReasonRemark ");
            sb.Append(" Where ");
            sb.Append(" BnpmD_Date=@BnpmD_Date and ");
            sb.Append(" BnpmD_Times=@BnpmD_Times and ");
            sb.Append(" BnpmD_MainBuyer=@BnpmD_MainBuyer and ");
            sb.Append(" BnpmD_Sample=@BnpmD_Sample");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmD_StyleNoxLine", DbType.Int32, m_stylenoxline);
            db.AddInParameter(cmd, "@BnpmD_BnpmCode", DbType.String, m_bnpmcode);
            db.AddInParameter(cmd, "@BnpmD_Qty01", DbType.Int32, m_qty01);
            db.AddInParameter(cmd, "@BnpmD_Qty02", DbType.Int32, m_qty02);
            db.AddInParameter(cmd, "@BnpmD_Qty03", DbType.Int32, m_qty03);
            db.AddInParameter(cmd, "@BnpmD_Qty04", DbType.Int32, m_qty04);
            db.AddInParameter(cmd, "@BnpmD_Qty05", DbType.Int32, m_qty05);
            db.AddInParameter(cmd, "@BnpmD_Qty06", DbType.Int32, m_qty06);
            db.AddInParameter(cmd, "@BnpmD_Qty07", DbType.Int32, m_qty07);
            db.AddInParameter(cmd, "@BnpmD_Qty08", DbType.Int32, m_qty08);
            db.AddInParameter(cmd, "@BnpmD_Qty09", DbType.Int32, m_qty09);
            db.AddInParameter(cmd, "@BnpmD_Qty10", DbType.Int32, m_qty10);
            db.AddInParameter(cmd, "@BnpmD_Qty11", DbType.Int32, m_qty11);
            db.AddInParameter(cmd, "@BnpmD_Qty12", DbType.Int32, m_qty12);
            db.AddInParameter(cmd, "@BnpmD_Qty13", DbType.Int32, m_qty13);
            db.AddInParameter(cmd, "@BnpmD_Qty14", DbType.Int32, m_qty14);
            db.AddInParameter(cmd, "@BnpmD_Qty15", DbType.Int32, m_qty15);
            db.AddInParameter(cmd, "@BnpmD_Qty16", DbType.Int32, m_qty16);
            db.AddInParameter(cmd, "@BnpmD_Qty17", DbType.Int32, m_qty17);
            db.AddInParameter(cmd, "@BnpmD_QtyTotal", DbType.Int32, m_qtytotal);
            db.AddInParameter(cmd, "@BnpmD_JustPrice", DbType.Double, m_justprice);
            db.AddInParameter(cmd, "@BnpmD_JustAmount", DbType.Double, m_justamount);
            db.AddInParameter(cmd, "@BnpmD_UsedRemark", DbType.String, m_usedremark);
            db.AddInParameter(cmd, "@BnpmD_ReasonRemark", DbType.String, m_reasonremark);

            db.AddInParameter(cmd, "@BnpmD_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmD_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmD_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmD_Sample", DbType.String, m_sample);

            whis.InsertWork("AS접수", "상세수정", cmd);
            db.ExecuteNonQuery(cmd);
        }

        public void DeleteData()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" DELETE FROM " + preVal + "BNPMD ");
            sb.Append(" Where ");
            sb.Append(" BnpmD_Date=@BnpmD_Date and ");
            sb.Append(" BnpmD_Times=@BnpmD_Times and ");
            sb.Append(" BnpmD_MainBuyer=@BnpmD_MainBuyer and ");
            sb.Append(" BnpmD_Sample=@BnpmD_Sample");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            
            db.AddInParameter(cmd, "@BnpmD_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmD_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmD_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmD_Sample", DbType.String, m_sample);

            whis.InsertWork("AS접수", "상세삭제", cmd);
            db.ExecuteNonQuery(cmd);
        }

        public void InsertFile()
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

            StringBuilder sb = new StringBuilder();
            
            sb.Append(" INSERT INTO " + preVal + "BNPMX (BnpmX_Date,BnpmX_Times,BnpmX_MainBuyer,BnpmX_Sample,BnpmX_LineSeqx,BnpmX_imageFileName) ");
            sb.Append(" VALUES(@BnpmX_Date,@BnpmX_Times,@BnpmX_MainBuyer,@BnpmX_Sample,@BnpmX_LineSeqx,@BnpmX_imageFileName) ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmX_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmX_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmX_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmX_Sample", DbType.String, m_sample);
            db.AddInParameter(cmd, "@BnpmX_LineSeqx", DbType.Int32, MaxFileLine());
            db.AddInParameter(cmd, "@BnpmX_imageFileName", DbType.String, m_imagefilename);

            db.ExecuteNonQuery(cmd);

            whis.InsertWork("AS접수", "첨부파일", cmd);
        }

        public void DeleteFile()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" DELETE FROM " + preVal + "BNPMX ");
            sb.Append(" Where ");
            sb.Append(" BnpmX_Date=@BnpmX_Date and ");
            sb.Append(" BnpmX_Times=@BnpmX_Times and ");
            sb.Append(" BnpmX_MainBuyer=@BnpmX_MainBuyer and ");
            sb.Append(" BnpmX_Sample=@BnpmX_Sample and ");
            sb.Append(" BnpmX_imageFileName=@BnpmX_imageFileName");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmX_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmX_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmX_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmX_Sample", DbType.String, m_sample);
            db.AddInParameter(cmd, "@BnpmX_imageFileName", DbType.String, m_imagefilename);

            whis.InsertWork("AS접수", "첨부파일삭제", cmd);
            db.ExecuteNonQuery(cmd);
        }

        public int MaxLine()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" select isnull(max([BnpmD_StyleNoxLine]) + 1,1) as maxLine from " + preVal + "BNPMD ");
            sb.Append(" where BnpmD_Date=@BnpmD_Date and BnpmD_Times=@BnpmD_Times and BnpmD_MainBuyer=@BnpmD_MainBuyer and BnpmD_Sample=@BnpmD_Sample ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmD_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmD_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmD_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmD_Sample", DbType.String, m_sample);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }

        public int MaxFileLine()
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

            StringBuilder sb = new StringBuilder();

            sb.Append(" select isnull(max([BnpmX_LineSeqx]) + 1,1) as maxLine from " + preVal + "BNPMX ");
            sb.Append(" where BnpmX_Date=@BnpmX_Date and BnpmX_Times=@BnpmX_Times and BnpmX_MainBuyer=@BnpmX_MainBuyer and BnpmX_Sample=@BnpmX_Sample ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@BnpmX_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@BnpmX_Times", DbType.String, m_times);
            db.AddInParameter(cmd, "@BnpmX_MainBuyer", DbType.String, m_mainbuyer);
            db.AddInParameter(cmd, "@BnpmX_Sample", DbType.String, m_sample);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
    }
}
