using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using ShopService.DTOs;
using ShopService.Models;
using System.Security.Cryptography.X509Certificates;

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
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return new BadRequestObjectResult(new { message = "Ошибка добавления продукта", details = ex.Message });
            }
            return new OkObjectResult(product);
        }

        public async Task<List<ProductResponseDto>> GetAllAsync() 
        {
            try
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
            catch (Exception)
            {
                return new List<ProductResponseDto>();
            }
        }

        public async Task<ProductResponseDto> GetByIdAsync(int id)
        {
            var productId = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return new ProductResponseDto
            {
                Id = productId.Id,
                Name = productId.Name,
                Description = productId.Description,
                Price = productId.Price,
                StockQuantity = productId.StockQuantity
            };
        }
    }
}
