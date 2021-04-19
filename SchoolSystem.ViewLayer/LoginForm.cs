using SchoolSystem.DataLayer.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.ViewLayer.Helpers;


namespace SchoolSystem.ViewLayer
{
    public partial class LoginForm : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private readonly TeacherService _teacherService = new TeacherService();
        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(400, 100);
        }

      
        private void LoginBox_Click(object sender, EventArgs e)
        {
            loginBox.Text = null;
            loginBox.ForeColor = Color.Black;
        }

        private void PwdBox_Click(object sender, EventArgs e)
        {
            password.Text = null;
            password.ForeColor = Color.Black;
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            if (studentRadioButton.Checked)
            {
                var student = await _studentService.VerifyStudentCredentials(loginBox.Text, password.Text);
                if (student != null)
                {
                    User.RememberUser(student.FirstName, student.LastName, User.Type.Student, student.Id, student.Login);
                    MainForm form = new MainForm();
                    form.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show(@"Sorry bad credentials!");
                }
            }

            if(teacherRadioButton.Checked)
            {
                TeacherEntity teacher = await _teacherService.VerifyTeacherCredentials(loginBox.Text, password.Text);
                if (teacher != null)
                {
                    User.RememberUser(teacher.FirstName,teacher.LastName,User.Type.Teacher,teacher.Id,teacher.Login);
                    MainForm form = new MainForm();
                    form.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show(@"Sorry bad credentials!");
                }
            }

            if (adminRadioButton.Checked)
            {
                if (loginBox.Text == @"admin" && password.Text == @"admin")
                {
                    User.RememberUser("admin", "", User.Type.Admin, 1, "admin");
                    MainForm form = new MainForm();
                    form.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show(@"Sorry bad credentials!");
                }
            }
        }

    }
}

