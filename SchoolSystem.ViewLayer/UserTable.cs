using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.ViewLayer.Helpers;

namespace SchoolSystem.ViewLayer
{
    public partial class UserTable : Form
    {
        private readonly StudentService _studentService = new StudentService();
        private readonly TeacherService _teacherService = new TeacherService();
        private IEnumerable<StudentEntity> _studentList;
        private IEnumerable<TeacherEntity> _teacherList;
        private bool _change;

        public UserTable()
        {
            InitializeComponent();
            if (User.Typ != User.Type.Admin)
            {
                addStudentButton.Hide();
            }
            Task.Run(async () => await GetData()).Wait();
        }

      
        private async Task GetData()
        {
            _studentList = await _studentService.GetListOfAllStudents();
            _teacherList = await _teacherService.GetListOfAllTeachers();
            ChangeDataSource();
        }

        private void ChangeDataSource()
        {
            if (!radioButton2.Checked)
            {
                table.DataSource = (from student in _studentList
                                    orderby student.LastName
                    select new { student.Id, student.FirstName, student.LastName,student.Login}).ToList();
                table.Columns[0].HeaderText = @"ID";
                table.Columns[0].Visible = false;
                table.Columns[1].HeaderText = @"First Name";
                table.Columns[2].HeaderText = @"Last Name";
                table.Columns[3].HeaderText = @"Login";
            }
            else
            {
                table.DataSource = (from teacher in _teacherList
                    orderby teacher.LastName
                    select new {teacher.Id, teacher.FirstName, teacher.LastName, teacher.Login,teacher.PhoneNumber }).ToList();
                table.Columns[0].HeaderText = @"ID";
                table.Columns[0].Visible = false;
                table.Columns[1].HeaderText = @"First Name";
                table.Columns[2].HeaderText = @"Last Name";
                table.Columns[3].HeaderText = @"Login";
                table.Columns[4].HeaderText = @"Phone number";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            addStudentButton.Text = _change ? "Add student" : "Add teacher";

            _change = !_change;
            ChangeDataSource();
        }

        private void DoubleClickTableEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || User.Typ != User.Type.Admin)
            {
                return;
            }

            switch (radioButton2.Checked)
            {
                case false:
                {
                    var id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
                    var student = GetStudent(id);
                    OpenEditStudent(student);
                    break;
                }
                case true:
                {
                    var id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
                    var teacher = GetTeacher(id);
                    OpenEditTeacher(teacher);
                    break;
                }
            }
        }

        private void OpenEditStudent(StudentEntity student)
        {
            EditStudent form = new EditStudent(student);
            form.Show();
            Close();
        }

        private void OpenEditTeacher(TeacherEntity teacher)
        {
            EditTeacher form = new EditTeacher(teacher);
            form.Show();
            Close();
        }

        private StudentEntity GetStudent(int id)
        {
            StudentEntity student = new StudentEntity();
            foreach (var stud in _studentList)
            {
                if (stud.Id == id)
                {
                    return stud;
                }
            }

            return student;
        }

        private TeacherEntity GetTeacher(int id)
        {
            TeacherEntity teacher = new TeacherEntity();
            foreach (var teach in _teacherList)
            {
                if (teach.Id == id)
                {
                    return teach;
                }
            }
            return teacher;
        }

        private void AddUser(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
            {
                OpenEditStudent(new StudentEntity());
            }
            else
            {
                OpenEditTeacher(new TeacherEntity());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
            MainForm form = new MainForm();
            form.Show();
        }
    }
}
