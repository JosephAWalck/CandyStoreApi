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

        public async Task<Order> CreateOrder(OrderDTO orderDTO)
        {
            Order order = new Order
            {
                OrderDetails = new List<OrderDetail>(),
                FirstName = orderDTO.FirstName,
                LastName = orderDTO.LastName,
                AddressLine1 = orderDTO.AddressLine1,
                AddressLine2 = orderDTO.AddressLine2,
                ZipCode = orderDTO.ZipCode,
                City = orderDTO.City,
                State = orderDTO.State,
                Country = orderDTO.Country,
                PhoneNumber = orderDTO.PhoneNumber,
                Email = orderDTO.Email,
                OrderTotal = await _shoppingCart.GetShoppingCartTotal(),
                OrderPlaced = DateTime.Now,
            };

            List<ShoppingCartItem> shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = shoppingCartItem.Quantity,
                    CandyId = shoppingCartItem.Candy.CandyId,
                    Price = shoppingCartItem.Candy.Price
                };
                shoppingCartItem.Candy.Inventory -= shoppingCartItem.Quantity;
                order.OrderDetails.Add(orderDetail); 
            }

            _candyStoreApiContext.Orders.Add(order); 
            _candyStoreApiContext.SaveChanges();

            return order;
        }
    }

    
}
