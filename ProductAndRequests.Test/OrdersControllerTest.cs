using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAndRequests.Abstraction;
using ProductAndRequests.Controllers;
using ProductAndRequests.Models;
using ProductAndRequests.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAndRequests.Test
{



    using Moq;
    using Xunit;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersControllerTests
    {
        private readonly Mock<IProductsApi> _mockProductsApi;
        private readonly Mock<RabbitMQService> _mockRabbitMqService;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockProductsApi = new Mock<IProductsApi>();
            _mockRabbitMqService = new Mock<RabbitMQService>();
            _controller = new OrdersController(_mockProductsApi.Object, _mockRabbitMqService.Object);
        }

        [Fact]
        public async Task CreateOrder_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var nonExistentProductId = 999;
            _mockProductsApi.Setup(api => api.GetProductByIdAsync(nonExistentProductId))
                .ReturnsAsync((Product?)null);

            var orderDto = new Order(nonExistentProductId, nonExistentProductId, 1, 0); // TotalPrice set to 0 for initialization

            var result = await _controller.CreateOrder(orderDto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateOrder_PublishesOrder_WhenProductExists()
        {
            var productId = 1;
            var productPrice = 100M;
            var orderQuantity = 2;
            var product = new Product ( productId, "Test Product", productPrice);

            _mockProductsApi.Setup(api => api.GetProductByIdAsync(productId))
                .ReturnsAsync(product);

            var orderDto = new Order(productId, productId,  orderQuantity, productPrice * orderQuantity); 

            var result = await _controller.CreateOrder(orderDto);

            _mockRabbitMqService.Verify(service => service.PublishOrder(It.IsAny<Order>()), Times.Once);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(productId, returnedOrder.ProductId);
            Assert.Equal(productPrice * orderQuantity, returnedOrder.TotalPrice);
        }
    }
}