using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add the chat application
builder.AddProject<Projects.Chat>("chat");

// Add the Customer API
builder.AddProject<Projects.Customer_API>("customerapi");

builder.AddProject<Projects.WorkerService>("worker");

builder.Build().Run();
