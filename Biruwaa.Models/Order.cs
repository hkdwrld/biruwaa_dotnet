using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biruwaa.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }

        [Required]
        public string? AuthUserId { get; set; }
        [ForeignKey("AuthUserId")]
        [ValidateNever]
        public AuthUser? AuthUser { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string? TrackingNumber { get; set; }

    }
}
