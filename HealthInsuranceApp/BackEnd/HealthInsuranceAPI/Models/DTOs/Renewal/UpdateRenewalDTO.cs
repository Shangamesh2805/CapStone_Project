using HealthInsuranceAPI.Models.DTOs.Renewal;

namespace HealthInsuranceAPI.DTOs
{
    public class UpdateRenewalDTO 
    {
        public Guid RenewalID { get; set; }
        public DateTime RenewalDate { get; set; }
        public decimal RenewalAmount { get; set; }
        public bool DiscountApplied { get; set; }
        public bool IsRenewed { get; set; }

    }
}
