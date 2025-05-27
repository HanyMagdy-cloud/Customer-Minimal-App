using CustomerManagementApp.Data.Entities;

namespace CustomerManagementApp.Data.Dtos
{
    public class CustomerCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public SalesMan ResponsibleSalesRep { get; set; } = new(); // Reference to the responsible sales rep
    }
}
