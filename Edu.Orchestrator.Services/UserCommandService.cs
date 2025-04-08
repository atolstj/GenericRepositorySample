using DbContext;
using Edu.Contracts.Orchestrator;
using Edu.Orchestrator.Services.DbContext.Entities;

namespace Edu.Orchestrator.Services;

public class UserCommandService<TId> : IUserCommandService
    where TId : struct
{
    private readonly IUnitOfWork _ctx;

    UserCommandService(IUnitOfWork ctx)
    {
        _ctx = ctx;
    }

    public async Task Handle(UserCommand userCommand)
    {
        var user = await _ctx.Repository<User>().GetAsync(d => d.FullName == userCommand.FullName)
            .SingleOrDefaultAsync();

        if(user == null)
        {
            user = new User { FullName = userCommand.FullName };

            await _ctx.Repository<User>().CreateAsync(user);
        }

        await CreateOrUpdateUserProductsAsync(user, userCommand.ProductCodes);

        await _ctx.SaveAllAsync();
    }


    private async Task CreateOrUpdateUserProductsAsync(User user, List<string> productCodes)
    {
        // Получаем продукты из БД по кодам и заодно проверяем существованиие продукта для кода
        var validProducts = await _ctx.Repository<Product>().GetAsync(d => productCodes.Contains(d.Code)).ToListAsync();

        if(!user.IsNew)
        {
            // получаем из БД связи с продуктами для пользователя
            var existedUserProducts = await _ctx.Repository<UserProduct>().GetAsync(d => d.UserId.Equals(user.Id), true)
                    .ToListAsync();

            foreach(var eup in existedUserProducts)
            {
                // деактивируем связь, не не передам код продукта
                eup.IsActive = validProducts.FirstOrDefault(d => !d.Id.Equals(eup.ProductId)) != null;
            }

            // уточняем продукты для создания новых связей
            var existedProductIds = existedUserProducts.Select(d => d.ProductId).ToList();
            validProducts = validProducts.Where(d => !existedProductIds.Contains(d.Id)).ToList();
        }

        // создаем новые связи
        await _ctx.Repository<UserProduct>().CreateRangeAsync(validProducts.Select(d =>
            new UserProduct
            {
                UserId = user.Id,
                ProductId = d.Id
            })
        );
    }
}