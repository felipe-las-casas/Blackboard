using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blackboard.Data;
using MVCPacienteHistorico.Models;

namespace Blackboard.Controllers
{
    public class HistoricosController : Controller
    {
        private readonly BlackboardContext _context;

        public HistoricosController(BlackboardContext context)
        {
            _context = context;
        }

        // GET: Historicos
        public async Task<IActionResult> Index()
        {
            var blackboardContext = _context.Historicos.Include(h => h.Paciente);
            return View(await blackboardContext.ToListAsync());
        }

        // GET: Historicos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Historicos == null)
            {
                return NotFound();
            }

            var historico = await _context.Historicos
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.HistoricoId == id);
            if (historico == null)
            {
                return NotFound();
            }

            return View(historico);
        }

        // GET: Historicos/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId");
            return View();
        }

        // POST: Historicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,PacienteId")] Historico historico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Nome", historico.PacienteId);
            return View(historico);
        }

        // GET: Historicos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Historicos == null)
            {
                return NotFound();
            }

            var historico = await _context.Historicos.FindAsync(id);
            if (historico == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId", historico.PacienteId);
            return View(historico);
        }

        // POST: Historicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("HistoricoId,Nome,PacienteId")] Historico historico)
        {
            if (id != historico.HistoricoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoExists(historico.HistoricoId))
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
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteId", historico.PacienteId);
            return View(historico);
        }

        // GET: Historicos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Historicos == null)
            {
                return NotFound();
            }

            var historico = await _context.Historicos
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.HistoricoId == id);
            if (historico == null)
            {
                return NotFound();
            }

            return View(historico);
        }

        // POST: Historicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Historicos == null)
            {
                return Problem("Entity set 'BlackboardContext.Historicos'  is null.");
            }
            var historico = await _context.Historicos.FindAsync(id);
            if (historico != null)
            {
                _context.Historicos.Remove(historico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoExists(long id)
        {
          return (_context.Historicos?.Any(e => e.HistoricoId == id)).GetValueOrDefault();
        }
    }
}
