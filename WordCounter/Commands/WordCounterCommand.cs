using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MediatR;
using Serilog;
using WordCounter.Models;
using WordCounter.Extensions;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace WordCounter.Commands
{

    public class WordCounterCommand : IRequest<WordResponse>
    {
        public string Url { get; set; }
    }

    public class WordCounterCommandHandler : IRequestHandler<WordCounterCommand, WordResponse>
    {
        private ILogger _logger;
        private readonly IMediator _mediator;
        private string _regexFilterExp;
        public WordCounterCommandHandler(ILogger logger, IMediator mediator, IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _regexFilterExp = configuration["Words:regexFilterExp"];
        }

        public async Task<WordResponse> Handle(WordCounterCommand request, CancellationToken cancellationToken)
        {
            List<Word> words = new List<Word>();
            
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(request.Url));
            
           var root = html.DocumentNode;

            var res = root.SelectNodes("//body").Descendants();

            foreach (HtmlNode node in res)
            {
                if (node.Name.CompareTo("#text") == 0) { 
                    foreach (string text in node.InnerText.Split(null))
                    {
                        MatchCollection mc = Regex.Matches(text, _regexFilterExp);

                        if (mc.Count > 0)
                        {
                            words.GetValueOrAddIfEmpty(() => new Word { word = text, count = 1 }
                            , word => word.word.CompareTo(text) == 0).count +=1;
                        } 
                    }
                }
                
            }

            words = words.SortedTopWords(100);
            int avg = (int)words.Average(word => word.count);

            _ = _mediator.Send(new SaveWordCountCommand { Url = request.Url, words= words });
            
            return new WordResponse(avg, words);
            
        }
    }
    
}
