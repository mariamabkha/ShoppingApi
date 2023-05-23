namespace ShoppingApi.ViewModel
{
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Gets or sets the count of the product.
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int UserId { get; set; }
    }
}
