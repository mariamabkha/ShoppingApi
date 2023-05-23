namespace ShoppingApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public DateTime Date { get; set; }

        public UserAccounts UserAccount { get; set; }
        public Cart Cart { get; set; }
    }
}
