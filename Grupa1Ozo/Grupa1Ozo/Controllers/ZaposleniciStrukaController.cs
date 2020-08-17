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
    public class ZaposleniciStrukaController : Controller
    {
        private readonly PI01Context _context;

        public ZaposleniciStrukaController(PI01Context context)
        {
            _context = context;
        }

        // GET: ZaposleniciStruka
        public IActionResult Index(string searchString, int pageNumber = 1, int pageSize = 7)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var zaposleniciStruka = from b in _context.ZaposleniciStruka
                                    .Include(m => m.Zaposlenici)
                                    .Include(m => m.Struka)
                                    select b;

            int zaposleniciStrukaCount = zaposleniciStruka.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                zaposleniciStruka = zaposleniciStruka.Where(s => s.Zaposlenici.Ime.Contains(searchString));
                zaposleniciStrukaCount = zaposleniciStruka.Count();
            }

            zaposleniciStruka = zaposleniciStruka
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<ZaposleniciStruka>
            {
                Data = zaposleniciStruka.AsNoTracking().ToList(),
                TotalItems = zaposleniciStrukaCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: ZaposleniciStruka/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var zaposleniciStruka = await _context.ZaposleniciStruka
        //        .Include(z => z.Struka)
        //        .Include(z => z.Zaposlenici)
        //        .FirstOrDefaultAsync(m => m.ZaposleniciId == id);
        //    if (zaposleniciStruka == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(zaposleniciStruka);
        //}

        // GET: ZaposleniciStruka/Create
        public IActionResult Create()
        {
            ViewData["StrukaId"] = new SelectList(_context.Struka, "StrukaId", "Naziv");
            ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime");

            return View();
        }

        // POST: ZaposleniciStruka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZaposleniciId,StrukaId")] ZaposleniciStruka zaposleniciStruka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposleniciStruka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StrukaId"] = new SelectList(_context.Struka, "StrukaId", "Naziv", zaposleniciStruka.StrukaId);
            ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciStruka.ZaposleniciId);
            return View(zaposleniciStruka);
        }

        // GET: ZaposleniciStruka/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var zaposleniciStruka = await _context.ZaposleniciStruka.FindAsync(id);
        //    if (zaposleniciStruka == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["StrukaId"] = new SelectList(_context.Struka, "StrukaId", "Naziv", zaposleniciStruka.StrukaId);
        //    ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciStruka.ZaposleniciId);
        //    return View(zaposleniciStruka);
        //}

        // POST: ZaposleniciStruka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ZaposleniciId,StrukaId")] ZaposleniciStruka zaposleniciStruka)
        //{
        //    if (id != zaposleniciStruka.ZaposleniciId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(zaposleniciStruka);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ZaposleniciStrukaExists(zaposleniciStruka.ZaposleniciId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StrukaId"] = new SelectList(_context.Struka, "StrukaId", "Naziv", zaposleniciStruka.StrukaId);
        //    ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciStruka.ZaposleniciId);
        //    return View(zaposleniciStruka);
        //}

        // GET: ZaposleniciStruka/Delete/5
        public async Task<IActionResult> Delete(int? id1, int?id2)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            if (id2 == null)
            {
                return NotFound();
            }

            var zaposleniciStruka = await _context.ZaposleniciStruka
                .Include(z => z.Struka)
                .Include(z => z.Zaposlenici)
                .FirstOrDefaultAsync(m => m.ZaposleniciId == id1 && m.StrukaId == id2);
            if (zaposleniciStruka == null)
            {
                return NotFound();
            }

            return View(zaposleniciStruka);
        }

        // POST: ZaposleniciStruka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id1, int? id2)
        {
            var zaposleniciStruka = await _context.ZaposleniciStruka.FirstOrDefaultAsync(m => m.ZaposleniciId == id1 && m.StrukaId == id2);/*FindAsync(id);*/ /*FirstOrDefaultAsync(m => m.ZaposleniciId == id)*/
            _context.ZaposleniciStruka.Remove(zaposleniciStruka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposleniciStrukaExists(int id)
        {
            return _context.ZaposleniciStruka.Any(e => e.ZaposleniciId == id);
        }
    }
}
