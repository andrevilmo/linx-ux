using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;

namespace Linx.Operacional.BM.Rules.PedidoEntrada
{    
    public class RegraBaixaPedido
    {
        private static object syncLock = new object();
        private RepositorioPedido repositorioPedido;

        public bool TentaEncerrarPedido(LGE_PEDIDO pedido, LinxOperacional contexto)
        {
            this.repositorioPedido = new RepositorioPedido(contexto);
            bool EncerraPedido = true;
            
            if(pedido != null)
            {
                Int32 DomainStatusPedidoEncerrado = Convert.ToInt32(Domains.LX_STATUS_LGE_PEDIDO.Encerrado.Value);
                LGE_PEDIDO_STATUS statusEncerrado = repositorioPedido.GetPedidoStatusByDomain(DomainStatusPedidoEncerrado);
                LGE_PEDIDO_STATUS statusCancelado = repositorioPedido.GetPedidoStatusByDomain(Convert.ToInt32(Domains.LX_STATUS_LGE_PEDIDO.Cancelado.Value));
                if (statusEncerrado == null) throw new BM.Exceptions.BusinessModelException("Não existe um status encerrado cadastrado. Cadastre o status com o tipo encerrado para que o item do pedido possa ser atualizado.");
                if (statusCancelado == null) throw new BM.Exceptions.BusinessModelException("Não existe um status cancelado cadastrado. Cadastre o status com o tipo cancelado para que o item do pedido possa ser atualizado.");

                foreach (var item in pedido.LGE_PEDIDO_ITEM_LISTA)
	            {
                    if (item.ID_PEDIDO_STATUS != statusEncerrado.ID_PEDIDO_STATUS && item.ID_PEDIDO_STATUS != statusCancelado.ID_PEDIDO_STATUS)
                    {
                        EncerraPedido = false;
                        break;
                    }		 
	            }

                if (EncerraPedido)
                {
                    pedido.ID_PEDIDO_STATUS = statusEncerrado.ID_PEDIDO_STATUS;
                    repositorioPedido.SaveChanges();
                }
                else if (pedido.ID_PEDIDO_STATUS == statusEncerrado.ID_PEDIDO_STATUS && !pedido.ID_PEDIDO_STATUS_ANTERIOR.IsNullOrEmpty())
                {
                    pedido.ID_PEDIDO_STATUS = Convert.ToInt32(pedido.ID_PEDIDO_STATUS_ANTERIOR);
                    repositorioPedido.SaveChanges();
                }
            }
            else 
                throw new BM.Exceptions.BusinessModelException("Pedido não informado!");

            return EncerraPedido;
        }
    }
}
