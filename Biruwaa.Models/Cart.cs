using System.ComponentModel.DataAnnotations.Schema;

namespace Biruwaa.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string? AuthUserId { get; set; }
        [ForeignKey("AuthUserId")]
        public AuthUser? ApplicationUser { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int Count { get; set; }
    }
}
