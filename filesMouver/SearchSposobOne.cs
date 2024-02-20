using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filesMove
{
    internal class SearchSposobOne : ISearch
    {
        public string text;
        public string slovo;

        public SearchSposobOne(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
        }
        public void DoSearch(out bool find, out int sovpadenieCount)
        {
            
            text = text.ToLower();
            text = text.Trim();

            slovo = slovo.ToLower();
            slovo = slovo.Trim();

            find = false; // переменная указывает есть совпадение или нет
            sovpadenieCount = 0;// считает сколько было совпадений

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
                            find = true;
                            sovpadenieCount += 1;//считаем сколько будет совпадений в файле
                            target = "";// очистка  target для продолжения поиска совпадений
                        }
                    }
                }
            }
        }
    }
}
