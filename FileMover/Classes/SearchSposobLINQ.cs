using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class SearchSposobLINQ : ISearch
    {
        public bool Sovpadenie { get; set; } = false;

        public string text;
        public string slovo;

        public SearchSposobLINQ(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;

        }
        public void DoSearch()
        {

            char[] separators = { ' ', ',', '.', '-' };
            string[] textArray = text.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);//преобразовали текст в массив, слово-элемент мас


            var selectedWords = textArray.Where(p => p.Contains(slovo.ToLower())).FirstOrDefault();


            if (selectedWords != null)
            {
                Sovpadenie = true;
            }
        }
    }
}
