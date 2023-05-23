namespace ShoppingApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public UserAccounts UserAccount { get; set; }
    }
}
