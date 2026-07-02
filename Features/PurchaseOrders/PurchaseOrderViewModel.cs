using System;
using System.ComponentModel.DataAnnotations;

namespace Milkman2.Features.PurchaseOrders
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Range(1, int.MaxValue, ErrorMessage = "Please select Supplier")]
        public int SupplierId { get; set; }

        public string? SupplierName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select Milk Type")]
        public int MilkTypeId { get; set; }

        public string? MilkTypeName { get; set; }

        [Range(0.01, 999999)]
        public decimal Quantity { get; set; }

        [Range(0.01, 999999)]
        public decimal Rate { get; set; }

        [Range(0.01, 999999)]
        public decimal Fats { get; set; }

        public bool IsMorningOrder { get; set; } = true;

        public decimal Amount => Quantity * Rate;
    }

    public class MilkQuantityViewModel
    {
        public int MilkTypeId { get; set; }
        public string? MilkTypeName { get; set; }
        public decimal Quantity { get; set; }
    }
}
