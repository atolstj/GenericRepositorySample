using System.ComponentModel.DataAnnotations.Schema;

namespace DbContext.Entities;

public class BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; }

    [NotMapped]
    public bool IsNew => CreatedAt != default;
}