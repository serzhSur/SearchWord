namespace filesMouver
{
    partial class Form1
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
            textBox1 = new TextBox();
            button1 = new Button();
            listBox1 = new ListBox();
            textBox2_dirIN = new TextBox();
            textBox3_dirOut = new TextBox();
            button2 = new Button();
            button3 = new Button();
            progressBar1 = new ProgressBar();
            button4 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.AcceptsReturn = true;
            textBox1.AcceptsTab = true;
            textBox1.AllowDrop = true;
            textBox1.Location = new Point(718, 295);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(622, 355);
            textBox1.TabIndex = 1;
            textBox1.Text = "View Dastination directory";
            // 
            // button1
            // 
            button1.Location = new Point(994, 663);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Show";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_ViewDestinationDir_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Items.AddRange(new object[] { "View Source directory" });
            listBox1.Location = new Point(718, 57);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(622, 184);
            listBox1.TabIndex = 2;
            // 
            // textBox2_dirIN
            // 
            textBox2_dirIN.Location = new Point(11, 57);
            textBox2_dirIN.Name = "textBox2_dirIN";
            textBox2_dirIN.Size = new Size(622, 27);
            textBox2_dirIN.TabIndex = 3;
            textBox2_dirIN.Text = "textBox2 dirIN C:\\test\\findWord";
            textBox2_dirIN.TextChanged += textBox2_dirIN_TextChanged;
            // 
            // textBox3_dirOut
            // 
            textBox3_dirOut.Location = new Point(11, 149);
            textBox3_dirOut.Name = "textBox3_dirOut";
            textBox3_dirOut.Size = new Size(622, 27);
            textBox3_dirOut.TabIndex = 5;
            textBox3_dirOut.Text = "textBox3 dirOut C:\\test\\findWord\\out";
            textBox3_dirOut.TextChanged += textBox3_dirOut_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(11, 232);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "Copy files";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_CopyFiles_Click;
            // 
            // button3
            // 
            button3.Location = new Point(112, 232);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 7;
            button3.Text = "Copy All";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_copyAllDirectory_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(11, 193);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(619, 24);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 12;
            progressBar1.Value = 50;
            progressBar1.Visible = false;
            // 
            // button4
            // 
            button4.Location = new Point(12, 267);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 13;
            button4.Text = "StartSearch";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(1357, 704);
            Controls.Add(button4);
            Controls.Add(progressBar1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox3_dirOut);
            Controls.Add(textBox2_dirIN);
            Controls.Add(listBox1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Files Mouver";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private ListBox listBox1;
        private TextBox textBox2_dirIN;
        private TextBox textBox3_dirOut;
        private Button button2;
        private Button button3;
        private ProgressBar progressBar1;
        private Button button4;
    }
}
