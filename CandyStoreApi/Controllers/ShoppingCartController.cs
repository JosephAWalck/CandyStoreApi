using CandyStoreApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(CandyStoreApiContext context, ICandyRepository candyRepository, IShoppingCart shoppingCart)
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShoppingCartItem>>> GetShoppingCart()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            return items;
        }
        [Route("/api/[controller]/add")]
        [HttpPut]
        public async Task<ActionResult<List<ShoppingCartItem>>> AddToShoppingCart(int CandyID)
        {
            var selectedCandy = await _candyRepository.GetCandyById(CandyID);
            if (selectedCandy != null) 
            {
                _shoppingCart.AddToCart(selectedCandy);
            }
            return await _shoppingCart.GetShoppingCartItems();
        }

        [Route("/api/[controller]/remove")]
        [HttpPut]
        public async Task<ActionResult<List<ShoppingCartItem>>> RemoveFromCart(int CandyID)
        {
            var selectedCandy = await _candyRepository.GetCandyById(CandyID);
            if (selectedCandy != null)
            {
                _shoppingCart.RemoveFromCart(selectedCandy);
            }
            return await _shoppingCart.GetShoppingCartItems();
        }
    }
}
