using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models.DTOs.AuthDTOs
{
    public class LoginReturnDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public UserRole Role { get; set; }
    }
}
