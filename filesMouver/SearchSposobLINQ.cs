﻿using FilesMouver;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filesMove
{
    internal class SearchSposobLINQ: ISearch
    {
        public bool sovpadenie { get; set; }=false;
        public int matchCount { get; set; } = 0;
        public string otchet { get; set; } = "";

        public string text;
        public string slovo;

        public SearchSposobLINQ(string text, string slovo)
        {
            this.text = text;
            this.slovo = slovo.ToLower();

        }
        public void DoSearch() 
        {
            
            char[] separators = { ' ', ',','.','-' };
            string[] textArray = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);//преобразовали текст в массив, слово-элемент мас
            
            // операторы запросов LINQ
            var selectedWords = from w in textArray
                                where w.ToLower().Contains(slovo) //StartsWith(slovo)
                                select w;

            matchCount = selectedWords.Count();

            if (matchCount > 0) { sovpadenie = true; }

            otchet = $"SearchSposobLINQ sovpadenie:{sovpadenie}\tnamberMatch:{matchCount}\tword:{slovo}";

        }

    }
}
