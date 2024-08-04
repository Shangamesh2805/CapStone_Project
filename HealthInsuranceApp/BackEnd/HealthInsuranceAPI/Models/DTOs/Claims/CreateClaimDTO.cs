namespace HealthInsuranceAPI.Models.DTOs.Claims
{
    public class CreateClaimDTO
    {
        public Guid CustomerPolicyID { get; set; }
        public decimal ClaimAmount { get; set; }
        public string Reason { get; set; }
    }
}
