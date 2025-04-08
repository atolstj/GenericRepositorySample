using Microsoft.EntityFrameworkCore;

namespace Purchase.Services;

public class PurchaseBDbContext : PurchaseDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=purchase_b.dat");
    }
}