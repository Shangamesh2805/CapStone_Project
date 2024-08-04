using Microsoft.AspNetCore.Mvc;
using HealthInsuranceAPI.Models.DTOs.AuthDTOs;
using HealthInsuranceAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using HealthInsuranceAPI.Models.DTOs.Users;

namespace HealthInsuranceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDto)
        {
            try
            {
                var token = await _authService.Register(registerDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
        {
            try
            {
                var loginResult = await _authService.Login(loginDto);
                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
