using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User User { get; set; }
        public ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
