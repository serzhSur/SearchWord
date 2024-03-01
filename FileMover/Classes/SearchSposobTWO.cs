using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class SearchSposobTWO : ISearch
    {
        public bool sovpadenie { get; set; }
        public int matchCount { get; set; }
        public string otchet { get; set; } = "";

        public string text;
        public string keyWord;
        public SearchSposobTWO(string text, string keyWord)
        {
            this.text = text.ToLower();
            this.keyWord = keyWord.ToLower();


        }
        public void DoSearch()
        {

            int keyIndex = 0;

            sovpadenie = false;

                keyIndex = text.IndexOf(keyWord);

                if (keyIndex > 0)
                {
                   
                    sovpadenie = true;
                }

        }
    }
}
