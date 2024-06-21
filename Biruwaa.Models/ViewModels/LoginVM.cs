﻿using System.ComponentModel.DataAnnotations;

namespace Biruwaa.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
