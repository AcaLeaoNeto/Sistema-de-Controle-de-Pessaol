using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entitys.Usuario;

namespace Domain.Entitys.Login
{
    public class Log
    {
        public Log(string username, byte[] passwordHash, byte[] passwordSalt, string role, int userId)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Role = role;
            UserId = userId;
        }

        [Required, Key, Index]
        public int Id { get; set; }
        [Index(IsUnique = true), Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Role { get; set; } = string.Empty;
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
