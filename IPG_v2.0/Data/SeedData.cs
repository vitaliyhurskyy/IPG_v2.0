using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG_v2._0.Models;
using IPG_v2._0.Data;
using Microsoft.AspNetCore.Server;
using System.IO;

namespace IPG_v2._0.Data
{
    public class SeedData
    {
        //private const string NOME_UTILIZADOR_ADMIN_PADRAO = "admin@ipg.pt";
        //private const string PASSWORD_UTILIZADOR_ADMIN_PADRAO = "Secret123$";

        //private const string ROLE_ADIMINISTRADOR = "Administrador";
        //private const string ROLE_CLIENTE = "Cliente";
        //private const string ROLE_GESTOR_PRODUTOS = "GestorProjetos";


        ////internal static void PreencheDadosLojaFicticia(ProjetosDbContext _db)
        ////{
        ////    InsereCategoriaFicticias(_db);
        ////    InsereProductosFicticios(_db);
        ////}

        ////private static void InsereCategoriaFicticias(ProjetosDbContext _db)
        ////{
        ////    GaranteExistenciaCategoria(_db, "C#");
        ////    GaranteExistenciaCategoria(_db, "HTML + CSS");
        ////    GaranteExistenciaCategoria(_db, "Outros");
        ////}

        ////private static void GaranteExistenciaCategoria(ProjetosDbContext _db, string nome)
        ////{
        ////    Categorias categoria = _db.Categorias.FirstOrDefault(c => c.Nome == nome);
        ////    if (categoria == null)
        ////    {
        ////        categoria = new Categorias { Nome = nome };
        ////        _db.Categorias.Add(categoria);
        ////        _db.SaveChanges();
        ////    }
        ////}



        ////private static void InsereProductosFicticios(ProjetosDbContext _db)
        ////{
        ////    if (_db.Projetos.Any()) return;

        ////    Categorias categoriaC = _db.Categorias.FirstOrDefault(c => c.Nome == "C#");
        ////    Categorias categoriaHtmlCss = _db.Categorias.FirstOrDefault(c => c.Nome == "HTML + CSS");

        ////    _db.Projetos.AddRange(new Projetos[] {
        ////        new Projetos
        ////        {
        ////            Nome = "TesteSeed",
        ////            Descricao = "Teste",
        ////            Categoria = categoriaHtmlCss
        ////            }
        ////    });

        ////    _db.SaveChanges();

        ////}

        //internal static async Task InsereUtilizadoresFicticiosAsync(UserManager<IdentityUser> gestorUtilizadores)
        //{
        //    IdentityUser cliente = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, "joao@ipg.pt", "Secret123$");
        //    await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, cliente, ROLE_CLIENTE);

        //    IdentityUser gestor = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, "maria@ipg.pt", "Secret123$");
        //    await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, gestor, ROLE_GESTOR_PRODUTOS);
        //}

        //internal static async Task InsereRolesAsync(RoleManager<IdentityRole> gestorRoles)
        //{
        //    await CriaRoleSeNecessario(gestorRoles, ROLE_ADIMINISTRADOR);
        //    await CriaRoleSeNecessario(gestorRoles, ROLE_CLIENTE);
        //    await CriaRoleSeNecessario(gestorRoles, ROLE_GESTOR_PRODUTOS);
        //    //await CriaRoleSeNecessario(gestorRoles, "PodeAlterarPrecoProdutos");
        //}
        //private static async Task CriaRoleSeNecessario(RoleManager<IdentityRole> gestorRoles, string funcao)
        //{
        //    if (!await gestorRoles.RoleExistsAsync(funcao))
        //    {
        //        IdentityRole role = new IdentityRole(funcao);
        //        await gestorRoles.CreateAsync(role);
        //    }
        //}

        //internal static async Task InsereAdministradorPadraoAsync(UserManager<IdentityUser> gestorUtilizadores)
        //{
        //    IdentityUser utilizador = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, NOME_UTILIZADOR_ADMIN_PADRAO, PASSWORD_UTILIZADOR_ADMIN_PADRAO);
        //    await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_ADIMINISTRADOR);
        //}

        //private static async Task AdicionaUtilizadorRoleSeNecessario(UserManager<IdentityUser> gestorUtilizadores, IdentityUser utilizador, string role)
        //{
        //    if (!await gestorUtilizadores.IsInRoleAsync(utilizador, role))
        //    {
        //        await gestorUtilizadores.AddToRoleAsync(utilizador, role);
        //    }
        //}

        //private static async Task<IdentityUser> CriaUtilizadorSeNaoExiste(UserManager<IdentityUser> gestorUtilizadores, string nomeUtilizador, string password)
        //{
        //    IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(nomeUtilizador);

        //    if (utilizador == null)
        //    {
        //        utilizador = new IdentityUser(nomeUtilizador);
        //        await gestorUtilizadores.CreateAsync(utilizador, password);
        //    }

        //    return utilizador;
        //}
    }
}

