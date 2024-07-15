namespace CandyStoreApi.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Candy Candy { get; set; } = default!;
        public int Quantity{  get; set; }
        public string? ShoppingCartId {  get; set; }
    }
}
