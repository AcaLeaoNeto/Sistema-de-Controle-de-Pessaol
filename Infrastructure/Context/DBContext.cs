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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .HasIndex(L => L.Username).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(U => U.CodigoUsuario).IsUnique();
        }

        public DbSet<User> usuarios { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
