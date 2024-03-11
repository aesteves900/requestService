using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductAndRequests.Controllers;
using ProductAndRequests.Data;
using ProductAndRequests.Models;
using ProductAndRequests.Test.MockU;

namespace ProductAndRequests.Test
{
    public class ProductsControllerTest
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly ProductsController _controller;
        private readonly List<Product> _products;
        private readonly Mock<DbSet<Product>> _mockSet;

        public ProductsControllerTest()
        {
            _mockSet = new Mock<DbSet<Product>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _products = new List<Product>
            {
                new(1,"Product 1", 100 ),
                new (2, "Product 2", 200)
            };

            var mockSet = MoqDbContext.MockDbSet(_products);
            _mockContext.Setup(m => m.Products).Returns(mockSet);

            var prodAsQueriable = _products.AsQueryable();

            _mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(prodAsQueriable.Provider);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(prodAsQueriable.Expression);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(prodAsQueriable.ElementType);
            _mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => prodAsQueriable.GetEnumerator());
            _mockContext.Setup(c => c.Products).Returns(_mockSet.Object);

            _controller = new ProductsController(_mockContext.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Act
            var result = await _controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var model = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(productId, model.Id);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 3; // Assuming no product with this ID exists

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction_WhenModelStateIsValid()
        {
            // Arrange
            var product = new Product(3, "New Product", 300M);
            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1)); // Simulate a successful save.

            // Act
            var result = await _controller.PostProduct(product);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetProduct", createdAtActionResult.ActionName);
            Assert.Equal(product.Id, ((Product)createdAtActionResult.Value).Id);
        }


        [Fact]
        public async Task PostProduct_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var product = new Product(3, "New Product", 300M);
            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = await _controller.PostProduct(product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductExists()
        {
            // Arrange
            var productId = 1;
            var product = new Product(productId, "New Product", 300M);

            var mockSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products.FindAsync(productId)).ReturnsAsync(product);

            var controller = new ProductsController(mockContext.Object);

            // Act
            var result = await controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = 2; // Non-existing product ID
            var mockSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);
            mockContext.Setup(m => m.Products.FindAsync(productId)).ReturnsAsync((Product)null);

            var controller = new ProductsController(mockContext.Object);

            // Act
            var result = await controller.DeleteProduct(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Product>()), Times.Never());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
        }


    }
}