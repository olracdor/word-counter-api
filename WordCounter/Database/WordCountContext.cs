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

        /// <summary>
        /// ToDo: words parameter needs to be ordered - need to restrict in to accept IOrderedEnumerable
        /// </summary>
        /// <param name="words">List of words to be upsert</param>
        /// <param name="partitionKey">partion key for checking</param>
        public async Task<IList<TableResult>> UpsertWords(List<Word> words, string partitionKey)
        {
            //Azure table does not allow forward slash as a partition key
            partitionKey = partitionKey.Replace("/", "-");

            TableQuery<WordCount> query = new TableQuery<WordCount>().Where(
                           TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            //TODO: Thinking of making this method reusable but the ExecuteQuery does not allow absract
            //- need to use something like where T : new()
            IEnumerable<WordCount> entities = table.ExecuteQuery(query).OrderByDescending(e => e.Count);
            if (entities.Count() > 0)
            {
                foreach (WordCount entity in entities)
                {
                    Word word = words.Find(word => word.word.CompareTo(entity.Word)==0);
                    if (word == null)
                        Add(new WordCount(word.word, word.count, partitionKey));
                    else {
                        entity.Word = word.word;
                        entity.Count = word.count;
                        Add(entity);
                    }
                }
            }
            else
            {
                foreach (Word word in words)
                {
                    Add(new WordCount(word.word, word.count, partitionKey));
                }

              
            }

            return await SaveChanges();

        }
    }


   
}
