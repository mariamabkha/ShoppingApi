using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Models;
using ShoppingApi.Repository;
using ShoppingApi.ViewModel;

namespace ShoppingApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Products> _repository;
        private readonly IGenericRepository<Categories> _categoryRepository;
        private readonly IGenericRepository<UserAccounts> _userAccountRepository;

        public ProductsController(
            IGenericRepository<Products> repository,
            IGenericRepository<Categories> categoryRepository,
            IGenericRepository<UserAccounts> userAccountRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _userAccountRepository = userAccountRepository;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repository.GetAllAsync();
            var productModels = new List<ProductModel>();

            foreach (var product in products)
            {
                var productModel = new ProductModel
                {
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.Count,
                    Description = product.Description,
                    UserId = product.UserId
                };

                productModels.Add(productModel);
            }

            return Ok(productModels);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productModel = new ProductModel
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                Description = product.Description,
                UserId = product.UserId
            };

            return Ok(productModel);
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel product)
        {
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var user = await _userAccountRepository.GetByIdAsync(product.UserId);

            if (category == null || user == null)
            {
                return BadRequest("Category or User not found");
            }

            var newProduct = new Products
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                Description = product.Description,
                CategoryId = product.CategoryId,
                UserId = product.UserId
            };

            await _repository.AddAsync(newProduct);
            await _repository.SaveAsync();

           
            return Ok("added product");

        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="product">Updated product object</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel product)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }

            var userAccount = await _userAccountRepository.GetByIdAsync(product.UserId);
            if (userAccount == null)
            {
                return BadRequest("Invalid UserId");
            }

            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Count = product.Count;
            existingProduct.Description = product.Description;
            existingProduct.UserId = product.UserId;

            _repository.Update(existingProduct);
            await _repository.SaveAsync();

            /*var updatedProductModel = new ProductModel
            {
                CategoryId = existingProduct.CategoryId,
                Name = existingProduct.Name,
                Price = existingProduct.Price,
                Count = existingProduct.Count,
                Description = existingProduct.Description,
                UserId = existingProduct.UserId
            };*/

            return Ok(existingProduct);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Result of the deletion</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return Ok();
        }
    }
}

