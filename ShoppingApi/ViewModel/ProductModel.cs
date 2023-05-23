namespace ShoppingApi.ViewModel
{

    /// <summary>
    /// Represents a product.
    /// </summary>
    public class ProductModel
    {

        /// <summary>
        /// Gets or sets the category ID of the product.
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
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the product.
        /// </summary>
        public int UserId { get; set; }
    }
}
