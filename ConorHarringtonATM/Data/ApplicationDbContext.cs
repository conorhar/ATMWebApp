using Microsoft.EntityFrameworkCore;

namespace ConorHarringtonATM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=localhost;Database=ATM;Trusted_Connection=True;");
            }
        }

        public DbSet<Bill> Bills { get; set; }
    }
}