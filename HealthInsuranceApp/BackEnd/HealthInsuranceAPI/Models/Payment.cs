using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentID { get; set; }

        [ForeignKey("CustomerPolicy")]
        public Guid CustomerPolicyID { get; set; }
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
