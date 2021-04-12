using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;

namespace AFSC
{
    public partial class StudentForm : System.Windows.Forms.Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse
         );
        private Point lstPoint;
        private Student UserStudent = new Student();

        private Form objLoginForm;
        public StudentForm()
        {
            InitializeComponent();
            //Show name login
            UserStudent.GetInfoStudent();
            NameLbl.Text = UserStudent.GetFullName().ToString();

            //rounded corners frm
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hide others pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 530);
            pnlSchedule.Location = new Point(10, 110);
            pnlSchedule.Visible = true;

            ShowSchedule();

            //Tip about student (group, stCard)
            string txt = "Група: " + UserStudent.GetGroup() + "\nСерія, номер студентського: " + UserStudent.GetStudentCard();
            ToolTip t = new ToolTip();
            t.SetToolTip(NameLbl, txt);
        }
        public StudentForm(Form obj)
        {
            objLoginForm = obj;

            InitializeComponent();
            //Show name login
            UserStudent.GetInfoStudent();
            NameLbl.Text = UserStudent.GetFullName().ToString();

            //rounded corners frm
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hide others pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 530);
            pnlSchedule.Location = new Point(10, 110);
            pnlSchedule.Visible = true;

            ShowSchedule();


            //Tip about student (group, stCard)
            string txt = "Група: " + UserStudent.GetGroup() + "\nСерія, номер студентського: " + UserStudent.GetStudentCard();
            ToolTip t = new ToolTip();
            t.SetToolTip(NameLbl, txt);
        }

        private void MinWinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Dispose();
            objLoginForm.txtLogFields();
            objLoginForm.Show();
        }

        private void StudentsForm_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lstPoint.X;
                this.Top += e.Y - lstPoint.Y;
            }
        }

        private void StudentsForm_MouseDown(object sender, MouseEventArgs e)
        {
            lstPoint = new Point(e.X, e.Y);
        }

        private void btn_Active(Button Btm)
        {
            pnlNav.Height = Btm.Height;
            pnlNav.Top = Btm.Top;
            Btm.BackColor = Color.FromArgb(24, 24, 36);
        }

        private void btn_NotActive(Button btn1, Button btn2, Button btn3, Button btn4)
        {
            btn1.BackColor = Color.FromArgb(2, 5, 12);
            btn2.BackColor = Color.FromArgb(2, 5, 12);
            btn3.BackColor = Color.FromArgb(2, 5, 12);
            btn4.BackColor = Color.FromArgb(2, 5, 12);
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hide others pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 530);
            pnlSchedule.Location = new Point(10, 110);
            pnlSchedule.Visible = true;

            ShowSchedule();
        }

        private void ShowSchedule()
        {
            #region PNL SCHEDULE1
            //draw pnl
            lblScheduleW1.Location = new Point(150, 0);
            dgridShowSheduleW1.Size = new System.Drawing.Size(340, 490);
            dgridShowSheduleW1.Location = new Point(10, 20);

            lblLeft1.Location = new Point(0, 0);
            lblLeft1.Size = new System.Drawing.Size(1, 450);
            lblLeft2.Location = new Point(339, 0);
            lblLeft2.Size = new System.Drawing.Size(1, 450);

            pnlNawSchedule1.Location = new Point(0, 0);
            pnlNawSchedule1.Size = new System.Drawing.Size(400, 1);

            lblMon1.Location = new Point(0, 35);
            pnlNawSchedule2.Location = new Point(0, 90);
            pnlNawSchedule2.Size = new System.Drawing.Size(400, 1);
            string cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek1Mon.Location = new Point(70, 5);
            dgridScheduleWeek1Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Mon.DataSource = UserStudent.GetDataSchedule(cmd);

            lblTue1.Location = new Point(0, 125);
            pnlNawSchedule3.Location = new Point(0, 180);
            pnlNawSchedule3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek1Tue.Location = new Point(70, 95);
            dgridScheduleWeek1Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Tue.DataSource = UserStudent.GetDataSchedule(cmd);

            lblWen1.Location = new Point(0, 215);
            pnlNawSchedule4.Location = new Point(0, 270);
            pnlNawSchedule4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek1Wen.Location = new Point(70, 185);
            dgridScheduleWeek1Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Wen.DataSource = UserStudent.GetDataSchedule(cmd);


            lblThu1.Location = new Point(0, 305);
            pnlNawSchedule5.Location = new Point(0, 360);
            pnlNawSchedule5.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek1Thu.Location = new Point(70, 275);
            dgridScheduleWeek1Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Thu.DataSource = UserStudent.GetDataSchedule(cmd);


            lblFri1.Location = new Point(0, 395);
            pnlNawSchedule6.Location = new Point(0, 450);
            pnlNawSchedule6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek1Fri.Location = new Point(70, 365);
            dgridScheduleWeek1Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Fri.DataSource = UserStudent.GetDataSchedule(cmd);
            #endregion

            #region PNL SCHEDULE2
            //draw pnl1Week
            lblScheduleW2.Location = new Point(490, 0);
            dgridShowSheduleW2.Size = new System.Drawing.Size(340, 490);
            dgridShowSheduleW2.Location = new Point(360, 20);

            pnlNawL.Location = new Point(0, 0);
            pnlNawL.Size = new System.Drawing.Size(1, 450);
            pnlNawR.Location = new Point(339, 0);
            pnlNawR.Size = new System.Drawing.Size(1, 450);

            pnlNaw5.Location = new Point(0, 0);
            pnlNaw5.Size = new System.Drawing.Size(400, 1);

            lblMon2.Location = new Point(0, 35);
            pnlNaw1.Location = new Point(0, 90);
            pnlNaw1.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek2Mon.Location = new Point(70, 5);
            dgridScheduleWeek2Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Mon.DataSource = UserStudent.GetDataSchedule(cmd);

            lblTue2.Location = new Point(0, 125);
            pnlNaw2.Location = new Point(0, 180);
            pnlNaw2.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek2Tue.Location = new Point(70, 95);
            dgridScheduleWeek2Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Tue.DataSource = UserStudent.GetDataSchedule(cmd);

            lblWen2.Location = new Point(0, 215);
            pnlNaw3.Location = new Point(0, 270);
            pnlNaw3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek2Wen.Location = new Point(70, 185);
            dgridScheduleWeek2Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Wen.DataSource = UserStudent.GetDataSchedule(cmd);

            lblThu2.Location = new Point(0, 305);
            pnlNaw4.Location = new Point(0, 360);
            pnlNaw4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek2Thu.Location = new Point(70, 275);
            dgridScheduleWeek2Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Thu.DataSource = UserStudent.GetDataSchedule(cmd);

            lblFri2.Location = new Point(0, 395);
            pnlNaw6.Location = new Point(0, 450);
            pnlNaw6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + UserStudent.GetGroup() + "'";
            dgridScheduleWeek2Fri.Location = new Point(70, 365);
            dgridScheduleWeek2Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Fri.DataSource = UserStudent.GetDataSchedule(cmd);

            #endregion
        }

        private void ReplaceBtm_Click(object sender, EventArgs e)
        {
            //hover om btn
            btn_Active(ReplaceBtm);
            btn_NotActive(ScheduleBtn, StudyPlanBtn, InfoBtn, LogOutBtn);

            pnlStudyPlan.Visible = false;
            pnlSchedule.Visible = false;

            //panel Log, draw
            pnlReplacment.Size = new System.Drawing.Size(690, 450);
            pnlReplacment.Location = new Point(25, 130);
            pnlReplacment.Visible = true;


            //show DT info raplacement
            ShowInfoRp.Size = new System.Drawing.Size(690, 250);
            ShowInfoRp.Location = new Point(20, 50);

            if (UserStudent.GetDataStudentRp().Rows.Count >= 1)
            {
                ShowInfoRp.DataSource = UserStudent.GetDataStudentRp();
            }
            else
            {
                MessageBox.Show("На даний момент заміни відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
        }

        private void StudyPlanBtn_Click(object sender, EventArgs e)
        {
            btn_Active(StudyPlanBtn);
            btn_NotActive(ReplaceBtm, ScheduleBtn, InfoBtn, LogOutBtn);

            pnlReplacment.Visible = false;
            pnlSchedule.Visible = false;

            //panel Log, draw
            pnlStudyPlan.Size = new System.Drawing.Size(690, 450);
            pnlStudyPlan.Location = new Point(20, 130);
            pnlStudyPlan.Visible = true;

            //show DT info raplacement
            ShowIfoPlan.Size = new System.Drawing.Size(550, 350);
            ShowIfoPlan.Location = new Point(80, 80);

            if (UserStudent.GetDataStudentStudyPlan().Rows.Count >= 1)
            {
                ShowIfoPlan.DataSource = UserStudent.GetDataStudentStudyPlan();
            }
            else
            {
                MessageBox.Show("Дані по навчальному плану відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }

        }

        private void InfoBtn_Click(object sender, EventArgs e)
        {
            btn_Active(InfoBtn);
            btn_NotActive(ReplaceBtm, ScheduleBtn, StudyPlanBtn, LogOutBtn);

            Info objInfoForm = new Info();
            objInfoForm.Show();
            
        }
    }
}
