namespace HealthInsuranceAPI.Models.DTOs.Agents
{
    public class CreateAgentDTO
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
    }
}
