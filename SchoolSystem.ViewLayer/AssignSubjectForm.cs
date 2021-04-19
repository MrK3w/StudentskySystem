using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;
using SchoolSystem.ViewLayer.Helpers;

namespace SchoolSystem.ViewLayer
{
    public partial class AssignSubjectForm : Form
    {
        private bool _allAvailablePlans;
        private readonly LessonPlanService _lessonPlanService = new LessonPlanService();
        private readonly StudentsLessonService _studentsLessonService = new StudentsLessonService();
        private IEnumerable<LessonPlanEntity> _lessonPlan = new List<LessonPlanEntity>();
        private IEnumerable<StudentsLessonEntity> _studentLessonsList = new List<StudentsLessonEntity>();
        private List<StudentsLessonEntity> _studentsSubjects;
        private List<LessonPlanEntity> _subjectWhichStudentDontHave;
        public AssignSubjectForm()
        {
            InitializeComponent();
            if (User.Typ == User.Type.Student)
            {
                studentButton.Show();
                studentLabel.Show();
                button1.Hide();
            }
            PrepareLists();
        }

        private async void PrepareLists()
        {
            _studentLessonsList = await _studentsLessonService.GetListOfAllPlans();
            _lessonPlan = await _lessonPlanService.GetListOfAllPlans();
            _studentsSubjects = _studentLessonsList.Where(x => x.Student.Id == User.Id).ToList();
            List<LessonPlanEntity> lessonPlanEntities = _lessonPlan.ToList();
            _subjectWhichStudentDontHave = lessonPlanEntities.ToList();
            foreach (var lesson in lessonPlanEntities)
            {
                foreach (var subjects in _studentsSubjects)
                {
                    if (subjects.LessonPlan.Id == lesson.Id) _subjectWhichStudentDontHave.Remove(lesson);
                }

            }
            LoadTable();
        }

        private void LoadTable()
        {
            if (User.Typ != User.Type.Student)
            {
                table.DataSource = (from lesson in _lessonPlan
                        orderby lesson.Day, lesson.StartTime,lesson.Subject.Name
                        select new
                        {
                            lesson.Id,
                            lesson.Day,
                            lesson.StartTime,
                            lesson.EndTime,
                            lesson.Subject.Name,
                        }).ToList();
                table.Columns[0].HeaderText = @"ID";
                table.Columns[0].Visible = false;
                table.Columns[1].HeaderText = @"Day";
                table.Columns[2].HeaderText = @"Start";
                table.Columns[3].HeaderText = @"End";
                table.Columns[4].HeaderText = @"Subject";

            }
            else if(User.Typ == User.Type.Student && _allAvailablePlans == false)
            {
                table.DataSource = (from subject in _studentsSubjects
                    orderby subject.LessonPlan.Day, subject.LessonPlan.StartTime, subject.LessonPlan.Subject.Name
                    select new
                    {
                        subject.Id,
                        subject.LessonPlan.Day,
                        subject.LessonPlan.StartTime,
                        subject.LessonPlan.EndTime,
                        subject.LessonPlan.Subject.Name
                    }).ToList();
                table.Columns[0].HeaderText = @"ID";
                table.Columns[0].Visible = false;
                table.Columns[1].HeaderText = @"Day";
                table.Columns[2].HeaderText = @"Start";
                table.Columns[3].HeaderText = @"End";
                table.Columns[4].HeaderText = @"Subject";
            }
            else if (User.Typ == User.Type.Student && _allAvailablePlans)
            {
               
                table.DataSource = (from subject in _subjectWhichStudentDontHave
                    orderby subject.Day, subject.StartTime, subject.Subject.Name
                    select new
                    {
                        subject.Id,
                        subject.Day,
                        subject.StartTime,
                        subject.EndTime,
                        subject.Subject.Name,
                    }).ToList();
                table.Columns[0].HeaderText = @"ID";
                table.Columns[0].Visible = false;
                table.Columns[1].HeaderText = @"Day";
                table.Columns[2].HeaderText = @"Start";
                table.Columns[3].HeaderText = @"End";
                table.Columns[4].HeaderText = @"Subject";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            OpenForm(new LessonPlanEntity());
        }

        private void OpenForm(LessonPlanEntity entity)
        {
            EditTimetable form = new EditTimetable(entity);
            form.Show();
            Close();
        }

        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !User.Exists())
            {
                return;
            }

            if (User.Typ == User.Type.Student && _allAvailablePlans)
            {

                var message = @"Do you want to assign this subject?";
                var title = @"subject";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    var id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
                    var lessonPlan = GetLessonPlan(id);
                    try
                    {
                        await _studentsLessonService.Insert(new StudentsLessonEntity()
                        {
                            LessonPlan = lessonPlan,
                            Student = new StudentEntity()
                            {
                                Id = User.Id
                            }
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(@"Cannot be changed");
                    }

                    PrepareLists();
                }
            }
            else if (User.Typ == User.Type.Student && _allAvailablePlans == false)
            {
                string message = @"Do you want to remove this subject from your plan?";
                string title = @"subject";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    var id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
                    var lessonPlan = GetStudentSubject(id);
                    try
                    {
                        await _studentsLessonService.Delete(lessonPlan);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(@"Cannot be changed!");
                    }
                    PrepareLists();
                }
            }
            else
            {
                var id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
                var subject = GetLessonPlan(id);
                OpenForm(subject);
            }
      
        }

        private StudentsLessonEntity GetStudentSubject(int id)
        {
            StudentsLessonEntity studentsLesson = new StudentsLessonEntity();
            foreach (var les in _studentLessonsList)
            {
                if (les.Id == id)
                {
                    studentsLesson = les;
                }
            }

            return studentsLesson;
        }


        private LessonPlanEntity GetLessonPlan(int id)
        {
            foreach (var les in _lessonPlan)
            {
                if (les.Id == id)
                {
                    return les;
                }
            }

            return default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Timetable form = new Timetable();
            form.Show();
            Close();
        }

        private void studentButton_Click(object sender, EventArgs e)
        {
            if (!_allAvailablePlans)
            {
                _allAvailablePlans = !_allAvailablePlans;
                studentButton.Text = @"Show my plans";
                studentLabel.Text = @"Lesson that i don't attend yet";
            }
            else
            {
                _allAvailablePlans = !_allAvailablePlans;
                studentButton.Text = @"Show all available plans";
                studentLabel.Text = @"My plans";
            }

            LoadTable();
        }
    }
}
