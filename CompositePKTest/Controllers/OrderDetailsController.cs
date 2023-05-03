using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompositePKTest.Models;

namespace CompositePKTest.Controllers
{
    [Route("/OrderDetails/{action}/{ProductId?}/{OrderId?}")]
    public class OrderDetailsController : Controller
    {
        private readonly NorthwindContext _context;

        public OrderDetailsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
              return _context.OrderDetails != null ? 
                          View(await _context.OrderDetails.ToListAsync()) :
                          Problem("Entity set 'NorthwindContext.OrderDetails'  is null.");
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? OrderId, int? ProductId)
        {
            if (OrderId == null || ProductId == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == OrderId && m.ProductId == ProductId);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? OrderId ,int? ProductId)
        {
            if (OrderId == null || ProductId == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.FindAsync(OrderId , ProductId);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int OrderId,int ProductId, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetails orderDetails)
        {
            if (OrderId != orderDetails.OrderId || ProductId != orderDetails.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.OrderId , orderDetails.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? OrderId , int? ProductId)
        {
            if (OrderId == null || ProductId == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == OrderId && m.ProductId == ProductId);
            if (orderDetails == null )
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int OrderId , int ProductId)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'NorthwindContext.OrderDetails'  is null.");
            }
            var orderDetails = await _context.OrderDetails.FindAsync(OrderId,ProductId);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsExists(int OrderId, int ProductId)
        {
          return (_context.OrderDetails?.Any(e => e.OrderId == OrderId
          && e.ProductId == ProductId)).GetValueOrDefault();
        }
    }
}
