using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FilesMove.Classes;
using FileMover.Classes;
using System.Diagnostics;


namespace FilesMouver
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts = null;// new CancellationTokenSource();

        internal AnalizFile Analizator { get; private set; }

        public Form1()
        {
            InitializeComponent();

        }

        
        private void CopyDirectoryAll()
        {
            string dirIN = textBox2_dirIn.Text;
            string dirOut = textBox3_dirOut.Text;

            perebor_updates(dirIN, dirOut);

            // ����������� ����� ������� �������� ���� � ����������
            void perebor_updates(string begin_dir, string end_dir)
            {
                //���� ���� �������� �����
                DirectoryInfo dir_inf = new DirectoryInfo(begin_dir);
                try
                {
                    //���������� ��� ���������� �����
                    foreach (DirectoryInfo dir in dir_inf.GetDirectories())
                    {
                        //��������� - ���� ���������� �� ����������, �� ������;
                        if (Directory.Exists(end_dir + "\\" + dir.Name) != true)
                        {
                            Directory.CreateDirectory(end_dir + "\\" + dir.Name);
                        }

                        //�������� (���������� ��������� ����� � ������ ��� ��� ��-�� �����).
                        perebor_updates(dir.FullName, end_dir + "\\" + dir.Name);
                    }

                    //���������� ������� � ����� ���������.
                    foreach (string file in Directory.GetFiles(begin_dir))
                    {
                        //���������� (��������) ��� ����� � ����������� - ��� ���� (�� � ������ "\").
                        string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                        //�������� ������ � ����������� �� ��������� � �������.
                        File.Copy(file, end_dir + "\\" + filik, true);
                    }

                    textBox1.Text = "������� �� ����� ���������� ����������.";
                }
                catch
                {
                    MessageBox.Show("����������� ����");
                }

            }
        }

        private async void button4Search_Click(object sender, EventArgs e)
        {
            string dirIn = textBox2_dirIn.Text;
            string slovoPath = textBox_pathWords.Text;
            string dirOut = textBox3_dirOut.Text;

            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Stopwatch stopwatch = new Stopwatch();//�������� ����� ���������� ������ Analizator
            stopwatch.Start();

            Analizator = new AnalizFile(dirIn, slovoPath, dirOut);
            var processAnalizator = Analizator.SerchInDirectoryAsync(token);
            //��������� �������� � ��������� ���� ����������� ������� Analizator.SerchInDirectoryAsync �� ������ await;
           
            textBox_log.BackColor = Color.White;
            textBox_log.Text = $"{Analizator.Status}";
            timer1.Enabled = true;
            

            await processAnalizator;
            
            stopwatch.Stop();
            string executionTime = stopwatch.Elapsed.TotalSeconds.ToString();
            
            timer1.Enabled = false;
            progressBar1.Value = progressBar1.Maximum;

            if (Analizator.ErrMessage.Length == 0)
            {
                var DbManager = await PostgreSqlManager.CreateObjectAsync();
                var MatchCount = await DbManager.GetMatchCountAsync();//������ �� ���������� ����� �� ��������� ����

                textBox_log.Text = $"{Analizator.Status}\r\n����� ����������: {executionTime} ���\r\n���������� ����������: {MatchCount}";

                var FindedWordsCount = await DbManager.GetFindedWordsCount();//������ �� ���������� ����� � ������ ���������� ��������� 
                foreach (var word in FindedWordsCount)
                {
                    textBox_log.Text += "\r\n�����: " + word.ToString() + " ���";
                }
                if (DbManager.ErrorsMessage.Length > 0)
                {
                    textBox_log.BackColor = Color.LightCoral;
                    textBox_log.Text = $"class PostgreSqlManager { DbManager.ErrorsMessage}";
                }

                DbManager.CloseConnection();
            }

            if (Analizator.ErrMessage.Length > 0)
            {
                textBox_log.BackColor = Color.LightCoral;
                textBox_log.Text = Analizator.ErrMessage;
            }

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = Analizator.CountFiles;
            progressBar1.Value = Analizator.Position;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cts.Cancel();
                cts.Dispose();
            }
            catch (Exception ex)
            {
                textBox_log.Text = ex.Message;

            }


        }
    }
}
