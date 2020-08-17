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
    public class KupacController : Controller
    {
        private readonly PI01Context _context;

        public KupacController(PI01Context context)
        {
            _context = context;
        }

        public IEnumerable<Usluga> DajMiUslugu()
        {
            return _context.Usluga.ToList();
        }

        // GET: Kupac
        public IActionResult Index(string usluga, string searchIme, string searchPrezime, int pageNumber = 1, int pageSize = 3)
        {
            ViewData["Usluge"] = new SelectList(_context.Usluga, "Naziv", "Naziv");
            ViewBag.CurrentUsluga = usluga;
            ViewBag.CurrentIme = searchIme;
            ViewBag.CurrentPrezime = searchPrezime;

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var kupac = from b in _context.Kupac.Include(m => m.Usluga)
                        select b;
            int kupacCount = kupac.Count();

            if (!string.IsNullOrEmpty(searchIme))
            {
                kupac = kupac.Where(s => s.Ime.Contains(searchIme));
                kupacCount = kupac.Count();
            }

            if (!string.IsNullOrEmpty(searchPrezime))
            {
                kupac = kupac.Where(s => s.Prezime.Contains(searchPrezime));
                kupacCount = kupac.Count();
            }

            if (!string.IsNullOrEmpty(usluga))
            {
                kupac = kupac.Where(x => x.Usluga.Naziv.Contains(usluga));
                kupacCount = kupac.Count();
            }

            kupac = kupac.Skip(ExcludeRecords).Take(pageSize);

            var results = new PagedResult<Kupac>
            {
                Data = kupac.AsNoTracking().ToList(),
                TotalItems = kupacCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            // var pI01Context = _context.Kupac.Include(k => k.Usluga);
            // return View(await pI01Context.ToListAsync());

            return View(results);
        }

        // GET: Kupac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupac = await _context.Kupac
                .Include(k => k.Usluga)
                .FirstOrDefaultAsync(m => m.KupacId == id);
            if (kupac == null)
            {
                return NotFound();
            }

            return View(kupac);
        }

        // GET: Kupac/Create
        public IActionResult Create()
        {
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv");
            return View();
        }

        // POST: Kupac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KupacId,Ime,Prezime,UslugaId")] Kupac kupac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kupac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", kupac.UslugaId);
            return View(kupac);
        }

        // GET: Kupac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupac = await _context.Kupac.FindAsync(id);
            if (kupac == null)
            {
                return NotFound();
            }
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", kupac.UslugaId);
            return View(kupac);
        }

        // POST: Kupac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KupacId,Ime,Prezime,UslugaId")] Kupac kupac)
        {
            if (id != kupac.KupacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kupac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KupacExists(kupac.KupacId))
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
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", kupac.UslugaId);
            return View(kupac);
        }

        // GET: Kupac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupac = await _context.Kupac
                .Include(k => k.Usluga)
                .FirstOrDefaultAsync(m => m.KupacId == id);
            if (kupac == null)
            {
                return NotFound();
            }

            return View(kupac);
        }

        // POST: Kupac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kupac = await _context.Kupac.FindAsync(id);
            _context.Kupac.Remove(kupac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KupacExists(int id)
        {
            return _context.Kupac.Any(e => e.KupacId == id);
        }
    }
}
