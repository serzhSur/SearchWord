using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal class StartSearch
    {
        public bool sovpadenie { get; set; }

        public void FinedWord(ISearch sposob)
        {
            if (sposob != null)
            {
                sposob.DoSearch();

                sovpadenie = sposob.sovpadenie;
            }

        }
    }
}
