using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class SearchSposobLinq : ISearch
    {
        public bool Sovpadenie { get; set; } = false;
        public string Name { get; set; } = "SearchSposob Linq";

        private string text;
        private string slovo;

        public SearchSposobLinq(string Text, string Slovo)
        {
            this.text = Text;
            this.slovo = Slovo;

        }
        public void DoSearch()
        {

            char[] separators = { ' ', ',', '.' };
            string[] textArray = text.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);//преобразовали текст в массив


            var selectedWords = textArray.Where(p => p.Contains(slovo.ToLower())).FirstOrDefault();


            if (selectedWords != null)
            {
                Sovpadenie = true;
            }
        }
    }
}
