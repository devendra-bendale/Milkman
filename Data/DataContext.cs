using Microsoft.EntityFrameworkCore;
using Milkman2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Milkman.db");
        //    optionsBuilder.UseSqlite($"Filename = {dbPath}");
        //}

        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<MilkType> MilkTypes => Set<MilkType>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<CustomerDailyEntry> CustomerDailyEntries => Set<CustomerDailyEntry>();
        public DbSet<CustomerAttendance> CustomerAttendances => Set<CustomerAttendance>();
        public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.PurchaseOrders)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(p => p.MilkType)
                .WithMany(m => m.PurchaseOrders)
                .HasForeignKey(p => p.MilkTypeId);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.SalesOrders)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<SalesOrder>()
                .HasOne(s => s.MilkType)
                .WithMany(m => m.SalesOrders)
                .HasForeignKey(s => s.MilkTypeId);

            modelBuilder.Entity<CustomerDailyEntry>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.CustomerDailyEntries)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<CustomerDailyEntry>()
                .HasOne(c => c.MilkType)
                .WithMany(m => m.CustomerDailyEntries)
                .HasForeignKey(c => c.MilkTypeId);

            modelBuilder.Entity<CustomerAttendance>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.CustomerId);
        }
    }
}
