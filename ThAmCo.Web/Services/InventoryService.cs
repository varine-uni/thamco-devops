using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // Set up the database context
            _dbContext = dbContext;
            client.BaseAddress = new System.Uri("http://localhost:5000/");
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client = client;
        }

        public Task<Product> AddProductAsync(Product product)
        {
            // Add product
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Task.FromResult(product);
        }

        public Task<bool> CheckProductAvailabilityAsync(int productId)
        {
            // Check product availability
            var product = _dbContext.Products.Find(productId);

            if (product.QuantityAvailable > 0)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            // Get all products
            var products = await _dbContext.Products.ToListAsync();

            return products;
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            // Get product by id
            var product = _dbContext.Products.Find(productId);

            return Task.FromResult(product);
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            // Get products by category
            var products = _dbContext.Products.FromSqlRaw("SELECT * FROM Products WHERE Category = {0}", category).ToListAsync();
            return products;
        }

        public Task<bool> RemoveProductAsync(int productId)
        {
            // Remove product by id
            var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return Task.FromResult(true);
        }

        public List<Product> SearchItems(string searchString)
        {
            searchString = searchString?.Trim();

            if (string.IsNullOrEmpty(searchString))
            {
                // Return an empty list or handle the scenario as per your requirement
                return new List<Product>();
            }

            return _dbContext.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{searchString}%"))
            .ToList();
        }

        public Task<bool> UpdateProductQuantityAsync(int productId, int quantity)
        {
            // Update product quantity
            var product = _dbContext.Products.Find(productId);
            product.QuantityAvailable = quantity;
            _dbContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
