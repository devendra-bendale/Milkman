using System.ComponentModel.DataAnnotations;

namespace Milkman2.Features.Suppliers
{
    public class SupplierViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(15)]
        public string? ContactNumber { get; set; }
    }
}
