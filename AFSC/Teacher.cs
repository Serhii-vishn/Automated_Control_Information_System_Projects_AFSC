using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

namespace AFSC
{
    class Teacher
    {
        private string ID;
        private string FullName, Position, TimeOfJob;

        ~Teacher(){}

        public void GetInfoTeacher()
        {
            string cmdLine = "SELECT *FROM TeachersDat WHERE Login = '" + Authorization.GetLog() + "'";
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
                            TimeOfJob = reader.GetValue(3).ToString();
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
        public string GetTimeOfJob()
        {
            return TimeOfJob;
        }

        public DataTable GetDataTeacherRp(string txt)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                string cmdLine = "SELECT *FROM ReplacemmentsDat";
                if (txt == "IsRp")
                {
                    cmdLine = "SELECT Rp.[Date] as [Дата], Rp.[Group] as [Група], Rp.[LessNum] as [Номер пари], " +
                                "Tch.[Full name] as [Заміняється], Tch2.[Full name] as [Заміняє],  LoadTch.[Subject] as [Предмет]" +
                                    " FROM ReplacemmentsDat Rp " +
                                    " JOIN TeachersLoadDat LoadTch on Rp.[IDSubWillRp] = LoadTch.[ID]" +
                                    " JOIN TeachersDat Tch on Rp.[IDSubIsRp] = Tch.[ID]" +
                                    " JOIN TeachersDat Tch2 on LoadTch.[IdTeacher] = Tch2.[ID]" +
                                        " WHERE [IDSubIsRp] = '" + ID + "'";
                }
                else if (txt == "WillRp")
                {
                    cmdLine = "SELECT Rp.[Date] as [Дата], Rp.[Group] as [Група], Rp.[LessNum] as [Номер пари], " +
                                " Tch.[Full name] as [Заміняється], Tch2.[Full name] as [Заміняє],  LoadTch.[Subject] as [Предмет]" +
                                    " FROM ReplacemmentsDat Rp" +
                                    " JOIN TeachersLoadDat LoadTch on Rp.[IDSubWillRp] = LoadTch.[ID]" +
                                    " JOIN TeachersDat Tch on Rp.[IDSubIsRp] = Tch.[ID]" +
                                    " JOIN TeachersDat Tch2 on LoadTch.[IdTeacher] = Tch2.[ID]" +
                                    " WHERE [IDSubWillRp] = '" + ID + "'";
                }              
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Close();
                return dtbl;
            }
        }

        public DataTable GetDataTch(string cmdLine)
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                     if (dtbl.Rows.Count <= 0)
                     {
                        MessageBox.Show("На даний момент дані відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
                     }
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

        public DataTable GetDataScheduleTch(string cmd)
        {
            DataTable dtbl = new DataTable();
            //string cmdLine = "SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE ([Week]= 2 AND [TchLess2] = '" + ID + "') OR [TchLess2] IS NULL";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmd, connection);
                    sqlDa.Fill(dtbl);

                    if (dtbl.Rows.Count <= 0)
                    {
                        MessageBox.Show("На даний момент дані відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
                    }
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

        public void InsertTimeManagment(string cmdLine)
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
         
        public bool CheckAvalTimeMan()
        {
            bool check = false; //if tbls whith time manage clear WHERE [ID] User check = false, else true
            DataTable dtbl = new DataTable();
            string cmdLine = "SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + ID + "'" +
                " SELECT *FROM TecahersTimeManageDatTue WHERE [IDTch] = '" + ID + "'" +
                " SELECT *FROM TecahersTimeManageDatWed WHERE [IDTch] = '" + ID + "'" +
                " SELECT *FROM TecahersTimeManageDatThu WHERE [IDTch] = '" + ID + "'" +
                " SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + ID + "'" +
                " SELECT *FROM TecahersTimeManageDatFri WHERE [IDTch] = '" + ID + "'";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                    if(dtbl.Rows.Count > 0)
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


    }
}