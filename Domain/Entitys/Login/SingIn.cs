
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Login
{
    public class SingIn
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
