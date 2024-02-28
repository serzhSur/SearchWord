using FilesMouver;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
    internal class SearchSposobONE: ISearch
    {
        public bool sovpadenie { get; set; }
        public int matchCount { get; set; }
        public string otchet { get; set; } = "";
        
        public string text;
        public string slovo;

        public SearchSposobONE(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
            
        }
        public void DoSearch()
        {
            
            text = text.ToLower();
            text = text.Trim();

            slovo = slovo.ToLower();
            slovo = slovo.Trim();

            sovpadenie = false; // переменная указывает есть совпадение или нет
            matchCount = 0;// считает сколько было совпадений

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == slovo[0])
                {
                    string target = text[i].ToString();// инициализируем переменную для поиска совпадений в цикле

                    for (int s = 1; (s < slovo.Length) && (i < text.Length); s++)
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
                            sovpadenie = true;
                            matchCount += 1;//считаем сколько будет совпадений в файле
                            target = "";// очистка  target для продолжения поиска совпадений
                            break;
                        }
                    }
                }
            }
            otchet = $"word:{slovo}\t\tnamberMatch:{matchCount}\tsovpadenie:{sovpadenie}";
        }
    }
}
