namespace CandyStoreApi.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Candy candy);
        void RemoveFromCart(Candy candy);
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        void ClearCart();
        Task<decimal> GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set;  }
    }
}
