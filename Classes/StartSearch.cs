using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace findeWords
{
    internal class StartSearch
    {
        public string text;
        public string slovo;

        public int match;
        public StartSearch(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
        }
        public void FinedWord(ISearch sposob)
        {
            if (sposob != null)
            {

                sposob.DoSearch();

            }
            SearchSposobOne sp1 = new SearchSposobOne(text, slovo);
            sp1.DoSearch();
            match = sp1.sovpadenie;
        }
    }
}
