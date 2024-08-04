using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsuranceAPI.Models
{
    public class Renewal
    {
        [Key]
        public Guid RenewalID { get; set; }

        [ForeignKey("CustomerPolicy")]
        public Guid CustomerPolicyID { get; set; }

        public DateTime RenewalDate { get; set; }
        public decimal RenewalAmount { get; set; }
        public bool DiscountApplied { get; set; }
        public bool IsRenewed { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }
    }
}
