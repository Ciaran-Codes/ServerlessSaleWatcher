using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PriceTracker.Functions.Models;
using PriceTracker.Functions.Services;

namespace PriceTracker.Functions;

public class SubmitProductFunction
{
    private readonly ILogger<SubmitProductFunction> _logger;
    private readonly StorageService _storage;
    private readonly QueueService _queue;

    public SubmitProductFunction(ILogger<SubmitProductFunction> logger, StorageService storage, QueueService queue)
    {
        _logger = logger;
        _storage = storage;
        _queue = queue;
    }

    [Function("SubmitProduct")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Received SubmitProduct request");

        var request = await req.ReadFromJsonAsync<SubmitProductRequest>();

        if (request == null || string.IsNullOrWhiteSpace(request.ProductUrl) || string.IsNullOrWhiteSpace(request.Email))
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Missing product URL or email.");
            return badResponse;
        }

        await _storage.SaveSubscriptionAsync(request);
        await _queue.EnqueueSubmissionAsync(request);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Subscription saved.");
        return response;
    }
}
