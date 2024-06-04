using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FilesMove.Classes
{
    internal class SearchSposobRegex : ISearch
    {
        public bool Sovpadenie { get; set; } = false;
        public string Name { get ; set ; } = "SearchSposob Regex";

        public string text;
        public string slovo;

        public SearchSposobRegex(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
        }
        public void DoSearch()
        {
            Match match = Regex.Match(text, slovo, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Sovpadenie = true;
            }
        }
    }
}
