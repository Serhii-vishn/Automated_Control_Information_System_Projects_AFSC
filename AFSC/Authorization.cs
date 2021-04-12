using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace AFSC
{
    class Authorization
    {
        private
          string Password, Accesslvl;
        private static string Login;
        public Authorization(string log, string pass)
        {
            Login = log;
            Password = pass;
        }
        ~Authorization(){}

        public string Autorithation()
        {
            string cmdLine = "SELECT *FROM UsersDat WHERE Login = '"+ Login+"' AND Password = '"+ Password+"'";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(cmdLine, DB.GetSqlConn());
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Accesslvl = reader.GetValue(3).ToString();
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }              
            }
            return Accesslvl;
        }

        public void LogRegicter(string log)
        {
            string cmdLine = "INSERT INTO HistoryLogs ([UserLogin], [Log/Logout], [Date, time]) VALUES ('"
                                        + Login + "', '" + log + "', SYSDATETIMEOFFSET())";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(cmdLine, DB.GetSqlConn());
                try
                {
                    connection.Open();
                    int number = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, 
                            "Помилка вводу даних про вхід/вихід", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
             
            }
        }

        public static string GetLog()
        {
            return Login;
        }
    }
}
