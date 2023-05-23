using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/useraccounts")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IGenericRepository<UserAccounts> _repository;

        public UserAccountsController(IGenericRepository<UserAccounts> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a user account by ID.
        /// </summary>
        /// <param name="id">The ID of the user account.</param>
        /// <returns>The user account.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserAccountsModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(new UserAccounts
            {
                TypeId = data.TypeId,
                Name = data.Name,
                Age = data.Age,
                Gender = data.Gender,
                Address = data.Address,
                ContactNumber = data.ContactNumber,
                Username = data.Username,
                Password = data.Password
            });

            //return Ok(userModel);
        }

        /// <summary>
        /// Add a new user account.
        /// </summary>
        /// <param name="userModel">The user account model.</param>
        /// <returns>The created user account.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserAccountsModel userModel)
        {
            var userAccount = new UserAccounts
            {
                TypeId = userModel.TypeId,
                Name = userModel.Name,
                Age = userModel.Age,
                Gender = userModel.Gender,
                Address = userModel.Address,
                ContactNumber = userModel.ContactNumber,
                Username = userModel.Username,
                Password = userModel.Password
            };

            await _repository.AddAsync(userAccount);
            await _repository.SaveAsync();

            return Created($"api/useraccounts/{userAccount.Id}", userAccount);
        }

        /// <summary>
        /// Delete a user account by ID.
        /// </summary>
        /// <param name="id">The ID of the user account to delete.</param>
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
