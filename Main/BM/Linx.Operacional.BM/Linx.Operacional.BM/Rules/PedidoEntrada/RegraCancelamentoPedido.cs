using Linx.Operacional.BM.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.PedidoEntrada
{
    public class RegraCancelamentoPedido
    {
        private static object syncLock = new object();
        private RepositorioPedido repositorioPedido;

        public RegraCancelamentoPedido()
        {

        }

        public bool CancelarPedido(int IdPedido)
        {
            lock (syncLock)
            {
                using (var contexto = new LinxOperacional())
                {
                    this.repositorioPedido = new RepositorioPedido(contexto);
                    LGE_PEDIDO pedido = this.repositorioPedido.GetPedido(IdPedido);

                    List<LGE_PEDIDO_STATUS> PedidoStatus = this.repositorioPedido.GetStatusPedido();
                    KeyValuePair<string, string> StatusCancelado = LX_STATUS_LGE_PEDIDO.GetValues().First(p => p.Value.Equals("Cancelado"));

                    foreach (LGE_PEDIDO_ITEM item in pedido.LGE_PEDIDO_ITEM_LISTA)
                    {
                        item.QTDE_CANCELADA = item.QTDE_ENTREGAR;
                        item.QTDE_ENTREGAR = 0;

                        item.LGE_PEDIDO_STATUS = PedidoStatus.First(p => p.LX_STATUS_LGE_PEDIDO == Convert.ToInt16(StatusCancelado.Key));
                    }

                    this.repositorioPedido.SaveChanges();

                    return true;
                }
            }
        }

        public bool CancelarItemPedido(int IdPedido, int IdItemPedido)
        {
            lock (syncLock)
            {
                using (var contexto = new LinxOperacional())
                {
                    this.repositorioPedido = new RepositorioPedido(contexto);
                    LGE_PEDIDO pedido = this.repositorioPedido.GetPedido(IdPedido);

                    LGE_PEDIDO_ITEM pedidoItem = pedido.LGE_PEDIDO_ITEM_LISTA.First(p => p.ID_LGE_PEDIDO_ITEM == IdItemPedido);

                    pedidoItem.QTDE_CANCELADA = pedidoItem.QTDE_ENTREGAR;
                    pedidoItem.QTDE_ENTREGAR = 0;

                    List<LGE_PEDIDO_STATUS> PedidoStatus = this.repositorioPedido.GetStatusPedido();

                    KeyValuePair<string, string> DomainStatusCancelado = LX_STATUS_LGE_PEDIDO.GetValues().First(p => p.Value.Equals("Cancelado"));
                    int idDomainStatusCancelado = Convert.ToInt16(DomainStatusCancelado.Key);

                    var primeiroStatusCancelado = PedidoStatus.First(p => p.LX_STATUS_LGE_PEDIDO == idDomainStatusCancelado);

                    if (pedidoItem.LGE_PEDIDO_STATUS.LX_STATUS_LGE_PEDIDO != Convert.ToInt16(DomainStatusCancelado.Key))
                    {
                        pedidoItem.LGE_PEDIDO_STATUS = primeiroStatusCancelado;
                    }                    

                    this.repositorioPedido.SaveChanges();
                }

                return VerificarStatusPedido(IdPedido);
            }
        }

        public bool VerificarStatusPedido(int IdPedido)
        {
            lock (syncLock)
            {
                using (var contexto = new LinxOperacional())
                {
                    this.repositorioPedido = new RepositorioPedido(contexto);
                    LGE_PEDIDO pedido = this.repositorioPedido.GetPedido(IdPedido);

                    int menorIndiceStatus = pedido.LGE_PEDIDO_ITEM_LISTA.First().LGE_PEDIDO_STATUS.LX_STATUS_LGE_PEDIDO;

                    foreach (var item in pedido.LGE_PEDIDO_ITEM_LISTA)
                    {
                        if (item.LGE_PEDIDO_STATUS.LX_STATUS_LGE_PEDIDO < menorIndiceStatus)
                        {
                            menorIndiceStatus = item.LGE_PEDIDO_STATUS.LX_STATUS_LGE_PEDIDO;
                        }
                    }

                    LGE_PEDIDO_STATUS PedidoStatus = this.repositorioPedido.GetStatusPedido().First(p=>p.LX_STATUS_LGE_PEDIDO == menorIndiceStatus);

                    pedido.LGE_PEDIDO_STATUS = PedidoStatus;

                    this.repositorioPedido.SaveChanges();
                }

                return true;
            }
        }
    }
}
