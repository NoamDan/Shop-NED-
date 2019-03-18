using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shope.Models;

namespace Shope.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShopeContext _context;

        public OrdersController(ShopeContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var shopeContext = _context.Order.Include(o => o.Customer);
            return View(await shopeContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            return View(order);
        }
        public async Task<IActionResult> MyOrders()
        {
            var query =
            from ord in _context.Order
            join ordandpro in _context.OrderAndProduct on ord.Id equals ordandpro.OrderId
            join pr in _context.Product on ordandpro.ProductId equals pr.Id
            where ord.CustomerId == Global.sessionID
            select pr;

            return View(await query.Distinct().ToListAsync());
        }
        public JsonResult GetUsersPerOrder()
        {
            var userOrders = new List<string>();
            var orders = _context.Order.ToList();
            var _usersOrder = new List<UserOrderReport>();

            foreach (var item in orders)
            {
                if (!userOrders.Contains(item.CustomerId.ToString()))
                {
                    userOrders.Add(item.CustomerId.ToString());
                    _usersOrder.Add(new UserOrderReport
                    {
                        UserName = _context.Customer.First(u => u.Id == item.CustomerId).Fname,
                        Count = orders.Count(x => x.CustomerId == item.CustomerId),
                    });
                }
            }
            return Json(_usersOrder);
        }
        public JsonResult GetProdectsPerOrder()
        {
            var productsPerOrders = new SortedDictionary<int, int>();

            var products = _context.OrderAndProduct;

            foreach (var item in products)
            {
                if (!productsPerOrders.ContainsKey(item.ProductId))
                {
                    productsPerOrders[item.ProductId] = 0;
                }
                productsPerOrders[item.ProductId] += item.Count;
            }

            var productsCount = new List<OrderProdectsCount>();
            foreach (var item in productsPerOrders)
            {
                productsCount.Add(new OrderProdectsCount()
                {
                    ProductName = _context.Product.First(k => k.Id == item.Key).TypeName,
                    Count = item.Value
                });
            }

            return Json(productsCount);
        }
        public ActionResult UserReports()
        {
            return View("UserReports", new List<UserOrderReport>());
        }

        public ActionResult ProductsReports()
        {
            return View("ProductsReports", new List<OrderProdectsCount>());
        }
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
