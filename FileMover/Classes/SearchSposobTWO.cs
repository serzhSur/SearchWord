using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class SearchSposobTWO : ISearch
    {
        public bool sovpadenie { get; set; }=false;

        public string text;
        public string slovo;
        public SearchSposobTWO(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo;


        }
        public void DoSearch()
        {

            int keyIndex = 0;

            keyIndex = text.ToLower().IndexOf(slovo.ToLower());

            if (keyIndex > 0)
            {
                sovpadenie = true;
            }

        }
    }
}
