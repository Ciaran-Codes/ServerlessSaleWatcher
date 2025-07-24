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
    private readonly NotificationService _notifier;

    public ProcessSubmissionFunction(
    ILogger<ProcessSubmissionFunction> logger,
    PriceCheckerFactory priceCheckerFactory,
    NotificationService notifier)
    {
        _logger = logger;
        _priceCheckerFactory = priceCheckerFactory;
        _notifier = notifier;
    }

    [Function("ProcessSubmission")]
    public async Task Run(
        [QueueTrigger("product-submissions", Connection = "StorageConnectionString")]
        SubmitProductRequest request)
    {
        _logger.LogInformation("Running ProcessSubmission");

        try
        {
            //var request = JsonSerializer.Deserialize<SubmitProductRequest>(queueMessage);

            if (request == null)
            {
                _logger.LogWarning("Received null or malformed message.");
                return;
            }

            _logger.LogInformation("Processing product submission: {url} from {email}",
                request.ProductUrl, request.Email);

            var checker = _priceCheckerFactory.GetChecker(request.ProductUrl);
            var price = await checker.CheckProductPriceAsync(request.ProductUrl);

            if (price.HasValue)
            {
                await _notifier.SendPriceDropEmailAsync(request.Email, request.ProductUrl, price.Value);
            }
            else
            {
                _logger.LogWarning("Failed to retrieve price for {url}", request.ProductUrl);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process queue message");
        }
    }
}
