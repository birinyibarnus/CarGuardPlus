using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarGuardPlus.Areas.Identity.Data;
using CarGuardPlus.BLL;
using CarGuardPlus.Models;

namespace CarGuardPlus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISendAlertService,SendAlertService>();
            builder.Services.AddScoped<IMyAlertService, MyAlertService>();
            builder.Services.AddScoped<IMyLicencesService, MyLicencesService>();
            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddScoped<SendAlertService>();
            builder.Services.AddScoped<SendAlertViewModel>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSignalR();
            #region Authorization
            AddAuthorizationPolicies(builder.Services);
            #endregion

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();

            void AddAuthorizationPolicies(IServiceCollection services)
            {
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("AdminNumber"));
                });
            }
        }
    }
}