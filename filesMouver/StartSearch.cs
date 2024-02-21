using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
    internal class StartSearch
    {
        public bool find;
        public int sovpadenieCount;

        public void FinedWord(ISearch sposob)
        {
            if (sposob != null)
            {

                sposob.DoSearch(out bool find, out int sovpadenieCount);
                this.find = find;
                this.sovpadenieCount = sovpadenieCount;
            }

        }
    }
}
