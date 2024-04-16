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

        private void Files() //копирование файлов по нажатию кнопки Copy
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


        private void CopyDirectoryAll()
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
            string executionTime = stopwatch.Elapsed.TotalSeconds.ToString();
            
            timer1.Enabled = false;
            progressBar1.Value = progressBar1.Maximum;

            if (Analizator.ErrMessage.Length > 0)
            {
                textBox_log.BackColor = Color.LightCoral;
                textBox_log.Text = Analizator.ErrMessage;
            }

            var PgManager = await PostgreSqlManager.CreateObjectAsync();//обращение к базе данных для подсчета количества совпадений
            var report = await PgManager.GetMatchCountAsync();

            textBox_log.Text = $"{Analizator.Status}\r\nвремя выполнения: {executionTime} сек\r\nколичество совпадений: {report}";
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
