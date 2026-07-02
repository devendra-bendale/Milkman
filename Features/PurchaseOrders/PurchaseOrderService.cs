using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milkman2.Features.PurchaseOrders
{
    public class PurchaseOrderService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public PurchaseOrderService(IDbContextFactory<DataContext> contextFactory, ICurrentUserService currentUser)
        {
            _contextFactory = contextFactory;
            _currentUser = currentUser;
        }

        public async Task<List<PurchaseOrderViewModel>> GetAllAsync()
        {
            _userId = _currentUser.UserId ?? 0;

            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.MilkType)
                .Where(x => x.IsActive && x.Supplier.UserId == _userId)
                .AsNoTracking()
                .OrderByDescending(x => x.Date)
                .Select(x => new PurchaseOrderViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    SupplierId = x.SupplierId,
                    SupplierName = x.Supplier.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    IsMorningOrder = x.IsMorningOrder,
                    Fats = x.Fats
                })
                .ToListAsync();
        }

        public async Task<PurchaseOrderViewModel?> GetByPOIdAsync(int poId)
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.MilkType)
                .Where(x => x.Id == poId && x.Supplier.UserId == _userId)
                .AsNoTracking()
                .Select(x => new PurchaseOrderViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    SupplierId = x.SupplierId,
                    SupplierName = x.Supplier.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    IsMorningOrder = x.IsMorningOrder,
                    Fats = x.Fats
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<MilkQuantityViewModel>> GetPurchasedQuantity()
        {
            _userId = _currentUser.UserId ?? 0;
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.PurchaseOrders
                .Include(x => x.MilkType)
                .Include(x => x.Supplier)
                .Where(x => x.Date == DateOnly.FromDateTime(DateTime.Today) && x.IsActive && x.Supplier.UserId == _userId)
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

        public async Task AddAsync(PurchaseOrderViewModel model)
        {
            var entity = new PurchaseOrder
            {
                Date = model.Date,
                SupplierId = model.SupplierId,
                MilkTypeId = model.MilkTypeId,
                Quantity = model.Quantity,
                Rate = model.Rate,
                IsActive = true,
                IsMorningOrder = model.IsMorningOrder,
                Fats = model.Fats
            };
            await using var context = await _contextFactory.CreateDbContextAsync();

            context.PurchaseOrders.Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseOrderViewModel model)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var entity = await context.PurchaseOrders
                .Include(x => x.Supplier)
                .Include(x => x.MilkType)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
                return;

            entity.Date = model.Date;
            entity.SupplierId = model.SupplierId;
            entity.MilkTypeId = model.MilkTypeId;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;
            entity.IsMorningOrder = model.IsMorningOrder;
            entity.Fats = model.Fats;

            context.PurchaseOrders.Update(entity);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var entity = await context.PurchaseOrders.FindAsync(id);

            //if (entity != null)
            //{
            //    _context.PurchaseOrders.Remove(entity);

            //    await _context.SaveChangesAsync();
            //}

            if (entity == null)
                return;

            entity.IsActive = false;

            context.PurchaseOrders.Update(entity);

            await context.SaveChangesAsync();
        }
    }
}
