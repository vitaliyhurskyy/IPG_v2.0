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

        public DbSet<IPG_v2._0.Models.Projetos> Projetos { get; set; }
    }
}
