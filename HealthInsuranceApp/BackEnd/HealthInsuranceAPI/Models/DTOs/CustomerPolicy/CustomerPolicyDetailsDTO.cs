using HealthInsuranceAPI.Models.DTOs.Claims;
using HealthInsuranceAPI.Models.DTOs.Payments;
using HealthInsuranceAPI.Models.DTOs.Renewal;
using HealthInsuranceAPI.Models.DTOs.Revival;

namespace HealthInsuranceAPI.Models.DTOs.CustomerPolicy
{
    public class CustomerPolicyDetailsDTO
    {
        public Guid CustomerPolicyID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid PolicyID { get; set; }
        public string Status { get; set; }
        public bool DiscountEligibility { get; set; }
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public List<ClaimDTO> Claims { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        public List<RenewalDTO> Renewals { get; set; }
        public List<RevivalDTO> Revivals { get; set; }
    }
}
