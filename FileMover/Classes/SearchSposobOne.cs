using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{


    internal class SearchSposobONE : ISearch
    {
        public bool Sovpadenie { get; set; } = false;// переменная указывает есть совпадение или нет
     
        public string text;
        public string slovo;

        public SearchSposobONE(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;

        }
        public void DoSearch()
        {
            string target = "";
            int index = 0;

            text = text.ToLower();
            slovo = slovo.ToLower();

            for (int i = 0; i <= text.Length - 1; i++)
            {
                if (Sovpadenie == true)
                {
                    break;
                }
                if (text[i] == slovo[0])
                {
                    target = text[i].ToString();// инициализируем переменную для поиска совпадений в цикле

                    for (int s = 1; (s <= slovo.Length - 1) && (i < text.Length - 1); s++, i++)
                    {

                        if (slovo[s] == text[i + 1])
                        {
                            target += text[i + 1].ToString();
                        }
                        if (target == slovo)
                        {
                            Sovpadenie = true;
                            index = i;

                            break;
                        }

                    }
                }
            }

        }
    }
}
