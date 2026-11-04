
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
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketMessage> TicketMessages => Set<TicketMessage>();

        public DbSet<TicketAssignmentHistory> TicketAssignmentHistories
            => Set<TicketAssignmentHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.FullName).HasMaxLength(150).IsRequired();
                entity.Property(u => u.Username).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Email).HasMaxLength(150).IsRequired();
                entity.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();
                entity.Property(u => u.UserType).HasMaxLength(20).IsRequired();
                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name).HasMaxLength(150).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.ImageUrl).HasMaxLength(500);
                entity.Property(p => p.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title).HasMaxLength(150).IsRequired();
                entity.Property(t => t.Description).HasMaxLength(2000).IsRequired();
                entity.Property(t => t.Status)
                    .HasMaxLength(30)
                    .IsRequired()
                    .HasDefaultValue("New");

                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(t => t.Client)
                    .WithMany()
                    .HasForeignKey(t => t.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Product)
                    .WithMany()
                    .HasForeignKey(t => t.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.AssignedTo)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedToId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TicketMessage>(entity =>
            {
                entity.ToTable("TicketMessages");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Message).HasMaxLength(2000).IsRequired();
                entity.Property(m => m.SentAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(m => m.Ticket)
                    .WithMany()
                    .HasForeignKey(m => m.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Sender)
                    .WithMany()
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TicketAssignmentHistory>(entity =>
            {
                entity.ToTable("TicketAssignmentHistories");
                entity.HasKey(h => h.Id);

                entity.Property(h => h.Action)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(h => h.ActionAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(h => h.Ticket)
                    .WithMany()
                    .HasForeignKey(h => h.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(h => h.SupportMember)
                    .WithMany()
                    .HasForeignKey(h => h.SupportMemberId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(h => h.Manager)
                    .WithMany()
                    .HasForeignKey(h => h.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
