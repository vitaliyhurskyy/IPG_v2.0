using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_v2._0.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="Categoria")]
        public string Nome { get; set; }

        public ICollection<Projetos> Projetos { get; set; }
    }
}
