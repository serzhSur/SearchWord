using FilesMouver;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filesMove
{
    internal class SearchSposobLINQ: ISearch
    {
        public bool sovpadenie { get; set; }=false;
        public int matchCount { get; set; } = 0;
        public string otchet { get; set; } = "";

        public string text;
        public string slovo;

        public SearchSposobLINQ(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo.ToLower();

        }
        public void DoSearch() 
        {
            
            char[] separators = { ' ', ',' };
            string[] textArray = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);//преобразовали текст в массив, слово-элемент мас

            var selectedWords = textArray.Where(w => w.ToLower().StartsWith(slovo));

            matchCount = selectedWords.Count();

            if (matchCount > 0) { sovpadenie = true; }

            otchet = $"sovpadenie:{sovpadenie}\tnamberMatch:{matchCount}\tword:{slovo}";

        }

    }
}
