using System;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using FirstOrder.Util;

namespace FirstOrder.Data
{
    public class OrderData_abl
    {
        SqlDatabase db = StDBConn.GetOpenDB(OpenDBType.mainDB);

        WorkHistory whis = new WorkHistory();

        private string m_date;
        private string m_time;
        private string m_kure;
        private string m_baesong;
        private string m_baesongname;
        private string m_etc;
        private string m_net;
        private string m_vat;
        private string m_hap;
        private string m_product;
        private double m_order1;
        private double m_order2;
        private double m_order3;
        private double m_order4;
        private double m_order5;
        private double m_order6;
        private double m_order7;
        private double m_order8;
        private double m_order9;
        private double m_order10;
        private double m_order11;
        private double m_order12;
        private double m_order13;
        private double m_order14;
        private double m_order15;
        private double m_order16;
        private double m_order17;
        private double m_ordertotal;
        private DateTime m_sdate;
        private string m_ssawon;
        private DateTime m_mdate;
        private string m_msawon;
        private string m_line;
        private string m_justprice;
        private string m_justamount;
        private string m_dnga1;
        private string m_dnga2;
        private string m_dnga3;
        private string m_dnga4;
        private string m_dnga5;
        private string m_dnga6;
        private string m_dnga7;
        private string m_dnga8;
        private string m_dnga9;
        private string m_dnga10;
        private string m_dnga11;
        private string m_dnga12;
        private string m_dnga13;
        private string m_dnga14;
        private string m_dnga15;
        private string m_dnga16;
        private string m_dnga17;
        private string m_dngaLowPrice;
        private string m_dngaJustPrice;
        private string m_dngaBigSizePrice;
        private double m_dngaSizeBoxQty;
        private double m_dngaBoxQty;
        private string m_dngaIpgoPrice;

        public string Date { get { return m_date; } set { m_date = value; } }
        public string Time { get { return m_time; } set { m_time = value; } }
        public string Kure { get { return m_kure; } set { m_kure = value; } }
        public string Baesong { get { return m_baesong; } set { m_baesong = value; } }
        public string BaesongName { get { return m_baesongname; } set { m_baesongname = value; } }
        public string Etc { get { return m_etc; } set { m_etc = value; } }
        public string Net { get { return m_net; } set { m_net = value; } }
        public string Vat { get { return m_vat; } set { m_vat = value; } }
        public string Hap { get { return m_hap; } set { m_hap = value; } }
        public string Product { get { return m_product; } set { m_product = value; } }
        public double Order1 { get { return m_order1; } set { m_order1 = value; } }
        public double Order2 { get { return m_order2; } set { m_order2 = value; } }
        public double Order3 { get { return m_order3; } set { m_order3 = value; } }
        public double Order4 { get { return m_order4; } set { m_order4 = value; } }
        public double Order5 { get { return m_order5; } set { m_order5 = value; } }
        public double Order6 { get { return m_order6; } set { m_order6 = value; } }
        public double Order7 { get { return m_order7; } set { m_order7 = value; } }
        public double Order8 { get { return m_order8; } set { m_order8 = value; } }
        public double Order9 { get { return m_order9; } set { m_order9 = value; } }
        public double Order10 { get { return m_order10; } set { m_order10 = value; } }
        public double Order11 { get { return m_order11; } set { m_order11 = value; } }
        public double Order12 { get { return m_order12; } set { m_order12 = value; } }
        public double Order13 { get { return m_order13; } set { m_order13 = value; } }
        public double Order14 { get { return m_order14; } set { m_order14 = value; } }
        public double Order15 { get { return m_order15; } set { m_order15 = value; } }
        public double Order16 { get { return m_order16; } set { m_order16 = value; } }
        public double Order17 { get { return m_order17; } set { m_order17 = value; } }
        public double OrderTotal { get { return m_ordertotal; } set { m_ordertotal = value; } }
        public DateTime Sdate { get { return m_sdate; } set { m_sdate = value; } }
        public string Ssawon { get { return m_ssawon; } set { m_ssawon = value; } }
        public DateTime Mdate { get { return m_mdate; } set { m_mdate = value; } }
        public string Msawon { get { return m_msawon; } set { m_msawon = value; } }
        public string Line { get { return m_line; } set { m_line = value; } }
        public string JustPrice { get { return m_justprice; } set { m_justprice = value; } }
        public string JustAmount { get { return m_justamount; } set { m_justamount = value; } }
        public string Dnga1 { get { return m_dnga1; } set { m_dnga1 = value; } }
        public string Dnga2 { get { return m_dnga2; } set { m_dnga2 = value; } }
        public string Dnga3 { get { return m_dnga3; } set { m_dnga3 = value; } }
        public string Dnga4 { get { return m_dnga4; } set { m_dnga4 = value; } }
        public string Dnga5 { get { return m_dnga5; } set { m_dnga5 = value; } }
        public string Dnga6 { get { return m_dnga6; } set { m_dnga6 = value; } }
        public string Dnga7 { get { return m_dnga7; } set { m_dnga7 = value; } }
        public string Dnga8 { get { return m_dnga8; } set { m_dnga8 = value; } }
        public string Dnga9 { get { return m_dnga9; } set { m_dnga9 = value; } }
        public string Dnga10 { get { return m_dnga10; } set { m_dnga10 = value; } }
        public string Dnga11 { get { return m_dnga11; } set { m_dnga11 = value; } }
        public string Dnga12 { get { return m_dnga12; } set { m_dnga12 = value; } }
        public string Dnga13 { get { return m_dnga13; } set { m_dnga13 = value; } }
        public string Dnga14 { get { return m_dnga14; } set { m_dnga14 = value; } }
        public string Dnga15 { get { return m_dnga15; } set { m_dnga15 = value; } }
        public string Dnga16 { get { return m_dnga16; } set { m_dnga16 = value; } }
        public string Dnga17 { get { return m_dnga17; } set { m_dnga17 = value; } }
        public string DngaLowPrice { get { return m_dngaLowPrice; } set { m_dngaLowPrice = value; } }
        public string DngaJustPrice { get { return m_dngaJustPrice; } set { m_dngaJustPrice = value; } }
        public string DngaBigSizePrice { get { return m_dngaBigSizePrice; } set { m_dngaBigSizePrice = value; } }
        public double DngaSizeBoxQty { get { return m_dngaSizeBoxQty; } set { m_dngaSizeBoxQty = value; } }
        public double DngaBoxQty { get { return m_dngaBoxQty; } set { m_dngaBoxQty = value; } }
        public string DngaIpgoPrice { get { return m_dngaIpgoPrice; } set { m_dngaIpgoPrice = value; } }

