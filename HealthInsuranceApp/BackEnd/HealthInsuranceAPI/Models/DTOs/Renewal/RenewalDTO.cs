namespace HealthInsuranceAPI.Models.DTOs.Renewal
{
    public class RenewalDTO 
    {
        public Guid RenewalID { get; set; }
        public Guid CustomerPolicyID { get; set; }
        public DateTime RenewalDate { get; set; }
        public decimal RenewalAmount { get; set; }
        public bool DiscountApplied { get; set; }
        public bool IsRenewed { get; set; }
    }
}
