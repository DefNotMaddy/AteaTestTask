using Core.Dtos.Orders;
using Core.Interfaces;
using Moq;
using Web.Orders.Mediators.Handlers;
using Web.Orders.Mediators.Queries;

namespace Web.UnitTests.Orders
{
    public class ProcessOrderTests
    {
        private readonly Mock<IOrderProcessor> _processor;
        private readonly ProcessOrderHandler _processOrderHandler;
        public ProcessOrderTests() 
        {
            _processor = new Mock<IOrderProcessor>();
            _processOrderHandler = new ProcessOrderHandler(_processor.Object);
        }
        [Fact]
        public async Task ProcessOrderHandler_ShouldReturnOrderResponse()
        {
            var request = new ProcessOrderQuery()
            {
                OptionalDescription = "OptionalDescriptionl",
                OrderId = "OrderId",
                PayableAmount = 0.0f,
                PayableGateway = "PayableGateway",
                UserId = "UserId"
            };
            var expectedResponse = new OrderResponse()
            { OrderId = request.OrderId, ResponseMessage = "Processed" };
            _processor
                .Setup(x => x.ProcessOrder(request))
                .ReturnsAsync(expectedResponse);

            var result = await _processOrderHandler.Handle(request, default);

            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);
        }
    }
}
