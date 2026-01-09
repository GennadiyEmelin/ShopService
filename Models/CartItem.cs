using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopService.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int CartId{ get; set; } //FK
        public int ProductId { get; set; } // ссылка на товар
        public string? ProductName { get; set; } // Денормализация имени товара
        public decimal UnitPrice { get; set; } // Цена на моменть добавления
        public int Quantity { get; set; } // Количество 
        [NotMapped]
        public decimal TotalPrice => UnitPrice * Quantity;
        public Cart? Cart { get; set; } // Навигация

    }
}
