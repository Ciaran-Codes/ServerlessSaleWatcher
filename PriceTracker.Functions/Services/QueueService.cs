using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using PriceTracker.Functions.Models;
using System.Text.Json;

namespace PriceTracker.Functions.Services;

public class QueueService
{
    private readonly QueueClient _queueClient;

    public QueueService(IConfiguration config)
    {
        var connectionString = config["StorageConnectionString"];
        var queueName = "product-submissions";

        var clientOptions = new QueueClientOptions(QueueClientOptions.ServiceVersion.V2021_06_08);

        _queueClient = new QueueClient(connectionString, queueName, clientOptions);
        _queueClient.CreateIfNotExists();
    }

    public async Task EnqueueSubmissionAsync(SubmitProductRequest request)
    {
        var message = JsonSerializer.Serialize(request);
        var encodedMessage = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message));
        await _queueClient.SendMessageAsync(encodedMessage);
    }
}
