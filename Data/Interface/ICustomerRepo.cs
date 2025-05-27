using CustomerManagementApp.Data.Entities;

namespace CustomerManagementApp.Data.Interface
{
    public interface ICustomerRepo
    {
        Task AddCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(string id);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task<IEnumerable<Customer>> SearchCustomersAsync(string? name, string? salesMan);
    }
}
