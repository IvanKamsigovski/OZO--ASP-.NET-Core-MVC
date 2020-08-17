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
    public class JavniNatjecajPonudeController : Controller
    {
        private readonly PI01Context _context;

        public JavniNatjecajPonudeController(PI01Context context)
        {
            _context = context;
        }

        public IEnumerable<JavniNatjecaj> DajMiNatjecaj()
        {
            return _context.JavniNatjecaj.ToList();
        }

        // GET: JavniNatjecajPonude
        public IActionResult Index(string javniNatjecaj, string searchFirma ,int pageNumber = 1, int pageSize = 3)
        {
            ViewData["JavniNatjecaj"] = new SelectList(_context.JavniNatjecaj, "Dobitnik", "Dobitnik");
            ViewBag.CurrentJNatjecaj = javniNatjecaj;
            ViewBag.CurrentFirma = searchFirma;

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var javniNatjecajPonude = from b in _context.JavniNatjecajPonude.Include(m => m.JavniNatjecaj)
                                      select b;

            int jnpCount = javniNatjecajPonude.Count();

            //Search
            if (!string.IsNullOrEmpty(searchFirma))
            {
                javniNatjecajPonude = javniNatjecajPonude.Where(s => s.Firma.Contains(searchFirma));
               
            }
            if (!string.IsNullOrEmpty(javniNatjecaj))
            {
                javniNatjecajPonude = javniNatjecajPonude.Where(x => x.JavniNatjecaj.Dobitnik.Contains(javniNatjecaj));
                jnpCount = javniNatjecaj.Count();
            }
            //-----------

            javniNatjecajPonude = javniNatjecajPonude.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<JavniNatjecajPonude>
            {
                Data = javniNatjecajPonude.AsNoTracking().ToList(),
                TotalItems = jnpCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: JavniNatjecajPonude/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecajPonude = await _context.JavniNatjecajPonude
                .Include(j => j.JavniNatjecaj)
                .FirstOrDefaultAsync(m => m.JavniNatjecajPonudeId == id);
            if (javniNatjecajPonude == null)
            {
                return NotFound();
            }

            return View(javniNatjecajPonude);
        }

        // GET: JavniNatjecajPonude/Create
        public IActionResult Create()
        {
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik");
            return View();
        }

        // POST: JavniNatjecajPonude/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JavniNatjecajPonudeId,Firma,Cijena,JavniNatjecajId")] JavniNatjecajPonude javniNatjecajPonude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(javniNatjecajPonude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", javniNatjecajPonude.JavniNatjecajId);
            return View(javniNatjecajPonude);
        }

        // GET: JavniNatjecajPonude/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecajPonude = await _context.JavniNatjecajPonude.FindAsync(id);
            if (javniNatjecajPonude == null)
            {
                return NotFound();
            }
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", javniNatjecajPonude.JavniNatjecajId);
            return View(javniNatjecajPonude);
        }

        // POST: JavniNatjecajPonude/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JavniNatjecajPonudeId,Firma,Cijena,JavniNatjecajId")] JavniNatjecajPonude javniNatjecajPonude)
        {
            if (id != javniNatjecajPonude.JavniNatjecajPonudeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(javniNatjecajPonude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JavniNatjecajPonudeExists(javniNatjecajPonude.JavniNatjecajPonudeId))
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
            ViewData["JavniNatjecajId"] = new SelectList(_context.JavniNatjecaj, "JavniNatjecajId", "Dobitnik", javniNatjecajPonude.JavniNatjecajId);
            return View(javniNatjecajPonude);
        }

        // GET: JavniNatjecajPonude/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecajPonude = await _context.JavniNatjecajPonude
                .Include(j => j.JavniNatjecaj)
                .FirstOrDefaultAsync(m => m.JavniNatjecajPonudeId == id);
            if (javniNatjecajPonude == null)
            {
                return NotFound();
            }

            return View(javniNatjecajPonude);
        }

        // POST: JavniNatjecajPonude/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var javniNatjecajPonude = await _context.JavniNatjecajPonude.FindAsync(id);
            _context.JavniNatjecajPonude.Remove(javniNatjecajPonude);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JavniNatjecajPonudeExists(int id)
        {
            return _context.JavniNatjecajPonude.Any(e => e.JavniNatjecajPonudeId == id);
        }
    }
}
