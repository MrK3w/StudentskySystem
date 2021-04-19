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
    public partial class Timetable : Form
    {
        private readonly StudentsLessonService _studentsLessonService = new StudentsLessonService();
        private IEnumerable<StudentsLessonEntity> _studentsLessonPlan = new List<StudentsLessonEntity>();
        public Timetable()
        {
            InitializeComponent();
            Task.Run(async () => await GetTimeTables()).Wait();
        }

        private async Task GetTimeTables()
        {
            _studentsLessonPlan = await _studentsLessonService.GetListOfAllPlans();
            if (User.Typ != User.Type.Student)
            {
                newMenu.Text = @"Add new subject";
                comboBox.Visible = true;
                label2.Visible = true;
                
                var students = _studentsLessonPlan.GroupBy(elem => elem.Student.Id).
                    Select(group => group.First()).Select(x => x.Student).ToList();
                
                var stud = new StudentEntity()
                {
                    FirstName = "All",
                    LastName = "students"
                };
                students.Add(stud);
                comboBox.DataSource = students;
                comboBox.SelectedItem = stud;
            }
            PrepareTable();
        }

        private void PrepareTable(int id =0)
        {
            IEnumerable<StudentsLessonEntity> lessonPlanPersonal = _studentsLessonPlan.ToList();
            if (User.Typ == User.Type.Student)
            {
                lessonPlanPersonal = _studentsLessonPlan.Where(lesson => lesson.Student.Id == User.Id);
            }

            if (id != 0)
            {
                lessonPlanPersonal = _studentsLessonPlan.Where(lesson => lesson.Student.Id == id);
            }
            timeTables.DataSource = (from lesson in lessonPlanPersonal
                                     orderby lesson.LessonPlan.Day, lesson.LessonPlan.StartTime, lesson.LessonPlan.Subject.Name
                                     select new
                                     {
                                         lesson.Id,
                                         lesson.LessonPlan.Day,
                                         lesson.LessonPlan.StartTime,
                                         lesson.LessonPlan.EndTime,
                                         lesson.LessonPlan.Subject.Name,
                                         TeacherName = lesson.LessonPlan.Subject.HeadTeacher.FirstName + " " + lesson.LessonPlan.Subject.HeadTeacher.LastName,
                                         StudentName = lesson.Student.FirstName + " " + lesson.Student.LastName
                                     }).ToList();
            timeTables.Columns[0].HeaderText = @"ID";
            timeTables.Columns[0].Visible = false;
            timeTables.Columns[1].HeaderText = @"Day";
            timeTables.Columns[2].HeaderText = @"Start";
            timeTables.Columns[3].HeaderText = @"End";
            timeTables.Columns[4].HeaderText = @"Subject";
            timeTables.Columns[5].HeaderText = @"Head Teacher full name";
            timeTables.Columns[6].HeaderText = @"Student";
            if (User.Type.Student == User.Typ) timeTables.Columns[6].Visible = false;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
            MainForm form = new MainForm();
            form.Show();
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var student = (StudentEntity)comboBox.SelectedItem;
            PrepareTable(student.Id);
        }

        private void newMenu_Click(object sender, EventArgs e)
        {
            AssignSubjectForm form = new AssignSubjectForm();
            form.Show();
            Close();
        }
    }
}

