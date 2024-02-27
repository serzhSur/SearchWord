using FilesMouver;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
<<<<<<< HEAD
    internal class SearchSposobONE: ISearch
=======
    internal class SearchSposobOne //: ISearch
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
    {
        public bool sovpadenie { get; set; } = false;// переменная указывает есть совпадение или нет
        public int matchCount { get; set; } = 0;// считает сколько было совпадений
        public string otchet { get; set; } = "";
        
        public string text;
        public string slovo;

        public SearchSposobONE(string text, string slovo)
        {
            this.text = text.ToLower(); 
            this.slovo = slovo.ToLower();
            
        }
        public void DoSearch()
        {

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
                            i -= 1;
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
            otchet = $"sovpadenie:{sovpadenie}\tnamberMatch:{matchCount}\tword:{slovo}";
        }
    }
}
