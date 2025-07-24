namespace PriceTracker.Functions.PriceCheckers;

public interface IPriceChecker
{
    Task<decimal?> CheckProductPriceAsync(string productUrl);
}
