using Core.Dtos.Orders;
using Core.Interfaces;
using MediatR;

namespace Web.Orders.Mediators.Handlers
{
    public class ProcessOrderHandler(IOrderProcessor orderProcessor) : IRequestHandler<OrderRequest, OrderResponse>
    {
        public async Task<OrderResponse> Handle(OrderRequest request, CancellationToken cancellationToken)
        {
            // Process the order using the processor and return a response
            var result = await orderProcessor.ProcessOrder(request);
            return result as OrderResponse;
        }
    }
}
