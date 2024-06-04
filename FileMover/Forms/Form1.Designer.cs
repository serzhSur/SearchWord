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
            progressBar1 = new ProgressBar();
            button4 = new Button();
            button5 = new Button();
            textBox_log = new TextBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox_pathWords = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            textBox4 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox2_dirIn
            // 
            textBox2_dirIn.Location = new Point(6, 39);
            textBox2_dirIn.Name = "textBox2_dirIn";
            textBox2_dirIn.Size = new Size(622, 27);
            textBox2_dirIn.TabIndex = 3;
            textBox2_dirIn.Text = "d:\\temp\\in\\";
            // 
            // textBox3_dirOut
            // 
            textBox3_dirOut.Location = new Point(6, 105);
            textBox3_dirOut.Name = "textBox3_dirOut";
            textBox3_dirOut.Size = new Size(622, 27);
            textBox3_dirOut.TabIndex = 5;
            textBox3_dirOut.Text = "d:\\temp\\out\\";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(6, 204);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(622, 31);
            progressBar1.Step = 1;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 12;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Location = new Point(6, 405);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 13;
            button4.Text = "Start";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4Search_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaption;
            button5.Location = new Point(141, 405);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 14;
            button5.Text = "Stop";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // textBox_log
            // 
            textBox_log.AcceptsReturn = true;
            textBox_log.AcceptsTab = true;
            textBox_log.AllowDrop = true;
            textBox_log.BackColor = Color.White;
            textBox_log.Location = new Point(6, 241);
            textBox_log.Multiline = true;
            textBox_log.Name = "textBox_log";
            textBox_log.RightToLeft = RightToLeft.No;
            textBox_log.ScrollBars = ScrollBars.Vertical;
            textBox_log.Size = new Size(622, 145);
            textBox_log.TabIndex = 15;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Silver;
            textBox1.Location = new Point(6, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(622, 27);
            textBox1.TabIndex = 16;
            textBox1.Text = "Входная директория";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Silver;
            textBox2.Location = new Point(6, 72);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(622, 27);
            textBox2.TabIndex = 17;
            textBox2.Text = "Выходная директория";
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.Silver;
            textBox3.Location = new Point(6, 138);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(622, 27);
            textBox3.TabIndex = 18;
            textBox3.Text = "Список слов";
            // 
            // textBox_pathWords
            // 
            textBox_pathWords.Location = new Point(6, 171);
            textBox_pathWords.Name = "textBox_pathWords";
            textBox_pathWords.Size = new Size(622, 27);
            textBox_pathWords.TabIndex = 19;
            textBox_pathWords.Text = "d:\\temp\\Words\\Words.txt";
            // 
            // timer1
            // 
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 155);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(629, 332);
            dataGridView1.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 490);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 22;
            label1.Text = "Всего запросов: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(268, 490);
            label2.Name = "label2";
            label2.Size = new Size(116, 20);
            label2.TabIndex = 23;
            label2.Text = "Всего записей: ";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(646, 547);
            tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.ControlDark;
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(textBox_log);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(textBox_pathWords);
            tabPage1.Controls.Add(textBox2_dirIn);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Controls.Add(progressBar1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(textBox3_dirOut);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(638, 514);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Search";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.DarkGray;
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(638, 514);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Analiz";
            // 
            // textBox4
            // 
            textBox4.AcceptsReturn = true;
            textBox4.AcceptsTab = true;
            textBox4.AllowDrop = true;
            textBox4.BackColor = SystemColors.Window;
            textBox4.Location = new Point(3, 6);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ScrollBars = ScrollBars.Vertical;
            textBox4.Size = new Size(629, 143);
            textBox4.TabIndex = 24;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(654, 550);
            Controls.Add(tabControl1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Поиск слов в файлах";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox textBox2_dirIn;
        private TextBox textBox3_dirOut;
        private ProgressBar progressBar1;
        private Button button4;
        private Button button5;
        private TextBox textBox_log;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox_pathWords;
        private System.Windows.Forms.Timer timer1;
        public DataGridView dataGridView1;
        public Label label1;
        public Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox4;
    }
}
