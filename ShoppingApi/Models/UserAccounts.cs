namespace ShoppingApi.Models
{
    public class UserAccounts
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserTypes UserType { get; set; }
        public ICollection<Products> Products { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Deliveries> Deliveries { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
