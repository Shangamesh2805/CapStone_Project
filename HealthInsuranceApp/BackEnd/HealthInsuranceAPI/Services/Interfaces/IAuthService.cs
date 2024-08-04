using HealthInsuranceAPI.Models.DTOs.AuthDTOs;
using HealthInsuranceAPI.Models.DTOs.Users;

namespace HealthInsuranceAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterDTO registerDto);
        Task<LoginReturnDTO> Login(UserLoginDTO loginDto);
    }
}
