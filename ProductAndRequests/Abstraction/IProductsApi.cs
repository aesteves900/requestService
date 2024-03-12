using ProductAndRequests.Models;
using Refit;

namespace ProductAndRequests.Abstraction
{
    public interface IProductsApi
    {
        [Get("/api/products/{id}")]
        Task<Product> GetProductByIdAsync(int? id);
    }
}