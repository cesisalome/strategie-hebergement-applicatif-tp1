using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using System.Text;

namespace MyAzureFunctions
{
    public class QueueTriggerFunction
    {
        private readonly ILogger<QueueTriggerFunction> _logger;
        private readonly TableClient _tableClient;

        public QueueTriggerFunction(ILogger<QueueTriggerFunction> logger, TableClient tableClient)
        {
            _logger = logger;
            _tableClient = tableClient;
        }

        [Function("QueueTriggerFunction")]
        public async Task Run([QueueTrigger("test", Connection = "AzureWebJobsStorage")] byte[] myQueueItem)
        {
            var message = Encoding.UTF8.GetString(myQueueItem);
            _logger.LogInformation($"C# Queue trigger function processed: {message}");

            var entity = new TableEntity("PartitionKey", "RowKey")
            {
                {"Message", message}
            };

            await _tableClient.UpsertEntityAsync(entity);
        }
    }
}
