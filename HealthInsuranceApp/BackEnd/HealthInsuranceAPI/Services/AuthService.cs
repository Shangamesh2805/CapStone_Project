using HealthInsuranceAPI.Models;
using HealthInsuranceAPI.Models.DTOs.AuthDTOs;
using HealthInsuranceAPI.Models.DTOs.Users;
using HealthInsuranceAPI.Repositories.Interfaces;
using HealthInsuranceAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Claim = System.Security.Claims.Claim;

namespace HealthInsuranceAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Register(UserRegisterDTO registerDto)
        {
            if (await _userRepository.UserExists(registerDto.Email))
                throw new Exception("User already exists");

            var salt = GenerateSalt();
            var user = new User
            {
                UserID = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                Role = registerDto.Role,
                PasswordSalt = salt,
                PasswordHash = ComputeHash(registerDto.Password, salt)
            };

            await _userRepository.Register(user, registerDto.Password);
            return GenerateToken(user);
        }

        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDto)
        {
            var user = await _userRepository.Login(loginDto.Email, loginDto.Password);
            if (user == null)
                throw new Exception("Invalid credentials");

            var token = GenerateToken(user);

            return new LoginReturnDTO
            {
                UserId = user.UserID,
                Token = token,
                Role = user.Role
            };
        }


        private string GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenKey:JWT"] ?? throw new Exception("JWT Key not configured"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()), // UserID as NameIdentifier
            new Claim(ClaimTypes.Name, user.Username ?? throw new Exception("Username is null")),
            new Claim(ClaimTypes.Email, user.Email ?? throw new Exception("Email is null")),
            new Claim(ClaimTypes.Role, user.Role.ToString() ?? throw new Exception("Role is null"))
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }





        private byte[] GenerateSalt()
        {
            using var hmac = new HMACSHA512();
            return hmac.Key;
        }

        private byte[] ComputeHash(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

       
    }
}

