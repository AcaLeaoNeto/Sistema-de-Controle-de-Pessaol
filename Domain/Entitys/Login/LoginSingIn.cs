
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Login
{
    public class LoginSingIn
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
