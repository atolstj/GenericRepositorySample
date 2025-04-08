using Edu.Contracts.Orchestrator;

namespace Edu.Orchestrator.Services;

public interface IUserInfoListService
{
    Task<UserInfoList> GetAll();
}