using DbContext;
using Edu.Orchestrator.Services.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Edu.Orchestrator.Services.DbContext;

public class OrchestratorDbContext(IConfiguration configuration) : EfDbContext(configuration)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>();

        modelBuilder.Entity<User>()
            .HasMany<Product>()
            .WithMany()
            .UsingEntity<UserProduct>();

        modelBuilder.Entity<UserProduct>();

        modelBuilder.Entity<OrderEvent>()
            .HasOne<UserProduct>()
            .WithMany();

        modelBuilder.Entity<OrderCreated>();
        
        modelBuilder.Entity<OrderExecuted>();
    }
}