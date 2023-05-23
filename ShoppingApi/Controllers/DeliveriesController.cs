using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IGenericRepository<Deliveries> _repository;

        public DeliveriesController(IGenericRepository<Deliveries> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get delivery by ID.
        /// </summary>
        /// <param name="id">ID of the delivery.</param>
        /// <returns>The delivery.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeliveriesModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            // Map the entity to the view model
            var deliveryModel = new DeliveriesModel
            {
                Id = data.Id,
                UserId = data.UserId,
                Date = data.Date
            };

            return Ok(deliveryModel);
        }

        /// <summary>
        /// Add a new delivery.
        /// </summary>
        /// <param name="deliveryModel">The delivery model.</param>
        /// <returns>The created delivery.</returns>
        [HttpPost]
        public async Task<IActionResult> AddDelivery(DeliveriesModel deliveryModel)
        {
            var delivery = new Deliveries
            {
                UserId = deliveryModel.UserId,
                Date = deliveryModel.Date
            };

            await _repository.AddAsync(delivery);
            await _repository.SaveAsync();

            // Map the entity to the view model
            var createdDeliveryModel = new DeliveriesModel
            {
                Id = delivery.Id,
                UserId = delivery.UserId,
                Date = delivery.Date
            };

            return Created($"api/deliveries/{delivery.Id}", createdDeliveryModel);
        }

        /// <summary>
        /// Delete a delivery by ID.
        /// </summary>
        /// <param name="id">ID of the delivery to delete.</param>
        /// <returns>OK if deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();

            return Ok();
        }
    }
}
