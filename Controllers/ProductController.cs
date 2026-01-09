using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.DTOs;
using ShopService.Models;
using ShopService.Services;

namespace ShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            await _productService.CreateAsync(dto.Name, dto.Description, dto.Price, dto.StockQuantity);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
        {
            await _productService.UpdateAsync(id, dto.Name, dto.Description, dto.Price, dto.StockQuantity);
            return Ok();
        }
    }
}
