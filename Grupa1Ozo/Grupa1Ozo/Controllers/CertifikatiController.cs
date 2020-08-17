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
    public class CertifikatiController : Controller
    {
        private readonly PI01Context _context;

        public CertifikatiController(PI01Context context)
        {
            _context = context;
        }

        // GET: Certifikati
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var certifikati = from b in _context.Certifikati
                         select b;

            int certifikatiCount = certifikati.Count();

            certifikati = certifikati
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Certifikati>
            {
                Data = certifikati.AsNoTracking().ToList(),
                TotalItems = certifikatiCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Certifikati/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikati = await _context.Certifikati
                .FirstOrDefaultAsync(m => m.CertifikatiId == id);
            if (certifikati == null)
            {
                return NotFound();
            }

            return View(certifikati);
        }

        // GET: Certifikati/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certifikati/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertifikatiId,Naziv")] Certifikati certifikati)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certifikati);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certifikati);
        }

        // GET: Certifikati/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikati = await _context.Certifikati.FindAsync(id);
            if (certifikati == null)
            {
                return NotFound();
            }
            return View(certifikati);
        }

        // POST: Certifikati/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertifikatiId,Naziv")] Certifikati certifikati)
        {
            if (id != certifikati.CertifikatiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certifikati);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertifikatiExists(certifikati.CertifikatiId))
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
            return View(certifikati);
        }

        // GET: Certifikati/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certifikati = await _context.Certifikati
                .FirstOrDefaultAsync(m => m.CertifikatiId == id);
            if (certifikati == null)
            {
                return NotFound();
            }

            return View(certifikati);
        }

        // POST: Certifikati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certifikati = await _context.Certifikati.FindAsync(id);
            _context.Certifikati.Remove(certifikati);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertifikatiExists(int id)
        {
            return _context.Certifikati.Any(e => e.CertifikatiId == id);
        }
    }
}
