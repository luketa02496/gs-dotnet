using Microsoft.EntityFrameworkCore;
using WorkWell.Api.Models;

namespace WorkWell.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Useres> Useres { get; set; }
        public DbSet<Assessment> Assessments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Useres>(entity =>
            {
                entity.ToTable("USERES");

                entity.Property(e => e.Id)
                      .HasColumnName("ID");

                entity.Property(e => e.Nome)
                      .HasColumnName("NOME");

                entity.Property(e => e.Idade)
                      .HasColumnName("IDADE");
            });

            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("ASSESSMENT");

                entity.Property(e => e.Id)
                      .HasColumnName("ID");

                entity.Property(e => e.Humor)
                      .HasColumnName("HUMOR");

                entity.Property(e => e.Estresse)
                      .HasColumnName("ESTRESSE");

                entity.Property(e => e.Produtividade)
                      .HasColumnName("PRODUTIVIDADE");

                entity.Property(e => e.UseresId)
                      .HasColumnName("USERES_ID");
            });
        }
    }
}
