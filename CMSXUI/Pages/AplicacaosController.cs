using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMSXData.Models;

namespace CMSXUI.Pages
{
    public class AplicacaosController : Controller
    {
        private readonly CmsxDbContext _context;

        public AplicacaosController(CmsxDbContext context)
        {
            _context = context;
        }

        // GET: Aplicacaos
        public async Task<IActionResult> Index()
        {
              return _context.Aplicacaos != null ? 
                          View(await _context.Aplicacaos.ToListAsync()) :
                          Problem("Entity set 'CmsxDbContext.Aplicacaos'  is null.");
        }

        // GET: Aplicacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Aplicacaos == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacaos
                .FirstOrDefaultAsync(m => m.Aplicacaoid == id);
            if (aplicacao == null)
            {
                return NotFound();
            }

            return View(aplicacao);
        }

        // GET: Aplicacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aplicacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Url,Datainicio,Datafinal,Idusuarioinicio,Idusuariofim,Pagsegurotoken,Layoutchoose,Posicao,Mailuser,Mailpassword,Mailserver,Mailport,Issecure,Pagefacebook,Pagelinkedin,Pageinstagram,Pagetwitter,Pagepinterest,Pageflicker,Lotipo,Ogleadsense,Isactivea,Header,Aplicacaoid,Isactive")] Aplicacao aplicacao)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Aplicacaoid = Guid.NewGuid();
                _context.Add(aplicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aplicacao);
        }

        // GET: Aplicacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Aplicacaos == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacaos.FindAsync(id);
            if (aplicacao == null)
            {
                return NotFound();
            }
            return View(aplicacao);
        }

        // POST: Aplicacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Url,Datainicio,Datafinal,Idusuarioinicio,Idusuariofim,Pagsegurotoken,Layoutchoose,Posicao,Mailuser,Mailpassword,Mailserver,Mailport,Issecure,Pagefacebook,Pagelinkedin,Pageinstagram,Pagetwitter,Pagepinterest,Pageflicker,Lotipo,Ogleadsense,Isactivea,Header,Aplicacaoid,Isactive")] Aplicacao aplicacao)
        {
            if (id != aplicacao.Aplicacaoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aplicacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicacaoExists(aplicacao.Aplicacaoid))
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
            return View(aplicacao);
        }

        // GET: Aplicacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Aplicacaos == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacaos
                .FirstOrDefaultAsync(m => m.Aplicacaoid == id);
            if (aplicacao == null)
            {
                return NotFound();
            }

            return View(aplicacao);
        }

        // POST: Aplicacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Aplicacaos == null)
            {
                return Problem("Entity set 'CmsxDbContext.Aplicacaos'  is null.");
            }
            var aplicacao = await _context.Aplicacaos.FindAsync(id);
            if (aplicacao != null)
            {
                _context.Aplicacaos.Remove(aplicacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicacaoExists(Guid id)
        {
          return (_context.Aplicacaos?.Any(e => e.Aplicacaoid == id)).GetValueOrDefault();
        }
    }
}
