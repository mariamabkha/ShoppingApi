using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IGenericRepository<Payment> _repository;
        private readonly IGenericRepository<UserAccounts> _userAccountRepository;
        private readonly IGenericRepository<Products> _productRepository;

        public PaymentsController(IGenericRepository<Payment> repository, IGenericRepository<UserAccounts> userAccountRepository, IGenericRepository<Products> productRepository)
        {
            _repository = repository;
            _userAccountRepository = userAccountRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get all payments
        /// </summary>
        /// <returns>List of payments</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PaymentModel>), 200)]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _repository.GetAllAsync();

            var paymentModels = payments.Select(payment => new PaymentModel
            {
                Id = payment.Id,
                UserId = payment.UserId,
                ProductId = payment.ProductId,
                Quantity = payment.Quantity,
                Amount = payment.Amount,
                Date = payment.Date
            }).ToList();

            return Ok(paymentModels);
        }

        /// <summary>
        /// Get payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>Payment</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            var paymentModel = new PaymentModel
            {
                UserId = payment.UserId,
                ProductId = payment.ProductId,
                Quantity = payment.Quantity,
                Amount = payment.Amount,
                Date = payment.Date
            };

            return Ok(paymentModel);
        }

        /// <summary>
        /// Add new payment
        /// </summary>
        /// <param name="payment">Payment object</param>
        /// <returns>Created payment</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PaymentModel), 201)]
        public async Task<IActionResult> AddPayment(PaymentModel payment)
        {
            var user = await _userAccountRepository.GetByIdAsync(payment.UserId);
            var product = await _productRepository.GetByIdAsync(payment.ProductId);

            if (user == null || product == null)
            {
                return BadRequest("User or Product not found");
            }

            var newPayment = new Payment
            {
                UserId = payment.UserId,
                ProductId = payment.ProductId,
                Quantity = payment.Quantity,
                Amount = payment.Amount,
                Date = payment.Date
            };

            await _repository.AddAsync(newPayment);
            await _repository.SaveAsync();

            var paymentModel = new PaymentModel
            {
                UserId = newPayment.UserId,
                ProductId = newPayment.ProductId,
                Quantity = newPayment.Quantity,
                Amount = newPayment.Amount,
                Date = newPayment.Date
            };

            return Created($"api/payments/{newPayment.UserId}", paymentModel);
        }

        /// <summary>
        /// Update payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="payment">Updated payment object</param>
        /// <returns>Updated payment</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PaymentModel), 200)]
        public async Task<IActionResult> UpdatePayment(int id, PaymentModel payment)
        {
            var existingPayment = await _repository.GetByIdAsync(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            var user = await _userAccountRepository.GetByIdAsync(payment.UserId);
            var product = await _productRepository.GetByIdAsync(payment.ProductId);

            if (user == null || product == null)
            {
                return BadRequest("User or Product not found");
            }

            existingPayment.UserId = payment.UserId;
            existingPayment.ProductId = payment.ProductId;
            existingPayment.Quantity = payment.Quantity;
            existingPayment.Amount = payment.Amount;
            existingPayment.Date = payment.Date;

            _repository.Update(existingPayment);
            await _repository.SaveAsync();

            var updatedPaymentModel = new PaymentModel
            {
                UserId = existingPayment.UserId,
                ProductId = existingPayment.ProductId,
                Quantity = existingPayment.Quantity,
                Amount = existingPayment.Amount,
                Date = existingPayment.Date
            };

            return Ok(updatedPaymentModel);
        }

        /// <summary>
        /// Delete payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
