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
    public class SkladisteController : Controller
    {
        private readonly PI01Context _context;

        public SkladisteController(PI01Context context)
        {
            _context = context;
        }

        // GET: Skladiste
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var skladista = from b in _context.Skladiste.Include(m => m.Lokacija)
                            select b;

            int skladistaCount = skladista.Count();

            skladista = skladista
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Skladiste>
            {
                Data = skladista.AsNoTracking().ToList(),
                TotalItems = skladistaCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Skladiste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladiste = await _context.Skladiste
                .Include(s => s.Lokacija)
                .FirstOrDefaultAsync(m => m.SkladisteId == id);
            if (skladiste == null)
            {
                return NotFound();
            }

            return View(skladiste);
        }

        // GET: Skladiste/Create
        public IActionResult Create()
        {
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "LokacijaId", "Adresa");
            return View();
        }

        // POST: Skladiste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkladisteId,Naziv,LokacijaId")] Skladiste skladiste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skladiste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "LokacijaId", "Adresa", skladiste.LokacijaId);
            return View(skladiste);
        }

        // GET: Skladiste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladiste = await _context.Skladiste.FindAsync(id);
            if (skladiste == null)
            {
                return NotFound();
            }
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "LokacijaId", "Adresa", skladiste.LokacijaId);
            return View(skladiste);
        }

        // POST: Skladiste/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkladisteId,Naziv,LokacijaId")] Skladiste skladiste)
        {
            if (id != skladiste.SkladisteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skladiste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkladisteExists(skladiste.SkladisteId))
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
            ViewData["LokacijaId"] = new SelectList(_context.Lokacija, "LokacijaId", "Adresa", skladiste.LokacijaId);
            return View(skladiste);
        }

        // GET: Skladiste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladiste = await _context.Skladiste
                .Include(s => s.Lokacija)
                .FirstOrDefaultAsync(m => m.SkladisteId == id);
            if (skladiste == null)
            {
                return NotFound();
            }

            return View(skladiste);
        }

        // POST: Skladiste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skladiste = await _context.Skladiste.FindAsync(id);
            _context.Skladiste.Remove(skladiste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkladisteExists(int id)
        {
            return _context.Skladiste.Any(e => e.SkladisteId == id);
        }
    }
}
