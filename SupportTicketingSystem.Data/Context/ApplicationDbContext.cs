using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.FullName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(u => u.UserType)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });
        }
    }
}