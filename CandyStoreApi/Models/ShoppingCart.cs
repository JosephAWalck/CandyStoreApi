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
                    Count = 1
                };

                _candyStoreApiDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Count++;
            }

            _candyStoreApiDbContext.SaveChanges();
        }
        public int RemoveFromCart(Candy candy)
        {
            var shoppingCartItem = _candyStoreApiDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Count > 1)
                {
                    shoppingCartItem.Count--;
                    localAmount = shoppingCartItem.Count;
                }
                else
                {
                    _candyStoreApiDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _candyStoreApiDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _candyStoreApiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Candy)
                .ToList();
        }
        public void ClearCart()
        {
            var cartItems = _candyStoreApiDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId != ShoppingCartId);

            _candyStoreApiDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _candyStoreApiDbContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            var total = _candyStoreApiDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(C => C.Candy.Price).Sum();
            return total;
        }
    }
}
