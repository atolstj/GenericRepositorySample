namespace Edu.Contracts.Orchestrator;

public class UserInfo : IObjectWithId<int>
{
    public required int Id { get; set; }

    public required string FullName { get; set; }

    public required List<ProductOrderInfo> ProductOrderInfos { get; init;} = [];
}   