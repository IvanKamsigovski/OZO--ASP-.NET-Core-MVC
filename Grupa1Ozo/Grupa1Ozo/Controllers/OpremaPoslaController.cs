using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupa1Ozo.Models;

namespace Grupa1Ozo.Controllers
{
    public class OpremaPoslaController : Controller
    {
        private readonly PI01Context _context;

        public OpremaPoslaController(PI01Context context)
        {
            _context = context;
        }

        // GET: OpremaPosla
        public async Task<IActionResult> Index()
        {
            var pI01Context = _context.OpremaPosla.Include(o => o.Posao);
            return View(await pI01Context.ToListAsync());
        }

        // GET: OpremaPosla/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opremaPosla = await _context.OpremaPosla
                .Include(o => o.Posao)
                .FirstOrDefaultAsync(m => m.OpremaPoslaId == id);
            if (opremaPosla == null)
            {
                return NotFound();
            }

            return View(opremaPosla);
        }

        // GET: OpremaPosla/Create
        public IActionResult Create()
        {
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis");
            return View();
        }

        // POST: OpremaPosla/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpremaPoslaId,OpremaId,PosaoId")] OpremaPosla opremaPosla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opremaPosla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", opremaPosla.PosaoId);
            return View(opremaPosla);
        }

        // GET: OpremaPosla/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opremaPosla = await _context.OpremaPosla.FindAsync(id);
            if (opremaPosla == null)
            {
                return NotFound();
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", opremaPosla.PosaoId);
            return View(opremaPosla);
        }

        // POST: OpremaPosla/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpremaPoslaId,OpremaId,PosaoId")] OpremaPosla opremaPosla)
        {
            if (id != opremaPosla.OpremaPoslaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opremaPosla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpremaPoslaExists(opremaPosla.OpremaPoslaId))
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
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", opremaPosla.PosaoId);
            return View(opremaPosla);
        }

        // GET: OpremaPosla/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opremaPosla = await _context.OpremaPosla
                .Include(o => o.Posao)
                .FirstOrDefaultAsync(m => m.OpremaPoslaId == id);
            if (opremaPosla == null)
            {
                return NotFound();
            }

            return View(opremaPosla);
        }

        // POST: OpremaPosla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opremaPosla = await _context.OpremaPosla.FindAsync(id);
            _context.OpremaPosla.Remove(opremaPosla);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpremaPoslaExists(int id)
        {
            return _context.OpremaPosla.Any(e => e.OpremaPoslaId == id);
        }
    }
}
