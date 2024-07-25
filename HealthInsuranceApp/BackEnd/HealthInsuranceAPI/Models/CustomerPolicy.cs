namespace HealthInsuranceAPI.Models
{
    public class CustomerPolicy
    {
        public int CustomerPolicyID { get; set; }
        public int CustomerID { get; set; }
        public int PolicyID { get; set; }
        public PolicyStatus Status { get; set; }
        public bool DiscountEligibility { get; set; }
        public Customer Customer { get; set; }
        public InsurancePolicy InsurancePolicy { get; set; }

        public ICollection<Claim> Claims { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

    public enum PolicyStatus
    {
        Active,
        Expired
    }
}
