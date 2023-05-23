using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly IGenericRepository<Cart> _repository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Products> _productRepository;

        public CartController(IGenericRepository<Cart> repository, IGenericRepository<Order> orderRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Add a product to the cart.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="productId"> ID of the product to add.</param>
        /// <returns>updated cart.</returns>
        [HttpPost("{userId}/add/{productId}")]
        [ProducesResponseType(typeof(CartModel), 200)]
        public async Task<IActionResult> AddToCart(int userId, int productId)
        {
            var cart = await _repository.GetByIdAsync(userId);

            if (cart == null)
            {
                // Create a new cart if it doesn't exist
                cart = new Cart
                {
                    UserId = userId,
                    IsFilled = false,
                    Count = 5, // Set the maximum cart count to 5
                    Product = new List<Products>()  // Initialize the Products collection
                };
                await _repository.AddAsync(cart);
            }
            else if (cart.Product.Count >= cart.Count)
            {
                return BadRequest("Cart is already full. Cannot add more items.");
            }

            // Retrieve the product from the database based on the productId
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return BadRequest("Invalid product ID.");
            }

            cart.Product.Add(product);

            cart.IsFilled = cart.Product.Count >= cart.Count;

            await _repository.SaveAsync();

            var cartModel = new CartModel
            {
                ProductId = productId,
                UserId = userId,
                Count = cart.Product.Count
            };

            return Ok(cartModel);
        }


        /// <summary>
        /// Get the cart details for a specific user.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>The cart details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CartModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new CartModel { 
                ProductId = data.ProductId,
                UserId = data.UserId
            });
        }


        /// <summary>
        /// Add a new cart.
        /// </summary>
        /// <param name="cart">cart data to add.</param>
        /// <returns>The created cart.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCart(CartModel cart)
        {
            Cart c = new Cart
            {
                ProductId = cart.ProductId,
                UserId = cart.UserId
            };

            await _repository.AddAsync(c);
            await _repository.SaveAsync();

            return Created($"api/cart/{c.Id}", c);
        }

        /// <summary>
        /// Delete a cart by its ID and associated product ID.
        /// </summary>
        /// <param name="id">ID of the cart.</param>
        /// <param name="productId">ID of the associated product.</param>
        /// <returns>An action result indicating the success of the operation.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int productId)
        {
            _repository.Delete(id); _repository.Delete(productId);
            await _repository.SaveAsync();
            return Ok();
        }

    }
}
