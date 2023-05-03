using CustomerWebSite.Data;
using CustomerWebSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //�ާ@SQL Server����Ƥ��e���O
            var NorthwindconnectionString = builder.Configuration.GetConnectionString("Northwind") ?? throw new InvalidOperationException("Connection string 'Northwind' not found.");
            builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(NorthwindconnectionString));

            //�]�w�[�JSeeion
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".CustomersWebSite.Seeion"; //�]�w�W��
                options.IdleTimeout = TimeSpan.FromSeconds(5); //�]�w�O��
                options.Cookie.HttpOnly = true; //�]�w�s�����˴�
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //�]�w�ݸg�LHTTPS�[�K��ĳ�ǿ�(Always)
            });


            
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}