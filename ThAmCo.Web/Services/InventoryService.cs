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

        public InventoryService(InventoryDbContext dbContext, HttpClient client)
        {
            _dbContext = dbContext;
            client.BaseAddress = new System.Uri("http://localhost:5000/");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public Task<Product> AddProductAsync(Product product)
        {
            throw new System.NotImplementedException();
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

        public Task<bool> RemoveProductAsync(int productId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateProductQuantityAsync(int productId, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}
