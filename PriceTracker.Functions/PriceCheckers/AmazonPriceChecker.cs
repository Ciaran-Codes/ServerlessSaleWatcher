using Microsoft.Extensions.Logging;

namespace PriceTracker.Functions.PriceCheckers;

public class AmazonPriceChecker : IPriceChecker
{
    private readonly ILogger<AmazonPriceChecker> _logger;

    public AmazonPriceChecker(ILogger<AmazonPriceChecker> logger)
    {
        _logger = logger;
    }

    public async Task<decimal?> CheckProductPriceAsync(string productUrl)
    {
        _logger.LogInformation("Simulating price check for Amazon URL: {url}", productUrl);

        // MOCK: Replace with real scraping logic later
        await Task.Delay(100);
        var random = new Random();
        return Math.Round((decimal)(random.NextDouble() * 100 + 10), 2);
    }
}
