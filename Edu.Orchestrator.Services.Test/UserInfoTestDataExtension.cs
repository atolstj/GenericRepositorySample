using DbContext;
using Edu.Orchestrator.Services.DbContext.Entities;

namespace Edu.Orchestrator.Services.Test;

public static class UserInfoTestDataExtension
{
    public static async Task PopulateTestDataAsync(this IUnitOfWork ctx)
    {
        await ctx.Repository<User>().CreateRangeAsync(
            [
                new User { Id = 1, FullName = "Иван Сидоров" },
                new User { Id = 2, FullName = "Петр Смирнов" }
            ]
        );

        await ctx.Repository<Product>().CreateRangeAsync(
            [
                new Product { Id = 1, Code = "ELPH", Desc = "Слоны"},
                new Product { Id = 2, Code = "CAT", Desc = "Кошки"},
                new Product { Id = 3, Code = "DOG", Desc = "Собаки"},
            ]
        );

        await ctx.Repository<UserProduct>().CreateRangeAsync(
            [
                new UserProduct { Id = 1, UserId = 1, ProductId = 1 },
                new UserProduct { Id = 2, UserId = 2, ProductId = 1 },
                new UserProduct { Id = 3, UserId = 2, ProductId = 2 },
                new UserProduct { Id = 4, UserId = 2, ProductId = 3 }
            ]
        );

        await ctx.Repository<OrderEvent>().CreateRangeAsync(
            [
                new OrderCreated { Id = 1, UserProductId = 1 },
                new OrderCreated { Id = 2, UserProductId = 2 },
                new OrderCreated { Id = 3, UserProductId = 2 },
                new OrderCreated { Id = 4, UserProductId = 2 },
                new OrderCreated { Id = 5, UserProductId = 3 },
                new OrderExecuted { Id = 6, UserProductId = 2, PurchaseToken = "ELPH1"},
                new OrderExecuted { Id = 7, UserProductId = 2, PurchaseToken = "ELPH2"}
            ]
            );
        
        await ctx.SaveAllAsync();
    }
}