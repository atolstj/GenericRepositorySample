using Edu.Contracts.Orchestrator;

namespace Edu.Orchestrator.Services;

public interface IUserInfoService
{
    Task<UserInfo?> GetByIdAsync(int id);
}