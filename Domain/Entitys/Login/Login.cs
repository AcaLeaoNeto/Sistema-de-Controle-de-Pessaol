
namespace Domain.Entitys.Login
{
    public class Login
    {
        public Login(string username, byte[] passwordHash, byte[] passwordSalt)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public int id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
