using WordCounter.Models;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using System.Threading.Tasks;

namespace WordCounter.Database
{
    public class WordCountContext : DatabaseContext<WordCount>
    {
        public WordCountContext(WordCounterCloudStorage wordCounterCloudStorage, IConfiguration configuration, ILogger logger)
            : base(wordCounterCloudStorage, configuration["AzureStorage:tableName"], logger)
        { }
        public List<WordCount> GetWords()
        {

            return table.ExecuteQuery(new TableQuery<WordCount>()).ToList();
        }
    }


   
}
