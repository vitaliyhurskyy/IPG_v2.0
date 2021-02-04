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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Index(string nomePesquisar, int pagina = 1)
        {
            Paginacao paginacao = new Paginacao
            {
                TotalItems = await _db.Projetos.Where(p => nomePesquisar == null || p.Nome.Contains(nomePesquisar)).CountAsync(),
                PaginaAtual = pagina
            };

            List<Projetos> projetos = await _db.Projetos.Where(p => nomePesquisar == null || p.Nome.Contains(nomePesquisar))
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1))
                .Take(paginacao.ItemsPorPagina)
                .ToListAsync();

            ListaProjetosViewModel modelo = new ListaProjetosViewModel
            {
                Paginacao = paginacao,
                Projetos = projetos,
                NomePesquisar = nomePesquisar
            };

            return base.View(modelo);
        }

        // GET: Projetos/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projetos = await _db.Projetos.Include(p => p.Categoria)
                .SingleOrDefaultAsync(m => m.ProjetoId == id);
            if (projetos == null)
            {
                return View("Missing");
            }

            return View(projetos);
        }

        // GET: Projetos/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Projetos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Descricao,CategoriaId")] Projetos projetos, IFormFile ficheiroFoto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_db.Categorias, "CategoriaId", "Nome");
                return View(projetos);
            }

            AtualizaFotoProjeto(projetos, ficheiroFoto);

            _db.Add(projetos);
            await _db.SaveChangesAsync();

            ViewBag.Mensagem = "Projeto adicionado com sucesso.";
            return View("Success");
        }

        private static void AtualizaFotoProjeto(Projetos projetos, IFormFile ficheiroFoto)
        {
            if (ficheiroFoto != null && ficheiroFoto.Length > 0)
            {
                using (var ficheiroMemoria = new MemoryStream())
                {
                    ficheiroFoto.CopyTo(ficheiroMemoria);
                    projetos.Foto = ficheiroMemoria.ToArray();
                }
            }
        }

        // GET: Projetos/Edit/5
        [Authorize]
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

            ViewData["CategoriaId"] = new SelectList(_db.Categorias, "CategoriaId", "Nome");
            return View(projetos);
        }

        // POST: Projetos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ProjetoId,Nome,Descricao,CategoriaId,Foto")] Projetos projetos, IFormFile ficheiroFoto)
        {
            if (id != projetos.ProjetoId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_db.Categorias, "CategoriaId", "Nome");
                return View(projetos);
            }
            try
            {
                AtualizaFotoProjeto(projetos, ficheiroFoto);
                _db.Update(projetos);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetosExist(projetos.ProjetoId))
                {
                    return View("DeleteInsert", projetos);
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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projetos = await _db.Projetos.Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProjetoId == id);
            if (projetos == null)
            {
                ViewBag.Mensagem = "O produto que estava a tentar apagar foi eliminado por outra pessoa.";
                return View("Success");
            }

            return View(projetos);
        }

        // POST: Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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