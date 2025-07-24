using Microsoft.Extensions.Logging;
using PriceTracker.Functions.Models;

namespace PriceTracker.Functions.Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendPriceDropEmailAsync(string email, string productUrl, decimal newPrice)
    {
        _logger.LogInformation("Email sent to {email} — new price for {url}: {price:C}", email, productUrl, newPrice);

        // TODO: Replace with real SendGrid or Logic App call
        return Task.CompletedTask;
    }
}
