using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace CategoryProducts.Controllers
{
    public class BaseController: Controller
    {
        string CookieKey = "CultureInfo";
        public IActionResult SetLanguage(string CultureName)
        {
            SetCookie(CultureName);
            string Referer = Request.Headers["referer"];
            return Redirect(Referer);
        }

        private void SetCookie(string CultureName)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddYears(5);
            options.Secure = true;
            options.HttpOnly = true;
            Response.Cookies.Delete(CookieKey);
            Response.Cookies.Append(CookieKey, CultureName,options);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            //=================
            //讀取喜好語言設定
            string CultureName = "";
            if (Request.Cookies.ContainsKey(CookieKey))
            {
                CultureName = Request.Cookies[CookieKey];
            }
            else
            {
                CultureName = Request.Headers["accept-language"][0].Split(",")[0];
            }
            //=================
            //執行緒注入對應語系
            Thread.CurrentThread.CurrentUICulture =
                Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo(CultureName);

            base.OnActionExecuting(context);
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //string CookieKey = "CultureInfo";
            ////=================
            ////讀取喜好語言設定
            //string CultureName = "";
            //if (Request.Cookies.ContainsKey(CookieKey))
            //{
            //    CultureName = Request.Cookies[CookieKey];
            //}
            //else
            //{
            //    CultureName = Request.Headers["accept-language"][0].Split(",")[0];
            //}
            ////=================
            //Thread.CurrentThread.CurrentUICulture = 
            //    Thread.CurrentThread.CurrentCulture=
            //    new System.Globalization.CultureInfo(CultureName);

            return base.OnActionExecutionAsync(context, next);
        }
    
    }
}
