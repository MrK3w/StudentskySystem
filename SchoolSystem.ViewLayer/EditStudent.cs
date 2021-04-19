using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.ViewLayer
{
    public partial class EditStudent : Form
    {
        private readonly StudentEntity _student;
        private readonly StudentService _studentService = new StudentService();
        public EditStudent(StudentEntity student)
        {
            InitializeComponent();
            _student = student;
            LoadStudent();
        }

        private void LoadStudent()
        {
            FirstNameBox.Text = _student.FirstName;
            LastNameBox.Text = _student.LastName;
            PasswordBox.Text = _student.Password;
            RepeatPasswordBox.Text = _student.Password;
        }


        private void GoBack(object sender, EventArgs e)
        {
            CloseForm();
        }

        private async void Confirm_Click(object sender, EventArgs e)
        {
            if (!CheckValue()) return;
            PrepareStudent();
            if (_student.Id ==0)
            {
                await Insert();
            }
            else
            {
                await UpdateStudent();
            }
        }

        private void PrepareStudent()
        {
            _student.FirstName = FirstNameBox.Text;
            _student.LastName = LastNameBox.Text;
            _student.Password = PasswordBox.Text;
            var login = new string(_student.LastName.Take(3).ToArray());
            login = login.ToUpper();
         
            if (_student.Id == 0)
            {
                Random rng = new Random();
                var number = rng.Next(0, 9999).ToString();
                login += GetNumber(number);
            }
            _student.Login = login;
        }

        private string GetNumber(string number)
        {
            for (var i = number.Length; i < 4; i++)
            {
                number = "0" + number;
            }

            return number;
        }

        private async Task Insert()
        {
            try
            {
                await _studentService.InsertStudent(_student);
            }
            catch (Exception)
            {
                Console.WriteLine(@"Student cannot be deleted because he probably has some selected images!");
            }

            CloseForm();
        }

        private void CloseForm()
        {
            Close();
            UserTable table = new UserTable();
            table.Show();
        }

        private async Task UpdateStudent()
        {
            try
            {
                await _studentService.UpdateStudent(_student);
            }
            catch (Exception )
            {
                MessageBox.Show("Cannot be updated");
            }
           
            Close();
            UserTable table = new UserTable();
            table.Show();
        }

        private bool CheckValue()
        {
            if (FirstNameBox.Text == "")
            {
                MessageBox.Show(@"You didn't specify name!");
                return false;
            }
            if (LastNameBox.Text == "")
            {
                MessageBox.Show(@"You didn't specify surname!");
                return false;
            }
            if (PasswordBox.Text == "" || RepeatPasswordBox.Text =="" || PasswordBox.Text != RepeatPasswordBox.Text)
            {
                MessageBox.Show(@"Bad password!");
                return false;
            }

            return true;
        }

        private async void DeleteClick(object sender, EventArgs e)
        {
            if (!CheckValue()) return;
            PrepareStudent();
            try
            {
                await _studentService.DeleteStudent(_student);
            }
            catch (SqlException)
            {
                Console.WriteLine(@"Cannot delete student because he has already selected some lessons");
            }
            CloseForm();
        }
    }
}
