using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AFSC
{
    class Employees
    {
        private string ID;
        private string FullName, Position;

        ~Employees() {}

        public void GetInfoEmployee()
        {
            string cmdLine = "SELECT *FROM EmployeeDat WHERE Login = '" + Authorization.GetLog() + "'";
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
                            ID = reader.GetValue(0).ToString();
                            FullName = reader.GetValue(1).ToString();
                            Position = reader.GetValue(2).ToString();
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
        }

        public string GetID()
        {
            return ID;
        }
        public string GetFullName()
        {
            return FullName;
        }
        public string GetPosition()
        {
            return Position;
        }

        public DataTable GetData(string cmdLine)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dtbl;
        }


        public string GetDataTeach(string cmdLine)
        {
            string idTeach = "";
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
                            idTeach = reader.GetValue(0).ToString();
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
            return idTeach;
        }

        public bool CheckAvalDat(string cmdLine)
        {
            bool check = false; //if tbls whith time manage clear WHERE [ID] User check = false, else true
            DataTable dtbl = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                    if (dtbl.Rows.Count > 0)
                    {
                        check = true;
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }


        public void EmployeeInsertDat(string cmdLine)
        {
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
                            "Помилка вводу даних", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
