using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using PriceTracker.Functions.Models;

namespace PriceTracker.Functions.Services;

public class StorageService
{
    private readonly TableClient _tableClient;

    public StorageService(IConfiguration config)
    {
        var storageConnection = config["StorageConnectionString"];
        var tableName = "ProductSubscriptions";

        _tableClient = new TableClient(storageConnection, tableName);
        _tableClient.CreateIfNotExists();
    }

    public async Task SaveSubscriptionAsync(SubmitProductRequest request)
    {
        var entity = new ProductSubscriptionEntity
        {
            ProductUrl = request.ProductUrl,
            Email = request.Email
        };

        await _tableClient.AddEntityAsync(entity);
    }

    public async Task RemoveSubscriptionAsync(string email, string productUrl)
    {
        var entity = new ProductSubscriptionEntity
        {
            PartitionKey = email,
            RowKey = productUrl
        };
        await _tableClient.DeleteEntityAsync(entity.PartitionKey, entity.RowKey);
    }
}
