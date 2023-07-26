using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroops.Domain.EFModels;

namespace RunGroops.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.FromUserId, f.ToUserId});

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FromUser)
                .WithMany(f => f.SentFriendRequests)
                .HasForeignKey(f => f.FromUserId);

            modelBuilder.Entity<Friend>()
                .HasOne(a => a.ToUser)
                .WithMany(b => b.ReceievedFriendRequests)
                .HasForeignKey(c => c.ToUserId);
        }
    }
}
