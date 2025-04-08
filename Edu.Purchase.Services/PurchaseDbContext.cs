using DbContext;
using Microsoft.EntityFrameworkCore;

namespace Purchase.Services;

public class PurchaseDbContext : EfDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}