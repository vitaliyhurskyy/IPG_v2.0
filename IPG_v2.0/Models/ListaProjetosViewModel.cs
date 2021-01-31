using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_v2._0.Models
{
    public class ListaProjetosViewModel
    {
        public List<Projetos> Projetos { get; set; }
        public Paginacao Paginacao { get; set; }
        public string NomePesquisar { get; set; }
    }
}
