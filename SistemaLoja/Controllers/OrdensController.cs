using SistemaLoja.Models;
using SistemaLoja.View_Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SistemaLoja.Controllers
{
    public class OrdensController : Controller
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["OrdemView"] = ordemView;


            var List = db.Customizars.ToList();
            List.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente!!]" });
            List = List.OrderBy(c =>c.NomeCompleto).ToList();
            ViewBag.CustoimizarId = new SelectList(List,"CustoimizarId","NomeCompleto");
            
            return View(ordemView);
        }


        [HttpPost]
        public ActionResult NovaOrdem(OrdemView ordemView)
        {
            ordemView = Session["ordemView"] as OrdemView;
            var custoimizarId = int.Parse(Request["CustoimizarId"]);
            var list = db.Customizars.ToList();


            if (custoimizarId == 0)
            {

                list = db.Customizars.ToList();
                list.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente!!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustoimizarId = new SelectList(list, "CustoimizarId", "NomeCompleto");
                ViewBag.Error = "Selecione o Cliente";

                return View(ordemView);
            }

            var cliente  = db.Customizars.Find(custoimizarId);
            if (cliente == null)
            {
                list = db.Customizars.ToList();
                list.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente!!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustoimizarId = new SelectList(list, "CustoimizarId", "NomeCompleto");
                ViewBag.Error = "O cliente não existe mais";

                return View(ordemView);
            }
            if(ordemView.Produtos.Count == 0)
            {

                list = db.Customizars.ToList();
                list.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente!!]" });
                list = list.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustoimizarId = new SelectList(list, "CustoimizarId", "NomeCompleto");
                ViewBag.Error = "Selecione um produto ";

                return View(ordemView);
            }
            int ordemId = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var ordem = new Ordem
                    {

                        CustoimizarId = custoimizarId,
                        OrdemData = DateTime.Now,
                        OrdemStatus = OrdemStatus.criada


                    };

                    db.Ordem.Add(ordem);
                    db.SaveChanges();

                    ordemId = db.Ordem.ToList().Select(o => o.OrdemId).Max();
                    foreach (var item in ordemView.Produtos)
                    {
                        var ordemDetalhes = new OrdemDetalhe
                        {
                            ProdutoId = item.ID,
                            Descricao = item.Descricao,
                            Preco = item.Preco,
                            Quantidade = item.Quantidade,
                            OrdemId = ordem.OrdemId
                        };
                        db.OrdemDetalhe.Add(ordemDetalhes);
                        db.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch ( Exception ex )
                {

                    transaction.Rollback();
                    ViewBag.Error = "Error"+ex.Message;
                    return View(ordemView);
                }
            }
               
                    

            
            ViewBag.Mensagem = string.Format("Ordem: {0},foi salva com sucesso", ordemId);

            list = db.Customizars.ToList();
            list.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente!!]" });
            list = list.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustoimizarId = new SelectList(list, "CustoimizarId", "NomeCompleto");

            ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["OrdemView"] = ordemView;



            return View(ordemView);
        }        
        public ActionResult AddProduto()
        {
          

            var List = db.Produtoes.ToList();
            List.Add(new ProdutoOrdem { ID = 0, Descricao = "[Selecione um Produto!!]" });
            List = List.OrderBy(c => c.Descricao).ToList();
            ViewBag.ID = new SelectList(List, "ID", "Descricao");
          
            return View();
        }
        [HttpPost]
        public ActionResult AddProduto(ProdutoOrdem produtoOrdem)
        {
            var ordemView = Session["ordemView"] as OrdemView;
            var List = db.Produtoes.ToList();
            var ID = int.Parse(Request["ID"]);

            if(ID == 0)
            {
                List.Add(new ProdutoOrdem { ID = 0, Descricao = "[Selecione um Produto!!]" });
                List = List.OrderBy(c => c.Descricao).ToList();
                ViewBag.ID = new SelectList(List, "ID", "Descricao");
                ViewBag.error = "Selecione um Produto";

                return View(produtoOrdem);
            }

            var produto = db.Produtoes.Find(ID);
            if (produto == null)
            {
                List.Add(new ProdutoOrdem { ID = 0, Descricao = "[Selecione um Produto!!]" });
                List = List.OrderBy(c => c.Descricao).ToList();
                ViewBag.ID = new SelectList(List, "ID", "Descricao");
                ViewBag.error = "Não Existe o Produto Selecionado";



                return View(produtoOrdem);
            }
            produtoOrdem = ordemView.Produtos.Find(p => p.ID == ID);
            if (produtoOrdem == null)
            {

                produtoOrdem = new ProdutoOrdem
                {
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ID = produto.ID,
                    Quantidade = float.Parse(Request["Quantidade"])
                };
          
                ordemView.Produtos.Add(produtoOrdem);
            }
            else
            {
                produtoOrdem.Quantidade += float.Parse(Request["Quantidade"]);
            }
            var ListC = db.Customizars.ToList();
            //ListC.Add(new Customizar { CustoimizarId = 0, Nome = "[Selecione um Cliente !!]" });
            ListC = ListC.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustoimizarId = new SelectList(ListC, "CustoimizarId", "NomeCompleto");

            return View("NovaOrdem", ordemView);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}