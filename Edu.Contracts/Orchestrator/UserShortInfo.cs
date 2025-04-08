namespace Edu.Contracts.Orchestrator;

public class UserShortInfo : IObjectWithId<int>
{
    public int Id { get; set; }

    public required string FullName { get; set; }
}