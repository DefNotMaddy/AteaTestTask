using Microsoft.AspNetCore.Mvc;
using Core.Dtos.Orders;
using Web.Orders.Mediators.Queries;
using MediatR;

namespace Web.Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(ILogger<OrderController> logger, IMediator mediator) : ControllerBase
    {
        [HttpPost("Order")]
        public async Task<IActionResult> ProcessOrder([FromBody] OrderRequest orderRequest)
        {
            logger.LogInformation($"Processing {orderRequest}");
            var request = new ProcessOrderQuery()
            {
                OrderId = orderRequest.OrderId,
                UserId = orderRequest.UserId,
                PayableGateway = orderRequest.PayableGateway,
                PayableAmount = orderRequest.PayableAmount,
                OptionalDescription = orderRequest.OptionalDescription
            };
            var result = await mediator.Send(request, default);

            return result is not null ?
                Ok(result) : 
                NoContent();
        }
    }
}
