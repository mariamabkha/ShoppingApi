using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using System.Linq.Expressions;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IGenericRepository<Order> _repository;
        public OrderController(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get orders for a specific user based on date submitted and user ID.
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <param name="date">date submitted</param>
        /// <returns>List of orders matching the criteria</returns>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetFilteredOrders(int userId, DateTime date)
        {
            // Filter orders based on user ID and date submitted
            Expression<Func<Order, bool>> expression = o => o.UserId == userId && o.Date.Date == date.Date;
            var filteredOrders = await _repository.GetAllAsync(expression);

            // Map the filtered orders to the OrderModel
            var orderModels = filteredOrders.Select(o => new OrderModel
            {
                UserId = o.UserId,
                CartId = o.CartId,
                Date = o.Date
            }).ToList();

            return Ok(orderModels);
        }

        /// <summary>
        /// Get the current day's order list by user.
        /// </summary>
        /// <param name="userId">user ID.</param>
        /// <returns>List of orders for the current day by the specified user.</returns>
        [HttpGet("current-day")]
        [ProducesResponseType(typeof(List<OrderModel>), 200)]
        public async Task<IActionResult> GetCurrentDayOrdersByUser(int userId)
        {
            // Get the current date
            DateTime currentDate = DateTime.UtcNow.Date;

            // Filter orders for the current day by user ID
            Expression<Func<Order, bool>> expression = o => o.UserId == userId && o.Date.Date == currentDate;
            var filteredOrders = await _repository.GetAllAsync(expression);

            // Map the filtered orders to the OrderModel
            var orderModels = filteredOrders.Select(o => new OrderModel
            {
                UserId = o.UserId,
                CartId = o.CartId,
                Date = o.Date
            }).ToList();

            return Ok(orderModels);
        }

        /// <summary>
        /// get order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(OrderModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new OrderModel
            {
                UserId = data.UserId,
                CartId = data.CartId,
                Date = data.Date
            });
        }

        /// <summary>
        /// add new order
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderModel orderModel)
        {
            Order o = new Order
            {
                UserId = orderModel.UserId,
                CartId = orderModel.CartId,
                Date = orderModel.Date
            };

            await _repository.AddAsync(o);
            await _repository.SaveAsync();

            return Created($"api/products/{o.Id}", o);
        }

        /// <summary>
        /// delete order
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();

            return Ok();
        }
    }
}
