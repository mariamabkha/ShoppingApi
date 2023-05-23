using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using ShoppingApi.Models;


namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IGenericRepository<Transaction> _repository;

        public TransactionController(IGenericRepository<Transaction> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <returns>Transaction details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            var transactionModel = new TransactionModel
            {
                Id = data.Id,
                TransactionType = data.TransactionType,
                Description = data.Description,
                UserId = data.UserId,
                Date = data.Date
            };
            return Ok(transactionModel);
        }

        /// <summary>
        /// Add new transaction
        /// </summary>
        /// <param name="transactionModel">Transaction details</param>
        /// <returns>Created transaction</returns>
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionModel transactionModel)
        {
            var transaction = new Transaction
            {
                TransactionType = transactionModel.TransactionType,
                Description = transactionModel.Description,
                UserId = transactionModel.UserId,
                Date = transactionModel.Date
            };

            await _repository.AddAsync(transaction);
            await _repository.SaveAsync();

            return Created($"api/transactions/{transaction.Id}", transaction);
        }

        /// <summary>
        /// Delete transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID</param>
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
