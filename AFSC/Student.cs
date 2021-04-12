using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

namespace AFSC
{
    class Student
    {
        private
            string FullName, Group, StudentCard;

        ~Student (){}

        public void GetInfoStudent()
        {
            string cmdLine = "SELECT *FROM StudentsDat WHERE Login = '" + Authorization.GetLog() + "'";
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
                            FullName = reader.GetValue(1).ToString();
                            Group = reader.GetValue(2).ToString();
                            StudentCard = reader.GetValue(3).ToString();
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Close();
            }
        }

        public DataTable GetDataStudentRp()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                string cmdLine = "SELECT Rp.[Date] as [Дата], Rp.[Group] as [Група], Rp.[LessNum] as [Номер пари],  " +
                    "Tch.[Full name] as [Заміняється], Tch2.[Full name] as [Заміняє],  LoadTch.[Subject] as [Предмет] " +
                    "FROM ReplacemmentsDat Rp " +
                        "JOIN TeachersLoadDat LoadTch on Rp.[IDSubWillRp] = LoadTch.[ID] " +
                        "JOIN TeachersDat Tch on Rp.[IDSubIsRp] = Tch.[ID] " +
                        "JOIN TeachersDat Tch2 on LoadTch.[IdTeacher] = Tch2.[ID] " +
                        "WHERE [Group] = '" + Group + "'";
                try
                {
                    connection.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                    if (dtbl.Rows.Count <= 0)
                    {
                        MessageBox.Show("На даний момент, заміни відсутні у групі " + Group + " відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
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

        public DataTable GetDataStudentStudyPlan()
        {
            DataTable dtbl = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ScheduleDB_Connection"].ConnectionString))
            {
                string cmdLine = "SELECT [Name] as [Назва], [Group] as [Група], [Semester] as [Семестр], [DateStart] as [Дата початку], [DateEnd] as [Дата кінця] FROM CurriculumDat WHERE [Group] = '" + Group + "' ORDER BY [Semester]";
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDa = new SqlDataAdapter(cmdLine, connection);
                    sqlDa.Fill(dtbl);

                    if (dtbl.Rows.Count <=0)
                    {
                        MessageBox.Show("Дані по навчальному плану відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
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

        public DataTable GetDataSchedule(string cmd)
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



        public string GetGroup()
        {
            return Group;
        }

        public string GetFullName()
        {
            return FullName;
        }

        public string GetStudentCard()
        {
            return StudentCard;
        }
    }
}
