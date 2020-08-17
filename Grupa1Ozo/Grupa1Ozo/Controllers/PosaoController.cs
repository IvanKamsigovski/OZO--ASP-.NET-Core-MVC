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
    public class PosaoController : Controller
    {
        private readonly PI01Context _context;

        public PosaoController(PI01Context context)
        {
            _context = context;
        }

        // GET: Posao
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var posao = from b in _context.Posao.Include(m => m.Natjecaj)
                        select b;

            int posaoCount = posao.Count();

            posao = posao.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<Posao>
            {
                Data = posao.AsNoTracking().ToList(),
                TotalItems = posaoCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: Posao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao
                .Include(p => p.Natjecaj)
                .FirstOrDefaultAsync(m => m.PosaoId == id);
            if (posao == null)
            {
                return NotFound();
            }

            return View(posao);
        }

        // GET: Posao/Create
        public IActionResult Create()
        {
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis");
            return View();
        }

        // POST: Posao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PosaoId,Opis,NatjecajId")] Posao posao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", posao.NatjecajId);
            return View(posao);
        }

        // GET: Posao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao.FindAsync(id);
            if (posao == null)
            {
                return NotFound();
            }
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", posao.NatjecajId);
            return View(posao);
        }

        // POST: Posao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PosaoId,Opis,NatjecajId")] Posao posao)
        {
            if (id != posao.PosaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosaoExists(posao.PosaoId))
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
            ViewData["NatjecajId"] = new SelectList(_context.Natjecaj, "NatjecajId", "Opis", posao.NatjecajId);
            return View(posao);
        }

        // GET: Posao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posao = await _context.Posao
                .Include(p => p.Natjecaj)
                .FirstOrDefaultAsync(m => m.PosaoId == id);
            if (posao == null)
            {
                return NotFound();
            }

            return View(posao);
        }

        // POST: Posao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posao = await _context.Posao.FindAsync(id);
            _context.Posao.Remove(posao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosaoExists(int id)
        {
            return _context.Posao.Any(e => e.PosaoId == id);
        }
    }
}
