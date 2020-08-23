using System;
using System.Collections.Generic;

namespace WordCounter.Models
{
    public class Word : IComparable<Word>
    { 
        public string word { get; set; }
        public int count { get; set; }
        public int CompareTo(Word c)
        {
            return string.Compare(word, c.word);
        }
    }
}
