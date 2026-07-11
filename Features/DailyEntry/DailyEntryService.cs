using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.Customers;
using Milkman2.Features.LogIn;
using Milkman2.Features.PurchaseOrders;
using Milkman2.Features.SalesOrders;
using Milkman2.Services.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.DailyEntry
{
    public class DailyEntryService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly SalesOrderService _salesOrderService;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public DailyEntryService(IDbContextFactory<DataContext> contextFactory, SalesOrderService salesOrderService, ICurrentUserService currentUser)
        {
            _contextFactory = contextFactory;
            _salesOrderService = salesOrderService;
            _currentUser = currentUser;
        }

        public async Task<List<DailyEntryViewModel>> GetAllAsync(bool isAll = false)
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();

            if (isAll)
            {
                return await context.CustomerDailyEntries
                    .Include(x => x.Customer)
                    .Include(x => x.MilkType)
                    .Where(c => c.IsActive && c.Customer.UserId == _userId)
                    .OrderByDescending(x => x.Date)
                    .ThenBy(x=>x.Customer.Name)
                    .AsNoTracking()
                    .Select(x => new DailyEntryViewModel
                    {
                        Id = x.Id,
                        Date = x.Date,
                        CustomerId = x.CustomerId,
                        CustomerName = x.Customer.Name,
                        MilkTypeId = x.MilkTypeId,
                        MilkTypeName = x.MilkType.TypeName,
                        Quantity = x.Quantity,
                        Rate = x.Rate
                    }).ToListAsync();
            }
            else {
                return await context.CustomerDailyEntries
                    .Include(x => x.Customer)
                    .Include(x => x.MilkType)
                    .Where(x => x.Date == DateOnly.FromDateTime(DateTime.Today) && x.IsActive && x.Customer.UserId == _userId)
                    .OrderByDescending(x => x.Date)
                    .ThenBy(x => x.Customer.Name)
                    .AsNoTracking()
                    .Select(x => new DailyEntryViewModel
                    {
                        Id = x.Id,
                        Date = x.Date,
                        CustomerId = x.CustomerId,
                        CustomerName = x.Customer.Name,
                        MilkTypeId = x.MilkTypeId,
                        MilkTypeName = x.MilkType.TypeName,
                        Quantity = x.Quantity,
                        Rate = x.Rate
                    }).ToListAsync();
            }
        }

        public async Task<DailyEntryViewModel?> GetByIdAsync(int id)
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.CustomerDailyEntries
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.Id == id && x.Customer.UserId == _userId)
                .AsNoTracking()
                .Select(x => new DailyEntryViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Quantity = x.Quantity,
                    Rate = x.Rate
                })
                .FirstOrDefaultAsync();            
        }

        public async Task<List<DailyEntryViewModel>?> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.CustomerDailyEntries
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.Date >= startDate && x.Date <= endDate && x.Customer.UserId == _userId)
                .AsNoTracking()
                .Select(x => new DailyEntryViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<List<MilkQuantityViewModel>> GetSoldQuantity()
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.CustomerDailyEntries
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.Date == DateOnly.FromDateTime(DateTime.Today) && x.IsActive && x.Customer.UserId == _userId)
                .AsNoTracking()
                .GroupBy(x => new { x.MilkTypeId, MilkTypeName = x.MilkType != null ? x.MilkType.TypeName : string.Empty })
                .Select(g => new MilkQuantityViewModel
                {
                    MilkTypeId = g.Key.MilkTypeId,
                    MilkTypeName = g.Key.MilkTypeName,
                    Quantity = g.Sum(p => p.Quantity)
                })
                .ToListAsync();
        }

        public async Task AddAsync(DailyEntryViewModel model)
        {
            var entity = new CustomerDailyEntry
            {
                Date = model.Date,
                CustomerId = model.CustomerId,
                MilkTypeId = model.MilkTypeId,
                Quantity = model.Quantity,
                Rate = model.Rate,
                IsActive = true
            };
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.CustomerDailyEntries.Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task AddDailyEntriesForAllCustomers(bool isMorningOrder = false, bool isPreOrderApplicable = false)
        {
            var date = DateOnly.FromDateTime(DateTime.Today);

            if (!isMorningOrder && isPreOrderApplicable)
                date = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

            //check if daily entry is already added for date
            await using var contextCheck = await _contextFactory.CreateDbContextAsync();

            var existingEntries = await contextCheck.CustomerDailyEntries
                .Include(x => x.Customer)
                .Where(x => x.Date == date && x.Customer.UserId == _userId)
                .ToListAsync();

            if (existingEntries.Any())
                return;

            List<CustomerDailyEntry> customerDailyEntries = new List<CustomerDailyEntry>();

            var salesOrders = await _salesOrderService.GetAllAsync();
            salesOrders = salesOrders.Where(s => s.StartDate <= DateOnly.FromDateTime(DateTime.Today)).ToList();

            foreach (var salesOrder in salesOrders)
            {
                customerDailyEntries.Add(new CustomerDailyEntry()
                {
                    Date = date,
                    CustomerId = salesOrder.CustomerId,
                    MilkTypeId = salesOrder.MilkTypeId,
                    Quantity = salesOrder.Quantity,
                    Rate = salesOrder.Rate,
                    IsActive = true
                });
            }
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.CustomerDailyEntries.AddRange(customerDailyEntries);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DailyEntryViewModel model)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var entity = await context.CustomerDailyEntries
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
                return;

            entity.Date = model.Date;
            entity.CustomerId = model.CustomerId;
            entity.MilkTypeId = model.MilkTypeId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;

            context.CustomerDailyEntries.Update(entity);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var entity = await context.CustomerDailyEntries.FindAsync(id);

            //if (entity != null)
            //{
            //    _context.CustomerDailyEntries.Remove(entity);

            //    await _context.SaveChangesAsync();
            //}

            if (entity == null)
                return;

            entity.IsActive = false;

            context.CustomerDailyEntries.Update(entity);

            await context.SaveChangesAsync();
        }
    }
}
