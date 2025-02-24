
using System.Text.Json;
using MediatR;

namespace Core.Dtos.Orders
{
    public class OrderRequest : IRequest<OrderResponse>
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public double PayableAmount { get; set; }
        public string PayableGateway { get; set; }
        public string OptionalDescription { get; set; }

        public override string? ToString() => JsonSerializer.Serialize(this);
    }
}
