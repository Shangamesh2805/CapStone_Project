namespace HealthInsuranceAPI.Models.DTOs.Revival
{
    public class RevivalDTO : CreateRevivalDTO
    {
        public Guid RevivalID { get; set; }
        public bool IsApproved { get; set; }
    }
}
