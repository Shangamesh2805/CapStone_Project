using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models.DTOs.Users
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
