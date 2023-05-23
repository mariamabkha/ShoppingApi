namespace ShoppingApi.ViewModel
{
    public class CartModel
    {
        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the count of the items in the cart.
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// is cart filled or not
        /// </summary>
        public bool isFilled { get; set; }
    }
}
