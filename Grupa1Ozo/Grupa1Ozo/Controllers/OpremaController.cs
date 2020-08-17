using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupa1Ozo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using cloudscribe.Pagination.Models;

namespace Grupa1Ozo.Controllers
{
    public class OpremaController : Controller
    {
        private readonly PI01Context _context;

        public OpremaController(PI01Context context)
        {
            _context = context;
        }

        public IEnumerable<Skladiste> DajMiSkladista()
        {
            return _context.Skladiste.ToList();
        }

        public IEnumerable<Usluga> DajMiUslugu()
        {
            return _context.Usluga.ToList();
        }
        //Index
        public IActionResult Index(string skladiste, string usluga, string searchString, int pageNumber = 1, int pageSize = 3)
        {
            ViewData["Skladista"] = new SelectList(_context.Skladiste, "Naziv", "Naziv");
            ViewData["Usluge"] = new SelectList(_context.Usluga, "Naziv", "Naziv");
            ViewBag.CurrentSkladiste = skladiste;
            ViewBag.CurrentUsluga = usluga;
            ViewBag.CurrentSearchString = searchString;

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var oprema = from b in _context.Oprema.Include(m => m.Skladiste).Include(o => o.Usluga)
                         select b;

            int opremaCount = oprema.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                oprema = oprema.Where(s => s.NazivOpreme.Contains(searchString));
                opremaCount = oprema.Count();
            }

            if (!string.IsNullOrEmpty(skladiste))
            {
                oprema = oprema.Where(x => x.Skladiste.Naziv.Contains(skladiste));
                opremaCount = oprema.Count();
            }

            if (!string.IsNullOrEmpty(usluga))
            {
                oprema = oprema.Where(s => s.Usluga.Naziv.Contains(usluga));
                opremaCount = oprema.Count();
            }

            oprema = oprema
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Oprema>
            {
                Data = oprema.AsNoTracking().ToList(),
                TotalItems = opremaCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Oprema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .Include(o => o.Skladiste)
                .Include(o => o.Usluga)
                .FirstOrDefaultAsync(m => m.OpremaId == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // GET: Oprema/Create
        public IActionResult Create()
        {
            ViewData["SkladisteId"] = new SelectList(_context.Skladiste, "SkladisteId", "Naziv");
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv");
            return View();
        }

        // POST: Oprema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpremaId,NazivOpreme,Raspolozivost,SkladisteId,UslugaId")] Oprema oprema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oprema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkladisteId"] = new SelectList(_context.Skladiste, "SkladisteId", "Naziv", oprema.SkladisteId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", oprema.UslugaId);
            return View(oprema);
        }

        // GET: Oprema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema.FindAsync(id);
            if (oprema == null)
            {
                return NotFound();
            }
            ViewData["SkladisteId"] = new SelectList(_context.Skladiste, "SkladisteId", "Naziv", oprema.SkladisteId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", oprema.UslugaId);
            return View(oprema);
        }

        // POST: Oprema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpremaId,NazivOpreme,Raspolozivost,SkladisteId,UslugaId")] Oprema oprema)
        {
            if (id != oprema.OpremaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oprema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpremaExists(oprema.OpremaId))
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
            ViewData["SkladisteId"] = new SelectList(_context.Skladiste, "SkladisteId", "Naziv", oprema.SkladisteId);
            ViewData["UslugaId"] = new SelectList(_context.Usluga, "UslugaId", "Naziv", oprema.UslugaId);
            return View(oprema);
        }

        // GET: Oprema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .Include(o => o.Skladiste)
                .Include(o => o.Usluga)
                .FirstOrDefaultAsync(m => m.OpremaId == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // POST: Oprema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oprema = await _context.Oprema.FindAsync(id);
            _context.Oprema.Remove(oprema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpremaExists(int id)
        {
            return _context.Oprema.Any(e => e.OpremaId == id);
        }
    }
}
