using System.ComponentModel.DataAnnotations;
using DbContext.Entities;

namespace Edu.Orchestrator.Services.DbContext.Entities;

public class Product : BaseEntityWithIdUpdatable<int>
{
    [MaxLength(10)]
    public required string Code { get; init; }

    [MaxLength(100)]
    public string? Desc { get; init; }
}