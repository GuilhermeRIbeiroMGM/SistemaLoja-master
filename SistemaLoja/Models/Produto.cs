using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class Produto
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]

        public string Descricao { get; set; }

        [Display(Name = "Preco")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]

        public decimal Preco { get; set; }

        [Display(Name = "Ultimacompra")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]
        [DataType(DataType.Date)]

        public DateTime UltimaCompra { get; set; }

        [Display(Name = "Estoque")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public float Estoque { get; set; }

        [Display(Name = "Comentario")]
        [DataType(DataType.MultilineText)]

        public string Comentario { get; set; }

        public virtual ICollection<FornecedorProduto> FornecedorProduto { get; set; }
        public virtual ICollection<OrdemDetalhe> OrdemDetalhe { get; set; }
       
    }
}