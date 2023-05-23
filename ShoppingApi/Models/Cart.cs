namespace ShoppingApi.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
        public bool IsFilled { get; set; }  // Add the IsFilled property
        public List<Products> Product { get; set; }  // Add the Products property


        public UserAccounts? UserAccount { get; set; }
        //public Products? Product { get; set; }
        public Order? Order { get; set; }
    }
}
