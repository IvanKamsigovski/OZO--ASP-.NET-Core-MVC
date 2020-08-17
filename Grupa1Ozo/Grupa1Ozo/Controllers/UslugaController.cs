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
    public class UslugaController : Controller
    {
        private readonly PI01Context _context;

        public UslugaController(PI01Context context)
        {
            _context = context;
        }

        public IEnumerable<KategorijaUsluge> DajMiKategorijeUsluga()
        {
            return _context.KategorijaUsluge.ToList();
        }

        //Index
        public IActionResult Index(string kategorijausluge, string searchString, int pageNumber = 1, int pageSize = 3)
        {
            ViewData["KategorijeUsluga"] = new SelectList(_context.KategorijaUsluge, "Naziv", "Naziv");
            ViewBag.CurrentKategorijaUsluge = kategorijausluge;
            ViewBag.CurrentSearchString = searchString;

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var usluga = from b in _context.Usluga.Include(m => m.KategorijaUsluge)
                         select b;

            int uslugaCount = usluga.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                usluga = usluga.Where(s => s.Naziv.Contains(searchString));
                uslugaCount = usluga.Count();
            }

            if (!string.IsNullOrEmpty(kategorijausluge))
            {
                usluga = usluga.Where(x => x.KategorijaUsluge.Naziv.Contains(kategorijausluge));
                uslugaCount = usluga.Count();
            }

            usluga = usluga
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Usluga>
            {
                Data = usluga.AsNoTracking().ToList(),
                TotalItems = uslugaCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Usluga/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .Include(u => u.KategorijaUsluge)
                .Include(u => u.Natjecaj)
                .FirstOrDefaultAsync(m => m.UslugaId == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // GET: Usluga/Create
        public IActionResult Create()
        {
            ViewData["KategorijaUslugeId"] = new SelectList(_context.KategorijaUsluge, "KategorijaUslugeId", "Naziv");
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis");
            return View();
        }

        // POST: Usluga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UslugaId,Naziv,Opis,Cijena,KategorijaUslugeId,NatjecajId")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaUslugeId"] = new SelectList(_context.KategorijaUsluge, "KategorijaUslugeId", "Naziv", usluga.KategorijaUslugeId);
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", usluga.NatjecajId);
            return View(usluga);
        }

        // GET: Usluga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga.FindAsync(id);
            if (usluga == null)
            {
                return NotFound();
            }
            ViewData["KategorijaUslugeId"] = new SelectList(_context.KategorijaUsluge, "KategorijaUslugeId", "Naziv", usluga.KategorijaUslugeId);
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", usluga.NatjecajId);
            return View(usluga);
        }

        // POST: Usluga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UslugaId,Naziv,Opis,Cijena,KategorijaUslugeId,NatjecajId")] Usluga usluga)
        {
            if (id != usluga.UslugaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UslugaExists(usluga.UslugaId))
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
            ViewData["KategorijaUslugeId"] = new SelectList(_context.KategorijaUsluge, "KategorijaUslugeId", "Naziv", usluga.KategorijaUslugeId);
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", usluga.NatjecajId);
            return View(usluga);
        }

        // GET: Usluga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluga = await _context.Usluga
                .Include(u => u.KategorijaUsluge)
                .Include(u => u.Natjecaj)
                .FirstOrDefaultAsync(m => m.UslugaId == id);
            if (usluga == null)
            {
                return NotFound();
            }

            return View(usluga);
        }

        // POST: Usluga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluga = await _context.Usluga.FindAsync(id);
            _context.Usluga.Remove(usluga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UslugaExists(int id)
        {
            return _context.Usluga.Any(e => e.UslugaId == id);
        }
    }
}
