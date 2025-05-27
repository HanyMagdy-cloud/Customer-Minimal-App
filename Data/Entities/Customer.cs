namespace CustomerManagementApp.Data.Entities
{
    public class Customer
    {
        public string id { get; set; } = Guid.NewGuid().ToString(); // Unique identifier for the customer
        public string Name { get; set; } = string.Empty; // Customer's name
        public string Title { get; set; } = string.Empty; // Customer's title
        public string Phone { get; set; } = string.Empty; // Phone number
        public string Email { get; set; } = string.Empty; // Email address
        public string Address { get; set; } = string.Empty; // Physical address
        public SalesMan ResponsibleSalesRep { get; set; } = new(); // Reference to the responsible sales rep
    }
}
