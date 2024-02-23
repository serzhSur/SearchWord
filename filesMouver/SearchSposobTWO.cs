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
        public string text;
        public string slovo;
        public string otchet="";
        public SearchSposobTWO(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;
            
        }
        public void DoSearch(out bool sovpadenie, out int count)
        {
            //string text = File.ReadAllText("C:\\C#_projects\\LiNQ\\test\\ключ_sur804.txt");
            //string key = "sur";//"123456789";
            int keyLength = slovo.Length;
            int keyIndex = 0;
            count = 0;
            sovpadenie = false;

            do
            {
                keyIndex = text.IndexOf(slovo);

                if (keyIndex > 0)
                {
                    count += 1;
                    sovpadenie = true;

                    text = text.Remove(0, keyIndex + (keyLength - 1));
                }
            }
            while (keyIndex > 0);

            otchet = $"word:{slovo} namberMatch:{count}  sovpadenie:{sovpadenie}\r\n";
        }
    }
}
