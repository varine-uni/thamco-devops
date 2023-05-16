using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ThAmCo.Web.Data;
using ThAmCo.Web.Models;

namespace ThAmCo.Web.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _dbContext;
        private readonly HttpClient _client;
        private InventoryDbContext @object;

        public InventoryService()
        {
        }

        public InventoryService(InventoryDbContext dbContext, HttpClient client)
        {
            _dbContext = dbContext;
            client.BaseAddress = new System.Uri("http://localhost:5000/");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public InventoryService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> CheckProductAvailabilityAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _dbContext.Products.ToListAsync();

            return products;
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveProductAsync(int productId)
        {
            // Find product via ID
            var product = await _dbContext.Products.FindAsync(productId);

            // Check if the product exists
            if (product == null)
            {
                // Handle the case where the product does not exist
                throw new Exception("Product does not exist.");
            }

            // Remove product from database
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> UpdateProductQuantityAsync(int productId, int quantity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetInventoryCountAsync()
        {
            return await _dbContext.Products.CountAsync();
        }

        Task IInventoryService.GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        Task<Product> IInventoryService.AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
