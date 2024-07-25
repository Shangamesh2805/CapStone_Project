namespace HealthInsuranceAPI.Models
{
    public class Agent
    {
        public int AgentID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string AgencyName { get; set; }
        public string ContactNumber { get; set; }
        public User User { get; set; }
    }
}
