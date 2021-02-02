using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG_v2._0.Models;
using IPG_v2._0.Data;

namespace IPG_v2._0.Data
{
    public class SeedData
    {


        internal static void PreencheDadosLojaFicticia(ProjetosDbContext _db)
        {
            InsereCategoriaFicticias(_db);
            InsereProductosFicticios(_db);
        }

        private static void InsereCategoriaFicticias(ProjetosDbContext bd)
        {
            GaranteExistenciaCategoria(bd, "HTML");
            GaranteExistenciaCategoria(bd, "CSS");
            GaranteExistenciaCategoria(bd, "Outros");
        }

        private static void GaranteExistenciaCategoria(ProjetosDbContext bd, string nome)
        {
            Categorias categoria = bd.Categorias.FirstOrDefault(c => c.Nome == nome);
            if (categoria == null)
            {
                categoria = new Categorias { Nome = nome };
                bd.Categorias.Add(categoria);
                bd.SaveChanges();
            }
        }

        private static void InsereProductosFicticios(ProjetosDbContext bd)
        {
            if (bd.Projetos.Any()) return;

            Categorias categoriaHTML = bd.Categorias.FirstOrDefault(c => c.Nome == "HTML");
            Categorias categoriaCSS = bd.Categorias.FirstOrDefault(c => c.Nome == "CSS");

            bd.Projetos.AddRange(new Projetos[] {
                new Projetos
                {
                    Nome = "Teste",
                    Descricao = "Teste",
                    Categoria = categoriaCSS
                },
                new Projetos
                {
                    Nome = "teste 2",
                    Descricao = "teste2",
                    Categoria = categoriaCSS
                },
                new Projetos
                {
                    Nome = "teste3",
                    Descricao = " teste 3",
                    Categoria = categoriaCSS
                }
            });

            bd.SaveChanges();
        }
    }
}

