namespace ShoppingApi.ViewModel
{

    /// <summary>
    /// Represents a payment.
    /// </summary>
    public class PaymentModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product ID associated with the payment.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the payment.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount of the payment.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the payment.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the payment.
        /// </summary>
        public int UserId { get; set; }
    }
}
