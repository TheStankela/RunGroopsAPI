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
        //public DbSet<AppUserClub> UserClubs { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AppUserClub>()
        //        .HasKey(ac => new { ac.AppUserId, ac.ClubId });
        //    modelBuilder.Entity<AppUserClub>()
        //        .HasOne(a => a.AppUser)
        //        .WithMany(ap => ap.Clubs)
        //        .HasForeignKey(p => p.PokemonId);
        //    modelBuilder.Entity<AppUserClub>()
        //        .HasOne(c => c.Club)
        //        .WithMany(pc => pc.PokemonCategories)
        //        .HasForeignKey(c => c.CategoryId);
        //}
    }
}
