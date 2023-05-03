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
            //--------------------�]�w�K�X��h-----------------
            builder.Services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = true;  //�������ԧB�Ʀr
                options.Password.RequireLowercase = true;  //�������p�g�^��r
                options.Password.RequireNonAlphanumeric = true;  //�����n���D�Ʀr
                options.Password.RequireUppercase = true;  //�����n���j�g
                options.Password.RequiredLength = 8;  // �̤֤K�Ӧr��
                options.Password.RequiredUniqueChars = 1;  //���i����

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  //��w�ɶ�
                options.Lockout.MaxFailedAccessAttempts = 3;  //���զ���
                options.Lockout.AllowedForNewUsers = true; 

                //�i���\�r��
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; //�q�l�l�󤣥i����
            });
            builder.Services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;  //
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //�w���s�u
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);  //Cookie�x�s�ɶ�
                options.LoginPath = "/Identity/Account/Login";  //�n�J�ɪ��e�����|
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