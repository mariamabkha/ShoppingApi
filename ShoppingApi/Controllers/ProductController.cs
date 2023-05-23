using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Products> _repository;
        public ProductController(IGenericRepository<Products> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a product by its ID.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        /// <returns>product details.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new ProductModel
            {
                CategoryId = data.CategoryId,
                Name = data.Name,
                Price = data.Price,
                Count = data.Count,
                Description = data.Description,
                UserId  = data.UserId
            }) ;
        }

        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="productModel">product data to add.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            Products p = new Products
            {
               CategoryId = productModel.CategoryId,
               Name = productModel.Name,
               Price = productModel.Price,
               Count = productModel.Count,
               Description = productModel.Description,
               UserId = productModel.UserId
            };

            await _repository.AddAsync(p);
            await _repository.SaveAsync();

            return Created($"api/products/{p.Id}", p);
        }

        /// <summary>
        /// delete product
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

