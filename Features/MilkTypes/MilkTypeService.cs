using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.LogIn;
using Milkman2.Features.MilkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Features.MilkTypes
{
    public class MilkTypeService
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUser;
        private int _userId;

        public MilkTypeService(
            DataContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;            
        }

        public async Task<List<MilkTypeViewModel>> GetAllAsync()
        {
            _userId = _currentUser.UserId ?? 0;

            return await _context.MilkTypes
                .Where(c => c.UserId == _userId)
                .AsNoTracking()
                .OrderBy(x => x.TypeName)
                .Select(x => new MilkTypeViewModel
                {
                    Id = x.Id,
                    TypeName = x.TypeName,
                    PurchaseRate = x.PurchaseRate,
                    SalesRate = x.SalesRate
                })
                .ToListAsync();
        }

        public async Task AddAsync(MilkTypeViewModel model)
        {
            _userId = _currentUser.UserId ?? 0;

            var entity = new MilkType
            {
                TypeName = model.TypeName.Trim(),
                PurchaseRate = model.PurchaseRate,
                SalesRate = model.SalesRate,
                UserId = _userId
            };

            _context.MilkTypes.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MilkTypeViewModel model)
        {
            var entity = await _context.MilkTypes.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity == null)
                return;

            entity.TypeName = model.TypeName.Trim();
            entity.PurchaseRate = model.PurchaseRate;
            entity.SalesRate = model.SalesRate;

            _context.MilkTypes.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MilkTypes.FindAsync(id);

            if (entity != null)
            {
                _context.MilkTypes.Remove(entity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
