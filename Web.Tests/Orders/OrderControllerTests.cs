using Moq;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Dtos.Orders;
using Web.Orders.Controllers;
using Web.Orders.Mediators.Queries;
using Microsoft.Extensions.Logging;

namespace Web.UnitTests.Orders;
public class OrderControllerTests
{
    [Fact]
    public async Task ProcessOrder_ShouldReturnOk_WhenValidOrderRequestIsGiven()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockLogger = new Mock<ILogger<OrderController>>();
        var orderRequest = new OrderRequest { OrderId = "123", UserId = "user1", PayableAmount = 100.0, PayableGateway = "PayPal" };
        var orderResponse = new OrderResponse { OrderId = "123", ResponseMessage = "Processed" };

        mockMediator.Setup(m => m.Send(It.IsAny<ProcessOrderQuery>(), default)).ReturnsAsync(orderResponse);

        var controller = new OrderController(mockLogger.Object, mockMediator.Object);

        // Act
        var result = await controller.ProcessOrder(orderRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(orderResponse, okResult.Value);
    }
}
