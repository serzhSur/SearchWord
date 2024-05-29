using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMover.Classes
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string DirIn { get; set; }
        public string KeyWord { get; set; }
        public bool Match { get; set; }
        public string DirOut { get; set; }
        public string DayTime { get; set; }
        public SearchResult() { }
        public SearchResult(string file_name, string dir_in, string key_word, bool match, string dir_out, string day_time)
        {
            //Id = id;
            FileName = file_name;
            DirIn = dir_in;
            KeyWord = key_word;
            Match = match;
            DirOut = dir_out;
            DayTime = day_time;
        }
    }
}
