using ShoppingApi.Models;

namespace ShoppingApi.ViewModel
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
