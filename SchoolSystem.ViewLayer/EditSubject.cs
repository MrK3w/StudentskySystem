using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SchoolSystem.BusinessLayer.Services;
using SchoolSystem.DataLayer.Entities;

namespace SchoolSystem.ViewLayer
{
    public partial class EditSubject : Form
    {
        private IEnumerable<TeacherEntity> _teacherList;
        private readonly SubjectEntity _subject;
        private readonly TeacherService _teacherService = new TeacherService();
        private readonly SubjectService _subjectService = new SubjectService();

        public EditSubject(SubjectEntity subject)
        {
            InitializeComponent();
            _subject = subject;
            Task.Run(async () => await PrepareForm()).Wait();
        }


        private async Task PrepareForm()
        {
            _teacherList = await _teacherService.GetListOfAllTeachers();
            SubjectNameBox.Text = _subject.Name;
            NumericCredits.Value = _subject.Credits;
            teacherComboBox.DataSource = _teacherList;
            if (_subject.Id !=0) teacherComboBox.SelectedItem = GetCurrent(); 
        }

        private TeacherEntity GetCurrent()
        {
            return _teacherList.FirstOrDefault(teacher => _subject.HeadTeacher.Id== teacher.Id);
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
            SubjectTable form = new SubjectTable();
            form.Show();
        }

        private async void Confirm_Click(object sender, EventArgs e)
        {
            GetValues();
            if (_subject.Id == 0)
            {
                try
                {
                    await _subjectService.CreateSubject(_subject);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Cannot be inserted!");
                }
                
            }
            else
            {
                try
                {
                    await _subjectService.UpdateSubject(_subject);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Cannot be updated");
                }
                
            }
                
            Close();
            SubjectTable form = new SubjectTable();
            form.Show();
        }

        private void GetValues()
        {
            _subject.HeadTeacher = (TeacherEntity)teacherComboBox.SelectedValue;
            _subject.Name = SubjectNameBox.Text;
            _subject.Credits = (int) NumericCredits.Value;
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            GetValues();
            try
            {
                await _subjectService.DeleteSubject(_subject);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Subject is already in some plan!");
            }
            Close();
            var form = new SubjectTable();
            form.Show();
        }
    }
}
