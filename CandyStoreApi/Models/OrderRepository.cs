namespace CandyStoreApi.Models
{
    public class OrderRepository : IOrderRepository

    {
        private readonly CandyStoreApiContext _candyStoreApiContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(CandyStoreApiContext candyStoreApiContext, IShoppingCart shoppingCart)
        {
            _candyStoreApiContext = candyStoreApiContext;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem> shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = await _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Count,
                    CandyId = shoppingCartItem.Candy.CandyId,
                    Price = shoppingCartItem.Candy.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _candyStoreApiContext.Orders.Add(order);
            _candyStoreApiContext.SaveChanges();
        }
    }

    
}
