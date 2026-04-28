namespace MyBrowser
{
    partial class Browser
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            search_btn = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            reload_btn = new Button();
            setHomepage_btn = new Button();
            BookmarksList = new ListBox();
            addBookmark_btn = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            editBookmarkToolStripMenuItem = new ToolStripMenuItem();
            deleteBookmarkToolStripMenuItem = new ToolStripMenuItem();
            HistoryList = new ListBox();
            label2 = new Label();
            label3 = new Label();
            BackNav_btn = new Button();
            ForwardNav_btn = new Button();
            LinksList = new ListBox();
            label4 = new Label();
            title_label = new Label();
            userLabel = new Label();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // search_btn
            // 
            search_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            search_btn.BackColor = SystemColors.Control;
            search_btn.Cursor = Cursors.Hand;
            search_btn.FlatStyle = FlatStyle.System;
            search_btn.ForeColor = SystemColors.ButtonFace;
            search_btn.Location = new Point(936, 12);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(69, 27);
            search_btn.TabIndex = 0;
            search_btn.Text = "Search";
            search_btn.UseVisualStyleBackColor = false;
            search_btn.Click += search_btn_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.Window;
            textBox1.Location = new Point(152, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(778, 27);
            textBox1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.Black;
            richTextBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.ForeColor = Color.LemonChiffon;
            richTextBox1.Location = new Point(12, 116);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(993, 545);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // reload_btn
            // 
            reload_btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reload_btn.ForeColor = Color.Black;
            reload_btn.Location = new Point(106, 12);
            reload_btn.Name = "reload_btn";
            reload_btn.Size = new Size(40, 27);
            reload_btn.TabIndex = 4;
            reload_btn.Text = "⟳";
            reload_btn.UseVisualStyleBackColor = true;
            reload_btn.Click += reload_btn_Click;
            // 
            // setHomepage_btn
            // 
            setHomepage_btn.ForeColor = Color.Black;
            setHomepage_btn.Location = new Point(14, 45);
            setHomepage_btn.Name = "setHomepage_btn";
            setHomepage_btn.Size = new Size(162, 29);
            setHomepage_btn.TabIndex = 5;
            setHomepage_btn.Text = "Save as home page";
            setHomepage_btn.UseVisualStyleBackColor = true;
            setHomepage_btn.Click += setHomepage_btn_Click;
            // 
            // BookmarksList
            // 
            BookmarksList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BookmarksList.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BookmarksList.FormattingEnabled = true;
            BookmarksList.Location = new Point(1011, 42);
            BookmarksList.Name = "BookmarksList";
            BookmarksList.Size = new Size(239, 184);
            BookmarksList.TabIndex = 6;
            BookmarksList.MouseDoubleClick += BookmarksList_MouseDoubleClick;
            // 
            // addBookmark_btn
            // 
            addBookmark_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addBookmark_btn.ForeColor = Color.Black;
            addBookmark_btn.Location = new Point(887, 45);
            addBookmark_btn.Name = "addBookmark_btn";
            addBookmark_btn.Size = new Size(118, 29);
            addBookmark_btn.TabIndex = 7;
            addBookmark_btn.Text = "Add bookmark";
            addBookmark_btn.UseVisualStyleBackColor = true;
            addBookmark_btn.Click += addBookmark_btn_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { editBookmarkToolStripMenuItem, deleteBookmarkToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(194, 52);
            // 
            // editBookmarkToolStripMenuItem
            // 
            editBookmarkToolStripMenuItem.Name = "editBookmarkToolStripMenuItem";
            editBookmarkToolStripMenuItem.Size = new Size(193, 24);
            editBookmarkToolStripMenuItem.Text = "Edit Bookmark";
            // 
            // deleteBookmarkToolStripMenuItem
            // 
            deleteBookmarkToolStripMenuItem.Name = "deleteBookmarkToolStripMenuItem";
            deleteBookmarkToolStripMenuItem.Size = new Size(193, 24);
            deleteBookmarkToolStripMenuItem.Text = "Delete Bookmark";
            // 
            // HistoryList
            // 
            HistoryList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            HistoryList.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HistoryList.FormattingEnabled = true;
            HistoryList.Location = new Point(1011, 417);
            HistoryList.Name = "HistoryList";
            HistoryList.Size = new Size(239, 244);
            HistoryList.TabIndex = 8;
            HistoryList.MouseDoubleClick += HistoryList_MouseDoubleClick;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(1011, 16);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 9;
            label2.Text = "Bookmarks";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(1011, 392);
            label3.Name = "label3";
            label3.Size = new Size(69, 23);
            label3.TabIndex = 10;
            label3.Text = "History";
            // 
            // BackNav_btn
            // 
            BackNav_btn.BackColor = Color.White;
            BackNav_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BackNav_btn.ForeColor = Color.Black;
            BackNav_btn.Location = new Point(14, 12);
            BackNav_btn.Name = "BackNav_btn";
            BackNav_btn.Size = new Size(40, 27);
            BackNav_btn.TabIndex = 11;
            BackNav_btn.Text = "←";
            BackNav_btn.UseVisualStyleBackColor = false;
            BackNav_btn.Click += BackNav_btn_Click;
            // 
            // ForwardNav_btn
            // 
            ForwardNav_btn.BackColor = Color.White;
            ForwardNav_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForwardNav_btn.ForeColor = Color.Black;
            ForwardNav_btn.Location = new Point(60, 12);
            ForwardNav_btn.Name = "ForwardNav_btn";
            ForwardNav_btn.Size = new Size(40, 27);
            ForwardNav_btn.TabIndex = 12;
            ForwardNav_btn.Text = "→";
            ForwardNav_btn.UseVisualStyleBackColor = false;
            ForwardNav_btn.Click += ForwardNav_btn_Click;
            // 
            // LinksList
            // 
            LinksList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LinksList.FormattingEnabled = true;
            LinksList.Location = new Point(1011, 268);
            LinksList.Name = "LinksList";
            LinksList.Size = new Size(239, 104);
            LinksList.TabIndex = 13;
            LinksList.MouseDoubleClick += LinksList_MouseDoubleClick;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(1011, 242);
            label4.Name = "label4";
            label4.Size = new Size(51, 23);
            label4.TabIndex = 14;
            label4.Text = "Links";
            // 
            // title_label
            // 
            title_label.AutoSize = true;
            title_label.BackColor = SystemColors.Info;
            title_label.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title_label.ForeColor = Color.Black;
            title_label.Location = new Point(12, 90);
            title_label.Name = "title_label";
            title_label.Size = new Size(0, 23);
            title_label.TabIndex = 15;
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            userLabel.ForeColor = Color.Black;
            userLabel.Location = new Point(182, 49);
            userLabel.Name = "userLabel";
            userLabel.Size = new Size(46, 20);
            userLabel.TabIndex = 16;
            userLabel.Text = "User:";
            // 
            // Browser
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1262, 673);
            Controls.Add(userLabel);
            Controls.Add(title_label);
            Controls.Add(label4);
            Controls.Add(LinksList);
            Controls.Add(ForwardNav_btn);
            Controls.Add(BackNav_btn);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(HistoryList);
            Controls.Add(addBookmark_btn);
            Controls.Add(BookmarksList);
            Controls.Add(setHomepage_btn);
            Controls.Add(reload_btn);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(search_btn);
            ForeColor = Color.White;
            Name = "Browser";
            Text = "Form1";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button search_btn;
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private Button reload_btn;
        private Button setHomepage_btn;
        private ListBox BookmarksList;
        private Button addBookmark_btn;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem editBookmarkToolStripMenuItem;
        private ToolStripMenuItem deleteBookmarkToolStripMenuItem;
        private ListBox HistoryList;
        private Label label2;
        private Label label3;
        private Button BackNav_btn;
        private Button ForwardNav_btn;
        private ListBox LinksList;
        private Label label4;
        private Label title_label;
        private Label userLabel;
    }
}
