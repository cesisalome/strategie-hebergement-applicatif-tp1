using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using System.Text;

namespace MyAzureFunctions
{
    public class HttpTriggerFunction
    {
        private readonly ILogger<HttpTriggerFunction> _logger;
        private readonly QueueClient _queueClient;

        public HttpTriggerFunction(ILogger<HttpTriggerFunction> logger, QueueClient queueClient)
        {
            _logger = logger;
            _queueClient = queueClient;
        }

        [Function("HttpTriggerFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a request.");

            var plainTextBytes = Encoding.UTF8.GetBytes("test");
            await _queueClient.SendMessageAsync(Convert.ToBase64String(plainTextBytes));

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteStringAsync("Message sent to queue.");
            return response;
        }
    }
}
