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

        private void Files() //����������� ������ �� ������� ������ Copy
        {

            string dirIN = textBox2_dirIn.Text;
            string dirOut = textBox3_dirOut.Text;

            if (!Directory.Exists(dirOut))
            {
                try
                {
                    Directory.CreateDirectory(dirOut);
                }
                catch
                {
                    MessageBox.Show("���������� ���������� (dirOut) ����������� ����");
                }

            }
            else
            {
                MessageBox.Show("���������� ��� ����������. ����� ����� ������������");
                //����� ������ ������ �����������
            }

            if (Directory.Exists(dirIN))
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 1;
                progressBar1.Maximum = Directory.GetFiles(dirIN).Length; //������������ �������� = ���������� ������ � ���������
                progressBar1.Value = 1;
                progressBar1.Step = 1;

                string[] spisokFiles = Directory.GetFiles(dirIN);// �������� ������  ���� ������ � dirIN(���������)

                foreach (string file in spisokFiles)
                {
                    string fileName = Path.GetFileName(file); // �������� ��� ����� �����

                    string destination_Path = ($@"{dirOut}\{fileName}");// �������� ���� ����������: ����(dirOut)+��� �����

                    File.Copy(file, destination_Path, true); // �������� ����, true-����������� ���� ��� ���� �����

                    progressBar1.PerformStep();

                }
                textBox1.Text = "����� ������������";

            }
            else { MessageBox.Show("�������� ���������� (dirIn) �������������� ����"); }

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

            if (Analizator.ErrMessage.Length > 0)
            {
                textBox_log.BackColor = Color.LightCoral;
                textBox_log.Text = Analizator.ErrMessage;
            }

            var PgManager = await PostgreSqlManager.CreateObjectAsync();//��������� � ���� ������ ��� �������� ���������� ����������
            var report = await PgManager.GetMatchCountAsync();

            textBox_log.Text = $"{Analizator.Status}\r\n����� ����������: {executionTime} ���\r\n���������� ����������: {report}";
            PgManager.CloseConnection();
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
