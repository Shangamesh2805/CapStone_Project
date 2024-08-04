using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsuranceAPI.Models
{
    public class Claim
    {
        [Key]
        public Guid ClaimID { get; set; }

        [ForeignKey("CustomerPolicy")]
        public Guid CustomerPolicyID { get; set; }

        public decimal ClaimAmount { get; set; }
        public DateTime ClaimDate { get; set; }
        public ClaimStatus ClaimStatus { get; set; }
        public string Reason { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
