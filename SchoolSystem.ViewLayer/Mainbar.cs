using System;
using System.Drawing;
using System.Windows.Forms;
using SchoolSystem.ViewLayer.Helpers;

namespace SchoolSystem.ViewLayer
{
    public partial class Mainbar : UserControl
    {
        private static bool _mouseDown;
        private static Point _lastLocation;

        public Mainbar()
        {
            InitializeComponent();
            if (!User.Exists()) maintext.Text = @"Not logged";
            else
            {
                maintext.Text = User.Typ == User.Type.Admin ? "Admin" : $"{User.Typ}:   {User.Name} {User.Surname}({User.Login})";
                RepositionLabel(maintext, 815);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void RepositionLabel(Label lab, int endPoint)
        {
            lab.Left = endPoint - lab.Width;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ((Form)((PictureBox) sender).TopLevelControl)?.Close();
            MainForm form = new MainForm();
            form.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            ((Form)((Button)sender).TopLevelControl)?.Close();
            User.Forget();
            LoginForm form = new LoginForm();
            form.Show();
        }

        private void Mainbar_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastLocation = e.Location;
        }

        private void Mainbar_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void Mainbar_MouseMove(object sender, MouseEventArgs e)
        {
            var form =(Form)((Mainbar) sender).Parent;
            if (_mouseDown)
            {
                form.Location = new Point((form.Location.X - _lastLocation.X) + e.X, (form.Location.Y - _lastLocation.Y) + e.Y);
            }
        }
    }
}
