using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class SearchSposobTwo : ISearch
    {
        public bool Sovpadenie { get; set; }=false;
        public string Name { get; set; } = "SearchSposob IndexOf";

        private string text;
        private string slovo;
        public SearchSposobTwo(string Text, string Slovo)
        {
            this.text = Text;
            this.slovo = Slovo;

        }
        public void DoSearch()
        {

            int keyIndex = 0;

            keyIndex = text.ToLower().IndexOf(slovo.ToLower());

            if (keyIndex > 0)
            {
                Sovpadenie = true;
            }

        }
    }
}
