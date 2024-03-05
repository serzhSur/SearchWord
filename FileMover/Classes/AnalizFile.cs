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

        public int CountMatches = 0;
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

                CountFiles = allFilesPath.Count();//для прогресс-бара
                Position = 0;

                foreach (string file in allFilesPath)//в каждом файле ищем список слов и перемещаем файл если нашли совпадение 
                {
                    
                    string text = await File.ReadAllTextAsync(file);

                    foreach (string slovo in SpisokSlov)
                    {

                        if (slovo.Length == 0)
                            continue;                        
                        
                        var startsearch = new StartSearch();

                        await Task.Run(()=> startsearch.FinedWord(new SearchSposobONE(text, slovo)));
                       
                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobTWO(text,slovo)));

                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobLINQ(text,slovo)));

                        //await Task.Run(() => startsearch.FinedWord(new SearchSposobRegex(text, slovo)));

                        sovpadenie = startsearch.Sovpadenie;
                       
                    }

                    if (sovpadenie == true)
                    {
                        //MoveFileTo(file);
                        CountMatches += 1;
                    }
                    else
                    {
                       //DeleteFile(file);
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
                File.Copy(file, dirOutPath + "\\" + filename, true);// Copy/Move
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
            }
        }


    }
}
