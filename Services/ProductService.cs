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

        public async Task<ProductResponseDto> CreateAsync(string name, string Description, decimal price, int quantity)
        {
            var product = new Product(name, Description, price, quantity);
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ProductResponseDto();
            }

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<List<ProductResponseDto>> GetAllAsync() 
        {
            try
            {
                List<ProductResponseDto> products = await _context.Products
                    .Where(p => p.IsActive)
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
            if (productId == null) 
            {
                return new ProductResponseDto();
            }
            return new ProductResponseDto
            {
                Id = productId.Id,
                Name = productId.Name,
                Description = productId.Description,
                Price = productId.Price,
                StockQuantity = productId.StockQuantity
            };
        }

        public async Task<UpdateProductDto> UpdateAsync(int id, string name, string description, decimal price, int stockQuantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null || product.IsActive == false)
            {
                return null;
            }
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.StockQuantity = stockQuantity;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            
            return new UpdateProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return false;
            }
            product.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
