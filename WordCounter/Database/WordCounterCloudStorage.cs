using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Serilog;
namespace WordCounter.Database
{
    public class WordCounterCloudStorage
    {
        private IConfiguration Configuration { get; }
        private readonly CloudTableClient tableClient;
        private ILogger Logger { get; }

        public WordCounterCloudStorage(IConfiguration configuration, ILogger logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                   Configuration["AzureStorage:connectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            //Persist tables
            CloudTable table = tableClient.GetTableReference(Configuration["AzureStorage:tableName"]);
            if (table.CreateIfNotExists())
                Logger.Information($"Table {table.Name} Successfully created");
        }

        public CloudTableClient getCloudTableClient()
        {
            return this.tableClient;
        }
    }
}
