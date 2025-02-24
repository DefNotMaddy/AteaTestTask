using Core.Interfaces;

namespace Core.Dtos.Orders
{
    public class OrderResponse : IResponse
    {
        public string ResponseMessage { get; set; }
        public string OrderId { get; set; }
        public string OrderResponseId { get => $"OrdRsp{OrderId}"; }

        public override bool Equals(object? obj)
        {
            if (obj is not OrderResponse other) return false;
            return ResponseMessage == other.ResponseMessage && OrderId == other.OrderId;
        }
    }
}
