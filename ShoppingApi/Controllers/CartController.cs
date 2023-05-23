using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<UserAccounts> _userRepository;
        private readonly IGenericRepository<Products> _productRepository;



        public CartController(IGenericRepository<Cart> cartRepository, IGenericRepository<UserAccounts> userRepository, IGenericRepository<Products> productRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Add a product to the cart.
        /// </summary>
        /// <param name="cart">CartModel containing product and user details</param>
        /// <returns>Created cart with product details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CartModel), 201)]
        public async Task<IActionResult> AddToCart(CartModel cart)
        {
            if (cart.IsFilled)
            {
                return BadRequest("Cart is already filled.");
            }

            var user = await _userRepository.GetByIdAsync(cart.UserId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Check if the product exists
            var product = await _productRepository.GetByIdAsync(cart.ProductId);
            if (product == null)
            {
                return BadRequest("Product not found.");
            }

            var newCart = new Cart
            {
                ProductId = cart.ProductId,
                UserId = cart.UserId,
                Count = cart.Count,
                IsFilled = cart.IsFilled
            };

            await _cartRepository.AddAsync(newCart);
            await _cartRepository.SaveAsync();

            var cartModel = new CartModel
            {
                ProductId = newCart.ProductId,
                UserId = newCart.UserId,
                Count = newCart.Count,
                IsFilled = newCart.IsFilled
            };

            return Created($"api/cart/{newCart.Id}", cartModel);
        }


        /// <summary>
        /// Get cart by ID.
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <returns>Cart details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CartModel), 200)]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            var cartModel = new CartModel
            {
                ProductId = cart.ProductId,
                UserId = cart.UserId,
                Count = cart.Count,
                IsFilled = cart.IsFilled
            };

            return Ok(cartModel);
        }

        /// <summary>
        /// Delete cart by ID.
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            _cartRepository.Delete(id);
            await _cartRepository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Update cart by ID.
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <param name="cart">Updated cart details</param>
        /// <returns>Updated cart</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(CartModel), 200)]
        public async Task<IActionResult> UpdateCart(int id, CartModel cart)
        {
            var existingCart = await _cartRepository.GetByIdAsync(id);
            if (existingCart == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(cart.UserId);
            var product = await _productRepository.GetByIdAsync(cart.ProductId);

            if (user == null || product == null)
            {
                return BadRequest("User or Product not found");
            }

            existingCart.UserId = cart.UserId;
            existingCart.ProductId = cart.ProductId;
            existingCart.Count = cart.Count;
            existingCart.IsFilled = cart.IsFilled;

            _cartRepository.Update(existingCart);
            await _cartRepository.SaveAsync();

            var updatedCartModel = new CartModel
            {
                ProductId = existingCart.ProductId,
                UserId = existingCart.UserId,
                Count = existingCart.Count,
                IsFilled = existingCart.IsFilled
            };

            return Ok(updatedCartModel);
        }
    }

}



