using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthInsuranceAPI.Models
{
    public class CustomerPolicy
    {
        [Key]
        public Guid CustomerPolicyID { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerID { get; set; }

        [ForeignKey("InsurancePolicy")]
        public Guid PolicyID { get; set; }
        public PolicyStatus Status { get; set; }
        public bool DiscountEligibility { get; set; }
        public Customer Customer { get; set; }
        public InsurancePolicy InsurancePolicy { get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime ExpiryDate { get; set; }

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Renewal> Renewals { get; set; } = new List<Renewal>();
        public ICollection<Revival> Revivals { get; set; } = new List<Revival>();
    }

    public enum PolicyStatus
    {
        Active,
        Expired
    }
}
