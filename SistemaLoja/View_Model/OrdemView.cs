using SistemaLoja.Models;
using System.Collections.Generic;

namespace SistemaLoja.View_Model
{
    public class OrdemView
    {
        public Customizar Customizar { get; set; }
        public ProdutoOrdem Produto { get; set; }
        public List<ProdutoOrdem> Produtos{get;set;}
        
    }
}