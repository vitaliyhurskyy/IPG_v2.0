using IPG_v2._0.Data;
using IPG_v2._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_v2._0.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ProjetosDbContext _db;
        public PortfolioController(ProjetosDbContext db)
        {
            _db= db;
        }

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

        
    }
}
