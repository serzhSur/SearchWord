using FilesMouver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filesMove
{
    internal class SearchSposobLINQ: ISearch
    {
        public bool sovpadenie { get; set; }
        public int matchCount { get; set; }
        public string otchet { get; set; } = "";

        public string text;
        public string slovo;

        public SearchSposobLINQ(string text, string slovo)
        {
            this.text = text.ToLower();
            this.slovo = slovo.ToLower();

        }
        public void DoSearch() 
        {
            
        }

    }
}
