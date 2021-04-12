using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace AFSC
{

    public partial class Form : System.Windows.Forms.Form
    {
        Color farbe;
        Point lstPoint;
        private string logStr = "Логін";
        private string passStr = " Пароль";

        public Form()
        {
            InitializeComponent();

            txtLogFields();

            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
        }

        public void txtLogFields()
        {
            this.PassField.Size = new Size(this.PassField.Size.Width, 30);//size of textBox password
            //invible info aboute textBox (login)
            farbe = LoginField.ForeColor;
            LoginField.ForeColor = Color.Gray;//color of txt
            LoginField.GotFocus += RemoveTextLog; //remove txt
            LoginField.LostFocus += AddTextLog;
            LoginField.Text = logStr;

            //invible info aboute textBox (password)
            PassField.UseSystemPasswordChar = false;
            farbe = PassField.ForeColor;
            PassField.ForeColor = Color.Gray;
            PassField.GotFocus += RemoveTextPass;
            PassField.LostFocus += AddTextPass;
            PassField.Text = passStr;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && LoginField.Text != "" && PassField.Text != "")
            {
                LoginBtn_Click(sender, e);
            }
        }

        public void RemoveTextLog(object sender, EventArgs e)
        {
          //  LoginField.ForeColor = Color.Gray;
            if (LoginField.Text == logStr)
            {
                LoginField.Text = "";
                LoginField.ForeColor = Color.Black;
            }      
        }
        public void AddTextLog(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginField.Text))
            {
                LoginField.ForeColor = Color.Gray;
                LoginField.Text = logStr;
            }
        }

        public void RemoveTextPass(object sender, EventArgs e)
        {
            if (PassField.Text == passStr)
            {
                PassField.Text = "";           
                PassField.ForeColor = Color.Black;
                PassField.UseSystemPasswordChar = true;
            }         
        }
        public void AddTextPass(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PassField.Text))
            {
                PassField.ForeColor = Color.Gray;
                PassField.Text = passStr;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {

            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lstPoint.X;
                this.Top += e.Y - lstPoint.Y;
            }
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            lstPoint = new Point(e.X, e.Y);
        }

        private void Display_password(object sender, EventArgs e)
        {
            if (DisplayPass.Checked)
            {
                PassField.UseSystemPasswordChar = false;
            }
            else
            {
                PassField.UseSystemPasswordChar = true;
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Authorization login = new Authorization(LoginField.Text.Trim(), PassField.Text.Trim());

            switch (login.Autorithation())
             {
                case "Student":
                {
                    login.LogRegicter("Login");
                    StudentForm objFrmStudent = new StudentForm(this);
                    objFrmStudent.Show();
                    this.Hide();
                    break;
                }
                case "Teacher":
                {
                    login.LogRegicter("Login");
                    TeacherForm objFrmTeacher = new TeacherForm(this);
                    objFrmTeacher.Show();
                    this.Hide();    
                    break;
                }
                case "EmployeePD":
                {
                    login.LogRegicter("Login");
                    EmployeePD objFrmEmployeePD = new EmployeePD(this);
                    objFrmEmployeePD.Show();
                    this.Hide();
                    break;
                }
                default:
                {                  
                    MessageBox.Show("Невірно введені дані. Повторіть спробу", "Помилка авторизації", 
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
             }
        }
        private void MinWinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
