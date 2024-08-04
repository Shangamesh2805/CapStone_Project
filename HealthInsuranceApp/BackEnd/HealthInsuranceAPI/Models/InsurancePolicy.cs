using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models
{
    public class InsurancePolicy
    {
        [Key]
        public Guid PolicyID { get; set; }

        [Required]
        [MaxLength(100)]
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal RenewalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
