using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FilesMove.Classes
{
    internal class SearchSposobRegex : ISearch
    {
        public bool sovpadenie { get; set; } = false;
        public int matchCount { get; set; } = 0;
        public string otchet { get; set; } = "";

        public string text;
        public string slovo;

        public SearchSposobRegex(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
        }
        public void DoSearch()
        {
            Regex regex = new Regex($@"\w*{slovo}\w*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection matches = regex.Matches(text);

            matchCount = matches.Count;

            if (matchCount > 0) 
            { 
                sovpadenie = true; 
            }

            otchet = $"SearchSposobRegex sovpadenie:{sovpadenie}\tnamberMatch:{matchCount}\tword:{slovo}";

        }
    }
}
