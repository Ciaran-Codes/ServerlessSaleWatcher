using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace PriceTracker.Functions;

public class SubmitProductFunction
{
    private readonly ILogger<SubmitProductFunction> _logger;

    public SubmitProductFunction(ILogger<SubmitProductFunction> logger)
    {
        _logger = logger;
    }

    [Function("SubmitProduct")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Received SubmitProduct request");

        var request = await req.ReadFromJsonAsync<SubmitProductRequest>();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"URL: {request?.ProductUrl}, Email: {request?.Email}");

        return response;
    }
}