namespace HealthInsuranceAPI.Models.DTOs.Payments
{
    public class PaymentDTO
    {
        public Guid PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
