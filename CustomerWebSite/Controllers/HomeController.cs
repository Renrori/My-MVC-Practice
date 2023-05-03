using CustomerWebSite.Models;
using CustomerWebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CustomerWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        NorthwindContext _context;
        IMemoryCache _cache;
        public HomeController(ILogger<HomeController> logger, NorthwindContext context,IMemoryCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public IActionResult Index()
        {
            ViewBag.小瑠理 = "Ruriri";
            ViewBag.瑠理 = "Ruri";
            ViewBag.Customers = new SelectList(_context.Customers.Select(c => c.CompanyName));
            ViewBag.Customer = _context.Customers.Find("WELLI");
            Customer c = _context.Customers.First();
            ViewBag.JavaScript = $"alert('Hello:{c.ContactName}')";

            //建立Session變數設定
            HttpContext.Session.SetString("SessionKey", "SeesionData");

            //建立Cache響應層級和逾時設定
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.SetSlidingExpiration(TimeSpan.FromDays(1));
            cacheOptions.SetPriority(CacheItemPriority.High);
            _cache.Set("CacheKey", "CacheData", cacheOptions);

            CookieOptions co =new CookieOptions();
            co.Expires = DateTime.Now.AddYears(5);
            co.HttpOnly = true;
            co.Secure = true;
            Response.Cookies.Append("CookieKey1", "CookieData1", co);
            Response.Cookies.Append("CookieKey2", "CookieData2", co);

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Contact(string Name , string Email ,  string Phone)
        //{
        //    return RedirectToAction("Index");
        //}

        public IActionResult Contact([Bind("Name, Email, Phone")] ContactViewModel cvm)
        {
            if (ModelState.IsValid) //驗證成功執行方法
            {
                TempData.Add("THM101" , "哈哈嘿嘿哈哈");
                return RedirectToAction("Index");
            }
            return View(cvm);
        }

        public IActionResult Privacy()
        {
            //檢查Session驗證
            if (HttpContext.Session.Keys.Contains("SessionKey"))
            {
                string SessionData = HttpContext.Session.GetString("SessionKey");
            }

            //檢查Cache驗證
            object CacheData;
            if (_cache.TryGetValue("CacheKey",out CacheData)){
                string CData=Convert.ToString(CacheData);
            }
            string CookieData =Request.Cookies["CookieKey2"];
            if (CookieData != null)
            {

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}