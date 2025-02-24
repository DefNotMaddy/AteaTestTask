using Core.Dtos.Orders;
using MediatR;

namespace Core.Interfaces
{
    public interface IOrderProcessor
    {
        public abstract OrderResponse Processing(OrderRequest orderRequest);
        public Task<IResponse> ProcessOrder(OrderRequest orderRequest);
    }
}
