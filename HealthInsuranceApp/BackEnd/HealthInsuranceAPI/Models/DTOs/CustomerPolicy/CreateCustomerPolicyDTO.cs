namespace HealthInsuranceAPI.Models.DTOs.CustomerPolicy
{
    public class CreateCustomerPolicyDTO
    {
        public Guid PolicyID { get; set; }
        public PolicyStatus Status { get; set; }
    }
}
