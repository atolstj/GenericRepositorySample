using DbContext.Entities;

namespace Edu.Orchestrator.Services.DbContext.Entities;

public class User : BaseEntityWithId<int>
{
    public required string FullName { get; init; }
}