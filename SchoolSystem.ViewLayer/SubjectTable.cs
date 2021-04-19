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
    public partial class SubjectTable : Form
    {
        private readonly SubjectService _subjectService = new SubjectService();
        private IEnumerable<SubjectEntity> _subjectList = new List<SubjectEntity>();
        public SubjectTable()
        {
            InitializeComponent();
            if (User.Typ == User.Type.Student || !User.Exists())
            {
                addSubjectButton.Hide();
            }

            if (User.Typ == User.Type.Student)
            {
                addSubjectButton.Hide();
            }
            Task.Run(async () => await GetData()).Wait();
        }

        private async Task GetData()
        {
            _subjectList = await _subjectService.GetListOfSubjects();
            table.DataSource = (from subject in _subjectList
                                orderby subject.Credits,subject.Name
                select new { subject.Id, subject.Name, subject.Credits,
                    TeacherName = subject.HeadTeacher.FirstName + " " + subject.HeadTeacher.LastName
                }).ToList();
            table.Columns[0].HeaderText = @"ID";
            table.Columns[0].Visible = false;
            table.Columns[1].HeaderText = @"Name";
            table.Columns[2].HeaderText = @"Credits";
            table.Columns[3].HeaderText = @"HeadTeacher's full name";
        }

        private void addSubjectButton_Click(object sender, EventArgs e)
        {
            OpenForm(new SubjectEntity());
        }

        private void table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || User.Typ == User.Type.Student || !User.Exists())
            {
                return;
            }
            int id = (int)table.Rows[e.RowIndex].Cells["ID"].Value;
            var subject = GetSubject(id);
            OpenForm(subject);
        }

        private void OpenForm(SubjectEntity subject)
        {
            EditSubject form = new EditSubject(subject);
            form.Show();
            Close();
        }

        private SubjectEntity GetSubject(int id)
        {
            SubjectEntity subject = new SubjectEntity();
            foreach (var sub in _subjectList)
            {
                if (sub.Id == id)
                {
                    subject = sub;
                }
            }

            return subject;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.Show();
            Close();
        }
    }
}
