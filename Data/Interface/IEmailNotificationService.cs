using CustomerManagementApp.Data.Entities;

namespace CustomerManagementApp.Data.Interface
{
    public interface IEmailNotificationService
    {
        Task NotifySalesRepAsync(Customer customer);

    }
}
