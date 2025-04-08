using DbContext;
using Edu.Contracts.Orchestrator;
using Edu.Orchestrator.Services.DbContext.Entities;
using Edu.Orchestrator.Services.Extensions;

namespace Edu.Orchestrator.Services;

public class UserInfoService : IUserInfoService
{
    private readonly IUnitOfWork _ctx;

    // ReSharper disable once ConvertToPrimaryConstructor
    public UserInfoService(IUnitOfWork ctx)
    {
        _ctx = ctx;
    }

    public async Task<UserInfo?> GetByIdAsync(int id)
    {
        var results = (from users in _ctx.Repository<User>().Query()
                join userProducts in _ctx.Repository<UserProduct>().Query() on users.Id equals userProducts.UserId into
                    up
                from userProducts in up.DefaultIfEmpty()
                join products in _ctx.Repository<Product>().Query() on userProducts.ProductId equals products.Id into p
                from products in p.DefaultIfEmpty()
                join orderEvents in _ctx.Repository<OrderEvent>().Query() on userProducts.Id equals orderEvents.UserProductId into
                    o
                from orderEvents in o.DefaultIfEmpty()
                where users.Id.Equals(id)
                select new { users, userProducts, products, orderEvents })
            .ToAsyncEnumerable();

        UserInfo? userInfo = default;

        await foreach(var item in results)
        {
            userInfo ??= new UserInfo
            {
                Id = item.users.Id,
                FullName = item.users.FullName,
                ProductOrderInfos = []
            };
            
            userInfo.Apply(item.products, item.orderEvents);
        }

        return userInfo;
    }
}