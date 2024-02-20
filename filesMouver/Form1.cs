using filesMove;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace filesMouver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void button1_ViewDestinationDir_Click(object sender, EventArgs e) //������ Show ����� ���������� pathOut (���� � textBox3)
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

        private void textBox2_dirIN_TextChanged(object sender, EventArgs e) //��� ��������� ������ � textBox2_dirIN

        {
            ViewDirIn();

            void ViewDirIn() //����� � listBox1 ���������� ���������(textBox2_dirIN)
            {
                string dirIN = textBox2_dirIN.Text;
                if (Directory.Exists(dirIN))
                {
                    listBox1.Items.Clear();
                    string[] dirView = Directory.GetDirectories(dirIN);
                    listBox1.Items.Insert(0, "Directories:");
                    foreach (string d in dirView)
                    {
                        listBox1.Items.Add(d);
                    }

                    string[] filesView = Directory.GetFiles(dirIN);
                    listBox1.Items.Add("Files:");
                    foreach (string f in filesView)
                    {
                        listBox1.Items.Add(f);
                    }
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("No such directory");
                }
            }

        }

        private void textBox3_dirOut_TextChanged(object sender, EventArgs e) //������� �� ��������� � textBox3_dirOut
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


        private void button2_CopyFiles_Click(object sender, EventArgs e) //����������� ������ �� ������� ������ Copy
        {

            string dirIN = textBox2_dirIN.Text;
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

                    File.Copy(file, destination_Path, true); // �������� ���� 

                    progressBar1.PerformStep();

                }
                textBox1.Text = "����� ������������";

            }
            else { MessageBox.Show("�������� ���������� (dirIn) �������������� ����"); }

        }

        private void button3_copyAllDirectory_Click(object sender, EventArgs e) //����������� �������� �� ������� ������ Move
        {

            string dirIN = textBox2_dirIN.Text;
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

        private void button4_Click(object sender, EventArgs e)
        {
            //�������� ������
            string dirIn = textBox2_dirIN.Text;//@"C:\test\findWord"; //���� � ����� � ������� ����� ������ ����������
            string slovoPath = @"C:\test\findWord\keyWord\w6.txt";//���� � ����� c �������� ������ (�� �����������)                               
            string dirOut = textBox3_dirOut.Text;// @"C:\test\findWord\out\";// ���� ���� �������-���������� ���� � ����������� �� ����������� 

            try 
            {
                AnalizFile af = new AnalizFile(dirIn, slovoPath, dirOut);
                af.SerchInDirectory();

                textBox1.Text += "\r\n" + af.report;
            } 
            catch 
            {
                MessageBox.Show("�������� ����");
            }

        }
    }
}
