using CustomerManagementApp.Data.Dtos;
using CustomerManagementApp.Data.Entities;
using CustomerManagementApp.Data.Interface;
using Microsoft.AspNetCore.Mvc;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        // POST /customers - Add a new customer
        app.MapPost("/customers", async (
            [FromBody] CustomerCreateDto input,
            [FromServices] ICustomerRepo db,
            [FromServices] IEmailNotificationService email
        ) =>
        {
            var customer = new Customer
            {
                id = Guid.NewGuid().ToString(),
                Name = input.Name,
                Title = input.Title,
                Phone = input.Phone,
                Email = input.Email,
                Address = input.Address,
                ResponsibleSalesRep = input.ResponsibleSalesRep
            };

            await db.AddCustomerAsync(customer);
            await email.NotifySalesRepAsync(customer);

            return Results.Created($"/customers/{customer.id}", customer);
        });

        // PUT /customers/{id} - Update an existing customer
        app.MapPut("/customers/{id}", async (
            string id,
            [FromBody] CustomerCreateDto input,
            [FromServices] ICustomerRepo db
        ) =>
        {
            var existing = await db.GetCustomerByIdAsync(id);
            if (existing == null) return Results.NotFound();

            var updatedCustomer = new Customer
            {
                id = id, 
                Name = input.Name,
                Title = input.Title,
                Phone = input.Phone,
                Email = input.Email,
                Address = input.Address,
                ResponsibleSalesRep = input.ResponsibleSalesRep
            };

            await db.UpdateCustomerAsync(updatedCustomer);
            return Results.Ok(updatedCustomer);
        });

        // DELETE /customers/{id} - Remove a customer
        app.MapDelete("/customers/{id}", async (
            string id,
            [FromServices] ICustomerRepo db
        ) =>
        {
            var existing = await db.GetCustomerByIdAsync(id);
            if (existing == null) return Results.NotFound();

            await db.DeleteCustomerAsync(id);
            return Results.NoContent(); // 204
        });

        // GET /customers/search?name=...&salesRep=... - Search customers
        app.MapGet("/customers/search", async (
            [FromQuery] string? name,
            [FromQuery] string? salesRep,
            [FromServices] ICustomerRepo db
        ) =>
        {
            var results = await db.SearchCustomersAsync(name, salesRep);
            return Results.Ok(results);
        });

        // GET /customers - List all customers
        app.MapGet("/customers", async (
            [FromServices] ICustomerRepo db
        ) =>
        {
            var customers = await db.GetAllCustomersAsync();
            return Results.Ok(customers);
        });

        // (Optional) GET /customers/{id} - Get by ID
        app.MapGet("/customers/{id}", async (
            string id,
            [FromServices] ICustomerRepo db
        ) =>
        {
            var customer = await db.GetCustomerByIdAsync(id);
            return customer is not null ? Results.Ok(customer) : Results.NotFound();
        });
    }
}
