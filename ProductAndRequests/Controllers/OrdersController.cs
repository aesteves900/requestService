using Microsoft.AspNetCore.Mvc;
using ProductAndRequests.Abstraction;
using ProductAndRequests.Models;
using ProductAndRequests.Service;

namespace ProductAndRequests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IProductsApi _productsApi;
        private readonly RabbitMQService _rabbitMqService;

        public OrdersController(IProductsApi productsApi, RabbitMQService rabbitMqService)
        {
            _productsApi = productsApi;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order orderDto)
        {         
            var product = await _productsApi.GetProductByIdAsync(orderDto.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var order = new Order(product.Id, null, orderDto.Quantity, product.Price * orderDto.Quantity);

            _rabbitMqService.PublishOrder(order);

            return Ok(order);
        }
    }
}