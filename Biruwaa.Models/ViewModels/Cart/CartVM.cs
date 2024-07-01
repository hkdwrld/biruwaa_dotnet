namespace Biruwaa.Models.ViewModels.Cart
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }

    public class CartShowVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
    }
}
