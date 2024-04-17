using FilesMove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileMover.Classes;


namespace FilesMove.Classes
{
    internal class AnalizFile
    {
        private string dirIn;
        private string slovoPath;
        private string dirOutPath;
        private bool sovpadenie = false;
        private StartSearch startsearch;

        public string ErrMessage { get; private set; } = "";
        public string Status { get; private set; }
        public int CountFiles { get; private set; } = 0;
        public int Position { get; private set; }
        public string[] SpisokSlov { get; private set; }

        public AnalizFile(string dirIn, string slovoPath, string dirOutPath)
        {
            this.dirIn = dirIn;
            this.slovoPath = slovoPath;
            this.dirOutPath = dirOutPath;

            try
            {
                startsearch = new StartSearch();

                SpisokSlov = File.ReadAllLines(slovoPath);// один раз считываем слова 
                if (SpisokSlov.Length < 1)
                {
                    ErrMessage = $"{slovoPath} не содержит слов для поиска, отмена обработки ";
                }
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }

        public async Task SerchInDirectoryAsync(CancellationToken token)
        {
            Status = "Выполняется...";
            
            if (ErrMessage.Length > 0)
            {
                Status = "Обработка завершена";
                return;
            }

            try
            {
                if (Directory.Exists(dirOutPath) == false)
                {
                    Directory.CreateDirectory(dirOutPath);
                }
                if (Directory.Exists(dirOutPath) == false)
                {
                    Status = "Обработка завершена";
                    return;
                }

                var DbManager = await PostgreSqlManager.CreateObjectAsync(); //Подключение к базе данных
                
                string[] allFilesPath = Directory.GetFiles(dirIn);//получаем список файлов для анализа

                CountFiles = allFilesPath.Count();//для прогресс-бара
                Position = 0;
                DateTime timeNow = DateTime.Now;// для базы данных
               
                foreach (string file in allFilesPath)//в каждом файле ищем слово из списка и перемещаем файл если нашли совпадение 
                {
                    if (token.IsCancellationRequested)
                    {
                        ErrMessage = "operation Cancel";
                        return;
                    }
                   
                    sovpadenie = false;
                    string keyWord = "";// для базы данных
                   
                    string text = await File.ReadAllTextAsync(file);
                    string nameOfFile = Path.GetFileName(file);
                    
                    foreach (string slovo in SpisokSlov)
                    {
                        if (slovo.Length==1 || slovo == " ")
                        {
                            continue;
                        }
                        //выбирается метод(4шт) которым будет осуществлятся поиск

                        //await Task.Run(()=> startsearch.FinedWord(new SearchSposobOne(text, slovo)));
                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobTwo(text, slovo)));
                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobLinq(text,slovo)));
                        await Task.Run(() => startsearch.FinedWord(new SearchSposobRegex(text, slovo)));

                        sovpadenie = startsearch.Sovpadenie;
                        if (sovpadenie)
                        {
                            keyWord = slovo;
                            break;
                        }
                    }

                    if (sovpadenie == true)
                    {
                       await DbManager.InsertDataAsync(nameOfFile, dirIn, keyWord, sovpadenie, dirOutPath, timeNow.ToString());
                        // MoveFileTo(file);
                    }
                    else
                    {
                       // DeleteFile(file);
                    }
                    
                    Position++;//позиция прогресс-бара
                }

                DbManager.CloseConnection();
                Status = "Обработка завершена";
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }

        }

        private void DeleteFile(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.SetAttributes(file, FileAttributes.Normal);

                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }

        private void MoveFileTo(string file)
        {
            try
            {
                string filename = Path.GetFileName(file);
                File.Move(file, dirOutPath + "\\" + filename, true);// Copy/Move
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }


    }
}
