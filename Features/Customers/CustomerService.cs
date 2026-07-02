using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.LogIn;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milkman2.Features.Customers
{
    public class CustomerService
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public CustomerService(
        DataContext context,
        ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<CustomerViewModel>> GetAllAsync()
        {
            _userId = _currentUser.UserId ?? 0; 
            return await _context.Customers
                .Where(c=>c.IsActive && c.UserId == _userId)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new CustomerViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    NumberOfOrders = x.SalesOrders.Count()
                })
                .ToListAsync();
        }

        public async Task AddAsync(CustomerViewModel model)
        {
            _userId = _currentUser.UserId ?? 0;

            var entity = new Customer
            {
                Name = model.Name.Trim(),
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                IsActive = true,
                UserId = _userId,
            };

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerViewModel model)
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
                return;

            entity.Name = model.Name.Trim();
            entity.ContactNumber = model.ContactNumber;
            entity.Address = model.Address;

            _context.Customers.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);

            //if (entity != null)
            //{
            //    _context.Customers.Remove(entity);

            //    await _context.SaveChangesAsync();
            //}

            if (entity == null)
                return;

            entity.IsActive = false;

            _context.Customers.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
