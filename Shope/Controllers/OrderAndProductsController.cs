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
    public class OrderAndProductsController : Controller
    {
        private readonly ShopeContext _context;

        public OrderAndProductsController(ShopeContext context)
        {
            _context = context;
        }

        // GET: OrderAndProducts
        public async Task<IActionResult> Index()
        {
            var shopeContext = _context.OrderAndProduct.Include(o => o.Order).Include(o => o.Product);
            return View(await shopeContext.ToListAsync());
        }

        // GET: OrderAndProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAndProduct = await _context.OrderAndProduct
                .Include(o => o.Order)
                .Include(o => o.Product)
                .SingleOrDefaultAsync(m => m.OrderAndProductId == id);
            if (orderAndProduct == null)
            {
                return NotFound();
            }

            return View(orderAndProduct);
        }

        // GET: OrderAndProducts/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: OrderAndProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderAndProductId,OrderId,ProductId")] OrderAndProduct orderAndProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderAndProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", orderAndProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderAndProduct.ProductId);
            return View(orderAndProduct);
        }

        // GET: OrderAndProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAndProduct = await _context.OrderAndProduct.SingleOrDefaultAsync(m => m.OrderAndProductId == id);
            if (orderAndProduct == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", orderAndProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderAndProduct.ProductId);
            return View(orderAndProduct);
        }

        // POST: OrderAndProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderAndProductId,OrderId,ProductId")] OrderAndProduct orderAndProduct)
        {
            if (id != orderAndProduct.OrderAndProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderAndProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAndProductExists(orderAndProduct.OrderAndProductId))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", orderAndProduct.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderAndProduct.ProductId);
            return View(orderAndProduct);
        }

        // GET: OrderAndProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAndProduct = await _context.OrderAndProduct
                .Include(o => o.Order)
                .Include(o => o.Product)
                .SingleOrDefaultAsync(m => m.OrderAndProductId == id);
            if (orderAndProduct == null)
            {
                return NotFound();
            }

            return View(orderAndProduct);
        }

        // POST: OrderAndProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderAndProduct = await _context.OrderAndProduct.SingleOrDefaultAsync(m => m.OrderAndProductId == id);
            _context.OrderAndProduct.Remove(orderAndProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAndProductExists(int id)
        {
            return _context.OrderAndProduct.Any(e => e.OrderAndProductId == id);
        }
    }
}
