using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG_v2._0.Models;
using IPG_v2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace IPG_v2._0.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly ProjetosDbContext _db;

        public ProjetosController(ProjetosDbContext context)
        {
            _db = context;
        }

        // GET: Projetos
        public async Task<IActionResult> Index()
        {
            return View(await _db.Projetos.ToListAsync());
        }

        // GET: Projetos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _db.Projetos
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (produto == null)
            {
                return View("Missing");
            }

            return View(produto);
        }

        // GET: Projetos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projetos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Descricao")] Projetos projetos, IFormFile ficheiroFoto)
        {
            if (!ModelState.IsValid)
            {
               return View(projetos);
            }

            if (ficheiroFoto != null && ficheiroFoto.Length > 0)
            {
                using (var ficheiroMemoria = new MemoryStream())
                {
                    ficheiroFoto.CopyTo(ficheiroMemoria);
                    projetos.Foto = ficheiroMemoria.ToArray();
                }
            }
            _db.Add(projetos);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Projeto adicionado com sucesso.";
            return View("Success");
        }

        // GET: Projetos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projetos = await _db.Projetos.FindAsync(id);
            if (projetos == null)
            {
                return View("Missing");
            }
            return View(projetos);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,Nome,Descricao")] Projetos projetos)
        {
            if (id != projetos.ProjetoId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(projetos);
            }
            try
            {
                _db.Update(projetos);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetosExist(projetos.ProjetoId))
                {
                    return View("DeleteInsert");
                }
                else
                {
                    ModelState.AddModelError("", "Ocorreu um erro. Não foi possível guardar o produto. Tente novamente e se o problema persistir contacte a assistência.");
                    return View(projetos);
                }
            }
            ViewBag.Mensagem = "Projeto alterado com sucesso";
            return View("Success");
        }
           
        

        // GET: Projetos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _db.Projetos
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (produto == null)
            {
                ViewBag.Mensagem = "O produto que estava a tentar apagar foi eliminado por outra pessoa.";
                return View("Success");
            }

            return View();
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projetos = await _db.Projetos.FindAsync(id);
            _db.Projetos.Remove(projetos);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "O produto foi eliminado com sucesso";
            return View("Success");
        }

        private bool ProjetosExist(int id)
        {
            return _db.Projetos.Any(p => p.ProjetoId == id);
        }

      
    }
}