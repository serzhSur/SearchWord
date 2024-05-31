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
            string dirOut = textBox3_dirOut.Text;
            string slovoPath = textBox_pathWords.Text;

            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Stopwatch stopwatch = new Stopwatch();//�������� ����� ���������� ������ Analizator
            stopwatch.Start();

            Analizator = new AnalizFile(dirIn, slovoPath, dirOut);
            var processAnaliza = Analizator.SerchInDirectoryAsync(token);

            //��������� �������� � ��������� ���� ����������� ������� Analizator.SerchInDirectoryAsync �� ������ await
            textBox_log.BackColor = Color.White;
            progressBar1.Value = progressBar1.Minimum;
            textBox_log.Text = $"{Analizator.Status}";
            timer1.Enabled = true;

           
            await processAnaliza;
            
            stopwatch.Stop();//�������� ������� ���������� ������ SerchInDirectoryAsync
            string executionTime = stopwatch.Elapsed.TotalSeconds.ToString();

            timer1.Enabled = false;

            progressBar1.Value = progressBar1.Maximum;

            if (Analizator.ErrMessage.Length == 0)
            {
                textBox_log.Text = $"{Analizator.Status}";


                var front = new FrontManager();
                front.executionTime = executionTime;
                await front.ShowFrontAsync(textBox_log, dataGridView1, label1, label2);
                
                if (front.ErrorsMessage.Length > 0)
                {
                    textBox_log.BackColor = Color.LightCoral;
                    textBox_log.Text = $"class PostgreSqlManager {front.ErrorsMessage}";
                }
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
