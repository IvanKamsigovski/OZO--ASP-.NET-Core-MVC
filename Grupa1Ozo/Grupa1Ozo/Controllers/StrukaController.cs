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
    public class StrukaController : Controller
    {
        private readonly PI01Context _context;

        public StrukaController(PI01Context context)
        {
            _context = context;
        }

        // GET: Struka
        public IActionResult Index(int pageNumber=1, int pageSize=3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var struke = from b in _context.Struka
                         select b;

            int strukeCount = struke.Count();

            struke = struke
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Struka>
            {
                Data = struke.AsNoTracking().ToList(),
                TotalItems = strukeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Struka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var struka = await _context.Struka
                .FirstOrDefaultAsync(m => m.StrukaId == id);
            if (struka == null)
            {
                return NotFound();
            }

            return View(struka);
        }

        // GET: Struka/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Struka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StrukaId,Naziv")] Struka struka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(struka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(struka);
        }

        // GET: Struka/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var struka = await _context.Struka.FindAsync(id);
            if (struka == null)
            {
                return NotFound();
            }
            return View(struka);
        }

        // POST: Struka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StrukaId,Naziv")] Struka struka)
        {
            if (id != struka.StrukaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(struka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrukaExists(struka.StrukaId))
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
            return View(struka);
        }

        // GET: Struka/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var struka = await _context.Struka
                .FirstOrDefaultAsync(m => m.StrukaId == id);
            if (struka == null)
            {
                return NotFound();
            }

            return View(struka);
        }

        // POST: Struka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var struka = await _context.Struka.FindAsync(id);
            _context.Struka.Remove(struka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrukaExists(int id)
        {
            return _context.Struka.Any(e => e.StrukaId == id);
        }
    }
}
