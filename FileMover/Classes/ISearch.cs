using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesMove.Classes
{
    internal interface ISearch
    {
        bool Sovpadenie { get; set; }
        string Name { get; set; }

        void DoSearch();
    }
}
