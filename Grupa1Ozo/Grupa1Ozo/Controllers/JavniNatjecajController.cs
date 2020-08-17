using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupa1Ozo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using cloudscribe.Pagination.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Grupa1Ozo.Controllers
{
    public class JavniNatjecajController : Controller
    {
        private readonly PI01Context _context;

        public JavniNatjecajController(PI01Context context)
        {
            _context = context;
        }

        // GET: JavniNatjecaj
        public IActionResult Index(int pageNumber = 1, int pageSize = 3)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            var javniNatjecaj = from b in _context.JavniNatjecaj
                                select b;

            int jnCount = javniNatjecaj.Count();

            javniNatjecaj = javniNatjecaj.Skip(ExcludeRecords).Take(pageSize);

            var result = new PagedResult<JavniNatjecaj>
            {
                Data = javniNatjecaj.AsNoTracking().ToList(),
                TotalItems = jnCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: JavniNatjecaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecaj = await _context.JavniNatjecaj
                .FirstOrDefaultAsync(m => m.JavniNatjecajId == id);
            if (javniNatjecaj == null)
            {
                return NotFound();
            }

            return View(javniNatjecaj);
        }

        // GET: JavniNatjecaj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JavniNatjecaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JavniNatjecajId,Dobitnik")] JavniNatjecaj javniNatjecaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(javniNatjecaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(javniNatjecaj);
        }

        // GET: JavniNatjecaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecaj = await _context.JavniNatjecaj.FindAsync(id);
            if (javniNatjecaj == null)
            {
                return NotFound();
            }
            return View(javniNatjecaj);
        }

        // POST: JavniNatjecaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JavniNatjecajId,Dobitnik")] JavniNatjecaj javniNatjecaj)
        {
            if (id != javniNatjecaj.JavniNatjecajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(javniNatjecaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JavniNatjecajExists(javniNatjecaj.JavniNatjecajId))
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
            return View(javniNatjecaj);
        }

        // GET: JavniNatjecaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javniNatjecaj = await _context.JavniNatjecaj
                .FirstOrDefaultAsync(m => m.JavniNatjecajId == id);
            if (javniNatjecaj == null)
            {
                return NotFound();
            }

            return View(javniNatjecaj);
        }

        // POST: JavniNatjecaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var javniNatjecaj = await _context.JavniNatjecaj.FindAsync(id);
            _context.JavniNatjecaj.Remove(javniNatjecaj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JavniNatjecajExists(int id)
        {
            return _context.JavniNatjecaj.Any(e => e.JavniNatjecajId == id);
        }
    }
}
