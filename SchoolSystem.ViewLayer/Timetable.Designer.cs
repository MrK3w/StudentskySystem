
namespace SchoolSystem.ViewLayer
{
    partial class Timetable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Timetable));
            this.mainbar1 = new SchoolSystem.ViewLayer.Mainbar();
            this.line = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timeTables = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newMenu = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.line.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainbar1
            // 
            this.mainbar1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainbar1.Location = new System.Drawing.Point(-3, -2);
            this.mainbar1.Name = "mainbar1";
            this.mainbar1.Size = new System.Drawing.Size(1250, 112);
            this.mainbar1.TabIndex = 0;
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.line.Controls.Add(this.panel1);
            this.line.Location = new System.Drawing.Point(-3, 105);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(1000, 5);
            this.line.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(981, 114);
            this.panel1.TabIndex = 14;
            // 
            // timeTables
            // 
            this.timeTables.AllowUserToAddRows = false;
            this.timeTables.AllowUserToDeleteRows = false;
            this.timeTables.AllowUserToOrderColumns = true;
            this.timeTables.AllowUserToResizeColumns = false;
            this.timeTables.AllowUserToResizeRows = false;
            this.timeTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.timeTables.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.timeTables.BackgroundColor = System.Drawing.SystemColors.Control;
            this.timeTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeTables.Location = new System.Drawing.Point(167, 236);
            this.timeTables.Name = "timeTables";
            this.timeTables.ReadOnly = true;
            this.timeTables.RowHeadersWidth = 51;
            this.timeTables.RowTemplate.Height = 29;
            this.timeTables.Size = new System.Drawing.Size(803, 355);
            this.timeTables.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 140);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.label1.Location = new System.Drawing.Point(605, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 31);
            this.label1.TabIndex = 19;
            this.label1.Text = "Assigned subjects";
            // 
            // newMenu
            // 
            this.newMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.newMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newMenu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.newMenu.Location = new System.Drawing.Point(12, 346);
            this.newMenu.Name = "newMenu";
            this.newMenu.Size = new System.Drawing.Size(149, 100);
            this.newMenu.TabIndex = 20;
            this.newMenu.Text = "Asign subject";
            this.newMenu.UseVisualStyleBackColor = false;
            this.newMenu.Click += new System.EventHandler(this.newMenu_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(167, 197);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(189, 28);
            this.comboBox.TabIndex = 22;
            this.comboBox.Visible = false;
            this.comboBox.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.label2.Location = new System.Drawing.Point(362, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 31);
            this.label2.TabIndex = 23;
            this.label2.Text = "Students";
            this.label2.Visible = false;
            // 
            // Timetable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 649);
            this.Controls.Add(this.newMenu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.timeTables);
            this.Controls.Add(this.line);
            this.Controls.Add(this.mainbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Timetable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timetable";
            this.line.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mainbar mainbar1;
        private System.Windows.Forms.Panel line;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView timeTables;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newMenu;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label2;
    }
}