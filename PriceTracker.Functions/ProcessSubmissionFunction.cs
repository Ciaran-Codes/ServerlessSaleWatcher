using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using PriceTracker.Functions.Models;

namespace PriceTracker.Functions;

public class ProcessSubmissionFunction
{
    private readonly ILogger<ProcessSubmissionFunction> _logger;

    public ProcessSubmissionFunction(ILogger<ProcessSubmissionFunction> logger)
    {
        _logger = logger;
    }

    [Function("ProcessSubmission")]
    public void Run(
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

            // TODO: Add price checking, webhook trigger, etc.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to process queue message: {msg}", queueMessage);
        }
    }
}
