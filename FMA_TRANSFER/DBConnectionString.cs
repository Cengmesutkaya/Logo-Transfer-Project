
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FMA_TRANSFER
{
    class DBConnectionString
    {
        private string connectionString;
        public string UnityDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dB_ConnectionString_UNITYDB"].ConnectionString;
            return connectionString;
        }
        public string FMADB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dB_ConnectionString_FMADB"].ConnectionString;
            return connectionString;
        }
        public string IntranetDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["dB_ConnectionString_INTRANET"].ConnectionString;
            return connectionString;
        }


    }
}
