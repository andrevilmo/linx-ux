using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.PedidoEntrada
{
    public class RepositorioPedido
    {
        private LinxOperacional contexto = null;

        public RepositorioPedido(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        public LGE_PEDIDO_STATUS GetPedidoStatusByDomain(int DomainStatusPedido)
        {
            return this.contexto.LGE_PEDIDO_STATUS.Where(w => w.LX_STATUS_LGE_PEDIDO == DomainStatusPedido).FirstOrDefault();
        }

        public LGE_PEDIDO GetPedido(int IdPedido)
        {
            var query = this.contexto.LGE_PEDIDO
                .Include("LGE_PEDIDO_ITEM_LISTA")
                .FirstOrDefault(p => p.ID_LGE_PEDIDO == IdPedido);

            return query;
        }

        public LGE_PEDIDO GetPedidoByNumberBranch(int Numero, int Filial)
        {
            return this.contexto.LGE_PEDIDO
                .Include("LGE_PEDIDO_ITEM_LISTA")
                .Where(w => w.NUMERO_PEDIDO == Numero && w.ID_FILIAL_PEDIDO == Filial)
                .FirstOrDefault();
        }

        public LGE_PEDIDO GetPedidoByIdLgePedidoItem(int idLgePedidoItem)
        {
            return this.contexto.LGE_PEDIDO
                .Include("LGE_PEDIDO_ITEM_LISTA")
                .Where(w => w.LGE_PEDIDO_ITEM_LISTA.Count(f => f.ID_LGE_PEDIDO_ITEM == idLgePedidoItem) > 0)
                .FirstOrDefault();
        }

        //public LGE_PEDIDO GetPedidoByNumberBranch(int Numero, int Filial)
        //{
        //    LGE_PEDIDO pedido = this.contexto.LGE_PEDIDO.FirstOrDefault(p => p.NUMERO_PEDIDO == Numero && p.ID_FILIAL_PEDIDO == Filial);

        //    if (pedido != null)
        //    {
        //        pedido.LGE_PEDIDO_ITEM_LISTA = SetPedidoItens(pedido.ID_LGE_PEDIDO);
        //    }

        //    return pedido;
        //}

        public List<LGE_PEDIDO_ITEM> SetPedidoItens(int idPedido)
        {
            List<LGE_PEDIDO_ITEM> list = new List<LGE_PEDIDO_ITEM>();

            list = this.contexto.LGE_PEDIDO_ITEM.Where(p => p.ID_LGE_PEDIDO == idPedido).ToList();

            return list;
        }

        public List<LGE_PEDIDO_ITEM> GetItensPedido(int IdPedido)
        {
            var query = this.contexto.LGE_PEDIDO_ITEM.Where(p => p.ID_LGE_PEDIDO == IdPedido);

            return query.ToList();
        }

        public LGE_PEDIDO_ITEM GetPedidoItem(int IdPedidoItem)
        {
            var query = this.contexto.LGE_PEDIDO_ITEM
                .Include("LGE_PEDIDO")
                .First(p => p.ID_LGE_PEDIDO_ITEM == IdPedidoItem);

            return query;
        }

        public List<LGE_PEDIDO_STATUS> GetStatusPedido()
        {
            var query = this.contexto.LGE_PEDIDO_STATUS.ToList();

            return query;
        }

        public List<TBC_PFJ> GetFiliais(List<string> lstCnpjFilial)
        {
            return this.contexto.TBC_PFJ.Where(w => lstCnpjFilial.Contains(w.CNPJ_CPF)).ToList();
        }

        public List<LGE_PEDIDO_ITEM> GetItensPedidoFluxoCaixa(List<int> lstIdFilial)
        {
            //Lista com os status possiveis

            List<int> lstStatusFiltrar = new List<int>();

            lstStatusFiltrar.Add(1); // "AprovacaoInterna"
            lstStatusFiltrar.Add(3); // "AprovacaoExterna"
            lstStatusFiltrar.Add(4); // AguardandoRecebimento

            return this.contexto.LGE_PEDIDO_ITEM
                       .Include("LGE_PEDIDO")
                       .Include("LGE_PEDIDO.LJV_LOJA")
                       .Include("LGE_PEDIDO.TBC_FORNECEDOR")
                       .Where(w => lstIdFilial.Contains(w.LGE_PEDIDO.ID_FILIAL_PEDIDO) && lstStatusFiltrar.Contains(w.LGE_PEDIDO_STATUS.LX_STATUS_LGE_PEDIDO))
                       .ToList();
        }

        public List<LNF_CONDICAO_PAGAMENTO> GetCondicaoPagamento(List<int> lstIdCondicaoPagamento)
        {
            return this.contexto.LNF_CONDICAO_PAGAMENTO.Include("LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA").Where(w => lstIdCondicaoPagamento.Contains(w.ID_CONDICAO_PAGAMENTO)).ToList();
        }

        public void SaveChanges()
        {
            try
            {
                if (contexto != null)
                    this.contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (contexto != null)
                contexto.Dispose();
        }
    }
}
