using Microsoft.OpenApi.Models;

namespace CustomerManagementApp.Data.Extensions
{
    // Extension method to configure and register Swagger
    public static class SwaggerExtensions
    {
        // Registers Swagger services to the DI container
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // Add Swagger generation with basic configuration
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Customer API",
                    Version = "v1",
                    Description = "API for managing customers and sales representatives"
                });
            });

            return services;
        }

        // Adds middleware to serve Swagger UI and JSON docs
        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger(); // Enable Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
                c.RoutePrefix = string.Empty; // Serve at root URL (http://localhost:5000)
            });

            return app;
        }
    }
}
