using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_v2._0.Models
{
    public class Projetos
    {
        [Key]
        public int ProjetoId { get; set; }
        [Required(ErrorMessage = "Deve preencher o nome.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "O nome deve ter pelo menos 4 caracteres e não deve exceder os 20 caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Deve preencher o Descrição.")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "A descrição deve ter pelo menos 10 caracteres e não deve exceder os 200 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        public byte[] Foto { get; set; } 
    }
}
