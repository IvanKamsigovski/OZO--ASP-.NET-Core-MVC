using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupa1Ozo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using cloudscribe.Pagination.Models;

namespace Grupa1Ozo.Controllers
{
    public class NatjecajController : Controller
    {
        private readonly PI01Context _context;

        public NatjecajController(PI01Context context)
        {
            _context = context;
        }

        // GET: Natjecaj
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var natjecaj = from b in _context.Natjecaj.Include(m => m.JavniNatjecaj)
                           select b;

            int natjecajCount = natjecaj.Count();

            natjecaj = natjecaj.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<Natjecaj>
            {
                Data = natjecaj.AsNoTracking().ToList(),
                TotalItems = natjecajCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Natjecaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecaj = await _context.Natjecaj
                .Include(n => n.JavniNatjecaj)
                .FirstOrDefaultAsync(m => m.NatjecajId == id);
            if (natjecaj == null)
            {
                return NotFound();
            }

            return View(natjecaj);
        }

        // GET: Natjecaj/Create
        public IActionResult Create()
        {
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik");
            return View();
        }

        // POST: Natjecaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NatjecajId,Opis,JavniNatjecajId")] Natjecaj natjecaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(natjecaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", natjecaj.JavniNatjecajId);
            return View(natjecaj);
        }

        // GET: Natjecaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecaj = await _context.Natjecaj.FindAsync(id);
            if (natjecaj == null)
            {
                return NotFound();
            }
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", natjecaj.JavniNatjecajId);
            return View(natjecaj);
        }

        // POST: Natjecaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NatjecajId,Opis,JavniNatjecajId")] Natjecaj natjecaj)
        {
            if (id != natjecaj.NatjecajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(natjecaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NatjecajExists(natjecaj.NatjecajId))
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
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", natjecaj.JavniNatjecajId);
            return View(natjecaj);
        }

        // GET: Natjecaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natjecaj = await _context.Natjecaj
                .Include(n => n.JavniNatjecaj)
                .FirstOrDefaultAsync(m => m.NatjecajId == id);
            if (natjecaj == null)
            {
                return NotFound();
            }

            return View(natjecaj);
        }

        // POST: Natjecaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var natjecaj = await _context.Natjecaj.FindAsync(id);
            _context.Natjecaj.Remove(natjecaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NatjecajExists(int id)
        {
            return _context.Natjecaj.Any(e => e.NatjecajId == id);
        }
    }
}
