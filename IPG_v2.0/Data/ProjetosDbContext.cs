using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG_v2._0.Models;

namespace IPG_v2._0.Data
{
    public class ProjetosDbContext : DbContext
    {
        public ProjetosDbContext( DbContextOptions<ProjetosDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projetos>() //Lado N
                .HasOne(p => p.Categoria) // um projetos tem uma Categoria
                .WithMany(c => c.Projetos) // que por sua vez tem vários projetos
                .HasForeignKey(p => p.CategoriaId) //chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); // não permitir o cascade delete

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<IPG_v2._0.Models.Projetos> Projetos { get; set; }
        public DbSet<IPG_v2._0.Models.Categorias> Categorias { get; set; }
    }
}
