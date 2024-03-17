using FilesMove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace FilesMove.Classes
{
    internal class AnalizFile
    {
        private string dirIn;
        private string slovoPath;
        private string dirOutPath;
        private bool sovpadenie = false;


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
        public async Task SerchInDirectory(CancellationToken token)
        {

            Status = "Старт...";

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


                string[] allFilesPath = Directory.GetFiles(dirIn);//получаем список файлов для анализа

                CountFiles = allFilesPath.Count();//для прогресс-бара
                Position = 0;

                foreach (string file in allFilesPath)//в каждом файле ищем список слов и перемещаем файл если нашли совпадение 
                {
                    
                    if (token.IsCancellationRequested)
                    {
                        ErrMessage = "operation Cancel";

                        return;
                    }

                    sovpadenie = false;
                   
                    string text = await File.ReadAllTextAsync(file);

                    foreach (string slovo in SpisokSlov)
                    {

                        if (slovo.Length==1 || slovo == " ")
                        {
                            continue;
                        }

                        var startsearch = new StartSearch();
                        //выбирается метод(4шт) которым будет осуществлятся поиск

                        await Task.Run(()=> startsearch.FinedWord(new SearchSposobOne(text, slovo)));

                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobTwo(text, slovo)));

                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobLinq(text,slovo)));

                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobRegex(text, slovo)));

                        sovpadenie = startsearch.Sovpadenie;
                        if (sovpadenie)
                        {
                            break;
                        }
                    }

                    if (sovpadenie == true)
                    {
                        MoveFileTo(file);
                    }
                    else
                    {
                        DeleteFile(file);
                    }

                    Position++;//позиция прогресс-бара
                }

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
