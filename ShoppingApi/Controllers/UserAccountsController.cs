using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/useraccounts")]
    public class UserAccountsController : ControllerBase
    {
        private readonly IGenericRepository<UserAccounts> _repository;
        private readonly IGenericRepository<UserTypes> _userTypesRepository;

        public UserAccountsController(IGenericRepository<UserAccounts> repository, IGenericRepository<UserTypes> userTypesRepository)
        {
            _repository = repository;
            _userTypesRepository = userTypesRepository;
        }

        /// <summary>
        /// Get all user accounts
        /// </summary>
        /// <returns>List of user accounts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserAccountsModel>), 200)]
        public async Task<IActionResult> GetAllUserAccounts()
        {
            var userAccounts = await _repository.GetAllAsync();
            var userAccountModels = new List<UserAccountsModel>();

            foreach (var userAccount in userAccounts)
            {
                userAccountModels.Add(new UserAccountsModel
                {
                    TypeId = userAccount.TypeId,
                    Name = userAccount.Name,
                    Age = userAccount.Age,
                    Gender = userAccount.Gender,
                    Address = userAccount.Address,
                    ContactNumber = userAccount.ContactNumber,
                    Username = userAccount.Username,
                    Password = userAccount.Password
                });
            }

            return Ok(userAccountModels);
        }

        /// <summary>
        /// Get user account by ID
        /// </summary>
        /// <param name="id">ID of the user account</param>
        /// <returns>User account with the specified ID</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserAccountsModel), 200)]
        public async Task<IActionResult> GetUserAccount(int id)
        {
            var userAccount = await _repository.GetByIdAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            var userAccountModel = new UserAccountsModel
            {
                TypeId = userAccount.TypeId,
                Name = userAccount.Name,
                Age = userAccount.Age,
                Gender = userAccount.Gender,
                Address = userAccount.Address,
                ContactNumber = userAccount.ContactNumber,
                Username = userAccount.Username,
                Password = userAccount.Password
            };

            return Ok(userAccountModel);
        }

        /// <summary>
        /// Add a new user account
        /// </summary>
        /// <param name="userAccounts">User account object</param>
        /// <returns>Created user account</returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAccount(UserAccountsModel userAccounts)
        {
            var userType = await _userTypesRepository.GetByIdAsync(userAccounts.TypeId);
            if (userType == null)
            {
                return BadRequest("Invalid TypeId");
            }

            var newUserAccount = new UserAccounts
            {
                TypeId = userAccounts.TypeId,
                Name = userAccounts.Name,
                Age = userAccounts.Age,
                Gender = userAccounts.Gender,
                Address = userAccounts.Address,
                ContactNumber = userAccounts.ContactNumber,
                Username = userAccounts.Username,
                Password = userAccounts.Password
            };

            var serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            await _repository.AddAsync(newUserAccount);
            await _repository.SaveAsync();

            var serializedUserAccount = JsonSerializer.Serialize(newUserAccount, serializerOptions);

            return Created($"api/useraccounts/{newUserAccount.Id}", serializedUserAccount);
        }

        /// <summary>
        /// Update a user account
        /// </summary>
        /// <param name="id">User account ID</param>
        /// <param name="userAccounts">Updated user account object</param>
        /// <returns>Updated user account</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserAccount(int id, UserAccountsModel userAccounts)
        {
            var existingUserAccount = await _repository.GetByIdAsync(id);
            if (existingUserAccount == null)
            {
                return NotFound();
            }

            var userType = await _userTypesRepository.GetByIdAsync(userAccounts.TypeId);
            if (userType == null)
            {
                return BadRequest("Invalid TypeId");
            }

            existingUserAccount.TypeId = userAccounts.TypeId;
            existingUserAccount.Name = userAccounts.Name;
            existingUserAccount.Age = userAccounts.Age;
            existingUserAccount.Gender = userAccounts.Gender;
            existingUserAccount.Address = userAccounts.Address;
            existingUserAccount.ContactNumber = userAccounts.ContactNumber;
            existingUserAccount.Username = userAccounts.Username;
            existingUserAccount.Password = userAccounts.Password;

            // Configure JSON serializer options to ignore reference loops
            var serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            _repository.Update(existingUserAccount);
            await _repository.SaveAsync();

            // Serialize the updated user account without reference loops
            var serializedUserAccount = JsonSerializer.Serialize(existingUserAccount, serializerOptions);

            return Ok(serializedUserAccount);
        }

        /// <summary>
        /// Delete a user account by ID
        /// </summary>
        /// <param name="id">ID of the user account</param>
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