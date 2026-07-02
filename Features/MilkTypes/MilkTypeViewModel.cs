using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.MilkTypes
{
    public class MilkTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type Name is required")]
        [StringLength(50)]
        public string TypeName { get; set; } = string.Empty;

        [Range(1, 9999)]
        public decimal PurchaseRate { get; set; } = 1;

        [Range(1, 9999)]
        public decimal SalesRate { get; set; } = 1;
    }
}
