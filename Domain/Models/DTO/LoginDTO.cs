﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "EmailRequired")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailNotValid")]
        [StringLength(maximumLength: 30, ErrorMessage = "MaxEmail30")]
        public string Email { get; init; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxPassword20")]
        [MinLength(6, ErrorMessage = "MinPassword6")]
        public string Password { get; init; }
    }
}
