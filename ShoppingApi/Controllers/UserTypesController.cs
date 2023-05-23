using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

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
        /// get user types
        /// </summary>
        /// <param name="id">id number of user type</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserTypesModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new UserTypesModel { 
                TypeName = data.TypeName,
                Description = data.Description
            });
        }

        /// <summary>
        /// add new user type
        /// </summary>
        /// <param name="userTypes">user type object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUserType(UserTypesModel userTypes)
        {
            UserTypes u = new UserTypes
            {
                TypeName = userTypes.TypeName,
                Description = userTypes.Description
            };

            await _repository.AddAsync(u);
            await _repository.SaveAsync();

            return Created($"api/usertypes/{u.Id}", u);
        }

        /// <summary>
        /// delete user type
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
