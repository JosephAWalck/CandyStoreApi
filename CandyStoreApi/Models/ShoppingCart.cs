using Microsoft.EntityFrameworkCore;

namespace CandyStoreApi.Models
{
    public class ShoppingCart: IShoppingCart
    {
        private readonly CandyStoreApiContext _candyStoreApiDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
        private ShoppingCart(CandyStoreApiContext candyStoreApiDbContext)
        {
            _candyStoreApiDbContext = candyStoreApiDbContext;          
        }
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            CandyStoreApiContext context = services.GetService<CandyStoreApiContext>() ?? throw new Exception("Error Initializing");
            string cartID = session?.GetString("CartID") ?? Guid.NewGuid().ToString();
            session?.SetString("CartID", cartID);
            return new ShoppingCart(context)
            {
                ShoppingCartId = cartID
            };
        }

        public void AddToCart(Candy candy)
        {
            var shoppingCartItem = _candyStoreApiDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Candy = candy,
                    Quantity = 1
                };

                _candyStoreApiDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }

            _candyStoreApiDbContext.SaveChanges();
        }
        public void RemoveFromCart(Candy candy)
        {
            var shoppingCartItem = _candyStoreApiDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                }
                else
                {
                    _candyStoreApiDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _candyStoreApiDbContext.SaveChanges();
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= await _candyStoreApiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Candy)
                .ToListAsync();
        }
        public void ClearCart()
        {
            var cartItems = _candyStoreApiDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId != ShoppingCartId);

            _candyStoreApiDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _candyStoreApiDbContext.SaveChanges();
        }
        public async Task<decimal> GetShoppingCartTotal()
        {
            var total = await _candyStoreApiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(C => C.Candy.Price * C.Quantity).SumAsync();
            return total;
        }
    }
}
