
using Microsoft.EntityFrameworkCore;

namespace CandyStoreApi.Models
{
    public class CandyStoreApiContext : DbContext
    {
        public CandyStoreApiContext(DbContextOptions<CandyStoreApiContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Candy> Candies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
