namespace ShoppingApi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public Products Product { get; set; }
        public UserAccounts UserAccount { get; set; }
    }
}
