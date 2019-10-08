using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramChecker
{
    class WordPair
    {
        public string Word1 { get; }
        public string Word2 { get; }

        public WordPair(string word1, string word2)
        {
            this.Word1 = word1;
            this.Word2 = word2;
        }
    }
}
