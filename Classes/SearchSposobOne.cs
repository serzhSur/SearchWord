using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace findeWords
{
    internal class SearchSposobOne: ISearch
    {
        public string text;
        public string slovo;

        public int sovpadenie;
        public SearchSposobOne(string TEXT, string SLOVO)
        {
            text = TEXT;
            slovo = SLOVO;
        }
        public void DoSearch()
        {
            text = text.ToLower();
            text = text.Trim();

            slovo = slovo.ToLower();
            slovo = slovo.Trim();

            sovpadenie = 0;// считает сколько было совпадений

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
                            sovpadenie += 1;//считаем сколько будет совпадений в файле
                            target = "";// очистка  target для продолжения поиска совпадений
                        }
                    }
                }
            }
        }
    }
}
