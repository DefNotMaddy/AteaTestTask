using Core.Dtos.Orders;
using Core.Interfaces;

namespace Core.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        public Task<IResponse> ProcessOrder(OrderRequest orderRequest) => Task.FromResult<IResponse>(Processing(orderRequest));
        public OrderResponse Processing(OrderRequest orderRequest) => new() { OrderId = orderRequest.OrderId, ResponseMessage = "Processed" };
    }
}
