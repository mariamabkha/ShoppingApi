using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/usertypes")]
    public class UserTypesController : ControllerBase
    {
        private readonly IGenericRepository<UserTypes> _repository;

        public UserTypesController(IGenericRepository<UserTypes> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all user types
        /// </summary>
        /// <returns>List of user types</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTypesModel>), 200)]
        public async Task<IActionResult> GetAllUserTypes()
        {
            var userTypes = await _repository.GetAllAsync();
            var userTypeModels = new List<UserTypesModel>();

            foreach (var userType in userTypes)
            {
                userTypeModels.Add(new UserTypesModel
                {
                    TypeName = userType.TypeName,
                    Description = userType.Description
                });
            }

            return Ok(userTypeModels);
        }

        /// <summary>
        /// Get user type by ID
        /// </summary>
        /// <param name="id">ID of the user type</param>
        /// <returns>User type with the specified ID</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserTypesModel), 200)]
        public async Task<IActionResult> GetUserType(int id)
        {
            var userType = await _repository.GetByIdAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            var userTypeModel = new UserTypesModel
            {
                TypeName = userType.TypeName,
                Description = userType.Description
            };

            return Ok(userTypeModel);
        }

        /// <summary>
        /// Add a new user type
        /// </summary>
        /// <param name="userTypes">User type object</param>
        /// <returns>Created user type</returns>
        [HttpPost]
        public async Task<IActionResult> AddUserType(UserTypesModel userTypes)
        {
            var newUserType = new UserTypes
            {
                TypeName = userTypes.TypeName,
                Description = userTypes.Description
            };

            await _repository.AddAsync(newUserType);
            await _repository.SaveAsync();

            return Created($"api/usertypes/{newUserType.Id}", newUserType);
        }

        /// <summary>
        /// Delete a user type by ID
        /// </summary>
        /// <param name="id">ID of the user type</param>
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

