using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchoolSystem.ViewLayer
{
    public partial class EditTimetable : Form
    {
        private readonly List<TimeSpan> _startTimes = new List<TimeSpan>()
        {
            new TimeSpan(7, 15, 00),
            new TimeSpan(8, 00, 00),
            new TimeSpan(9, 00, 00),
            new TimeSpan(9, 45, 00),
            new TimeSpan(10, 45, 00),
            new TimeSpan(11, 30, 00),
            new TimeSpan(12, 30, 00),
            new TimeSpan(13, 15, 00),
            new TimeSpan(14, 15, 00),
            new TimeSpan(15, 00, 00),
            new TimeSpan(16, 00, 00),
            new TimeSpan(16, 45, 00),
            new TimeSpan(17, 45, 00),
            new TimeSpan(18, 30, 00),
        };

        private readonly List<TimeSpan> _endTimes = new List<TimeSpan>()
        {
            new TimeSpan(8,0, 0),
            new TimeSpan(8, 45, 00),
            new TimeSpan(9, 45, 00),
            new TimeSpan(10, 30, 00),
            new TimeSpan(11, 30, 00),
            new TimeSpan(12, 15, 00),
            new TimeSpan(13, 15, 00),
            new TimeSpan(14, 0, 00),
            new TimeSpan(15, 0, 00),
            new TimeSpan(15, 45, 00),
            new TimeSpan(16, 45, 00),
            new TimeSpan(17, 30, 00),
            new TimeSpan(18, 30, 00),
            new TimeSpan(19, 15, 00),
        };

        private readonly SubjectService _subjectService = new SubjectService();

        private readonly LessonPlanService _lessonPlanService = new LessonPlanService();

        private readonly TeacherService _teacherService = new TeacherService();

        private readonly LessonPlanEntity _lessonPlanEntity = new LessonPlanEntity();

        IEnumerable<TeacherEntity> _teachers;

        private IEnumerable<SubjectEntity> _subjects;
        public EditTimetable(LessonPlanEntity lessonPlan)
        {
            InitializeComponent();
            LoadComboBoxes();
            if (lessonPlan.Id != 0)
            {
                _lessonPlanEntity = lessonPlan;
            }

        }


        private async void LoadComboBoxes()
        {
            _subjects = await _subjectService.GetListOfSubjects();
            _teachers = await _teacherService.GetListOfAllTeachers();
            comboBoxDay.DataSource = Enum.GetNames(typeof(DayOfTheWeekEntity));
            comboBoxStartTime.DataSource = _startTimes;
            comboBoxEndTime.DataSource = _endTimes;
            comboBoxSubject.DataSource = _subjects;
            comboboxTeachers.DataSource = _teachers;
            if (_lessonPlanEntity.Id != 0)
            {
                comboBoxDay.SelectedItem = _lessonPlanEntity.Day;
                comboBoxStartTime.SelectedItem = _lessonPlanEntity.StartTime;
                comboBoxEndTime.SelectedItem = _lessonPlanEntity.EndTime;
                comboBoxSubject.SelectedItem = GetCurrentSubject();
                comboboxTeachers.SelectedItem = GetCurrentTeacher();
            }
        }

        private object GetCurrentSubject()
        {
            return _subjects.FirstOrDefault(subject => _lessonPlanEntity.Subject.Id == subject.Id);
        }

        private TeacherEntity GetCurrentTeacher()
        {
            return _teachers.FirstOrDefault(teacher => _lessonPlanEntity.Teacher.Id == teacher.Id);
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            if (CheckValues())
            {
                if (_lessonPlanEntity.Id == 0)
                {
                    await _lessonPlanService.InsertPlan(GetLessonPlanEntity());
                    GoBack();
                }


            }
            else
            {
                await _lessonPlanService.UpdatePlan(GetLessonPlanEntity());
                GoBack();
            }
        }


        private LessonPlanEntity GetLessonPlanEntity()
        {
            return new LessonPlanEntity()
            {
                Id = _lessonPlanEntity.Id,
                Day = (DayOfTheWeekEntity)Enum.Parse(typeof(DayOfTheWeekEntity),
                    (string)comboBoxDay.SelectedItem),
                StartTime = (TimeSpan)comboBoxStartTime.SelectedItem,
                EndTime = (TimeSpan)comboBoxEndTime.SelectedItem,
                Subject = (SubjectEntity)comboBoxSubject.SelectedItem,
                Teacher = (TeacherEntity)comboboxTeachers.SelectedItem
            };
        }
        private void GoBack()
        {
            AssignSubjectForm form = new AssignSubjectForm();
            form.Show();
            Close();
        }


        private bool CheckValues()
        {
            if ((TimeSpan)comboBoxStartTime.SelectedItem <= (TimeSpan)comboBoxEndTime.SelectedItem) return true;
            MessageBox.Show(@"Lesson cannot end before start!");
            return false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = GetLessonPlanEntity();
                await _lessonPlanService.DeletePlan(entity);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot be deleted");
            }
            GoBack();
        }
    }
}
