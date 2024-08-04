namespace HealthInsuranceAPI.Models.DTOs.CustomerPolicy
{
    public class UpdateCustomerPolicyDTO
    {
        public Guid CustomerPolicyID { get; set; }
        public PolicyStatus Status { get; set; }
    }
}
