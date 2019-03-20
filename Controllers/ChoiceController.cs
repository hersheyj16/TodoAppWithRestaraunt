using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Controllers
{
    public class ChoiceController : Controller
    {
        private readonly MyDatabaseContext _context;

        public ChoiceController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: Choice
        public async Task<IActionResult> Index()
        {
            var myDatabaseContext = _context.Choice.Include(c => c.Event).Include(c => c.Restaurant);
            return View(await myDatabaseContext.ToListAsync());
        }

        // GET: Choice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choice
                .Include(c => c.Event)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // GET: Choice/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID");
            ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "Name");
            return View();
        }

        // POST: Choice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Suggester,RestaurantID,EventID")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(choice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", choice.EventID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "ID", choice.RestaurantID);
            return View(choice);
        }

        // GET: Choice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choice.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", choice.EventID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "ID", choice.RestaurantID);
            return View(choice);
        }

        // POST: Choice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Suggester,RestaurantID,EventID")] Choice choice)
        {
            if (id != choice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(choice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.ID))
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
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", choice.EventID);
            ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "ID", "ID", choice.RestaurantID);
            return View(choice);
        }

        // GET: Choice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choice
                .Include(c => c.Event)
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (choice == null)
            {
                return NotFound();
            }

            return View(choice);
        }

        // POST: Choice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var choice = await _context.Choice.FindAsync(id);
            _context.Choice.Remove(choice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoiceExists(int id)
        {
            return _context.Choice.Any(e => e.ID == id);
        }
    }
}
