using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Serilog;
using WordCounter.Models;

namespace WordCounter.Database
{
    public class DatabaseContext<T> where T : TableEntity
    {

        private CloudTableClient tableClient { set; get; }
        protected CloudTable table { set; get; }
        private ILogger Logger { set; get; }
        private List<T> entities { get; set; }

        public DatabaseContext(WordCounterCloudStorage wordCounterCloudStorage, string tableName, ILogger logger)
        {
            Logger = logger;
            tableClient = wordCounterCloudStorage.getCloudTableClient();
            table = tableClient.GetTableReference(tableName);
            entities = new List<T>();
        }

        public void Add(T value)
        {
            entities.Add(value);
        }

        public void Remove(T value)
        {
            entities.Remove(value);
        }

        public async Task<IList<TableResult>> SaveChanges()
        {
            IList<TableResult> results = null;
            try
            {
                TableBatchOperation operations = new TableBatchOperation();
                foreach (T entity in entities)
                {
                    operations.InsertOrMerge(entity);
                }
                results = await table.ExecuteBatchAsync(operations);
                Logger.Information($"Save successful {results}");
                entities.Clear();
            }
            catch (StorageException e){
                Logger.Error(e.RequestInformation.ExtendedErrorInformation.ErrorMessage);
            } catch(Exception e){
                Logger.Error(e.Message);
            }
            return results;
        }
    }
}
