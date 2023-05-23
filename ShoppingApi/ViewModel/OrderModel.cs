namespace ShoppingApi.ViewModel
{
    /// <summary>
    /// Represents an order.
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Gets or sets the user ID associated with the order.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the cart ID associated with the order.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        public DateTime Date { get; set; }
      
    }
}
