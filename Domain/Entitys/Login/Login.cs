using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Login
{
    public class Login
    {
        public Login(string username, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Role = role;
        }

        [Required, Key, Index]
        public int id { get; set; }
        [Index(IsUnique = true), Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
