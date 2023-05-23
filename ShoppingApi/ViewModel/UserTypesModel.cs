namespace ShoppingApi.ViewModel
{
    /// <summary>
    /// Represents a user type.
    /// </summary>
    public class UserTypesModel
    {
        /// <summary>
        /// Gets or sets the id of user type.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the user type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the description of the user type.
        /// </summary>
        public string Description { get; set; }

    }
}
