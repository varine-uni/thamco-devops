using ThAmCo.Web.Services;
using ThAmCo.Web.Models;
using ThAmCo.Web.Data;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace InventoryServiceTest
{
    public class InventoryServiceUnitTests
    {
        private readonly InventoryService _inventoryService;
        private readonly InventoryDbContext _dbContext;

        public InventoryServiceUnitTests()
        {
            // Configure in-memory database options
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create a mock instance of the inventory database context
            _dbContext = new InventoryDbContext(options);

            // Create instance of inventory service
            _inventoryService = new InventoryService(_dbContext);
        }

        [Fact]
        public async Task AddItemToInventory_ShouldIncreaseItemCount()
        {
            // Arrange
            var product = new Product { Name = "Item A", Price = 10, Category = "Test Item"};

            // Act
            await _inventoryService.AddProductAsync(product); // Call the method you want to test

            // Assert
            Assert.Equal(1, await _inventoryService.GetInventoryCountAsync()); // Verify the expected result
        }

        [Fact]
        public async Task RemoveItemFromInventory_ShouldDecreaseItemCount()
        {
            // Arrange
            var product = new Product { Name = "Item B", Price = 5, Category = "Test Item"};
            await _inventoryService.AddProductAsync(product);

            // Act
            await _inventoryService.RemoveProductAsync(product.Id);

            // Assert
            Assert.Equal(0, await _inventoryService.GetInventoryCountAsync());
        }
    }
}