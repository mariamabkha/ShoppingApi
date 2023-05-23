namespace ShoppingApi.ViewModel
{
    public class PaymentModel
    { 
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
