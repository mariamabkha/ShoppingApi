namespace ShoppingApi.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } 
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public Categories? Category { get; set; }  
        public UserAccounts? UserAccount { get; set; }

    }
}
