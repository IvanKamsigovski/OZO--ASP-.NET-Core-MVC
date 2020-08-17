using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupa1Ozo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using cloudscribe.Pagination.Models;

namespace Grupa1Ozo.Controllers
{
    public class ZaposleniciController : Controller
    {
        private readonly PI01Context _context;

        public ZaposleniciController(PI01Context context)
        {
            _context = context;
        }

        //public IEnumerable<Opcina> DajMiOpcine()
        //{
        //    return _context.Opcina.ToList();
        //}

        public IActionResult Index(string opcina, string struka, string certifikat, string searchString, int pageNumber = 1, int pageSize = 7)
        {
            ViewData["Opcine"] = new SelectList(_context.Opcina, "Naziv", "Naziv");
            ViewData["Struke"] = new SelectList(_context.Struka, "Naziv", "Naziv");
            ViewData["Certifikati"] = new SelectList(_context.Certifikati, "Naziv", "Naziv");
            ViewBag.CurrentOpcina = opcina;
            ViewBag.CurrentStruka = struka;
            ViewBag.CurrentCertifikat = certifikat;
            ViewBag.CurrentSearchString = searchString;

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var zaposlenici = from b in _context.Zaposlenici.Include(m => m.Opcina)
                              select b;

            int zaposleniciCount = zaposlenici.Count();

            if (!string.IsNullOrEmpty(searchString))
            {
                zaposlenici = zaposlenici.Where(s => s.Ime.Contains(searchString));
                zaposleniciCount = zaposlenici.Count();
            }

            if (!string.IsNullOrEmpty(opcina))
            {
                zaposlenici = zaposlenici.Where(x => x.Opcina.Naziv.Contains(opcina));
                zaposleniciCount = zaposlenici.Count();
            }

            if (!string.IsNullOrEmpty(struka))
            {
                zaposlenici = zaposlenici.Where(s => s.ZaposleniciStruka.Any(e => e.Struka.Naziv == struka));
            }

            if (!string.IsNullOrEmpty(certifikat))
            {
                zaposlenici = zaposlenici.Where(s => s.ZaposleniciCertifikati.Any(e => e.Certifikati.Naziv == certifikat));
            }

            zaposlenici = zaposlenici
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Zaposlenici>
            {
                Data = zaposlenici.AsNoTracking().ToList(),
                TotalItems = zaposleniciCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        //Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenici
                .Include(c => c.Opcina)
                .FirstOrDefaultAsync(m => m.ZaposleniciId == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        //Create
        public IActionResult Create()
        {
            ViewData["OpcinaId"] = new SelectList(_context.Opcina, "OpcinaId", "Naziv");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime,Prezime,OpcinaId")] Zaposlenici zaposlenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaposlenik);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenici.Include(c => c.Opcina).FirstOrDefaultAsync(m => m.ZaposleniciId == id); /*FindAsync(id);*/

            if (zaposlenik == null)
            {
                return NotFound();
            }
            ViewData["OpcinaId"] = new SelectList(_context.Opcina, "OpcinaId", "Naziv");

            return View(zaposlenik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZaposleniciId,Ime,Prezime,OpcinaId")] Zaposlenici zaposlenik)
        {
            if (id != zaposlenik.ZaposleniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposleniciExists(zaposlenik.ZaposleniciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(zaposlenik);
        }

        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenici
                .Include(c => c.Opcina)
                .FirstOrDefaultAsync(m => m.ZaposleniciId == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.Zaposlenici.FindAsync(id);
            _context.Zaposlenici.Remove(zaposlenik);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Check for existance
        private bool ZaposleniciExists(int id)
        {
            return _context.Zaposlenici.Any(e => e.ZaposleniciId == id);
        }
    }
}