        public void InsertHead()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(" select * from ablBJHD where ");
            sb.Append(" Bjhd_Date='" + m_date + "' and Bjhd_Times='" + m_time + "' and Bjhd_MainBuyer='" + m_kure + "'");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables[0].Rows.Count == 0)
            {
                sb = new StringBuilder();
                sb.Append(" INSERT INTO ablBJHD (Bjhd_Date,Bjhd_Times,Bjhd_MainBuyer,Bjhd_Sample,Bjhd_BaeSong,Bjhd_BaeSongName,Bjhd_NetAmount,Bjhd_VatAmount,Bjhd_HapAmount,Bjhd_Remark ");
                sb.Append(" ,Bjhd_CreateDate,Bjhd_CreateSawon,Bjhd_ModifyDate,Bjhd_ModifySaWon,Bjhd_Bonsa_Check) ");
                sb.Append(" VALUES(@Bjhd_Date,@Bjhd_Times,@Bjhd_MainBuyer,@Bjhd_Sample,@Bjhd_BaeSong,@Bjhd_BaeSongName,@Bjhd_NetAmount,@Bjhd_VatAmount,@Bjhd_HapAmount,@Bjhd_Remark ");
                sb.Append(" ,@Bjhd_CreateDate,@Bjhd_CreateSawon,@Bjhd_ModifyDate,@Bjhd_ModifySaWon,@Bjhd_Bonsa_Check) ");

                cmd = db.GetSqlStringCommand(sb.ToString());

                db.AddInParameter(cmd, "@Bjhd_Date", DbType.String, m_date);
                db.AddInParameter(cmd, "@Bjhd_Times", DbType.String, m_time);
                db.AddInParameter(cmd, "@Bjhd_MainBuyer", DbType.String, m_kure);
                db.AddInParameter(cmd, "@Bjhd_Sample", DbType.String, "0");
                db.AddInParameter(cmd, "@Bjhd_BaeSong", DbType.String, m_baesong);
                db.AddInParameter(cmd, "@Bjhd_BaeSongName", DbType.String, m_baesongname);
                db.AddInParameter(cmd, "@Bjhd_NetAmount", DbType.Double, m_net);
                db.AddInParameter(cmd, "@Bjhd_VatAmount", DbType.Double, m_vat);
                db.AddInParameter(cmd, "@Bjhd_HapAmount", DbType.Double, m_hap);
                db.AddInParameter(cmd, "@Bjhd_Remark", DbType.String, m_etc);
                db.AddInParameter(cmd, "@Bjhd_CreateDate", DbType.DateTime, m_sdate);
                db.AddInParameter(cmd, "@Bjhd_CreateSawon", DbType.String, m_ssawon);
                db.AddInParameter(cmd, "@Bjhd_ModifyDate", DbType.DateTime, m_mdate);
                db.AddInParameter(cmd, "@Bjhd_ModifySaWon", DbType.String, m_msawon);
                db.AddInParameter(cmd, "@Bjhd_Bonsa_Check", DbType.String, "0");
                
                db.ExecuteNonQuery(cmd);

                whis.InsertWork("발주", "헤더입력", cmd);
            }
            else
            {
                UpdateHead();
            }
        }

        public void UpdateHead()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE ablBJHD SET ");
            sb.Append(" Bjhd_BaeSong=@Bjhd_BaeSong, ");
            sb.Append(" Bjhd_BaeSongName=@Bjhd_BaeSongName, ");
            sb.Append(" Bjhd_NetAmount=@Bjhd_NetAmount, ");
            sb.Append(" Bjhd_VatAmount=@Bjhd_VatAmount, ");
            sb.Append(" Bjhd_HapAmount=@Bjhd_HapAmount, ");
            sb.Append(" Bjhd_Remark=@Bjhd_Remark, ");
            sb.Append(" Bjhd_ModifyDate=@Bjhd_ModifyDate, ");
            sb.Append(" Bjhd_ModifySaWon=@Bjhd_ModifySaWon ");
            sb.Append(" Where ");
            sb.Append(" Bjhd_Date=@Bjhd_Date and ");
            sb.Append(" Bjhd_Times=@Bjhd_Times and ");
            sb.Append(" Bjhd_MainBuyer=@Bjhd_MainBuyer ");
            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Bjhd_BaeSong", DbType.String, m_baesong);
            db.AddInParameter(cmd, "@Bjhd_BaeSongName", DbType.String, m_baesongname);
            db.AddInParameter(cmd, "@Bjhd_NetAmount", DbType.Double, m_net);
            db.AddInParameter(cmd, "@Bjhd_VatAmount", DbType.Double, m_vat);
            db.AddInParameter(cmd, "@Bjhd_HapAmount", DbType.Double, m_hap);
            db.AddInParameter(cmd, "@Bjhd_Remark", DbType.String, m_etc);
            db.AddInParameter(cmd, "@Bjhd_ModifyDate", DbType.DateTime, m_mdate);
            db.AddInParameter(cmd, "@Bjhd_ModifySaWon", DbType.String, m_msawon);

            db.AddInParameter(cmd, "@Bjhd_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@Bjhd_Times", DbType.String, m_time);
            db.AddInParameter(cmd, "@Bjhd_MainBuyer", DbType.String, m_kure);

            db.ExecuteNonQuery(cmd);

            whis.InsertWork("발주", "헤더수정", cmd);
        }

        public void InsertData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" INSERT INTO ablBLJU (Blju_Date,Blju_Times,Blju_MainBuyer,Blju_Sample,Blju_Line,Blju_StyleNox,Blju_Qty01,Blju_Qty02,Blju_Qty03,Blju_Qty04,Blju_Qty05,Blju_Qty06,Blju_Qty07,Blju_Qty08,Blju_Qty09 ");
            sb.Append(" ,Blju_Qty10,Blju_Qty11,Blju_Qty12,Blju_Qty13,Blju_Qty14,Blju_Qty15,Blju_Qty16,Blju_Qty17,Blju_QtyTotal ");
            sb.Append(" ,Blju_Dnga01,Blju_Dnga02,Blju_Dnga03,Blju_Dnga04,Blju_Dnga05,Blju_Dnga06,Blju_Dnga07,Blju_Dnga08,Blju_Dnga09,Blju_Dnga10,Blju_Dnga11,Blju_Dnga12,Blju_Dnga13,Blju_Dnga14,Blju_Dnga15,Blju_Dnga16,Blju_Dnga17 ");
            sb.Append(" ,Blju_JustPrice,Blju_JustAmount,Blju_Remark ");
            sb.Append(" ,Blju_DngaLowPrice,Blju_DngaJustPrice,Blju_DngaBigSizePrice,Blju_DngaSizeBoxQty,Blju_DngaBoxQty,Blju_DngaIpgoPrice) ");
            sb.Append(" values(@Blju_Date,@Blju_Times,@Blju_MainBuyer,@Blju_Sample,@Blju_Line,@Blju_StyleNox,@Blju_Qty01,@Blju_Qty02,@Blju_Qty03,@Blju_Qty04,@Blju_Qty05,@Blju_Qty06,@Blju_Qty07,@Blju_Qty08,@Blju_Qty09 ");
            sb.Append(" ,@Blju_Qty10,@Blju_Qty11,@Blju_Qty12,@Blju_Qty13,@Blju_Qty14,@Blju_Qty15,@Blju_Qty16,@Blju_Qty17,@Blju_QtyTotal ");
            sb.Append(" ,@Blju_Dnga01,@Blju_Dnga02,@Blju_Dnga03,@Blju_Dnga04,@Blju_Dnga05,@Blju_Dnga06,@Blju_Dnga07,@Blju_Dnga08,@Blju_Dnga09,@Blju_Dnga10,@Blju_Dnga11,@Blju_Dnga12,@Blju_Dnga13,@Blju_Dnga14,@Blju_Dnga15,@Blju_Dnga16,@Blju_Dnga17 ");
            sb.Append(" ,@Blju_JustPrice,@Blju_JustAmount,@Blju_Remark ");
            sb.Append(" ,@Blju_DngaLowPrice,@Blju_DngaJustPrice,@Blju_DngaBigSizePrice,@Blju_DngaSizeBoxQty,@Blju_DngaBoxQty,@Blju_DngaIpgoPrice) ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Blju_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@Blju_Times", DbType.String, m_time);
            db.AddInParameter(cmd, "@Blju_MainBuyer", DbType.String, m_kure);
            db.AddInParameter(cmd, "@Blju_Sample", DbType.String, "0");
            db.AddInParameter(cmd, "@Blju_Line", DbType.Int32, MaxLine());
            db.AddInParameter(cmd, "@Blju_StyleNox", DbType.String, m_product);
            db.AddInParameter(cmd, "@Blju_Qty01", DbType.Int32, m_order1);
            db.AddInParameter(cmd, "@Blju_Qty02", DbType.Int32, m_order2);
            db.AddInParameter(cmd, "@Blju_Qty03", DbType.Int32, m_order3);
            db.AddInParameter(cmd, "@Blju_Qty04", DbType.Int32, m_order4);
            db.AddInParameter(cmd, "@Blju_Qty05", DbType.Int32, m_order5);
            db.AddInParameter(cmd, "@Blju_Qty06", DbType.Int32, m_order6);
            db.AddInParameter(cmd, "@Blju_Qty07", DbType.Int32, m_order7);
            db.AddInParameter(cmd, "@Blju_Qty08", DbType.Int32, m_order8);
            db.AddInParameter(cmd, "@Blju_Qty09", DbType.Int32, m_order9);
            db.AddInParameter(cmd, "@Blju_Qty10", DbType.Int32, m_order10);
            db.AddInParameter(cmd, "@Blju_Qty11", DbType.Int32, m_order11);
            db.AddInParameter(cmd, "@Blju_Qty12", DbType.Int32, m_order12);
            db.AddInParameter(cmd, "@Blju_Qty13", DbType.Int32, m_order13);
            db.AddInParameter(cmd, "@Blju_Qty14", DbType.Int32, m_order14);
            db.AddInParameter(cmd, "@Blju_Qty15", DbType.Int32, m_order15);
            db.AddInParameter(cmd, "@Blju_Qty16", DbType.Int32, m_order16);
            db.AddInParameter(cmd, "@Blju_Qty17", DbType.Int32, m_order17);
            db.AddInParameter(cmd, "@Blju_QtyTotal", DbType.Int32, m_ordertotal);
            db.AddInParameter(cmd, "@Blju_Dnga01", DbType.Double, m_dnga1);
            db.AddInParameter(cmd, "@Blju_Dnga02", DbType.Double, m_dnga2);
            db.AddInParameter(cmd, "@Blju_Dnga03", DbType.Double, m_dnga3);
            db.AddInParameter(cmd, "@Blju_Dnga04", DbType.Double, m_dnga4);
            db.AddInParameter(cmd, "@Blju_Dnga05", DbType.Double, m_dnga5);
            db.AddInParameter(cmd, "@Blju_Dnga06", DbType.Double, m_dnga6);
            db.AddInParameter(cmd, "@Blju_Dnga07", DbType.Double, m_dnga7);
            db.AddInParameter(cmd, "@Blju_Dnga08", DbType.Double, m_dnga8);
            db.AddInParameter(cmd, "@Blju_Dnga09", DbType.Double, m_dnga9);
            db.AddInParameter(cmd, "@Blju_Dnga10", DbType.Double, m_dnga10);
            db.AddInParameter(cmd, "@Blju_Dnga11", DbType.Double, m_dnga11);
            db.AddInParameter(cmd, "@Blju_Dnga12", DbType.Double, m_dnga12);
            db.AddInParameter(cmd, "@Blju_Dnga13", DbType.Double, m_dnga13);
            db.AddInParameter(cmd, "@Blju_Dnga14", DbType.Double, m_dnga14);
            db.AddInParameter(cmd, "@Blju_Dnga15", DbType.Double, m_dnga15);
            db.AddInParameter(cmd, "@Blju_Dnga16", DbType.Double, m_dnga16);
            db.AddInParameter(cmd, "@Blju_Dnga17", DbType.Double, m_dnga17);
            db.AddInParameter(cmd, "@Blju_JustPrice", DbType.Double, m_justprice);
            db.AddInParameter(cmd, "@Blju_JustAmount", DbType.Double, m_justamount);
            db.AddInParameter(cmd, "@Blju_Remark", DbType.String, "");
            db.AddInParameter(cmd, "@Blju_DngaLowPrice", DbType.Double, m_dngaLowPrice);
            db.AddInParameter(cmd, "@Blju_DngaJustPrice", DbType.Double, m_dngaJustPrice);
            db.AddInParameter(cmd, "@Blju_DngaBigSizePrice", DbType.Double, m_dngaBigSizePrice);
            db.AddInParameter(cmd, "@Blju_DngaSizeBoxQty", DbType.Double, m_dngaSizeBoxQty);
            db.AddInParameter(cmd, "@Blju_DngaBoxQty", DbType.Double, m_dngaBoxQty);
            db.AddInParameter(cmd, "@Blju_DngaIpgoPrice", DbType.Double, StCommon.ToDouble(m_dngaIpgoPrice, 0));

            db.ExecuteNonQuery(cmd);

            whis.InsertWork("발주", "입력", cmd);
        }

        public void UpdateData()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE ablBLJU SET ");
            sb.Append(" Blju_StyleNox=@Blju_StyleNox, ");
            sb.Append(" Blju_Qty01=@Blju_Qty01, ");
            sb.Append(" Blju_Qty02=@Blju_Qty02, ");
            sb.Append(" Blju_Qty03=@Blju_Qty03, ");
            sb.Append(" Blju_Qty04=@Blju_Qty04, ");
            sb.Append(" Blju_Qty05=@Blju_Qty05, ");
            sb.Append(" Blju_Qty06=@Blju_Qty06, ");
            sb.Append(" Blju_Qty07=@Blju_Qty07, ");
            sb.Append(" Blju_Qty08=@Blju_Qty08, ");
            sb.Append(" Blju_Qty09=@Blju_Qty09, ");
            sb.Append(" Blju_Qty10=@Blju_Qty10, ");
            sb.Append(" Blju_Qty11=@Blju_Qty11, ");
            sb.Append(" Blju_Qty12=@Blju_Qty12, ");
            sb.Append(" Blju_Qty13=@Blju_Qty13, ");
            sb.Append(" Blju_Qty14=@Blju_Qty14, ");
            sb.Append(" Blju_Qty15=@Blju_Qty15, ");
            sb.Append(" Blju_Qty16=@Blju_Qty16, ");
            sb.Append(" Blju_Qty17=@Blju_Qty17, ");
            sb.Append(" Blju_QtyTotal=@Blju_QtyTotal, ");
            sb.Append(" Blju_Dnga01=@Blju_Dnga01, ");
            sb.Append(" Blju_Dnga02=@Blju_Dnga02, ");
            sb.Append(" Blju_Dnga03=@Blju_Dnga03, ");
            sb.Append(" Blju_Dnga04=@Blju_Dnga04, ");
            sb.Append(" Blju_Dnga05=@Blju_Dnga05, ");
            sb.Append(" Blju_Dnga06=@Blju_Dnga06, ");
            sb.Append(" Blju_Dnga07=@Blju_Dnga07, ");
            sb.Append(" Blju_Dnga08=@Blju_Dnga08, ");
            sb.Append(" Blju_Dnga09=@Blju_Dnga09, ");
            sb.Append(" Blju_Dnga10=@Blju_Dnga10, ");
            sb.Append(" Blju_Dnga11=@Blju_Dnga11, ");
            sb.Append(" Blju_Dnga12=@Blju_Dnga12, ");
            sb.Append(" Blju_Dnga13=@Blju_Dnga13, ");
            sb.Append(" Blju_Dnga14=@Blju_Dnga14, ");
            sb.Append(" Blju_Dnga15=@Blju_Dnga15, ");
            sb.Append(" Blju_Dnga16=@Blju_Dnga16, ");
            sb.Append(" Blju_Dnga17=@Blju_Dnga17, ");
            sb.Append(" Blju_JustPrice=@Blju_JustPrice, ");
            sb.Append(" Blju_JustAmount=@Blju_JustAmount, ");
            sb.Append(" Blju_Remark=@Blju_Remark, ");
            sb.Append(" Blju_DngaLowPrice=@Blju_DngaLowPrice, ");
            sb.Append(" Blju_DngaJustPrice=@Blju_DngaJustPrice, ");
            sb.Append(" Blju_DngaBigSizePrice=@Blju_DngaBigSizePrice, ");
            sb.Append(" Blju_DngaSizeBoxQty=@Blju_DngaSizeBoxQty, ");
            sb.Append(" Blju_DngaBoxQty=@Blju_DngaBoxQty, ");
            sb.Append(" Blju_DngaIpgoPrice=@Blju_DngaIpgoPrice ");
            sb.Append(" Where ");
            sb.Append(" Blju_Date=@Blju_Date and ");
            sb.Append(" Blju_Times=@Blju_Times and ");
            sb.Append(" Blju_MainBuyer=@Blju_MainBuyer and ");
            sb.Append(" Blju_Line=@Blju_Line");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());
            
            db.AddInParameter(cmd, "@Blju_StyleNox", DbType.String, m_product);
            db.AddInParameter(cmd, "@Blju_Qty01", DbType.Int32, m_order1);
            db.AddInParameter(cmd, "@Blju_Qty02", DbType.Int32, m_order2);
            db.AddInParameter(cmd, "@Blju_Qty03", DbType.Int32, m_order3);
            db.AddInParameter(cmd, "@Blju_Qty04", DbType.Int32, m_order4);
            db.AddInParameter(cmd, "@Blju_Qty05", DbType.Int32, m_order5);
            db.AddInParameter(cmd, "@Blju_Qty06", DbType.Int32, m_order6);
            db.AddInParameter(cmd, "@Blju_Qty07", DbType.Int32, m_order7);
            db.AddInParameter(cmd, "@Blju_Qty08", DbType.Int32, m_order8);
            db.AddInParameter(cmd, "@Blju_Qty09", DbType.Int32, m_order9);
            db.AddInParameter(cmd, "@Blju_Qty10", DbType.Int32, m_order10);
            db.AddInParameter(cmd, "@Blju_Qty11", DbType.Int32, m_order11);
            db.AddInParameter(cmd, "@Blju_Qty12", DbType.Int32, m_order12);
            db.AddInParameter(cmd, "@Blju_Qty13", DbType.Int32, m_order13);
            db.AddInParameter(cmd, "@Blju_Qty14", DbType.Int32, m_order14);
            db.AddInParameter(cmd, "@Blju_Qty15", DbType.Int32, m_order15);
            db.AddInParameter(cmd, "@Blju_Qty16", DbType.Int32, m_order16);
            db.AddInParameter(cmd, "@Blju_Qty17", DbType.Int32, m_order17);
            db.AddInParameter(cmd, "@Blju_QtyTotal", DbType.Int32, m_ordertotal);
            db.AddInParameter(cmd, "@Blju_Dnga01", DbType.Double, m_dnga1);
            db.AddInParameter(cmd, "@Blju_Dnga02", DbType.Double, m_dnga2);
            db.AddInParameter(cmd, "@Blju_Dnga03", DbType.Double, m_dnga3);
            db.AddInParameter(cmd, "@Blju_Dnga04", DbType.Double, m_dnga4);
            db.AddInParameter(cmd, "@Blju_Dnga05", DbType.Double, m_dnga5);
            db.AddInParameter(cmd, "@Blju_Dnga06", DbType.Double, m_dnga6);
            db.AddInParameter(cmd, "@Blju_Dnga07", DbType.Double, m_dnga7);
            db.AddInParameter(cmd, "@Blju_Dnga08", DbType.Double, m_dnga8);
            db.AddInParameter(cmd, "@Blju_Dnga09", DbType.Double, m_dnga9);
            db.AddInParameter(cmd, "@Blju_Dnga10", DbType.Double, m_dnga10);
            db.AddInParameter(cmd, "@Blju_Dnga11", DbType.Double, m_dnga11);
            db.AddInParameter(cmd, "@Blju_Dnga12", DbType.Double, m_dnga12);
            db.AddInParameter(cmd, "@Blju_Dnga13", DbType.Double, m_dnga13);
            db.AddInParameter(cmd, "@Blju_Dnga14", DbType.Double, m_dnga14);
            db.AddInParameter(cmd, "@Blju_Dnga15", DbType.Double, m_dnga15);
            db.AddInParameter(cmd, "@Blju_Dnga16", DbType.Double, m_dnga16);
            db.AddInParameter(cmd, "@Blju_Dnga17", DbType.Double, m_dnga17);
            db.AddInParameter(cmd, "@Blju_JustPrice", DbType.Double, m_justprice);
            db.AddInParameter(cmd, "@Blju_JustAmount", DbType.Double, m_justamount);
            db.AddInParameter(cmd, "@Blju_Remark", DbType.String, "");
            db.AddInParameter(cmd, "@Blju_DngaLowPrice", DbType.Double, m_dngaLowPrice);
            db.AddInParameter(cmd, "@Blju_DngaJustPrice", DbType.Double, m_dngaJustPrice);
            db.AddInParameter(cmd, "@Blju_DngaBigSizePrice", DbType.Double, m_dngaBigSizePrice);
            db.AddInParameter(cmd, "@Blju_DngaSizeBoxQty", DbType.Double, m_dngaSizeBoxQty);
            db.AddInParameter(cmd, "@Blju_DngaBoxQty", DbType.Double, m_dngaBoxQty);
            db.AddInParameter(cmd, "@Blju_DngaIpgoPrice", DbType.Double, StCommon.ToDouble(m_dngaIpgoPrice, 0));

            db.AddInParameter(cmd, "@Blju_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@Blju_Times", DbType.String, m_time);
            db.AddInParameter(cmd, "@Blju_MainBuyer", DbType.String, m_kure);
            db.AddInParameter(cmd, "@Blju_Line", DbType.Int32, m_line);

            whis.InsertWork("발주", "수정", cmd);
            db.ExecuteNonQuery(cmd);
        }

        public int MaxLine()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select isnull(max([Blju_Line]) + 1,1) as maxLine from ablBLJU ");
            sb.Append(" where Blju_Date=@Blju_Date and Blju_Times=@Blju_Times and Blju_MainBuyer=@Blju_MainBuyer AND Blju_Line < 999 ");

            DbCommand cmd = db.GetSqlStringCommand(sb.ToString());

            db.AddInParameter(cmd, "@Blju_Date", DbType.String, m_date);
            db.AddInParameter(cmd, "@Blju_Times", DbType.String, m_time);
            db.AddInParameter(cmd, "@Blju_MainBuyer", DbType.String, m_kure);

            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
    }
}
