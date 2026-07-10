using Linx.Operacional.BM.Rules.PedidoEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Estoque
{
    public class RegraRomaneio
    {
        private RepositorioRomaneio repositorioRomaneio = null;
        private LinxOperacional context = null;

        public RegraRomaneio()
        {
            this.context = new LinxOperacional();
            this.repositorioRomaneio = new RepositorioRomaneio(this.context);
        }

        public void ExcluiRomaneioNotaFiscal(Int64 idStkRomaneio, int idLinx)
        {
            //parâmetro idLinx está inútil, pois não podemos filtrar pelo ID_LINX pois no operacional é só um ID_LINX
            try
            {
                var romaneioNf = repositorioRomaneio.GetRomaneioNf(idStkRomaneio);

                if (romaneioNf != null)
                {
                    // exclui romaneionf e romaneio                
                    repositorioRomaneio.Delete(romaneioNf);
                    repositorioRomaneio.SaveChanges();

                    var romaneio = repositorioRomaneio.GetRomaneio(romaneioNf.ID_STK_ROMANEIO);

                    if (romaneio != null)
                    {
                        while (romaneio.STK_ROMANEIO_ITEM_LISTA.Count() > 0)
                        {
                            var itemRomaneio = romaneio.STK_ROMANEIO_ITEM_LISTA.FirstOrDefault();

                            while (itemRomaneio.STK_ROMANEIO_ITEM_RELACAO_LISTA.Count() > 0)
                            {
                                var itemRomaneioRelacao = itemRomaneio.STK_ROMANEIO_ITEM_RELACAO_LISTA.FirstOrDefault();
                                if (itemRomaneioRelacao.NTS_ROMANEIO_ITEM_RELACIONADO != null)
                                    itemRomaneioRelacao.NTS_ROMANEIO_ITEM_RELACIONADO.QTDE_ROMANEIO_ITEM_RETORNADO = itemRomaneioRelacao.NTS_ROMANEIO_ITEM_RELACIONADO.QTDE_ROMANEIO_ITEM_RETORNADO - itemRomaneioRelacao.QTDE_ROMANEIO_ITEM;
                            }

                            if (itemRomaneio.ID_LGE_PEDIDO_ITEM != null)
                                new RegraBaixaItemPedido().BaixarItemPedidoPorIdLgePedidoItem((int)itemRomaneio.ID_LGE_PEDIDO_ITEM, itemRomaneio.QTDE_ROMANEIO_ITEM * -1, this.context);
                            repositorioRomaneio.Delete(itemRomaneio);
                        }

                        repositorioRomaneio.Delete(romaneio);
                        repositorioRomaneio.SaveChanges();
                    }                    
                }
            }
            catch (Exception err)
            {                
                throw new Exceptions.BusinessModelException("Não foi possível excluir o Romaneio da Nota Fiscal. Erro: " + err.Message);
            }
        }

    }
}
