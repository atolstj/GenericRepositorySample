using Edu.Contracts.Orchestrator;
using Edu.Orchestrator.Services.DbContext.Entities;

namespace Edu.Orchestrator.Services.Extensions;

public static class UserInfoExtension
{
    public static void Apply(this UserInfo userInfo, Product product, OrderEvent orderEvent)
    {
        var productOrderInfo = userInfo.ProductOrderInfos.Find(d => d.Code == product.Code);
        if (productOrderInfo == null)
        {
            productOrderInfo = new ProductOrderInfo{ Code = product.Code };
            userInfo.ProductOrderInfos.Add(productOrderInfo);
        }

        switch (orderEvent)
        {
            case OrderCreated:
                productOrderInfo.TotalOrderQty++;
                break;
            case OrderExecuted:
                productOrderInfo.ExecOrderQty++;
                break;
        }
    }
}