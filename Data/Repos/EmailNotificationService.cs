using CustomerManagementApp.Data.Entities;
using CustomerManagementApp.Data.Interface;

namespace CustomerManagementApp.Data.Repos
{
    public class EmailNotificationService: IEmailNotificationService
    {
        // Simulates sending an email notification to a sales rep
        public Task NotifySalesRepAsync(Customer customer)
        {
            // Output message to console (for demo purposes)
            Console.WriteLine(" Email sent to sales rep:");
            Console.WriteLine($"To: {customer.ResponsibleSalesRep.Email}");
            Console.WriteLine($"Subject: New/Updated Customer - {customer.Name}");
            Console.WriteLine($"Message: Customer Info:\n" +
                              $"- Name: {customer.Name}\n" +
                              $"- Title: {customer.Title}\n" +
                              $"- Phone: {customer.Phone}\n" +
                              $"- Email: {customer.Email}\n" +
                              $"- Address: {customer.Address}");

            return Task.CompletedTask;
        }
    }
}
