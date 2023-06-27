using Microsoft.EntityFrameworkCore;
using RunGroops.Domain.EFModels;

namespace RunGroops.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Club> Clubs { get; set; } 
        public DbSet<Address> Addresses { get; set; }
    }
}
