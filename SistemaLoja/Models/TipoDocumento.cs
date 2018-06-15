using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaLoja.Models
{
    public class TipoDocumento
    {
        [Key]
        [Display(Name ="Tipo de Documento")]
        public int TipoDocumentoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Funcionario> Funcionario { get; set; }
        public virtual ICollection<Customizar> Customizacao { get; set; }
    }
}