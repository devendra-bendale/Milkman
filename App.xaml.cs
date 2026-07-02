using Microsoft.EntityFrameworkCore;
using Milkman2.Data;
using Milkman2.Data.Models;
using Milkman2.Features.DailyEntry;
using Milkman2.Features.LogIn;
using Milkman2.Features.LogIn.Components;

namespace Milkman2
{
    public partial class App : Application
    {
        private readonly DataContext _context;
        private readonly DailyEntryService _dailyEntryService;

        public App(DataContext dbContext, DailyEntryService dailyEntryService)
        {
            _context = dbContext;
            _dailyEntryService = dailyEntryService;
            InitializeComponent();
            dbContext.Database.Migrate();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            SeedData().GetAwaiter();
            return new Window(new MainPage()) { Title = "Milkman2" };
        }
        private async Task SeedData()
        {
            if (_context.CustomerDailyEntries.Where(x=>x.Date == DateOnly.FromDateTime(DateTime.Today)).Any())
                return;

            await _dailyEntryService.AddDailyEntriesForAllCustomers();

            //var milkType = new MilkType
            //{
            //    TypeName = "Cow Milk",
            //    SalesRate = 60,
            //    PurchaseRate = 50
            //};
            //_context.MilkTypes.Add(milkType);
            //_context.SaveChanges();
        }
    }
}
