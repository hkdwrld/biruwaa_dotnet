namespace Biruwaa.Models.ViewModels.Order
{
    public class CreateOrderVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
