using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
    internal class StartSearch
    {
        public bool sovpadenie { get; set; }
        public int matchCount { get; set; }
<<<<<<< HEAD
        public string otchet { get; set; } = "";

=======
        public string otchet { get; set; }
        //public bool find;
        //public int sovpadenieCount;
>>>>>>> 5845eb6 (добавил свойства в интерфейс)

        public void FinedWord(ISearch sposob)
        {
            if (sposob != null)
            {

<<<<<<< HEAD
                sposob.DoSearch();

                sovpadenie = sposob.sovpadenie;
                matchCount = sposob.matchCount;
                otchet = $"{sposob} {sposob.otchet}"; 
 
=======
                sposob.DoSearch();//out bool find, out int sovpadenieCount);
                //this.find = find;
                //this.sovpadenieCount = sovpadenieCount;
                sovpadenie = sposob.sovpadenie;
                matchCount = sposob.matchCount;
                otchet = sposob.otchet;
>>>>>>> 5845eb6 (добавил свойства в интерфейс)
            }

        }
    }
}
