
using System.Linq;
using WordCounter.Models;

namespace WordCounter.Services
{
    public interface IGetWordsService
    {
        public IOrderedEnumerable<WordCount> GetWords();
    }
}
