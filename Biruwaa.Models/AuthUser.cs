using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Biruwaa.Models
{
    public class AuthUser : IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
