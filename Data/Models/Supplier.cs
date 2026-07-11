using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Data.Models
{
    [Table("UserAccount", Schema = "milkman")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string UserPassword { get; set; } = string.Empty;

        //If the user is allowed to take pre-orders for customers, then this property will be true. Otherwise, it will be false.
        //if its true then for evening orders next day daily entry will be created for the customer. If its false then for evening orders next day daily entry will not be created for the customer.
        public bool IsPreOrderApplicable { get; set; } = false;

        public bool IsPasswordActivated { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public virtual ICollection<Supplier> Suppliers { get; set; }
            = new List<Supplier>();

        public virtual ICollection<Customer> Customers { get; set; }
            = new List<Customer>();
    }

    [Table("Supplier", Schema = "milkman")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        [MaxLength(15)]
        public string? ContactNumber { get; set; }

        [ForeignKey(nameof(UserDetail))]
        public int UserId { get; set; }

        public virtual UserAccount UserDetail { get; set; } = null!;

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();
    }

    [Table("MilkType", Schema = "milkman")]
    public class MilkType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal SalesRate { get; set; }

        [Precision(18, 2)]
        public decimal PurchaseRate { get; set; }

        [ForeignKey(nameof(UserDetail))]
        public int UserId { get; set; }

        public virtual UserAccount UserDetail { get; set; } = null!;

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
            = new List<SalesOrder>();

        public virtual ICollection<CustomerDailyEntry> CustomerDailyEntries { get; set; }
            = new List<CustomerDailyEntry>();
    }

    [Table("PurchaseOrder", Schema = "milkman")]
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        [ForeignKey(nameof(Supplier))]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;

        [ForeignKey(nameof(MilkType))]
        public int MilkTypeId { get; set; }

        public virtual MilkType MilkType { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Quantity { get; set; }

        [Precision(18, 2)]
        public decimal Rate { get; set; }

        [Precision(18, 2)]
        public decimal Fats { get; set; }

        public bool IsMorningOrder { get; set; }

        [NotMapped]
        public decimal Amount => Quantity * Rate;

        public bool IsActive { get; set; } = true;
    }

    [Table("Customer", Schema = "milkman")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? ContactNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(UserDetail))]
        public int UserId { get; set; }
        public virtual UserAccount UserDetail { get; set; } = null!;

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
            = new List<SalesOrder>();

        public virtual ICollection<CustomerDailyEntry> CustomerDailyEntries { get; set; }
            = new List<CustomerDailyEntry>();

        public virtual ICollection<CustomerAttendance> Attendances { get; set; }
            = new List<CustomerAttendance>();
    }

    public enum DeliveryFrequency
    {
        Daily = 1
        //AlternateDay = 2,
        //Weekly = 3
    }

    [Table("SalesOrder", Schema = "milkman")]
    public class SalesOrder
    {
        [Key]
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(MilkType))]
        public int MilkTypeId { get; set; }

        public virtual MilkType MilkType { get; set; } = null!;

        public DeliveryFrequency Frequency { get; set; }

        [Precision(18, 2)]
        public decimal Quantity { get; set; }

        [Precision(18, 2)]
        public decimal Rate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    [Table("CustomerDailyEntry", Schema = "milkman")]
    public class CustomerDailyEntry
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey(nameof(MilkType))]
        public int MilkTypeId { get; set; }

        public virtual MilkType MilkType { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Quantity { get; set; }

        [Precision(18, 2)]
        public decimal Rate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    [Table("CustomerAttendance", Schema = "milkman")]
    public class CustomerAttendance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public DateOnly AbsentStartDate { get; set; }

        public DateOnly AbsentEndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
