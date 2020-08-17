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
    public class ZaposleniciPoslaController : Controller
    {
        private readonly PI01Context _context;

        public ZaposleniciPoslaController(PI01Context context)
        {
            _context = context;
        }

        // GET: ZaposleniciPosla
        public async Task<IActionResult> Index()
        {
            var pI01Context = _context.ZaposleniciPosla.Include(z => z.Posao);
            return View(await pI01Context.ToListAsync());
        }

        // GET: ZaposleniciPosla/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleniciPosla = await _context.ZaposleniciPosla
                .Include(z => z.Posao)
                .FirstOrDefaultAsync(m => m.ZaposleniciPoslaId == id);
            if (zaposleniciPosla == null)
            {
                return NotFound();
            }

            return View(zaposleniciPosla);
        }

        // GET: ZaposleniciPosla/Create
        public IActionResult Create()
        {
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis");
            return View();
        }

        // POST: ZaposleniciPosla/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZaposleniciPoslaId,ZaposlenikId,PosaoId")] ZaposleniciPosla zaposleniciPosla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposleniciPosla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", zaposleniciPosla.PosaoId);
            return View(zaposleniciPosla);
        }

        // GET: ZaposleniciPosla/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleniciPosla = await _context.ZaposleniciPosla.FindAsync(id);
            if (zaposleniciPosla == null)
            {
                return NotFound();
            }
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", zaposleniciPosla.PosaoId);
            return View(zaposleniciPosla);
        }

        // POST: ZaposleniciPosla/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZaposleniciPoslaId,ZaposlenikId,PosaoId")] ZaposleniciPosla zaposleniciPosla)
        {
            if (id != zaposleniciPosla.ZaposleniciPoslaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposleniciPosla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposleniciPoslaExists(zaposleniciPosla.ZaposleniciPoslaId))
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
            ViewData["PosaoId"] = new SelectList(_context.Posao, "PosaoId", "Opis", zaposleniciPosla.PosaoId);
            return View(zaposleniciPosla);
        }

        // GET: ZaposleniciPosla/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposleniciPosla = await _context.ZaposleniciPosla
                .Include(z => z.Posao)
                .FirstOrDefaultAsync(m => m.ZaposleniciPoslaId == id);
            if (zaposleniciPosla == null)
            {
                return NotFound();
            }

            return View(zaposleniciPosla);
        }

        // POST: ZaposleniciPosla/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposleniciPosla = await _context.ZaposleniciPosla.FindAsync(id);
            _context.ZaposleniciPosla.Remove(zaposleniciPosla);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposleniciPoslaExists(int id)
        {
            return _context.ZaposleniciPosla.Any(e => e.ZaposleniciPoslaId == id);
        }
    }
}
