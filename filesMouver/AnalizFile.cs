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
        // public string textFilePath=""; // путь текстового файла @"C:\test\findWord" 
        public string slovoPath; //= @"C:\test\findWord\keyWord\w6.txt ";
        string outDirectoryPath;

        public string report;
        public string otchetDir;
        //public bool needMoveFile = false;

        public AnalizFile(string dirIn, string slovoPath, string outDirectoryPath)
        {
            //this.textFilePath = textFilePath;
            this.dirIn = dirIn;
            this.slovoPath = slovoPath;
            this.outDirectoryPath = outDirectoryPath;
        }
        public void SerchInDirectory()
        {

            if (File.Exists(slovoPath))//если слово(для поиска) существует
            {
                string[] allTxtFilesPath = Directory.GetFiles(dirIn);//получаем список файлов для анализа
                //string otchet = "";
                foreach (string file in allTxtFilesPath)//в каждом файле ищем слово и перемещаем файл если нашли совпадение file это путь к фаилу
                {

                    AnalizFile analiz = new AnalizFile(file, slovoPath, outDirectoryPath);
                    analiz.SearchInFile(file);
                    otchetDir += analiz.report;
                    //Console.WriteLine(analiz.report);

                }
            }

            // Console.WriteLine("program  Finish.");
            // Console.ReadLine();
        }
        public void SearchInFile(string textFilePath)//, out string report)  //считывает текстовые фаилы: text и slovo и ищет в них совпадения.
        {
            bool sovpadenie = false;

            string text = File.ReadAllText(textFilePath);

            FileInfo fi = new FileInfo(textFilePath);//нужно для выделения Имя Тхт файла, чтобы (если есть совпадения) его переместить
            string name = fi.Name;

            report += $"Файл:{name}\n";//создаем отчет, добавляем имя файла

            string[] spisokSlov = File.ReadAllLines(slovoPath);//считываем слова в файле slovoPath
            foreach (string slovo in spisokSlov)
            {
                ///////////////////////////////////////////////
                StartSearch startsearch = new StartSearch();
                startsearch.FinedWord(new SearchSposobOne(text, slovo));

                sovpadenie = startsearch.find;
                ////////////////////////////////////////////////
                report += $"содержит:'{slovo}'раз:{startsearch.sovpadenieCount}\n";//добавляем в отчет результат поиска
            }

            if (sovpadenie == true)
            {
                report += "status: Math\n";
                //перемещение файла в котором нашлось совпадение (задаем путь)
                // fi.CopyTo(outDirectoryPath + "\\" + fi.Name, true);
            }

            if (sovpadenie == false)
            {
                report += "status: No Mathces\n";
            }

        }
    }
}
