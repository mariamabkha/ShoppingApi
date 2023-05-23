using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericRepository<Categories> _repository;
        public CategoriesController(IGenericRepository<Categories> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// get category
        /// </summary>
        /// <param name="id"> id number of category</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CategoriesModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(new CategoriesModel { Name = data.Name, Description = data.Description });
        }

        /// <summary>
        /// add new category
        /// </summary>
        /// <param name="categories">category object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoriesModel categories)
        {
            Categories c = new Categories
            {
                Name = categories.Name,
                Description = categories.Description
            };

            await _repository.AddAsync(c);
            await _repository.SaveAsync();
           
            return Created($"api/categories/{c.Id}", c);
        }


        /// <summary>
        /// delete category
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
