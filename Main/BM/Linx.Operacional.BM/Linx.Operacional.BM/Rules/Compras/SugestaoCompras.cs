using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Compras
{
	public class SugestaoCompras
	{
		public void GerarPedidosCompraSugeridos(List<SugestaoDeCompra> sugestoes)
		{
			LinxOperacional context = new LinxOperacional();

			foreach (var item in sugestoes)
			{
				LGE_PEDIDO ped = new LGE_PEDIDO();
				LGE_PEDIDO_ITEM pedItem = new LGE_PEDIDO_ITEM();
				context.LGE_PEDIDO.Add(ped);
				context.LGE_PEDIDO_ITEM.Add(pedItem);
			}
		}

		public void GerarPedidoCompra()
		{
			LinxOperacional context = new LinxOperacional();
			LGE_PEDIDO ped = new LGE_PEDIDO();
			ped.DATA_EMISSAO = DateTime.Now;
			ped.ID_CONDICAO_PAGAMENTO = 1;
			ped.ID_DOCUMENTO_TIPO = 1;
			ped.ID_FILIAL_PEDIDO = 1;
			ped.ID_FORNECEDOR = 1;
			ped.ID_GPECON = 1;						
			ped.ID_LOJA = 1;
			ped.ID_OPERACAO_FINALIDADE = 1;
			ped.ID_PEDIDO_STATUS = 1;
			ped.ID_TAB_PRECO = 1;
			ped.LX_MODALIDADE_FRETE = 1;
			ped.NUM_PEDIDO_FORNECEDOR = "";
			ped.NUMERO_PEDIDO = 1;
			ped.OBS = "";
			ped.PORC_ACRESCIMO_PEDIDO = 0;
			ped.PORC_DESCONTO_PEDIDO = 0;
			ped.QTDE_TOTAL_CANCELADA = 0;
			ped.QTDE_TOTAL_ENTREGAR = 0;
			ped.QTDE_TOTAL_PEDIDO = 0;
			ped.VALOR_ACRESCIMO_ITEM = 0;
			ped.VALOR_ACRESCIMO_PEDIDO = 0;
			ped.VALOR_DESCONTO_ITEM = 0;
			ped.VALOR_DESCONTO_PEDIDO = 0;
			ped.VALOR_DESPESA = 0;
			ped.VALOR_ENTREGAR_TOTAL = 0;
			ped.VALOR_FRETE = 0;
			ped.VALOR_ORIGINAL_TOTAL = 0;
			ped.VALOR_SEGURO = 0;

			LGE_PEDIDO_ITEM pedItem = new LGE_PEDIDO_ITEM();
			pedItem.LGE_PEDIDO = ped;
			pedItem.DATA_ENTREGA_LIMITE = DateTime.Now;
			pedItem.DATA_ENTREGA_ORIGINAL = DateTime.Now;
			pedItem.DATA_ENTREGA_PREVISTA = DateTime.Now;
			pedItem.ID_CONDICAO_PAGAMENTO = 0;
			pedItem.ID_LGE_PEDIDO = ped.ID_LGE_PEDIDO;
			pedItem.ID_LGE_PEDIDO_ITEM = 0;
			pedItem.ID_LGE_PEDIDO_ITEM_ORIGEM = 0;			
			pedItem.ID_OPERACAO_FINALIDADE = 0;
			pedItem.ID_PEDIDO_STATUS = 0;
			pedItem.ID_SKU = 0;
			pedItem.ID_STK_DEPOSITO = 0;
			pedItem.NUM_ITEM_FORNECEDOR = "";
			pedItem.PORC_ACRESCIMO_ITEM = 0;
			pedItem.PORC_DESCONTO_ITEM = 0;
			pedItem.PRECO_UNITARIO_BRUTO = 0;
			pedItem.QTDE_CANCELADA = 0;
			pedItem.QTDE_ENTREGAR = 0;
			pedItem.QTDE_PEDIDO = 0;
			pedItem.RATEIO_ACRESCIMO_PEDIDO = 0;
			pedItem.RATEIO_DESCONTO_PEDIDO = 0;
			pedItem.RATEIO_OUTRAS_DESPESAS = 0;
			pedItem.VALOR_ACRESCIMO_ITEM = 0;
			pedItem.VALOR_DESCONTO_ITEM = 0;
			pedItem.VALOR_PEDIDO_BRUTO = 0;
			pedItem.VALOR_PEDIDO_LIQUIDO = 0;

			context.LGE_PEDIDO.Add(ped);
			context.LGE_PEDIDO_ITEM.Add(pedItem);

			context.SaveChanges();
		}
	}

	public class SugestaoDeCompra
	{
		public int idSku;
		public int qtdSugerida;
	}
}