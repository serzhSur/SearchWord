using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filesMove
{
    internal class AnalizFile
    {
        public string dirIn;
        public string slovoPath;// = @"C:\test\findWord\keyWord\w6.txt";
        public string dirOutPath;

        public string report="";
        bool sovpadenie = false;

        public AnalizFile(string dirIn, string slovoPath, string dirOutPath)
        {
            this.dirIn = dirIn;
            this.slovoPath = slovoPath;
            this.dirOutPath = dirOutPath;
        }
        public void SerchInDirectory()
        {

            if (File.Exists(slovoPath)&&Directory.Exists(dirIn))//если слово(для поиска) существует
            {
                if (Directory.Exists(dirOutPath) != true)
                {
                    Directory.CreateDirectory(dirOutPath);
                }
                
                string[] allTxtFilesPath = Directory.GetFiles(dirIn);//получаем список файлов для анализа
                
                foreach (string file in allTxtFilesPath)//в каждом файле ищем слово и перемещаем файл если нашли совпадение file это путь к фаилу
                {
                    AnalizFile analiz = new AnalizFile(file, slovoPath, dirOutPath);
                    analiz.SearchInFile(file);
                    
                    report += analiz.report;
                    
                    if (analiz.sovpadenie == true) 
                    { 
                        string filename = Path.GetFileName(file);
                        File.Copy(file, dirOutPath+"\\"+ filename, true);
                        
                        report += " COPY";
                    }
                }
                string pathPeport = $"{dirOutPath}\\_REPORT.txt"; 
                File.WriteAllTextAsync(pathPeport, "\r\n" + report);// запись отчета в файл _REPORT.txt" (путь dirOutPath)
            }
        }
        public void SearchInFile(string textFilePath)//, out string report)  //считывает текстовые фаилы: text и slovo и ищет в них совпадения.
        {
            int count = 0;

            report += $"\r\nФайл:{Path.GetFileName(textFilePath)}";//создаем отчет, добавляем имя файла
            
            string text = File.ReadAllText(textFilePath);

            string[] spisokSlov = File.ReadAllLines(slovoPath);//считываем слова в файле slovoPath
            foreach (string slovo in spisokSlov)
            {
                ///////////////////////////////////////////////
                StartSearch startsearch = new StartSearch();
                
                startsearch.FinedWord(new SearchSposobOne(text, slovo));

                count += startsearch.sovpadenieCount;
                if (startsearch.find==true) 
                {
                    sovpadenie = true;
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
