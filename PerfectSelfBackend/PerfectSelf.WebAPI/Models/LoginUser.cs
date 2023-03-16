﻿using System.ComponentModel.DataAnnotations;

namespace PerfectSelf.WebAPI.Models
{
    public class LoginUser
    {
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Password length must be above 6.")]
        public string Password { get; set; }
    }
}
