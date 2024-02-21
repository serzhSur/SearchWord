using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMouver
{
    internal interface ISearch
    {
        void DoSearch(out bool find, out int sovpadenieCount);
    }
}
