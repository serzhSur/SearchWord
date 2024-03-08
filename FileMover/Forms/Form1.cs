using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FilesMove.Classes;


namespace FilesMouver
{
    public partial class Form1 : Form
    {
        int timePb = 0;
      
        internal AnalizFile Analizator { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }


        private void button2CopyFiles_Click(object sender, EventArgs e) //����������� ������ �� ������� ������ Copy
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
            

            Analizator = new AnalizFile(dirIn, slovoPath, dirOut);
            var processAnalizator = Analizator.SerchInDirectory();
            //��������� �������� � ��������� ���� ����������� ������� Analizator.SerchInDirectory;
            textBox_log.Text = $"{Analizator.Status}";

            timer1.Enabled = true;

            ////
            await processAnalizator;

            timer1.Enabled = false;
            progressBar2.Value = progressBar2.Maximum;

            progressBar1.Maximum = Analizator.CountFiles;
            progressBar1.Value = Analizator.Position;

            textBox_log.Text = $"{Analizator.Status} ����������: {Analizator.CountMatches}";


            if (Analizator.ErrMessage.Length > 0)
            {
                textBox_log.BackColor = Color.LightCoral;
                textBox_log.Text = Analizator.ErrMessage;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar2.Maximum = 20;

            if (timePb == progressBar2.Maximum)
            {
                timePb = 0;
            }

            progressBar2.Value = timePb;

            timePb++;

        }

       
    }
}
