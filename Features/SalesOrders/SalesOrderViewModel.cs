using Milkman2.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Milkman2.Features.SalesOrders
{
    public class SalesOrderViewModel
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Range(1, int.MaxValue, ErrorMessage = "Please select Customer")]
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select Milk type")]
        public int MilkTypeId { get; set; }

        public string? MilkTypeName { get; set; }

        public DeliveryFrequency Frequency { get; set; } = DeliveryFrequency.Daily;

        [Range(0.01, 999999)]
        public decimal Quantity { get; set; }

        [Range(0.01, 999999)]
        public decimal Rate { get; set; }
    }
}
