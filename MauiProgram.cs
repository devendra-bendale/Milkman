using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Milkman2.Data;
using Milkman2.Features.Customers;
using Milkman2.Features.DailyEntry;
using Milkman2.Features.LogIn;
using Milkman2.Features.MilkTypes;
using Milkman2.Features.PurchaseOrders;
using Milkman2.Features.Reports;
using Milkman2.Features.Suppliers;
using Milkman2.Services.SalesOrders;

namespace Milkman2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContextFactory<DataContext>(options =>
            {
                options.UseNpgsql("User Id=postgres.jmksayjmurtaejyyqjhf;Password=$cjGd*MFj5sQLM@;Server=aws-1-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres");
            });

            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<SupplierService>();
            builder.Services.AddScoped<MilkTypeService>();
            builder.Services.AddScoped<PurchaseOrderService>();
            builder.Services.AddScoped<SalesOrderService>();
            builder.Services.AddScoped<DailyEntryService>();
            builder.Services.AddScoped<ReportService>();
            builder.Services.AddSingleton<LogInService>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddSingleton<AppAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AppAuthStateProvider>());
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            return builder.Build();
        }
    }
}