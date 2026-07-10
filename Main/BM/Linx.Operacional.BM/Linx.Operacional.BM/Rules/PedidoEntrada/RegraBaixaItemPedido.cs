using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Operacional.BM.Rules.PedidoEntrada
{
    public class RegraBaixaItemPedido
    {
        private static object syncLock = new object();
        private RepositorioPedido repositorioPedido;

        public int? BaixarItemPedidoPorIdSku(int NumeroPedido, int IdFilial, int IdSku, decimal Qtde, LinxOperacional contexto)
        {
            int? idLgePedidoItem = null;
            lock (syncLock)
            {
                this.repositorioPedido = new RepositorioPedido(contexto);
                LGE_PEDIDO pedido = repositorioPedido.GetPedidoByNumberBranch(NumeroPedido, IdFilial);
                if (pedido != null)
                {
                    LGE_PEDIDO_ITEM pedItem = pedido.LGE_PEDIDO_ITEM_LISTA.FirstOrDefault(p => p.ID_SKU == IdSku);

                    if (pedItem != null)
                        idLgePedidoItem = BaixarItemPedido(pedItem, Qtde, contexto);
                    else
                        throw new Exception("Baixa do Pedido: Não foi possível encontrar o item do pedido " + (!String.IsNullOrEmpty(pedItem.NUM_ITEM_FORNECEDOR) ? pedItem.NUM_ITEM_FORNECEDOR : String.Empty) +  " por IdSku.");

                    new RegraBaixaPedido().TentaEncerrarPedido(pedido, contexto);
                }
                else
                    throw new Exception("Baixa do Pedido: Não foi possível encontrar o pedido " + (NumeroPedido > 0 ? NumeroPedido.ToString() : String.Empty) + ".");
            }

            return idLgePedidoItem;
        }

        public void BaixarItemPedidoPorIdLgePedidoItem(int idLgePedidoItem, decimal Qtde, LinxOperacional contexto)
        {
            lock (syncLock)
            {
                this.repositorioPedido = new RepositorioPedido(contexto);
                LGE_PEDIDO pedido = repositorioPedido.GetPedidoByIdLgePedidoItem(idLgePedidoItem);
                if (pedido != null)
                {
                    LGE_PEDIDO_ITEM pedItem = pedido.LGE_PEDIDO_ITEM_LISTA.FirstOrDefault(p => p.ID_LGE_PEDIDO_ITEM == idLgePedidoItem);

                    if (pedItem != null)
                        BaixarItemPedido(pedItem, Qtde, contexto);
                    else
                        throw new Exception("Baixa do Pedido: Não foi possível encontrar o item do pedido " + (!String.IsNullOrEmpty(pedItem.NUM_ITEM_FORNECEDOR) ? pedItem.NUM_ITEM_FORNECEDOR : String.Empty) + ".");

                    new RegraBaixaPedido().TentaEncerrarPedido(pedido, contexto);
                }
                else
                    throw new Exception("Baixa do Pedido: Não foi possível encontrar o pedido.");
            }
        }

        public int? BaixarItemPedido(LGE_PEDIDO_ITEM pedidoItem, decimal Qtde)
        {
            int? idLgePedidoItem = null;
            lock (syncLock)
            {
                using (var contexto = new LinxOperacional())
                {
                    idLgePedidoItem = BaixarItemPedido(pedidoItem, Qtde, contexto);
                    contexto.SaveChanges();
                }
            }

            return idLgePedidoItem;
        }

        public int BaixarItemPedido(LGE_PEDIDO_ITEM pedidoItem, decimal Qtde, LinxOperacional contexto)
        {
            lock (syncLock)
            {
                this.repositorioPedido = new RepositorioPedido(contexto);

                Int32 DomainStatusPedidoEncerrado = Convert.ToInt32(Domains.LX_STATUS_LGE_PEDIDO.Encerrado.Value);
                LGE_PEDIDO_STATUS statusEncerrado = repositorioPedido.GetPedidoStatusByDomain(DomainStatusPedidoEncerrado);
                if (statusEncerrado == null) 
                    throw new BM.Exceptions.BusinessModelException("Baixa do Pedido: Não existe um status encerrado cadastrado. Cadastre o status com o tipo encerrado para que o item do pedido possa ser atualizado.");


                pedidoItem.QTDE_ENTREGAR = (pedidoItem.QTDE_PEDIDO) //qtde original a entregar                                           
                                           - (pedidoItem.QTDE_PEDIDO - pedidoItem.QTDE_ENTREGAR) //ja entregue
                                           - Qtde; // Entregando agora

                // Verificação retirada devido necessidade da boticário permite quantidade a entregar ficar negativa (Padial 07/10/2014)
                //if ((pedidoItem.QTDE_ENTREGAR) < 0) 
                //    throw new BM.Exceptions.BusinessModelException("A quantidade baixada excede a quantidade pendente de entrega.");                
                //else 
                if (pedidoItem.QTDE_ENTREGAR <= 0)
                    pedidoItem.ID_PEDIDO_STATUS = statusEncerrado.ID_PEDIDO_STATUS;
                //volta o status anterior quando estiver estornando a quantidade do pedido e restar quantidade a entregar
                if (Qtde < 0 && pedidoItem.QTDE_ENTREGAR > 0 && !pedidoItem.ID_PEDIDO_STATUS_ANTERIOR.IsNullOrEmpty())
                    pedidoItem.ID_PEDIDO_STATUS = Convert.ToInt32(pedidoItem.ID_PEDIDO_STATUS_ANTERIOR);

                pedidoItem.LGE_PEDIDO.QTDE_TOTAL_ENTREGAR = (pedidoItem.LGE_PEDIDO.QTDE_TOTAL_PEDIDO) //qtde original a entregar
                                                      - (pedidoItem.LGE_PEDIDO.QTDE_TOTAL_PEDIDO - pedidoItem.LGE_PEDIDO.QTDE_TOTAL_ENTREGAR) //ja entregue
                                                      - Qtde; // Entregando agora
                pedidoItem.LGE_PEDIDO.VALOR_ENTREGAR_TOTAL = pedidoItem.LGE_PEDIDO.VALOR_ENTREGAR_TOTAL - ((pedidoItem.VALOR_PEDIDO_LIQUIDO / pedidoItem.QTDE_PEDIDO) * Qtde);

                return pedidoItem.ID_LGE_PEDIDO_ITEM;
            }
        }
    }
}
