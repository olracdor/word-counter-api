
using System.Collections.Generic;
using System.Linq;
using WordCounter.Models;

namespace WordCounter.Extensions
{
    public static class WordsExtension
    {
        // <summary>
        /// Checks if the value exists and automatically add and return the new object if it does not exist
        /// </summary>
        /// <param name="top">Expression that return the default value</param>
        /// <param name="findExpression">Expression that checks if the object exists in the list</param>
        public static List<Word> SortedTopWords(this IEnumerable<Word> words, int top) {
            if (words == null) return Enumerable.Empty<Word>().ToList();
            words = words.OrderByDescending(word => word.count);
            words = words.Take(top);
            return words.ToList();
        }

        
    }
}
