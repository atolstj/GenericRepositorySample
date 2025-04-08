namespace Edu.Orchestrator.Services.DbContext.Entities;

public class OrderExecuted : OrderEvent
{
    public required string PurchaseToken { get; set; }
}