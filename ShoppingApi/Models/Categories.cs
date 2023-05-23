namespace ShoppingApi.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
