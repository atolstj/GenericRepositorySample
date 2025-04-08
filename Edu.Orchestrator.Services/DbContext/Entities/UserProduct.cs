using DbContext.Entities;

namespace Edu.Orchestrator.Services.DbContext.Entities;

public class UserProduct : BaseEntityWithIdUpdatable<int>
{
    public int UserId { get; init; }

    public int ProductId { get; init; }

    public bool IsActive { get; set; } = true;
}