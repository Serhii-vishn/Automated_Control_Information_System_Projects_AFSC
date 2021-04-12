using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AFSC
{
    class DB
    {
        private static SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString);


        public bool OpenConnection()
        {   
            if (sqlConn.State != ConnectionState.Open)
            {
                sqlConn.Open();
                return true;
            }
            return false;
        }

        public bool CloseConnection()
        {
            if (sqlConn.State != ConnectionState.Closed)
            {
                sqlConn.Close();
                return true;
            }
            return false;
        }

        public static SqlConnection GetSqlConn()
        {
            return sqlConn;
        }
        public static string GetConnToString()
        {
            return sqlConn.ToString();
        }
    }
}
