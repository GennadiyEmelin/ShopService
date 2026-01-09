using Microsoft.AspNetCore.Mvc;
using ShopService.Data;
using ShopService.Models;

namespace ShopService.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<ActionResult> CreateAsync(string name, string Description, decimal price, int quantity) 
        {
            var product = new Product(name, Description, price, quantity);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new OkObjectResult(product);
        }
    }
}
