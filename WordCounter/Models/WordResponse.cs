using System;
using System.Collections.Generic;

namespace WordCounter.Models
{
    public class WordResponse
    {
        public int average { get; set; }
        public List<Word> words { get; set; }
        public WordResponse() { }
        
        public WordResponse(int average, List<Word> words)
        {
            this.average = average;
            this.words = words;

        }
    }
}
