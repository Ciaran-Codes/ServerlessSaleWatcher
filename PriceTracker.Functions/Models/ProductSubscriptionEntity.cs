using Azure;
using Azure.Data.Tables;

namespace PriceTracker.Functions.Models;

public class ProductSubscriptionEntity : ITableEntity
{
    public string PartitionKey { get; set; } = "Subscriptions";
    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    public string ProductUrl { get; set; }
    public string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ETag ETag { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
}
