using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository<Payment> _repository;

        public PaymentController(IGenericRepository<Payment> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>Payment details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var paymentModel = new PaymentModel
            {
                Id = data.Id,
                ProductId = data.ProductId,
                Quantity = data.Quantity,
                Amount = data.Amount,
                Date = data.Date,
                UserId = data.UserId
            };
            return Ok(paymentModel);
        }

        /// <summary>
        /// Add new payment
        /// </summary>
        /// <param name="paymentModel">Payment details</param>
        /// <returns>Created payment</returns>
        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentModel paymentModel)
        {
            var payment = new Payment
            {
                ProductId = paymentModel.ProductId,
                Quantity = paymentModel.Quantity,
                Amount = paymentModel.Amount,
                Date = paymentModel.Date,
                UserId = paymentModel.UserId
            };

            await _repository.AddAsync(payment);
            await _repository.SaveAsync();

            return Created($"api/payments/{payment.Id}", payment);
        }

        /// <summary>
        /// Delete payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>Ok if successful</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return Ok();
        }
    }
}
