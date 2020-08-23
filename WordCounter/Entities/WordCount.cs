using System;
using Microsoft.Azure.Cosmos.Table;

namespace WordCounter.Models
{
    public class WordCount: TableEntity
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public WordCount()
        {
        }
        public WordCount(string word, int count, string url)
        {
            Word = word;
            Count = count;
            // replace forward slash with dash as its invalid as partition key 
            PartitionKey = url.Replace("/","-");
            RowKey = Guid.NewGuid().ToString();

        }
    }
}
