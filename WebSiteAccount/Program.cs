using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSiteAccount.Data;

namespace WebSiteAccount
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

            //builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            builder.Services.AddControllersWithViews();
            //--------------------設定密碼原則-----------------
            builder.Services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;  //必須阿拉伯數字
                options.Password.RequireLowercase = true;  //必須有小寫英文字
                options.Password.RequireNonAlphanumeric = true;  //必須要有非數字
                options.Password.RequireUppercase = true;  //必須要有大寫
                options.Password.RequiredLength = 8;  // 最少八個字元
                options.Password.RequiredUniqueChars = 1;  //不可重複

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  //鎖定時間
                options.Lockout.MaxFailedAccessAttempts = 3;  //嘗試次數
                options.Lockout.AllowedForNewUsers = true; 

                //可允許字元
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; //電子郵件不可重複
            });
            builder.Services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;  //
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //安全連線
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);  //Cookie儲存時間
                options.LoginPath = "/Identity/Account/Login";  //登入時的畫面路徑
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";  
                options.SlidingExpiration = true;
            });

            //--------------------------------
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}