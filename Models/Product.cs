using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ShopService.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } // Название товара
        public string? Description { get; set; } // Описание товара
        [Required]
        public decimal Price { get; set; } // Цена товара
        [Required]
        public int StockQuantity { get; set; } // Количество на складе
        [Required]
        public bool IsActive { get; set; } // Активен ли товар
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Когда создан

        public Product(string name, string description, decimal price, int stockQuantity, bool isActive) 
        {
            Name = name;
            Description = description;
            if(price < 0)
                throw new Exception();
            Price = price;
            if(stockQuantity < 0)
                throw new Exception();
            StockQuantity = stockQuantity;
            IsActive = isActive;
        }
        public Product() { }

    }
}
