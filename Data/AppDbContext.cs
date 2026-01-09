using Microsoft.EntityFrameworkCore;
using ShopService.Models;

namespace ShopService.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) 
        { 
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.Ignore(e => e.TotalPrice);
                entity.Property(ci => ci.ProductName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(ci => ci.UnitPrice)
                      .HasPrecision(18, 2);
            });
        }
    }


}
