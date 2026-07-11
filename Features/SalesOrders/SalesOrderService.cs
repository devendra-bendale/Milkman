using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.LogIn;
using Milkman2.Features.SalesOrders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milkman2.Services.SalesOrders
{
    public class SalesOrderService
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public SalesOrderService(DataContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<SalesOrderViewModel>> GetAllAsync()
        {
            _userId = _currentUser.UserId ?? 0;

            return await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.IsActive && x.Customer.UserId == _userId)
                .AsNoTracking()
                .OrderBy(x => x.StartDate)
                .Select(x => new SalesOrderViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Frequency = x.Frequency,
                    Quantity = x.Quantity,
                    Rate = x.Rate
                })
                .ToListAsync();
        }

        public async Task<List<SalesOrderViewModel>> GetAllForDailyOrderAsync()
        {
            _userId = _currentUser.UserId ?? 0;

            return await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.IsActive && x.Customer.UserId == _userId)
                .AsNoTracking()
                .OrderBy(x => x.StartDate)
                .Select(x => new SalesOrderViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Frequency = x.Frequency,
                    Quantity = x.Quantity,
                    Rate = x.Rate
                })
                .ToListAsync();
        }

        public async Task<SalesOrderViewModel?> GetBySOIdAsync(int soId)
        {
            _userId = _currentUser.UserId ?? 0;
            return await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .Where(x => x.Id == soId && x.Customer.UserId == _userId)
                .AsNoTracking()
                .OrderBy(x => x.StartDate)
                .Select(x => new SalesOrderViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    MilkTypeId = x.MilkTypeId,
                    MilkTypeName = x.MilkType.TypeName,
                    Frequency = x.Frequency,
                    Quantity = x.Quantity,
                    Rate = x.Rate
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(SalesOrderViewModel model)
        {
            var entity = new SalesOrder
            {
                StartDate = model.StartDate,
                CustomerId = model.CustomerId,
                MilkTypeId = model.MilkTypeId,
                Frequency = model.Frequency,
                Quantity = model.Quantity,
                Rate = model.Rate,
                IsActive = true
            };

            _context.SalesOrders.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesOrderViewModel model)
        {
            _userId = _currentUser.UserId ?? 0;
            var entity = await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.MilkType)
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.Customer.UserId == _userId);

            if (entity == null)
                return;

            entity.StartDate = model.StartDate;
            entity.CustomerId = model.CustomerId;
            entity.MilkTypeId = model.MilkTypeId;
            entity.Frequency = model.Frequency;
            entity.Quantity = model.Quantity;
            entity.Rate = model.Rate;

            _context.SalesOrders.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SalesOrders.FindAsync(id);

            //if (entity != null)
            //{
            //    _context.SalesOrders.Remove(entity);

            //    await _context.SaveChangesAsync();
            //}

            if (entity == null)
                return;

            entity.IsActive = false;

            _context.SalesOrders.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
