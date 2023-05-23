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
        private readonly IGenericRepository<UserAccounts> _userAccountRepository;

        public DeliveriesController(IGenericRepository<Deliveries> repository, IGenericRepository<UserAccounts> userAccountRepository)
        {
            _repository = repository;
            _userAccountRepository = userAccountRepository;
        }

        /// <summary>
        /// Get all deliveries
        /// </summary>
        /// <returns>List of deliveries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeliveriesModel>), 200)]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _repository.GetAllAsync();
            var deliveryModels = deliveries.Select(delivery => new DeliveriesModel
            {
                UserId = delivery.UserId,
                Date = delivery.Date
            });

            return Ok(deliveryModels);
        }

        /// <summary>
        /// Get delivery by ID
        /// </summary>
        /// <param name="id">Delivery ID</param>
        /// <returns>Delivery</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DeliveriesModel), 200)]
        public async Task<IActionResult> GetDelivery(int id)
        {
            var delivery = await _repository.GetByIdAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            var deliveryModel = new DeliveriesModel
            {
                UserId = delivery.UserId,
                Date = delivery.Date
            };

            return Ok(deliveryModel);
        }

        /// <summary>
        /// Add new delivery
        /// </summary>
        /// <param name="delivery">Delivery object</param>
        /// <returns>Created delivery</returns>
        [HttpPost]
        [ProducesResponseType(typeof(DeliveriesModel), 201)]
        public async Task<IActionResult> AddDelivery(DeliveriesModel delivery)
        {
            var user = await _userAccountRepository.GetByIdAsync(delivery.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var newDelivery = new Deliveries
            {
                UserId = delivery.UserId,
                Date = delivery.Date
            };

            await _repository.AddAsync(newDelivery);
            await _repository.SaveAsync();

            /*var deliveryModel = new DeliveriesModel
            {
                UserId = newDelivery.UserId,
                Date = newDelivery.Date
            };

            return Created($"api/deliveries/{newDelivery.Id}", newDelivery);*/
            return Ok("delivery added");
        }

        /// <summary>
        /// Update delivery by ID
        /// </summary>
        /// <param name="id">Delivery ID</param>
        /// <param name="delivery">Updated delivery object</param>
        /// <returns>Updated delivery</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DeliveriesModel), 200)]
        public async Task<IActionResult> UpdateDelivery(int id, DeliveriesModel delivery)
        {
            var existingDelivery = await _repository.GetByIdAsync(id);
            if (existingDelivery == null)
            {
                return NotFound();
            }

            var user = await _userAccountRepository.GetByIdAsync(delivery.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            existingDelivery.UserId = delivery.UserId;
            existingDelivery.Date = delivery.Date;

            _repository.Update(existingDelivery);
            await _repository.SaveAsync();

            var updatedDeliveryModel = new DeliveriesModel
            {
                UserId = existingDelivery.UserId,
                Date = existingDelivery.Date
            };

            return Ok(updatedDeliveryModel);
        }

        /// <summary>
        /// Delete delivery by ID
        /// </summary>
        /// <param name="id">Delivery ID</param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return Ok();
        }
    }
}