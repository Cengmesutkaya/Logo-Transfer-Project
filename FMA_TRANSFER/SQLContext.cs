using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA_TRANSFER
{
    
    class SQLContext
    {
        Setting setting = new Setting();
        public string SQL_selectQuery_Order()
        {
            return String.Format(@"SELECT [LOGICALREF]
,[NUMBER]
,[DATE]
,[TIME]
,[AUXIL_CODE]
,[AUTH_CODE]
,[ARP_CODE]
,[PAYMENT_CODE]
,[SHIPLOC_CODE]
,[SOURCE_WH]
,[SOURCE_COST_GRP]
,[DIVISION]
,[ORDER_STATUS]
,[CREATED_BY]
,[DATE_CREATED]
,[HOUR_CREATED]
,[MIN_CREATED]
,[SEC_CREATED]
,[MODIFIED_BY]
,[DATE_MODIFIED]
,[HOUR_MODIFIED]
,[MIN_MODIFIED]
,[SEC_MODIFIED]
,[SALESMAN_CODE]
,[TRADING_GRP]
,[STATUS]
,[DOC_NUMBER]
  FROM [dbo].[TBL_ORDER] where STATUS=0");

        }
        public string SQL_selectQuery_Lines()
        {
            return String.Format(@"SELECT [LOGICALREF]
,[INVOICEREF]
,[TYPE]
,[MASTER_CODE]
,[QUANTITY]
,[PRICE]
,[DISCOUNT_RATE]
,[VAT_RATE]
,[UNIT_CODE]
,[DUE_DATE]
,[SOURCE_WH]
,[SOURCE_COST_GRP]
,[DIVISION]
,[SALESMAN_CODE]
,[AFFECT_RISK]
,[EDT_CURR]
,[PRODUCER_CODE]
,[STATUS]
  FROM [dbo].[TBL_LINES] where STATUS=0");

        }

        public string SQL_UpdateQuery_Order_Statu(string Number)
        {
            return String.Format(@"exec Sps_StatusUpdate {0}", Number);

        }
        public string SQL_UpdateQuery_ORFLINE_DATE(string newDate, string OrdFicherefList)
        {
            return String.Format(@"update LG_"+setting.FirmaNo+"_"+setting.Donem+"_ORFLINE set DATE_='{0}' where ORDFICHEREF in ({1})", newDate, OrdFicherefList);

        }
        public string SQL_SelectQuery_Login()
        {
            return "Select [UserName],[Password] FROM [Person] P inner join UNITY_DB.dbo.TBL_FG_ADMIN TSA on TSA.User_NickName=P.UserName";

        }


    }
}
