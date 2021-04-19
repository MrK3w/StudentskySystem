using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolSystem.ViewLayer
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            Load += BaseFormLoad;
        }

        private void BaseFormLoad(object sender, EventArgs e)
        {
            Size = new Size(0, 0);
            LoginForm form = new LoginForm();
            form.Show();
        }
    }
}
