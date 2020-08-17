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
    public class ZaposleniciCertifikatiController : Controller
    {
        private readonly PI01Context _context;

        public ZaposleniciCertifikatiController(PI01Context context)
        {
            _context = context;
        }

        // GET: ZaposleniciCertifikati
        public IActionResult Index(string searchString, int pageNumber = 1, int pageSize = 7)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var zaposleniciCertifikati = from b in _context.ZaposleniciCertifikati
                                         .Include(b => b.Zaposlenici)
                                         .Include(m => m.Certifikati)
                                         select b;

            int zaposleniciCertifikatiCount = zaposleniciCertifikati.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                zaposleniciCertifikati = zaposleniciCertifikati.Where(m => m.Zaposlenici.Ime.Contains(searchString));
                zaposleniciCertifikatiCount = zaposleniciCertifikati.Count();
            }

            zaposleniciCertifikati = zaposleniciCertifikati
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<ZaposleniciCertifikati>
            {
                Data = zaposleniciCertifikati.AsNoTracking().ToList(),
                TotalItems = zaposleniciCertifikatiCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: ZaposleniciCertifikati/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var zaposleniciCertifikati = await _context.ZaposleniciCertifikati
        //        .Include(z => z.Certifikati)
        //        .Include(z => z.Zaposlenici)
        //        .FirstOrDefaultAsync(m => m.ZaposleniciId == id);
        //    if (zaposleniciCertifikati == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(zaposleniciCertifikati);
        //}

        // GET: ZaposleniciCertifikati/Create
        public IActionResult Create()
        {
            ViewData["CertifikatiId"] = new SelectList(_context.Certifikati, "CertifikatiId", "Naziv");
            ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime");
            return View();
        }

        // POST: ZaposleniciCertifikati/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZaposleniciId,CertifikatiId")] ZaposleniciCertifikati zaposleniciCertifikati)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposleniciCertifikati);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CertifikatiId"] = new SelectList(_context.Certifikati, "CertifikatiId", "Naziv", zaposleniciCertifikati.CertifikatiId);
            ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciCertifikati.ZaposleniciId);
            return View(zaposleniciCertifikati);
        }

        // GET: ZaposleniciCertifikati/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var zaposleniciCertifikati = await _context.ZaposleniciCertifikati.FindAsync(id);
        //    if (zaposleniciCertifikati == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CertifikatiId"] = new SelectList(_context.Certifikati, "CertifikatiId", "Naziv", zaposleniciCertifikati.CertifikatiId);
        //    ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciCertifikati.ZaposleniciId);
        //    return View(zaposleniciCertifikati);
        //}

        // POST: ZaposleniciCertifikati/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ZaposleniciId,CertifikatiId")] ZaposleniciCertifikati zaposleniciCertifikati)
        //{
        //    if (id != zaposleniciCertifikati.ZaposleniciId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(zaposleniciCertifikati);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ZaposleniciCertifikatiExists(zaposleniciCertifikati.ZaposleniciId))
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
        //    ViewData["CertifikatiId"] = new SelectList(_context.Certifikati, "CertifikatiId", "Naziv", zaposleniciCertifikati.CertifikatiId);
        //    ViewData["ZaposleniciId"] = new SelectList(_context.Zaposlenici, "ZaposleniciId", "Ime", zaposleniciCertifikati.ZaposleniciId);
        //    return View(zaposleniciCertifikati);
        //}

        // GET: ZaposleniciCertifikati/Delete/5
        public async Task<IActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var zaposleniciCertifikati = await _context.ZaposleniciCertifikati
                .Include(z => z.Certifikati)
                .Include(z => z.Zaposlenici)
                .FirstOrDefaultAsync(m => m.ZaposleniciId == id1 && m.CertifikatiId == id2);
            if (zaposleniciCertifikati == null)
            {
                return NotFound();
            }

            return View(zaposleniciCertifikati);
        }

        // POST: ZaposleniciCertifikati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id1, int? id2)
        {
            var zaposleniciCertifikati = await _context.ZaposleniciCertifikati.FirstOrDefaultAsync(m => m.ZaposleniciId == id1 && m.CertifikatiId == id2);
            _context.ZaposleniciCertifikati.Remove(zaposleniciCertifikati);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposleniciCertifikatiExists(int id)
        {
            return _context.ZaposleniciCertifikati.Any(e => e.ZaposleniciId == id);
        }
    }
}
