using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PriceTracker.Functions.Services;
using System.Net;

namespace PriceTracker.Functions;

public class UnsubscribeFunction
{
    private readonly ILogger<UnsubscribeFunction> _logger;
    private readonly StorageService _storage;

    public UnsubscribeFunction(ILogger<UnsubscribeFunction> logger, StorageService storage)
    {
        _logger = logger;
        _storage = storage;
    }

    [Function("Unsubscribe")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "unsubscribe")] HttpRequestData req)
    {
        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var email = query["email"];
        var productUrl = query["url"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(productUrl))
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Missing 'email' or 'url' query parameters.");
            return badResponse;
        }

        await _storage.RemoveSubscriptionAsync(email, productUrl);
        _logger.LogInformation("Unsubscribed {Email} from {Url}", email, productUrl);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"You've been unsubscribed from alerts for {productUrl}");
        return response;
    }
}
