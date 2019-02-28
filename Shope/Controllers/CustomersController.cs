using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;
using Shope.Models;

namespace Shope.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ShopeContext _context;

        public CustomersController(ShopeContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string cus , string fname)
        {
            var filter = from m in _context.Customer select m;
            if (!string.IsNullOrEmpty(cus))
            {
                filter = filter.Where(s => s.City.Contains(cus));
            }
            if (!string.IsNullOrEmpty(fname))
            {
                filter = filter.Where(t => t.Fname.Contains(fname));
            }
            return View(await filter.ToListAsync());
            // return View(await _context.Mesima1.ToListAsync());
        }

        public IActionResult Login()
        {
            Global.Admin = 0;
            return View();

        }

        [HttpPost]
        public IActionResult Login(string uname, string psw)
        {

            var check = from cus in _context.Customer
                        where cus.Email == uname
                        select cus.Password;

            if (check.Contains(psw))
            {
                var admin = from cus in _context.Customer
                            where cus.Email == uname
                            select cus.IsAdmin;
                if (admin.Contains(1))
                {
                    Global.Admin = 2;
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    Global.Admin = 1;
                    return RedirectToAction("Index", "Products");


                }
            }
            else
            { 


                
                //Global.Admin = 0;
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lnam,City,Street,NumberHome,Email,Password,IsAdmin")] Customer customer)
        //public async Task<IActionResult> Register([Bind("email,psw,fname,lname,city,street,number")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                //// check if email adress invalid
                //bool invalid = IsValidEmail(customer.Email);

                //if (!invalid)
                //{
                //    return View(customer);
                //}

                // check if email adress exists
                var Exists = from cus in _context.Customer
                             where cus.Email == customer.Email
                             select cus.Id;
                bool IsNotEmpty = Exists.Any();
                if (IsNotEmpty)
                {
                    return View();
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                Global.Admin = 1;
                return RedirectToAction("Index", "Products");
                /*eturn RedirectToAction(nameof(Index));*/
            }
            return View(customer);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lnam,City,Street,NumberHome,Email,Password,IsAdmin")] Customer customer)
        {
            
            if (ModelState.IsValid)
            {
                // check if email adress invalid
                bool invalid = IsValidEmail(customer.Email);

                if (!invalid)
                {
                    return View(customer);
                }

                // check if email adress exists
                var Exists = from cus in _context.Customer
                         where cus.Email == customer.Email
                         select cus.Id;
                bool IsNotEmpty = Exists.Any();
                if (IsNotEmpty)
                {
                    return View(customer);
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // check if email adress invalid
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lnam,City,Street,NumberHome,Email,Password,IsAdmin")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
