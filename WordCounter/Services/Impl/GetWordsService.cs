using System.Linq;
using WordCounter.Database;
using WordCounter.Models;

namespace WordCounter.Services.Impl
{
    public class GetWordsService : IGetWordsService
    {
        private WordCountContext _wordCountContext;
        public GetWordsService(WordCountContext wordCountContext)
        {

            _wordCountContext = wordCountContext;
        }
        public IOrderedEnumerable<WordCount> GetWords()
        {
            return _wordCountContext.GetWords().OrderByDescending(word => word.Count);
        }
    }
}
