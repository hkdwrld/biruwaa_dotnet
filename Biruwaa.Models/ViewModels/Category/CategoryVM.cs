using System.ComponentModel.DataAnnotations;

namespace Biruwaa.Models.ViewModels.Category
{
    public class CategoryVM
    {
        [Required]
        public string? Name { get; set; }
    }
}
