using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
    internal class AnalizFile
    {
        public string dirIn;
        public string slovoPath;
        public string dirOutPath;

        public string report="";
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

            if (File.Exists(slovoPath)==false)
            {
                ErrMessage = $"{slovoPath} не найден, отмена обработки ";
            }

           SpisokSlov = File.ReadAllLines(slovoPath);// один раз считываем слова 

            if (SpisokSlov.Length==0)
            {
                ErrMessage = $"{slovoPath} не содержит слов для поиска, отмена обработки ";
            }
        }
        public async Task SerchInDirectory()
        {
            Status = "Старт...";

            if (ErrMessage.Length>0)
            {
                Status = "Обработка завершена";
                return;
            }

            try
            {
             //   if (File.Exists(slovoPath) && Directory.Exists(dirIn))//если слово(для поиска) существует
             //   -здесь только поиск проверка д.б не в этом методе
              //  {
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

                    foreach (string file in allFilesPath)//в каждом файле ищем слово и перемещаем файл если нашли совпадение file это путь к фаилу
                    {
                       // AnalizFile analiz = new AnalizFile(file, slovoPath, dirOutPath); ?????????????
                       // analiz.SearchInFile(file);

                        this.SearchInFile(file);    

                         // report += report;

                    if (sovpadenie == true)
                    {
                        MoveFileTo(file);
                        report += " COPY";
                    }
                    else
                    {
                        DeleteFile(file);
                    }

                    Position++;
             }

                    //этого не было в ТЗ
                   // string pathPeport = $"{dirOutPath}\\_REPORT.txt";
                  //  File.WriteAllTextAsync(pathPeport, report);// запись отчета в файл _REPORT.txt" (путь dirOutPath)
              //  }

                Status = "Обработка завершена";


            }
            catch (Exception ex)
            {

                ErrMessage= ex.Message;
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
                File.Copy(file, dirOutPath + "\\" + filename, true);
            }
            catch (Exception ex)
            {

                ErrMessage = ex.Message;
            }
        }

        public void SearchInFile(string textFilePath)//, out string report)  //считывает текстовые фаилы: text и slovo и ищет в них совпадения.
        {
            int count = 0;

            report += $"\r\nФайл:{Path.GetFileName(textFilePath)}";//создаем отчет, добавляем имя файла
            
            string text = File.ReadAllText(textFilePath);

            //string[] spisokSlov = File.ReadAllLines(slovoPath);//считываем слова в файле slovoPath
            foreach (string slovo in SpisokSlov)
            {
                ///////////////////////////////////////////////
                StartSearch startsearch = new StartSearch();
                
                startsearch.FinedWord(new SearchSposobOne(text, slovo));

                count += startsearch.sovpadenieCount;
                if (startsearch.find==true) 
                {
                    sovpadenie = true;
                    break;
                }
                ////////////////////////////////////////////////
                report += $"\r\nсодержит:'{slovo}'раз:{count}";//добавляем в отчет результат поиска
            }

            if (sovpadenie == true)
            {
                report += "\r\nstatus: Math";
                //перемещение файла в котором нашлось совпадение (задаем путь)
                //fi.CopyTo(dirOutPath + "\\" + fi.Name, true);
            }

            if (sovpadenie == false)
            {
                report += "\r\nstatus: No Matches";
            }

        }
    }
}
