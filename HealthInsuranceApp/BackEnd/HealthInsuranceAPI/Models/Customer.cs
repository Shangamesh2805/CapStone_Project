namespace HealthInsuranceAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }
        public ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
