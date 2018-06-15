using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class Customizar
    {
        [Key]

        public int CustoimizarId { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]

        public string Sobrenome { get; set; }


        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]


        public string Telefone { get; set; }

        [Display(Name = "Endereco")]
        [Required(ErrorMessage = "Você prescisa entrar com o {0}")]

        public string Endereco { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Display(Name = "Documento")]

        public string Documento { get; set; }

        public int TipoDocumentoId { get; set; }

        public string NomeCompleto { get { return string.Format("{0} {1}", Nome, Sobrenome);} }

        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<Ordem> Ordem { get; set; }
    }
}