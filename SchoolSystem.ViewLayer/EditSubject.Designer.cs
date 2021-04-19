
namespace SchoolSystem.ViewLayer
{
    partial class EditSubject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSubject));
            this.mainbar1 = new SchoolSystem.ViewLayer.Mainbar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Confirm = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.SubjectNameBox = new System.Windows.Forms.TextBox();
            this.NumericCredits = new System.Windows.Forms.NumericUpDown();
            this.teacherComboBox = new System.Windows.Forms.ComboBox();
            this.line = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCredits)).BeginInit();
            this.line.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainbar1
            // 
            this.mainbar1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainbar1.Location = new System.Drawing.Point(-2, -2);
            this.mainbar1.Name = "mainbar1";
            this.mainbar1.Size = new System.Drawing.Size(1004, 112);
            this.mainbar1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(855, 129);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Red;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.deleteButton.Location = new System.Drawing.Point(751, 313);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(183, 77);
            this.deleteButton.TabIndex = 26;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(245, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 45);
            this.label2.TabIndex = 22;
            this.label2.Text = "HeadTeacher";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(255, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 45);
            this.label1.TabIndex = 20;
            this.label1.Text = "Credits";
            // 
            // Confirm
            // 
            this.Confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Confirm.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Confirm.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Confirm.Location = new System.Drawing.Point(751, 209);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(183, 77);
            this.Confirm.TabIndex = 19;
            this.Confirm.Text = "CONFIRM";
            this.Confirm.UseVisualStyleBackColor = false;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name.Location = new System.Drawing.Point(255, 191);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(127, 45);
            this.name.TabIndex = 18;
            this.name.Text = "Subject";
            // 
            // SubjectNameBox
            // 
            this.SubjectNameBox.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SubjectNameBox.Location = new System.Drawing.Point(457, 194);
            this.SubjectNameBox.Name = "SubjectNameBox";
            this.SubjectNameBox.Size = new System.Drawing.Size(237, 43);
            this.SubjectNameBox.TabIndex = 17;
            // 
            // NumericCredits
            // 
            this.NumericCredits.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericCredits.Location = new System.Drawing.Point(457, 268);
            this.NumericCredits.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumericCredits.Name = "NumericCredits";
            this.NumericCredits.Size = new System.Drawing.Size(237, 38);
            this.NumericCredits.TabIndex = 28;
            // 
            // teacherComboBox
            // 
            this.teacherComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teacherComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.teacherComboBox.FormattingEnabled = true;
            this.teacherComboBox.Location = new System.Drawing.Point(457, 362);
            this.teacherComboBox.Name = "teacherComboBox";
            this.teacherComboBox.Size = new System.Drawing.Size(237, 28);
            this.teacherComboBox.TabIndex = 30;
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(167)))), ((int)(((byte)(155)))));
            this.line.Controls.Add(this.panel1);
            this.line.Location = new System.Drawing.Point(2, 105);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(1000, 5);
            this.line.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(981, 114);
            this.panel1.TabIndex = 14;
            // 
            // EditSubject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 562);
            this.Controls.Add(this.line);
            this.Controls.Add(this.teacherComboBox);
            this.Controls.Add(this.NumericCredits);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.name);
            this.Controls.Add(this.SubjectNameBox);
            this.Controls.Add(this.mainbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditSubject";
            this.Text = "EditSubject";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCredits)).EndInit();
            this.line.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mainbar mainbar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.TextBox SubjectNameBox;
        private System.Windows.Forms.NumericUpDown NumericCredits;
        private System.Windows.Forms.ComboBox teacherComboBox;
        private System.Windows.Forms.Panel line;
        private System.Windows.Forms.Panel panel1;
    }
}