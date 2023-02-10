using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Login
{
    public class SingOn
    {

        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public int UserId { get; set; }

    }
}
