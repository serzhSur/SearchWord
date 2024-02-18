using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace findeWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //исходные данные
            string dirIn = @"C:\test\findWord"; //путь к файлу в котором будем искать совпадения
            string dirOut = @"C:\test\findWord\out\";// путь куда перемещ-копируется файл с совпадением со словоформой 
            string slovoPath = @"C:\test\findWord\keyWord\w6.txt";//путь к файлу c условием поиска (со слофоформой)                                

            Console.WriteLine($"{dirIn}\nSTART reading files...");

            StartSorfFiles(dirIn, slovoPath, dirOut);

            Console.WriteLine("program  Finish.");
            Console.ReadLine();
        }

        private static void StartSorfFiles(string dirIn, string slovoPath, string dirOut)
        {
            if (File.Exists(slovoPath))//если слово(для поиска) существует получение списка файлов в выбранном каталоге dirIn
            {
                string[] allFilesPath = Directory.GetFiles(dirIn);//получаем список файлов

                foreach (string file in allFilesPath)//в каждом файле ищем слово и перемещаем файл если нашли совпадение
                {
                    string report = MoveFileWithTargetWords(file, slovoPath, dirOut);
                    Console.WriteLine(report);
                }
            }
        }
        private static string MoveFileWithTargetWords(string textPath, string slovoPath, string outPath)
        {
            bool needMove = false;
            //считываем текстовые фаилы: text и слово и ищем в них совпадения.
            string text = File.ReadAllText(textPath);

            FileInfo fi = new FileInfo(textPath);//нужно для выделения Имя файла, чтобы (если есть совпадения) его переместить

            string report = $"Файл:{fi.Name}\n";//создаем отчет

            string[] spisokSlov = File.ReadAllLines(slovoPath);//считываем слова в файле slovoPath
            foreach (string slovo in spisokSlov)
            {
                // int result = SearchWordInText(text, slovo);//МЕТОД SearchWord выдает кол-во совпадений
                ///
                var startsearch = new StartSearch(text, slovo);
                startsearch.FinedWord(new SearchSposobOne(text, slovo));
                int result = startsearch.match;
                ///
                report += $"содержит:'{slovo}'раз:{result}\n";//добавляем в отчет что есть совпадение

                if (result > 0)
                {
                    needMove = true; //чтобы переместить файл 1раз а не при каждом совподении если несколько слов ищем в файле
                }
            }

            if (needMove == true)
            {
                report += "status: MOVE\n";
                //перемещение файла в котором нашлось совпадение (задаем путь)
                //fi.CopyTo(outPath + "\\" + fi.Name, true);
            }

            if (needMove == false)
            {
                report += "status: No Mathces\n";
            }

            return report;

        }
        private static int SearchWordInText(string text, string slovo)  //МЕТОД поиск совпадений 
        {
            text = text.ToLower();
            slovo = slovo.ToLower();
            slovo = slovo.Trim();

            int sovpadenie = 0;// считает сколько было совпадений

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == slovo[0])
                {
                    string target = text[i].ToString();// инициализируем переменную для поиска совпадений в цикле

                    for (int s = 1; s < slovo.Length && i < text.Length; s++)
                    {
                        i += 1;
                        if (slovo[s] == text[i])
                        {
                            target += text[i].ToString();
                        }
                        else
                        {
                            target = "";
                            i -= 1;// добавил!!!
                            break;
                        }

                        if (target == slovo)
                        {
                            sovpadenie += 1;//считаем сколько будет совпадений в файле
                            target = "";// очистка  target для продолжения поиска совпадений
                        }

                    }

                }
            }

            return sovpadenie;

        }

    }
}