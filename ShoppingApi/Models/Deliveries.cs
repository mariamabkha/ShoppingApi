namespace ShoppingApi.Models
{
    public class Deliveries
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public UserAccounts? UserAccount { get; set; }
    }
}
