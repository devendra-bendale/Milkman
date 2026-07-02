using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.Reports
{
    public class CustomerMasterReportViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<MilkDetails> MilkMasterDetails { get; set; }
        public List<MilkDetails> MilkDailyDetails { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MilkDetails
    {
        public int Day { get; set; }
        public decimal Quantity { get; set; }
        public string MilkType { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}
