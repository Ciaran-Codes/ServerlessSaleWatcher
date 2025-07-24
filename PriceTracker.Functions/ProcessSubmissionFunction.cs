using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PriceTracker.Functions.Models;
using PriceTracker.Functions.Services;
using System.Text.Json;

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
    public async Task Run(
        [QueueTrigger("product-submissions", Connection = "StorageConnectionString")]
        string queueMessage)
    {
        try
        {
            var request = JsonSerializer.Deserialize<SubmitProductRequest>(queueMessage);

            if (request == null)
            {
                _logger.LogWarning("Received null or malformed message.");
                return;
            }

            _logger.LogInformation("Processing product submission: {url} from {email}",
                request.ProductUrl, request.Email);

            var checker = _priceCheckerFactory.GetChecker(request.ProductUrl);
            var price = await checker.CheckProductPriceAsync(request.ProductUrl);

            _logger.LogInformation("Checked price for {url}: {price}", request.ProductUrl, price);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process queue message: {msg}", queueMessage);
        }
    }
}
