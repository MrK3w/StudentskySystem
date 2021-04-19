using System;
using System.Windows.Forms;

namespace SchoolSystem.ViewLayer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UserTable(object sender, EventArgs e)
        {
            UserTable userTable = new UserTable();
            userTable.Show();
            Close();
        }

        private void SubjectTable(object sender, EventArgs e)
        {
            SubjectTable subject = new SubjectTable();
            subject.Show();
            Close();
        }

        private void TimetableForm(object sender, EventArgs e)
        {
            Timetable table = new Timetable();
            table.Show();
            Close();
        }
    }
}
