namespace HealthInsuranceAPI.Models.DTOs.Renewal
{
    public class CreateRenewalDTO
    {
        public Guid CustomerPolicyID { get; set; }
        public DateTime RenewalDate { get; set; }
        public decimal RenewalAmount { get; set; }
        public bool DiscountApplied { get; set; }
    }
}
