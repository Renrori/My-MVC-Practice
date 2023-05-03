using CustomerOrders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerOrders.Controllers
{
    public class CustomersController : Controller
    {
        NorthwindContext _context = null;
        
        public CustomersController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Customers = new SelectList(
                _context.Customers.Select(c => new {
                    CustomerId=c.CustomerId,
                    CompanyName=c.CompanyName
                }),"CustomerId","CompanyName");
            return View();
        }
        public async Task<IActionResult> Orders(string id)
        {
            Customers c = await _context.Customers.FindAsync(id);
            if(c == null)
            {
                return NotFound();
            }
            else
            {
                return PartialView("_OrderPartial" , c.Orders );
            }
        }
    }
}
