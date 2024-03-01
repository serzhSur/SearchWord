using FilesMove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class AnalizFile
    {
        public string dirIn;
        public string slovoPath;
        public string dirOutPath;

        int count = 0;
        bool sovpadenie = false;

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

            if (File.Exists(slovoPath) == false)
            {
                ErrMessage = $"{slovoPath} не найден, отмена обработки ";
            }

            SpisokSlov = File.ReadAllLines(slovoPath);// один раз считываем слова 

            if (SpisokSlov.Length == 0)
            {
                ErrMessage = $"{slovoPath} не содержит слов для поиска, отмена обработки ";
            }
        }
        public async Task SerchInDirectory()
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

                CountFiles = allFilesPath.Count();
                Position = 0;

                foreach (string file in allFilesPath)//в каждом файле ищем список слов и перемещаем файл если нашли совпадение 
                {

                    string text =await File.ReadAllTextAsync(file);// делать асинхронно (может долго считывать) параллельно можно считывать другие поменьше.
                    
                    
                    string findedWord = "";


                    foreach (string slovo in SpisokSlov)
                    {

                        if (slovo.Length == 0)
                            continue;                        
                        
                        StartSearch startsearch = new StartSearch();

                        startsearch.FinedWord(new SearchSposobONE(text, slovo));

                        startsearch.FinedWord(new SearchSposobTWO(text,slovo));

                        startsearch.FinedWord(new SearchSposobLINQ(text,slovo));

                        startsearch.FinedWord(new SearchSposobRegex(text, slovo));

                        //count += startsearch.matchCount;
                        sovpadenie = startsearch.sovpadenie;
                        

                        if (sovpadenie == true)
                        {
                            findedWord = slovo;
                            break;
                        }

                    }

                    if (sovpadenie == true)
                    {
                        MoveFileTo(file);
                        //report += "\r\nCOPY";
                    }
                    else
                    {
                        DeleteFile(file);
                        //report += "\r\nDeleted";
                    }

                    Position++;//позиция прогресс бара
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
                File.Delete(file);
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
                File.Move(file, dirOutPath + "\\" + filename, true);
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }


    }
}
