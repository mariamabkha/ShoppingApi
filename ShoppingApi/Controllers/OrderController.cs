using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IGenericRepository<UserAccounts> _userAccountRepository;
        private readonly IGenericRepository<Cart> _cartRepository;

        public OrdersController(
            IGenericRepository<Order> repository,
            IGenericRepository<UserAccounts> userAccountRepository,
            IGenericRepository<Cart> cartRepository)
        {
            _repository = repository;
            _userAccountRepository = userAccountRepository;
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _repository.GetAllAsync();

            var orderModels = orders.Select(order => new OrderModel
            {
                UserId = order.UserId,
                CartId = order.CartId,
                Date = order.Date
            }).ToList();

            return Ok(orderModels);
        }

        /// <summary>
        /// Get order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OrderModel), 200)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderModel = new OrderModel
            {
                UserId = order.UserId,
                CartId = order.CartId,
                Date = order.Date
            };

            return Ok(orderModel);
        }

        /// <summary>
        /// Get orders for a specific user based on the passed date.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="date">Filter date</param>
        /// <returns>List of filtered orders</returns>
        [HttpGet("user/{userId:int}")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetOrdersForUser(int userId, [FromQuery] DateTime date)
        {
            var user = await _userAccountRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var orders = await _repository.GetAllAsync();

            var filteredOrders = orders.Where(order => order.UserId == userId && order.Date.Date == date.Date);

            var orderModels = filteredOrders.Select(order => new OrderModel
            {
                UserId = order.UserId,
                CartId = order.CartId,
                Date = order.Date
            }).ToList();

            return Ok(orderModels);
        }

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Created order</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderModel), 201)]
        public async Task<IActionResult> AddOrder(OrderModel order)
        {
            try
            {
                var user = await _userAccountRepository.GetByIdAsync(order.UserId);
                var cart = await _cartRepository.GetByIdAsync(order.CartId);

                if (user == null || cart == null)
                {
                    return BadRequest("User or Cart not found");
                }

                var newOrder = new Order
                {
                    UserId = order.UserId,
                    CartId = order.CartId,
                    Date = order.Date
                };

                await _repository.AddAsync(newOrder);
                await _repository.SaveAsync();

                var orderModel = new OrderModel
                {
                    UserId = newOrder.UserId,
                    CartId = newOrder.CartId,
                    Date = newOrder.Date
                };

                return Created($"api/orders/{newOrder.UserId}", orderModel);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to create order. Duplicate CartId detected. Please add new Cart");
            }
        }

        /// <summary>
        /// Update an existing order.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="order">Updated order object</param>
        /// <returns>Updated order</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(OrderModel), 200)]
        public async Task<IActionResult> UpdateOrder(int id, OrderModel order)
        {
            var existingOrder = await _repository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            var user = await _userAccountRepository.GetByIdAsync(order.UserId);
            var cart = await _cartRepository.GetByIdAsync(order.CartId);

            if (user == null || cart == null)
            {
                return BadRequest("User or Cart not found");
            }

            existingOrder.UserId = order.UserId;
            existingOrder.CartId = order.CartId;
            existingOrder.Date = order.Date;

            _repository.Update(existingOrder);
            await _repository.SaveAsync();

            var updatedOrderModel = new OrderModel
            {
                UserId = existingOrder.UserId,
                CartId = existingOrder.CartId,
                Date = existingOrder.Date
            };

            return Ok(updatedOrderModel);
        }

        /// <summary>
        /// Get the list of orders for the current day based on the provided user ID.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of orders for the current day</returns>
        [HttpGet("user/{userId:int}/current-day")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetCurrentDayOrdersByUser(int userId)
        {
            var user = await _userAccountRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var currentDate = DateTime.Now.Date;
            var orders = await _repository.GetAllAsync(o => o.UserId == userId && o.Date.Date == currentDate);

            var orderModels = orders.Select(order => new OrderModel
            {
                UserId = order.UserId,
                CartId = order.CartId,
                Date = order.Date
            }).ToList();

            return Ok(orderModels);
        }

        /// <summary>
        /// Delete an order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
