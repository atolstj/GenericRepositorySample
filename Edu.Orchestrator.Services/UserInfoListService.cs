using DbContext;
using Edu.Contracts.Orchestrator;

namespace Edu.Orchestrator.Services;

public class UserInfoListService : IUserInfoListService
{
    private readonly IUnitOfWork _ctx;

    // ReSharper disable once ConvertToPrimaryConstructor
    public UserInfoListService(IUnitOfWork ctx)
    {
        _ctx = ctx;
    }

    public Task<UserInfoList> GetAll()
    {
        throw new NotImplementedException();
    }
}