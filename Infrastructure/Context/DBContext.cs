using Microsoft.EntityFrameworkCore;
using Domain.Entitys.Usuario;
using Domain.Entitys.Login;

namespace Infrastructure.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SimpleApi;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Login> Logs { get; set; }
    }
}
