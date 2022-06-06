using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderWebApi.Entities;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IPublishEndpoint publishEndPoint)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndPoint = publishEndPoint ?? throw new ArgumentNullException(nameof(publishEndPoint));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            await _publishEndPoint.Publish<IOrderCreated>(new
            {
                Id=1,
                ProductName = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price
            });
            _logger.LogInformation($"Order Created");
            return Ok();
        }

    }
}
