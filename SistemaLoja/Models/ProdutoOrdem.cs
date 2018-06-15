using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Models
{
    public class ProdutoOrdem: Produto
    {
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]

        public float Quantidade { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]

        public decimal Valor { get { return Preco * (decimal)Quantidade; } }
    }
}