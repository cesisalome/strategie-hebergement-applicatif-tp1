using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Storage.Queues;
using Azure.Data.Tables;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(s =>
        {
            var connectionString = context.Configuration["AzureWebJobsStorage"];
            return new QueueClient(connectionString, "test");
        });

        services.AddSingleton(s =>
        {
            var connectionString = context.Configuration["AzureWebJobsStorage"];
            return new TableClient(connectionString, "test");
        });
    })
    .Build();

host.Run();
