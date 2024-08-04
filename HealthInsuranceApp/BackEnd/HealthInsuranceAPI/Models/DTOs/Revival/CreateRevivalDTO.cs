namespace HealthInsuranceAPI.Models.DTOs.Revival
{
    public class CreateRevivalDTO
    {
        public Guid CustomerPolicyID { get; set; }
        public DateTime RevivalDate { get; set; }
        public string Reason { get; set; }
    }
}
