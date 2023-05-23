using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IGenericRepository<Transaction> _repository;
        private readonly IGenericRepository<UserAccounts> _userAccountRepository;

        public TransactionController(IGenericRepository<Transaction> repository, IGenericRepository<UserAccounts> userAccountRepository)
        {
            _repository = repository;
            _userAccountRepository = userAccountRepository;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns>List of transactions</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TransactionModel>), 200)]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _repository.GetAllAsync();

            var transactionModels = transactions.Select(transaction => new TransactionModel
            {
                TransactionType = transaction.TransactionType,
                Description = transaction.Description,
                UserId = transaction.UserId,
                Date = transaction.Date
            }).ToList();

            return Ok(transactionModels);
        }

        /// <summary>
        /// Get transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <returns>Transaction</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TransactionModel), 200)]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionModel = new TransactionModel
            {
                TransactionType = transaction.TransactionType,
                Description = transaction.Description,
                UserId = transaction.UserId,
                Date = transaction.Date
            };

            return Ok(transactionModel);
        }

        /// <summary>
        /// Add new transaction
        /// </summary>
        /// <param name="transaction">Transaction object</param>
        /// <returns>Created transaction</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TransactionModel), 201)]
        public async Task<IActionResult> AddTransaction(TransactionModel transaction)
        {
            var user = await _userAccountRepository.GetByIdAsync(transaction.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var newTransaction = new Transaction
            {
                TransactionType = transaction.TransactionType,
                Description = transaction.Description,
                UserId = transaction.UserId,
                Date = transaction.Date
            };

            await _repository.AddAsync(newTransaction);
            await _repository.SaveAsync();

            var transactionModel = new TransactionModel
            {
                TransactionType = newTransaction.TransactionType,
                Description = newTransaction.Description,
                UserId = newTransaction.UserId,
                Date = newTransaction.Date
            };

            return Created($"api/transactions/{newTransaction.Id}", transactionModel);
        }

        /// <summary>
        /// Update transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <param name="transaction">Updated transaction object</param>
        /// <returns>Updated transaction</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TransactionModel), 200)]
        public async Task<IActionResult> UpdateTransaction(int id, TransactionModel transaction)
        {
            var existingTransaction = await _repository.GetByIdAsync(id);
            if (existingTransaction == null)
            {
                return NotFound();
            }

            var user = await _userAccountRepository.GetByIdAsync(transaction.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            existingTransaction.TransactionType = transaction.TransactionType;
            existingTransaction.Description = transaction.Description;
            existingTransaction.UserId = transaction.UserId;
            existingTransaction.Date = transaction.Date;

            _repository.Update(existingTransaction);
            await _repository.SaveAsync();

            var updatedTransactionModel = new TransactionModel
            {
                TransactionType = existingTransaction.TransactionType,
                Description = existingTransaction.Description,
                UserId = existingTransaction.UserId,
                Date = existingTransaction.Date
            };

            return Ok(updatedTransactionModel);
        }

        /// <summary>
        /// Delete transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <returns>NoContent</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
