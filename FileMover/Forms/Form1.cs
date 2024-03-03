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
        internal AnalizFile Analizator { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2DirIN_TextChanged(object sender, EventArgs e) //��� ��������� ������ � textBox2_dirIN

        {

            ViewDirIn();                      

        }

        void ViewDirIn() //����� � listBox1 ���������� ���������(textBox2_dirIN)
        {
            string dirIN = textBox2_dirIn.Text;
            if (Directory.Exists(dirIN))
            {
                //listBox1.Items.Clear();
                //string[] dirView = Directory.GetDirectories(dirIN);
                //listBox1.Items.Insert(0, "Directories:");
                //foreach (string d in dirView)
                //{
                //    listBox1.Items.Add(d);
                //}

                //string[] filesView = Directory.GetFiles(dirIN);
                //listBox1.Items.Add("Files:");
                //foreach (string f in filesView)
                //{
                //    listBox1.Items.Add(f);
                //}
            }
            else
            {
                //listBox1.Items.Clear();
                //listBox1.Items.Add("No such directory");
            }
        }


        private void textBox3DirOut_TextChanged(object sender, EventArgs e) //������� �� ��������� � textBox3_dirOut
        {
            ViewDirOut();

            void ViewDirOut() // ����� ���������� � ��������� ���� textBox1, ���� �� textBox3_dirOut
            {
                string path = textBox3_dirOut.Text;
                if (Directory.Exists(path))
                {
                    textBox1.Text = "Such directory exist";

                    string[] dirView = Directory.GetFileSystemEntries(path);
                    foreach (string s in dirView)
                    {
                        textBox1.Text += "\r\n" + s;
                    }
                }
                else { textBox1.Text = "No such directory, new path"; }
            }
        }

        private void button1Show_Click(object sender, EventArgs e) //������ Show ����� ���������� pathOut (���� � textBox3)
        {
            ViewDirectory();

        }

        private void ViewDirectory()
        {
            string dir = textBox3_dirOut.Text;
            if (Directory.Exists(dir))
            {
                string[] dirView = Directory.GetFileSystemEntries(dir);
                foreach (string f in dirView)
                {
                    textBox1.Text += "\r\n" + f;
                }

            }
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

        private void button3CopyAllDirectory_Click(object sender, EventArgs e) //����������� �������� �� ������� ������ Move
        {
            CopyFiles();

        }

        private void CopyFiles()
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
            progressBar1.Value = Analizator.Position;
            progressBar1.Maximum = Analizator.CountFiles;
            await Analizator.SerchInDirectory();

            textBox_log.Text = Analizator.Status;

            if (Analizator.ErrMessage.Length > 0)
            {
                textBox_log.BackColor =Color.LightCoral;
                textBox_log.Text = Analizator.ErrMessage;
            }

           
            


        }
    }
}
