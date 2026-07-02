using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.LogIn;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milkman2.Features.Suppliers
{
    public class SupplierService
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public SupplierService(
            DataContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<SupplierViewModel>> GetAllAsync()
        {
            _userId = _currentUser.UserId ?? 0;

            return await _context.Suppliers
                .Where(c => c.IsActive && c.UserId == _userId)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new SupplierViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactNumber = x.ContactNumber
                })
                .ToListAsync();
        }

        public async Task AddAsync(SupplierViewModel model)
        {
            _userId = _currentUser.UserId ?? 0;
            var entity = new Supplier
            {
                Name = model.Name.Trim(),
                ContactNumber = model.ContactNumber,
                IsActive = true,
                UserId = _userId
            };

            _context.Suppliers.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SupplierViewModel model)
        {
            var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
                return;

            entity.Name = model.Name.Trim();
            entity.ContactNumber = model.ContactNumber;

            _context.Suppliers.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id);

            //if (entity != null)
            //{
            //    _context.Suppliers.Remove(entity);

            //    await _context.SaveChangesAsync();
            //}

            if (entity == null)
                return;

            entity.IsActive = false;

            _context.Suppliers.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
