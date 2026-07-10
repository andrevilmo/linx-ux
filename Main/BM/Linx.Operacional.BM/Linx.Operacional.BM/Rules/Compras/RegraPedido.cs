using Linx.Operacional.BM.Rules.PedidoEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Compras
{
    public class RegraPedido
    {
        private RepositorioPedido _repPedido;

        /// <summary>
        /// Construtor da classe RegraPedido
        /// </summary>
        public RegraPedido()
        {
            _repPedido = new RepositorioPedido(new LinxOperacional());
        }

        /// <summary>
        /// Método para retornar os pedidos para calculo de fluxo de caixa
        /// </summary>
        /// <param name="vencimentoInicial">Vencimento Inicial</param>
        /// <param name="vencimentoFinal">Vencimento Final</param>
        /// <param name="lstCnpjFilial">Lista com os CNPJs das Filiais</param>
        /// <param name="lstIdConta">Lista com os Ids das contas</param>
        /// <returns>Lista do tipo PedidoFluxoCaixa</returns>
        public List<PedidoFluxoCaixa> BuscaPedidoFluxoCaixa(DateTime vencimentoInicial, DateTime vencimentoFinal, List<string> lstCnpjFilial)
        {
            List<PedidoFluxoCaixa> lstPedidoFluxoCaixa = new List<PedidoFluxoCaixa>();

            //Busco os ids das filiais

            List<int> lstIdFilial = new List<int>();

            lstIdFilial = _repPedido.GetFiliais(lstCnpjFilial).Select(s => s.ID_PFJ).ToList();

            //Busco os pedidos

            List<LGE_PEDIDO_ITEM> itensPedido = _repPedido.GetItensPedidoFluxoCaixa(lstIdFilial);

            //Busco as condições de pagamento dos itens

            List<int> lstIdCondicaoPagamento = itensPedido.Where(w => w.LGE_PEDIDO.ID_CONDICAO_PAGAMENTO != null).Select(s => (int)s.LGE_PEDIDO.ID_CONDICAO_PAGAMENTO).Distinct().ToList();

            List<LNF_CONDICAO_PAGAMENTO> lstCondicaoPagamento = new List<LNF_CONDICAO_PAGAMENTO>();

            if (lstIdCondicaoPagamento.Any())
                lstCondicaoPagamento = _repPedido.GetCondicaoPagamento(lstIdCondicaoPagamento);

            //Incluo no retorno os pedidos sem condição de pagamento

            lstPedidoFluxoCaixa.AddRange(itensPedido.Where(w => w.LGE_PEDIDO.ID_CONDICAO_PAGAMENTO == null)
                                        .GroupBy(gb => new
                                        {
                                            DataEntrega = gb.DATA_ENTREGA_ORIGINAL,
                                            gb.ID_LGE_PEDIDO,
                                            gb.LGE_PEDIDO.NUMERO_PEDIDO,
                                            gb.LGE_PEDIDO.LJV_LOJA.COD_LOJA,
                                            gb.LGE_PEDIDO.LJV_LOJA.DESC_LOJA,
                                            gb.LGE_PEDIDO.DATA_EMISSAO,
                                            gb.LGE_PEDIDO.TBC_FORNECEDOR.CODIGO_FORNECEDOR,
                                            gb.LGE_PEDIDO.TBC_FORNECEDOR.NOME_FORNECEDOR
                                        })
                                        .Select(s => new PedidoFluxoCaixa
                                        {
                                            ID_LGE_PEDIDO = s.Key.ID_LGE_PEDIDO,
                                            NUMERO_PEDIDO = s.Key.NUMERO_PEDIDO,
                                            COD_LOJA = s.Key.COD_LOJA,
                                            DESC_LOJA = s.Key.DESC_LOJA,
                                            DATA_EMISSAO = s.Key.DATA_EMISSAO,
                                            CODIGO_FORNECEDOR = s.Key.CODIGO_FORNECEDOR,
                                            NOME_FORNECEDOR = s.Key.NOME_FORNECEDOR,
                                            DATA_ENTREGA = s.Key.DataEntrega,
                                            VALOR = s.Sum(sm => sm.VALOR_PEDIDO_LIQUIDO)
                                        }
            ).ToList());

            #region Calculo os encargos se existir e gero o parcelamento

            var pedidosAgrupado = itensPedido.Where(w => w.LGE_PEDIDO.ID_CONDICAO_PAGAMENTO != null
                //&& w.DATA_ENTREGA_ORIGINAL >= vencimentoInicial && w.DATA_ENTREGA_ORIGINAL <= vencimentoFinal
                                                      ).GroupBy(gb => new
            {
                gb.DATA_ENTREGA_ORIGINAL,
                gb.LGE_PEDIDO.ID_CONDICAO_PAGAMENTO,
                gb.ID_LGE_PEDIDO,
                gb.LGE_PEDIDO.NUMERO_PEDIDO,
                gb.LGE_PEDIDO.LJV_LOJA.COD_LOJA,
                gb.LGE_PEDIDO.LJV_LOJA.DESC_LOJA,
                gb.LGE_PEDIDO.DATA_EMISSAO,
                gb.LGE_PEDIDO.TBC_FORNECEDOR.CODIGO_FORNECEDOR,
                gb.LGE_PEDIDO.TBC_FORNECEDOR.NOME_FORNECEDOR
            })
            .Select(s => new
            {
                s.Key.DATA_ENTREGA_ORIGINAL,
                s.Key.ID_CONDICAO_PAGAMENTO,
                ID_LGE_PEDIDO = s.Key.ID_LGE_PEDIDO,
                NUMERO_PEDIDO = s.Key.NUMERO_PEDIDO,
                COD_LOJA = s.Key.COD_LOJA,
                DESC_LOJA = s.Key.DESC_LOJA,
                DATA_EMISSAO = s.Key.DATA_EMISSAO,
                CODIGO_FORNECEDOR = s.Key.CODIGO_FORNECEDOR,
                NOME_FORNECEDOR = s.Key.NOME_FORNECEDOR,
                VALOR_PEDIDO_LIQUIDO = s.Sum(sm => sm.VALOR_PEDIDO_LIQUIDO)
            });

            foreach (var item in pedidosAgrupado)
            {
                List<PedidoFluxoCaixa> lstPedidosFluxoTemporario = new List<PedidoFluxoCaixa>();

                DateTime dataAtual = item.DATA_ENTREGA_ORIGINAL;

                //Busco a condição de pagamento

                var condicao = lstCondicaoPagamento.Where(w => w.ID_CONDICAO_PAGAMENTO == item.ID_CONDICAO_PAGAMENTO).FirstOrDefault();

                if (condicao.LX_TIPO_COND_PAGTO == 1)
                {
                    PedidoFluxoCaixa pedidoFluxoTemporario = new PedidoFluxoCaixa();

                    pedidoFluxoTemporario.DATA_ENTREGA = item.DATA_ENTREGA_ORIGINAL;
                    pedidoFluxoTemporario.ID_LGE_PEDIDO = item.ID_LGE_PEDIDO;
                    pedidoFluxoTemporario.NUMERO_PEDIDO = item.NUMERO_PEDIDO;
                    pedidoFluxoTemporario.COD_LOJA = item.COD_LOJA;
                    pedidoFluxoTemporario.DESC_LOJA = item.DESC_LOJA;
                    pedidoFluxoTemporario.DATA_EMISSAO = item.DATA_EMISSAO;
                    pedidoFluxoTemporario.CODIGO_FORNECEDOR = item.CODIGO_FORNECEDOR;
                    pedidoFluxoTemporario.NOME_FORNECEDOR = item.NOME_FORNECEDOR;

                    //Tratamento para encargo

                    int mediaDias = 1;

                    decimal porcEncargoTotal = condicao.PORC_ENCARGO_TOTAL == null ? 0 : (decimal)condicao.PORC_ENCARGO_TOTAL;

                    decimal porcEncargoMensal = Convert.ToDecimal((Math.Pow(Math.Pow(Convert.ToDouble((1 + (porcEncargoTotal / 100))), Convert.ToDouble((decimal)1 / 30)), Convert.ToDouble(mediaDias)) - 1)) * 100;

                    pedidoFluxoTemporario.VALOR = item.VALOR_PEDIDO_LIQUIDO + (item.VALOR_PEDIDO_LIQUIDO * porcEncargoMensal / 100);

                    lstPedidosFluxoTemporario.Add(pedidoFluxoTemporario);

                }
                else if (condicao.LX_TIPO_COND_PAGTO == 2)
                {
                    var vencimento = item.DATA_ENTREGA_ORIGINAL;

                    for (int i = 1; i <= condicao.PARCELAS_SUGESTAO; i++)
                    {
                        PedidoFluxoCaixa pedidoFluxoTemporario = new PedidoFluxoCaixa();

                        //Se existe valor no dias_vencimento e for a primeira parcela, 
                        //adiciono os dias no vencimento para depois calcular as parcelas
                        if (condicao.DIAS_VENCIMENTO > 0 && i <= 1)
                            vencimento = vencimento.AddDays(condicao.DIAS_VENCIMENTO ?? 0);

                        //Variavel para tratamento de dia
                        var mesInicial = vencimento.Month;

                        //Adiono os dias entre parcelas para geração do vencimento
                        vencimento = vencimento.AddDays(condicao.DIAS_ENTRE_PARCELAS ?? 0);
                        var mesAtual = vencimento.Month;
                        var vencimentoAtual = vencimento;

                        //Tratamento dia fixo
                        if (condicao.DIA_FIXO_1 != 0 || condicao.DIA_FIXO_2 != 0)
                        {
                            int qtdeDiaFixo1 = 0;
                            int qtdeDiaFixo2 = 0;
                            int diaAlterar = 0;

                            if (condicao.DIA_FIXO_1 > 0)
                                qtdeDiaFixo1 = (condicao.DIA_FIXO_1 ?? 0) - vencimento.Day < 0 ? -1 * ((condicao.DIA_FIXO_1 ?? 0) - vencimento.Day) : (condicao.DIA_FIXO_1 ?? 0) - vencimento.Day;

                            if (condicao.DIA_FIXO_2 > 0)
                                qtdeDiaFixo2 = (condicao.DIA_FIXO_2 ?? 0) - vencimento.Day < 0 ? -1 * ((condicao.DIA_FIXO_2 ?? 0) - vencimento.Day) : (condicao.DIA_FIXO_2 ?? 0) - vencimento.Day;

                            //Se o dia fixo 2 não for preenchido, utilizo o dia fixo 1, caso contrario verifico qual é o dia mais proximo e utilizo o mesmo. 
                            //Quando validação for igual utilizo o maior dia
                            if ((condicao.DIA_FIXO_2 ?? 0) == 0 && (condicao.DIA_FIXO_1 ?? 0) > 0)
                                diaAlterar = (condicao.DIA_FIXO_1 ?? 0);
                            else
                            {
                                if (qtdeDiaFixo1 < qtdeDiaFixo2)
                                    diaAlterar = (condicao.DIA_FIXO_1 ?? 0);
                                else
                                    diaAlterar = (condicao.DIA_FIXO_2 ?? 0);
                            }

                            //Se o dia alterar for > 0, efetuo tratamento para vencimento
                            if (diaAlterar > 0)
                            {
                                //Gero lista com os meses que tem 30 dias ou menos
                                List<int> lstMeses30dias = new List<int>() { 2, 4, 6, 9, 11 };

                                //Se o dia para alterar for maior que 28 e o mês não tiver o dia, jogo para o ultimo dia 
                                //do mês
                                if (lstMeses30dias.Contains(vencimento.Month) && diaAlterar > 28)
                                    vencimento = DateTime.Parse(string.Format("{0}/{1}/{2}", "01", (vencimento.Month < 10 ? "0" + vencimento.Month.ToString() : vencimento.Month.ToString()), vencimento.Year.ToString())).AddMonths(1).AddDays(-1);
                                else
                                    vencimento = DateTime.Parse(string.Format("{0}/{1}/{2}", (diaAlterar < 10 ? "0" + diaAlterar.ToString() : diaAlterar.ToString()), (vencimento.Month < 10 ? "0" + vencimento.Month.ToString() : vencimento.Month.ToString()), vencimento.Year.ToString()));

                                //Tratamento para vencimento, caso a emissao seja 31/08/2014 com 30 dias entre as parcelas
                                //com dia fixo no dia 01, o sistema sem o tratamento abaixo iria gerar a data em 01/09/2014
                                //porém esse vencimento é 1 dias depois da emissão, não respeitando os 30 dias entre as parcelas.
                                //Com o tratamento abaixo o sistema gera o vencimento em 01/10/2014 que é o proximo dia fixo.
                                if (mesInicial == mesAtual && vencimentoAtual != vencimento)
                                    vencimento = vencimento.AddMonths(1);

                                if (vencimento.Month == mesAtual && vencimentoAtual != vencimento)
                                    vencimento = vencimento.AddMonths(1);
                            }
                        }

                        //Tratamento para dia semana fixo.

                        if (condicao.DIA_FIXO_SEMANA_1 != 0 || condicao.DIA_FIXO_SEMANA_2 != 0)
                        {
                            int qtdeDiaFixoSemana1 = 0;
                            int qtdeDiaFixoSemana2 = 0;
                            DayOfWeek diaSemanaAlterar = 0;
                            DayOfWeek diaSemana1 = new DayOfWeek();
                            DayOfWeek diaSemana2 = new DayOfWeek();

                            Linx.Business.Common.Calendario calendario = new Business.Common.Calendario();

                            //Busco o dia da semana e a qtde para o dia proximo
                            if (condicao.DIA_FIXO_SEMANA_1 > 0)
                            {
                                diaSemana1 = calendario.GetDiaDaSemana((condicao.DIA_FIXO_SEMANA_1 ?? 0));
                                qtdeDiaFixoSemana1 = (condicao.DIA_FIXO_SEMANA_1 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek) < 0
                                    ? -1 * ((condicao.DIA_FIXO_SEMANA_1 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek))
                                    : (condicao.DIA_FIXO_SEMANA_1 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek);
                            }

                            if ((condicao.DIA_FIXO_SEMANA_2 ?? 0) > 0)
                            {
                                diaSemana2 = calendario.GetDiaDaSemana((condicao.DIA_FIXO_SEMANA_2 ?? 0));
                                qtdeDiaFixoSemana2 = (condicao.DIA_FIXO_SEMANA_2 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek) < 0
                                    ? -1 * ((condicao.DIA_FIXO_SEMANA_2 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek))
                                    : (condicao.DIA_FIXO_SEMANA_2 ?? 0) - calendario.GetDiaDaSemanaNumero(vencimento.DayOfWeek);
                            }

                            //Busco o dia da semana que vou utilizar para gerar o proximo vencimento
                            if ((condicao.DIA_FIXO_SEMANA_2 ?? 0) <= 0 && (condicao.DIA_FIXO_SEMANA_1 ?? 0) > 0)
                                diaSemanaAlterar = diaSemana1;
                            else
                            {
                                if (qtdeDiaFixoSemana1 < qtdeDiaFixoSemana2)
                                    diaSemanaAlterar = diaSemana1;
                                else
                                    diaSemanaAlterar = diaSemana2;
                            }

                            //Verifico se o dia do vencimento é igual ao da semana, caso contrário
                            //busco o dia mais proximo, antes ou depois

                            if (vencimento.DayOfWeek != diaSemanaAlterar)
                            {
                                DateTime dataVerificar = vencimento;
                                int qtdeDiasApos = 0;
                                int qtdeDiasAntes = 0;

                                //Verifico dia após o vencimento
                                while (dataVerificar.DayOfWeek != diaSemanaAlterar)
                                {
                                    dataVerificar = dataVerificar.AddDays(1);
                                    qtdeDiasApos += 1;
                                }

                                //Calculo dia antes do vencimento
                                dataVerificar = vencimento;

                                while (dataVerificar.DayOfWeek != diaSemanaAlterar)
                                {
                                    dataVerificar = dataVerificar.AddDays(-1);
                                    qtdeDiasAntes += 1;
                                }

                                if (qtdeDiasApos < qtdeDiasAntes)
                                    vencimento = vencimento.AddDays(qtdeDiasApos);
                                else
                                {
                                    //Verifico se o vencimento não retornou um mês, se retornou uso o 
                                    //próximo dia da semana
                                    mesAtual = vencimento.Month;

                                    vencimento = vencimento.AddDays(-1 * qtdeDiasAntes);

                                    if (mesAtual != vencimento.Month)
                                        vencimento = vencimento.AddDays(qtdeDiasAntes + qtdeDiasApos);
                                }
                            }
                        }

                        pedidoFluxoTemporario.DATA_ENTREGA = vencimento;

                        pedidoFluxoTemporario.ID_LGE_PEDIDO = item.ID_LGE_PEDIDO;
                        pedidoFluxoTemporario.NUMERO_PEDIDO = item.NUMERO_PEDIDO;
                        pedidoFluxoTemporario.COD_LOJA = item.COD_LOJA;
                        pedidoFluxoTemporario.DESC_LOJA = item.DESC_LOJA;
                        pedidoFluxoTemporario.DATA_EMISSAO = item.DATA_EMISSAO;
                        pedidoFluxoTemporario.CODIGO_FORNECEDOR = item.CODIGO_FORNECEDOR;
                        pedidoFluxoTemporario.NOME_FORNECEDOR = item.NOME_FORNECEDOR;

                        pedidoFluxoTemporario.VALOR = Math.Round(item.VALOR_PEDIDO_LIQUIDO / (condicao.PARCELAS_SUGESTAO ?? 1), 2);

                        lstPedidosFluxoTemporario.Add(pedidoFluxoTemporario);

                    }

                    //Tratamento para encargos

                    int mediaDias = 0;
                    if (lstPedidosFluxoTemporario.Any())
                    {
                        int totalDias = 0;

                        foreach (var parcela in lstPedidosFluxoTemporario)
                        {
                            totalDias += (int)Convert.ToDateTime(parcela.DATA_ENTREGA).Subtract(dataAtual).TotalDays;
                        }

                        mediaDias = totalDias / lstPedidosFluxoTemporario.Count();

                        decimal porcEncargoTotal = condicao.PORC_ENCARGO_TOTAL == null ? 0 : (decimal)condicao.PORC_ENCARGO_TOTAL;

                        decimal porcEncargoMensal = Convert.ToDecimal((Math.Pow(Math.Pow(Convert.ToDouble((1 + (porcEncargoTotal / 100))), Convert.ToDouble((decimal)1 / 30)), Convert.ToDouble(mediaDias)) - 1)) * 100;

                        //Gero as parcelas em objeto

                        foreach (var parcela in lstPedidosFluxoTemporario)
                        {
                            parcela.VALOR = parcela.VALOR + (parcela.VALOR * porcEncargoMensal / 100);
                        }
                    }
                }
                else
                {

                    var parcelas = new List<object>();
                    var contador = 0;

                    //Tratamento para encargos

                    int mediaDias = 0;

                    if (condicao.LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA.Any())
                        mediaDias = condicao.LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA.Sum(sm => (sm.DIAS_PARCELA ?? 0)) / condicao.LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA.Count();

                    decimal porcEncargoTotal = condicao.PORC_ENCARGO_TOTAL == null ? 0 : (decimal)condicao.PORC_ENCARGO_TOTAL;

                    decimal porcEncargoMensal = Convert.ToDecimal((Math.Pow(Math.Pow(Convert.ToDouble((1 + (porcEncargoTotal / 100))), Convert.ToDouble((decimal)1 / 30)), Convert.ToDouble(mediaDias)) - 1)) * 100;

                    //tratamento para condição de pagamento variavel sem parcelas informadas

                    foreach (var parc in condicao.LNF_CONDICAO_PAGAMENTO_PARCELA_LISTA)
                    {
                        PedidoFluxoCaixa pedidoFluxoTemporario = new PedidoFluxoCaixa();

                        var vencimento = dataAtual.AddDays((parc.DIAS_PARCELA ?? 0));

                        decimal valor = Math.Round(item.VALOR_PEDIDO_LIQUIDO * ((decimal)parc.PORC_PARCELA / 100), 2);

                        pedidoFluxoTemporario.DATA_ENTREGA = vencimento;

                        pedidoFluxoTemporario.ID_LGE_PEDIDO = item.ID_LGE_PEDIDO;
                        pedidoFluxoTemporario.NUMERO_PEDIDO = item.NUMERO_PEDIDO;
                        pedidoFluxoTemporario.COD_LOJA = item.COD_LOJA;
                        pedidoFluxoTemporario.DESC_LOJA = item.DESC_LOJA;
                        pedidoFluxoTemporario.DATA_EMISSAO = item.DATA_EMISSAO;
                        pedidoFluxoTemporario.CODIGO_FORNECEDOR = item.CODIGO_FORNECEDOR;
                        pedidoFluxoTemporario.NOME_FORNECEDOR = item.NOME_FORNECEDOR;

                        pedidoFluxoTemporario.VALOR = valor + valor * (porcEncargoMensal / 100);

                        contador++;

                        lstPedidosFluxoTemporario.Add(pedidoFluxoTemporario);
                    }
                }

                if (lstPedidosFluxoTemporario.Any())
                    lstPedidoFluxoCaixa.AddRange(lstPedidosFluxoTemporario.Where(w => w.DATA_ENTREGA >= vencimentoInicial && w.DATA_ENTREGA <= vencimentoFinal));
            }

            #endregion

            //Agrupo os dados existentes

            lstPedidoFluxoCaixa = lstPedidoFluxoCaixa.Where(w => w.DATA_ENTREGA >= vencimentoInicial && w.DATA_ENTREGA <= vencimentoFinal).GroupBy(gb => new
            {
                gb.DATA_ENTREGA,
                gb.ID_LGE_PEDIDO,
                gb.NUMERO_PEDIDO,
                gb.COD_LOJA,
                gb.DESC_LOJA,
                gb.DATA_EMISSAO,
                gb.CODIGO_FORNECEDOR,
                gb.NOME_FORNECEDOR
            })
            .Select(s => new PedidoFluxoCaixa
            {
                ID_LGE_PEDIDO = s.Key.ID_LGE_PEDIDO,
                NUMERO_PEDIDO = s.Key.NUMERO_PEDIDO,
                COD_LOJA = s.Key.COD_LOJA,
                DESC_LOJA = s.Key.DESC_LOJA,
                DATA_EMISSAO = s.Key.DATA_EMISSAO,
                CODIGO_FORNECEDOR = s.Key.CODIGO_FORNECEDOR,
                NOME_FORNECEDOR = s.Key.NOME_FORNECEDOR,
                DATA_ENTREGA = s.Key.DATA_ENTREGA.Date,
                VALOR = s.Sum(sm => sm.VALOR)
            }).ToList();


            return lstPedidoFluxoCaixa;
        }
    }
}
