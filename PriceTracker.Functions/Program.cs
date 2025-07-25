using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceTracker.Functions;
using PriceTracker.Functions.PriceCheckers;
using PriceTracker.Functions.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton<StorageService>();
builder.Services.AddSingleton<QueueService>();
builder.Services.AddTransient<AmazonPriceChecker>();
builder.Services.AddSingleton<PriceCheckerFactory>();

builder.Services.AddSingleton<ProcessSubmissionFunction>();


builder.Build().Run();
