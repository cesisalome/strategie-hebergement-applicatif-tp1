using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;

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
        public async Task Run([QueueTrigger("queue", Connection = "")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var entity = new TableEntity("PartitionKey", "RowKey")
            {
                {"Message", myQueueItem}
            };

            await _tableClient.UpsertEntityAsync(entity);
        }
    }
}
