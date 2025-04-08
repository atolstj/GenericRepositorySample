using Edu.Contracts.Orchestrator;

namespace Edu.Orchestrator.Services;

public interface IUserCommandService
{
    Task Handle(UserCommand user);
}
