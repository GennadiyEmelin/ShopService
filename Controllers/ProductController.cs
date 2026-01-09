using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.DTOs;
using ShopService.Models;
using ShopService.Services;

namespace ShopService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) 
        {
            _productService = productService;
        }
        [HttpPost("CreateProducts")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            await _productService.CreateAsync(dto.Name, dto.Description, dto.Price, dto.StockQuantity);
            return Ok();
        }

        [HttpGet("AllProducts")]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return products;
        }

        [HttpGet("ProductsById")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product;
        }
    }
}
