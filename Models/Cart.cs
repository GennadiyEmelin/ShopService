namespace ShopService.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } // Когда создана
        public DateTime UpdatedAt { get; set; } // Когда обновлена
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // связь один ко многим

        public Cart() { }
    }
}
