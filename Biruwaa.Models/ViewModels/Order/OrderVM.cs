namespace Biruwaa.Models.ViewModels.Order
{
    public class OrderVM
    {
        public int ProductId { get; set; }
        public string? AuthUserId { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
