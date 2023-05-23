namespace ShoppingApi.ViewModel
{
    public class OrderModel
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the cart.
        /// </summary>
        public int CartId { get; set; }
        /// <summary>
        /// Gets or sets the date of the order.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
