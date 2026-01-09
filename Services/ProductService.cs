using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using ShopService.DTOs;
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

        public async Task<List<ProductResponseDto>> GetAllAsync() 
        {
            List<ProductResponseDto> products = await _context.Products
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
            return products;
        }
    }
}
