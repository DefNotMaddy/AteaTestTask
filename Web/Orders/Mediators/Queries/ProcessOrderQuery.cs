using Core.Dtos.Orders;
using MediatR;

namespace Web.Orders.Mediators.Queries
{
    public class ProcessOrderQuery : OrderRequest, IRequest<OrderResponse>
    {

    }
}
