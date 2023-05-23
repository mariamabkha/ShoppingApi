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
        public int ContactNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypes UserTypes { get; set; }
        public IList<Products> Products { get; set; }
        public IList<Payment> Payments { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Deliveries> Deliveries { get; set; }
    }
}
