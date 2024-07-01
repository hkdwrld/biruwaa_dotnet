namespace Biruwaa.Models.ViewModels.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double Price { get; set; }
        //public Category Category { get; set; }
        public string? Category { get; set; }
    }
}
