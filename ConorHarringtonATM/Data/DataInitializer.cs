using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConorHarringtonATM.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
            SeedBills(dbContext);
        }

        private static void SeedBills(ApplicationDbContext dbContext)
        {
            var bill = dbContext.Bills.FirstOrDefault(r => r.Value == 1000);
            if (bill == null)
            {
                dbContext.Bills.Add(new Bill {Value = 1000, Quantity = 2});
            }

            bill = dbContext.Bills.FirstOrDefault(r => r.Value == 500);
            if (bill == null)
            {
                dbContext.Bills.Add(new Bill { Value = 500, Quantity = 3 });
            }

            bill = dbContext.Bills.FirstOrDefault(r => r.Value == 100);
            if (bill == null)
            {
                dbContext.Bills.Add(new Bill { Value = 100, Quantity = 5 });
            }

            dbContext.SaveChanges();
        }
    }
}