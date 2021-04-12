using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSC
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            DB Schedule = new DB();
            if(Schedule.OpenConnection()==true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form());
            }
            else
            {
                MessageBox.Show("Не знайдено файл бази даних", "Помилка підключення БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
            }
            Schedule.CloseConnection();
        }
    }
}
