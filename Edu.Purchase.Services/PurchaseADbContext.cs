using Microsoft.EntityFrameworkCore;

namespace Purchase.Services;

public class PurchaseADbContext : PurchaseDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=purchase_a.dat");
    }
}