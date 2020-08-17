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
    public class KategorijaUslugeController : Controller
    {
        private readonly PI01Context _context;

        public KategorijaUslugeController(PI01Context context)
        {
            _context = context;
        }

        // GET: KategorijaUsluge
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var kategorijeusluga = from b in _context.KategorijaUsluge
                                   select b;

            int kategorijeuslugaCount = kategorijeusluga.Count();

            kategorijeusluga = kategorijeusluga
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<KategorijaUsluge>
            {
                Data = kategorijeusluga.AsNoTracking().ToList(),
                TotalItems = kategorijeuslugaCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: KategorijaUsluge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaUsluge = await _context.KategorijaUsluge
                .FirstOrDefaultAsync(m => m.KategorijaUslugeId == id);
            if (kategorijaUsluge == null)
            {
                return NotFound();
            }

            return View(kategorijaUsluge);
        }

        // GET: KategorijaUsluge/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KategorijaUsluge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategorijaUslugeId,Naziv")] KategorijaUsluge kategorijaUsluge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategorijaUsluge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorijaUsluge);
        }

        // GET: KategorijaUsluge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaUsluge = await _context.KategorijaUsluge.FindAsync(id);
            if (kategorijaUsluge == null)
            {
                return NotFound();
            }
            return View(kategorijaUsluge);
        }

        // POST: KategorijaUsluge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategorijaUslugeId,Naziv")] KategorijaUsluge kategorijaUsluge)
        {
            if (id != kategorijaUsluge.KategorijaUslugeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorijaUsluge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorijaUslugeExists(kategorijaUsluge.KategorijaUslugeId))
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
            return View(kategorijaUsluge);
        }

        // GET: KategorijaUsluge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategorijaUsluge = await _context.KategorijaUsluge
                .FirstOrDefaultAsync(m => m.KategorijaUslugeId == id);
            if (kategorijaUsluge == null)
            {
                return NotFound();
            }

            return View(kategorijaUsluge);
        }

        // POST: KategorijaUsluge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorijaUsluge = await _context.KategorijaUsluge.FindAsync(id);
            _context.KategorijaUsluge.Remove(kategorijaUsluge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorijaUslugeExists(int id)
        {
            return _context.KategorijaUsluge.Any(e => e.KategorijaUslugeId == id);
        }
    }
}
