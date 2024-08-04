﻿using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceAPI.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public UserRole Role { get; set; } 
    }

    public enum UserRole
    {
        Customer,
        Agent
    }
}
