using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMover.Classes
{
    public class SearchResult
    {
        public int id { get; set; }
        public string file_name { get; set; }
        public string dir_in { get; set; }
        public string key_word { get; set; }
        public bool match { get; set; }
        public string dir_out { get; set; }
        public string day_time { get; set; }
        public int count { get; set; }
        public SearchResult() { }
        public SearchResult(int id, string file_name, string dir_in, string key_word, bool match, string dir_out, string day_time)
        {
            this.id = id;
            this.file_name = file_name;
            this.dir_in = dir_in;
            this.key_word = key_word;
            this.match = match;
            this.dir_out = dir_out;
            this.day_time = day_time;
        }
    }
}
