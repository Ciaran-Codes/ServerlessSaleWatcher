using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PriceTracker.Functions.Models;
using PriceTracker.Functions.Services;

namespace PriceTracker.Functions;

public class ProcessSubmissionFunction
{
    private readonly ILogger<ProcessSubmissionFunction> _logger;
    private readonly PriceCheckerFactory _priceCheckerFactory;

    public ProcessSubmissionFunction(
        ILogger<ProcessSubmissionFunction> logger,
        PriceCheckerFactory priceCheckerFactory)
    {
        _logger = logger;
        _priceCheckerFactory = priceCheckerFactory;
    }

    [Function("ProcessSubmission")]
    [QueueOutput("price-results", Connection = "StorageConnectionString")]
    public async Task<PriceCheckResult?> Run(
    [QueueTrigger("product-submissions", Connection = "StorageConnectionString")]
    SubmitProductRequest request)
    {
        try
        {
            if (request == null)
            {
                _logger.LogWarning("Received null or malformed message.");
                return null;
            }

            _logger.LogInformation("Processing product submission: {url} from {email}",
                request.ProductUrl, request.Email);

            var checker = _priceCheckerFactory.GetChecker(request.ProductUrl);
            var price = await checker.CheckProductPriceAsync(request.ProductUrl);

            if (price.HasValue)
            {
                return new PriceCheckResult
                {
                    ProductUrl = request.ProductUrl,
                    Email = request.Email,
                    NewPrice = price.Value
                };
            }
            else
            {
                _logger.LogWarning("Failed to retrieve price for {url}", request.ProductUrl);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process queue message");
            return null;
        }
    }

}
