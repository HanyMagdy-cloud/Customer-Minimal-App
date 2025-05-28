using CustomerManagementApp.Data.Entities;
using CustomerManagementApp.Data.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CustomerManagementApp.Data.Repos
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly SendGridClient _client;

        public EmailNotificationService(IConfiguration configuration, ILogger<EmailNotificationService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var apiKey = _configuration["SendGridApiKey"];
            _client = new SendGridClient(apiKey);
        }

        public async Task NotifySalesRepAsync(Customer customer)
        {
            if (customer?.ResponsibleSalesRep?.Email != null)
            {
                var from = new EmailAddress(_configuration["SendGridFromEmail"], "Customer Notification Service"); // Configure in settings
                var to = new EmailAddress(customer.ResponsibleSalesRep.Email, customer.ResponsibleSalesRep.Name);
                var subject = $"New/Updated Customer - {customer.Name}";
                var body = $"A new/updated customer has been assigned to you:\n\n" +
                           $"- Name: {customer.Name}\n" +
                           $"- Title: {customer.Title}\n" +
                           $"- Phone: {customer.Phone}\n" +
                           $"- Email: {customer.Email}\n" +
                           $"- Address: {customer.Address}";
                var plainTextBody = body; // For clients that don't support HTML

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextBody, body);

                var response = await _client.SendEmailAsync(msg);

                _logger.LogInformation($"SendGrid Response Status Code: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Body.ReadAsStringAsync();
                    _logger.LogError($"SendGrid Error: {errorBody}");
                }
            }
            else
            {
                _logger.LogWarning($"Could not send notification for customer '{customer?.Name}' as the responsible salesperson's email is missing.");
            }
        }
    }
}