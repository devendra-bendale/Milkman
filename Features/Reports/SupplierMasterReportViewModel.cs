using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.Reports
{
    public class SupplierMasterReportViewModel
    {
        public string SupplierName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalFats { get; set; }
        public decimal TotalQuantity { get; set; }
        public List<SupplierOrderViewModel> SupplierOrderDetails { get; set; } = new();
    }

    public class  SupplierOrderViewModel
    {
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public string? MilkTypeName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Fats { get; set; }
        public bool IsMorningOrder { get; set; } = true;
        public decimal Amount => Quantity * Rate;
    }
}
