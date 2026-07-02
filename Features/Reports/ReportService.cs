using Milkman2.Features.DailyEntry;
using Milkman2.Features.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.Reports
{
    public class ReportService
    {
        private readonly DailyEntryService _dailyEntryService;
        private readonly PurchaseOrderService _purchaseOrderService;

        public ReportService(DailyEntryService dailyEntryService, PurchaseOrderService purchaseOrderService)
        {
            _dailyEntryService = dailyEntryService;
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task<List<CustomerMasterReportViewModel>> GetCustomerMasterReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var dailyEntries = await _dailyEntryService.GetByDateRangeAsync(startDate, endDate);
            var report = dailyEntries
                .GroupBy(e => new { e.CustomerId, e.CustomerName })
                .Select(g => new CustomerMasterReportViewModel
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.CustomerName,
                    MilkDailyDetails = g.Select(e => new MilkDetails
                    {
                        Day = e.Date.Day,
                        Quantity = e.Quantity,
                        MilkType = e.MilkTypeName,
                        Amount = e.IsActive ? e.Quantity * e.Rate : 0,
                        IsActive = e.IsActive
                    })
                    .OrderBy(md => md.Day)
                    .ToList(),
                    MilkMasterDetails = g.GroupBy(e => e.MilkTypeName)
                        .Select(mg => new MilkDetails
                        {
                            Day = 0, // Not applicable for master details
                            Quantity = mg.Sum(e => e.IsActive ? e.Quantity : 0),
                            MilkType = mg.Key,
                            Amount = mg.Sum(e => e.IsActive ? e.Quantity * e.Rate : 0)
                        }).ToList(),
                    TotalAmount = g.Sum(e => e.IsActive ? e.Quantity * e.Rate : 0)
                })
                .ToList();
            return report;
        }

        public async Task<List<SupplierMasterReportViewModel>> GetSupplierMasterReportAsync(DateOnly startDate, DateOnly endDate)
        {
            var purchaseOrders = await _purchaseOrderService.GetAllAsync();
            var report = purchaseOrders
                .Where(po => po.Date >= startDate && po.Date <= endDate)
                .GroupBy(po => new { po.SupplierId, po.SupplierName })
                .Select(g => new SupplierMasterReportViewModel
                {
                    SupplierName = g.Key.SupplierName,
                    TotalAmount = g.Sum(po => po.Amount),
                    TotalFats = g.Sum(po => po.Fats),
                    TotalQuantity = g.Sum(po => po.Quantity),
                    SupplierOrderDetails = g.Select(po => new SupplierOrderViewModel
                    {
                        Date = po.Date,
                        MilkTypeName = po.MilkTypeName,
                        Quantity = po.Quantity,
                        Rate = po.Rate,
                        Fats = po.Fats,
                        IsMorningOrder = po.IsMorningOrder
                    }).OrderBy(po => po.Date).ThenBy(po => po.MilkTypeName).ToList()
                }).ToList();
            return report;
        }
    }
}
