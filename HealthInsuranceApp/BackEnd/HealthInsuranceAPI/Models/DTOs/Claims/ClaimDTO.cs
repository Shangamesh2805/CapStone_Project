namespace HealthInsuranceAPI.Models.DTOs.Claims
{
    public class ClaimDTO : CreateClaimDTO
    {
        public Guid ClaimID { get; set; }
        public DateTime ClaimDate { get; set; }
        public string ClaimStatus { get; set; }
    }
}
