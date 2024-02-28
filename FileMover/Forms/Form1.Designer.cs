namespace FilesMouver
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
            button1 = new Button();
            textBox2_dirIn = new TextBox();
            textBox3_dirOut = new TextBox();
            button2 = new Button();
            button3 = new Button();
            progressBar1 = new ProgressBar();
            button4 = new Button();
            button5 = new Button();
            textBox_log = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox_pathWords = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(473, 165);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(82, 22);
            button1.TabIndex = 0;
            button1.Text = "Show";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1Show_Click;
            // 
            // textBox2_dirIn
            // 
            textBox2_dirIn.Location = new Point(12, 54);
            textBox2_dirIn.Margin = new Padding(3, 2, 3, 2);
            textBox2_dirIn.Name = "textBox2_dirIn";
            textBox2_dirIn.Size = new Size(545, 23);
            textBox2_dirIn.TabIndex = 3;
            textBox2_dirIn.Text = "d:\\temp\\in\\";
            // 
            // textBox3_dirOut
            // 
            textBox3_dirOut.Location = new Point(12, 112);
            textBox3_dirOut.Margin = new Padding(3, 2, 3, 2);
            textBox3_dirOut.Name = "textBox3_dirOut";
            textBox3_dirOut.Size = new Size(543, 23);
            textBox3_dirOut.TabIndex = 5;
            textBox3_dirOut.Text = "d:\\temp\\out\\";
            // 
            // button2
            // 
            button2.Location = new Point(338, 139);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(82, 22);
            button2.TabIndex = 6;
            button2.Text = "Copy files";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2CopyFiles_Click;
            // 
            // button3
            // 
            button3.Location = new Point(459, 139);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(82, 22);
            button3.TabIndex = 7;
            button3.Text = "Copy All";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3CopyAllDirectory_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(10, 299);
            progressBar1.Margin = new Padding(3, 2, 3, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(547, 18);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 12;
            // 
            // button4
            // 
            button4.Location = new Point(10, 331);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(82, 22);
            button4.TabIndex = 13;
            button4.Text = "Start";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4Search_Click;
            // 
            // button5
            // 
            button5.Location = new Point(108, 331);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(82, 22);
            button5.TabIndex = 14;
            button5.Text = "Stop";
            button5.UseVisualStyleBackColor = true;
            // 
            // textBox_log
            // 
            textBox_log.AcceptsReturn = true;
            textBox_log.AcceptsTab = true;
            textBox_log.AllowDrop = true;
            textBox_log.BackColor = Color.White;
            textBox_log.Location = new Point(10, 204);
            textBox_log.Margin = new Padding(3, 2, 3, 2);
            textBox_log.Multiline = true;
            textBox_log.Name = "textBox_log";
            textBox_log.RightToLeft = RightToLeft.No;
            textBox_log.ScrollBars = ScrollBars.Vertical;
            textBox_log.Size = new Size(545, 91);
            textBox_log.TabIndex = 15;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Silver;
            textBox1.Location = new Point(12, 27);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(545, 23);
            textBox1.TabIndex = 16;
            textBox1.Text = "Входная директория";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Silver;
            textBox2.Location = new Point(12, 85);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(545, 23);
            textBox2.TabIndex = 17;
            textBox2.Text = "Выходная директория";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.Silver;
            textBox3.Location = new Point(10, 140);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(545, 23);
            textBox3.TabIndex = 18;
            textBox3.Text = "Облако слов";
            // 
            // textBox_pathWords
            // 
            textBox_pathWords.Location = new Point(10, 167);
            textBox_pathWords.Margin = new Padding(3, 2, 3, 2);
            textBox_pathWords.Name = "textBox_pathWords";
            textBox_pathWords.Size = new Size(543, 23);
            textBox_pathWords.TabIndex = 19;
            textBox_pathWords.Text = "Words\\Words.txt";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(565, 364);
            Controls.Add(textBox_pathWords);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox_log);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(progressBar1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox3_dirOut);
            Controls.Add(textBox2_dirIn);
            Controls.Add(button1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поиск слов в файлах";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox2_dirIn;
        private TextBox textBox3_dirOut;
        private Button button2;
        private Button button3;
        private ProgressBar progressBar1;
        private Button button4;
        private Button button5;
        private TextBox textBox_log;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox_pathWords;
    }
}
