using CategoryProducts.Data;
using CategoryProducts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CategoryProducts
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

            var NorthwindconnectionString = builder.Configuration.GetConnectionString("Northwind") ?? throw new InvalidOperationException("Connection string 'Northwind' not found.");
            builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(NorthwindconnectionString));



            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //���UView�MData�ԭz([�ݩ�])���h�y�t
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(option=>
                option.ResourcesPath= "LangResource");
            builder.Services.AddMvcCore()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

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
            //�h�y�t�P�_�M�ǤJ
            var SupportedCulture = new[] { "en-US" ,"zh-TW" ,"ja"};
            var Options = new RequestLocalizationOptions()
                .SetDefaultCulture(SupportedCulture[1])
                .AddSupportedCultures(SupportedCulture)  //�]�m�t�ε{���t�� (�ɶ��f����)
                .AddSupportedUICultures(SupportedCulture); //�]�mUI����
            //�]�m�޽u 
            app.UseRequestLocalization(Options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}