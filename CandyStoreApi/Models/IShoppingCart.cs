namespace CandyStoreApi.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Candy candy);
        int RemoveFromCart(Candy candy);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set;  }
    }
}
