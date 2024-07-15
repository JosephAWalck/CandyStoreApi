using CandyStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandyStoreApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        [Route("Checkout")]
        [HttpPost]
        public async Task<ActionResult<Order>> Checkout(OrderDTO orderDTO)
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                return BadRequest();
            }

            var order = await _orderRepository.CreateOrder(orderDTO);
            _shoppingCart.ClearCart();
            return order;
        }
    }
}
