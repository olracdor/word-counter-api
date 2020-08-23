using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.Cosmos.Table;
using Serilog;
using WordCounter.Database;
using WordCounter.Models;
namespace WordCounter.Commands
{
   

    public class SaveWordCountCommand : IRequest<IList<TableResult>>
    {
        public List<Word> words { get; set; }
        public string Url { get; set; }
    }

    public class SaveWordCountCommandHandler : IRequestHandler<SaveWordCountCommand, IList<TableResult>>
    {
       
        private WordCountContext _wordCountContext;
        public SaveWordCountCommandHandler(WordCountContext wordCountContext)
        {
            _wordCountContext = wordCountContext;
        }

        public async Task<IList<TableResult>> Handle(SaveWordCountCommand request, CancellationToken cancellationToken)
        {
            foreach(Word word in request.words)
            {
                _wordCountContext.Add(new WordCount(word.word, word.count, request.Url));
            }

            _ = _wordCountContext.SaveChanges();



            return await _wordCountContext.SaveChanges();
        }
    }
}
