using CustomerManagementApp.Data.Entities;
using CustomerManagementApp.Data.Interface;
using Microsoft.Azure.Cosmos;


namespace CustomerManagementApp.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly Container _container; // Cosmos DB container instance


        // Constructor that initializes Cosmos DB client using config values
        public CustomerRepo(IConfiguration config)
        {
            // Create Cosmos client using connection string from appsettings
            var client = new CosmosClient(config["CosmosDb:ConnectionString"]);

            // Access the database and container using names from config
            var database = client.GetDatabase(config["CosmosDb:DatabaseName"]);
            _container = database.GetContainer(config["CosmosDb:ContainerName"]);
        }

        // Adds a new customer to the Cosmos DB container
        public async Task AddCustomerAsync(Customer customer)
        {
            
            await _container.CreateItemAsync(customer, new PartitionKey(customer.id));
        }


        // Gets all customers from the container
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var query = _container.GetItemQueryIterator<Customer>(); // Cosmos query
            List<Customer> results = new();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        // Gets a specific customer by their ID
        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Customer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // Updates an existing customer
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _container.UpsertItemAsync(customer, new PartitionKey(customer.id));
        }

        // Deletes a customer by their ID
        public async Task DeleteCustomerAsync(string id)
        {
            await _container.DeleteItemAsync<Customer>(id, new PartitionKey(id));
        }

        // Searches customers by name or responsible sales rep's name
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string? name, string? salesRep)
        {
            var queryString = "SELECT * FROM c WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(name))
                queryString += $" AND CONTAINS(c.Name, '{name}', true)"; 

            if (!string.IsNullOrWhiteSpace(salesRep))
                queryString += $" AND CONTAINS(c.ResponsibleSalesRep.Name, '{salesRep}', true)"; 

            var query = _container.GetItemQueryIterator<Customer>(new QueryDefinition(queryString));
            List<Customer> results = new();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

    }
}
