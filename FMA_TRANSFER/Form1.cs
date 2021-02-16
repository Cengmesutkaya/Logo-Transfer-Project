using Microsoft.Win32;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace FMA_TRANSFER
{
    public partial class Form1 : Form
    {
        DBConnectionString dBConnectionString;
        MethodContext methodContext;
        SQLContext sQLContext;
        SendMail sendMail;
        Setting setting;
        RegistryKey key;
        int alpha = 0;
        int alpha1 = 255;
        Color newColor = Color.FromArgb(0, Color.Blue);
        Color newColor1 = Color.FromArgb(255, Color.Green);

        public Form1()
        {
            InitializeComponent();

            bool kontrol;
            Mutex mt = new Mutex(true, Application.ProductName, out kontrol);
            if (!kontrol)
            {
                MessageBox.Show("Program Zaten Çalışıyor", "UYARI!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
            else
            {

                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");

                key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                try
                {
                    if (key.GetValue("FMAStart1").ToString() == "\"" + Application.ExecutablePath + "\"")
                    {
                        checkBox1.Checked = true;
                    }
                }
                catch (Exception)
                { }
                dBConnectionString = new DBConnectionString();
                methodContext = new MethodContext(dBConnectionString.UnityDB());
                sQLContext = new SQLContext();
                setting = new Setting();
                sendMail = new SendMail();
                LoginSample();
            }

        }

        private void LoginSample()
        {
            bool IslogIn = (setting.UserName != "") && (Global.UnityApp.Login(setting.UserName, setting.Pass, Convert.ToInt32(setting.FirmaNo)));
            if (IslogIn)
            {
                lblLogin.Text = "Login OK";
            }
            else
            {
                string Err = Global.UnityApp.GetLastError().ToString() + ":" + Global.UnityApp.GetLastErrorString().ToString();
                MessageBox.Show(Err);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            simpleButton1_Click(this, null);
        }
        public void AddOrderFiche(DataTable dt_order, DataTable dt_lines)
        {
            UnityObjects.Data order = Global.UnityApp.NewDataObject(UnityObjects.DataObjectType.doSalesOrderSlip);
            for (int i = 0; i < dt_order.Rows.Count; i++)
            {
                try
                {
                    order.New();
                    order.DataFields.FieldByName("NUMBER").Value = dt_order.Rows[i]["NUMBER"].ToString(); // "ferit_deneme2";
                    order.DataFields.FieldByName("DATE").Value = dt_order.Rows[i]["DATE"].ToString(); // "04.11.2018";
                    order.DataFields.FieldByName("TIME").Value = dt_order.Rows[i]["TIME"].ToString(); //"303828495";
                    order.DataFields.FieldByName("AUXIL_CODE").Value = dt_order.Rows[i]["AUXIL_CODE"].ToString(); // "FERİT";
                    order.DataFields.FieldByName("AUTH_CODE").Value = dt_order.Rows[i]["AUTH_CODE"].ToString(); // "M";
                    order.DataFields.FieldByName("ARP_CODE").Value = dt_order.Rows[i]["ARP_CODE"].ToString(); //"LB67.1333.VPG17";
                    order.DataFields.FieldByName("SHIPLOC_CODE").Value = dt_order.Rows[i]["SHIPLOC_CODE"].ToString(); //"LB67.1333.VPG17";
                    order.DataFields.FieldByName("PAYMENT_CODE").Value = dt_order.Rows[i]["PAYMENT_CODE"].ToString(); //"LB67.1333.VPG17";
                    order.DataFields.FieldByName("SOURCE_WH").Value = dt_order.Rows[i]["SOURCE_WH"].ToString(); // "4";
                    order.DataFields.FieldByName("SOURCE_COST_GRP").Value = dt_order.Rows[i]["SOURCE_COST_GRP"].ToString(); // "4";
                    order.DataFields.FieldByName("DIVISION").Value = dt_order.Rows[i]["DIVISION"].ToString(); // "4";
                    order.DataFields.FieldByName("ORDER_STATUS").Value = dt_order.Rows[i]["ORDER_STATUS"].ToString(); // "4";
                    order.DataFields.FieldByName("CREATED_BY").Value = dt_order.Rows[i]["CREATED_BY"].ToString(); // "947";
                    order.DataFields.FieldByName("DATE_CREATED").Value = dt_order.Rows[i]["DATE_CREATED"].ToString(); //"04.11.2018";
                    order.DataFields.FieldByName("HOUR_CREATED").Value = dt_order.Rows[i]["HOUR_CREATED"].ToString(); // "18";
                    order.DataFields.FieldByName("MIN_CREATED").Value = dt_order.Rows[i]["MIN_CREATED"].ToString(); // "28";
                    order.DataFields.FieldByName("SEC_CREATED").Value = dt_order.Rows[i]["SEC_CREATED"].ToString(); //"47";
                    order.DataFields.FieldByName("MODIFIED_BY").Value = dt_order.Rows[i]["MODIFIED_BY"].ToString(); // "947";
                    order.DataFields.FieldByName("DATE_MODIFIED").Value = dt_order.Rows[i]["DATE_MODIFIED"].ToString(); //"04.11.2018";
                    order.DataFields.FieldByName("HOUR_MODIFIED").Value = dt_order.Rows[i]["HOUR_MODIFIED"].ToString(); // "18";
                    order.DataFields.FieldByName("MIN_MODIFIED").Value = dt_order.Rows[i]["MIN_MODIFIED"].ToString(); //"30";
                    order.DataFields.FieldByName("SEC_MODIFIED").Value = dt_order.Rows[i]["SEC_MODIFIED"].ToString(); //"10";
                    order.DataFields.FieldByName("SALESMAN_CODE").Value = dt_order.Rows[i]["SALESMAN_CODE"].ToString(); // "PG17";
                    order.DataFields.FieldByName("TRADING_GRP").Value = dt_order.Rows[i]["TRADING_GRP"].ToString(); //"L67";
                    order.DataFields.FieldByName("DOC_NUMBER").Value = dt_order.Rows[i]["DOC_NUMBER"].ToString();
                    UnityObjects.Lines transactions_lines = order.DataFields.FieldByName("TRANSACTIONS").Lines;
                    for (int j = 0; j < dt_lines.Rows.Count; j++)
                    {
                        if (dt_lines.Rows[j]["INVOICEREF"].ToString() == dt_order.Rows[i]["LOGICALREF"].ToString())
                        {
                            transactions_lines.AppendLine();
                            transactions_lines[transactions_lines.Count - 1].FieldByName("TYPE").Value = dt_lines.Rows[j]["TYPE"].ToString(); // "0";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("MASTER_CODE").Value = dt_lines.Rows[j]["MASTER_CODE"].ToString(); // "01ALM5000.81685649";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("QUANTITY").Value = dt_lines.Rows[j]["QUANTITY"].ToString(); // "1";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("PRICE").Value = dt_lines.Rows[j]["PRICE"].ToString(); // float.Parse("32");
                            transactions_lines[transactions_lines.Count - 1].FieldByName("VAT_RATE").Value = dt_lines.Rows[j]["VAT_RATE"].ToString(); // "18";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("UNIT_CODE").Value = dt_lines.Rows[j]["UNIT_CODE"].ToString(); // "Adet";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("DUE_DATE").Value = dt_lines.Rows[j]["DUE_DATE"].ToString(); // "04.11.2018";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("SOURCE_WH").Value = dt_lines.Rows[j]["SOURCE_WH"].ToString(); // "4";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("SOURCE_COST_GRP").Value = dt_lines.Rows[j]["SOURCE_COST_GRP"].ToString(); // "4";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("DIVISION").Value = dt_lines.Rows[j]["DIVISION"].ToString(); // "4";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("SALESMAN_CODE").Value = dt_lines.Rows[j]["SALESMAN_CODE"].ToString(); // "PG17";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("AFFECT_RISK").Value = dt_lines.Rows[j]["AFFECT_RISK"].ToString(); // "1";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("EDT_CURR").Value = dt_lines.Rows[j]["EDT_CURR"].ToString(); // "1";
                            transactions_lines[transactions_lines.Count - 1].FieldByName("PRODUCER_CODE").Value = dt_lines.Rows[j]["PRODUCER_CODE"].ToString(); // "81685649";
                            if (double.Parse(dt_lines.Rows[j]["DISCOUNT_RATE"].ToString()) > 0) // satır altı indirim tanımlama
                            {
                                transactions_lines.AppendLine();
                                transactions_lines[transactions_lines.Count - 1].FieldByName("TYPE").Value = "2";
                                transactions_lines[transactions_lines.Count - 1].FieldByName("DISCOUNT_RATE").Value = dt_lines.Rows[j]["DISCOUNT_RATE"].ToString(); //
                            }


                        }
                    }
                    order.DataFields.FieldByName("AFFECT_RISK").Value = "1";
                    order.FillAccCodes();
                    if (order.Post() == true)
                    {
                        if (UpdateStatu(dt_order.Rows[i]["NUMBER"].ToString()) > 0)
                        {
                            // MessageBox.Show("SİPARİŞ GÖNDERİLDİ!");
                            M_Log("Success", dt_order.Rows[i]["NUMBER"].ToString());
                        }
                        else
                        {
                            // MessageBox.Show("SİPARİŞ GÖNDERİLEMEDİ!");
                            M_Log("Error", dt_order.Rows[i]["NUMBER"].ToString());
                        }
                    }
                    else
                    {
                        if (order.ErrorCode != 0)
                        {
                            MessageBox.Show("DBError(" + order.ErrorCode.ToString() + ")-" + order.ErrorDesc + order.DBErrorDesc);
                        }
                        else if (order.ValidateErrors.Count > 0)
                        {
                            string result = "XML ErrorList:";
                            for (int z = 0; z < order.ValidateErrors.Count; z++)
                            {
                                result += "(" + order.ValidateErrors[z].ID.ToString() + ") - " + order.ValidateErrors[z].Error;
                            }
                            MessageBox.Show(result);
                        }
                    }
                }
                catch (Exception)
                {

                    sendMail.Send(dt_order.Rows[i]["SALESMAN_CODE"].ToString(), dt_order.Rows[i]["NUMBER"].ToString(), dt_order.Rows[i]["ARP_CODE"].ToString());
                }
            }


        }
        public int UpdateStatu(string Number)
        {
            return methodContext.Handle(sQLContext.SQL_UpdateQuery_Order_Statu(Number));
        }
        public static void M_Log(string Statu, String Number)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\TransferLogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + "   Tarihinde :" + Statu + " NUMBER: " + Number);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                try
                {
                    key.SetValue("FMAStart1", "\"" + Application.ExecutablePath + "\"");
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    key.DeleteValue("FMAStart1");
                }
                catch
                {
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            methodContext = new MethodContext(dBConnectionString.FMADB());
            DataTable dt_order = new DataTable();
            DataTable dt_lines = new DataTable();
            dt_order = methodContext.getTable(sQLContext.SQL_selectQuery_Order());
            dt_lines = methodContext.getTable(sQLContext.SQL_selectQuery_Lines());
            AddOrderFiche(dt_order, dt_lines);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            methodContext = new MethodContext(dBConnectionString.FMADB());
            DataTable dt_order = new DataTable();
            DataTable dt_lines = new DataTable();
            dt_order = methodContext.getTable(sQLContext.SQL_selectQuery_Order());
            dt_lines = methodContext.getTable(sQLContext.SQL_selectQuery_Lines());
            AddOrderFiche(dt_order, dt_lines);

            timer2.Start();

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            { simpleButton1.Text = "STOPPED"; timer1.Stop(); timer2.Stop(); }
            else { simpleButton1.Text = "TRANSFERRING"; timer1.Start(); }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            alpha += 10;  // change this for greater or less speed
            if (alpha >= 255) alpha = 0;
            this.simpleButton1.Appearance.BackColor = Color.FromArgb(alpha, newColor);

            if (this.simpleButton1.Appearance.BackColor.GetBrightness() < 0.3) simpleButton1.ForeColor = Color.White;

            alpha1 -= 10;  // change this for greater or less speed
            if (alpha1 <= 0) alpha1 = 255;
            this.simpleButton1.Appearance.BackColor2 = Color.FromArgb(alpha1, newColor1);

            if (this.simpleButton1.Appearance.BackColor2.GetBrightness() < 0.3) simpleButton1.ForeColor = Color.White;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Programın kapatılması sahadan atılan siparişlerin LOGO'ya aktarılmasını engelleyecektir.", "Emin Misiniz?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
