using System.ComponentModel.DataAnnotations;

namespace Milkman2.Features.Customers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(15)]
        public string? ContactNumber { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public int NumberOfOrders { get; set; } = 0;
    }
}
