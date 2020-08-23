
using System.Collections.Generic;
using System.Linq;
using WordCounter.Models;

namespace WordCounter.Extensions
{
    public static class WordsExtension
    {
        // <summary>
        /// Sorts and gets the top X list of Words by counts.
        /// Added to help 
        /// </summary>
        /// <param name="top">Top</param>
        public static List<Word> SortedTopWords(this IEnumerable<Word> words, int top) {
            if (words == null) return Enumerable.Empty<Word>().ToList();
            words = words.OrderByDescending(word => word.count);
            words = words.Take(top);
            return words.ToList();
        }

        
    }
}
