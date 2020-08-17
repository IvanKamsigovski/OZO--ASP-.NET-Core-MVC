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
    public class OpcinaController : Controller
    {
        private readonly PI01Context _context;

        public OpcinaController(PI01Context context)
        {
            _context = context;
        }

        // GET: Opcina
        public IActionResult Index(int pageNumber=1, int pageSize=3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var opcine = from b in _context.Opcina
                         select b;

            int opcineCount = opcine.Count();

            opcine = opcine
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<Opcina>
            {
                Data = opcine.AsNoTracking().ToList(),
                TotalItems = opcineCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Opcina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcina = await _context.Opcina
                .FirstOrDefaultAsync(m => m.OpcinaId == id);
            if (opcina == null)
            {
                return NotFound();
            }

            return View(opcina);
        }

        // GET: Opcina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opcina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpcinaId,Naziv")] Opcina opcina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opcina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opcina);
        }

        // GET: Opcina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcina = await _context.Opcina.FindAsync(id);
            if (opcina == null)
            {
                return NotFound();
            }
            return View(opcina);
        }

        // POST: Opcina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpcinaId,Naziv")] Opcina opcina)
        {
            if (id != opcina.OpcinaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opcina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpcinaExists(opcina.OpcinaId))
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
            return View(opcina);
        }

        // GET: Opcina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opcina = await _context.Opcina
                .FirstOrDefaultAsync(m => m.OpcinaId == id);
            if (opcina == null)
            {
                return NotFound();
            }

            return View(opcina);
        }

        // POST: Opcina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opcina = await _context.Opcina.FindAsync(id);
            _context.Opcina.Remove(opcina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpcinaExists(int id)
        {
            return _context.Opcina.Any(e => e.OpcinaId == id);
        }
    }
}
