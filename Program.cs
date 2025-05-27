using CustomerManagementApp.Data.Extensions;          // For SwaggerExtensions
using CustomerManagementApp.Data.Interface;           // For service interfaces
using CustomerManagementApp.Data.Repos;               // For service implementations

var builder = WebApplication.CreateBuilder(args);

// Enable minimal API endpoint discovery (required for Swagger)
builder.Services.AddEndpointsApiExplorer();

// Register application services
builder.Services.AddSingleton<ICustomerRepo, CustomerRepo>();         // Cosmos DB service
builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>(); // Email service

// Register Swagger (via extension method)
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Enable Swagger middleware and UI (via extension method)
app.UseSwaggerDocumentation();

// Map all customer-related endpoints
app.MapCustomerEndpoints();

// Run the web app
app.Run();
