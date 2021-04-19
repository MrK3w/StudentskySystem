using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.ViewLayer
{
    public partial class EditTeacher : Form
    {
        private readonly TeacherEntity _teacher;
        private readonly TeacherService _teacherService = new TeacherService();
        public EditTeacher(TeacherEntity teacher)
        {
            InitializeComponent();
            _teacher = teacher;
            LoadTeacher();
        }

        private void LoadTeacher()
        {
            FirstNameBox.Text = _teacher.FirstName;
            LastNameBox.Text = _teacher.LastName;
            TelephoneTextbox.Text = _teacher.PhoneNumber;
            PasswordBox.Text = _teacher.Password;
            RepeatPasswordBox.Text = _teacher.Password;
        }

        private void GoBack(object sender, EventArgs e)
        {
            CloseForm();
        }

        private async void Confirm_Click(object sender, EventArgs e)
        {
            if (!CheckValue()) return;
            PrepareTeacher();
            if (_teacher.Id ==0)
            {
                try
                {
                    await Insert();
                }
                catch (SqlException)
                {
                    Console.WriteLine(@"Cannot be deleted because teacher is probably already teaching some subjects this semester!");
                }
            }
            else
            {
                await UpdateStudent();
            }
        }

        private void PrepareTeacher()
        {
            _teacher.FirstName = FirstNameBox.Text;
            _teacher.LastName = LastNameBox.Text;
            _teacher.Password = PasswordBox.Text;
            _teacher.PhoneNumber = TelephoneTextbox.Text;


            if (_teacher.Id != 0) return;
            var login = new string(_teacher.LastName.Take(3).ToArray()).ToUpper();
            Random rng = new Random();
            var number = rng.Next(0, 9999).ToString();
            login += GetNumber(number);
            _teacher.Login = login;

        }

        private string GetNumber(string number)
        {
            for (int i = number.Length; i < 4; i++)
            {
                number = "0" + number;
            }

            return number;
        }

        private async Task Insert()
        {
            await _teacherService.InsertTeacher(_teacher);
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
                await _teacherService.UpdateTeacher(_teacher);
            }
            catch (Exception )
            {
                MessageBox.Show(@"Cannot be updated!");
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

            if (PasswordBox.Text != "" && RepeatPasswordBox.Text != "" &&
                PasswordBox.Text == RepeatPasswordBox.Text) return true;
            MessageBox.Show(@"Bad password!");
            return false;

        }

        private async void DeleteClick(object sender, EventArgs e)
        {
            if (!CheckValue()) return;
            PrepareTeacher();
            try
            {
                await _teacherService.DeleteTeacher(_teacher);
            }
            catch(Exception)
            {
                MessageBox.Show(@"Cannot be deleted because teacher is probably something already teaching!");
            }

            CloseForm();
        }

        private void TelephoneTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
