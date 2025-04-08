namespace Edu.Contracts.Orchestrator;

public class ProductOrderInfo
{
    public required string Code { get; init; }

    public int TotalOrderQty { get; set; }

    public int ExecOrderQty { get; set; }
}