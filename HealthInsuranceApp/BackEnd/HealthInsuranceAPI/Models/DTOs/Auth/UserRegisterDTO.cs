using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models.DTOs.AuthDTOs
{
    public class UserRegisterDTO
    {
        [Required]
        
        public string Username { get; set; }

        [Required]
        
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
