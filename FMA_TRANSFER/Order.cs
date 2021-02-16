using System.Collections;
using System.Collections.Generic;

namespace FMA_TRANSFER
{
    class Order:IEnumerable
    {
        public List<OrderList> L_Order = new List<OrderList>();
        public List<LinesList> L_Lines = new List<LinesList>();
        public void Add(OrderList order)
        {
            L_Order.Add(order);
        }
       
        public void Add(LinesList lines)
        {
            L_Lines.Add(lines);
        }
       
        public IEnumerator GetEnumerator()
        {
            if(Properties.Settings.Default.IsOrder)
                return ((IEnumerable)L_Order).GetEnumerator();
            else return ((IEnumerable)L_Lines).GetEnumerator();


        }
        

    }
    class OrderList
    {
        public string LOGICALREF { get; set; }
        public string NUMBER { get; set; }
        public string DATE  { get; set; }
        public string TIME  { get; set; }
        public string AUXIL_CODE { get; set; }
        public string AUTH_CODE { get; set; }
        public string ARP_CODE  { get; set; }
        public string SOURCE_WH  { get; set; }
        public string SOURCE_COST_GRP  { get; set; }
        public string DIVISION { get; set; }
        public string ORDER_STATUS  { get; set; }
        public string CREATED_BY  { get; set; }
        public string DATE_CREATED  { get; set; }
        public string HOUR_CREATED  { get; set; }
        public string MIN_CREATED { get; set; }
        public string SEC_CREATED  { get; set; }
        public string MODIFIED_BY { get; set; }
        public string DATE_MODIFIED  { get; set; }
        public string HOUR_MODIFIED  { get; set; }
        public string MIN_MODIFIED  { get; set; }
        public string SEC_MODIFIED { get; set; }
        public string TRADING_GRP { get; set; }
 } 
    class LinesList
    {
        public string INVOICEREF { get; set; }
        public string TYPE  { get; set; }
        public string MASTER_CODE { get; set; }
        public string QUANTITY { get; set; }
        public string PRICE { get; set; }
        public string VAT_RATE{ get; set; }
        public string UNIT_CODE { get; set; }
        public string DUE_DATE  { get; set; }
        public string SOURCE_WH { get; set; }
        public string SOURCE_COST_GR  { get; set; }
        public string DIVISION   { get; set; }
        public string SALESMAN_CODE  { get; set; }
        public string AFFECT_RISK { get; set; }
        public string EDT_CURR { get; set; }
        public string PRODUCER_CODE { get; set; }
    }

}
