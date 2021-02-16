using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA_TRANSFER
{
    class MethodContext
    {

        private string connectionstring;
        SqlConnection oCnn;
        SqlCommand oCmd;
        public MethodContext(string connectionstring)
        {
            this.connectionstring = connectionstring;
            oCnn = new SqlConnection(connectionstring);
        }

        public SqlConnection ConnectionOpenAndClose(bool open)
        {

            if (oCnn.State == ConnectionState.Closed && open)
            {
                oCnn.Open();
            }
            else if(oCnn.State == ConnectionState.Open && !open)
            {
                oCnn.Close();
            }

            return oCnn;


        }

        public DataTable getTable(string sql)
        {
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa = new SqlDataAdapter(sql, ConnectionOpenAndClose(true));
            oDa.Fill(oDt);
            ConnectionOpenAndClose(false);
            return oDt;
        }

        public DataRow getRow(string sql)
        {
            DataRow oDr = getTable(sql).Rows[0];
            return oDr;
        }

        public SqlDataReader getData(string sql)
        {
            oCmd = get_command(sql);
            SqlDataReader oRdr = oCmd.ExecuteReader();
            //ConnectionOpenAndClose();
            return oRdr;
        }
        public String getScalar(string sql)
        {
            oCmd = get_command(sql);
            var read = oCmd.ExecuteScalar();
            //ConnectionOpenAndClose();
            return read.ToString();
        }
        public int Handle(string sql)
        {
            oCmd = get_command(sql);
           return oCmd.ExecuteNonQuery();
            
        }
        public void Handle(string sql,DataTable HandleTable)
        {
            SqlCommand oCmd = get_command(sql);

            for (int i = 0; i < HandleTable.Rows.Count; i++)
            {
               oCmd.Parameters.AddWithValue("@"+ HandleTable.Rows[i][0].ToString() + "", HandleTable.Rows[i][1].ToString());
                string x = "@" + HandleTable.Rows[i][0].ToString() + "";
                string y = HandleTable.Rows[i][1].ToString();
            }
            
            oCmd.ExecuteNonQuery();
          

        }
        public SqlCommand get_command(string sql)
        {
            SqlCommand oCmd = new SqlCommand(sql, ConnectionOpenAndClose(true));
           

            return oCmd;
        }

        public DataRow GetRow(DataTable dt,string parameter,string value)
        {
            DataRow Row = dt.NewRow();
            Row["parameter"] = parameter;
            Row["value"] = value;

            return Row;
        }
    }
}
