namespace HealthInsuranceAPI.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int CustomerPolicyID { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType PaymentType { get; set; }
        public CustomerPolicy CustomerPolicy { get; set; }
    }
    public enum PaymentType
    {
        Renewal,
        NewApplication
    }
}
