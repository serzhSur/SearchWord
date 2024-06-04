using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{


    internal class SearchSposobOne : ISearch
    {
        public bool Sovpadenie { get; set; } = false;// переменная указывает есть совпадение или нет
        public string Name { get; set; } = "SearchSposobOne (perebor)";


        private string text;
        private string slovo;

        public SearchSposobOne(string Text, string Slovo)
        {
            this.text = Text.ToLower();
            this.slovo = Slovo.ToLower();

        }
        public void DoSearch()
        {

            string target = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (Sovpadenie == true)
                {
                    break;
                }
                if (text[i] == slovo[0])
                {
                    target = text[i].ToString();// инициализируем переменную для поиска совпадений в цикле

                    for (int s = 1; (s <slovo.Length) && ((i+1) <text.Length); s++, i++)
                    {

                        if (slovo[s] == text[i + 1])
                        {
                            target += text[i + 1].ToString();
                        }
                        if (target == slovo)
                        {
                            Sovpadenie = true;

                            break;
                        }

                    }
                }
            }

        }
    }
}
