namespace HealthInsuranceAPI.Models.DTOs.Customers
{
    public class CreateCustomerDTO
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
