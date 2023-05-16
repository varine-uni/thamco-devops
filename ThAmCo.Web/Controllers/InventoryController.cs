using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThAmCo.Web.Models;
using ThAmCo.Web.Services;

namespace ThAmCo.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Details()
        {
            var products = await _inventoryService.GetAllProductsAsync();

            return View(products);
        }
    }
}
