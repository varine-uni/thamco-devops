using System.Collections.Generic;
using System.Threading.Tasks;
using ThAmCo.Web.Models;

namespace ThAmCo.Web.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<List<Product>> GetProductsByCategoryAsync(string category);
        Task<bool> CheckProductAvailabilityAsync(int productId);
        Task<bool> UpdateProductQuantityAsync(int productId, int quantity);
        Task<Product> AddProductAsync(Product product);
        Task<bool> RemoveProductAsync(int productId);
        List<Product> SearchItems(string searchString);
    }
}
