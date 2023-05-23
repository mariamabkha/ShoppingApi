using ShoppingApi.Models;

namespace ShoppingApi.ViewModel
{
    /// <summary>
    /// Represents a transaction.
    /// </summary>
    public class TransactionModel
    {
        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the description of the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the transaction.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
