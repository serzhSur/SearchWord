using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FilesMove.Classes;
using FileMover.Classes;
using System.Diagnostics;
using FileMover;



namespace FilesMouver
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts = null;

        internal AnalizFile Analizator { get; private set; }

        public Form1()
        {
            InitializeComponent();

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
            
            stopwatch.Stop();//�������� ������� ���������� ������ SerchInDirectoryAsync
            string executionTime = stopwatch.Elapsed.TotalSeconds.ToString();

            timer1.Enabled = false;

            progressBar1.Value = progressBar1.Maximum;

            if (Analizator.ErrMessage.Length == 0)
            {
                var DbManager = await PostgreSqlManager.CreateObjectAsync();
                
                //�� ���������� ������ ������� ���������� ������ � ������� ���� �������� �������� �����
                var countMatches = await DbManager.CountMatchesAsync();
                textBox_log.Text = $"{Analizator.Status}\r\n����� ����������: {executionTime} ���\r\n���������� ����������: {countMatches}";

                //�� ���������� ����� ������� � ������� ������ ���� �������� ����� 
                var countFilesByWord = new List<SearchResult>(await DbManager.CountFilesByMatchesAsync());
                foreach (SearchResult C in countFilesByWord)
                {
                    textBox_log.Text += $"\r\n������ �� ������: {C.key_word} = {C.count}��";
                }
                
                // ���������� � dataGridView1 ��� ������ �� ���������� ������
                dataGridView1.DataSource = await DbManager.GetAllRowsLastSearchAsync();

                if (DbManager.ErrorsMessage.Length > 0)
                {
                    textBox_log.BackColor = Color.LightCoral;
                    textBox_log.Text = $"class PostgreSqlManager {DbManager.ErrorsMessage}";
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
