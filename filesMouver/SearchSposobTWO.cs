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
<<<<<<< HEAD
        public string otchet { get; set; } = "";

=======
        public string otchet { get; set; }
        
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
        public string text;
        public string keyWord;
        public SearchSposobTWO(string text, string keyWord)
        {
<<<<<<< HEAD
            this.text = text.ToLower();
            this.keyWord = keyWord.ToLower();
=======
            this.text = text;
            this.keyWord = keyWord;
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
            

        }
        public void DoSearch()
        {
<<<<<<< HEAD

=======
            //text = File.ReadAllText("C:\\C#_projects\\LiNQ\\test\\ключ_sur804.txt");
            //string key = "sur";//"123456789";
            
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
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

<<<<<<< HEAD
            otchet = $"sovpadenie:{sovpadenie}\tnamberMatch:{matchCount}\tword:{keyWord}";
           
=======
            otchet = $"\r\nword:{keyWord} namberMatch:{matchCount}  sovpadenie:{sovpadenie}";
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
        }
    }
}
