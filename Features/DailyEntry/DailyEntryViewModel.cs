using Microsoft.EntityFrameworkCore;
using Milkman2.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.DailyEntry
{
    public class DailyEntryViewModel
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Range(1, int.MaxValue, ErrorMessage = "Please select Customer")]
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select Milk type")]
        public int MilkTypeId { get; set; }

        public string? MilkTypeName { get; set; }

        [Range(0.01, 999999)]
        public decimal Quantity { get; set; }

        [Range(0.01, 999999)]
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
    }
}
