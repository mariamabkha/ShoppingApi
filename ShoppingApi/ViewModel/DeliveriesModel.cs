namespace ShoppingApi.ViewModel
{
    /// <summary>
    /// Represents a delivery.
    /// </summary>
    public class DeliveriesModel
    {
        /// <summary>
        /// Gets or sets the user ID associated with the delivery.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of the delivery.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
