using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AFSC
{
    public partial class TeacherForm : System.Windows.Forms.Form
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
        private Teacher UserTeacher = new Teacher();
        private Form objLoginForm;

        
        public TeacherForm(Form obj)
        {
            InitializeComponent();
            objLoginForm = obj;
            //rounded corners frm
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            UserTeacher.GetInfoTeacher();
            NameLbl.Text = UserTeacher.GetFullName().ToString();
            //frst button hover
            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hideothers pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 530);
            pnlSchedule.Location = new Point(10, 110);
            pnlSchedule.Visible = true;

            ShowSchedule();

            //Tip about teacher
            string txt = "Посада: " + UserTeacher.GetPosition() + "\nЗайнятість: " + UserTeacher.GetTimeOfJob();
            ToolTip t = new ToolTip();
            t.SetToolTip(NameLbl, txt);
        }


        private void TeachersForm_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lstPoint.X;
                this.Top += e.Y - lstPoint.Y;
            }
        }

        private void TeachersForm_MouseDown(object sender, MouseEventArgs e)
        {
            lstPoint = new Point(e.X, e.Y);
        }

        private void btn_Active(Button Btm)
        {
            pnlNav.Height = Btm.Height;
            pnlNav.Top = Btm.Top;
            Btm.BackColor = Color.FromArgb(24, 24, 36);
        }

        private void btn_NotActive(Button btn1, Button btn2, Button btn3, Button btn4, Button btn5)
        {
            btn1.BackColor = Color.FromArgb(2, 5, 12);
            btn2.BackColor = Color.FromArgb(2, 5, 12);
            btn3.BackColor = Color.FromArgb(2, 5, 12);
            btn4.BackColor = Color.FromArgb(2, 5, 12);
            btn5.BackColor = Color.FromArgb(2, 5, 12);
        }

        private void MinWinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseWinBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Dispose();

            objLoginForm.txtLogFields();
            objLoginForm.Show();
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hide others pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 530);
            pnlSchedule.Location = new Point(10, 110);
            pnlSchedule.Visible = true;

            ShowSchedule();
        }

        private void ShowSchedule ()
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

            string cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek1Mon.Location = new Point(70, 5);
            dgridScheduleWeek1Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Mon.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblTue1.Location = new Point(0, 125);
            pnlNawSchedule3.Location = new Point(0, 180);
            pnlNawSchedule3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek1Tue.Location = new Point(70, 95);
            dgridScheduleWeek1Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Tue.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblWen1.Location = new Point(0, 215);
            pnlNawSchedule4.Location = new Point(0, 270);
            pnlNawSchedule4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek1Wen.Location = new Point(70, 185);
            dgridScheduleWeek1Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Wen.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblThu1.Location = new Point(0, 305);
            pnlNawSchedule5.Location = new Point(0, 360);
            pnlNawSchedule5.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                   " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                   " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                   " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                   " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek1Thu.Location = new Point(70, 275);
            dgridScheduleWeek1Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Thu.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblFri1.Location = new Point(0, 395);
            pnlNawSchedule6.Location = new Point(0, 450);
            pnlNawSchedule6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                             " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                             " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                             " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                             " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek1Fri.Location = new Point(70, 365);
            dgridScheduleWeek1Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Fri.DataSource = UserTeacher.GetDataScheduleTch(cmd);
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

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek2Mon.Location = new Point(70, 5);
            dgridScheduleWeek2Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Mon.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblTue2.Location = new Point(0, 125);
            pnlNaw2.Location = new Point(0, 180);
            pnlNaw2.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                      " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek2Tue.Location = new Point(70, 95);
            dgridScheduleWeek2Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Tue.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblWen2.Location = new Point(0, 215);
            pnlNaw3.Location = new Point(0, 270);
            pnlNaw3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                    " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek2Wen.Location = new Point(70, 185);
            dgridScheduleWeek2Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Wen.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblThu2.Location = new Point(0, 305);
            pnlNaw4.Location = new Point(0, 360);
            pnlNaw4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                       " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                       " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                       " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                       " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek2Thu.Location = new Point(70, 275);
            dgridScheduleWeek2Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Thu.DataSource = UserTeacher.GetDataScheduleTch(cmd);

            lblFri2.Location = new Point(0, 395);
            pnlNaw6.Location = new Point(0, 450);
            pnlNaw6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [Group] as [Група], [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [TchLess1] = '" + UserTeacher.GetID() + "' UNION" +
                 " SELECT [№] = 2, [Group] as [Група], [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [TchLess2] = '" + UserTeacher.GetID() + "' UNION" +
                 " SELECT [№] = 3, [Group] as [Група], [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [TchLess3] = '" + UserTeacher.GetID() + "' UNION" +
                 " SELECT [№] = 4, [Group] as [Група], [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [TchLess4] = '" + UserTeacher.GetID() + "' UNION" +
                 " SELECT [№] = 5, [Group] as [Група], [5_lesson] as [Предмет], [Lecture/practice5] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [TchLess5] = '" + UserTeacher.GetID() + "'";
            dgridScheduleWeek2Fri.Location = new Point(70, 365);
            dgridScheduleWeek2Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Fri.DataSource = UserTeacher.GetDataScheduleTch(cmd);
            #endregion
        }

        private void ReplaceBtm_Click(object sender, EventArgs e)
        {
            //hover on btn
            btn_Active(ReplaceBtm);
            btn_NotActive(TimeManagBtn, ScheduleBtn, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hide other panels
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlSchedule.Visible = false;
            pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            //panel Log, draw
            pnlReplacment.Size = new System.Drawing.Size(690, 450);
            pnlReplacment.Location = new Point(20, 130);
            pnlReplacment.Visible = true;

            ShowRp2.Location = new Point(40, 210);
            ShowRp2.Size = new System.Drawing.Size(170, 30);

            ShowInfoRp.Size = new System.Drawing.Size(690, 150);
            ShowInfoRp.Location = new Point(10, 50);

            //show DT info raplacement
            ShowRp1.Location = new Point(40, 10);
            ShowRp1.Size = new System.Drawing.Size(170, 30);

            ShowInfoRp2.Size = new System.Drawing.Size(690, 150);
            ShowInfoRp2.Location = new Point(10, 250);
        }

        private void StudyPlanBtn_Click(object sender, EventArgs e)
        {
            //hover on btn
            btn_Active(StudyPlanBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, ScheduleBtn, InfoBtn, LogOutBtn);

            //hide other panels and btns
            pnlReplacment.Visible = false;
            pnlSchedule.Visible = false;
            pnlTimeManage.Visible = false;
            pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            //panel log, draw
            pnlStudyPlan.Size = new System.Drawing.Size(700, 550);
            pnlStudyPlan.Location = new Point(15, 40);
            pnlStudyPlan.Visible = true;

            //ListBox log, draw
            NameListBox.Size = new System.Drawing.Size(190, 160);
            NameListBox.Location = new Point(10, 30);

            CourseListBox.Size = new System.Drawing.Size(105, 160);
            CourseListBox.Location = new Point(180, 30);

            SemestrListBox.Size = new System.Drawing.Size(150, 70);
            SemestrListBox.Location = new Point(285, 30);

            FindDataBtn.Size = new System.Drawing.Size(80, 30);
            FindDataBtn.Location = new Point(285, 80);

            //grid draw
            ShowInfoStudyPlan.Size = new System.Drawing.Size(500, 350);
            ShowInfoStudyPlan.Location = new Point(100, 180);
        }

        private void ShowRp1_Click(object sender, EventArgs e)
        {
            if (UserTeacher.GetDataTeacherRp("IsRp").Rows.Count >= 1)
            {
                ShowInfoRp.DataSource = UserTeacher.GetDataTeacherRp("IsRp");
            }
            else
            {
                MessageBox.Show("На даний момент заміни відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
        }

        private void ShowRp2_Click(object sender, EventArgs e)
        {
            if (UserTeacher.GetDataTeacherRp("WillRp").Rows.Count >= 1)
            {
                ShowInfoRp2.DataSource = UserTeacher.GetDataTeacherRp("WillRp");
            }
            else
            {
                MessageBox.Show("На даний момент заміни відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
        }

        private void FindDatabtn_Click(object sender, EventArgs e)
        {
            string Fcmd = "SELECT [Name] as [Етап навчання], [Group] as [Група], [Semester] as [Семестр], [DateStart] as [Початок], [DateEnd] as [Кінець] FROM CurriculumDat ";

            if (NameListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "WHERE (";
                for (int i = 0; i < NameListBox.CheckedIndices.Count; i++)
                {
                    Fcmd += " [Name] = '" + NameListBox.CheckedItems[i] + "' OR  ";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 4);
                Fcmd += ")";
            }
            else if (CourseListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "WHERE (";
                for (int i = 0; i < CourseListBox.CheckedIndices.Count; i++)
                {
                    int ind = CourseListBox.CheckedIndices[i] + 1;
                    Fcmd += " [Course] = '" + ind + "' OR";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 2);
                Fcmd += ")";
            }
            else if (SemestrListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "WHERE (";
                for (int i = 0; i < SemestrListBox.CheckedIndices.Count; i++)
                {
                    int ind = SemestrListBox.CheckedIndices[i] + 1;
                    Fcmd += " [Semester] = '" + ind + "' OR";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 2);
                Fcmd += ")";
            }
            
            if (CourseListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "AND (";
                for (int i = 0; i < CourseListBox.CheckedIndices.Count; i++)
                {
                    int ind = CourseListBox.CheckedIndices[i] + 1;
                    Fcmd += " [Course] = '" + ind + "' OR  ";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 4);
                Fcmd += ")";
            }
            else if (SemestrListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "AND (";
                for (int i = 0; i < SemestrListBox.CheckedIndices.Count; i++)
                {
                    int ind = SemestrListBox.CheckedIndices[i] + 1;
                    Fcmd += " [Semester] = '" + ind + "' OR  ";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 5);
                Fcmd += ")";
            }

            if (SemestrListBox.CheckedIndices.Count > 0)
            {
                Fcmd += "AND (";
                for (int i = 0; i < SemestrListBox.CheckedIndices.Count; i++)
                {
                    int ind = SemestrListBox.CheckedIndices[i] + 1;
                    Fcmd += " [Semester] = '" + ind + "' OR  ";
                }
                Fcmd = Fcmd.Substring(0, Fcmd.Length - 5);
                Fcmd += ")";
            }
            ShowInfoStudyPlan.DataSource = UserTeacher.GetDataTch(Fcmd);
        }

        private void TimeManagbtn_MouseEnter(object sender, EventArgs e)
        {
            //hover btn
            btn_Active(TimeManagBtn);
            btn_NotActive(InfoBtn, ReplaceBtm, ScheduleBtn, StudyPlanBtn, LogOutBtn);

            btnShowTimeM.Visible = true;
            btnAddTimeM.Visible = true;
        }

        private void InfoBtn_Click(object sender, EventArgs e)
        {
            btn_Active(InfoBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, ScheduleBtn, StudyPlanBtn, LogOutBtn);

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            Info objInfoForm = new Info();
            objInfoForm.Show();
        }

        private void BntCheckedFalse(object sender, EventArgs e)
        {
            FindDataBtn.Checked = false;
        }

        private void pnl1Week_Click(object sender, EventArgs e)
        {
            pnlWeek1.Visible = true;
            pnlWeek2.Visible = false;


        }

        private void btnWeek2_Click(object sender, EventArgs e)
        {
            pnlWeek1.Visible = false;
            pnlWeek2.Visible = true;
        }

        private void btnShowTimeM_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;

            if (UserTeacher.CheckAvalTimeMan() != true)
            {
                DialogResult result = MessageBox.Show("Дані відсутні, будь ласка заповніть",
                        "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                ShowDataTimeManage();
            }
        }

        private void ShowDataTimeManage()
        {
            pnlShowTimeMan.Visible = true;
            //draw pnl
            pnlShowTimeMan.Size = new System.Drawing.Size(690, 550);
            pnlShowTimeMan.Location = new Point(10, 100);

            //draw pnl1Week
            pnlShowDatManWeek1.Size = new System.Drawing.Size(690, 330);
            pnlShowDatManWeek1.Location = new Point(0, 80);

            string cmdLine = "SELECT [Week] as [Тиждень]," +
                " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                "FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + UserTeacher.GetID() + "';";

            datGrMon.DataSource = UserTeacher.GetDataTch(cmdLine);

            datGrMon.Location = new Point(85, 3);
            datGrMon.Size = new System.Drawing.Size(610, 80);
            lblShowMon.Location = new Point(5, 35);

            cmdLine = "SELECT [Week] as [Тиждень]," +
                " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                "FROM TecahersTimeManageDatTue WHERE [IDTch] = '" + UserTeacher.GetID() + "';";

            datGrTue.DataSource = UserTeacher.GetDataTch(cmdLine);

            datGrTue.Location = new Point(85, 82);
            datGrTue.Size = new System.Drawing.Size(610, 80);
            lblShowTue.Location = new Point(8, 85);

            cmdLine = "SELECT [Week] as [Тиждень]," +
                " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                "FROM TecahersTimeManageDatWed WHERE [IDTch] = '" + UserTeacher.GetID() + "';";

            datGrWen.DataSource = UserTeacher.GetDataTch(cmdLine);

            datGrWen.Location = new Point(85, 132);
            datGrWen.Size = new System.Drawing.Size(610, 80);
            lblShowWen.Location = new Point(14, 135);

            cmdLine = "SELECT [Week] as [Тиждень]," +
                " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                "FROM TecahersTimeManageDatThu WHERE [IDTch] = '" + UserTeacher.GetID() + "';";

            datGrThu.DataSource = UserTeacher.GetDataTch(cmdLine);

            datGrThu.Location = new Point(85, 182);
            datGrThu.Size = new System.Drawing.Size(610, 80);
            lblShowThu.Location = new Point(14, 185);

            cmdLine = "SELECT [Week] as [Тиждень]," +
                " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                "FROM TecahersTimeManageDatFri WHERE [IDTch] = '" + UserTeacher.GetID() + "';";

            datGrFri.DataSource = UserTeacher.GetDataTch(cmdLine);

            datGrFri.Location = new Point(85, 232);
            datGrFri.Size = new System.Drawing.Size(610, 80);
            lblShowFri.Location = new Point(8, 235);
        }

        private void btnAddTimeM_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlShowTimeMan.Visible = false;

            //draw pnl
            pnlTimeManage.Size = new System.Drawing.Size(690, 500);
            pnlTimeManage.Location = new Point(20, 95);
            pnlTimeManage.Visible = true;

            //draw pnl1Week
            lblWeek1.Location = new Point(110, 5);
            pnlWeek1.Size = new System.Drawing.Size(280, 480);
            pnlWeek1.Location = new Point(25, 25);

            //draw pnl2Week
            lblWeek2.Location = new Point(390, 5);
            pnlWeek2.Size = new System.Drawing.Size(280, 480);
            pnlWeek2.Location = new Point(305, 25);

            //draw btn
            btnSaveTimeM.Size = new System.Drawing.Size(90, 30);
            btnSaveTimeM.Location = new Point(590, 70);
            //show info
            pnlWeek1.Visible = true;
            pnlWeek2.Visible = true;
        }

        private void btnSaveTimeM_Click(object sender, EventArgs e)
        {
            if (UserTeacher.CheckAvalTimeMan() == true)
            {
                DialogResult result = MessageBox.Show(
                        "Дані вже введені. Бажаєте їх перезаписати нитисніть 'Да'",
                        "Повідомлення AFSC",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                    UpdateTimeManage();
            }
            else
            {
                InsertTimeMange();
            }
        }

        private void InsertTimeMange()
        {
            string cmdInsertTimeManage = "INSERT INTO TecahersTimeManageDatMon Values (" + UserTeacher.GetID() + ", 1, ";
            //Insert into Monday dtbl
            #region INSERT INTO MON
            if (Check1WeekMon1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekMon2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekMon3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekMon4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekMon5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatMon Values (" + UserTeacher.GetID() + ", 2, ";

            if (Check2WeekMon1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekMon2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekMon3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekMon4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekMon5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";
            #endregion

            //Insert into Tuesday dtbl
            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatTue Values (" + UserTeacher.GetID() + ", 1, ";
            #region INSERT INTO TUE
            if (Check1WeekTue1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekTue2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekTue3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekTue4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekTue5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatTue Values (" + UserTeacher.GetID() + ", 2, ";

            if (Check2WeekTue1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekTue2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekTue3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekTue4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekTue5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            #endregion

            //Insert into Wendsday dtbl
            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatWed Values (" + UserTeacher.GetID() + ", 1, ";
            #region INSER INTO WEN
            if (Check1WeekWen1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekWen2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekWen3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekWen4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekWen5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatWed Values (" + UserTeacher.GetID() + ", 2, ";

            if (Check2WeekWen1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekWen2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekWen3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekWen4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekWen5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            #endregion

            //Insert into Thuesday dtbl
            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatThu Values (" + UserTeacher.GetID() + ", 1, ";
            #region INSERT INTO THU
            if (Check1WeekThu1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekThu2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekThu3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekThu4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekThu5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatThu Values (" + UserTeacher.GetID() + ", 2, ";

            if (Check2WeekThu1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekThu2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekThu3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekThu4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekThu5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            #endregion

            //Insert into Friday dtbl
            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatFri Values (" + UserTeacher.GetID() + ", 1, ";
            #region INSERT INTO FRI
            if (Check1WeekFri1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekFri2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekFri3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekFri4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check1WeekFri5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatFri Values (" + UserTeacher.GetID() + ", 2, ";

            if (Check2WeekFri1L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekFri2L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekFri3L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekFri4L.Checked == true)
                cmdInsertTimeManage += "1, ";
            else
                cmdInsertTimeManage += "0, ";

            if (Check2WeekFri5L.Checked == true)
                cmdInsertTimeManage += "1);";
            else
                cmdInsertTimeManage += "0);";

            #endregion
            UserTeacher.InsertTimeManagment(cmdInsertTimeManage);
        }

        private void UpdateTimeManage()
        {
            string cmdInsertTimeManage = "UPDATE TecahersTimeManageDatMon SET ";
            //Update into Monday dtbl
            #region UPDATE MON
            if (Check1WeekMon1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check1WeekMon2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check1WeekMon3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check1WeekMon4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check1WeekMon5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";

            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatMon SET ";

            if (Check2WeekMon1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check2WeekMon2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check2WeekMon3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check2WeekMon4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check2WeekMon5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";

            #endregion

            //Update into Tuesday dtbl
            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatTue SET ";
            #region UPDATE TUE
            if (Check1WeekTue1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check1WeekTue2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check1WeekTue3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check1WeekTue4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check1WeekTue5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";


            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatTue SET ";

            if (Check2WeekTue1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check2WeekTue2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check2WeekTue3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check2WeekTue4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check2WeekTue5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            #endregion

            //Update into Wendsday dtbl
            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatWed SET ";
            #region INSER INTO WEN
            if (Check1WeekWen1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check1WeekWen2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check1WeekWen3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check1WeekWen4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check1WeekWen5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";

            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatWed SET ";

            if (Check2WeekWen1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check2WeekWen2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check2WeekWen3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check2WeekWen4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check2WeekWen5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            #endregion

            //Update into Thuesday dtbl
            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatThu SET ";
            #region UPDATE THU
            if (Check1WeekThu1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check1WeekThu2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check1WeekThu3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check1WeekThu4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check1WeekThu5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";

            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatThu SET ";

            if (Check2WeekThu1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check2WeekThu2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check2WeekThu3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check2WeekThu4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check2WeekThu5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            #endregion

            //Insert into Friday dtbl
            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatFri SET ";
            #region UPDATE FRI
            if (Check1WeekFri1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check1WeekFri2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check1WeekFri3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check1WeekFri4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check1WeekFri5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + UserTeacher.GetID() + "';";

            cmdInsertTimeManage += " UPDATE TecahersTimeManageDatFri SET ";

            if (Check2WeekFri1L.Checked == true)
                cmdInsertTimeManage += "[1_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[1_lesson] = 0, ";

            if (Check2WeekFri2L.Checked == true)
                cmdInsertTimeManage += "[2_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[2_lesson] = 0, ";

            if (Check2WeekFri3L.Checked == true)
                cmdInsertTimeManage += "[3_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[3_lesson] = 0, ";

            if (Check2WeekFri4L.Checked == true)
                cmdInsertTimeManage += "[4_lesson] = 1, ";
            else
                cmdInsertTimeManage += "[4_lesson] = 0, ";

            if (Check2WeekFri5L.Checked == true)
                cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            else
                cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + UserTeacher.GetID() + "';";
            #endregion

            UserTeacher.InsertTimeManagment(cmdInsertTimeManage);
        }

        private void dgridScheduleWeek1Fri_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.Value != null)
            {
                int val;
                if (int.TryParse(e.Value.ToString(), out val) && val < 0)
                {
                    e.Value = "N/A";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}