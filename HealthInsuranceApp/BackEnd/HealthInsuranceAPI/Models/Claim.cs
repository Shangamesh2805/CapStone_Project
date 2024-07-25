namespace HealthInsuranceAPI.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public int CustomerPolicyID { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime ClaimDate { get; set; }
        public ClaimStatus ClaimStatus { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }

    }
    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
