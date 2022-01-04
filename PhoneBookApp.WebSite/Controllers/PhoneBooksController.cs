using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.ContextEF;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace PhoneBookApp.WebSite.Controllers
{
    public class PhoneBooksController : Controller
    {
        private readonly DataContext _context;

        public PhoneBooksController(DataContext context)
        {
            _context = context;
        }

        // GET: PhoneBooks
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            return View(await _context.PhoneBooks.ToListAsync());
        }


        // GET: PhoneBooks/Details/5
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneBook = await _context.PhoneBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneBook == null)
            {
                return NotFound();
            }

            return View(phoneBook);
        }

        // GET: PhoneBooks/Create
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,MiddleName,PhoneNumber,Address,Description")] PhoneBookApp.WebSite.Models.PhoneBook phoneBook)
        {

            if (ModelState.IsValid)
            {
                var entry = new DataAccess.Models.PhoneBook()
                {
                    Id = phoneBook.Id,
                    FirstName = phoneBook.FirstName,
                    LastName = phoneBook.LastName,
                    MiddleName = phoneBook.MiddleName,
                    PhoneNumber = phoneBook.PhoneNumber,
                    Address = phoneBook.Address,
                    Description = phoneBook.Description
                };
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneBook);
        }

        // GET: PhoneBooks/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneBook = await _context.PhoneBooks.FindAsync(id);
            if (phoneBook == null)
            {
                return NotFound();
            }
            return View(phoneBook);
        }

        // POST: PhoneBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,MiddleName,PhoneNumber,Address,Description")] PhoneBook phoneBook)
        {
            if (id != phoneBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneBookExists(phoneBook.Id))
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
            return View(phoneBook);
        }

        // GET: PhoneBooks/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneBook = await _context.PhoneBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneBook == null)
            {
                return NotFound();
            }

            return View(phoneBook);
        }

        // POST: PhoneBooks/Delete/5
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneBook = await _context.PhoneBooks.FindAsync(id);
            _context.PhoneBooks.Remove(phoneBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneBookExists(int id)
        {
            return _context.PhoneBooks.Any(e => e.Id == id);
        }
    }
}
