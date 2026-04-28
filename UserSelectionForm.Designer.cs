namespace MyBrowser
{
    partial class UserSelectionForm
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
            userComboBox = new ComboBox();
            selectUserbtn = new Button();
            createUserbtn = new Button();
            label1 = new Label();
            label2 = new Label();
            deleteUserbtn = new Button();
            //SuspendLayout();
            // 
            // userComboBox
            // 
            userComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            userComboBox.FormattingEnabled = true;
            userComboBox.Location = new Point(100, 250);
            userComboBox.Name = "userComboBox";
            userComboBox.Size = new Size(200, 28);
            userComboBox.TabIndex = 0;
            // 
            // selectUserbtn
            // 
            selectUserbtn.BackColor = Color.FromArgb(192, 255, 192);
            selectUserbtn.FlatStyle = FlatStyle.Flat;
            selectUserbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            selectUserbtn.Location = new Point(320, 250);
            selectUserbtn.Name = "selectUserbtn";
            selectUserbtn.Size = new Size(109, 29);
            selectUserbtn.TabIndex = 1;
            selectUserbtn.Text = "Select User";
            selectUserbtn.UseVisualStyleBackColor = false;
            selectUserbtn.Click += selectUserbtn_Click;
            // 
            // createUserbtn
            // 
            createUserbtn.BackColor = Color.FromArgb(192, 255, 255);
            createUserbtn.FlatStyle = FlatStyle.Flat;
            createUserbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            createUserbtn.Location = new Point(500, 250);
            createUserbtn.Name = "createUserbtn";
            createUserbtn.Size = new Size(130, 29);
            createUserbtn.TabIndex = 2;
            createUserbtn.Text = "Create Account";
            createUserbtn.UseVisualStyleBackColor = false;
            createUserbtn.Click += createUserbtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 212);
            label1.Name = "label1";
            label1.Size = new Size(166, 20);
            label1.TabIndex = 3;
            label1.Text = "Select a user to login as";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(500, 212);
            label2.Name = "label2";
            label2.Size = new Size(182, 20);
            label2.TabIndex = 4;
            label2.Text = "Create a new user account";
            // 
            // deleteUserbtn
            // 
            deleteUserbtn.BackColor = Color.FromArgb(255, 128, 128);
            deleteUserbtn.FlatStyle = FlatStyle.Flat;
            deleteUserbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deleteUserbtn.ForeColor = SystemColors.Desktop;
            deleteUserbtn.Location = new Point(320, 317);
            deleteUserbtn.Name = "deleteUserbtn";
            deleteUserbtn.Size = new Size(109, 29);
            deleteUserbtn.TabIndex = 5;
            deleteUserbtn.Text = "Delete User";
            deleteUserbtn.UseVisualStyleBackColor = false;
            deleteUserbtn.Click += deleteUserbtn_Click;
            // 
            // UserSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 553);
            Controls.Add(deleteUserbtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(createUserbtn);
            Controls.Add(selectUserbtn);
            Controls.Add(userComboBox);
            Name = "UserSelectionForm";
            Text = "UserSelectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox userComboBox;
        private Button selectUserbtn;
        private Button createUserbtn;
        private Label label1;
        private Label label2;
        private Button deleteUserbtn;
    }
}