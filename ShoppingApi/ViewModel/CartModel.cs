namespace ShoppingApi.ViewModel
{

    /// <summary>
    /// Represents a cart.
    /// </summary>
    public class CartModel
    {
       /* /// <summary>
        /// Gets or sets the cart ID.
        /// </summary>
        public int Id { get; set; }*/

        /// <summary>
        /// Gets or sets the product ID associated with the cart.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the cart.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the count of the product in the cart.
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the cart is filled or not.
        /// </summary>
        public bool IsFilled { get; set; }
    }
}
