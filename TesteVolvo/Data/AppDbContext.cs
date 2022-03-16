using Microsoft.EntityFrameworkCore;
using TesteVolvo.Models;

namespace TesteVolvo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Caminhao> Caminhoes { get; set; }
        public DbSet<Modelo> Modelos { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caminhao>()
                .HasOne(e => e.Modelo)
                .WithMany(c => c.Caminhoes)
                .HasForeignKey(h => h.IdModelo);

            modelBuilder.Entity<Modelo>()
                .HasMany(c => c.Caminhoes)
                .WithOne(e => e.Modelo)
                .HasForeignKey(c => c.IdModelo);

            modelBuilder.Entity<Modelo>()
                .HasData(new Modelo[] {
                    new Modelo{ IdModelo = 1, Nome = "FH", Ativo = true },
                    new Modelo{ IdModelo = 2, Nome = "FM", Ativo = true },
                });
        }
    }
}
