using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupa1Ozo.Models;
using cloudscribe.Pagination.Models;

namespace Grupa1Ozo.Controllers
{
    public class UslugaNarudzbaController : Controller
    {
        private readonly PI01Context _context;

        public UslugaNarudzbaController(PI01Context context)
        {
            _context = context;
        }

        // GET: UslugaNarudzba
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var UNarudzba = from b in _context.UslugaNarudzba.Include(m => m.Narudzba).Include(o =>  o.Usluga)
                           select b;

            int natjecajCount = UNarudzba.Count();

            UNarudzba = UNarudzba.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<UslugaNarudzba>
            {
                Data = UNarudzba.AsNoTracking().ToList(),
                TotalItems = natjecajCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: UslugaNarudzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaNarudzba = await _context.UslugaNarudzba
                .Include(u => u.Narudzba)
                .Include(u => u.Usluga)
                .FirstOrDefaultAsync(m => m.UslugaNarudzbaId == id);
            if (uslugaNarudzba == null)
            {
                return NotFound();
            }

            return View(uslugaNarudzba);
        }

        // GET: UslugaNarudzba/Create
        public IActionResult Create()
        {
            ViewData["NarudzbaId"] = new SelectList(_context.Narudzba, "NarudzbaId", "StatusNarudzbe");
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv");
            return View();
        }

        // POST: UslugaNarudzba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UslugaNarudzbaId,UslugaId,NarudzbaId")] UslugaNarudzba uslugaNarudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uslugaNarudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NarudzbaId"] = new SelectList(_context.Narudzba, "NarudzbaId", "StatusNarudzbe", uslugaNarudzba.NarudzbaId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", uslugaNarudzba.UslugaId);
            return View(uslugaNarudzba);
        }

        // GET: UslugaNarudzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaNarudzba = await _context.UslugaNarudzba.FindAsync(id);
            if (uslugaNarudzba == null)
            {
                return NotFound();
            }
            ViewData["NarudzbaId"] = new SelectList(_context.Narudzba, "NarudzbaId", "StatusNarudzbe", uslugaNarudzba.NarudzbaId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", uslugaNarudzba.UslugaId);
            return View(uslugaNarudzba);
        }

        // POST: UslugaNarudzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UslugaNarudzbaId,UslugaId,NarudzbaId")] UslugaNarudzba uslugaNarudzba)
        {
            if (id != uslugaNarudzba.UslugaNarudzbaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uslugaNarudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaNarudzbaExists(uslugaNarudzba.UslugaNarudzbaId))
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
            ViewData["NarudzbaId"] = new SelectList(_context.Narudzba, "NarudzbaId", "StatusNarudzbe", uslugaNarudzba.NarudzbaId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", uslugaNarudzba.UslugaId);
            return View(uslugaNarudzba);
        }

        // GET: UslugaNarudzba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uslugaNarudzba = await _context.UslugaNarudzba
                .Include(u => u.Narudzba)
                .Include(u => u.Usluga)
                .FirstOrDefaultAsync(m => m.UslugaNarudzbaId == id);
            if (uslugaNarudzba == null)
            {
                return NotFound();
            }

            return View(uslugaNarudzba);
        }

        // POST: UslugaNarudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uslugaNarudzba = await _context.UslugaNarudzba.FindAsync(id);
            _context.UslugaNarudzba.Remove(uslugaNarudzba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaNarudzbaExists(int id)
        {
            return _context.UslugaNarudzba.Any(e => e.UslugaNarudzbaId == id);
        }
    }
}
