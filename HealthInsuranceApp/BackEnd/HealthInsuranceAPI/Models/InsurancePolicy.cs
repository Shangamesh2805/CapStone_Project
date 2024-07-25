namespace HealthInsuranceAPI.Models
{
    public class InsurancePolicy
    {
        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
