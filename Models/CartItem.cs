using System.ComponentModel.DataAnnotations;

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
        public decimal TotalPrice { get; set; }
        public Cart? Cart { get; set; } // Навигация

        public CartItem() { TotalPrice = UnitPrice * Quantity; }
    }
}
