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
        CancellationTokenSource cts = null;// new CancellationTokenSource();

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

            Stopwatch stopwatch = new Stopwatch();//замеряет время выполнения метода Analizator
            stopwatch.Start();

            Analizator = new AnalizFile(dirIn, slovoPath, dirOut);
            var processAnalizator = Analizator.SerchInDirectoryAsync(token);
            //остальные действия в программе пока выполняется процесс Analizator.SerchInDirectoryAsync до строки await;

            textBox_log.BackColor = Color.White;
            textBox_log.Text = $"{Analizator.Status}";
            timer1.Enabled = true;


            await processAnalizator;

            stopwatch.Stop();
            string executionTime = stopwatch.Elapsed.TotalSeconds.ToString();//время выполнения метода Analizator

            timer1.Enabled = false;

            progressBar1.Value = progressBar1.Maximum;

            if (Analizator.ErrMessage.Length == 0)
            {
                var DbManager = await PostgreSqlManager.CreateObjectAsync();
                var MatchCount = await DbManager.GetMatchCountAsync();//запрос бд количество строк последнего поиска

                textBox_log.Text = $"{Analizator.Status}\r\nвремя выполнения: {executionTime} сек\r\nколичество совпадений: {MatchCount}";

                /*
                var FindedWordsCount = await DbManager.GetFindedWordsCount();//запрос бд количество строк с каждым уникальным значением 
                foreach (var word in FindedWordsCount)
                {
                    textBox_log.Text += "\r\nфайлов со словом: " + word.ToString() + " шт";
                }
                */
                var countMatches = new List<SearchResult>(await DbManager.CountFilesByMatchesAsync());
                foreach (SearchResult C in countMatches)
                {
                    textBox_log.Text += $"\r\nфайлов со словом: {C.key_word} шт";
                }
                
                dataGridView1.DataSource =  DbManager.GetAllRows();

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
