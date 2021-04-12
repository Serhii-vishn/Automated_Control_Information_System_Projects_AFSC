using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSC
{
    public partial class EmployeePD : System.Windows.Forms.Form
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
        // private EmployeePD UserEmployeerPD = new EmployeePD();
        private Employees UserEmployeePD = new Employees();

        private Form objLoginForm;

        public EmployeePD(Form frm)
        {
            InitializeComponent();
            objLoginForm = frm;
            //rounded corners frm
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            UserEmployeePD.GetInfoEmployee();
            NameLbl.Text = UserEmployeePD.GetFullName().ToString();

            ScheduleBtn.Location = new Point(3, 160);

            //frst button hover
            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hideothers pnl
            pnlShowTimeMan.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlAddRp.Visible = false;
            pnlSchedule.Visible = false;
            pnlAddSchedule.Visible = false;


            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;
            btnShowRp.Visible = false;
            btnAddRp.Visible = false;
            btnShowStudyPlan.Visible = false;
            btnAddStudyPlan.Visible = false;

            btnShowSchedule.Size = new System.Drawing.Size(230, 30);
            btnShowSchedule.Location = new Point(20, 200);
            btnAddSchedule.Size = new System.Drawing.Size(230, 30);
            btnAddSchedule.Location = new Point(20, 230);

            btnShowSchedule.Visible = true;
            btnAddSchedule.Visible = true;

            ReplaceBtm.Location = new Point(3, 260);
            StudyPlanBtn.Location = new Point(3, 320);
            TimeManagBtn.Location = new Point(3, 380);

            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 550);
            pnlStudyPlan.Location = new Point(15, 40);
            pnlSchedule.Visible = true;

            comboBoxGroupSch.Size = new System.Drawing.Size(110, 30);
            comboBoxGroupSch.Location = new Point(10, 10);
            comboBoxGroupSch.Items.Clear();

            string cmd = "SELECT [Group] FROM GroupsDat";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    comboBoxGroupSch.Items.Add(dr["Group"].ToString());
                }
            }
            btnSchowScheduleInPnl.Size = new System.Drawing.Size(80, 30);
            btnSchowScheduleInPnl.Location = new Point(130, 10);

            dgridShowSheduleW1.Visible = false;
            dgridShowSheduleW2.Visible = false;
            lblScheduleW1.Visible = false;
            lblScheduleW2.Visible = false;

            //Tip about teacher
            string txt = "Посада: " + UserEmployeePD.GetPosition();
            ToolTip t = new ToolTip();
            t.SetToolTip(NameLbl, txt);
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

        private void CloseWinBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void LogOutBtn_Click(object sender, EventArgs e)
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Dispose();

            objLoginForm.txtLogFields();
            objLoginForm.Show();
        }

        private void MinWinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnShowTimeM_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlAddRp.Visible = false;
            pnlAddSchedule.Visible = false;

            //draw pnl
            pnlShowTimeMan.Size = new System.Drawing.Size(700, 550);
            pnlShowTimeMan.Location = new Point(10, 40);
            pnlShowTimeMan.Visible = true;

            cBoxTeachersNames.Size = new System.Drawing.Size(150, 37);
            cBoxTeachersNames.Location = new Point(10, 10);
            cBoxTeachersNames.Items.Clear();

            bntFindTeacher.Size = new System.Drawing.Size(80, 25);
            bntFindTeacher.Location = new Point(170, 10);

            pnlShowDatManWeek1.Visible = false;

            string cmd = "SELECT Tch2.[Full name] FROM TeachersLoadDat LoadTch" +
                            " JOIN TeachersDat Tch2 ON LoadTch.[IdTeacher] = Tch2.[ID]" +
                            " GROUP BY[Full name]";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    cBoxTeachersNames.Items.Add(dr["Full name"].ToString());
                }
            }
        }

        private void bntFindTeacherTimeMan_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + cBoxTeachersNames.Text.ToString() + "'";
            string TeachId = UserEmployeePD.GetDataTeach(cmd);

            string CheckCmd = "SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatTue WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatWed WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatThu WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatFri WHERE [IDTch] = '" + TeachId + "'";

            if (UserEmployeePD.CheckAvalDat(CheckCmd) != true)
            {
                DialogResult result = MessageBox.Show("Дані відсутні, будь ласка спочатку заповніть",
                                                            "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                //draw pnl1Week
                pnlShowDatManWeek1.Size = new System.Drawing.Size(690, 450);
                pnlShowDatManWeek1.Location = new Point(0, 150);
                pnlShowDatManWeek1.Visible = true;

                string cmdLine = "SELECT [Week] as [Тиждень]," +
                    " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                    " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                    " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                    " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                    " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                    "FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + TeachId + "';";
                datGrMon.DataSource = UserEmployeePD.GetData(cmdLine);

                datGrMon.Location = new Point(85, 3);
                datGrMon.Size = new System.Drawing.Size(610, 80);
                lblShowMon.Location = new Point(5, 35);

                cmdLine = "SELECT [Week] as [Тиждень]," +
                    " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                    " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                    " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                    " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                    " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                    "FROM TecahersTimeManageDatTue WHERE [IDTch] = '" + TeachId + "';";
                datGrTue.DataSource = UserEmployeePD.GetData(cmdLine);

                datGrTue.Location = new Point(85, 82);
                datGrTue.Size = new System.Drawing.Size(610, 80);
                lblShowTue.Location = new Point(8, 85);

                cmdLine = "SELECT [Week] as [Тиждень]," +
                    " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                    " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                    " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                    " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                    " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                    "FROM TecahersTimeManageDatWed WHERE [IDTch] = '" + TeachId + "';";
                datGrWen.DataSource = UserEmployeePD.GetData(cmdLine);

                datGrWen.Location = new Point(85, 132);
                datGrWen.Size = new System.Drawing.Size(610, 80);
                lblShowWen.Location = new Point(14, 135);

                cmdLine = "SELECT [Week] as [Тиждень]," +
                    " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                    " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                    " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                    " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                    " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                    "FROM TecahersTimeManageDatThu WHERE [IDTch] = '" + TeachId + "';";
                datGrThu.DataSource = UserEmployeePD.GetData(cmdLine);

                datGrThu.Location = new Point(85, 182);
                datGrThu.Size = new System.Drawing.Size(610, 80);
                lblShowThu.Location = new Point(14, 185);

                cmdLine = "SELECT [Week] as [Тиждень]," +
                    " CASE WHEN [1_lesson] = 1 THEN 'Присутній' ELSE '-' END as [1 пара]," +
                    " CASE WHEN [2_lesson] = 1 THEN 'Присутній' ELSE '-' END as [2 пара]," +
                    " CASE WHEN [3_lesson] = 1 THEN 'Присутній' ELSE '-' END as [3 пара], " +
                    " CASE WHEN [4_lesson] = 1 THEN 'Присутній' ELSE '-' END as [4 пара]," +
                    " CASE WHEN [5_lesson] = 1 THEN 'Присутній' ELSE '-' END as [5 пара]" +
                    "FROM TecahersTimeManageDatFri WHERE [IDTch] = '" + TeachId + "';";
                datGrFri.DataSource = UserEmployeePD.GetData(cmdLine);

                datGrFri.Location = new Point(85, 232);
                datGrFri.Size = new System.Drawing.Size(610, 80);
                lblShowFri.Location = new Point(8, 235);
            }
        }

        private void cBoxTeachersNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            bntFindTeacher.Checked = false;
        }

        private void btnAddTimeM_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlAddRp.Visible = false;
            pnlAddSchedule.Visible = false;

            cBoxTeachersNames.Items.Clear();

            //draw pnl
            pnlTimeManage.Size = new System.Drawing.Size(700, 580);
            pnlTimeManage.Location = new Point(10, 40);
            pnlTimeManage.Visible = true;

            //draw pnl1Week
            lblWeek1.Location = new Point(160, 60);
            pnlWeek1.Size = new System.Drawing.Size(280, 480);
            pnlWeek1.Location = new Point(70, 80);

            //draw pnl2Week
            lblWeek2.Location = new Point(450, 60);
            pnlWeek2.Size = new System.Drawing.Size(280, 480);
            pnlWeek2.Location = new Point(360, 80);

            cBoxTeachersNames2.Size = new System.Drawing.Size(150, 37);
            cBoxTeachersNames2.Location = new Point(10, 5);
            //draw btn
            btnSaveTimeM.Size = new System.Drawing.Size(90, 30);
            btnSaveTimeM.Location = new Point(160, 5);

            string cmd = "SELECT Tch2.[Full name] FROM TeachersLoadDat LoadTch" +
                             " JOIN TeachersDat Tch2 ON LoadTch.[IdTeacher] = Tch2.[ID]" +
                             " GROUP BY[Full name]";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    cBoxTeachersNames2.Items.Add(dr["Full name"].ToString());
                }
            }
            //show info
            pnlWeek1.Visible = true;
            pnlWeek2.Visible = true;

        }

        private void btnSaveTimeM_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + cBoxTeachersNames2.Text.ToString() + "'";
            string TeachId = UserEmployeePD.GetDataTeach(cmd);

            string CheckCmd = "SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatTue WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatWed WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatThu WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatMon WHERE [IDTch] = '" + TeachId + "'" +
                " SELECT *FROM TecahersTimeManageDatFri WHERE [IDTch] = '" + TeachId + "'";

            if (UserEmployeePD.CheckAvalDat(CheckCmd) == true)
            {
                DialogResult result = MessageBox.Show(
                        "Дані вже введені. Бажаєте їх перезаписати нитисніть 'Да'",
                        "Повідомлення AFSC",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";

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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";

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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";


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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";

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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";

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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 1 AND [IDTch] = '" + TeachId + "';";

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
                        cmdInsertTimeManage += "[5_lesson] = 1 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    else
                        cmdInsertTimeManage += "[5_lesson] = 0 WHERE [Week] = 2 AND [IDTch] = '" + TeachId + "';";
                    #endregion

                    UserEmployeePD.EmployeeInsertDat(cmdInsertTimeManage);
                }
            }
            else
            {
                string cmdInsertTimeManage = "INSERT INTO TecahersTimeManageDatMon Values (" + TeachId + ", 1, ";
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

                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatMon Values (" + TeachId + ", 2, ";

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
                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatTue Values (" + TeachId + ", 1, ";
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

                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatTue Values (" + TeachId + ", 2, ";

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
                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatWed Values (" + TeachId + ", 1, ";
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

                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatWed Values (" + TeachId + ", 2, ";

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
                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatThu Values (" + TeachId + ", 1, ";
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

                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatThu Values (" + TeachId + ", 2, ";

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
                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatFri Values (" + TeachId + ", 1, ";
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

                cmdInsertTimeManage += " INSERT INTO TecahersTimeManageDatFri Values (" + TeachId + ", 2, ";

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

                UserEmployeePD.EmployeeInsertDat(cmdInsertTimeManage);
            }
        }

        private void cBoxTeachersNames2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSaveTimeM.Checked = false;
            //mon
            Check1WeekMon1L.Checked = false;
            Check1WeekMon2L.Checked = false;
            Check1WeekMon3L.Checked = false;
            Check1WeekMon4L.Checked = false;
            Check1WeekMon5L.Checked = false;

            Check2WeekMon1L.Checked = false;
            Check2WeekMon2L.Checked = false;
            Check2WeekMon3L.Checked = false;
            Check2WeekMon4L.Checked = false;
            Check2WeekMon5L.Checked = false;
            //tue
            Check1WeekTue1L.Checked = false;
            Check1WeekTue2L.Checked = false;
            Check1WeekTue3L.Checked = false;
            Check1WeekTue4L.Checked = false;
            Check1WeekTue5L.Checked = false;

            Check2WeekTue1L.Checked = false;
            Check2WeekTue2L.Checked = false;
            Check2WeekTue3L.Checked = false;
            Check2WeekTue4L.Checked = false;
            Check2WeekTue5L.Checked = false;
            //wen
            Check1WeekWen1L.Checked = false;
            Check1WeekWen2L.Checked = false;
            Check1WeekWen3L.Checked = false;
            Check1WeekWen4L.Checked = false;
            Check1WeekWen5L.Checked = false;

            Check2WeekWen1L.Checked = false;
            Check2WeekWen2L.Checked = false;
            Check2WeekWen3L.Checked = false;
            Check2WeekWen4L.Checked = false;
            Check2WeekWen5L.Checked = false;
            //thu 
            Check1WeekThu1L.Checked = false;
            Check1WeekThu2L.Checked = false;
            Check1WeekThu3L.Checked = false;
            Check1WeekThu4L.Checked = false;
            Check1WeekThu5L.Checked = false;

            Check2WeekThu1L.Checked = false;
            Check2WeekThu2L.Checked = false;
            Check2WeekThu3L.Checked = false;
            Check2WeekThu4L.Checked = false;
            Check2WeekThu5L.Checked = false;
            //fri
            Check1WeekFri1L.Checked = false;
            Check1WeekFri2L.Checked = false;
            Check1WeekFri3L.Checked = false;
            Check1WeekFri4L.Checked = false;
            Check1WeekFri5L.Checked = false;

            Check2WeekFri1L.Checked = false;
            Check2WeekFri2L.Checked = false;
            Check2WeekFri3L.Checked = false;
            Check2WeekFri4L.Checked = false;
            Check2WeekFri5L.Checked = false;
        }

        private void ReplaceBtm_Click(object sender, EventArgs e)
        {
            //hover btn
            btn_Active(ReplaceBtm);
            btn_NotActive(InfoBtn, TimeManagBtn, ScheduleBtn, StudyPlanBtn, LogOutBtn);

            ScheduleBtn.Location = new Point(3, 160);
            ReplaceBtm.Location = new Point(3, 220);

            btnShowSchedule.Visible = false;
            btnAddSchedule.Visible = false;
            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;
            btnShowStudyPlan.Visible = false;
            btnAddStudyPlan.Visible = false;

            btnShowRp.Size = new System.Drawing.Size(230, 30);
            btnShowRp.Location = new Point(20, 260);

            btnAddRp.Size = new System.Drawing.Size(230, 30);
            btnAddRp.Location = new Point(20, 290);

            btnShowRp.Visible = true;
            btnAddRp.Visible = true;

            StudyPlanBtn.Location = new Point(3, 320);
            TimeManagBtn.Location = new Point(3, 380);
        }

        private void TimeManagBtn_Click(object sender, EventArgs e)
        {
            ScheduleBtn.Location = new Point(3, 160);
            ReplaceBtm.Location = new Point(3, 220);
            StudyPlanBtn.Location = new Point(3, 280);
            TimeManagBtn.Location = new Point(3, 340);
            //hover btn
            btn_Active(TimeManagBtn);
            btn_NotActive(InfoBtn, ReplaceBtm, ScheduleBtn, StudyPlanBtn, LogOutBtn);

            btnShowSchedule.Visible = false;
            btnAddSchedule.Visible = false;
            btnShowRp.Visible = false;
            btnAddRp.Visible = false;
            btnShowStudyPlan.Visible = false;
            btnAddStudyPlan.Visible = false;

            btnShowTimeM.Size = new System.Drawing.Size(230, 30);
            btnShowTimeM.Location = new Point(20, 380);

            btnAddTimeM.Size = new System.Drawing.Size(230, 30);
            btnAddTimeM.Location = new Point(20, 410);

            btnShowTimeM.Visible = true;
            btnAddTimeM.Visible = true;
        }

        private void StudyPlanBtn_Click(object sender, EventArgs e)
        {
            ScheduleBtn.Location = new Point(3, 160);
            ReplaceBtm.Location = new Point(3, 220);
            StudyPlanBtn.Location = new Point(3, 280);

            //hover btn
            btn_Active(StudyPlanBtn);
            btn_NotActive(InfoBtn, ReplaceBtm, ScheduleBtn, TimeManagBtn, LogOutBtn);

            btnShowSchedule.Visible = false;
            btnAddSchedule.Visible = false;
            btnShowRp.Visible = false;
            btnAddRp.Visible = false;
            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;

            btnShowStudyPlan.Size = new System.Drawing.Size(230, 30);
            btnShowStudyPlan.Location = new Point(20, 320);

            btnAddStudyPlan.Size = new System.Drawing.Size(230, 30);
            btnAddStudyPlan.Location = new Point(20, 350);

            btnShowStudyPlan.Visible = true;
            btnAddStudyPlan.Visible = true;

            TimeManagBtn.Location = new Point(3, 380);
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            ScheduleBtn.Location = new Point(3, 160);


            //frst button hover
            //hover btn
            btn_Active(ScheduleBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, StudyPlanBtn, InfoBtn, LogOutBtn);

            //hideothers pnl
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            // pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;
            btnShowRp.Visible = false;
            btnAddRp.Visible = false;
            btnShowStudyPlan.Visible = false;
            btnAddStudyPlan.Visible = false;

            btnShowSchedule.Size = new System.Drawing.Size(230, 30);
            btnShowSchedule.Location = new Point(20, 200);

            btnAddSchedule.Size = new System.Drawing.Size(230, 30);
            btnAddSchedule.Location = new Point(20, 230);

            btnShowSchedule.Visible = true;
            btnAddSchedule.Visible = true;

            ReplaceBtm.Location = new Point(3, 260);
            StudyPlanBtn.Location = new Point(3, 320);
            TimeManagBtn.Location = new Point(3, 380);
        }

        private void btnShowStudyPlan_Click(object sender, EventArgs e)
        {
            //hover on btn
            btn_Active(StudyPlanBtn);
            btn_NotActive(TimeManagBtn, ReplaceBtm, ScheduleBtn, InfoBtn, LogOutBtn);

            //hide other panels and btns
            pnlReplacment.Visible = false;
            pnlSchedule.Visible = false;
            pnlTimeManage.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlAddRp.Visible = false;
            pnlSchedule.Visible = false;

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

        private void FindDataBtn_Click(object sender, EventArgs e)
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
            ShowInfoStudyPlan.DataSource = UserEmployeePD.GetData(Fcmd);
        }

        private void btnShowRp_Click(object sender, EventArgs e)
        {
            //hide other panels
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlSchedule.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlAddRp.Visible = false;
            pnlAddSchedule.Visible = false;

            //panel Log, draw
            pnlReplacment.Size = new System.Drawing.Size(690, 450);
            pnlReplacment.Location = new Point(20, 130);
            pnlReplacment.Visible = true;

            //show DT info raplacement
            ShowRp1.Location = new Point(220, 20);
            ShowRp1.Size = new System.Drawing.Size(170, 30);

            comboBoxShowRp1.Size = new System.Drawing.Size(150, 37);
            comboBoxShowRp1.Location = new Point(40, 10);
            comboBoxShowRp1.Items.Clear();

            ShowRp2.Location = new Point(220, 210);
            ShowRp2.Size = new System.Drawing.Size(170, 30);

            comboBoxShowRp2.Size = new System.Drawing.Size(150, 37);
            comboBoxShowRp2.Location = new Point(40, 210);
            comboBoxShowRp2.Items.Clear();

            ShowInfoRp.Size = new System.Drawing.Size(690, 150);
            ShowInfoRp.Location = new Point(10, 60);

            ShowInfoRp2.Size = new System.Drawing.Size(690, 150);
            ShowInfoRp2.Location = new Point(10, 260);

            string cmd = "SELECT Tch2.[Full name] FROM TeachersLoadDat LoadTch" +
                            " JOIN TeachersDat Tch2 ON LoadTch.[IdTeacher] = Tch2.[ID]" +
                            " GROUP BY[Full name]";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    comboBoxShowRp1.Items.Add(dr["Full name"].ToString());
                    comboBoxShowRp2.Items.Add(dr["Full name"].ToString());
                }
            }

        }

        private void ShowRp1_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + comboBoxShowRp1.Text.ToString() + "'";
            string TeachId = UserEmployeePD.GetDataTeach(cmd);

            string CheckCmd = "SELECT Rp.[Date] as [Дата], Rp.[Group] as [Група], Rp.[LessNum] as [Номер пари], " +
                "Tch.[Full name] as [Заміняється], Tch2.[Full name] as [Заміняє],  LoadTch.[Subject] as [Предмет]" +
                    " FROM ReplacemmentsDat Rp " +
                    " JOIN TeachersLoadDat LoadTch on Rp.[IDSubWillRp] = LoadTch.[ID]" +
                    " JOIN TeachersDat Tch on Rp.[IDSubIsRp] = Tch.[ID]" +
                    " JOIN TeachersDat Tch2 on LoadTch.[IdTeacher] = Tch2.[ID]" +
                        " WHERE [IDSubIsRp] = '" + TeachId + "'";
            if (UserEmployeePD.CheckAvalDat(CheckCmd) == true)
            {

                ShowInfoRp.DataSource = UserEmployeePD.GetData(CheckCmd);
            }
            else
            {
                MessageBox.Show("На даний момент заміни відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
        }

        private void ShowRp2_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + comboBoxShowRp2.Text.ToString() + "'";
            string TeachId = UserEmployeePD.GetDataTeach(cmd);

            string CheckCmd = "SELECT Rp.[Date] as [Дата], Rp.[Group] as [Група], Rp.[LessNum] as [Номер пари], " +
                                " Tch.[Full name] as [Заміняється], Tch2.[Full name] as [Заміняє],  LoadTch.[Subject] as [Предмет]" +
                                    " FROM ReplacemmentsDat Rp" +
                                    " JOIN TeachersLoadDat LoadTch on Rp.[IDSubWillRp] = LoadTch.[ID]" +
                                    " JOIN TeachersDat Tch on Rp.[IDSubIsRp] = Tch.[ID]" +
                                    " JOIN TeachersDat Tch2 on LoadTch.[IdTeacher] = Tch2.[ID]" +
                                    " WHERE [IDSubWillRp] = '" + TeachId + "'";
            if (UserEmployeePD.CheckAvalDat(CheckCmd) == true)
            {

                ShowInfoRp2.DataSource = UserEmployeePD.GetData(CheckCmd);
            }
            else
            {
                MessageBox.Show("На даний момент заміни відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
        }

        private void comboBoxShowRp1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRp1.Checked = false;
        }

        private void comboBoxShowRp2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRp2.Checked = false;
        }

        private void btnAddRp_Click(object sender, EventArgs e)
        {
            //hide other panels
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlSchedule.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlSchedule.Visible = false;
            pnlAddSchedule.Visible = false;

            //panel Log, draw
            pnlAddRp.Size = new System.Drawing.Size(690, 430);
            pnlAddRp.Location = new Point(20, 150);

            calendarRp.Size = new System.Drawing.Size(30, 30);
            calendarRp.Location = new Point(60, 10);

            comboBoxGroup.Size = new System.Drawing.Size(120, 30);
            comboBoxGroup.Location = new Point(310, 70);
            lblGroup.Location = new Point(440, 70);
            comboBoxGroup.Items.Clear();
            string cmd = "SELECT [Group] FROM GroupsDat";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    comboBoxGroup.Items.Add(dr["Group"].ToString());
                }
            }

            comboBoxLessNum.Size = new System.Drawing.Size(100, 30);
            comboBoxLessNum.Location = new Point(30, 200);
            lblLessNum.Location = new Point(150, 200);

            comboBoxTchIsRp.Size = new System.Drawing.Size(150, 30);
            comboBoxTchIsRp.Location = new Point(310, 200);
            lblTchIsRp.Location = new Point(470, 200);
            comboBoxTchIsRp.Items.Clear();

            comboBoxTchWillRp.Size = new System.Drawing.Size(150, 30);
            comboBoxTchWillRp.Location = new Point(30, 250);
            lblTchWillRp.Location = new Point(190, 250);
            comboBoxTchWillRp.Items.Clear();
            cmd = "SELECT Tch2.[Full name] FROM TeachersLoadDat LoadTch" +
                " JOIN TeachersDat Tch2 ON LoadTch.[IdTeacher] = Tch2.[ID]" +
                " GROUP BY[Full name]";
            dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    comboBoxTchIsRp.Items.Add(dr["Full name"].ToString());
                    comboBoxTchWillRp.Items.Add(dr["Full name"].ToString());
                }
            }

            //comboBoxSubjects.Size = new System.Drawing.Size(230, 30);
            //comboBoxSubjects.Location = new Point(310, 250);
            //lblSubjects.Location = new Point(550, 250);
            //cmd = "SELECT Sbj.[Name] FROM TeachersLoadDat LoadTch" +
            //        " JOIN SubjectsDat Sbj ON LoadTch.[Subject] = Sbj.[Name]" +
            //        " GROUP BY[Name]";
            //dtbl = UserEmployeePD.GetData(cmd);

            //if (dtbl.Rows.Count <= 0)
            //{
            //    MessageBox.Show("На даний момент дані про викладачів відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    foreach (DataRow dr in dtbl.Rows)
            //    {
            //        comboBoxSubjects.Items.Add(dr["Name"].ToString());
            //    }
            //}

            btnInsertRp.Size = new System.Drawing.Size(100, 30);
            btnInsertRp.Location = new Point(290, 330);

            pnlAddRp.Visible = true;
        }

        private void btnInsertRp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxGroup.Text) && string.IsNullOrEmpty(comboBoxLessNum.Text) &&
                        string.IsNullOrEmpty(comboBoxTchIsRp.Text) && string.IsNullOrEmpty(comboBoxTchWillRp.Text) 
                            && string.IsNullOrEmpty(calendarRp.Text))
            {
                MessageBox.Show("Помилка! Оберіть всу пункти, для коректного заповнення даних", "Повідомлення AFSC",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + comboBoxTchIsRp.Text.ToString() + "'";
                string TeachIdIsRp = UserEmployeePD.GetDataTeach(cmd);
                     cmd = "SELECT [ID] FROM TeachersDat WHERE [Full name] = '" + comboBoxTchWillRp.Text.ToString() + "'";
                string TeachIdWillRp = UserEmployeePD.GetDataTeach(cmd);

                DateTime start = calendarRp.SelectionRange.Start;
                string formattedStart = start.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                cmd = "INSERT INTO ReplacemmentsDat VALUES ('" + formattedStart +
                        "', '" + comboBoxGroup.Text + "', '" + comboBoxLessNum.Text + 
                        "', '" + TeachIdIsRp+ "', '" + TeachIdWillRp+ "');";

                UserEmployeePD.EmployeeInsertDat(cmd);

                MessageBox.Show("Дані внесено", "Повідомлення AFSC",
                                        MessageBoxButtons.OK);

                comboBoxTchWillRp.Items.Clear();
                comboBoxTchIsRp.Items.Clear();
                comboBoxGroup.Items.Clear();
                comboBoxLessNum.Items.Clear();
            }
        }

        private void btnShowSchedule_Click(object sender, EventArgs e)
        {
            //hideothers pnl
            pnlShowTimeMan.Visible = false;
            pnlReplacment.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlTimeManage.Visible = false;
            pnlAddRp.Visible = false;
            pnlSchedule.Visible = false;

            // pnlShowTimeMan.Visible = false;

            btnShowTimeM.Visible = false;
            btnAddTimeM.Visible = false;
            btnShowRp.Visible = false;
            btnAddRp.Visible = false;
            btnShowStudyPlan.Visible = false;
            btnAddStudyPlan.Visible = false;

            btnShowSchedule.Size = new System.Drawing.Size(230, 30);
            btnShowSchedule.Location = new Point(20, 200);
            btnAddSchedule.Size = new System.Drawing.Size(230, 30);
            btnAddSchedule.Location = new Point(20, 230);

            btnShowSchedule.Visible = true;
            btnAddSchedule.Visible = true;

            ReplaceBtm.Location = new Point(3, 260);
            StudyPlanBtn.Location = new Point(3, 320);
            TimeManagBtn.Location = new Point(3, 380);

            //draw pnl
            pnlSchedule.Size = new System.Drawing.Size(700, 550);
            pnlStudyPlan.Location = new Point(15, 40);
            pnlSchedule.Visible = true;

            dgridShowSheduleW1.Visible = false;
            dgridShowSheduleW2.Visible = false;
            lblScheduleW1.Visible = false;
            lblScheduleW2.Visible = false;

            comboBoxGroupSch.Size = new System.Drawing.Size(110, 30);
            comboBoxGroupSch.Location = new Point(10, 10);
            comboBoxGroupSch.Items.Clear();

            string cmd = "SELECT [Group] FROM GroupsDat";
            DataTable dtbl = UserEmployeePD.GetData(cmd);

            if (dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("На даний момент дані відсутні", "Повідомлення AFSC", MessageBoxButtons.OK);
            }
            else
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    comboBoxGroupSch.Items.Add(dr["Group"].ToString());
                }
            }
            btnSchowScheduleInPnl.Size = new System.Drawing.Size(80, 30);
            btnSchowScheduleInPnl.Location = new Point(130, 10);    
        }

        private void btnSchowScheduleInPnl_Click(object sender, EventArgs e)
        {
            dgridShowSheduleW1.Visible = true;
            dgridShowSheduleW2.Visible = true;
            lblScheduleW1.Visible = true;
            lblScheduleW2.Visible = true;

            #region PNL SCHEDULE1
            //draw pnl
            lblScheduleW1.Location = new Point(150, 50);
            dgridShowSheduleW1.Size = new System.Drawing.Size(340, 490);
            dgridShowSheduleW1.Location = new Point(0, 70);

            lblLeft1.Location = new Point(0, 0);
            lblLeft1.Size = new System.Drawing.Size(1, 450);
            lblLeft2.Location = new Point(339, 0);
            lblLeft2.Size = new System.Drawing.Size(1, 450);

            pnlNawSchedule1.Location = new Point(0, 0);
            pnlNawSchedule1.Size = new System.Drawing.Size(400, 1);

            lblMon1.Location = new Point(0, 35);
            pnlNawSchedule2.Location = new Point(0, 90);
            pnlNawSchedule2.Size = new System.Drawing.Size(400, 1);
            string cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek1Mon.Location = new Point(70, 5);
            dgridScheduleWeek1Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Mon.DataSource = UserEmployeePD.GetData(cmd);

            lblTue1.Location = new Point(0, 125);
            pnlNawSchedule3.Location = new Point(0, 180);
            pnlNawSchedule3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek1Tue.Location = new Point(70, 95);
            dgridScheduleWeek1Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Tue.DataSource = UserEmployeePD.GetData(cmd);

            lblWen1.Location = new Point(0, 215);
            pnlNawSchedule4.Location = new Point(0, 270);
            pnlNawSchedule4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek1Wen.Location = new Point(70, 185);
            dgridScheduleWeek1Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Wen.DataSource = UserEmployeePD.GetData(cmd);


            lblThu1.Location = new Point(0, 305);
            pnlNawSchedule5.Location = new Point(0, 360);
            pnlNawSchedule5.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                       " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek1Thu.Location = new Point(70, 275);
            dgridScheduleWeek1Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Thu.DataSource = UserEmployeePD.GetData(cmd);


            lblFri1.Location = new Point(0, 395);
            pnlNawSchedule6.Location = new Point(0, 450);
            pnlNawSchedule6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 1 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek1Fri.Location = new Point(70, 365);
            dgridScheduleWeek1Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek1Fri.DataSource = UserEmployeePD.GetData(cmd);
            #endregion

            #region PNL SCHEDULE2
            //draw pnl1Week
            lblScheduleW2.Location = new Point(490, 50);
            dgridShowSheduleW2.Size = new System.Drawing.Size(340, 490);
            dgridShowSheduleW2.Location = new Point(340, 70);

            pnlNawL.Location = new Point(0, 0);
            pnlNawL.Size = new System.Drawing.Size(1, 450);
            pnlNawR.Location = new Point(339, 0);
            pnlNawR.Size = new System.Drawing.Size(1, 450);

            pnlNaw5.Location = new Point(0, 0);
            pnlNaw5.Size = new System.Drawing.Size(400, 1);

            lblMon2.Location = new Point(0, 35);
            pnlNaw1.Location = new Point(0, 90);
            pnlNaw1.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatMon WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek2Mon.Location = new Point(70, 5);
            dgridScheduleWeek2Mon.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Mon.DataSource = UserEmployeePD.GetData(cmd);

            lblTue2.Location = new Point(0, 125);
            pnlNaw2.Location = new Point(0, 180);
            pnlNaw2.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatTue WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek2Tue.Location = new Point(70, 95);
            dgridScheduleWeek2Tue.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Tue.DataSource = UserEmployeePD.GetData(cmd);

            lblWen2.Location = new Point(0, 215);
            pnlNaw3.Location = new Point(0, 270);
            pnlNaw3.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatWen WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek2Wen.Location = new Point(70, 185);
            dgridScheduleWeek2Wen.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Wen.DataSource = UserEmployeePD.GetData(cmd);

            lblThu2.Location = new Point(0, 305);
            pnlNaw4.Location = new Point(0, 360);
            pnlNaw4.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatThu WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek2Thu.Location = new Point(70, 275);
            dgridScheduleWeek2Thu.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Thu.DataSource = UserEmployeePD.GetData(cmd);

            lblFri2.Location = new Point(0, 395);
            pnlNaw6.Location = new Point(0, 450);
            pnlNaw6.Size = new System.Drawing.Size(400, 1);

            cmd = "SELECT [№] = 1, [1_lesson] as [Предмет], [Lecture/practice1] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 2, [2_lesson] as [Предмет], [Lecture/practice2] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 3, [3_lesson] as [Предмет], [Lecture/practice3] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "' UNION" +
                      " SELECT [№] = 4, [4_lesson] as [Предмет], [Lecture/practice4] as [Лекція/прктична] FROM ScheduleDatFri WHERE [Week]= 2 AND [Group] = '" + comboBoxGroupSch.Text.ToString() + "'";
            dgridScheduleWeek2Fri.Location = new Point(70, 365);
            dgridScheduleWeek2Fri.Size = new System.Drawing.Size(400, 90);
            dgridScheduleWeek2Fri.DataSource = UserEmployeePD.GetData(cmd);

            #endregion
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlTimeManage.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlAddRp.Visible = false;

            pnlAddSchedule.Visible = true;
        }

        private void btnAddStudyPlan_Click(object sender, EventArgs e)
        {
            pnlSchedule.Visible = false;
            pnlReplacment.Visible = false;
            pnlTimeManage.Visible = false;
            pnlStudyPlan.Visible = false;
            pnlShowTimeMan.Visible = false;
            pnlAddRp.Visible = false;
            pnlAddSchedule.Visible = false;
        }
    }
}