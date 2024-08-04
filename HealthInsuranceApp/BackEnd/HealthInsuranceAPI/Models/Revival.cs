using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsuranceAPI.Models
{
    public class Revival
    {
        [Key]
        public Guid RevivalID { get; set; }

        [ForeignKey("CustomerPolicy")]
        public Guid CustomerPolicyID { get; set; }

        public DateTime RevivalDate { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }
    }
}
