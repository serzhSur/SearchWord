using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesMouver;

namespace filesMove
{
    internal class SearchSposobTWO: ISearch
    {
        public bool sovpadenie { get; set; }
        public int matchCount { get; set; }
        public string otchet { get; set; }
        
        public string text;
        public string keyWord;
        public SearchSposobTWO(string text, string keyWord)
        {
            this.text = text;
            this.keyWord = keyWord;
            
        }
        public void DoSearch()
        {
            //text = File.ReadAllText("C:\\C#_projects\\LiNQ\\test\\ключ_sur804.txt");
            //string key = "sur";//"123456789";
            
            int keyIndex = 0;
            matchCount = 0;
            sovpadenie = false;

            do
            {
                keyIndex = text.IndexOf(keyWord);

                if (keyIndex > 0)
                {
                    matchCount += 1;
                    sovpadenie = true;

                    text = text.Remove(0, keyIndex + (keyWord.Length - 1));
                }
            }
            while (keyIndex > 0);

            otchet = $"\r\nword:{keyWord} namberMatch:{matchCount}  sovpadenie:{sovpadenie}";
        }
    }
}
