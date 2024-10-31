using Hangfire;
using OnlineStore.AppServices.Common.Telegram.Services;
using OnlineStore.ComponentRegistrar;
using OnlineStore.MVC.Filters;
using Serilog;

namespace OnlineStore.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();

            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day));

            OnlineStoreRegistrar.AddComponents(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            OnlineStoreRegistrar.RegisterMiddlewares(app);

            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            using (var scope = app.Services.CreateScope())
            {
                //var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                //recurringJobManager.AddOrUpdate(
                //    "process-orders-job",
                //    () => scope.ServiceProvider.GetRequiredService<IProductService>().GetProductsAsync(),
                //    Cron.Minutely());

                var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramService>();
                await telegramService.SetWebhookAsync();
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
