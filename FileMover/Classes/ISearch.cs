using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal interface ISearch
    {
        bool sovpadenie { get; set; }
        int matchCount { get; set; }
        string otchet { get; set; }
        void DoSearch();
    }
}
