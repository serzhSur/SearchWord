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

        private void textBox2DirIN_TextChanged(object sender, EventArgs e) //при изменении текста в textBox2_dirIN

        {

            ViewDirIn();                      

        }

        void ViewDirIn() //обзор в listBox1 дириктории Источника(textBox2_dirIN)
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


        private void textBox3DirOut_TextChanged(object sender, EventArgs e) //событие на изменение в textBox3_dirOut
        {
            ViewDirOut();

            void ViewDirOut() // обзор директории в текстовом поле textBox1, путь из textBox3_dirOut
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

        private void button1Show_Click(object sender, EventArgs e) //кнопка Show обзор директроии pathOut (путь в textBox3)
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

        private void button2CopyFiles_Click(object sender, EventArgs e) //копирование файлов по нажатию кнопки Copy
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
                    MessageBox.Show("Директория назначения (dirOut) Невозможный путь");
                }

            }
            else
            {
                MessageBox.Show("Директория уже существует. Файлы будут перезаписаны");
                //нужна кнопка отмены копирования
            }

            if (Directory.Exists(dirIN))
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 1;
                progressBar1.Maximum = Directory.GetFiles(dirIN).Length; //максимальное значение = количеству файлов в источнике
                progressBar1.Value = 1;
                progressBar1.Step = 1;

                string[] spisokFiles = Directory.GetFiles(dirIN);// получили массив  пути файлов в dirIN(источнике)

                foreach (string file in spisokFiles)
                {
                    string fileName = Path.GetFileName(file); // получаем имя этого файла

                    string destination_Path = ($@"{dirOut}\{fileName}");// получаем путь назначения: путь(dirOut)+имя файла

                    File.Copy(file, destination_Path, true); // копируем файл, true-перезапишет если уже есть файлы

                    progressBar1.PerformStep();

                }
                textBox1.Text = "Файлы скопированны";

            }
            else { MessageBox.Show("Входящая директория (dirIn) Несуществующий путь"); }

        }

        private void button3CopyAllDirectory_Click(object sender, EventArgs e) //перемещение каталога по нажатию кнопки Move
        {
            CopyFiles();

        }

        private void CopyFiles()
        {
            string dirIN = textBox2_dirIn.Text;
            string dirOut = textBox3_dirOut.Text;

            perebor_updates(dirIN, dirOut);

            // рекурсивный метод который копирует паку с вложениями
            void perebor_updates(string begin_dir, string end_dir)
            {
                //Берём нашу исходную папку
                DirectoryInfo dir_inf = new DirectoryInfo(begin_dir);
                try
                {
                    //Перебираем все внутренние папки
                    foreach (DirectoryInfo dir in dir_inf.GetDirectories())
                    {
                        //Проверяем - если директории не существует, то создаём;
                        if (Directory.Exists(end_dir + "\\" + dir.Name) != true)
                        {
                            Directory.CreateDirectory(end_dir + "\\" + dir.Name);
                        }

                        //Рекурсия (перебираем вложенные папки и делаем для них то-же самое).
                        perebor_updates(dir.FullName, end_dir + "\\" + dir.Name);
                    }

                    //Перебираем файлики в папке источнике.
                    foreach (string file in Directory.GetFiles(begin_dir))
                    {
                        //Определяем (отделяем) имя файла с расширением - без пути (но с слешем "\").
                        string filik = file.Substring(file.LastIndexOf('\\'), file.Length - file.LastIndexOf('\\'));
                        //Копируем файлик с перезаписью из источника в приёмник.
                        File.Copy(file, end_dir + "\\" + filik, true);
                    }

                    textBox1.Text = "Католог со всеми вложениями скопирован.";
                }
                catch
                {
                    MessageBox.Show("Невозможный путь");
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
