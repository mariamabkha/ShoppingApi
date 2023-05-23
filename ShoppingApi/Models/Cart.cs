namespace ShoppingApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
        public bool IsFilled { get; set; }

        public Products Product { get; set; }
        public UserAccounts UserAccount { get; set; }
        public Order Order { get; set; }
    }
}
