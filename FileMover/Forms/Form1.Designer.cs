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
            components = new System.ComponentModel.Container();
            textBox2_dirIn = new TextBox();
            textBox3_dirOut = new TextBox();
            button2 = new Button();
            progressBar1 = new ProgressBar();
            button4 = new Button();
            button5 = new Button();
            textBox_log = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox_pathWords = new TextBox();
            progressBar2 = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // textBox2_dirIn
            // 
            textBox2_dirIn.Location = new Point(14, 72);
            textBox2_dirIn.Name = "textBox2_dirIn";
            textBox2_dirIn.Size = new Size(622, 27);
            textBox2_dirIn.TabIndex = 3;
            textBox2_dirIn.Text = "d:\\temp\\in\\";
            // 
            // textBox3_dirOut
            // 
            textBox3_dirOut.Location = new Point(14, 149);
            textBox3_dirOut.Name = "textBox3_dirOut";
            textBox3_dirOut.Size = new Size(620, 27);
            textBox3_dirOut.TabIndex = 5;
            textBox3_dirOut.Text = "d:\\temp\\out\\";
            // 
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(11, 399);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(625, 16);
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 12;
            // 
            // button4
            // 
            button4.Location = new Point(11, 441);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 13;
            button4.Text = "Start";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4Search_Click;
            // 
            // button5
            // 
            button5.Location = new Point(123, 441);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
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
            textBox_log.Location = new Point(11, 272);
            textBox_log.Multiline = true;
            textBox_log.Name = "textBox_log";
            textBox_log.RightToLeft = RightToLeft.No;
            textBox_log.ScrollBars = ScrollBars.Vertical;
            textBox_log.Size = new Size(622, 120);
            textBox_log.TabIndex = 15;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Silver;
            textBox1.Location = new Point(14, 36);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(622, 27);
            textBox1.TabIndex = 16;
            textBox1.Text = "Входная директория";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Silver;
            textBox2.Location = new Point(14, 113);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(622, 27);
            textBox2.TabIndex = 17;
            textBox2.Text = "Выходная директория";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.Silver;
            textBox3.Location = new Point(11, 187);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(622, 27);
            textBox3.TabIndex = 18;
            textBox3.Text = "Облако слов";
            // 
            // textBox_pathWords
            // 
            textBox_pathWords.Location = new Point(11, 223);
            textBox_pathWords.Name = "textBox_pathWords";
            textBox_pathWords.Size = new Size(620, 27);
            textBox_pathWords.TabIndex = 19;
            textBox_pathWords.Text = "Words\\Words.txt";
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(11, 421);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(625, 15);
            progressBar2.Style = ProgressBarStyle.Continuous;
            progressBar2.TabIndex = 20;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(646, 485);
            Controls.Add(progressBar2);
            Controls.Add(textBox_pathWords);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(textBox_log);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(progressBar1);
            Controls.Add(button2);
            Controls.Add(textBox3_dirOut);
            Controls.Add(textBox2_dirIn);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поиск слов в файлах";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox2_dirIn;
        private TextBox textBox3_dirOut;
        private Button button2;
        private ProgressBar progressBar1;
        private Button button4;
        private Button button5;
        private TextBox textBox_log;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox_pathWords;
        private ProgressBar progressBar2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}
