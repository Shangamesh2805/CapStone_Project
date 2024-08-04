using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models
{
    public class Agent
    {
        [Key]
        public Guid AgentID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public User User { get; set; }
    }
}
