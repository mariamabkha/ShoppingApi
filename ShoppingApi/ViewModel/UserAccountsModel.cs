namespace ShoppingApi.ViewModel
{

    /// <summary>
    /// Represents a user account.
    /// </summary>
    public class UserAccountsModel
    {
        /// <summary>
        /// Gets or sets the type ID of the user account.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the user.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the contact number of the user.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the username of the user account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user account.
        /// </summary>
        public string Password { get; set; }
    }
}
