using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramChecker
{
    class WordPair
    {
        public string word1 { get; set; }
        public string word2 { get; set; }

        public WordPair(string word1, string word2)
        {
            this.word1 = word1;
            this.word2 = word2;
        }
    }
}
